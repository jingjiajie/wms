using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WMS.DataAccess;

namespace WMS.UI.FormSupplier
{
    public partial class FormSupplierAlter : Form
    {
        public FormSupplierAlter()
        {
            InitializeComponent();
        }

        public FormSupplierAlter(string a2,string a4 ,string a5,string a6,string a7,string a8,string a9,string a10,string a11,string a12,string a13,string a14,
            string a15,string a16,string a17)
        {
            InitializeComponent();
            
            textBox2.Text = a2;         
            textBox4.Text = a4;
            textBox5.Text = a5;
            textBox6.Text = a6;
            textBox7.Text = a7;
            textBox8.Text = a8;
            textBox9.Text = a9;
            textBox10.Text = a10;
            textBox11.Text = a11;
            textBox12.Text = a12;
            textBox13.Text = a13;
            textBox14.Text = a14;
            textBox15.Text = a15;
            textBox16.Text = a16;
            textBox17.Text = a17;
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
         
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            //修改
            WMSEntities wms = new WMSEntities();
            Supplier objsupplier = (from s in wms.Supplier
                                    where s.Name == textBox2.Text
                                    select s).First<Supplier>();

            objsupplier.Name = textBox2.Text;
            objsupplier.ContractNo = textBox4.Text;
            objsupplier.StartDate = Convert.ToDateTime(textBox5.Text);
            objsupplier.EndDate = Convert.ToDateTime(textBox6.Text);
            objsupplier.InvoiceDate = Convert.ToDateTime(textBox7.Text);
            objsupplier.BalanceDate = Convert.ToDateTime(textBox8.Text);
            objsupplier.FullName = textBox9.Text;
            objsupplier.TaxpayerNumber = textBox10.Text;
            objsupplier.Address = textBox11.Text;
            objsupplier.Tel = textBox12.Text;
            objsupplier.BankName = textBox13.Text;
            objsupplier.BankAccount = textBox14.Text;
            objsupplier.BankNo = textBox15.Text;
            objsupplier.ZipCode = textBox16.Text;
            objsupplier.RecipientName = textBox17.Text;



            wms.SaveChanges();
            MessageBox.Show("修改成功");
            this.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormSupplierAlter_Load(object sender, EventArgs e)
        {

        }
    }
}
