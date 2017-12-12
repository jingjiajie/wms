using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WMS.DataAccess;

namespace WMS.UI.FormReceipt
{
    public partial class FormReceiptTicketModify : Form
    {
        FormMode formMode;
        int ID;
        WMSEntities wmsEntities = new WMSEntities();
        Action modifyFinishedCallback = null;
        Action addFinishedCallback = null;

        public FormReceiptTicketModify()
        {
            InitializeComponent();
        }
        public FormReceiptTicketModify(FormMode formMode, int ID)
        {
            InitializeComponent();
            this.formMode = formMode;
            this.ID = ID;
        }

        private void ReceiptTicketModify_Load(object sender, EventArgs e)
        {
            this.tableLayoutPanelTextBoxes.Controls.Clear();
            for (int i = 0; i < ReceiptMetaData.receiptNameKeys.Length; i++)
            {
                KeyName curKeyName = ReceiptMetaData.receiptNameKeys[i];
                
                Label label = new Label();
                label.Text = curKeyName.Name;
                this.tableLayoutPanelTextBoxes.Controls.Add(label);

                TextBox textBox = new TextBox();
                textBox.Name = "textBox" + curKeyName.Key;
                if(this.formMode == FormMode.ALTER && curKeyName.Editable == false)
                {
                    textBox.Enabled = false;
                }
                
                this.tableLayoutPanelTextBoxes.Controls.Add(textBox);
            }
            if (this.formMode == FormMode.ALTER)
            {
                ReceiptTicketView receiptTicketView = (from s in this.wmsEntities.ReceiptTicketView
                                                       where s.ID == this.ID
                                                       select s).Single();
                Utilities.CopyPropertiesToTextBoxes(receiptTicketView, this);
            }
            //else (this.formMode == FormMode.ADD);
            
            //this.Controls.Find("textBoxID", true)[0].TextChanged += textBoxID_TextChanged;
            this.Controls.Find("textBoxProjectID", true)[0].TextChanged += textBoxProjectID_TextChanged;
            this.Controls.Find("textBoxWarehouse", true)[0].TextChanged += textBoxWarehouseID_TextChanged;
            this.Controls.Find("textBoxSupplierID", true)[0].TextChanged += textBoxSupplierID_TextChanged;
            this.Controls.Find("textBoxState", true)[0].Text = "待检";
            this.Controls.Find("textBoxState", true)[0].Enabled = false;
        }

        private void textBoxSupplierID_TextChanged(object sender, EventArgs e)
        {
            var textBoxSupplierID = this.Controls.Find("textBoxSupplierID", true)[0];
            string supplierID = textBoxSupplierID.Text;
            int iSupplierID;
            if (int.TryParse(supplierID, out iSupplierID) == false)
            {
                return;
            }
            try
            {
                Supplier supplierName = (from s in this.wmsEntities.Supplier where s.ID == iSupplierID select s).Single();
                this.Controls.Find("TextBoxSupplierName", true)[0].Text = supplierName.Name;
            }
            catch
            {

            }
        }

        private void textBoxWarehouseID_TextChanged(object sender, EventArgs e)
        {
            var textBoxWarehouseID = this.Controls.Find("textBoxWarehouse", true)[0];
            string warehouseID = textBoxWarehouseID.Text;
            int iWarehouseID;
            if (int.TryParse(warehouseID, out iWarehouseID) == false)
            {
                return;
            }
            try
            {
                Warehouse warehouseName = (from s in this.wmsEntities.Warehouse where s.ID == iWarehouseID select s).Single();
                this.Controls.Find("TextBoxWarehouseName", true)[0].Text = warehouseName.Name;
            }
            catch
            {

            }
            
        }

        private void textBoxProjectID_TextChanged(object sender, EventArgs e)
        {
            var textBoxProjectID = this.Controls.Find("textBoxProjectID", true)[0];
            string projectID = textBoxProjectID.Text;
            int iProjectID;
            if (int.TryParse(projectID, out iProjectID) == false)
            {
                return;
            }
            try
            {
                Project projectName = (from s in this.wmsEntities.Project where s.ID == iProjectID select s).Single();
                this.Controls.Find("TextBoxProjectName", true)[0].Text = projectName.Name;
            }
            catch
            {

            }
        }
        /*
        private void textBoxID_TextChanged(object sender, EventArgs e)
        {
            var textBoxID = this.Controls.Find("textBoxID", true)[0];
            string id = textBoxID.Text;
            if (textBoxID.Text == "")
            {
                return;
            }
            int receiptTicketID = 0;
            if (int.TryParse(id, out receiptTicketID) == false)
            {
                return;
            }
            try
            {
                ReceiptTicketView receiptTicketView = (from s in this.wmsEntities.ReceiptTicketView
                                                       where s.ID == this.ID
                                                       select s).Single();
                Utilities.CopyPropertiesToTextBoxes(receiptTicketView, this);
            }
            catch { }
        }*/

        public void SetModifyFinishedCallback(Action callback)
        {
            this.modifyFinishedCallback = callback;
        }

        public void SetAddFinishedCallback(Action callback)
        {
            this.addFinishedCallback = callback;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.formMode == FormMode.ALTER)
            {
                ReceiptTicket receiptTicket = (from rt in this.wmsEntities.ReceiptTicket where rt.ID == this.ID select rt).Single();
                string errorInfo;
                if (Utilities.CopyTextBoxTextsToProperties(this, receiptTicket, ReceiptMetaData.receiptNameKeys, out errorInfo) == false)
                {
                    MessageBox.Show(errorInfo);
                    return;
                }
                //wmsEntities.ReceiptTicket.Add(receiptTicket);
                else
                {
                    wmsEntities.SaveChanges();
                    MessageBox.Show("Successful!");
                }
            }
            else
            {
                ReceiptTicket receiptTicket = new ReceiptTicket();
                string errorInfo;
                if (Utilities.CopyTextBoxTextsToProperties(this, receiptTicket, ReceiptMetaData.receiptNameKeys, out errorInfo) == false)
                {
                    MessageBox.Show(errorInfo);
                    return;
                }
                //wmsEntities.ReceiptTicket.Add(receiptTicket);
                else
                {
                    wmsEntities.ReceiptTicket.Add(receiptTicket);
                    wmsEntities.SaveChanges();
                    MessageBox.Show("Successful!");
                }
            }
            modifyFinishedCallback();
            this.Close();
        }
    }
}
