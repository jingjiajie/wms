using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using unvell.ReoGrid;
using WMS.DataAccess;

namespace WMS.UI
{
    public partial class FormBaseSupplier : Form
    {
        public FormBaseSupplier()
        {
            InitializeComponent();
        }

        private void FormBaseSupplier_Load(object sender, EventArgs e)
        {
            showreoGridControl();//显示所有数据
           
            toolStripComboBoxSelect.Items.Add("供应商名称");
            toolStripComboBoxSelect.Items.Add("合同编码");
            toolStripComboBoxSelect.Items.Add("起始有效日期");
            toolStripComboBoxSelect.Items.Add("结束有效日期");
            toolStripComboBoxSelect.Items.Add("开票日期");
            toolStripComboBoxSelect.Items.Add("结算日期");
            toolStripComboBoxSelect.Items.Add("供货商全称");
            toolStripComboBoxSelect.Items.Add("纳税人识别号");
            toolStripComboBoxSelect.Items.Add("地址");
            toolStripComboBoxSelect.Items.Add("电话");
            toolStripComboBoxSelect.Items.Add("开户行");
            toolStripComboBoxSelect.Items.Add("账号");
            toolStripComboBoxSelect.Items.Add("开户行行号");
            toolStripComboBoxSelect.Items.Add("邮编");
            toolStripComboBoxSelect.Items.Add("收件人");

        }

        private void toolStripButtonAdd_Click(object sender, EventArgs e)
        {
            FormSupplier.FormSupplierAdd  ad = new FormSupplier.FormSupplierAdd(); ;
            ad.ShowDialog();
            fresh();
        }

        private void toolStripButtonSelect_Click(object sender, EventArgs e)
        {
           
            if (toolStripComboBoxSelect.Text == "供应商名称" && toolStripTextBoxSelect.Text != string.Empty)
            {
                searchnameReoGridControl();
            }
            
            if (toolStripComboBoxSelect.Text == "合同编码" && toolStripTextBoxSelect.Text != string.Empty)
            {
                searchcontractidReoGridControl();
            }
            if (toolStripComboBoxSelect.Text == "起始有效日期" && toolStripTextBoxSelect.Text != string.Empty)
            {
                searchstartdateReoGridControl();
            }
            if (toolStripComboBoxSelect.Text == "结束有效日期" && toolStripTextBoxSelect.Text != string.Empty)
            {
                searchenddateReoGridControl();
            }
            if (toolStripComboBoxSelect.Text == "开票日期" && toolStripTextBoxSelect.Text != string.Empty)
            {
                searchinvoicedateReoGridControl();
            }
            if (toolStripComboBoxSelect.Text == "结算日期" && toolStripTextBoxSelect.Text != string.Empty)
            {
                searchbalancedateReoGridControl();

            }
            if (toolStripComboBoxSelect.Text == "供货商全称" && toolStripTextBoxSelect.Text != string.Empty)
            {
                searchfullnameReoGridControl();

            }
            if (toolStripComboBoxSelect.Text == "纳税人识别号" && toolStripTextBoxSelect.Text != string.Empty)
            {
                searchtaxnumReoGridControl();


            }
            if (toolStripComboBoxSelect.Text == "地址" && toolStripTextBoxSelect.Text != string.Empty)
            {
                searchadrressReoGridControl();

            }
            if (toolStripComboBoxSelect.Text == "电话" && toolStripTextBoxSelect.Text != string.Empty)
            {
                searchtelReoGridControl();

            }
            if (toolStripComboBoxSelect.Text == "开户行" && toolStripTextBoxSelect.Text != string.Empty)
            {
                searchbanknameReoGridControl();

            }
            if (toolStripComboBoxSelect.Text == "账号" && toolStripTextBoxSelect.Text != string.Empty)
            {
                searchbankaccReoGridControl();

            }
            if (toolStripComboBoxSelect.Text == "开户行行号" && toolStripTextBoxSelect.Text != string.Empty)
            {
                searchbanknoReoGridControl();

            }
            if (toolStripComboBoxSelect.Text == "邮编" && toolStripTextBoxSelect.Text != string.Empty)
            {
                searchzipcodeReoGridControl();

            }
            if (toolStripComboBoxSelect.Text == "收件人" && toolStripTextBoxSelect.Text != string.Empty)
            {
                searchrecnameReoGridControl();

            }
        }
       


        private void searchnameReoGridControl()
        {
            ReoGridControl grid = this.reoGridControlUser;
            var worksheet1 = grid.Worksheets[0];
            WMSEntities wms = new WMSEntities();

            var name = (from s in wms.Supplier
                      where s.Name == toolStripTextBoxSelect.Text
                      select s).ToArray();

            for (int i = 0; i < name.Count(); i++)
            {
                Supplier supplierb = new Supplier();
               
                worksheet1[i, 0] = supplierb.Name;              
                worksheet1[i, 1] = supplierb.ContractNo;
                worksheet1[i, 2] = supplierb.StartDate;
                worksheet1[i, 3] = supplierb.EndDate;
                worksheet1[i, 4] = supplierb.InvoiceDate;
                worksheet1[i, 5] = supplierb.BalanceDate;
                worksheet1[i, 6] = supplierb.FullName;
                worksheet1[i, 7] = supplierb.TaxpayerNumber;
                worksheet1[i, 8] = supplierb.Address;
                worksheet1[i, 9] = supplierb.Tel;
                worksheet1[i, 10] = supplierb.BankName;
                worksheet1[i, 11] = supplierb.BankAccount;
                worksheet1[i, 12] = supplierb.BankNo;
                worksheet1[i, 13] = supplierb.ZipCode;
                worksheet1[i, 14] = supplierb.RecipientName;
            }
        }


        private void searchcontractidReoGridControl()
        {

            ReoGridControl grid = this.reoGridControlUser;
            var worksheet1 = grid.Worksheets[0];
            WMSEntities wms = new WMSEntities();

            var wid = (from s in wms.Supplier
                       where s.WarehouseID == Convert.ToInt32(toolStripTextBoxSelect.Text)
                       select s).ToArray();

            for (int i = 0; i < wid.Count(); i++)
            {
                Supplier supplierb = new Supplier();

                worksheet1[i, 0] = supplierb.Name;
                worksheet1[i, 1] = supplierb.ContractNo;
                worksheet1[i, 2] = supplierb.StartDate;
                worksheet1[i, 3] = supplierb.EndDate;
                worksheet1[i, 4] = supplierb.InvoiceDate;
                worksheet1[i, 5] = supplierb.BalanceDate;
                worksheet1[i, 6] = supplierb.FullName;
                worksheet1[i, 7] = supplierb.TaxpayerNumber;
                worksheet1[i, 8] = supplierb.Address;
                worksheet1[i, 9] = supplierb.Tel;
                worksheet1[i, 10] = supplierb.BankName;
                worksheet1[i, 11] = supplierb.BankAccount;
                worksheet1[i, 12] = supplierb.BankNo;
                worksheet1[i, 13] = supplierb.ZipCode;
                worksheet1[i, 14] = supplierb.RecipientName;
            }


        }



        private void searchstartdateReoGridControl()
        {
            ReoGridControl grid = this.reoGridControlUser;
            var worksheet1 = grid.Worksheets[0];
            WMSEntities wms = new WMSEntities();

            var sd = (from s in wms.Supplier
                        where s.StartDate == Convert.ToDateTime(toolStripTextBoxSelect.Text)
                        select s).ToArray();

            for (int i = 0; i < sd.Count(); i++)
            {
                Supplier supplierb = new Supplier();

                worksheet1[i, 0] = supplierb.Name;
                worksheet1[i, 1] = supplierb.ContractNo;
                worksheet1[i, 2] = supplierb.StartDate;
                worksheet1[i, 3] = supplierb.EndDate;
                worksheet1[i, 4] = supplierb.InvoiceDate;
                worksheet1[i, 5] = supplierb.BalanceDate;
                worksheet1[i, 6] = supplierb.FullName;
                worksheet1[i, 7] = supplierb.TaxpayerNumber;
                worksheet1[i, 8] = supplierb.Address;
                worksheet1[i, 9] = supplierb.Tel;
                worksheet1[i, 10] = supplierb.BankName;
                worksheet1[i, 11] = supplierb.BankAccount;
                worksheet1[i, 12] = supplierb.BankNo;
                worksheet1[i, 13] = supplierb.ZipCode;
                worksheet1[i, 14] = supplierb.RecipientName;
            }
        }


        private void searchenddateReoGridControl()
        {
            ReoGridControl grid = this.reoGridControlUser;
            var worksheet1 = grid.Worksheets[0];
            WMSEntities wms = new WMSEntities();

            var ed = (from s in wms.Supplier
                      where s.EndDate == Convert.ToDateTime(toolStripTextBoxSelect.Text)
                      select s).ToArray();

            for (int i = 0; i < ed.Count(); i++)
            {
                Supplier supplierb = new Supplier();
                worksheet1[i, 0] = supplierb.Name;
                worksheet1[i, 1] = supplierb.ContractNo;
                worksheet1[i, 2] = supplierb.StartDate;
                worksheet1[i, 3] = supplierb.EndDate;
                worksheet1[i, 4] = supplierb.InvoiceDate;
                worksheet1[i, 5] = supplierb.BalanceDate;
                worksheet1[i, 6] = supplierb.FullName;
                worksheet1[i, 7] = supplierb.TaxpayerNumber;
                worksheet1[i, 8] = supplierb.Address;
                worksheet1[i, 9] = supplierb.Tel;
                worksheet1[i, 10] = supplierb.BankName;
                worksheet1[i, 11] = supplierb.BankAccount;
                worksheet1[i, 12] = supplierb.BankNo;
                worksheet1[i, 13] = supplierb.ZipCode;
                worksheet1[i, 14] = supplierb.RecipientName;
            }
        }


        private void searchinvoicedateReoGridControl()
        {
            ReoGridControl grid = this.reoGridControlUser;
            var worksheet1 = grid.Worksheets[0];
            WMSEntities wms = new WMSEntities();

            var ind = (from s in wms.Supplier
                       where s.InvoiceDate == Convert.ToDateTime(toolStripTextBoxSelect.Text)
                       select s).ToArray();

            for (int i = 0; i < ind.Count(); i++)
            {
                Supplier supplierb = new Supplier();

                worksheet1[i, 0] = supplierb.Name;
                worksheet1[i, 1] = supplierb.ContractNo;
                worksheet1[i, 2] = supplierb.StartDate;
                worksheet1[i, 3] = supplierb.EndDate;
                worksheet1[i, 4] = supplierb.InvoiceDate;
                worksheet1[i, 5] = supplierb.BalanceDate;
                worksheet1[i, 6] = supplierb.FullName;
                worksheet1[i, 7] = supplierb.TaxpayerNumber;
                worksheet1[i, 8] = supplierb.Address;
                worksheet1[i, 9] = supplierb.Tel;
                worksheet1[i, 10] = supplierb.BankName;
                worksheet1[i, 11] = supplierb.BankAccount;
                worksheet1[i, 12] = supplierb.BankNo;
                worksheet1[i, 13] = supplierb.ZipCode;
                worksheet1[i, 14] = supplierb.RecipientName;
            }
        }

        private void searchbalancedateReoGridControl()
        {
            ReoGridControl grid = this.reoGridControlUser;
            var worksheet1 = grid.Worksheets[0];
            WMSEntities wms = new WMSEntities();

            var bad = (from s in wms.Supplier
                       where s.BalanceDate == Convert.ToDateTime(toolStripTextBoxSelect.Text)
                       select s).ToArray();

            for (int i = 0; i < bad.Count(); i++)
            {
                Supplier supplierb = new Supplier();
                worksheet1[i, 0] = supplierb.Name;
                worksheet1[i, 1] = supplierb.ContractNo;
                worksheet1[i, 2] = supplierb.StartDate;
                worksheet1[i, 3] = supplierb.EndDate;
                worksheet1[i, 4] = supplierb.InvoiceDate;
                worksheet1[i, 5] = supplierb.BalanceDate;
                worksheet1[i, 6] = supplierb.FullName;
                worksheet1[i, 7] = supplierb.TaxpayerNumber;
                worksheet1[i, 8] = supplierb.Address;
                worksheet1[i, 9] = supplierb.Tel;
                worksheet1[i, 10] = supplierb.BankName;
                worksheet1[i, 11] = supplierb.BankAccount;
                worksheet1[i, 12] = supplierb.BankNo;
                worksheet1[i, 13] = supplierb.ZipCode;
                worksheet1[i, 14] = supplierb.RecipientName;
            }
        }

        private void searchfullnameReoGridControl()
        {
            ReoGridControl grid = this.reoGridControlUser;
            var worksheet1 = grid.Worksheets[0];
            WMSEntities wms = new WMSEntities();

            var fname = (from s in wms.Supplier
                        where s.FullName  == toolStripTextBoxSelect.Text
                        select s).ToArray();

            for (int i = 0; i < fname.Count(); i++)
            {
                Supplier supplierb = new Supplier();
                worksheet1[i, 0] = supplierb.Name;
                worksheet1[i, 1] = supplierb.ContractNo;
                worksheet1[i, 2] = supplierb.StartDate;
                worksheet1[i, 3] = supplierb.EndDate;
                worksheet1[i, 4] = supplierb.InvoiceDate;
                worksheet1[i, 5] = supplierb.BalanceDate;
                worksheet1[i, 6] = supplierb.FullName;
                worksheet1[i, 7] = supplierb.TaxpayerNumber;
                worksheet1[i, 8] = supplierb.Address;
                worksheet1[i, 9] = supplierb.Tel;
                worksheet1[i, 10] = supplierb.BankName;
                worksheet1[i, 11] = supplierb.BankAccount;
                worksheet1[i, 12] = supplierb.BankNo;
                worksheet1[i, 13] = supplierb.ZipCode;
                worksheet1[i, 14] = supplierb.RecipientName;
            }
        }

        private void searchtaxnumReoGridControl()
        {
            ReoGridControl grid = this.reoGridControlUser;
            var worksheet1 = grid.Worksheets[0];
            WMSEntities wms = new WMSEntities();

            var taxnum = (from s in wms.Supplier
                         where s.TaxpayerNumber == toolStripTextBoxSelect.Text
                         select s).ToArray();

            for (int i = 0; i < taxnum.Count(); i++)
            {
                Supplier supplierb = new Supplier();
                worksheet1[i, 0] = supplierb.Name;
                worksheet1[i, 1] = supplierb.ContractNo;
                worksheet1[i, 2] = supplierb.StartDate;
                worksheet1[i, 3] = supplierb.EndDate;
                worksheet1[i, 4] = supplierb.InvoiceDate;
                worksheet1[i, 5] = supplierb.BalanceDate;
                worksheet1[i, 6] = supplierb.FullName;
                worksheet1[i, 7] = supplierb.TaxpayerNumber;
                worksheet1[i, 8] = supplierb.Address;
                worksheet1[i, 9] = supplierb.Tel;
                worksheet1[i, 10] = supplierb.BankName;
                worksheet1[i, 11] = supplierb.BankAccount;
                worksheet1[i, 12] = supplierb.BankNo;
                worksheet1[i, 13] = supplierb.ZipCode;
                worksheet1[i, 14] = supplierb.RecipientName;
            }
        }

        private void searchadrressReoGridControl()
        {
            ReoGridControl grid = this.reoGridControlUser;
            var worksheet1 = grid.Worksheets[0];
            WMSEntities wms = new WMSEntities();

            var taxnum = (from s in wms.Supplier
                          where s.Address == toolStripTextBoxSelect.Text
                          select s).ToArray();

            for (int i = 0; i < taxnum.Count(); i++)
            {
                Supplier supplierb = new Supplier();
                worksheet1[i, 0] = supplierb.Name;
                worksheet1[i, 1] = supplierb.ContractNo;
                worksheet1[i, 2] = supplierb.StartDate;
                worksheet1[i, 3] = supplierb.EndDate;
                worksheet1[i, 4] = supplierb.InvoiceDate;
                worksheet1[i, 5] = supplierb.BalanceDate;
                worksheet1[i, 6] = supplierb.FullName;
                worksheet1[i, 7] = supplierb.TaxpayerNumber;
                worksheet1[i, 8] = supplierb.Address;
                worksheet1[i, 9] = supplierb.Tel;
                worksheet1[i, 10] = supplierb.BankName;
                worksheet1[i, 11] = supplierb.BankAccount;
                worksheet1[i, 12] = supplierb.BankNo;
                worksheet1[i, 13] = supplierb.ZipCode;
                worksheet1[i, 14] = supplierb.RecipientName;
            }
        }

        private void searchtelReoGridControl()
        {
            ReoGridControl grid = this.reoGridControlUser;
            var worksheet1 = grid.Worksheets[0];
            WMSEntities wms = new WMSEntities();

            var taxnum = (from s in wms.Supplier
                          where s.Tel  == toolStripTextBoxSelect.Text
                          select s).ToArray();

            for (int i = 0; i < taxnum.Count(); i++)
            {
                Supplier supplierb = new Supplier();
                worksheet1[i, 0] = supplierb.Name;
                worksheet1[i, 1] = supplierb.ContractNo;
                worksheet1[i, 2] = supplierb.StartDate;
                worksheet1[i, 3] = supplierb.EndDate;
                worksheet1[i, 4] = supplierb.InvoiceDate;
                worksheet1[i, 5] = supplierb.BalanceDate;
                worksheet1[i, 6] = supplierb.FullName;
                worksheet1[i, 7] = supplierb.TaxpayerNumber;
                worksheet1[i, 8] = supplierb.Address;
                worksheet1[i, 9] = supplierb.Tel;
                worksheet1[i, 10] = supplierb.BankName;
                worksheet1[i, 11] = supplierb.BankAccount;
                worksheet1[i, 12] = supplierb.BankNo;
                worksheet1[i, 13] = supplierb.ZipCode;
                worksheet1[i, 14] = supplierb.RecipientName;
            }
        }

        private void searchbanknameReoGridControl()
        {
            ReoGridControl grid = this.reoGridControlUser;
            var worksheet1 = grid.Worksheets[0];
            WMSEntities wms = new WMSEntities();

            var taxnum = (from s in wms.Supplier
                          where s.BankName == toolStripTextBoxSelect.Text
                          select s).ToArray();

            for (int i = 0; i < taxnum.Count(); i++)
            {
                Supplier supplierb = new Supplier();
                worksheet1[i, 0] = supplierb.Name;
                worksheet1[i, 1] = supplierb.ContractNo;
                worksheet1[i, 2] = supplierb.StartDate;
                worksheet1[i, 3] = supplierb.EndDate;
                worksheet1[i, 4] = supplierb.InvoiceDate;
                worksheet1[i, 5] = supplierb.BalanceDate;
                worksheet1[i, 6] = supplierb.FullName;
                worksheet1[i, 7] = supplierb.TaxpayerNumber;
                worksheet1[i, 8] = supplierb.Address;
                worksheet1[i, 9] = supplierb.Tel;
                worksheet1[i, 10] = supplierb.BankName;
                worksheet1[i, 11] = supplierb.BankAccount;
                worksheet1[i, 12] = supplierb.BankNo;
                worksheet1[i, 13] = supplierb.ZipCode;
                worksheet1[i, 14] = supplierb.RecipientName;
            }
        }
        private void searchbankaccReoGridControl()
        {
            ReoGridControl grid = this.reoGridControlUser;
            var worksheet1 = grid.Worksheets[0];
            WMSEntities wms = new WMSEntities();

            var taxnum = (from s in wms.Supplier
                          where s.BankAccount == toolStripTextBoxSelect.Text
                          select s).ToArray();

            for (int i = 0; i < taxnum.Count(); i++)
            {
                Supplier supplierb = new Supplier();
                worksheet1[i, 0] = supplierb.Name;
                worksheet1[i, 1] = supplierb.ContractNo;
                worksheet1[i, 2] = supplierb.StartDate;
                worksheet1[i, 3] = supplierb.EndDate;
                worksheet1[i, 4] = supplierb.InvoiceDate;
                worksheet1[i, 5] = supplierb.BalanceDate;
                worksheet1[i, 6] = supplierb.FullName;
                worksheet1[i, 7] = supplierb.TaxpayerNumber;
                worksheet1[i, 8] = supplierb.Address;
                worksheet1[i, 9] = supplierb.Tel;
                worksheet1[i, 10] = supplierb.BankName;
                worksheet1[i, 11] = supplierb.BankAccount;
                worksheet1[i, 12] = supplierb.BankNo;
                worksheet1[i, 13] = supplierb.ZipCode;
                worksheet1[i, 14] = supplierb.RecipientName;
            }
        }
        private void searchbanknoReoGridControl()
        {
            ReoGridControl grid = this.reoGridControlUser;
            var worksheet1 = grid.Worksheets[0];
            WMSEntities wms = new WMSEntities();

            var taxnum = (from s in wms.Supplier
                          where s.BankNo == toolStripTextBoxSelect.Text
                          select s).ToArray();

            for (int i = 0; i < taxnum.Count(); i++)
            {
                Supplier supplierb = new Supplier();
                worksheet1[i, 0] = supplierb.Name;
                worksheet1[i, 1] = supplierb.ContractNo;
                worksheet1[i, 2] = supplierb.StartDate;
                worksheet1[i, 3] = supplierb.EndDate;
                worksheet1[i, 4] = supplierb.InvoiceDate;
                worksheet1[i, 5] = supplierb.BalanceDate;
                worksheet1[i, 6] = supplierb.FullName;
                worksheet1[i, 7] = supplierb.TaxpayerNumber;
                worksheet1[i, 8] = supplierb.Address;
                worksheet1[i, 9] = supplierb.Tel;
                worksheet1[i, 10] = supplierb.BankName;
                worksheet1[i, 11] = supplierb.BankAccount;
                worksheet1[i, 12] = supplierb.BankNo;
                worksheet1[i, 13] = supplierb.ZipCode;
                worksheet1[i, 14] = supplierb.RecipientName;
            }
        }

        private void searchzipcodeReoGridControl()
        {
            ReoGridControl grid = this.reoGridControlUser;
            var worksheet1 = grid.Worksheets[0];
            WMSEntities wms = new WMSEntities();

            var taxnum = (from s in wms.Supplier
                          where s.ZipCode == toolStripTextBoxSelect.Text
                          select s).ToArray();

            for (int i = 0; i < taxnum.Count(); i++)
            {
                Supplier supplierb = new Supplier();
                worksheet1[i, 0] = supplierb.Name;
                worksheet1[i, 1] = supplierb.ContractNo;
                worksheet1[i, 2] = supplierb.StartDate;
                worksheet1[i, 3] = supplierb.EndDate;
                worksheet1[i, 4] = supplierb.InvoiceDate;
                worksheet1[i, 5] = supplierb.BalanceDate;
                worksheet1[i, 6] = supplierb.FullName;
                worksheet1[i, 7] = supplierb.TaxpayerNumber;
                worksheet1[i, 8] = supplierb.Address;
                worksheet1[i, 9] = supplierb.Tel;
                worksheet1[i, 10] = supplierb.BankName;
                worksheet1[i, 11] = supplierb.BankAccount;
                worksheet1[i, 12] = supplierb.BankNo;
                worksheet1[i, 13] = supplierb.ZipCode;
                worksheet1[i, 14] = supplierb.RecipientName;
            }
        }
        private void searchrecnameReoGridControl()
        {
            ReoGridControl grid = this.reoGridControlUser;
            var worksheet1 = grid.Worksheets[0];
            WMSEntities wms = new WMSEntities();

            var taxnum = (from s in wms.Supplier
                          where s.RecipientName == toolStripTextBoxSelect.Text
                          select s).ToArray();

            for (int i = 0; i < taxnum.Count(); i++)
            {
                Supplier supplierb = new Supplier();
                worksheet1[i, 0] = supplierb.Name;
                worksheet1[i, 1] = supplierb.ContractNo;
                worksheet1[i, 2] = supplierb.StartDate;
                worksheet1[i, 3] = supplierb.EndDate;
                worksheet1[i, 4] = supplierb.InvoiceDate;
                worksheet1[i, 5] = supplierb.BalanceDate;
                worksheet1[i, 6] = supplierb.FullName;
                worksheet1[i, 7] = supplierb.TaxpayerNumber;
                worksheet1[i, 8] = supplierb.Address;
                worksheet1[i, 9] = supplierb.Tel;
                worksheet1[i, 10] = supplierb.BankName;
                worksheet1[i, 11] = supplierb.BankAccount;
                worksheet1[i, 12] = supplierb.BankNo;
                worksheet1[i, 13] = supplierb.ZipCode;
                worksheet1[i, 14] = supplierb.RecipientName;
            }
        }

        





        

        private void toolStripButtonAlter_Click(object sender, EventArgs e)
        {
            ReoGridControl grid = this.reoGridControlUser;
            var worksheet1 = grid.Worksheets[0];

            worksheet1.SelectionMode = WorksheetSelectionMode.Row;//选中行操作

            string str = worksheet1.SelectionRange.ToRelativeAddress();//返回选中地址串
            int start = 2, length = 1;
            //MessageBox.Show(str.Substring(start - 1, length));//返回行数
            //MessageBox.Show(a+"+"+b);
            int i = Convert.ToInt32(str.Substring(start - 1, length));//变为int型

            string suppliername= worksheet1[i - 1, 0].ToString();

            WMSEntities wms = new WMSEntities();
            Supplier nameSupplier = (from s in wms.Supplier
                                     where s.Name == suppliername
                              select s).First<Supplier>();


            
            string a2 = nameSupplier.Name;           
            string a4 = nameSupplier.ContractNo;
            
            string a5 =Convert .ToString( nameSupplier.StartDate);
            string a6 = Convert.ToString(nameSupplier.EndDate);
            string a7 = Convert.ToString(nameSupplier.InvoiceDate);
            string a8 = Convert.ToString(nameSupplier.BalanceDate);
            string a9 = nameSupplier.FullName;
            string a10 = nameSupplier.TaxpayerNumber;
            string a11 = nameSupplier.Address;
            string a12 = nameSupplier.Tel;
            string a13 = nameSupplier.BankName;
            string a14 = nameSupplier.BankAccount;
            string a15 = nameSupplier.BankNo;
            string a16 = nameSupplier.ZipCode;
            string a17 = nameSupplier.RecipientName;
            FormSupplier.FormSupplierAlter  Al=new FormSupplier.FormSupplierAlter (a2, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13, a14, a15, a16, a17);
            
            Al.ShowDialog();
            fresh();
        }

        private void showreoGridControl()
        {
            ReoGridControl grid = this.reoGridControlUser;
            var worksheet1 = grid.Worksheets[0];

            worksheet1.ColumnHeaders[0].Text = "供货商名称";
            worksheet1.ColumnHeaders[1].Text = "合同编码";
            worksheet1.ColumnHeaders[2].Text = "起始有效日期";
            worksheet1.ColumnHeaders[3].Text = "结束有效日期";
            worksheet1.ColumnHeaders[4].Text = "开票日期";
            worksheet1.ColumnHeaders[5].Text = "结算日期";
            worksheet1.ColumnHeaders[6].Text = "供货商全称";
            worksheet1.ColumnHeaders[7].Text = "纳税人识别号";
            worksheet1.ColumnHeaders[8].Text = "地址";
            worksheet1.ColumnHeaders[9].Text = "电话";
            worksheet1.ColumnHeaders[10].Text = "开户行";
            worksheet1.ColumnHeaders[11].Text = "帐号";
            worksheet1.ColumnHeaders[12].Text = "开户行行号";
            worksheet1.ColumnHeaders[13].Text = "邮编";
            worksheet1.ColumnHeaders[14].Text = "收件人";
            

            WMSEntities wms = new WMSEntities();
            var allSupplier = (from s in wms.Supplier select s).ToArray();
            for (int i = 0; i < allSupplier.Count(); i++)
            {
                Supplier supplier = allSupplier[i];

                worksheet1[i, 0] =supplier.Name  ;
                worksheet1[i, 1] = supplier.ContractNo ;
                worksheet1[i, 2] = supplier.StartDate;
                worksheet1[i, 3] = supplier.EndDate;
                worksheet1[i, 4] = supplier.InvoiceDate;
                worksheet1[i, 5] = supplier.BalanceDate;
                worksheet1[i, 6] = supplier.FullName;
                worksheet1[i, 7] = supplier.TaxpayerNumber;
                worksheet1[i, 8] = supplier.Address;
                worksheet1[i, 9] = supplier.Tel;
                worksheet1[i, 10] = supplier.BankName;
                worksheet1[i, 11] =supplier.BankAccount;
                worksheet1[i, 12] = supplier.BankNo;
                worksheet1[i, 13] = supplier.ZipCode;
                worksheet1[i, 14] = supplier.RecipientName;
                worksheet1[i, 15] = supplier.ID;



            }
        }//显示

        private void fresh()//刷新表格
        {
            ReoGridControl grid = this.reoGridControlUser;
            var worksheet1 = grid.Worksheets[0];
            worksheet1.Reset();
            showreoGridControl();//显示所有数据
        }



        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            ReoGridControl grid = this.reoGridControlUser;
            var worksheet1 = grid.Worksheets[0];

            worksheet1.SelectionMode = WorksheetSelectionMode.Row;//选中行操作

            string str = worksheet1.SelectionRange.ToRelativeAddress();//返回选中地址串
            int start = 2, length = 1;
            //MessageBox.Show(str.Substring(start - 1, length));//返回行数
            int i = Convert.ToInt32(str.Substring(start - 1, length));//变为int型

            string usrname = worksheet1[i - 1, 0].ToString();

            WMSEntities wms = new WMSEntities();
           Supplier supplier = (from s in wms.Supplier
                              where s.Name== usrname
                              select s).First<Supplier>();
            wms.Supplier.Remove(supplier);//删除

            wms.SaveChanges();
            worksheet1.Reset();
            showreoGridControl();//显示所有数据
        }
    }

}
