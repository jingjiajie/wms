using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using WMS.UI.FormReceipt;
using WMS.UI.FormBase;
using WMS.DataAccess;

namespace WMS.UI
{



    public partial class FormSupplierRemind : Form
    {


        private int supplierid;
        private WMSEntities wmsEntities = new WMSEntities();
        WMS.DataAccess.ComponentView component = null;
        private DateTime contract_enddate;
        //private TimeSpan days;
        private int days;

        public FormSupplierRemind(int supplierid)
        {
            InitializeComponent();

            this.supplierid = supplierid;
        }





        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void FormSupplierRemind_Load(object sender, EventArgs e)
        {


            this.textBoxContractRemind.Text = " 无合同过期提醒";
            this.textBoxContractRemind.Font = new Font("宋体", 12, FontStyle.Bold);
            this.textBox3.Text = "无库存预警";
            this.textBox3 .Font = new Font("宋体", 12, FontStyle.Bold);
            contractrenmind();

            componentremind();
            



        }








        private void contractrenmind()


        {
            Supplier Supplier = new Supplier();

            Supplier = (from u in this.wmsEntities.Supplier
                        where u.ID == supplierid
                        select u).Single();





            if (Convert.ToString(Supplier.EndingTime) != string.Empty)
            {
                this.contract_enddate = Convert.ToDateTime(Supplier.EndingTime);
                days = (DateTime.Now - contract_enddate).Days;

                if ((-days) < 10)
                {

                    this.textBoxContractRemind.Text= "您的合同还有" + (-days) + "天就到期了";
                    

                }




                if (Supplier.EndingTime < DateTime.Now)
                {
                    this.textBoxContractRemind.Text = " 您的合同已经到截止日期";
                    this.textBoxContractRemind.Font = new Font("宋体", 12, FontStyle.Bold);

                }



            }


        }





        private void componentremind()
        {

            int[] warringdays = { 3, 5, 10 };
            int reminedays;
           
            var ShipmentAreaAmount = (from u in wmsEntities.StockInfoView
                       where u.ReceiptTicketSupplierID ==
                       this.supplierid
                       select u.ShipmentAreaAmount ).ToArray();
            var ComponentName = (from u in wmsEntities.StockInfoView
                           where u.ReceiptTicketSupplierID == supplierid
                           select u.ComponentName).ToArray();
            int[] singlecaramount = new int[ComponentName.Length] ;
            



            int[] dailyproduction = new int[ComponentName.Length];
            
            for (int i=0; i<ComponentName .Length;i++ )

            {
                

                string ComponentNamei = ComponentName[i];
                
                if (ShipmentAreaAmount[i] == null)
                {
                    MessageBox.Show(ComponentNamei + "的发货区内容没填写，跳过该零件的库存预警", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    continue;
                }







                component = (from u in wmsEntities.ComponentView
                              where u.Name == ComponentNamei
                             select u).FirstOrDefault ();
                if(component ==null)
                {
                    MessageBox.Show("没查找到零件"+ ComponentNamei+"可能已经被删除", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    continue ;
                 }

                if (component.SingleCarUsageAmount == null)
                {
                    MessageBox.Show(ComponentNamei + "的单台车用量内容没填写，跳过该零件的库存预警", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    continue ;

                }

                singlecaramount[i] = Convert.ToInt32(component.SingleCarUsageAmount);

                if (component.DailyProduction == null)
                {
                    MessageBox.Show(ComponentNamei + "的单日生产量内容没填写，跳过该零件的库存预警", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    continue ;

                }

                dailyproduction[i] = Convert.ToInt32(component.DailyProduction);
               

                reminedays = Convert.ToInt32(ShipmentAreaAmount[i]) / (singlecaramount[i] * dailyproduction[i]);



                if (reminedays < 10)
                {
                    if (this.textBox3.Text == "无库存预警")
                    {
                        this.textBox3.Text = "";
                        this.textBox3.AppendText("零件"+ ComponentName[i] + "大约仅可以用" + reminedays + "天");
                    }
                    else
                    {
                        this.textBox3.AppendText("零件"+ ComponentName[i] + "大约仅可以用" + reminedays + "天");
                    }

                }





            }





        }







            private void button1_Click(object sender, EventArgs e)
            {
                this.Close();
            }
        
    }
}