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
using System.Runtime.InteropServices;

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
        //默认值设置
        private List<KeySQL> keyDefaultValueSQL = new List<KeySQL>();
        //联想设置 key:列号 value:sql列表（要是对于一个列有多个联想sql，就加多项）
        private Dictionary<int, List<string>> columnAssociation = new Dictionary<int, List<string>>();

        private FormAssociate formAssociate = null;
        private TextBox inputTextBox = null;
        private bool canChangeSelectionRange = true;

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
            this.InitReoGridImport();
        }

        private void StandardImportForm_Load(object sender, EventArgs e)
        {
            var worksheet = this.reoGridControlMain.CurrentWorksheet;
            this.Icon = System.Drawing.Icon.FromHandle(Properties.Resources._20180114034630784_easyicon_net_64.GetHicon());

            //寻找ReoGrid自动生成的TextBox。把联想绑到这个上面
            worksheet.StartEdit(); //让它先把TextBox创建出来
            foreach (Control control in this.reoGridControlMain.Controls)
            {
                if (control is TextBox && control.Name == "")
                {
                    inputTextBox = control as TextBox;
                }
            }
            if (this.inputTextBox == null) return;
            inputTextBox.Font = new Font(worksheet.EditingCell.Style.FontName, worksheet.EditingCell.Style.FontSize + 1);
            this.inputTextBox.VisibleChanged += inputTextBox_VisibleChanged;
            this.inputTextBox.PreviewKeyDown += InputTextBox_PreviewKeyDown;
            this.formAssociate = new FormAssociate(inputTextBox);
            this.formAssociate.ClearAssociationSQL();
            if (this.columnAssociation.ContainsKey(worksheet.SelectionRange.Col))
            {
                foreach (string sql in this.columnAssociation[worksheet.SelectionRange.Col])
                {
                    this.formAssociate.AddAssociationSQL(sql);
                }
            }
            this.formAssociate.RefreshAssociation();
            worksheet.EndEdit(new EndEditReason());
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            FormLoading formLoading = new FormLoading("正在导入，请稍后...");
            formLoading.Show();
            var worksheet = this.reoGridControlMain.Worksheets[0];
            worksheet.EndEdit(new EndEditReason());
            RemoveEmptyLines(worksheet);
            var result = this.MakeObjectByReoGridImport<TargetClass>(out int[] emptyLines,out string errorMessage);
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
                    unImportedColumns.Add(this.importVisibleKeyNames[i].Key, this.GetColumn(i,emptyLines));
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
                catch(Exception ex)
                {
                    if (!this.IsDisposed)
                    {
                        this.Invoke(new Action(() =>
                        {
                            formLoading.Close();
                            MessageBox.Show("导入失败，请检查网络连接\n请把如下错误信息反馈给我们：\n"+ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private string[] GetColumn(int column,int[] skipLines)
        {
            List<string> results = new List<string>();
            var worksheet = this.reoGridControlMain.Worksheets[0];
            for(int line = 0; line < worksheet.Rows; line++)
            {
                if (skipLines.Contains(line)) continue;
                Cell curCell = worksheet.GetCell(line,column);
                if (curCell == null || curCell.Data == null)
                {
                    results.Add("");
                    continue;
                }
                results.Add(curCell.Data.ToString());
            }
            return results.ToArray();
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
            //禁止自动分析文本格式
            worksheet.SetSettings(WorksheetSettings.Edit_AutoFormatCell, false);
            //worksheet.SetRangeDataFormat(RangePosition.EntireRange, unvell.ReoGrid.DataFormat.CellDataFormatFlag.Text);
            worksheet.BeforeSelectionRangeChange += Worksheet_BeforeSelectionRangeChange;
            worksheet.CellMouseDown += worksheet_CellMouseDown;
        }

        private void worksheet_CellMouseDown(object sender, unvell.ReoGrid.Events.CellMouseEventArgs e)
        {
            this.canChangeSelectionRange = true;
        }

        private volatile bool isWaitingShow = false;
        private void inputTextBox_VisibleChanged(object sender, EventArgs e)
        {
            Console.WriteLine(InputLanguage.CurrentInputLanguage.LayoutName);
            //只处理上下键选联想项的情况。此时canChangeSelectionRange=false
            if (canChangeSelectionRange == true)
            {
                return;
            }
            //只处理从显示到隐藏。不处理显示
            if (inputTextBox.Visible == true)
            {
                return;
            }
            new Thread(() =>
            {
                if (isWaitingShow) return;
                isWaitingShow = true;
                while (this.reoGridControlMain.CurrentWorksheet.IsEditing) //Reogrid太tm坑了！无奈只能等着它结束编辑，再重新开启
                {
                    Console.WriteLine("Waiting for end edit");
                    Thread.Sleep(1);
                }
                //必须是当前不能移动选区并且编辑框被隐藏，才需要重新显示编辑框开始编辑
                if (!(this.canChangeSelectionRange == false && this.inputTextBox.Visible == false))
                {
                    isWaitingShow = false;
                    return;
                }
                this.Invoke(new Action(() =>
                {
                    //重新启动编辑的时候不触发本事件
                    this.inputTextBox.VisibleChanged -= this.inputTextBox_VisibleChanged;
                    this.reoGridControlMain.CurrentWorksheet.StartEdit();
                    this.inputTextBox.VisibleChanged += this.inputTextBox_VisibleChanged;
                    if (formAssociate.Selected == false)
                    {
                        this.formAssociate.Show();
                    }
                    else
                    {
                        this.formAssociate.Hide();
                    }
                }));
                isWaitingShow = false;
            }).Start();
        }

        private void InputTextBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if(e.Control && e.Alt) //Ctrl+Alt切换中英文输入
            {
                this.checkBoxLockEnglish.Checked = !this.checkBoxLockEnglish.Checked;
            }

            if ((e.KeyCode == Keys.Up || e.KeyCode == Keys.Down ||e.KeyCode == Keys.Enter) && this.formAssociate != null && this.formAssociate.Visible == true)
            {
                this.canChangeSelectionRange = false;
            }
            else
            {
                this.canChangeSelectionRange = true;
            }
        }

        private void Worksheet_BeforeSelectionRangeChange(object sender, unvell.ReoGrid.Events.BeforeSelectionChangeEventArgs e)
        {
            var worksheet = this.reoGridControlMain.CurrentWorksheet;
            if (this.canChangeSelectionRange == false)
            {
                e.IsCancelled = true;
                return;
            }
            if(this.formAssociate != null && this.formAssociate.IsDisposed == false)
            {
                this.formAssociate.Hide();
            }
            //刷新默认值，刷新原选区的第一行和新选区所有行
            List<int> refreshRows = new List<int>();
            refreshRows.Add(worksheet.SelectionRange.Row);
            for(int i = e.StartRow; i <= e.EndRow; i++)
            {
                if (refreshRows.Contains(i)) continue;
                refreshRows.Add(i);
            }
            this.RefreshDefaultValue(refreshRows.ToArray());
            //设置联想框SQL
            if(this.formAssociate != null)
            {
                this.formAssociate.ClearAssociationSQL();
                if (this.columnAssociation.ContainsKey(e.StartCol))
                {
                    foreach (string sql in this.columnAssociation[e.StartCol])
                    {
                        this.formAssociate.AddAssociationSQL(sql);
                    }
                }
            }
        }

        private WMSEntities globalWMSEntities = new WMSEntities();
        private void RefreshDefaultValue(int[] rows)
        {
            //异步刷新默认值，防止卡顿
            new Thread(()=>
            {
                try
                {
                    if (globalWMSEntities.Database.Connection.State == ConnectionState.Closed)
                    {
                        globalWMSEntities.Database.Connection.Open();
                    }
                    var worksheet = this.reoGridControlMain.CurrentWorksheet;
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
                            SqlCommand command = new SqlCommand(sql, (SqlConnection)globalWMSEntities.Database.Connection);
                            command.Parameters.AddRange(parameters.ToArray());
                            SqlDataReader dataReader = command.ExecuteReader();
                            command.Parameters.Clear();
                            if (dataReader.HasRows == false) continue;
                            dataReader.Read();
                            object value = dataReader.GetValue(0);
                            if (dataReader.Read()) continue; //如果结果不唯一，则不填写默认值
                            this.Invoke(new Action(() =>
                            {
                                worksheet[row, col] = value == null ? "" : value.ToString();
                            }));
                        }
                    }
                }
                catch //网络连接错误，就直接返回
                {
                    return;
                }
            }).Start();
        }

        public void AddDefaultValue(string key, string sqlValue)
        {
            this.keyDefaultValueSQL.Add(new KeySQL()
            {
                Key=key,
                SQL = sqlValue
            });
        }

        public void AddAssociation(string key,string sql)
        {
            if (this.keyColumn.ContainsKey(key) == false)
            {
                throw new Exception("导入表格中不包含"+key+"字段！请检查程序");
            }
            int column = this.keyColumn[key];
            if (this.columnAssociation.ContainsKey(column))
            {
                this.columnAssociation[column].Add(sql);
            }
            else
            {
                this.columnAssociation.Add(column, new List<string>(new string[] { sql }));
            }
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

        private void RemoveEmptyLines(Worksheet worksheet)
        {
            Stack<Tuple<int, int>> emptyRowAndCount = new Stack<Tuple<int, int>>(); //空行和连续几行
            bool isTailEmptyLines = true; //末尾的几十行空行不能删
            for (int i = worksheet.Rows - 1; i >= 0; i--)
            {
                //从后往前循环，如果遇到一个非空行，则将开始记录后续空行
                if(isTailEmptyLines && IsEmptyLine(worksheet, i) == false)
                {
                    isTailEmptyLines = false;
                    continue;
                }
                //如果遇到空行，则记录
                if(isTailEmptyLines==false && IsEmptyLine(worksheet, i) == true)
                {
                    //若没有记录，则新增一条
                    if (emptyRowAndCount.Count == 0)
                    {
                        emptyRowAndCount.Push(new Tuple<int, int>(i, 1));
                    }
                    else //若已经有相邻行记录，则更新相邻行记录的count+1，否则新增记录
                    {
                        var topElem = emptyRowAndCount.Pop();
                        Tuple<int, int> newElem;
                        int row = topElem.Item1;
                        int count = topElem.Item2;
                        if(row == i + 1)
                        {
                            newElem = new Tuple<int, int>(i, count + 1);
                            emptyRowAndCount.Push(newElem);
                        }
                        else
                        {
                            newElem = new Tuple<int, int>(i, 1);
                            emptyRowAndCount.Push(topElem);
                            emptyRowAndCount.Push(newElem);
                        }
                    }
                }
            }
            foreach(var item in emptyRowAndCount.Reverse())
            {
                worksheet.DeleteRows(item.Item1, item.Item2);
            }
        }

        private T[] MakeObjectByReoGridImport<T>(out int[] emptyLines,out string errorMessage) where T : new()
        {
            var worksheet = this.reoGridControlMain.CurrentWorksheet;
            List<T> result = new List<T>();
            List<int> emptyLineList = new List<int>();
            string[] propertyNames = (from kn in importVisibleKeyNames
                                      select kn.Key).ToArray();
            //遍历行
            for (int line = 0; line < worksheet.Rows; line++)
            {
                //如果是空行，则跳过
                if (IsEmptyLine(this.reoGridControlMain, line))
                {
                    emptyLineList.Add(line);
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
                        emptyLines = emptyLineList.ToArray();
                        return null;
                    }
                }
            }
            errorMessage = null;
            emptyLines = emptyLineList.ToArray();
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

        public void AddButton(string buttonText,Image image,Action callback)
        {
            AddButton(new string[] { buttonText }, new Image[] { image }, new Action[] { callback });
        }

        public void AddButton(string[] buttonTexts,Image[] images,Action[] callbacks)
        {
            int oriColumnCount = this.tableLayoutPanelTop.ColumnCount;
            this.tableLayoutPanelTop.ColumnCount += buttonTexts.Length;
            for(int i = 0; i < buttonTexts.Length; i++)
            {
                Button button = new Button();
                button.Text = buttonTexts[i];
                button.Width = button.Text.Length * 15 + 20;
                if (images[i] != null)
                {
                    button.Image = images[i];
                    button.ImageAlign = ContentAlignment.MiddleLeft;
                    button.TextAlign = ContentAlignment.MiddleRight;
                    button.Width += button.Image.Width;
                }
                button.Dock = DockStyle.Fill;
                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.BorderSize = 0;
                button.Font = this.buttonImport.Font;
                button.Margin = new Padding(3, 3, 3, 3);
                if (callbacks.Length > i && callbacks[i] != null)
                {
                    Action callback = callbacks[i];
                    button.Click += (obj, e) =>
                    {
                        callback();
                    };
                }
                this.tableLayoutPanelTop.ColumnStyles.Insert(oriColumnCount + i - 1, new ColumnStyle(SizeType.Absolute, button.Width));
                this.tableLayoutPanelTop.Controls.Add(button, oriColumnCount + i - 1, 0);
            }
        }
        
        private void reoGridControlMain_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            var worksheet = this.reoGridControlMain.CurrentWorksheet;
            if (worksheet.SelectionRange.IsSingleCell == false) return;

            if (e.Control)
            {
                if (e.Alt) //Ctrl + Alt切换输入中英文
                {
                    this.checkBoxLockEnglish.Checked = !this.checkBoxLockEnglish.Checked;
                }
                return;
            }

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
                Keys.PageUp,
                Keys.Enter
            };
            if (dontEditkeys.Contains(e.KeyCode)) return;
            if (worksheet.IsEditing == false)
            {
                worksheet[worksheet.SelectionRange.StartPos] = "";
                worksheet.StartEdit();
            }
        }

        private void worksheet_BeforeCellKeyDown_Cancel(object sender, unvell.ReoGrid.Events.BeforeCellKeyDownEventArgs e)
        {
            e.IsCancelled = true;
        }

        private void StandardImportForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.formAssociate != null && this.formAssociate.IsDisposed == false)
            {
                this.formAssociate.Close();
            }
        }

        public void PushData<T>(T[] data,Dictionary<string,string> keyConvert = null,bool fillDefaultValue = false)
        {
            var worksheet = this.reoGridControlMain.CurrentWorksheet;
            this.RemoveEmptyLines(worksheet);
            int firstEmptyLine = FindFirstEmptyLine();
            int totalLines = worksheet.Rows;
            if(totalLines < firstEmptyLine + data.Length)
            {
                worksheet.Rows = firstEmptyLine + data.Length;
            }
            //遍历传入对象
            int curLine = firstEmptyLine;
            foreach (T obj in data)
            {
                object[] columns = Utilities.GetValuesByPropertieNames(obj, (from kn in importVisibleKeyNames select keyConvert == null ? kn.Key : (keyConvert.ContainsKey(kn.Key) ? keyConvert[kn.Key] : kn.Key)).ToArray(),false);
                for (int j = 0; j < columns.Length; j++)
                {
                    //多选模式则空出第一列，放置选择框
                    if (columns[j] == null) continue;
                    worksheet.Cells[curLine, j].DataFormat = unvell.ReoGrid.DataFormat.CellDataFormatFlag.Text;
                    string text = null;
                    if (importVisibleKeyNames[j].Translator != null)
                    {
                        text = importVisibleKeyNames[j].Translator(columns[j]).ToString();
                    }
                    else
                    {
                        if (columns[j] is decimal || columns[j] is decimal?)
                        {
                            text = Utilities.DecimalToString((decimal)columns[j]);
                        }
                        else
                        {
                            text = columns[j].ToString();
                        }
                    }
                    worksheet[curLine, j] = text;
                }
                if(fillDefaultValue) this.RefreshDefaultValue(new int[] { curLine });
                curLine++;
            }
        }

        private int FindFirstEmptyLine()
        {
            var worksheet = this.reoGridControlMain.CurrentWorksheet;
            for (int i = 0; i < worksheet.Rows; i++)
            {
                if (IsEmptyLine(worksheet, i)) return i;
            }
            return -1;
        }

        private void checkBoxLockEnglish_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxLockEnglish.Checked)
            {
                this.reoGridControlMain.ImeMode = ImeMode.Disable;
                this.inputTextBox.ImeMode = ImeMode.Disable;
            }
            else
            {
                this.reoGridControlMain.ImeMode = ImeMode.On;
                this.inputTextBox.ImeMode = ImeMode.On;
            }
        }

        private void tableLayoutPanelTop_Paint(object sender, PaintEventArgs e)
        {

        }

    }

    public partial class Utilities
    {
        public static bool GetPersonByNameAmbiguous(string name,out Person person,out string errorMessage,WMSEntities wmsEntities = null)
        {
            if (wmsEntities == null) wmsEntities = new WMSEntities();
            //如果输入的名字是空的，直接抛出异常。这儿不允许传入空的
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new Exception("GetPersonByNameAmbiguous()函数不允许传入空的零件名字（代号）！空格也不行！请使用string.IsNullOrWhiteSpace()自行判空");
            }
            //首先精确查询，如果没有，再模糊查询
            person = (from p in wmsEntities.Person
                      where p.Name == name
                      select p).FirstOrDefault();
            //如果搜到了，直接返回
            if(person != null)
            {
                errorMessage = null;
                return true;
            }
            //如果没搜到，模糊搜索
            Person[] persons = (from p in wmsEntities.Person
                              where p.Name.Contains(name)
                              select p).ToArray();
            //如果模糊搜索也没搜到，提示错误
            if(persons.Length == 0)
            {
                person = null;
                errorMessage = "未找到人员：" + name;
                return false;
            }
            //正好一个就太好了，直接返回这一个
            if(persons.Length == 1)
            {
                person = persons[0];
                errorMessage = null;
                return true;
            }
            else //如果搜到了多个，那就得让用户选是哪一个了
            {
                Person selectedPerson =
                       FormChooseAmbiguousPerson.ChoosePerson(
                       persons,
                       name);
                if (selectedPerson == null)
                {
                    errorMessage = "用户取消了导入";
                    return false;
                }
                else
                {
                    person = selectedPerson;
                    errorMessage = null;
                    return true;
                }
            }
        }

        public static bool GetSupplyOrComponentAmbiguous(string supplyNoOrComponentName, out DataAccess.Component component, out Supply supply, out string errorMessage, int supplierID = -1, WMSEntities wmsEntities = null)
        {
            if (wmsEntities == null) wmsEntities = new WMSEntities();
            //如果输入的名字是空的，直接抛出异常。这儿不允许传入空的
            if (string.IsNullOrWhiteSpace(supplyNoOrComponentName))
            {
                throw new Exception("GetSupplyOrComponent()函数不允许传入空的零件名字（代号）！空格也不行！请使用string.IsNullOrWhiteSpace()自行判空");
            }
            //首先精确查询，如果没有，再模糊查询
            component = (from c in wmsEntities.Component
                         where c.Name == supplyNoOrComponentName
                         && (from s in wmsEntities.Supply
                             where s.ComponentID == c.ID
                             && s.ProjectID == GlobalData.ProjectID
                             && s.WarehouseID == GlobalData.WarehouseID
                             && s.SupplierID == (supplierID == -1 ? s.SupplierID : supplierID)
                             select s).Count() > 0
                         select c).FirstOrDefault();
            supply = (from s in wmsEntities.Supply
                      where s.No == supplyNoOrComponentName
                      && s.ProjectID == GlobalData.ProjectID
                      && s.WarehouseID == GlobalData.WarehouseID
                      && s.SupplierID == (supplierID == -1 ? s.SupplierID : supplierID)
                      && s.IsHistory == 0
                      select s).FirstOrDefault();
            if (component == null && supply == null)
            {
                //模糊查询供货
                Supply[] supplies = (from s in wmsEntities.Supply
                                     where s.No.Contains(supplyNoOrComponentName)
                                     && s.ProjectID == GlobalData.ProjectID
                                     && s.WarehouseID == GlobalData.WarehouseID
                                     && s.SupplierID == (supplierID == -1 ? s.SupplierID : supplierID)
                                     && s.IsHistory == 0
                                     select s).ToArray();
                //模糊查询零件
                DataAccess.Component[] components = (from c in wmsEntities.Component
                                                     where c.Name.Contains(supplyNoOrComponentName)
                                                     && (from s in wmsEntities.Supply
                                                         where s.ComponentID == c.ID
                                                         && s.ProjectID == GlobalData.ProjectID
                                                         && s.WarehouseID == GlobalData.WarehouseID
                                                         && s.SupplierID == (supplierID == -1 ? s.SupplierID : supplierID)
                                                         select s).Count() > 0
                                                     select c).ToArray();

                if (supplies.Length + components.Length == 0)
                {
                    component = null;
                    supply = null;
                    errorMessage = "未找到零件：" + supplyNoOrComponentName;
                    return false;
                }
                //Supply或Component不唯一的情况
                if (supplies.Length + components.Length != 1)
                {
                    object selectedObj =
                        FormChooseAmbiguousSupplyOrComponent.ChooseAmbiguousSupplyOrComponent(
                        components,
                        supplies,
                        supplyNoOrComponentName);
                    if (selectedObj == null)
                    {
                        errorMessage = "用户取消了导入";
                        return false;
                    }
                    else if (selectedObj is DataAccess.Component)
                    {
                        component = selectedObj as DataAccess.Component;
                    }
                    else if (selectedObj is Supply)
                    {
                        supply = selectedObj as Supply;
                    }
                    else
                    {
                        throw new Exception("FormChooseAmbiguousSupplyOrComponent返回值类型错误");
                    }
                    errorMessage = null;
                    return true;
                }

                //如果搜索到唯一的零件/供货，则确定就是它。
                if (supplies.Length > 0)
                {
                    supply = supplies[0];
                }
                else
                {
                    component = components[0];
                }
                errorMessage = null;
                return true;
            }
            errorMessage = null;
            return true;
        }
    }
}
