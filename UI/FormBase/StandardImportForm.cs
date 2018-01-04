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

namespace WMS.UI
{
    public partial class StandardImportForm<TargetClass> : Form where TargetClass:class,new()
    {
        private KeyName[] keyNames = null;
        private Func<TargetClass[],Dictionary<string,string[]>,bool> importListener = null;
        private Action importFinishedCallback = null;

        public StandardImportForm(KeyName[] keyNames, Func<TargetClass[], Dictionary<string, string[]>, bool> importHandler,Action importFinishedCallback)
        {
            InitializeComponent();
            this.keyNames = keyNames;
            this.importListener = importHandler;
            this.importFinishedCallback = importFinishedCallback;
        }

        private void StandardImportForm_Load(object sender, EventArgs e)
        {
            this.InitReoGridImport();
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            FormLoading formLoading = new FormLoading("正在导入，请稍后...");
            formLoading.Show();
            var worksheet = this.reoGridControlMain.Worksheets[0];
            TargetClass[] newObjs = this.MakeObjectByReoGridImport<TargetClass>(this.reoGridControlMain, this.keyNames, out string errorMessage);
            if (newObjs == null)
            {
                formLoading.Close();
                MessageBox.Show(errorMessage,"提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (newObjs.Length == 0)
            {
                formLoading.Close();
                MessageBox.Show("未导入任何数据！","提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Dictionary<string, string[]> unImportedColumns = new Dictionary<string, string[]>();
            for(int i = 0; i < this.keyNames.Length; i++)
            {
                //如果在导入窗口中可见的列设置为不导入，则加入未导入列表中
                if(this.keyNames[i].ImportVisible == true && this.keyNames[i].Import == false)
                {
                    unImportedColumns.Add(this.keyNames[i].Key, this.GetColumn(i, newObjs.Length));
                }
            }
            if (this.importListener(newObjs, unImportedColumns) == false)
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
            for (int i = 0; i < keyNames.Length; i++)
            {
                worksheet.ColumnHeaders[i].Text = keyNames[i].Name;
                worksheet.ColumnHeaders[i].IsVisible = keyNames[i].ImportVisible;
            }
            worksheet.Columns = keyNames.Length; //限制表的长度
        }

        private T[] MakeObjectByReoGridImport<T>(ReoGridControl reoGridControl, KeyName[] keyNames, out string errorMessage) where T : new()
        {
            var worksheet = reoGridControl.Worksheets[0];
            List<T> result = new List<T>();
            string[] propertyNames = (from kn in keyNames
                                      select kn.Key).ToArray();
            //遍历行
            for (int line = 0; line < worksheet.Rows; line++)
            {
                //如果是空行，则跳过
                if (IsEmptyLine(reoGridControl, line))
                {
                    continue;
                }

                T newObj = new T();
                result.Add(newObj);
                //遍历列
                for (int col = 0; col < keyNames.Length; col++)
                {
                    //如果字段对导入不可见，或者可见但不导入，则跳过
                    if (keyNames[col].ImportVisible == false || keyNames[col].Import == false)
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
                    if (Utilities.CopyTextToProperty(cellString, propertyNames[col], newObj, keyNames, out errorMessage) == false)
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
    }
}
