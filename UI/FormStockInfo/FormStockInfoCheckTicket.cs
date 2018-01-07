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
        int userID = -1;
        private PagerWidget<StockInfoCheckTicketView > pagerWidget = null;
        public FormStockInfoCheckTicket(int projectID, int warehouseID,int userID)
        {
            this.projectID = projectID;
            this.warehouseID = warehouseID;
            this.userID = userID;
            InitializeComponent();
        }

        private void FormStockInfoCheckTicket_Load(object sender, EventArgs e)
        {
            InitComponents();
            
            
            this.pagerWidget.Search();
        }
        private void InitComponents()
        {
            this.wmsEntities.Database.Connection.Open();

            string[] visibleColumnNames = (from kn in StockInfoCheckTicketViewMetaData.KeyNames
                                           where kn.Visible == true
                                           select kn.Name).ToArray();

            //初始化查询框
            this.comboBoxSearchCondition.Items.Add("无");
            this.comboBoxSearchCondition.Items.AddRange(visibleColumnNames);
            this.comboBoxSearchCondition.SelectedIndex = 0;

            //初始化分页控件
            this.pagerWidget = new PagerWidget<StockInfoCheckTicketView >(this.reoGridControlMain , StockInfoCheckTicketViewMetaData.KeyNames, this.projectID, this.warehouseID);
            this.paperpanel.Controls.Add(pagerWidget);
            pagerWidget.Show();
        }

        private void reoGridControlMain_Click(object sender, EventArgs e)
        {

        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            if (this.comboBoxSearchCondition.SelectedIndex != 0)
            {
                this.pagerWidget.AddCondition(this.comboBoxSearchCondition.SelectedItem.ToString(), this.textBoxSearchValue.Text);
            }
            this.pagerWidget.Search();
        }

        

       

        private void textBoxSearchValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (this.comboBoxSearchCondition.SelectedIndex != 0)
                {
                    this.pagerWidget.AddCondition(this.comboBoxSearchCondition.SelectedItem.ToString(), this.textBoxSearchValue.Text);
                }
                this.pagerWidget.Search();
            }
        }

        private void comboBoxSearchCondition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBoxSearchCondition.SelectedIndex == 0)
            {
                this.textBoxSearchValue.Text = "";
                this.textBoxSearchValue.Enabled = false;
               
            }
            else
            {
                this.textBoxSearchValue.Enabled = true;
            }
        }

        private void buttonAdd_Click_1(object sender, EventArgs e)
        {
           
            var form = new FormStockInfoCheckTicketModify(this.projectID, this.warehouseID,this.userID);
            form.SetMode(FormMode.ADD);
            form.SetAddFinishedCallback(() =>
            {
               
                this.pagerWidget.Search();
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
                var a1 = new FormStockInfoCheckTicketModify (this.projectID, this.warehouseID,this.userID,stockInfoCheckID);
                a1 .SetMode(FormMode.ALTER);
                a1.SetModifyFinishedCallback(() =>
                {
                    this.pagerWidget.Search();
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
                this.Invoke(new Action(() =>
                {
                    this.pagerWidget.Search();
                }));
            })).Start();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.pagerWidget.Search();
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
                var a1 = new FormStockInfoCheckTicketModify(-1, -1,-1, stockiofocheckid);
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
