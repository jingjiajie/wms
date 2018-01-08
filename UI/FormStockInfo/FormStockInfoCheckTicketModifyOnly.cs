using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using unvell.ReoGrid;
using System.Threading;
using System.Data.SqlClient;

namespace WMS.UI
{
    public partial class FormStockInfoCheckTicketModifyOnly : Form
    {
        private FormMode mode = FormMode.ALTER;
        private int stockInfoCheckID = -1;
        private int projectID = -1;
        private int warehouseID = -1;
        private int userID = -1;
        private WMS.DataAccess.WMSEntities wmsEntities = new WMS.DataAccess.WMSEntities();
        private Action modifyFinishedCallback = null;
        private Action addFinishedCallback = null;
        //private Action checkFinishedCallback = null;




        public FormStockInfoCheckTicketModifyOnly(int projectID, int warehouseID, int userID, int stockInfoCheckID = -1)
        {
            InitializeComponent();
            this.stockInfoCheckID = stockInfoCheckID;
            this.projectID = projectID;
            this.warehouseID = warehouseID;
            this.userID = userID;

        }

        private void FormStockCheckModify_Load(object sender, EventArgs e)
        {

            if (this.mode == FormMode.ALTER && this.stockInfoCheckID == -1)
            {
                throw new Exception("未设置源库存信息");
            }
            if (this.mode == FormMode.ADD || this.mode == FormMode.ALTER)
            {


                this.MinimizeBox = false;
                this.MaximizeBox = false;


            }
            if (this.mode == FormMode.ADD)
            {
                this.labelStatus.Text = "添加盘点单";

            }
            if (this.mode == FormMode.ALTER)
            {
                this.labelStatus.Text = "修改盘点单";

            }
            if (this.mode == FormMode.CHECK)
            {

                this.labelStatus.Text = "盘点单条目";
                // this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;


            }

            for (int i = 0; i < StockInfoCheckTicketViewMetaData.KeyNames.Length; i++)
            {
                KeyName curKeyName = StockInfoCheckTicketViewMetaData.KeyNames[i];

                if (curKeyName.Visible == false && curKeyName.Editable == false) 
                {
                    continue;
                }
                Label label = new Label();
                label.Text = curKeyName.Name;
                this.tableLayoutPanel3.Controls.Add(label);

                TextBox textBox = new TextBox();
                textBox.Name = "textBox" + curKeyName.Key;
                if (curKeyName.Editable == false || this.mode == FormMode.CHECK)
                {
                    textBox.Enabled = false;
                }
                this.tableLayoutPanel3.Controls.Add(textBox);
            }



            if (this.mode == FormMode.ALTER || this.mode == FormMode.CHECK)
            {
                WMS.DataAccess.StockInfoCheckTicketView stockInfoCheckView = (from s in this.wmsEntities.StockInfoCheckTicketView
                                                                              where s.ID == this.stockInfoCheckID
                                                                              select s).Single();

                Utilities.CopyPropertiesToTextBoxes(stockInfoCheckView, this);
            }


            this.InitComponents();

        }
        private void InitComponents()
        {

        }



        private void buttonDelete_Click(object sender, EventArgs e)
        {
            WMS.DataAccess.StockInfoCheckTicket stockInfoCheck = null;

            if (this.mode == FormMode.ALTER)
            {
                try
                {
                    stockInfoCheck = (from s in this.wmsEntities.StockInfoCheckTicket
                                      where s.ID == this.stockInfoCheckID
                                      select s).Single();
                    stockInfoCheck.LastUpdateUserID = Convert.ToString(userID);
                }
                catch
                {
                    MessageBox.Show("要修改的项目已不存在，请确认后操作！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    this.Close();
                    return;
                }
            }
            else if (mode == FormMode.ADD)
            {
                stockInfoCheck = new WMS.DataAccess.StockInfoCheckTicket();
                this.wmsEntities.StockInfoCheckTicket.Add(stockInfoCheck);
                stockInfoCheck.CreateUserID = userID;
                stockInfoCheck.CheckDate = Convert.ToDateTime(DateTime.Now.ToLongDateString());
                stockInfoCheck.CreateTime = Convert.ToDateTime(DateTime.Now.ToLongTimeString());
                stockInfoCheck.LastUpdateTime = Convert.ToDateTime(DateTime.Now.ToLongTimeString());
            }

            stockInfoCheck.WarehouseID = warehouseID;
            stockInfoCheck.ProjectID = projectID;

            stockInfoCheck.LastUpdateUserID = Convert.ToString(userID);

            wmsEntities.SaveChanges();
            //调用回调函数

            if (this.mode == FormMode.ALTER && this.modifyFinishedCallback != null)
            {
                this.modifyFinishedCallback();
            }
            else if (this.mode == FormMode.ADD && this.addFinishedCallback != null)
            {
                this.addFinishedCallback();
            }


            this.Close();


        }
        public void SetModifyFinishedCallback(Action callback)
        {
            this.modifyFinishedCallback = callback;
        }

        public void SetAddFinishedCallback(Action callback)
        {
            this.addFinishedCallback = callback;
        }


       








        public void SetMode(FormMode mode)
        {
            this.mode = mode;
            if (mode == FormMode.ALTER)
            {
                this.Text = "修改盘点单信息";
                this.buttonModify.Text = "修改盘点单信息";

            }
            else if (mode == FormMode.ADD)
            {
                this.Text = "添加盘点单信息";
                this.buttonModify.Text = "添加盘点单信息";

            }
            else if (mode == FormMode.CHECK)
                this.Text = "盘点单条目";


        }





       

      
        private void buttonModify_Click(object sender, EventArgs e)
        {
            WMS.DataAccess.StockInfoCheckTicket stockInfoCheck = null;

            if (this.mode == FormMode.ALTER)
            {
                try
                {
                    stockInfoCheck = (from s in this.wmsEntities.StockInfoCheckTicket
                                      where s.ID == this.stockInfoCheckID
                                      select s).Single();
                    stockInfoCheck.LastUpdateUserID = Convert.ToString(userID);
                }
                catch
                {
                    MessageBox.Show("要修改的项目已不存在，请确认后操作！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    this.Close();
                    return;
                }
            }
            else if (mode == FormMode.ADD)
            {
                stockInfoCheck = new WMS.DataAccess.StockInfoCheckTicket();
                this.wmsEntities.StockInfoCheckTicket.Add(stockInfoCheck);
                stockInfoCheck.CreateUserID = userID;
                stockInfoCheck.CheckDate = Convert.ToDateTime(DateTime.Now.ToLongDateString());
                stockInfoCheck.CreateTime = Convert.ToDateTime(DateTime.Now.ToLongTimeString());
                stockInfoCheck.LastUpdateTime = Convert.ToDateTime(DateTime.Now.ToLongTimeString());
            }

            stockInfoCheck.WarehouseID = warehouseID;
            stockInfoCheck.ProjectID = projectID;

            stockInfoCheck.LastUpdateUserID = Convert.ToString(userID);

            wmsEntities.SaveChanges();
            //调用回调函数

            if (this.mode == FormMode.ALTER && this.modifyFinishedCallback != null)
            {
                this.modifyFinishedCallback();
            }
            else if (this.mode == FormMode.ADD && this.addFinishedCallback != null)
            {
                this.addFinishedCallback();
            }


            this.Close();

        }
    }
}

        
