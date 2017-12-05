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
using System.Data.SqlClient;
using unvell.ReoGrid.DataFormat;

namespace WMS.UI
{
    public partial class FormBaseSupplier : Form
    {
        private WMSEntities wmsEntities = new WMSEntities();
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
            var a1  = new FormSupplierModify();
            a1.SetMode(FormMode.ADD);

            a1.SetAddFinishedCallback(() =>
            {
                this.Search();
            });
            a1.Show();  
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
                key = (from kn in SupplierInfoMetaData.KeyNames
                               where kn.Name == this.toolStripComboBoxSelect.SelectedItem.ToString()
                               select kn.Key).First();
                value = this.toolStripTextBoxSelect.Text;
            }
            this.labelStatus.Text = "正在搜索中...";
            var worksheet = this.reoGridControlUser.Worksheets[0];
            worksheet[0, 0] = "加载中...";
            new Thread(new ThreadStart(() =>
            {
                

                DataAccess.Supplier[] Supplier = null;
                if (key == null || value == null) //查询条件为null则查询全部内容
                {
                    Supplier = wmsEntities.Database.SqlQuery<DataAccess.Supplier>("SELECT * FROM Supplier").ToArray();
                    
                }
                else
                {
                    if (decimal.TryParse(value, out decimal result) == false)
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
                        for (int j = 0; j < worksheet.Columns; j++)
                        {
                            
                            worksheet[i, j] = columns[j] == null ? "" : columns[j].ToString();                   
                            worksheet.SetRangeDataFormat(RangePosition.EntireRange,CellDataFormatFlag.Text,null);
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
            var worksheet = this.reoGridControlUser.Worksheets[0];
            List<int> deleteIDs = new List<int>();
            for (int i = 0; i < worksheet.SelectionRange.Rows; i++)
            {
                try
                {
                    int curID = int.Parse(worksheet[i + worksheet.SelectionRange.Row, 0].ToString());
                    deleteIDs.Add(curID);
                }
                catch
                {
                    continue;
                }
            }
            if (deleteIDs.Count == 0)
            {
                MessageBox.Show("请选择您要删除的记录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("您真的要删除这些记录吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }
            this.labelStatus.Text = "正在删除...";
            new Thread(new ThreadStart(() =>
            {
                foreach (int id in deleteIDs)
                {
                    this.wmsEntities.Database.ExecuteSqlCommand("DELETE FROM Supplier WHERE ID = @supplierID", new SqlParameter("supplierID", id));
                }
                this.wmsEntities.SaveChanges();
                this.Invoke(new Action(this.Search));
            })).Start();


        }
        

        private void reoGridControlUser_Click(object sender, EventArgs e)
        {

        }
    }

}
