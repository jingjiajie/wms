using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WMS.UI
{
    public partial class FormStockInfoCheckTicketModify : Form
    {
        private FormMode mode = FormMode.ALTER;
        private int stockInfoCheckID = -1;
        private WMS.DataAccess.WMSEntities wmsEntities = new WMS.DataAccess.WMSEntities();
        private Action modifyFinishedCallback = null;
        private Action addFinishedCallback = null;



        public FormStockInfoCheckTicketModify(int stockInfoCheckID = -1)
        {
            InitializeComponent();
            this.stockInfoCheckID = stockInfoCheckID;
        }

        private void FormStockCheckModify_Load(object sender, EventArgs e)
        {

            if (this.mode == FormMode.ALTER && this.stockInfoCheckID == -1)
            {
                throw new Exception("未设置源库存信息");
            }

            for (int i = 0; i < StockInfoCheckTicketViewMetaData.KeyNames.Length; i++)
            {
                KeyName curKeyName = StockInfoCheckTicketViewMetaData.KeyNames[i];
                if (curKeyName.Visible == false && curKeyName.Editable == false) //&& curKeyName.Name != "ID")
                {
                    continue;
                }
                Label label = new Label();
                label.Text = curKeyName.Name;
                this.tableLayoutPanel2.Controls.Add(label);

                TextBox textBox = new TextBox();
                textBox.Name = "textBox" + curKeyName.Key;
                if (curKeyName.Editable == false)
                {
                    textBox.Enabled = false;
                }
                this.tableLayoutPanel2.Controls.Add(textBox);
            }



            if (this.mode == FormMode.ALTER)
            {
                WMS.DataAccess.StockInfoCheckTicketView  stockInfoCheckView = (from s in this.wmsEntities.StockInfoCheckTicketView
                                               where s.ID == this.stockInfoCheckID
                                               select s).Single();

                Utilities.CopyPropertiesToTextBoxes(stockInfoCheckView, this);
            }

             
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            WMS.DataAccess.StockInfoCheckTicket   stockInfoCheck = null;
             
            if (this.mode == FormMode.ALTER)
            {
                stockInfoCheck = (from s in this.wmsEntities.StockInfoCheckTicket
                             where s.ID == this.stockInfoCheckID 
                             select s).Single();
            }
            else if (mode == FormMode.ADD)
            {
                stockInfoCheck = new WMS.DataAccess.StockInfoCheckTicket();
                this.wmsEntities.StockInfoCheckTicket.Add(stockInfoCheck);
            }
            //开始数据库操作
            if (Utilities.CopyTextBoxTextsToProperties(this, stockInfoCheck, StockInfoCheckTicketViewMetaData .KeyNames, out string errorMessage) == false)
            {
                MessageBox.Show(errorMessage, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
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


        private void tableLayoutPanelProperties_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            FormStockInfoCheckTicketComponenModify a1 = new FormStockInfoCheckTicketComponenModify();
            a1.Show();
        }




        public void SetMode(FormMode mode)
        {
            this.mode = mode;
            if (mode == FormMode.ALTER)
            {
                this.Text = "修改库存信息";
                this.buttonOK.Text = "修改库存信息";
            }
            else if (mode == FormMode.ADD)
            {
                this.Text = "添加库存信息";
                this.buttonOK.Text = "添加库存信息";
            }
        }


    }
}
