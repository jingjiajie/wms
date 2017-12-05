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

namespace WMS.UI
{
    public partial class FormBaseWarehouse : Form
    {
        private WMSEntities wmsEntities = new WMSEntities();
        public FormBaseWarehouse()
        {
            InitializeComponent();
        }

        private void InitComponents()
        {
            string[] visibleColumnNames = (from kn in FormBase.BaseWarehouseMetaData.KeyNames
                                           where kn.Visible == true
                                           select kn.Name).ToArray();

            //初始化
            this.toolStripComboBoxSelect.Items.Add("无");
            this.toolStripComboBoxSelect.Items.AddRange(visibleColumnNames);
            this.toolStripComboBoxSelect.SelectedIndex = 0;


            //初始化表格
            var worksheet = this.reoGridControlWarehouse.Worksheets[0];
            worksheet.SelectionMode = WorksheetSelectionMode.Row;
            for (int i = 0; i < FormBase.BaseWarehouseMetaData.KeyNames.Length; i++)
            {
                worksheet.ColumnHeaders[i].Text = FormBase.BaseWarehouseMetaData.KeyNames[i].Name;
                worksheet.ColumnHeaders[i].IsVisible = FormBase.BaseWarehouseMetaData.KeyNames[i].Visible;
            }
            worksheet.Columns = FormBase.BaseWarehouseMetaData.KeyNames.Length;//限制表的长度
        }


        private void base_Warehouse_Load(object sender, EventArgs e)
        {
            InitComponents();
            this.Search();
        }

        private void Search()
        {
            string key = null;
            string value = null;

            if (this.toolStripComboBoxSelect.SelectedIndex != 0)
            {
                key = (from kn in FormBase.BaseWarehouseMetaData.KeyNames
                       where kn.Name == this.toolStripComboBoxSelect.SelectedItem.ToString()
                       select kn.Key).First();
                value = this.toolStripTextBoxSelect.Text;
            }

            this.labelStatus.Text = "正在搜索中...";
            var worksheet = this.reoGridControlWarehouse.Worksheets[0];
            worksheet[0, 0] = "加载中...";
            new Thread(new ThreadStart(() =>
            {
                var wmsEntities = new WMSEntities();

                Warehouse[] Warehouses = null;
                //Project[] Warehouses = null;
                if (key == null || value == null) //查询条件为null则查询全部内容
                {
                    Warehouses = wmsEntities.Database.SqlQuery<DataAccess.Warehouse>("SELECT * FROM Warehouse").ToArray();
                }
                else
                {
                    if (Double.TryParse(value, out double tmp) == false) //不是数字则加上单引号
                    {
                        value = "'" + value + "'";
                    }
                    try
                    {
                        Warehouses = wmsEntities.Database.SqlQuery<DataAccess.Warehouse>(String.Format("SELECT * FROM Warehouse WHERE {0} = {1}", key, value)).ToArray();
                    }
                    catch
                    {
                        MessageBox.Show("查询的值不合法，请输入正确的值！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                this.reoGridControlWarehouse.Invoke(new Action(() =>
                {
                    this.labelStatus.Text = "搜索完成";
                    worksheet.DeleteRangeData(RangePosition.EntireRange);
                    if (Warehouses.Length == 0)
                    {
                        worksheet[1, 1] = "没有查询到符合条件的记录";
                    }
                    for (int i = 0; i < Warehouses.Length; i++)
                    {
                        DataAccess.Warehouse curProject = Warehouses[i];
                        object[] columns = Utilities.GetValuesByPropertieNames(curProject, (from kn in FormBase.BaseWarehouseMetaData.KeyNames select kn.Key).ToArray());
                        for (int j = 0; j < worksheet.Columns; j++)
                        {
                            worksheet[i, j] = columns[j] == null ? "" : columns[j].ToString();
                        }
                    }
                }));
            })).Start();
        }


        private void toolStripButtonAdd_Click(object sender, EventArgs e)
        {
            var a1 = new FormBase.FormBaseWarehouseModify();
            a1.SetMode(FormMode.ADD);

            a1.SetAddFinishedCallback(() =>
            {
                this.Search();
            });
            a1.Show();
        }

        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            var worksheet = this.reoGridControlWarehouse.Worksheets[0];
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
                    this.wmsEntities.Database.ExecuteSqlCommand("DELETE FROM Warehouse WHERE ID = @warehouseID", new SqlParameter("warehouseID", id));
                }
                this.wmsEntities.SaveChanges();
                this.Invoke(new Action(this.Search));
            })).Start();
        }

        private void Fresh()//刷新表格
        {
            ReoGridControl grid = this.reoGridControlWarehouse;
            var worksheet1 = grid.Worksheets[0];
            worksheet1.Reset();
            ShowReoGridControl();//显示所有数据
        }

        private void ShowReoGridControl()//表格显示
        {
            ReoGridControl grid = this.reoGridControlWarehouse;
            var worksheet1 = grid.Worksheets[0];

            worksheet1.ColumnHeaders[0].Text = "仓库ID";
            worksheet1.ColumnHeaders[1].Text = "仓库名";

            worksheet1.SelectionMode = WorksheetSelectionMode.Row;//选中行操作

            WMSEntities wms = new WMSEntities();
            var allWare = (from s in wms.Warehouse select s).ToArray();
            for (int i = 0; i < allWare.Count(); i++)
            {
                Warehouse ware = allWare[i];
                worksheet1[i, 0] = ware.ID;
                worksheet1[i, 1] = ware.Name;
            }
        }

        private void toolStripButtonAlter_Click(object sender, EventArgs e)
        {
            var worksheet = this.reoGridControlWarehouse.Worksheets[0];
            try
            {
                if (worksheet.SelectionRange.Rows != 1)
                {
                    throw new Exception();
                }
                int warehouseID = int.Parse(worksheet[worksheet.SelectionRange.Row, 0].ToString());
                var a1 = new FormBase.FormBaseWarehouseModify(warehouseID);
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

        private void toolStripButtonSelect_Click(object sender, EventArgs e)
        {
            this.Search();
        }
    }
}
