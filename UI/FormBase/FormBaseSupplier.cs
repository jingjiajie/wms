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
using System.Threading;

namespace WMS.UI
{
    public partial class FormBaseSupplier : Form
    {
        class KeyName
        {
            public string Key;
            public string Name;
            public bool Visible = true;
        }

        private KeyName[] keyNames = {
            new KeyName(){Key="ID",Name="ID",Visible=false},
            new KeyName(){Key="Name",Name="供货商名称"},
            new KeyName(){Key="WarehouseID",Name="仓库ID"},
            new KeyName(){Key="ContractNo",Name="合同编码"},
            new KeyName(){Key="StartDate",Name="起始有效日期"},
            new KeyName(){Key="EndDate",Name="结束有效日期"},
            new KeyName(){Key="InvoiceDate",Name="开票日期"},
            new KeyName(){Key="BalanceDate",Name="结算日期"},
            new KeyName(){Key="FullName",Name="供货商全称"},
            new KeyName(){Key="TaxpayerNumber",Name="纳税人识别号"},
            new KeyName(){Key="Address",Name="地址"},
            new KeyName(){Key="Tel",Name="电话"},
            new KeyName(){Key="BankName",Name="开户行"},
            new KeyName(){Key="BankAccount",Name="帐号"},
            new KeyName(){Key="BankNo",Name="开户行行号"},
            new KeyName(){Key="ZipCode",Name="邮编"},
            new KeyName(){Key="RecipientName",Name="收件人"},

         };


        private void InitSupplier ()
        {
            string[] visibleColumnNames = (from kn in this.keyNames
                                           where kn.Visible == true
                                           select kn.Name).ToArray();

            //初始化
            this.toolStripComboBoxSelect.Items.Add("无");
            this.toolStripComboBoxSelect.Items.AddRange(visibleColumnNames);
            this.toolStripComboBoxSelect.SelectedIndex = 0;


            //初始化表格
            var worksheet = this.reoGridControlUser.Worksheets[0];
            worksheet.SelectionMode = WorksheetSelectionMode.Row;
            for (int i = 0; i < this.keyNames.Length; i++)
            {
                worksheet.ColumnHeaders[i].Text = this.keyNames[i].Name;
                worksheet.ColumnHeaders[i].IsVisible = this.keyNames[i].Visible;
            }
            worksheet.Columns = this.keyNames.Length;//限制表的长度
            Console.WriteLine("表格行数：" + this.keyNames.Length);
        }




        public FormBaseSupplier()
        {
            InitializeComponent();
            
        }

        private void FormBaseSupplier_Load(object sender, EventArgs e)
        {
            InitSupplier();
            this.Search(null, null);
         
        }

        private void toolStripButtonAdd_Click(object sender, EventArgs e)
        {
            FormSupplier.FormSupplierAdd  ad = new FormSupplier.FormSupplierAdd(); ;
            ad.ShowDialog();
            this.Search(null, null);
        }

        private void toolStripButtonSelect_Click(object sender, EventArgs e)
        {

            if (this.toolStripComboBoxSelect.SelectedIndex == 0)
            {
                this.Search(null, null);
                return;
            }
            else
            {
                string key = (from kn in this.keyNames
                              where kn.Name == this.toolStripComboBoxSelect.SelectedItem.ToString()
                              select kn.Key).First();
                string value = this.toolStripTextBoxSelect.Text;
                this.Search(key, value);
                return;
            }


        }

        private void Search(string key, string value)

        {
            this.labelStatus.Text = "正在搜索中...";
            var worksheet = this.reoGridControlUser.Worksheets[0];
            worksheet[0, 0] = "加载中...";
            new Thread(new ThreadStart(() =>
            {
                var wmsEntities = new WMSEntities();

                DataAccess.Supplier[] Supplier = null;
                if (key == null || value == null) //查询条件为null则查询全部内容
                {
                    Supplier = wmsEntities.Database.SqlQuery<DataAccess.Supplier>("SELECT * FROM Supplier").ToArray();
                }
                else
                {
                    if (Double.TryParse(value, out double tmp) == false) //不是数字则加上单引号
                    {
                        value = "'" + value + "'";
                    }
                    try
                    {
                        Supplier = wmsEntities.Database.SqlQuery<DataAccess.Supplier>(String.Format("SELECT * FROM Supplier WHERE {0} = {1}", key, value)).ToArray();
                    }
                    catch
                    {
                        MessageBox.Show("查询的值不合法，请输入正确的值！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                this.reoGridControlUser.Invoke(new Action(() =>
                {
                    this.labelStatus.Text = "搜索完成";
                    worksheet.DeleteRangeData(RangePosition.EntireRange);
                    if (Supplier.Length == 0)
                    {
                        worksheet[1, 1] = "没有查询到符合条件的记录";
                    }
                    for (int i = 0; i < Supplier.Length; i++)
                    {
                        DataAccess.Supplier curComponent = Supplier[i];
                        object[] columns = Utilities.GetValuesByPropertieNames(curComponent, (from kn in this.keyNames select kn.Key).ToArray());
                        for (int j = 0; j < keyNames.Length; j++)
                        {
                            worksheet[i, j] = columns[j] == null ? "" : columns[j].ToString();
                        }
                    }
                }));
            })).Start();
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
            this.Search(null, null);
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
            //showreoGridControl();//显示所有数据
            this.Search(null, null);//显示所有数据
        }

        private void reoGridControlUser_Click(object sender, EventArgs e)
        {

        }
    }

}
