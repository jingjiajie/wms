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
using System.Threading;
using System.Data.SqlClient;

namespace WMS.UI
{
    public partial class FormStockInfoCheckTicket : Form
    {
        private WMSEntities wmsEntities = new WMSEntities();
        int projectID = -1;
        int warehouseID = -1;
        public FormStockInfoCheckTicket(int projectID, int warehouseID)
        {
            this.projectID = projectID;
            this.warehouseID = warehouseID;
            InitializeComponent();
        }

        private void FormStockInfoCheckTicket_Load(object sender, EventArgs e)
        {
            InitComponents();
            this.Search();
        }
        private void InitComponents()
        {
            this.wmsEntities.Database.Connection.Open();

            string[] visibleColumnNames = (from kn in StockInfoCheckTicketViewMetaData.KeyNames
                                           where kn.Visible == true
                                           select kn.Name).ToArray();

            //初始化
            this.comboBoxSearchCondition.Items.Add("无");
            this.comboBoxSearchCondition.Items.AddRange(visibleColumnNames);
            this.comboBoxSearchCondition.SelectedIndex = 0;


            //初始化表格
            var worksheet = this.reoGridControlMain.Worksheets[0];
            worksheet.SelectionMode = WorksheetSelectionMode.Row;
            for (int i = 0; i < StockInfoCheckTicketViewMetaData.KeyNames.Length; i++)
            {
                worksheet.ColumnHeaders[i].Text = StockInfoCheckTicketViewMetaData.KeyNames[i].Name;
                worksheet.ColumnHeaders[i].IsVisible = StockInfoCheckTicketViewMetaData.KeyNames[i].Visible;
            }
            worksheet.Columns = StockInfoCheckTicketViewMetaData.KeyNames.Length; //限制表的长度
        }

        private void reoGridControlMain_Click(object sender, EventArgs e)
        {

        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            this.Search();
        }

        private void Search()
        {
            string key = null;
            string value = null;

            if (this.comboBoxSearchCondition.SelectedIndex != 0)
            {
                key = (from kn in StockInfoCheckTicketViewMetaData.KeyNames
                       where kn.Name == this.comboBoxSearchCondition.SelectedItem.ToString()
                       select kn.Key).First();
                value = this.textBoxSearchValue.Text;
            }

            this.labelStatus.Text = "正在搜索中...";
            //var worksheet = this.reoGridControlMain.Worksheets[0];
            //worksheet[0, 0] = "加载中...";
            new Thread(new ThreadStart(() =>
            {
                StockInfoCheckTicketView[] stockCheckViews = null;
                var wmsEntities = new WMSEntities();
                string sql = "SELECT * FROM StockInfoCheckTicketView WHERE 1=1 ";
                List<SqlParameter> parameters = new List<SqlParameter>();
           
                if (this.projectID != -1)
                {
                    sql += "AND ProjectID = @projectID ";
                    parameters.Add(new SqlParameter("projectID", this.projectID));
                }
                if (warehouseID != -1)
                {
                    sql += "AND WarehouseID = @warehouseID ";
                    parameters.Add(new SqlParameter("warehouseID", this.warehouseID));
                }
                if (key != null && value != null) //查询条件不为null则增加查询条件
                {
                    sql += "AND " + key + " = @value ";
                    parameters.Add(new SqlParameter("value", value));
                }
                sql += " ORDER BY ID DESC"; //倒序排序
                stockCheckViews = wmsEntities.Database.SqlQuery<StockInfoCheckTicketView>(sql, parameters.ToArray()).ToArray();


                this.reoGridControlMain.Invoke(new Action(() =>
                {
                    this.labelStatus.Text = "搜索完成";
                    var worksheet = this.reoGridControlMain.Worksheets[0];
                    worksheet.DeleteRangeData(RangePosition.EntireRange);
                    if (stockCheckViews.Length == 0)
                    {
                        worksheet[0, 1] = "没有查询到符合条件的记录";
                    }
                    for (int i = 0; i < stockCheckViews.Length; i++)
                    {
                        StockInfoCheckTicketView curStockInfoCheckTicketView = stockCheckViews[i];
                        object[] columns = Utilities.GetValuesByPropertieNames(curStockInfoCheckTicketView, (from kn in StockInfoCheckTicketViewMetaData.KeyNames select kn.Key).ToArray());
                        for (int j = 0; j < worksheet.Columns; j++)
                        {
                            worksheet[i, j] = columns[j] == null ? "" : columns[j].ToString();
                        }
                    }
                }));
            })).Start();
        }

       

        private void textBoxSearchValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.Search();
            }
        }

        private void comboBoxSearchCondition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBoxSearchCondition.SelectedIndex == 0)
            {
                this.textBoxSearchValue.Text = "";
                this.textBoxSearchValue.Enabled = false;
                this.Search();
            }
            else
            {
                this.textBoxSearchValue.Enabled = true;
            }
        }

        private void buttonAdd_Click_1(object sender, EventArgs e)
        {
           
            var form = new FormStockInfoCheckTicketModify(this.projectID, this.warehouseID);
            form.SetMode(FormMode.ADD);
            form.SetAddFinishedCallback(() =>
            {
                this.Search();
                //var worksheet = this.reoGridControlMain.Worksheets[0];
                //var range = worksheet.SelectionRange;
                //worksheet.SelectionRange = new RangePosition("A1:A1");

                //int[] ids = Utilities.GetSelectedIDs(this.reoGridControlMain);

                //if (ids.Length != 1)
                //{
                //    MessageBox.Show("请选择一项");
                //    return;
                //}
                //else if ((ids.Length == 1))
                //{
                //    int stockiofocheckid = ids[0];
                //    FormStockInfoCheckTicketComponentModify a1 = new FormStockInfoCheckTicketComponentModify(stockiofocheckid);
                //    a1.Show();
                //}
            });
            form.Show();
        }

        private void buttonAlter_Click(object sender, EventArgs e)
        {
            var worksheet = this.reoGridControlMain.Worksheets[0];
            
            try
            {
                if (worksheet.SelectionRange.Rows != 1)
                {
                    throw new Exception();
                }
                int stockInfoCheckID = int.Parse(worksheet[worksheet.SelectionRange.Row, 0].ToString());
                var a1 = new FormStockInfoCheckTicketModify (this.projectID, this.warehouseID,stockInfoCheckID);
                a1 .SetMode(FormMode.ALTER);
                a1.SetModifyFinishedCallback(() =>
                {
                    this.Search();
                });
                a1.Show();
            }
            catch
            {
                MessageBox.Show("请选择一项进行修改", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void buttonDelete_Click_1(object sender, EventArgs e)
        {
            var worksheet = this.reoGridControlMain.Worksheets[0];
            List<int> deleteIDs = new List<int>();
            for (int i = 0; i < worksheet.SelectionRange.Rows; i++)
            {
                try
                {
                    int curID = int.Parse(worksheet[i + worksheet.SelectionRange.Row, 0].ToString());
                    deleteIDs.Add(curID);
                }
                catch
                {
                    continue;
                }
            }
            if (deleteIDs.Count == 0)
            {
                MessageBox.Show("请选择您要删除的记录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("您真的要删除这些记录吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }
            this.labelStatus.Text = "正在删除...";


            new Thread(new ThreadStart(() =>
            {
                foreach (int id in deleteIDs)
                {
                    this.wmsEntities.Database.ExecuteSqlCommand("DELETE FROM StockInfoCheckTicket WHERE ID = @stockCheckID", new SqlParameter("stockCheckID", id));
                }
                this.wmsEntities.SaveChanges();
                this.Invoke(new Action(this.Search));
            })).Start();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Search();
        }

        private void button_additeam_Click(object sender, EventArgs e)
        {
            int[] ids = Utilities.GetSelectedIDs(this.reoGridControlMain);

            if (ids.Length != 1)
            {
                MessageBox.Show("请选择一项");
                return;
            }
            else if ((ids.Length == 1))
            {
                int stockiofocheckid = ids[0];
                var a1 = new FormStockInfoCheckTicketModify(-1, -1, stockiofocheckid);
                a1.SetMode(FormMode.CHECK);
                a1.Show();
            }

            //var worksheet = this.reoGridControlMain.Worksheets[0];

            //try
            //{
            //    if (worksheet.SelectionRange.Rows != 1)
            //    {
            //        throw new Exception();
            //    }
            //    int stockInfoCheckID = int.Parse(worksheet[worksheet.SelectionRange.Row, 0].ToString());
            //    var a1 = new FormStockInfoCheckTicketModify(this.projectID, this.warehouseID, stockInfoCheckID);
            //    a1.SetMode(FormMode.CHECK);
            //    a1.SetCheckFinishedCallback(() =>
            //    {
            //        this.Search();
            //    });
            //    a1.Show();
            //}
            //catch
            //{
            //    MessageBox.Show("请选择一项进行修改", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}



        }
    }
}
