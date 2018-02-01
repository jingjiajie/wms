using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using unvell.ReoGrid;
using System.Threading;
using WMS.DataAccess;
using System.Data.SqlClient;
using unvell.ReoGrid.CellTypes;
using System.Reflection;

namespace WMS.UI
{
    public partial class PagerWidget<TargetClass> : Form
    {
        private class Condition
        {
            public string Sql = null;
            public List<SqlParameter> Parameters = new List<SqlParameter>();
        }
        private int pageSize = 50;
        private int curPage = 0;
        private int totalPage = 1;
        private ReoGridControl reoGrid = null;
        private string dbTableName = null;
        private int projectID = -1;
        private int warehouseID = -1;
        private KeyName[] keyNames = null;

        private List<Condition> condition = new List<Condition>();
        private List<Condition> staticCondition = new List<Condition>();
        private List<string> order = new List<string>();

        private Dictionary<int, bool> selectedIDs = new Dictionary<int, bool>();

        private int RecordCount
        {
            set
            {
                if(pageSize == -1)
                {
                    this.totalPage = 1;
                    return;
                }
                int totalPage = value / this.pageSize + (value % this.pageSize == 0 ? 0 : 1);
                this.TotalPage = totalPage == 0 ? 1 : totalPage; //总页数最少为1，不会出现0页
            }
        }

        public int CurPage
        {
            get { return curPage; }
            set
            {
                if (value >= totalPage)
                {
                    throw new Exception(string.Format("目标页{0}大于总页数{1}！", value + 1, TotalPage));
                }
                else if (value < 0)
                {
                    throw new Exception(string.Format("目标页{0}不可小于第一页！", value + 1, TotalPage));
                }
                curPage = value;
                try
                {
                    this.textBoxPage.Text = (curPage + 1).ToString();
                    this.labelPage.Text = string.Format("{0}/{1}页", curPage + 1, TotalPage);
                }
                catch
                {
                    //Do nothing
                }
            }
        }

        public int TotalPage
        {
            get => totalPage;
            set
            {
                totalPage = value;
                if (CurPage >= totalPage)
                {
                    CurPage = totalPage - 1;
                }
                try { 
                        this.labelPage.Text = string.Format("{0}/{1}页", CurPage + 1, totalPage);
                }
                catch
                {
                    //Do nothing
                }
            }
        }

        private Mode mode = Mode.NORMAL;

        public int ProjectID { get => projectID; set => projectID = value; }
        public int WarehouseID { get => warehouseID; set => warehouseID = value; }

        public enum Mode { NORMAL,MULTISELECT}

        public PagerWidget(ReoGridControl reoGridControl, KeyName[] keyNames, int defaultProjectID = -1, int defaultWarehouseID = -1,Mode mode= Mode.NORMAL)
        {
            InitializeComponent();
            this.TopLevel = false;
            this.Dock = DockStyle.Fill;
            this.dbTableName = typeof(TargetClass).Name;
            this.reoGrid = reoGridControl;
            this.ProjectID = defaultProjectID;
            this.WarehouseID = defaultWarehouseID;
            this.keyNames = keyNames;

            Utilities.InitReoGrid(this.reoGrid,this.keyNames);
            this.mode = mode;
            if(this.mode == Mode.MULTISELECT)
            {
                var worksheet = this.reoGrid.CurrentWorksheet;
                worksheet.InsertColumns(1, 1);
                worksheet.ColumnHeaders[1].Text = "选择";
            }
        }

        public void SetPageSize(int pageSize)
        {
            this.pageSize = pageSize;
        }

        private Condition MakeCondition(string key, string value)
        {
            string realKey = null;
            //首先判断查询的Key是否在KeyName中对应的Name，如果是，则转换成相应的Key
            KeyName foundKeyName = (from kn in keyNames
                               where kn.Name == key
                               select kn).FirstOrDefault();
            if (foundKeyName == null)
            {
                foundKeyName = (from kn in keyNames
                                where kn.Key == key
                                select kn).FirstOrDefault();
            }
            if (foundKeyName != null)
            {
                realKey = foundKeyName.Key;
                if (foundKeyName.Translator != null)
                {
                    try
                    {
                        value = foundKeyName.Translator(value).ToString();
                    }
                    catch
                    {
                        MessageBox.Show("查询失败，请检查查询条件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return null;
                    }
                }
            }
            else
            {
                PropertyInfo property = typeof(TargetClass).GetProperty(key);
                if (property == null)
                {
                    throw new Exception(typeof(TargetClass).Name + "的KeyNames中不存在Name:" + key + "请检查你的代码！");
                }
                realKey = key;
            }

            string paramName = "@value" + Guid.NewGuid().ToString("N");
            Type propertyType = typeof(TargetClass).GetProperty(realKey).PropertyType;
            string sql = "(1<>1 ";
            if (value.Length == 0) //长度为0，则认为搜索NULL值
            {
                sql += "OR " + realKey + " IS NULL ";
                if (propertyType == typeof(string))
                {
                    sql += "OR " + realKey + " = ''";
                }
            }
            else //value.length != 0
            {
                if (propertyType == typeof(string))
                {
                    sql += "OR " + realKey + " LIKE '%'+" + paramName + "+'%'";
                }
                else
                {
                    sql += "OR " + realKey + " = " + paramName;
                }
            }
            sql += ")";
            Condition condition = new Condition();
            condition.Sql = sql;
            condition.Parameters.Add(new SqlParameter(paramName, value));
            return condition;
        }

        public void AddOrderBy(string orderByCondition)
        {
            this.order.Add(orderByCondition);
        }

        public void ClearOrderBy()
        {
            this.order.Clear();
        }

        public void AddCondition(string key,string value)
        {
            Condition cond = this.MakeCondition(key, value);
            if (cond != null)
            {
                this.condition.Add(cond);
            }
        }


        public void AddCondition(string sql, params SqlParameter[] parameters)
        {
            this.condition.Add(new Condition()
            {
                Sql = sql,
                Parameters = new List<SqlParameter>(parameters)
            });
        }

        public void ClearCondition()
        {
            this.condition.Clear();
        }

        public void AddStaticCondition(string key, string value)
        {
            this.staticCondition.Add(this.MakeCondition(key, value));
        }

        public void AddStaticCondition(string sql,params SqlParameter[] parameters)
        {
            this.staticCondition.Add(new Condition()
            {
                Sql = sql,
                Parameters = new List<SqlParameter>(parameters)
            });
        }

        public void ClearStaticCondition()
        {
            this.staticCondition.Clear();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void buttonGoto_Click(object sender, EventArgs e)
        {
            if(int.TryParse(this.textBoxPage.Text,out int targetPage) == false)
            {
                MessageBox.Show("请输入正确的页码！","提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if(targetPage - 1 < 0 || targetPage-1 >= TotalPage)
            {
                if(TotalPage == 1)
                {
                    MessageBox.Show("查询结果只有1页", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                MessageBox.Show("请输入 1~"+TotalPage+" 之间的页码","提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            this.CurPage = targetPage - 1;
            this.Search(true);
        }

        private void buttonNextPage_Click(object sender, EventArgs e)
        {
            if (this.CurPage == this.TotalPage - 1)
            {
                MessageBox.Show("已经到达尾页", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            this.CurPage++;
            this.Search(true);
        }

        private void buttonPreviousPage_Click(object sender, EventArgs e)
        {
            if (this.CurPage == 0)
            {
                MessageBox.Show("已经到达首页","提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            this.CurPage--;
            this.Search(true);
        }

        private void textBoxPage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13) //回车
            {
                this.buttonGoto.PerformClick();
            }
        }

        private bool enableCheckEvent = false;
        public void Search(bool savePage = false, int selectID = -1,Action<TargetClass[]> searchFinishedCallback=null)
        {
            enableCheckEvent = false;
            if(savePage == false)
            {
                this.CurPage = 0;
            }
            var worksheet = this.reoGrid.Worksheets[0];
            worksheet[0, 1] = "加载中...";
            new Thread(new ThreadStart(() =>
            {
                TargetClass[] results = null;
                string sqlSelect = "SELECT * FROM " + this.dbTableName;
                StringBuilder sqlCondition = new StringBuilder(" WHERE 1=1 ");
                List<SqlParameter> parameters = new List<SqlParameter>();

                if (projectID != -1)
                {
                    sqlCondition.Append("AND ProjectID = @projectID ");
                    parameters.Add(new SqlParameter("projectID", projectID));
                }
                if (warehouseID != -1)
                {
                    sqlCondition.Append("AND WarehouseID = @warehouseID ");
                    parameters.Add(new SqlParameter("warehouseID", warehouseID));
                }

                foreach(Condition cond in this.condition)
                {
                    sqlCondition.Append(" AND " + cond.Sql);
                    parameters.AddRange(cond.Parameters);
                }

                foreach (Condition cond in this.staticCondition)
                {
                    sqlCondition.Append(" AND " + cond.Sql);
                    parameters.AddRange(cond.Parameters);
                }
                using (WMSEntities wmsEntities = new WMSEntities()) {
                    //查询总数量
                    try
                    {
                        string sql = "SELECT COUNT(*) FROM " + dbTableName + sqlCondition;
                        int count = wmsEntities.Database.SqlQuery<int>(sql, (from p in parameters select ((ICloneable)p).Clone()).ToArray()).First();
                        int page = this.CurPage;
                        this.RecordCount = count;
                        if (this.CurPage != page)
                        {
                            this.Search();
                            return;
                        }
                    }
                    catch (EntityCommandExecutionException)
                    {
                        MessageBox.Show("查询失败，请检查输入查询条件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        enableCheckEvent = true;
                        return;
                    }
                    catch (Exception)
                    { 
                        MessageBox.Show("查询失败，请检查网络连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        enableCheckEvent = true;
                        return;
                    }

                    //添加分页条件
                    sqlCondition.Append(" ORDER BY "); //倒序排序
                    if (this.order.Count == 0) //如果没有设置排序条件，就按照ID倒序排列
                    {
                        sqlCondition.Append("ID DESC ");
                    }
                    for (int i = 0; i < this.order.Count; i++)
                    {
                        string orderByCondition = this.order[i];
                        sqlCondition.Append(orderByCondition);
                        //如果不是最后一个条件，则加上逗号
                        if (i != this.order.Count - 1)
                        {
                            sqlCondition.Append(',');
                        }
                    }

                    if(this.pageSize != -1)
                    {
                        sqlCondition.Append(" OFFSET @offsetRows ROWS FETCH NEXT @pageSize ROWS ONLY");
                        parameters.Add(new SqlParameter("@offsetRows", this.curPage * this.pageSize));
                        parameters.Add(new SqlParameter("@pageSize", this.pageSize));
                    }

                    try
                    {
                        string sql = sqlSelect + sqlCondition.ToString();
                        results = wmsEntities.Database.SqlQuery<TargetClass>(sql, (from p in parameters select ((ICloneable)p).Clone()).ToArray()).ToArray();
                    }
                    catch (EntityCommandExecutionException)
                    {
                        MessageBox.Show("查询失败，请检查输入查询条件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        enableCheckEvent = true;
                        return;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("查询失败，请检查网络连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        enableCheckEvent = true;
                        return;
                    }
                    wmsEntities.Database.Connection.Close();
                }
                //查询完成
                this.reoGrid.Invoke(new Action(() =>
                {
                    worksheet.DeleteRangeData(RangePosition.EntireRange);
                    worksheet.Rows = (results.Length < 10 ? 10 : results.Length);
                    if (results.Length == 0)
                    {
                        worksheet[0, 1] = "没有查询到符合条件的记录";
                    }
                    for (int i = 0; i < results.Length; i++)
                    {
                        TargetClass cur = results[i];
                        KeyName[] usedKeyNames = (from kn in keyNames where kn.Visible == true || kn.Key == "ID" select kn).ToArray();
                        object[] columns = Utilities.GetValuesByPropertieNames(cur,(from kn in usedKeyNames select kn.Key).ToArray());
                        for (int j = 0, col = 0; j < columns.Length; j++, col++)
                        {
                            //多选模式则空出第一列，放置选择框
                            if (j == 1 && this.mode == Mode.MULTISELECT) col++;
                            if (columns[j] == null) continue;
                            worksheet.Cells[i, col].DataFormat = unvell.ReoGrid.DataFormat.CellDataFormatFlag.Text;
                            string text = null;
                            if (usedKeyNames[j].Translator != null)
                            {
                                text = usedKeyNames[j].Translator(columns[j]).ToString();
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
                            worksheet[i, col] = text;
                        }
                    }
                    Utilities.SelectLineByID(this.reoGrid,selectID);
                    if(this.mode == Mode.MULTISELECT)
                    {
                        PropertyInfo propertyID = typeof(TargetClass).GetProperty("ID");
                        if(propertyID == null)
                        {
                            throw new Exception("在分页控件中使用多选功能，目标类型"+typeof(TargetClass).Name+"必须具有ID属性！");
                        }
                        this.Invoke(new Action(()=>
                        {
                            for(int i = 0; i < results.Length; i++)
                            {
                                CheckBoxCell checkBoxCell = new CheckBoxCell();
                                worksheet[i, 1] = checkBoxCell;
                                int id = (int)propertyID.GetValue(results[i],null);
                                if (this.selectedIDs.ContainsKey(id)) checkBoxCell.IsChecked = true;
                                checkBoxCell.CheckChanged += (obj, e) =>
                                {
                                    if (enableCheckEvent == false) return;
                                    if(checkBoxCell.IsChecked && this.selectedIDs.ContainsKey(id)==false)
                                    {
                                        this.selectedIDs.Add(id, true);
                                    }
                                    else if(checkBoxCell.IsChecked==false && this.selectedIDs.ContainsKey(id))
                                    {
                                        this.selectedIDs.Remove(id);
                                    }
                                };
                            }
                        }));
                    }
                    Utilities.AutoFitReoGridColumnWidth(this.reoGrid);
                    searchFinishedCallback?.Invoke(results);
                    enableCheckEvent = true;
                }));
            })).Start();
        }

        private void PagerWidget_Shown(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public int[] GetSelectedIDs()
        {
            return this.selectedIDs.Keys.ToArray();
        }
    }
}
