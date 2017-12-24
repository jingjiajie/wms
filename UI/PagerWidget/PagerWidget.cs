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

namespace WMS.UI
{
    public partial class PagerWidget<TargetClass> : Form
    {
        private class Condition
        {
            public string Sql = null;
            public List<SqlParameter> Parameters = new List<SqlParameter>();
        }

        private int curPage = 0;
        private int totalPage = 1;
        private ReoGridControl reoGrid = null;
        private string dbTableName = null;
        private int projectID = -1;
        private int warehouseID = -1;
        private KeyName[] keyNames = null;

        private List<Condition> condition = new List<Condition>();
        private List<Condition> staticCondition = new List<Condition>();

        private int RecordCount
        {
            set
            {
                int totalPage = value / Utilities.PAGE_SIZE + (value % Utilities.PAGE_SIZE == 0 ? 0 : 1);
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
                if (!this.IsDisposed)
                {
                    this.Invoke(new Action(()=>
                    {
                        this.textBoxPage.Text = (curPage + 1).ToString();
                        this.labelPage.Text = string.Format("{0}/{1}页", curPage + 1, TotalPage);
                    }));
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
                if (!this.IsDisposed)
                {
                    this.Invoke(new Action(()=>
                    {
                        this.labelPage.Text = string.Format("{0}/{1}页", CurPage + 1, totalPage);
                    }));
                }
            }
        }

        public int ProjectID { get => projectID; set => projectID = value; }
        public int WarehouseID { get => warehouseID; set => warehouseID = value; }

        public PagerWidget(ReoGridControl reoGridControl, KeyName[] keyNames, int defaultProjectID = -1, int defaultWarehouseID = -1)
        {
            InitializeComponent();
            this.TopLevel = false;
            this.Dock = DockStyle.Fill;
            this.dbTableName = typeof(TargetClass).Name;
            this.reoGrid = reoGridControl;
            this.ProjectID = defaultProjectID;
            this.WarehouseID = defaultWarehouseID;
            this.keyNames = keyNames;

            InitReoGrid();
        }


        private void InitReoGrid()
        {
            //初始化表格
            var worksheet = this.reoGrid.Worksheets[0];
            worksheet.SelectionMode = WorksheetSelectionMode.Row;
            for (int i = 0; i < this.keyNames.Length; i++)
            {
                worksheet.ColumnHeaders[i].Text = this.keyNames[i].Name;
                worksheet.ColumnHeaders[i].IsVisible = this.keyNames[i].Visible;
            }
            worksheet.Columns = StockInfoViewMetaData.KeyNames.Length; //限制表的长度
        }

        private Condition MakeCondition(string key, string value)
        {
            string realKey = null;
            //首先判断查询的Key是否在KeyName中对应的Name，如果是，则转换成相应的Key
            string foundKey = (from kn in keyNames
                               where kn.Name == key
                               select kn.Key).FirstOrDefault();
            if (foundKey != null)
            {
                realKey = foundKey;
            }
            else if (typeof(TargetClass).GetProperty(key) != null)
            {
                realKey = key;
            }
            else
            {
                throw new Exception("KeyNames中不存在Name:" + key + "请检查你的代码！");
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

        public void AddCondition(string key,string value)
        {
            this.condition.Add(this.MakeCondition(key,value));
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

        public void Search(bool savePage = false, int selectID = -1)
        {
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
                string sqlCondition = " WHERE 1=1 ";
                List<SqlParameter> parameters = new List<SqlParameter>();

                if (projectID != -1)
                {
                    sqlCondition += "AND ProjectID = @projectID ";
                    parameters.Add(new SqlParameter("projectID", projectID));
                }
                if (warehouseID != -1)
                {
                    sqlCondition += "AND WarehouseID = @warehouseID ";
                    parameters.Add(new SqlParameter("warehouseID", warehouseID));
                }

                foreach(Condition cond in this.condition)
                {
                    sqlCondition += " AND " + cond.Sql;
                    parameters.AddRange(cond.Parameters);
                }

                foreach (Condition cond in this.staticCondition)
                {
                    sqlCondition += " AND " + cond.Sql;
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
                        return;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("查询失败，请检查网络连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    //添加分页条件
                    sqlCondition += " ORDER BY ID DESC"; //倒序排序
                    sqlCondition += " OFFSET @offsetRows ROWS FETCH NEXT @pageSize ROWS ONLY";
                    parameters.Add(new SqlParameter("@offsetRows", this.curPage * Utilities.PAGE_SIZE));
                    parameters.Add(new SqlParameter("@pageSize", Utilities.PAGE_SIZE));

                    try
                    {
                        string sql = sqlSelect + sqlCondition;
                        results = wmsEntities.Database.SqlQuery<TargetClass>(sql, (from p in parameters select ((ICloneable)p).Clone()).ToArray()).ToArray();
                    }
                    catch (EntityCommandExecutionException)
                    {
                        MessageBox.Show("查询失败，请检查输入查询条件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("查询失败，请检查网络连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    wmsEntities.Database.Connection.Close();
                }
                this.reoGrid.Invoke(new Action(() =>
                {
                    worksheet.DeleteRangeData(RangePosition.EntireRange);
                    worksheet.Rows = (results.Length < 1 ? 1 : results.Length);
                    if (results.Length == 0)
                    {
                        worksheet[0, 1] = "没有查询到符合条件的记录";
                    }
                    for (int i = 0; i < results.Length; i++)
                    {
                        TargetClass cur = results[i];
                        object[] columns = Utilities.GetValuesByPropertieNames(cur, (from kn in keyNames select kn.Key).ToArray());
                        for (int j = 0; j < worksheet.Columns; j++)
                        {
                            if (columns[j] == null) continue;
                            worksheet[i, j] = columns[j].ToString();
                        }
                    }
                    Utilities.SelectLineByID(this.reoGrid,selectID);
                }));
            })).Start();
        }
    }
}
