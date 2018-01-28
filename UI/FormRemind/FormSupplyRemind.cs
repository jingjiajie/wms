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
using System.Diagnostics;
using System.Threading;
namespace WMS.UI
{
    public partial class FormSupplyRemind : Form
    {
        string remind;
        //       
        public  StringBuilder stringBuilder = new StringBuilder();
        private int projectID = GlobalData.ProjectID;
        private int warehouseID = GlobalData.WarehouseID;
        Button  button= null;
        public FormSupplyRemind(Button button)
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            //this.FormBorderStyle = FormBorderStyle.None;

            this.button = button;
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

            ClearTextBox();
            RefreshID();
            RemindSupply();
            RemindStock();
            TextDeliver();
            //MessageBox.Show("执行了一次程序");
        }


       public void ClearTextBox()
        {
            this.stringBuilder = new StringBuilder();
        }

        public void RefreshID()
        {
            this.projectID = GlobalData.ProjectID;
            this.warehouseID = GlobalData.WarehouseID;
        }

        public   void RemindSupply()
        {
            DateTime before = DateTime.Now;
            //存货有效期
            WMSEntities wmsEntities = new WMSEntities();
            StockInfoView[] stockInfoView = null;
            try
            {
                stockInfoView = (from u in wmsEntities.StockInfoView
                                 where u.ProjectID ==this.projectID &&
                                 u.WarehouseID ==this.warehouseID 
                                 select u).ToArray();

                if(stockInfoView ==null)
                {
                    return;
                }

                for (int i = 0; i < stockInfoView.Length; i++)
                {
                    //找到每个零件的保质期
                    string ComponentName = stockInfoView[i].ComponentName;
                    string SupplierName = stockInfoView[i].SupplierName;
                    string SupplyNo = stockInfoView[i].SupplyNo;
                    if (ComponentName == null || SupplierName == null || SupplyNo == null || stockInfoView[i].InventoryDate == null)
                    {

                        continue;
                    }
                    DateTime InventoryDate = Convert.ToDateTime(stockInfoView[i].InventoryDate);
                    var SafetyDate1 = (from u in wmsEntities.SupplyView
                                       where u.ComponentName == ComponentName &&
                                       u.SupplierName == SupplierName &&
                                       u.No == SupplyNo&&u.ProjectID ==this.projectID 
                                       &&u.WarehouseID ==this.warehouseID &&u.IsHistory ==0
                                       select u).FirstOrDefault();
                    if(SafetyDate1 == null)
                    {
                        continue;
                    }

                    //到期日期
                    if (SafetyDate1.ValidPeriod == null)
                    {
                        continue;
                    }
                    var SafetyDate = InventoryDate.AddDays(Convert.ToDouble(SafetyDate1.ValidPeriod));
                    int day;
                    day = ( SafetyDate-DateTime .Now ).Days;
                    if (day<=30&&day>0)
                    {
                        stringBuilder.Append(SupplierName + "  " + ComponentName + "  " + SupplyNo + "  " + "存货日期" + " " + InventoryDate +"  " +"有效期还剩"+day+"天"+"\r\n" + "\r\n");
                    }
                    else if (day <= 0)
                    {
                        stringBuilder.Append(SupplierName + "  " + ComponentName + "  " + SupplyNo + "  " + "存货日期" + " " + InventoryDate + "  " + "已过期"  + "\r\n" + "\r\n");
                    }

                }
            }catch

            {
                stringBuilder = new StringBuilder();
                stringBuilder.Append("刷新失败,请检查网络连接");
                return;
            }
            Console.WriteLine("刷新供货预警提醒花费时间：{0}毫秒", (DateTime.Now - before).TotalMilliseconds);
        }

        public void RemindStock()
        {
            DateTime before = DateTime.Now;
            WMSEntities wmsEntities = new WMSEntities();
            SupplyView[] supplyView = null;
            try
            {
                supplyView = (from u in wmsEntities.SupplyView
                              where u.ProjectID ==this.projectID 
                              &&u.WarehouseID ==this.warehouseID 
                              &&u.IsHistory ==0
                              select u).ToArray();

                if(supplyView ==null)
                {
                    return;
                }
                for (int i = 0; i < supplyView.Length; i++)
                {
                    decimal amount = 0;
                    string ComponentName = supplyView[i].ComponentName;
                    string SupplierName = supplyView[i].SupplierName;
                    string SupplyNo = supplyView[i].No;
                    decimal SaftyStock;
                    StockInfoView[] stockInfo = null;
                    if (supplyView[i].SafetyStock == null)
                    {
                        continue;
                    }
                    int suppluID = supplyView[i].ID;
                    stockInfo = (from kn in wmsEntities.StockInfoView
                                 where kn.SupplyID == suppluID &&
                                 kn.WarehouseID ==this.warehouseID &&
                                 kn.ProjectID ==this.projectID 
                                 select kn).ToArray();
                    if (stockInfo == null)
                    {
                        continue;
                    }
                    SaftyStock = Convert.ToDecimal(supplyView[i].SafetyStock);

                    for (int j = 0; j < stockInfo.Length; j++)

                    {
                        amount = amount+Convert.ToDecimal(stockInfo[j].OverflowAreaAmount) + Convert.ToDecimal(stockInfo[j].ShipmentAreaAmount);
                    }

                    if (amount < SaftyStock)
                    {
                        stringBuilder.Append(SupplierName + "  " + ComponentName + "  " + SupplyNo + "  " + "库存量" + "  " + amount + "  " + "已小于安全库存" + "   " + SaftyStock + "\r\n" + "\r\n");

                    }
                }
            }
            catch
            {
                stringBuilder = new StringBuilder();
                stringBuilder.Append("刷新失败2,请检查网络连接");
                return;
            }
            Console.WriteLine("刷新库存提醒花费时间：{0}毫秒",(DateTime.Now - before).TotalMilliseconds);
        }

        public void TextDeliver()
        {
            this.textBox1.Text = stringBuilder.ToString();
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
            //double a = 0.35;
            //this.Top = 0;//25
            //this.Left = (int)(a * Screen.PrimaryScreen.Bounds.Width);          
            this.Left = 3;
            this.Top = (int)(0.7 * Screen.PrimaryScreen.Bounds.Height);
            this.Width = (int)(0.35 * Screen.PrimaryScreen.Bounds.Width );
            this.Height = (int)(0.25 * Screen.PrimaryScreen.Bounds.Height);//75
            Thread thread = new Thread(loadData);
            thread.Start();
            this.textBox1.Text = "数据加载中...";           
            //this.TransparencyKey = System.Drawing.Color.Black;//设置黑的是透明色
            //this.BackColor = System.Drawing.Color.Black;//把窗口的背景色设置为黑
            this.ShowInTaskbar = false;///使窗体不显示在任务栏
            this.stringBuilder = new StringBuilder();
            //Stopwatch sw = new Stopwatch();
            //ClearTextBox();
            //RefreshID();
            ////sw.Start();
            //RemindSupply();
            ////sw.Stop();
            //RemindStock();
            ////TimeSpan ts2 = sw.Elapsed;
            ////MessageBox.Show("Stopwatch总共花费" + Convert.ToString(ts2.TotalMilliseconds) + "ms.");
            //TextDeliver();
        }
        public void loadData()
        {
            //加载数据
            ClearTextBox();
            RefreshID();
            RemindSupply();
            RemindStock();
            if (textBox1.InvokeRequired)
            {
                textBox1.BeginInvoke(new Action(() => textBox1.Text = stringBuilder.ToString() ));
                if (this.textBox1.Text == "刷新失败,请检查网络连接")
                {
                    this.textBox1.ForeColor = Color.Red;

                }
                else
                {
                    this.textBox1.ForeColor = Color.Black;
                }
            }
        }
        public void RefreshDate()
        {
            Thread thread = new Thread(loadData);
            thread.Start();
            this.textBox1.Text = "数据加载中...";
        }

        private void FormSupplyRemind_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
            this.button.Visible = true;           
        }

 
    }

    
}
