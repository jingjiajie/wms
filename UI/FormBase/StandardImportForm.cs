using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using unvell.ReoGrid;
using WMS.DataAccess;
using System.Reflection;
using System.Data.Entity;
using System.Threading;
using System.Data.SqlClient;

namespace WMS.UI
{
    public partial class StandardImportForm<TargetClass> : Form where TargetClass:class,new()
    {
        class KeySQL
        {
            public string Key = null;
            public string SQL = null;
        }

        private KeyName[] importVisibleKeyNames = null;
        private Func<List<TargetClass>, Dictionary<string, string[]>, bool> listImportHandler = null;
        private Action importFinishedCallback = null;

        //Key和列号的对应关系
        private Dictionary<string, int> keyColumn = new Dictionary<string, int>();
        private List<KeySQL> keyDefaultValueSQL = new List<KeySQL>();

        //public StandardImportForm(KeyName[] keyNames, Func<TargetClass[], Dictionary<string, string[]>, bool> importHandler,Action importFinishedCallback,string formTitle = "导入信息")
        //{
        //    InitializeComponent();
        //    this.importVisibleKeyNames = (from kn in keyNames where kn.ImportVisible == true select kn).ToArray(); ;
        //    this.importListener = importHandler;
        //    this.importFinishedCallback = importFinishedCallback;
        //    this.Text = formTitle;
        //}

        public StandardImportForm(KeyName[] keyNames, Func<List<TargetClass>, Dictionary<string, string[]>, bool> importHandler, Action importFinishedCallback, string formTitle = "导入信息")
        {
            InitializeComponent();
            this.importVisibleKeyNames = (from kn in keyNames where kn.ImportVisible == true select kn).ToArray(); ;
            this.listImportHandler = importHandler;
            this.importFinishedCallback = importFinishedCallback;
            this.Text = formTitle;
        }

        private void StandardImportForm_Load(object sender, EventArgs e)
        {
            this.comboBoxImeMode.SelectedIndex = 0;
            this.Icon = System.Drawing.Icon.FromHandle(Properties.Resources._20180114034630784_easyicon_net_64.GetHicon());
            this.InitReoGridImport();
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            FormLoading formLoading = new FormLoading("正在导入，请稍后...");
            formLoading.Show();
            var worksheet = this.reoGridControlMain.Worksheets[0];
            var result = this.MakeObjectByReoGridImport<TargetClass>(out string errorMessage);
            if (result == null)
            {
                formLoading.Close();
                MessageBox.Show(errorMessage,"提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            List<TargetClass> newObjs = result.ToList();
            if (newObjs.Count == 0)
            {
                formLoading.Close();
                MessageBox.Show("未导入任何数据！","提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Dictionary<string, string[]> unImportedColumns = new Dictionary<string, string[]>();
            for(int i = 0; i < this.importVisibleKeyNames.Length; i++)
            {
                //如果在导入窗口中可见的列设置为不导入，则加入未导入列表中
                if(this.importVisibleKeyNames[i].Import == false)
                {
                    unImportedColumns.Add(this.importVisibleKeyNames[i].Key, this.GetColumn(i, newObjs.Count));
                }
            }

            if (this.listImportHandler != null && this.listImportHandler(newObjs, unImportedColumns) == false)
            {
                formLoading.Close();
                return;
            }

            new Thread(()=>
            {
                WMSEntities wmsEntities = new WMSEntities();
                //获取wmsEntities.XXX对象
                PropertyInfo propertyOfTargetClass = wmsEntities.GetType().GetProperty(typeof(TargetClass).Name);
                if (propertyOfTargetClass == null)
                {
                    throw new Exception("WMSEntities中不存在" + typeof(TargetClass).Name + "！");
                }
                DbSet<TargetClass> dbSetOfTargetClass = propertyOfTargetClass.GetValue(wmsEntities, null) as DbSet<TargetClass>;
                //获取wmsEntities.XXX.Add()方法
                MethodInfo methodAdd = typeof(DbSet<TargetClass>).GetMethod("Add");

                foreach (var obj in newObjs)
                {
                    methodAdd.Invoke(dbSetOfTargetClass, new object[] { obj });
                }
                try
                {
                    wmsEntities.SaveChanges();
                }
                catch
                {
                    if (!this.IsDisposed)
                    {
                        this.Invoke(new Action(() =>
                        {
                            formLoading.Close();
                            MessageBox.Show("导入失败，请检查网络连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }));
                    }
                    return;
                }
                if (!this.IsDisposed)
                {
                    this.Invoke(new Action(() =>
                    {
                        formLoading.Close();
                        this.importFinishedCallback?.Invoke();
                        MessageBox.Show("导入成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }));
                }
            }).Start();
        }

        private string[] GetColumn(int column,int length)
        {
            string[] results = new string[length];
            var worksheet = this.reoGridControlMain.Worksheets[0];
            for(int line = 0; line < length; line++)
            {
                Cell curCell = worksheet.GetCell(line,column);
                if (curCell == null || curCell.Data == null)
                {
                    results[line] = "";
                    continue;
                }
                results[line] = curCell.Data.ToString();
            }
            return results;
        }

        private void InitReoGridImport()
        {
            //初始化表格
            var worksheet = this.reoGridControlMain.Worksheets[0];
            
            for (int i = 0; i < importVisibleKeyNames.Length; i++)
            {
                string name = importVisibleKeyNames[i].Name;
                string key = importVisibleKeyNames[i].Key;
                worksheet.ColumnHeaders[i].Text = name;
                worksheet.ColumnHeaders[i].Width = (ushort)(name.Length * 10 + 30);

                if (this.keyColumn.ContainsKey(key))
                {
                    this.keyColumn[key] = i;
                }
                else
                {
                    this.keyColumn.Add(key, i);
                }
            }
            worksheet.Columns = importVisibleKeyNames.Length; //限制表的长度
            //设置表格输入形式为文本
            worksheet.SetRangeDataFormat(RangePosition.EntireRange, unvell.ReoGrid.DataFormat.CellDataFormatFlag.Text);
            worksheet.BeforeSelectionRangeChange += Worksheet_BeforeSelectionRangeChange;
        }

        private void Worksheet_BeforeSelectionRangeChange(object sender, unvell.ReoGrid.Events.BeforeSelectionChangeEventArgs e)
        {
            var worksheet = this.reoGridControlMain.CurrentWorksheet;
            this.RefreshDefaultValue(new int[] { worksheet.SelectionRange.Row });
        }

        private void RefreshDefaultValue(int[] rows)
        {
            var worksheet = this.reoGridControlMain.CurrentWorksheet;
            
            using (WMSEntities wmsEntities = new WMSEntities())
            {
                wmsEntities.Database.Connection.Open();
                //遍历行
                foreach (int row in rows)
                {
                    if (IsEmptyLine(worksheet, row)) continue;
                    //将worksheet当前行的值作为SQL参数传进去
                    List<SqlParameter> parameters = new List<SqlParameter>();
                    foreach (var kc in this.keyColumn)
                    {
                        parameters.Add(new SqlParameter(kc.Key, worksheet[row, kc.Value] ?? ""));
                    }

                    //遍历设置了默认值的列
                    foreach (var keySQL in this.keyDefaultValueSQL)
                    {
                        int col = this.keyColumn[keySQL.Key];
                        //如果已经有字，不覆盖
                        if (worksheet.GetCell(row, col) != null && worksheet[row, col] != null && string.IsNullOrWhiteSpace(worksheet[row, col].ToString()) == false) continue;
                        string sql = keySQL.SQL;
                        SqlCommand command = new SqlCommand(sql,(SqlConnection)wmsEntities.Database.Connection);
                        command.Parameters.AddRange(parameters.ToArray());
                        SqlDataReader dataReader = command.ExecuteReader();
                        command.Parameters.Clear();
                        if (dataReader.HasRows == false) continue;
                        dataReader.Read();
                        object value = dataReader.GetValue(0);
                        if (dataReader.Read()) continue; //如果结果不唯一，则不填写默认值
                        worksheet[row, col] = value == null ? "" : value.ToString();
                    }
                }
            }
        }

        public void AddDefaultValue(string key, string sqlValue)
        {
            this.keyDefaultValueSQL.Add(new KeySQL()
            {
                Key=key,
                SQL = sqlValue
            });
        }

        private bool IsEmptyLine(Worksheet worksheet,int row)
        {
            for(int i = 0; i < worksheet.Columns; i++)
            {
                if(worksheet.GetCell(row,i) != null && worksheet[row,i] != null && string.IsNullOrWhiteSpace(worksheet[row, i].ToString()) == false)
                {
                    return false;
                }
            }
            return true;
        }

        private T[] MakeObjectByReoGridImport<T>(out string errorMessage) where T : new()
        {
            var worksheet = this.reoGridControlMain.CurrentWorksheet;
            List<T> result = new List<T>();
            string[] propertyNames = (from kn in importVisibleKeyNames
                                      select kn.Key).ToArray();
            //遍历行
            for (int line = 0; line < worksheet.Rows; line++)
            {
                //如果是空行，则跳过
                if (IsEmptyLine(this.reoGridControlMain, line))
                {
                    continue;
                }

                T newObj = new T();
                result.Add(newObj);
                //遍历列
                for (int col = 0; col < importVisibleKeyNames.Length; col++)
                {
                    //如果字段对导入不可见，或者可见但不导入，则跳过
                    if (importVisibleKeyNames[col].Import == false)
                    {
                        continue;
                    }
                    Cell curCell = worksheet.GetCell(line, col);
                    string cellString; //单元格字符串
                    //如果单元格为null，则认为是零长字符串，否则取单元格数据
                    if (curCell == null || curCell.Data == null)
                    {
                        cellString = "";
                    }
                    else
                    {
                        cellString = curCell.Data.ToString();
                    }
                    if (Utilities.CopyTextToProperty(cellString, propertyNames[col], newObj, importVisibleKeyNames, out errorMessage) == false)
                    {
                        errorMessage = string.Format("行{0}：{1}", line + 1, errorMessage);
                        return null;
                    }
                }
            }
            errorMessage = null;
            return result.ToArray();
        }

        private bool IsEmptyLine(ReoGridControl reoGridControl, int line)
        {
            var worksheet = reoGridControl.Worksheets[0];
            for (int col = 0; col < worksheet.Columns; col++)
            {
                //只要有单元格有字，就判定不为空行。如果所有单元格都是null或者没有字，判定为空行。
                if (worksheet.GetCell(line, col) != null &&
                    worksheet.GetCell(line, col).Data != null &&
                    worksheet.GetCell(line, col).Data.ToString().Length != 0)
                {
                    return false;
                }
            }
            return true;
        }

        private void reoGridControlMain_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void comboBoxImeMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(this.comboBoxImeMode.SelectedIndex == 0)
            {
                this.reoGridControlMain.ImeMode = ImeMode.On;
            }
            else
            {
                this.reoGridControlMain.ImeMode = 0;
            }
        }

        private void reoGridControlMain_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            var worksheet = this.reoGridControlMain.CurrentWorksheet;
            if (worksheet.SelectionRange.IsSingleCell == false) return;
            if (e.Control) return;
            Keys[] dontEditkeys = new Keys[]
            {
                Keys.ControlKey,
                Keys.ShiftKey,
                Keys.Menu,
                Keys.CapsLock,
                Keys.LWin,
                Keys.RWin,
                Keys.Tab,
                Keys.Escape,
                Keys.Up,
                Keys.Down,
                Keys.Left,
                Keys.Right,
                Keys.PageDown,
                Keys.PageUp
            };
            if (dontEditkeys.Contains(e.KeyCode)) return;
            if (worksheet.IsEditing == false)
            {
                worksheet[worksheet.SelectionRange.StartPos] = "";
                worksheet.StartEdit();
            }
        }
    }
}
