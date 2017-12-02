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
        public FormBaseSupplier()
        {
            InitializeComponent();

        }


        private void InitSupplier ()
        {
            string[] visibleColumnNames = (from kn in SupplierInfoMetaData.KeyNames
                                           where kn.Visible == true
                                           select kn.Name).ToArray();

            //初始化
            this.toolStripComboBoxSelect.Items.Add("无");
            this.toolStripComboBoxSelect.Items.AddRange(visibleColumnNames);
            this.toolStripComboBoxSelect.SelectedIndex = 0;


            //初始化表格
            var worksheet = this.reoGridControlUser.Worksheets[0];
            worksheet.SelectionMode = WorksheetSelectionMode.Row;
            for (int i = 0; i < SupplierInfoMetaData.KeyNames.Length; i++)
            {
                worksheet.ColumnHeaders[i].Text = SupplierInfoMetaData.KeyNames[i].Name;
                worksheet.ColumnHeaders[i].IsVisible = SupplierInfoMetaData.KeyNames[i].Visible;
            }
            worksheet.Columns = SupplierInfoMetaData.KeyNames.Length;//限制表的长度
           
        }




    

        private void FormBaseSupplier_Load(object sender, EventArgs e)
        {
            InitSupplier();
            this.Search();
         
        }

        private void toolStripButtonAdd_Click(object sender, EventArgs e)
        {
            FormSupplier.FormSupplierAdd  ad = new FormSupplier.FormSupplierAdd(); ;
            ad.ShowDialog();
            this.Search();
        }



        private void toolStripButtonSelect_Click(object sender, EventArgs e)
        {

            this.Search();




        }

        private void Search()

        {
            string key = null;
            string value = null;


            if (this.toolStripComboBoxSelect.SelectedIndex != 0)
            {
                key = (from kn in StockInfoMetaData.KeyNames
                       where kn.Name == this.toolStripComboBoxSelect.SelectedItem.ToString()
                       select kn.Key).First();
                value = this.toolStripComboBoxSelect.Text;
            }
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
                        object[] columns = Utilities.GetValuesByPropertieNames(curComponent, (from kn in SupplierInfoMetaData.KeyNames select kn.Key).ToArray());
                        for (int j = 0; j < SupplierInfoMetaData.KeyNames.Length; j++)
                        {
                            worksheet[i, j] = columns[j] == null ? "" : columns[j].ToString();
                        }
                    }
                }));
            })).Start();
        }







         

        private void toolStripButtonAlter_Click(object sender, EventArgs e)
        {
            var worksheet = this.reoGridControlUser.Worksheets[0];
            try
            {
                if (worksheet.SelectionRange.Rows != 1)
                {
                    throw new Exception();
                }
                int supplierID  = int.Parse(worksheet[worksheet.SelectionRange.Row, 0].ToString());
                var a1= new FormSupplierModify(supplierID);
                a1.SetModifyFinishedCallback(() =>
                {
                    this.Search();
                });
                a1.Show();
            }
            catch
            {
                MessageBox.Show("请选择一项进行修改", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

        }

     
     



        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
           // ReoGridControl grid = this.reoGridControlUser;
           // var worksheet1 = grid.Worksheets[0];

           // worksheet1.SelectionMode = WorksheetSelectionMode.Row;//选中行操作

           // string str = worksheet1.SelectionRange.ToRelativeAddress();//返回选中地址串
           // int start = 2, length = 1;
           // //MessageBox.Show(str.Substring(start - 1, length));//返回行数
           // int i = Convert.ToInt32(str.Substring(start - 1, length));//变为int型

           // string usrname = worksheet1[i - 1, 0].ToString();

           // WMSEntities wms = new WMSEntities();
           //Supplier supplier = (from s in wms.Supplier
           //                   where s.Name== usrname
           //                   select s).First<Supplier>();
           // wms.Supplier.Remove(supplier);//删除

           // wms.SaveChanges();
           // worksheet1.Reset();
           // //showreoGridControl();//显示所有数据
           // this.Search();//显示所有数据
        }

        private void reoGridControlUser_Click(object sender, EventArgs e)
        {

        }
    }

}
