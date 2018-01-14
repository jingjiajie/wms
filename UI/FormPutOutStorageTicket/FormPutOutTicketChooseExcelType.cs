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

        private void buttonZhongDu_Click(object sender, EventArgs e)
        {
            StandardFormPreviewExcel formPreview = new StandardFormPreviewExcel("出库单预览");
            if (formPreview.SetPatternTable(@"Excel\patternPutOutStorageTicketZhongDu.xlsx") == false)
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

        private void buttonMoBiSi_Click(object sender, EventArgs e)
        {
            StandardFormPreviewExcel formPreview = new StandardFormPreviewExcel("出库单预览");
            WMSEntities wmsEntities = new WMSEntities();
            PutOutStorageTicketView putOutStorageTicketView = (from p in wmsEntities.PutOutStorageTicketView
                                                               where p.ID == this.putOutStorageTicketID
                                                               select p).FirstOrDefault();
            PutOutStorageTicketItemView[] putOutStorageTicketItemViews =
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

            int[] supplierIDs = (from item in putOutStorageTicketItemViews
                                 where item.SupplierID != null
                                 select item.SupplierID.Value).ToArray();
            Supplier[] suppliers = (from s in wmsEntities.Supplier
                                    where supplierIDs.Contains(s.ID)
                                    select s).ToArray();

            foreach(Supplier supplier in suppliers)
            {
                formPreview.AddPatternTable(@"Excel\patternPutOutStorageTicketMoBiSi.xlsx",supplier.Name);
                formPreview.AddData("ticket", putOutStorageTicketView,supplier.Name);
                formPreview.AddData("items", (from item in putOutStorageTicketItemViews where item.SupplierID == supplier.ID select item).ToArray(), supplier.Name);
                formPreview.AddData("shipmentTicket", shipmentTicketView, supplier.Name);
                formPreview.AddData("supplier", supplier, supplier.Name);
            }

            //没有供应商信息的单独显示一个tab（非正常情况）
            PutOutStorageTicketItemView[] noSupplierItems = (from item in putOutStorageTicketItemViews
                                                           where item.SupplierID == null
                                                           select item).ToArray();
            if (noSupplierItems.Length > 0)
            {
                formPreview.AddPatternTable(@"Excel\patternPutOutStorageTicketMoBiSi.xlsx", "无供应商");
                formPreview.AddData("ticket", putOutStorageTicketView, "无供应商");
                formPreview.AddData("items", noSupplierItems, "无供应商");
                formPreview.AddData("shipmentTicket", shipmentTicketView, "无供应商");
                formPreview.AddData("supplier", default(Supplier), "无供应商");
            }

            formPreview.Show();
            this.Close();
        }
    }
}
