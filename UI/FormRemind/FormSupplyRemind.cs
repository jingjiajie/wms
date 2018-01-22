using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using WMS.UI.FormReceipt;
using WMS.UI.FormBase;
using WMS.DataAccess;
namespace WMS.UI
{
    public partial class FormSupplyRemind : Form
    {
        string remind;

        public  StringBuilder  sb = new StringBuilder();
        
        public FormSupplyRemind()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Enabled = true;
            timer.Interval = 30000;//执行间隔时间,单位为毫秒  一千分之一
            timer.Start();
            timer.Elapsed += new System.Timers.ElapsedEventHandler(Timer1_Elapsed);


            //this.Opacity = 109;
        }

        private void Timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.sb = new StringBuilder();
            RemindSupply();
            RemindStock();
            TextDeliver();
            //MessageBox.Show("执行了一次程序");
        }

        public   void RemindSupply()
        {
            //存货有效期
            WMSEntities wmsEntities = new WMSEntities();
            
            StockInfoView[] StockInfoView = null;




            try
            {
                StockInfoView = (from u in wmsEntities.StockInfoView
                                 select u).ToArray();

                for (int i = 0; i < StockInfoView.Length; i++)
                {
                    //找到每个零件的保质期
                    string ComponentName = StockInfoView[i].ComponentName;
                    string SupplierName = StockInfoView[i].SupplierName;
                    string SupplyNo = StockInfoView[i].SupplyNo;
                    if (ComponentName == null || SupplierName == null || SupplyNo == null || StockInfoView[i].InventoryDate == null)
                    {

                        continue;
                    }
                    DateTime InventoryDate = Convert.ToDateTime(StockInfoView[i].InventoryDate);
                    var SafetyDate1 = (from u in wmsEntities.SupplyView
                                       where u.ComponentName == ComponentName &&
                                       u.SupplierName == SupplierName &&
                                       u.No == SupplyNo
                                       select u).FirstOrDefault();

                    //到期日期
                    if (SafetyDate1.ValidPeriod == null)
                    {
                        continue;
                    }
                    var SafetyDate = InventoryDate.AddDays(Convert.ToDouble(SafetyDate1.ValidPeriod));

                    if (SafetyDate <= DateTime.Now)
                    {

                        sb.Append(SupplierName + "  " + ComponentName + "  " + SupplyNo + "  " + "存货日期" + " " + InventoryDate + "\r\n" + "\r\n");

                    }

                }
            }catch

            {
                sb = new StringBuilder();
                sb.Append("刷新失败,请检查网络连接");
                return;
            }


        }

        public  void RemindStock()
        {
            WMSEntities wmsEntities = new WMSEntities();
            SupplyView[] SupplyView = null;
            decimal  amount=0;

            try
            {
                SupplyView = (from u in wmsEntities.SupplyView
                              select u).ToArray();


                for (int i = 0; i < SupplyView.Length; i++)
                {

                    string ComponentName = SupplyView[i].ComponentName;
                    string SupplierName = SupplyView[i].SupplierName;
                    string SupplyNo = SupplyView[i].No;
                    decimal SaftyStock;
                    StockInfoView[] stockInfo = null;
                    if (SupplyView[i].SafetyStock == null)
                    {




                        continue;



                    }

                    stockInfo = (from kn in wmsEntities.StockInfoView
                                 where kn.ComponentName == ComponentName &&
                                 kn.SupplierName == SupplierName &&
                                 kn.SupplyNo == SupplyNo
                                 select kn).ToArray();
                    if (stockInfo == null)
                    {
                        continue;
                    }
                    SaftyStock = Convert.ToDecimal(SupplyView[i].SafetyStock);

                    for (int j = 0; j < stockInfo.Length; j++)

                    {
                        amount = Convert.ToDecimal(stockInfo[j].OverflowAreaAmount) + Convert.ToDecimal(stockInfo[j].ShipmentAreaAmount);

                    }

                    if (amount < SaftyStock)
                    {
                        sb.Append(SupplierName + "  " + ComponentName + "  " + SupplyNo + "  " + "库存量" + "  " + amount + "  " + "已小于安全库存" + "   " + SaftyStock + "\r\n" + "\r\n");

                    }


                }
            }
            catch
            {


                sb = new StringBuilder();
                sb.Append("刷新失败,请检查网络连接");
                return;
            }






        }






        public void TextDeliver()
        {

            this.textBox1.Text = sb.ToString();
            //MessageBox.Show("4646");
            if (this.textBox1 .Text =="刷新失败,请检查网络连接")
            {
                this.textBox1.ForeColor = Color.Red;
                
            }
            else
            {
                this.textBox1.ForeColor = Color.Black;
            }

        }

        





















        private void FormSupplyRemind_Load(object sender, EventArgs e)
        {
            double a = 0.35;
            this.Top = 0;//25
            this.Left = (int)(a * Screen.PrimaryScreen.Bounds.Width);
            this.Width = 400;
            this.Height = 100;//75
            this.textBox1.Text = "";
            this.TransparencyKey = System.Drawing.Color.Black;//设置黑的是透明色
            this.BackColor = System.Drawing.Color.Black;//把窗口的背景色设置为黑
            this.ShowInTaskbar = false;///使窗体不显示在任务栏
            this.sb = new StringBuilder();
            RemindSupply();
            RemindStock();
            TextDeliver();




        }






        

    }

    
}
