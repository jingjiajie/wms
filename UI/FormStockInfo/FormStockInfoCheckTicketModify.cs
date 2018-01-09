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
    public partial class FormStockInfoCheckTicketModify : Form
    {
        private FormMode mode = FormMode.ALTER;
        private int stockInfoCheckID = -1;
        private int projectID = -1;
        private int warehouseID = -1;
        private int userID = -1;
        private int personid = -1;
        private WMS.DataAccess.WMSEntities wmsEntities = new WMS.DataAccess.WMSEntities();
        private Action modifyFinishedCallback = null;
        private Action addFinishedCallback = null;
        //private Action checkFinishedCallback = null;




        public FormStockInfoCheckTicketModify(int projectID, int warehouseID, int userID, int stockInfoCheckID = -1)
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


            
            Utilities.CreateEditPanel(this.tableLayoutPanel3, StockInfoCheckTicketViewMetaData.KeyNames);
            this.Controls.Find("textBoxPersonID", true)[0].Click += textBoxPersonID_Click;

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







        private void textBoxPersonID_Click(object sender, EventArgs e)
             

        {
            var FormSelectPerson = new FormSelectPerson ();
            FormSelectPerson.SetSelectFinishCallback((selectedID) =>
            {

                var PersonName = (from s in wmsEntities.PersonView 
                                     where s.ID == selectedID
                                     select s).FirstOrDefault();
                if (PersonName.Name  == null)
                {
                    MessageBox.Show("选择人员信息失败，人员信息不存在", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //this.supplierID = selectedID;
                //selectedID = 1;
                this.personid  = selectedID;
                this.Controls.Find("textBoxPersonID", true)[0].Text = PersonName .Name ;
               
                





            });
            FormSelectPerson.Show();






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
                this.groupBox1.Text = "修改盘点单信息";

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
                    stockInfoCheck .LastUpdateTime = Convert.ToDateTime(DateTime.Now.ToLongTimeString());
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
            stockInfoCheck.PersonID = this.personid;

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

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}

        
