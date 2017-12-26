using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WMS.DataAccess;
using WMS.UI.FormBase;

namespace WMS.UI.FormReceipt
{
    public partial class FormReceiptTicketModify : Form
    {
        private FormMode formMode;
        private int ID;
        private WMSEntities wmsEntities = new WMSEntities();
        private Action modifyFinishedCallback = null;
        private Action addFinishedCallback = null;
        private int projectID;
        private int warehouseID;
        private int userID;
        private int supplierID;

        public FormReceiptTicketModify()
        {
            InitializeComponent();
        }
        public FormReceiptTicketModify(FormMode formMode, int ID, int projectID, int warehouseID ,int userID)
        {
            InitializeComponent();
            this.formMode = formMode;
            this.ID = ID;
            this.projectID = projectID;
            this.warehouseID = warehouseID;
            this.userID = userID;
        }

        private void ReceiptTicketModify_Load(object sender, EventArgs e)
        {
            this.tableLayoutPanelTextBoxes.Controls.Clear();
            for (int i = 0; i < ReceiptMetaData.receiptNameKeys.Length; i++)
            {
                KeyName curKeyName = ReceiptMetaData.receiptNameKeys[i];
                if (curKeyName.Editable == false && curKeyName.Visible == false && curKeyName.Save == true)
                {
                    continue;
                }
                Label label = new Label();
                label.AutoSize = true;
                label.Text = curKeyName.Name;
                this.tableLayoutPanelTextBoxes.Controls.Add(label);

                TextBox textBox = new TextBox();
                textBox.Name = "textBox" + curKeyName.Key;
                if(curKeyName.Editable == false)
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
                this.Controls.Find("textBoxState", true)[0].Enabled = false;
                //TextBox textBoxLastUpdateUserID = (TextBox)this.Controls.Find("textBoxLastUpdateUserUserID", true)[0];
                //textBoxLastUpdateUserID.Text = this.userID.ToString();
            }
            else
            {
                Warehouse warehouse = (from wh in wmsEntities.Warehouse where wh.ID == this.warehouseID select wh).FirstOrDefault();
                if (warehouse == null)
                {
                    Console.Write("this warehouse is null");
                }
                else
                {
                    TextBox textBoxWarehouseName = (TextBox)this.Controls.Find("textBoxWarehouseName", true)[0];
                    textBoxWarehouseName.Text = warehouse.Name;
                    textBoxWarehouseName.Enabled = false;
                }
                Project project = (from p in wmsEntities.Project where p.ID == this.projectID select p).FirstOrDefault();
                if (project == null)
                {
                    Console.Write("this project is null");
                }
                else
                {
                    TextBox textBoxProjectName = (TextBox)this.Controls.Find("textBoxProjectName", true)[0];
                    textBoxProjectName.Text = project.Name;
                    textBoxProjectName.Enabled = false;
                    /*
                    TextBox textBoxWarehouseName = (TextBox)this.Controls.Find("textBoxWarehouseName", true)[0];
                    textBoxWarehouseName.Text = warehouse.Name;
                    textBoxWarehouseName.Enabled = false;*/
                }
                this.Controls.Find("textBoxState", true)[0].Text = "待送检";
                this.Controls.Find("textBoxState", true)[0].Enabled = false;
                //TextBox textBoxProjectID = (TextBox)this.Controls.Find("textBoxProjectID", true)[0];
                //textBoxProjectID.Text = this.projectID.ToString();
                //textBoxProjectID.Enabled = false;
                //TextBox textBoxCreateUserID = (TextBox)this.Controls.Find("textBoxCreateUserID", true)[0];
                //textBoxCreateUserID.Text = this.userID.ToString();
                //TextBox textBoxWarehouse = (TextBox)this.Controls.Find("textBoxWarehouse", true)[0];
                //textBoxWarehouse.Text = this.warehouseID.ToString();
                //TextBox textBoxID = (TextBox)this.Controls.Find("textBoxID", true)[0];
                //textBoxID.Text = "0";
                //textBoxCreateUserID.Enabled = false;
            }
            //else (this.formMode == FormMode.ADD);

            //this.Controls.Find("textBoxID", true)[0].TextChanged += textBoxID_TextChanged;
            //this.Controls.Find("textBoxProjectID", true)[0].TextChanged += textBoxProjectID_TextChanged;
            //this.Controls.Find("textBoxWarehouse", true)[0].TextChanged += textBoxWarehouseID_TextChanged;
            //this.Controls.Find("textBoxSupplierID", true)[0].TextChanged += textBoxSupplierID_TextChanged;
            this.Controls.Find("textBoxSupplierName", true)[0].Click += textBoxSupplierID_Click;
            
        }
        
        private void textBoxSupplierID_Click(object sender, EventArgs e)
        {
            FormSelectSupplier formSelectSupplier = new FormSelectSupplier();
            formSelectSupplier.Show();
            formSelectSupplier.SetSelectFinishCallback(new Action<int>((int ID)=> 
            {
                //this.Controls.Find("textBoxSupplierNo", true)[0].Text = ID.ToString();
                this.supplierID = ID;
                Supplier supplier = (from s in wmsEntities.Supplier where s.ID == ID select s).FirstOrDefault();
                if (supplier != null)
                {
                    this.Controls.Find("textBoxSupplierName", true)[0].Text = supplier.Name;
                    //this.Controls.Find("textBoxSupplierNo", true)[0].Text
                }
            }));
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
                    receiptTicket.LastUpdateTime = DateTime.Now;
                    receiptTicket.LastUpdateUserID = this.userID;
                    receiptTicket.ProjectID = this.projectID;
                    receiptTicket.Warehouse = this.warehouseID;
                    wmsEntities.SaveChanges();
                    //MessageBox.Show("Successful!");
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
                    receiptTicket.LastUpdateUserID = this.userID;
                    receiptTicket.Warehouse = this.warehouseID;
                    receiptTicket.ProjectID = this.projectID;
                    receiptTicket.CreateUserID = this.userID;
                    receiptTicket.LastUpdateTime = DateTime.Now;
                    receiptTicket.CreateTime = DateTime.Now;
                    receiptTicket.SupplierID = this.supplierID;
                    wmsEntities.ReceiptTicket.Add(receiptTicket);
                    wmsEntities.SaveChanges();
                    receiptTicket.No = Utilities.GenerateNo("H", receiptTicket.ID);
                    wmsEntities.SaveChanges();
                    //MessageBox.Show("Successful!");
                }
            }
            modifyFinishedCallback();
            this.Close();
        }
    }
}
