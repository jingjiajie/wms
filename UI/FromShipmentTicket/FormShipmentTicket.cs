using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WMS.DataAccess;
using unvell.ReoGrid;
using System.Threading;
using System.Data.SqlClient;

namespace WMS.UI
{
    public partial class FormShipmentTicket : Form
    {
        WMSEntities wmsEntities = new WMSEntities();
        int userID = -1;
        int projectID = -1;
        int warehouseID = -1;

        public FormShipmentTicket(int userID,int projectID,int warehouseID)
        {
            InitializeComponent();
            this.userID = userID;
            this.projectID = projectID;
            this.warehouseID = warehouseID;
        }

        private void FormShipmentTicket_Load(object sender, EventArgs e)
        {
            InitComponents();
            this.Search();
        }

        private void InitComponents()
        {
            this.wmsEntities.Database.Connection.Open();

            string[] visibleColumnNames = (from kn in ShipmentTicketViewMetaData.KeyNames
                                           where kn.Visible == true
                                           select kn.Name).ToArray();

            //初始化
            this.comboBoxSearchCondition.Items.Add("无");
            this.comboBoxSearchCondition.Items.AddRange(visibleColumnNames);
            this.comboBoxSearchCondition.SelectedIndex = 0;


            //初始化表格
            var worksheet = this.reoGridControlMain.Worksheets[0];
            worksheet.SelectionMode = WorksheetSelectionMode.Row;

            for (int i = 0; i < ShipmentTicketViewMetaData.KeyNames.Length; i++)
            {
                worksheet.ColumnHeaders[i].Text = ShipmentTicketViewMetaData.KeyNames[i].Name;
                worksheet.ColumnHeaders[i].IsVisible = ShipmentTicketViewMetaData.KeyNames[i].Visible;
            }
            worksheet.Columns = ShipmentTicketViewMetaData.KeyNames.Length; //限制表的长度
        }

        private void Search()
        {
            string key = null;
            string value = null;

            if (this.comboBoxSearchCondition.SelectedIndex != 0)
            {
                key = (from kn in ShipmentTicketViewMetaData.KeyNames
                       where kn.Name == this.comboBoxSearchCondition.SelectedItem.ToString()
                       select kn.Key).First();
                value = this.textBoxSearchValue.Text;
            }

            this.labelStatus.Text = "正在搜索中...";
            var worksheet = this.reoGridControlMain.Worksheets[0];
            worksheet[0, 0] = "加载中...";
            new Thread(new ThreadStart(() =>
            {
                ShipmentTicketView[] shipmentTicketViews = null;
                string sql = "SELECT * FROM ShipmentTicketView WHERE 1=1 ";
                List<SqlParameter> parameters = new List<SqlParameter>();
                
                if(this.projectID != -1)
                {
                    sql += "AND ProjectID = @projectID ";
                    parameters.Add(new SqlParameter("projectID",this.projectID));
                }
                if(warehouseID != -1)
                {
                    sql += "AND WarehouseID = @warehouseID ";
                    parameters.Add(new SqlParameter("warehouseID",this.warehouseID));
                }
                if (key == null || value == null) //查询条件为null则查询全部内容
                {
                    shipmentTicketViews = wmsEntities.Database.SqlQuery<ShipmentTicketView>(sql, parameters.ToArray()).ToArray();
                }
                else
                {
                    try
                    {
                        sql += "AND " + key + " = @value ";
                        parameters.Add(new SqlParameter("value",value));
                        shipmentTicketViews = wmsEntities.Database.SqlQuery<ShipmentTicketView>(sql,parameters.ToArray()).ToArray();
                    }
                    catch
                    {
                        MessageBox.Show("查询的值不合法，请输入正确的值！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                this.reoGridControlMain.Invoke(new Action(() =>
                {
                    this.labelStatus.Text = "搜索完成";
                    worksheet.DeleteRangeData(RangePosition.EntireRange);
                    if (shipmentTicketViews.Length == 0)
                    {
                        worksheet[0, 1] = "没有查询到符合条件的记录";
                    }
                    for (int i = 0; i < shipmentTicketViews.Length; i++)
                    {
                        var curShipmentTicketViews = shipmentTicketViews[i];
                        object[] columns = Utilities.GetValuesByPropertieNames(curShipmentTicketViews, (from kn in ShipmentTicketViewMetaData.KeyNames select kn.Key).ToArray());
                        for (int j = 0; j < worksheet.Columns; j++)
                        {
                            worksheet[i, j] = columns[j] == null ? "" : columns[j].ToString();
                        }
                    }
                }));
            })).Start();
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            var worksheet = this.reoGridControlMain.Worksheets[0];
            try
            {
                if (worksheet.SelectionRange.Rows != 1)
                {
                    throw new Exception();
                }
                int shipmentTicketID = int.Parse(worksheet[worksheet.SelectionRange.Row, 0].ToString());
                var formShipmentTicketItem = new FormShipmentTicketItem(shipmentTicketID);
                formShipmentTicketItem.SetShipmentTicketStateChangedCallback(() =>
                {
                    this.Invoke(new Action(this.Search));
                });
                formShipmentTicketItem.Show();
            }
            catch
            {
                MessageBox.Show("请选择一项进行查看", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = new FormShipmentTicketModify(this.projectID,this.warehouseID,this.userID);
            form.SetMode(FormMode.ADD);
            form.SetAddFinishedCallback(() =>
            {
                this.Search();
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
                int shipmentTicketID = int.Parse(worksheet[worksheet.SelectionRange.Row, 0].ToString());
                var formShipmentTicketModify = new FormShipmentTicketModify(this.projectID,this.warehouseID,this.userID,shipmentTicketID);
                formShipmentTicketModify.SetModifyFinishedCallback(() =>
                {
                    this.Search();
                });
                formShipmentTicketModify.Show();
            }
            catch
            {
                MessageBox.Show("请选择一项进行修改", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            int[] deleteIDs = this.GetSelectedIDs();
            if (deleteIDs.Length == 0)
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
                    this.wmsEntities.Database.ExecuteSqlCommand("DELETE FROM ShipmentTicket WHERE ID = @shipmentTicketID", new SqlParameter("shipmentTicketID", id));
                }
                this.wmsEntities.SaveChanges();
                this.Invoke(new Action(this.Search));
            })).Start();
        }

        private void buttonGenerateJobTicket_Click(object sender, EventArgs e)
        {
            int[] ids = this.GetSelectedIDs();
            if(ids.Length != 1)
            {
                MessageBox.Show("请选择一项进行操作","提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int shipmentTicketID = ids[0];
            ShipmentTicket shipmentTicket = (from s in this.wmsEntities.ShipmentTicket
                                             where s.ID == shipmentTicketID
                                             select s).FirstOrDefault();

            JobTicket jobTicket = new JobTicket();
            jobTicket.JobTicketNo = "ZYD1234567";
            jobTicket.JobType = "发货";
            jobTicket.ShipmentTicketID = shipmentTicket.ID;
            jobTicket.ScheduledAmount = shipmentTicket.ScheduledAmount;
            jobTicket.State = JobTicketViewMetaData.STRING_STATE_UNFINISHED;
            jobTicket.PrintedTimes = 0;
            jobTicket.CreateUserID = this.userID;
            jobTicket.CreateTime = DateTime.Now;
            jobTicket.LastUpdateUserID = this.userID;
            jobTicket.LastUpdateTime = DateTime.Now;

            this.wmsEntities.JobTicket.Add(jobTicket);

            foreach (var shipmentTicketItem in shipmentTicket.ShipmentTicketItem)
            {
                var jobTicketItem = new JobTicketItem();
                jobTicketItem.StockInfoID = shipmentTicketItem.StockInfoID;
                jobTicketItem.No = "ZY1234567";
                jobTicketItem.State = JobTicketItemViewMetaData.STRING_STATE_UNFINISHED;

                jobTicket.JobTicketItem.Add(jobTicketItem);
            }

            this.wmsEntities.SaveChanges();
            MessageBox.Show("生成作业单成功！","提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private int[] GetSelectedIDs()
        {
            var worksheet = this.reoGridControlMain.Worksheets[0];
            List<int> ids = new List<int>();
            for (int i = 0; i < worksheet.SelectionRange.Rows; i++)
            {
                try
                {
                    int curID = int.Parse(worksheet[i + worksheet.SelectionRange.Row, 0].ToString());
                    ids.Add(curID);
                }
                catch
                {
                    continue;
                }
            }
            return ids.ToArray();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            this.Search();
        }
    }
}
