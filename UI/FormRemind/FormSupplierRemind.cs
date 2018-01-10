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
            this.textBoxContractRemind .Font = new Font("宋体", 12, FontStyle.Bold);

            Supplier Supplier = (from u in this.wmsEntities.Supplier
                                 where u.ID == supplierid
                                 select u).Single();
            if (Convert.ToString(Supplier.EndingTime) != string.Empty)
            {
                this.contract_enddate = Convert .ToDateTime ( Supplier.EndingTime);
                days = (DateTime.Now - contract_enddate).Days  ;

                if((-days)<10)
                {

                    this.textBoxContractRemind.Text = "您的合同还有"+(-days)+"天就到期了";
                    this.textBoxContractRemind.Font = new Font("宋体", 12, FontStyle.Bold);

                }




                if (Supplier.EndingTime < DateTime.Now)
                {
                    this.textBoxContractRemind.Text = " 您的合同已经到截止日期" ;
                    this.textBoxContractRemind.Font = new Font("宋体", 12, FontStyle.Bold);
                    
                }


            }



        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
