using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WMS.UI.FormSupplier
{
    public partial class FormSupplierAdd : Form
    {
        public FormSupplierAdd()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           if (textBox2.Text== string.Empty)
            {
                MessageBox.Show("供应商名称不能为空！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
           
            //添加
           
            WMS.DataAccess.Supplier objsupplier = new WMS.DataAccess.Supplier();
            {
                
                objsupplier.Name = textBox2.Text;
               
                objsupplier.ContractNo = textBox4.Text;
                objsupplier.StartDate = Convert.ToDateTime(textBox5.Text);
                objsupplier.EndDate = Convert.ToDateTime(textBox6.Text);
           
                objsupplier.FullName = textBox9.Text;
                objsupplier.TaxpayerNumber = textBox10.Text;
                objsupplier.Address = textBox11.Text;
                objsupplier.Tel = textBox12.Text;
                objsupplier.BankName = textBox13.Text;
                objsupplier.BankAccount = textBox14.Text;
                objsupplier.BankNo = textBox15.Text;
                objsupplier.ZipCode = textBox16.Text;
                objsupplier.RecipientName = textBox17.Text;
            }


            WMS.DataAccess.WMSEntities wms1 = new WMS.DataAccess.WMSEntities();
            wms1.Supplier.Add(objsupplier);
            wms1.SaveChanges();
            MessageBox.Show("添加用户成功");
            this.Close();


        }







        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormSupplierAdd_Load(object sender, EventArgs e)
        {

        }
    }
}
