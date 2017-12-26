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

namespace WMS.UI
{
    public partial class FormJobTicketNew : Form
    {
        private PagerWidget<ShipmentTicketItemView> pagerWidget = null;
        private int shipmentTicketID = -1;

        public FormJobTicketNew(int shipmentTicketID)
        {
            InitializeComponent();
            this.shipmentTicketID = shipmentTicketID;
        }

        private void FormJobTicketNew_Load(object sender, EventArgs e)
        {
            this.InitComponents();
            Utilities.CreateEditPanel(this.tableLayoutEditPanel, JobTicketViewMetaData.KeyNames);
            ShipmentTicket shipmentTicket = null;
            try
            {
                WMSEntities wmsEntities = new WMSEntities();
                shipmentTicket = (from s in wmsEntities.ShipmentTicket
                                  where s.ID == shipmentTicketID
                                  select s).FirstOrDefault();
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
            this.Search();
        }

        private void InitComponents()
        {
            this.pagerWidget = new PagerWidget<ShipmentTicketItemView>(this.reoGridControlMain, ShipmentTicketItemViewMetaData.KeyNames);
            this.panelPagerWidget.Controls.Add(this.pagerWidget);
            this.pagerWidget.Show();
        }

        private void Search()
        {
            this.pagerWidget.AddStaticCondition("ShipmentTicketID",this.shipmentTicketID.ToString());
            this.pagerWidget.Search();
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

                foreach (var shipmentTicketItem in shipmentTicket.ShipmentTicketItem)
                {
                    var jobTicketItem = new JobTicketItem();
                    jobTicketItem.StockInfoID = shipmentTicketItem.StockInfoID;
                    jobTicketItem.No = "";
                    jobTicketItem.State = JobTicketItemViewMetaData.STRING_STATE_UNFINISHED;

                    newJobTicket.JobTicketItem.Add(jobTicketItem);
                }
                wmsEntities.SaveChanges();
                MessageBox.Show("生成作业单成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }).Start();
        }
    }
}
