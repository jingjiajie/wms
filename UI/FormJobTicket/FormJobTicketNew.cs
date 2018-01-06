using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WMS.DataAccess;
using System.Threading;
using unvell.ReoGrid;

namespace WMS.UI
{
    public partial class FormJobTicketNew : Form
    {
        private int shipmentTicketID = -1;
        private int userID = -1;
        private int projectID = -1;
        private int warehouseID = -1;

        private Action<string> toJobTicketCallback = null;

        public void SetToJobTicketCallback(Action<string> callback)
        {
            this.toJobTicketCallback = callback;
        }

        public FormJobTicketNew(int shipmentTicketID,int userID,int projectID,int warehouseID)
        {
            InitializeComponent();
            this.shipmentTicketID = shipmentTicketID;
            this.userID = userID;
            this.projectID = projectID;
            this.warehouseID = warehouseID;
        }

        private void FormJobTicketNew_Load(object sender, EventArgs e)
        {
            this.InitComponents();
        }

        private void InitComponents()
        {
            Utilities.InitReoGrid(this.reoGridControlMain, ShipmentTicketItemViewMetaData.KeyNames,WorksheetSelectionMode.Cell);
            var worksheet = this.reoGridControlMain.Worksheets[0];
            int[] editableColumns = new int[] { 1, 2 };
            worksheet.InsertColumns(1, 2);
            worksheet.ColumnHeaders[1].Text = "选择";
            worksheet.ColumnHeaders[1].DefaultCellBody = typeof(unvell.ReoGrid.CellTypes.CheckBoxCell);
            worksheet.ColumnHeaders[2].Text = "计划翻包数量";
            worksheet.BeforeCellEdit += (s, e) =>
            {
                e.IsCancelled = !editableColumns.Contains(e.Cell.Column);
            };

            Utilities.CreateEditPanel(this.tableLayoutEditPanel, JobTicketViewMetaData.KeyNames);
            ShipmentTicket shipmentTicket = null;
            User user = null;
            try
            {
                WMSEntities wmsEntities = new WMSEntities();
                shipmentTicket = (from s in wmsEntities.ShipmentTicket
                                  where s.ID == shipmentTicketID
                                  select s).FirstOrDefault();
                user = (from u in wmsEntities.User
                        where u.ID == this.userID
                        select u).FirstOrDefault();
            }
            catch
            {
                MessageBox.Show("无法连接到服务器，请检查网络连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            if (shipmentTicket == null)
            {
                MessageBox.Show("发货单信息不存在，请刷新显示", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            if (user == null)
            {
                MessageBox.Show("登录失效，操作失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.Controls.Find("textBoxCreateUserUsername", true)[0].Text = user.Username;
            this.Controls.Find("textBoxCreateTime", true)[0].Text = DateTime.Now.ToString();
            this.Controls.Find("textBoxShipmentTicketNo", true)[0].Text = shipmentTicket.No;
            this.Search();
        }

        private void Search()
        {
            
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            JobTicket newJobTicket = new JobTicket();
            if(Utilities.CopyTextBoxTextsToProperties(this, newJobTicket, JobTicketViewMetaData.KeyNames, out string errorMesage) == false)
            {
                MessageBox.Show(errorMesage, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            new Thread(()=>
            {
                WMSEntities wmsEntities = new WMSEntities();
                wmsEntities.JobTicket.Add(newJobTicket);

                ShipmentTicket shipmentTicket = (from s in wmsEntities.ShipmentTicket
                                  where s.ID == shipmentTicketID
                                  select s).FirstOrDefault();
                if(shipmentTicket == null)
                {
                    MessageBox.Show("发货单不存在，请重新查询", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                shipmentTicket.State = ShipmentTicketViewMetaData.STRING_STATE_WAITING_PUTOUT;
                newJobTicket.ShipmentTicketID = shipmentTicket.ID;
                newJobTicket.ProjectID = this.projectID;
                newJobTicket.WarehouseID = this.warehouseID;
                newJobTicket.CreateUserID = this.userID;
                newJobTicket.CreateTime = DateTime.Now;

                foreach (var shipmentTicketItem in shipmentTicket.ShipmentTicketItem)
                {
                    var jobTicketItem = new JobTicketItem();
                    jobTicketItem.StockInfoID = shipmentTicketItem.StockInfoID;
                    jobTicketItem.State = JobTicketItemViewMetaData.STRING_STATE_UNFINISHED;

                    newJobTicket.JobTicketItem.Add(jobTicketItem);
                }

                if (string.IsNullOrWhiteSpace(newJobTicket.JobTicketNo))
                {
                    DateTime createDay = new DateTime(shipmentTicket.CreateTime.Value.Year, shipmentTicket.CreateTime.Value.Month, shipmentTicket.CreateTime.Value.Day);
                    DateTime nextDay = createDay.AddDays(1);
                    int maxRankOfToday = Utilities.GetMaxTicketRankOfDay((from j in wmsEntities.JobTicket
                                                                          where j.CreateTime >= createDay && j.CreateTime < nextDay
                                                                          select j.JobTicketNo).ToArray());
                    newJobTicket.JobTicketNo = Utilities.GenerateTicketNo("Z", newJobTicket.CreateTime.Value, maxRankOfToday + 1);
                }
                wmsEntities.SaveChanges();
                if(MessageBox.Show("生成作业单成功，是否查看作业单？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    if(this.toJobTicketCallback == null)
                    {
                        throw new Exception("ToJobTicketCallback不允许为空！");
                    }
                    this.toJobTicketCallback(shipmentTicket.No);
                }
                if (!this.IsDisposed)
                {
                    this.Invoke(new Action(() =>
                    {
                        this.Close();
                    }));
                }
            }).Start();
        }

        private void buttonOK_MouseEnter(object sender, EventArgs e)
        {
            buttonOK.BackgroundImage = WMS.UI.Properties.Resources.bottonB2_s;
        }

        private void buttonOK_MouseLeave(object sender, EventArgs e)
        {
            buttonOK.BackgroundImage = WMS.UI.Properties.Resources.bottonB2_q;
        }

        private void buttonOK_MouseDown(object sender, MouseEventArgs e)
        {
            buttonOK.BackgroundImage = WMS.UI.Properties.Resources.bottonB3_q;
        }
    }
}
