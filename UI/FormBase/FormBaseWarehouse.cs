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
    public partial class FormBaseWarehouse : Form
    {
        public FormBaseWarehouse()
        {
            InitializeComponent();
        }

        private void base_Warehouse_Load(object sender, EventArgs e)
        {
            ShowReoGridControl();//显示所有数据
        }

        private void toolStripButtonAdd_Click(object sender, EventArgs e)
        {
            FormBase.FormBaseWarehouseAdd wd = new FormBase.FormBaseWarehouseAdd();
            wd.ShowDialog();
            Fresh();
        }

        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            ReoGridControl grid = this.reoGridControlWarehouse;
            var worksheet1 = grid.Worksheets[0];

            worksheet1.SelectionMode = WorksheetSelectionMode.Row;//选中行操作

            string str = worksheet1.SelectionRange.ToRelativeAddress();//返回选中地址串
            int start = 2, length = 1;
            int i = Convert.ToInt32(str.Substring(start - 1, length));//变为int型
            string warename = worksheet1[i - 1, 1].ToString();

            WMSEntities wms = new WMSEntities();
            Warehouse ware = (from s in wms.Warehouse
                              where s.Name == warename
                              select s).First<Warehouse>();
            wms.Warehouse.Remove(ware);//删除
            wms.SaveChanges();
            worksheet1.Reset();
            ShowReoGridControl();//显示所有数据
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
    }
}
