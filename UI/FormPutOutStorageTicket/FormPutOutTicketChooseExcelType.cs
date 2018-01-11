using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WMS.DataAccess;

namespace WMS.UI
{
    public partial class FormPutOutTicketChooseExcelType : Form
    {
        int putOutStorageTicketID = -1;
        public FormPutOutTicketChooseExcelType(int putOutStorageTicketID)
        {
            InitializeComponent();
            this.putOutStorageTicketID = putOutStorageTicketID;
        }

        private void FormPutOutTicketChooseExcelType_Load(object sender, EventArgs e)
        {

        }

        private void buttonCover_Click(object sender, EventArgs e)
        {
            StandardFormPreviewExcel formPreview = new StandardFormPreviewExcel("发货单预览");
            if (formPreview.SetPatternTable(@"Excel\patternPutOutStorageTicketNormal.xlsx") == false)
            {
                this.Close();
                return;
            }

            PutOutStorageTicketView putOutStorageTicketView = null;
            ShipmentTicketView shipmentTicketView = null;
            PutOutStorageTicketItemView[] putOutStorageTicketItemViews = null;
            try
            {
                WMSEntities wmsEntities = new WMSEntities();
                putOutStorageTicketView = (from p in wmsEntities.PutOutStorageTicketView
                                           where p.ID == this.putOutStorageTicketID
                                           select p).FirstOrDefault();
                putOutStorageTicketItemViews =
                    (from p in wmsEntities.PutOutStorageTicketItemView
                     where p.PutOutStorageTicketID == putOutStorageTicketView.ID
                     select p).ToArray();
                if (putOutStorageTicketView == null)
                {
                    MessageBox.Show("出库单不存在，请重新查询！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                shipmentTicketView =
                    wmsEntities.Database.SqlQuery<ShipmentTicketView>(string.Format(@"SELECT * FROM ShipmentTicketView WHERE ID = 
                                                                    (SELECT ShipmentTicketID FROM JobTicket
                                                                    WHERE JobTicket.ID = {0})", putOutStorageTicketView.JobTicketID)).FirstOrDefault();
            }
            catch
            {
                MessageBox.Show("加载失败，请检查网络连接！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            formPreview.AddData("putOutStorageTicket", putOutStorageTicketView);
            formPreview.AddData("putOutStorageTicketItems", putOutStorageTicketItemViews);
            formPreview.AddData("shipmentTicket", shipmentTicketView);
            formPreview.SetPrintScale(0.9F);
            formPreview.Show();
            this.Close();
        }

        private void buttonInner_Click(object sender, EventArgs e)
        {
            StandardFormPreviewExcel formPreview = new StandardFormPreviewExcel("出库单预览");
            if (formPreview.SetPatternTable(@"Excel\patternPutOutStorageTicketCover.xlsx") == false)
            {
                this.Close();
                return;
            }
            WMSEntities wmsEntities = new WMSEntities();
            PutOutStorageTicketView putOutStorageTicketView = (from p in wmsEntities.PutOutStorageTicketView
                                                               where p.ID == this.putOutStorageTicketID
                                                               select p).FirstOrDefault();
            PutOutStorageTicketItemView[] putOutStorageTicketTiemViews =
                (from p in wmsEntities.PutOutStorageTicketItemView
                 where p.PutOutStorageTicketID == putOutStorageTicketView.ID
                 select p).ToArray();
            if (putOutStorageTicketView == null)
            {
                MessageBox.Show("出库单不存在，请重新查询！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ShipmentTicketView shipmentTicketView =
                wmsEntities.Database.SqlQuery<ShipmentTicketView>(string.Format(@"SELECT * FROM ShipmentTicketView WHERE ID = 
                                                    (SELECT ShipmentTicketID FROM JobTicket
                                                        WHERE JobTicket.ID = {0})", putOutStorageTicketView.JobTicketID)).FirstOrDefault();

            formPreview.AddData("putOutStorageTicket", putOutStorageTicketView);
            formPreview.AddData("putOutStorageTicketItems", putOutStorageTicketTiemViews);
            formPreview.AddData("shipmentTicket", shipmentTicketView);
            formPreview.Show();
            this.Close();
        }
    }
}
