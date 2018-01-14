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
        private Func<int> personIDGetter;

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
            /*
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
                
            }*/
            Utilities.CreateEditPanel(this.tableLayoutPanelTextBoxes, ReceiptMetaData.receiptNameKeys);
            this.personIDGetter = Utilities.BindTextBoxSelect<FormSelectPerson, Person>(this, "textBoxPersonName", "Name");
            if (this.formMode == FormMode.ALTER)
            {
                ReceiptTicketView receiptTicketView = (from s in this.wmsEntities.ReceiptTicketView
                                                       where s.ID == this.ID
                                                       select s).Single();
                Utilities.CopyPropertiesToTextBoxes(receiptTicketView, this);
                Utilities.CopyPropertiesToComboBoxes(receiptTicketView, this);
                if (receiptTicketView.SupplierID != null)
                {
                    this.supplierID = (int)receiptTicketView.SupplierID;
                }
                else
                {
                    MessageBox.Show("请重新选择供应商");
                }
                //this.Controls.Find("comboBoxState", true)[0].Enabled = false;
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
                this.Controls.Find("comboBoxState", true)[0].Text = "待送检";
                //this.Controls.Find("textBoxState", true)[0].Enabled = false;
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
            formSelectSupplier.SetSelectFinishCallback(new Action<int>((int ID)=> 
            {
                //this.Controls.Find("textBoxSupplierNo", true)[0].Text = ID.ToString();
                Supplier supplier = (from s in wmsEntities.Supplier where s.ID == ID select s).FirstOrDefault();
                if (supplier == null)
                {
                    MessageBox.Show("该供应商已被删除");
                    return;
                }
                else
                {
                    if (supplier.EndingTime < DateTime.Now)
                    {
                       if (MessageBox.Show("改供货商合同截止日期已过，是否继续？","提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            this.supplierID = ID;
                        }
                        else
                        {
                            return;
                        }
                    }
                }
                this.supplierID = ID;
                //Supplier supplier = (from s in wmsEntities.Supplier where s.ID == ID select s).FirstOrDefault();
                if (supplier != null)
                {
                    this.Controls.Find("textBoxSupplierName", true)[0].Text = supplier.Name;
                    
                    //this.Controls.Find("textBoxSupplierNo", true)[0].Text
                }
            }));
            formSelectSupplier.ShowDialog();

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
            string oldState;
            TextBox textBox = this.Controls.Find("textBoxSupplierName", true)[0] as TextBox;
            if (textBox.Text == "")
            {
                MessageBox.Show("请选择供货商!");
                return;
            }
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
                    oldState = receiptTicket.State;
                    if (Utilities.CopyComboBoxsToProperties(this, receiptTicket,ReceiptMetaData.receiptNameKeys) == false)
                    {
                        MessageBox.Show("状态获取失败。");
                        return;
                    }
                    receiptTicket.LastUpdateTime = DateTime.Now;
                    receiptTicket.LastUpdateUserID = this.userID;
                    receiptTicket.ProjectID = this.projectID;
                    receiptTicket.Warehouse = this.warehouseID;
                    receiptTicket.SupplierID = this.supplierID;
                    receiptTicket.PersonID = this.personIDGetter();
                    wmsEntities.SaveChanges();
                    if (oldState == "待送检")
                    {
                        if (receiptTicket.State == "已收货")
                        {
                            if (MessageBox.Show("是否同时将所有条目置为收货，并将货物放置溢库区？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                foreach (ReceiptTicketItem rti in receiptTicket.ReceiptTicketItem)
                                {
                                    StockInfo stockInfo = (from si in wmsEntities.StockInfo where si.ReceiptTicketItemID == rti.ID select si).FirstOrDefault();
                                    if (stockInfo == null)
                                    {
                                        return;
                                    }
                                    stockInfo.OverflowAreaAmount += stockInfo.ReceiptAreaAmount;
                                    stockInfo.ReceiptAreaAmount -= stockInfo.ReceiptAreaAmount;
                                    rti.State = receiptTicket.State;
                                }
                            }
                        }
                        else if (receiptTicket.State == "拒收")
                        {
                            if (MessageBox.Show("是否同时将所有条目置为拒收？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                foreach (ReceiptTicketItem rti in receiptTicket.ReceiptTicketItem)
                                {
                                    rti.State = receiptTicket.State;
                                }
                            }
                        }
                    }
                    else if (oldState == "已收货")
                    {
                        if (receiptTicket.State == "待送检" || receiptTicket.State == "拒收")
                        {
                            if (MessageBox.Show("是否同时将所有条目置为" + receiptTicket.State + "？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                foreach(ReceiptTicketItem rti in receiptTicket.ReceiptTicketItem)
                                {
                                    rti.State = receiptTicket.State;
                                    StockInfo stockInfo = (from si in wmsEntities.StockInfo where si.ReceiptTicketItemID == rti.ID select si).FirstOrDefault();
                                    if (stockInfo == null)
                                    {
                                        return;
                                    }
                                    stockInfo.ReceiptAreaAmount += stockInfo.OverflowAreaAmount;
                                    stockInfo.OverflowAreaAmount -= stockInfo.OverflowAreaAmount;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (receiptTicket.State == "待送检")
                        {
                            if (MessageBox.Show("是否同时将所有零件置为待送检？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                foreach (ReceiptTicketItem rti in receiptTicket.ReceiptTicketItem)
                                {
                                    rti.State = receiptTicket.State;
                                }
                            }
                        }
                        else if (receiptTicket.State == "已收货")
                        {
                            if (MessageBox.Show("是否同时将所有零件置为已收货，并将货物移到溢库区？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                foreach(ReceiptTicketItem rti in receiptTicket.ReceiptTicketItem)
                                {
                                    StockInfo stockInfo = (from si in wmsEntities.StockInfo where si.ReceiptTicketItemID == rti.ID select si).FirstOrDefault();
                                    if (stockInfo == null)
                                    {
                                        return;
                                    }
                                    stockInfo.OverflowAreaAmount += stockInfo.ReceiptAreaAmount;
                                    stockInfo.ReceiptAreaAmount -= stockInfo.ReceiptAreaAmount;
                                    rti.State = receiptTicket.State;
                                }
                            }
                        }
                    }
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
                    if (Utilities.CopyComboBoxsToProperties(this, receiptTicket, ReceiptMetaData.receiptNameKeys) == false)
                    {
                        MessageBox.Show("状态获取失败。");
                        return;
                    }
                    receiptTicket.LastUpdateUserID = this.userID;
                    receiptTicket.Warehouse = this.warehouseID;
                    receiptTicket.ProjectID = this.projectID;
                    receiptTicket.CreateUserID = this.userID;
                    receiptTicket.LastUpdateTime = DateTime.Now;
                    receiptTicket.CreateTime = DateTime.Now;
                    receiptTicket.SupplierID = this.supplierID;
                    receiptTicket.HasPutawayTicket = "否";
                    receiptTicket.PersonID = this.personIDGetter();
                    wmsEntities.ReceiptTicket.Add(receiptTicket);
                    wmsEntities.SaveChanges();

                    ////////////////////////////
                    if (string.IsNullOrWhiteSpace(receiptTicket.No))
                    {
                        if (receiptTicket.CreateTime.HasValue == false)
                        {
                            MessageBox.Show("单号生成失败（未知创建日期）！请手动填写单号");
                            return;
                        }

                        DateTime createDay = new DateTime(receiptTicket.CreateTime.Value.Year, receiptTicket.CreateTime.Value.Month, receiptTicket.CreateTime.Value.Day);
                        DateTime nextDay = createDay.AddDays(1);
                        int maxRankOfToday = Utilities.GetMaxTicketRankOfDay((from s in wmsEntities.ReceiptTicket
                                                                              where s.CreateTime >= createDay && s.CreateTime < nextDay
                                                                              select s.No).ToArray());
                        if (maxRankOfToday == -1)
                        {
                            MessageBox.Show("单号生成失败！请手动填写单号");
                            return;
                        }
                        receiptTicket.No = Utilities.GenerateTicketNo("S", receiptTicket.CreateTime.Value, maxRankOfToday + 1);
                    }
                    if (string.IsNullOrWhiteSpace(receiptTicket.Number))
                    {
                        if (receiptTicket.CreateTime.HasValue == false)
                        {
                            MessageBox.Show("单号生成失败（未知创建日期）！请手动填写编号");
                            return;
                        }
                        Supplier supplier = (from s in wmsEntities.Supplier
                                             where s.ID == receiptTicket.SupplierID
                                             select s).FirstOrDefault();
                        if (supplier == null)
                        {
                            MessageBox.Show("编号生成失败（供应商信息不存在）！请手动填写编号");
                            return;
                        }
                        DateTime createMonth = new DateTime(receiptTicket.CreateTime.Value.Year, receiptTicket.CreateTime.Value.Month, 1);
                        DateTime nextMonth = createMonth.AddMonths(1);
                        var tmp = (from s in wmsEntities.ReceiptTicket
                                   where s.CreateTime >= createMonth &&
                                         s.CreateTime < nextMonth &&
                                         s.SupplierID == receiptTicket.SupplierID
                                   select s.Number).ToArray();
                        int maxRankOfMonth = Utilities.GetMaxTicketRankOfSupplierAndMonth(tmp);
                        receiptTicket.Number = Utilities.GenerateTicketNumber(supplier.Number, receiptTicket.CreateTime.Value, maxRankOfMonth + 1);
                    }

                    ///////////////////////////////////////////////////////////////

                    //receiptTicket.No = Utilities.GenerateNo("H", receiptTicket.ID);
                    wmsEntities.SaveChanges();
                    //MessageBox.Show("Successful!");
                }
            }
            
            modifyFinishedCallback();
            this.Close();
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
