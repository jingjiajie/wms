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
    public partial class FormBaseComponent : Form
    {
        public FormBaseComponent()
        {
            InitializeComponent();
        }

        private void FormBaseComponent_Load(object sender, EventArgs e)
        {
            showreoGridControl();//显示所有数据
            //toolStripComboBoxSelect.Items.Add("零件ID");
            //toolStripComboBoxSelect.Items.Add("仓库ID");
            toolStripComboBoxSelect.Items.Add("供货商ID");
            toolStripComboBoxSelect.Items.Add("容器号");
            toolStripComboBoxSelect.Items.Add("工厂");
            toolStripComboBoxSelect.Items.Add("工位");
            toolStripComboBoxSelect.Items.Add("零件代号");
            toolStripComboBoxSelect.Items.Add("零件名称");
            toolStripComboBoxSelect.Items.Add("A系列/B系列供应商");
            toolStripComboBoxSelect.Items.Add("机型区分");
            toolStripComboBoxSelect.Items.Add("尺寸（大件/小件）");
            toolStripComboBoxSelect.Items.Add("分类");
            toolStripComboBoxSelect.Items.Add("分组负责人");
            toolStripComboBoxSelect.Items.Add("单台用量");
            toolStripComboBoxSelect.Items.Add("物流服务费1-50000台套");
            toolStripComboBoxSelect.Items.Add("物流服务费50000台套以上");
            toolStripComboBoxSelect.Items.Add("1天库存要求");
            toolStripComboBoxSelect.Items.Add("3天库存要求");
            toolStripComboBoxSelect.Items.Add("5天库存要求");
            toolStripComboBoxSelect.Items.Add("10天库存要求");

        }
        private void showreoGridControl()
        {
            ReoGridControl grid = this.reoGridControlComponen;
            var worksheet1 = grid.Worksheets[0];


            worksheet1.ColumnHeaders[0].Text = "供货商ID";
            worksheet1.ColumnHeaders[1].Text = "容器号";
            worksheet1.ColumnHeaders[2].Text = "工厂";
            worksheet1.ColumnHeaders[3].Text = "工位";
            worksheet1.ColumnHeaders[4].Text = "零件代号";
            worksheet1.ColumnHeaders[5].Text = "零件名称";
            worksheet1.ColumnHeaders[6].Text = "A系列/B系列供应商";
            worksheet1.ColumnHeaders[7].Text = "机型区分";
            worksheet1.ColumnHeaders[8].Text = "尺寸（大件/小件）";
            worksheet1.ColumnHeaders[9].Text = "分类";
            worksheet1.ColumnHeaders[10].Text = "分组负责人";
            worksheet1.ColumnHeaders[11].Text = "单台用量";
            worksheet1.ColumnHeaders[12].Text = "物流服务费1-50000台套";
            worksheet1.ColumnHeaders[13].Text = "物流服务费50000台套以上";
            worksheet1.ColumnHeaders[14].Text = "1天库存要求";
            worksheet1.ColumnHeaders[15].Text = "3天库存要求";
            worksheet1.ColumnHeaders[16].Text = "5天库存要求";
            worksheet1.ColumnHeaders[17].Text = "10天库存要求";
            worksheet1.ColumnHeaders[18].Text = "零件ID";
            worksheet1.ColumnHeaders[19].Text = "仓库ID";
            //设定表头信息
            worksheet1.SelectionMode = WorksheetSelectionMode.Row;//选中行操作

            WMSEntities wms = new WMSEntities();
            var allcomponen = (from s in wms.Component select s).ToArray();
            for (int i = 0; i < allcomponen.Count(); i++)
            {
                DataAccess.Component component = allcomponen[i];
                worksheet1[i, 0] = component.SupplierID;//第一列显示
                worksheet1[i, 1] = component.ContainerNo;
                worksheet1[i, 2] = component.Factroy;
                worksheet1[i, 3] = component.WorkPosition;
                worksheet1[i, 4] = component.No;
                worksheet1[i, 5] = component.Name;
                worksheet1[i, 6] = component.SupplierType;
                worksheet1[i, 7] = component.Type;
                worksheet1[i, 8] = component.Size;
                worksheet1[i, 9] = component.Category;
                worksheet1[i, 10] = component.GroupPrincipal;
                worksheet1[i, 11] = component.SingleCarUsageAmount;
                worksheet1[i, 12] = component.ChargeBelow50000;
                worksheet1[i, 13] = component.ChargeAbove50000;
                worksheet1[i, 14] = component.InventoryRequirement1Day;
                worksheet1[i, 15] = component.InventoryRequirement3Day;
                worksheet1[i, 16] = component.InventoryRequirement5Day;
                worksheet1[i, 17] = component.InventoryRequirement10Day;
                worksheet1[i, 18] = component.ID;
                worksheet1[i, 19] = component.WarehouseID;

            }
        }//表格显示

        private void fresh()//刷新表格
        {
            ReoGridControl grid = this.reoGridControlComponen;
            var worksheet1 = grid.Worksheets[0];
            worksheet1.Reset();
            showreoGridControl();//显示所有数据
        }

        private void reoGridControlUser_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButtonSelect_Click(object sender, EventArgs e)
        {
            showreoGridControl();//显示所有数据
            //if (toolStripComboBoxSelect.Text == "零件ID" && toolStripTextBoxSelect.Text != string.Empty)
            //{
            //    searchIDReoGridControl();
            //}
            //if (toolStripComboBoxSelect.Text == "仓库ID" && toolStripTextBoxSelect.Text != string.Empty)
            //{
            //    searchWarehouseIDReoGridControl();
            //}
            if (toolStripComboBoxSelect.Text == "供货商ID" && toolStripTextBoxSelect.Text != string.Empty)
            {
                searchSupplierIDReoGridControl();
            }
            if (toolStripComboBoxSelect.Text == "容器号" && toolStripTextBoxSelect.Text != string.Empty)
            {
                searchContainerNoReoGridControl();
            }
            if (toolStripComboBoxSelect.Text == "工厂" && toolStripTextBoxSelect.Text != string.Empty)
            {
                searchFactroyReoGridControl();
            }
            if (toolStripComboBoxSelect.Text == "工位" && toolStripTextBoxSelect.Text != string.Empty)
            {
                searchWorkPositionReoGridControl();
            }
            if (toolStripComboBoxSelect.Text == "零件代号" && toolStripTextBoxSelect.Text != string.Empty)
            {
                searchNoReoGridControl();
            }
            if (toolStripComboBoxSelect.Text == "零件名称" && toolStripTextBoxSelect.Text != string.Empty)
            {
                searchName1ReoGridControl();
            }
            if (toolStripComboBoxSelect.Text == "A系列/B系列供应商" && toolStripTextBoxSelect.Text != string.Empty)
            {
                searchSupplierTypeReoGridControl();
            }
            if (toolStripComboBoxSelect.Text == "机型区分" && toolStripTextBoxSelect.Text != string.Empty)
            {
                searchTypeReoGridControl();
            }
            if (toolStripComboBoxSelect.Text == "尺寸（大件/小件）" && toolStripTextBoxSelect.Text != string.Empty)
            {
                searchSizeReoGridControl();
            }
            if (toolStripComboBoxSelect.Text == "分类" && toolStripTextBoxSelect.Text != string.Empty)
            {
                searchCategoryReoGridControl();
            }
            if (toolStripComboBoxSelect.Text == "分组负责人" && toolStripTextBoxSelect.Text != string.Empty)
            {
                searchGroupPrincipalReoGridControl();
            }
            if (toolStripComboBoxSelect.Text == "单台用量" && toolStripTextBoxSelect.Text != string.Empty)
            {
                searchSingleCarUsageAmountReoGridControl();
            }
            if (toolStripComboBoxSelect.Text == "物流服务费1-50000台套" && toolStripTextBoxSelect.Text != string.Empty)
            {
                searchChargeBelow50000ReoGridControl();
            }
            if (toolStripComboBoxSelect.Text == "物流服务费50000台套以上" && toolStripTextBoxSelect.Text != string.Empty)
            {
                searchChargeAbove50000ReoGridControl();
            }
            if (toolStripComboBoxSelect.Text == "1天库存要求" && toolStripTextBoxSelect.Text != string.Empty)
            {
                searchInventoryRequirement1DayReoGridControl();
            }
            if (toolStripComboBoxSelect.Text == "3天库存要求" && toolStripTextBoxSelect.Text != string.Empty)
            {
                searchInventoryRequirement3DayReoGridControl();
            }
            if (toolStripComboBoxSelect.Text == "5天库存要求" && toolStripTextBoxSelect.Text != string.Empty)
            {
                searchInventoryRequirement5DayReoGridControl();
            }
            if (toolStripComboBoxSelect.Text == "10天库存要求" && toolStripTextBoxSelect.Text != string.Empty)
            {
                searchInventoryRequirement10DayReoGridControl();
            }

        }
        private void searchIDReoGridControl()
        {
            ReoGridControl grid = this.reoGridControlComponen;
            var worksheet1 = grid.Worksheets[0];
            worksheet1.Reset();

            WMSEntities wms = new WMSEntities();

            var nameComponent = (from s in wms.Component
                                 where s.ID == Convert.ToInt32(toolStripTextBoxSelect.Text)
                                 select s).ToArray();

            for (int i = 0; i < nameComponent.Count(); i++)
            {
                DataAccess.Component Componentb = nameComponent[i];                
                worksheet1[i, 0] = Componentb.SupplierID;
                worksheet1[i, 1] = Componentb.ContainerNo;
                worksheet1[i, 2] = Componentb.Factroy;
                worksheet1[i, 3] = Componentb.WorkPosition;
                worksheet1[i, 4] = Componentb.No;
                worksheet1[i, 5] = Componentb.Name;
                worksheet1[i, 6] = Componentb.SupplierType;
                worksheet1[i, 7] = Componentb.Type;
                worksheet1[i, 8] = Componentb.Size;
                worksheet1[i, 9] = Componentb.Category;
                worksheet1[i, 10] = Componentb.GroupPrincipal;
                worksheet1[i, 11] = Componentb.SingleCarUsageAmount;
                worksheet1[i, 12] = Componentb.ChargeBelow50000;
                worksheet1[i, 13] = Componentb.ChargeAbove50000;
                worksheet1[i, 14] = Componentb.InventoryRequirement1Day;
                worksheet1[i, 15] = Componentb.InventoryRequirement3Day;
                worksheet1[i, 16] = Componentb.InventoryRequirement5Day;
                worksheet1[i, 17] = Componentb.InventoryRequirement10Day;
                worksheet1[i, 18] = Componentb.ID;
                worksheet1[i, 19] = Componentb.WarehouseID;
            }//查找零件ID
        }
        private void searchWarehouseIDReoGridControl()
        {
            ReoGridControl grid = this.reoGridControlComponen;
            var worksheet1 = grid.Worksheets[0];
            worksheet1.Reset();

            WMSEntities wms = new WMSEntities();

            var nameComponent = (from s in wms.Component
                                 where s.WarehouseID == Convert.ToInt32(toolStripTextBoxSelect.Text)
                                 select s).ToArray();

            for (int i = 0; i < nameComponent.Count(); i++)
            {
                DataAccess.Component Componentb = nameComponent[i];
                worksheet1[i, 0] = Componentb.SupplierID;
                worksheet1[i, 1] = Componentb.ContainerNo;
                worksheet1[i, 2] = Componentb.Factroy;
                worksheet1[i, 3] = Componentb.WorkPosition;
                worksheet1[i, 4] = Componentb.No;
                worksheet1[i, 5] = Componentb.Name;
                worksheet1[i, 6] = Componentb.SupplierType;
                worksheet1[i, 7] = Componentb.Type;
                worksheet1[i, 8] = Componentb.Size;
                worksheet1[i, 9] = Componentb.Category;
                worksheet1[i, 10] = Componentb.GroupPrincipal;
                worksheet1[i, 11] = Componentb.SingleCarUsageAmount;
                worksheet1[i, 12] = Componentb.ChargeBelow50000;
                worksheet1[i, 13] = Componentb.ChargeAbove50000;
                worksheet1[i, 14] = Componentb.InventoryRequirement1Day;
                worksheet1[i, 15] = Componentb.InventoryRequirement3Day;
                worksheet1[i, 16] = Componentb.InventoryRequirement5Day;
                worksheet1[i, 17] = Componentb.InventoryRequirement10Day;
                worksheet1[i, 18] = Componentb.ID;
                worksheet1[i, 19] = Componentb.WarehouseID;

            }
        }//查找仓库ID
        private void searchSupplierIDReoGridControl()
        {
            ReoGridControl grid = this.reoGridControlComponen;
            var worksheet1 = grid.Worksheets[0];
            worksheet1.Reset();

            WMSEntities wms = new WMSEntities();

            var nameComponent = (from s in wms.Component
                                 where s.SupplierID == Convert.ToInt32(toolStripTextBoxSelect.Text)
                                 select s).ToArray();

            for (int i = 0; i < nameComponent.Count(); i++)
            {
                DataAccess.Component Componentb = nameComponent[i];
                worksheet1[i, 0] = Componentb.SupplierID;
                worksheet1[i, 1] = Componentb.ContainerNo;
                worksheet1[i, 2] = Componentb.Factroy;
                worksheet1[i, 3] = Componentb.WorkPosition;
                worksheet1[i, 4] = Componentb.No;
                worksheet1[i, 5] = Componentb.Name;
                worksheet1[i, 6] = Componentb.SupplierType;
                worksheet1[i, 7] = Componentb.Type;
                worksheet1[i, 8] = Componentb.Size;
                worksheet1[i, 9] = Componentb.Category;
                worksheet1[i, 10] = Componentb.GroupPrincipal;
                worksheet1[i, 11] = Componentb.SingleCarUsageAmount;
                worksheet1[i, 12] = Componentb.ChargeBelow50000;
                worksheet1[i, 13] = Componentb.ChargeAbove50000;
                worksheet1[i, 14] = Componentb.InventoryRequirement1Day;
                worksheet1[i, 15] = Componentb.InventoryRequirement3Day;
                worksheet1[i, 16] = Componentb.InventoryRequirement5Day;
                worksheet1[i, 17] = Componentb.InventoryRequirement10Day;
                worksheet1[i, 18] = Componentb.ID;
                worksheet1[i, 19] = Componentb.WarehouseID;

            }
        }
        private void searchContainerNoReoGridControl()
        {
            ReoGridControl grid = this.reoGridControlComponen;
            var worksheet1 = grid.Worksheets[0];
            worksheet1.Reset();

            WMSEntities wms = new WMSEntities();

            var nameComponent = (from s in wms.Component
                                 where s.ContainerNo == toolStripTextBoxSelect.Text
                                 select s).ToArray();

            for (int i = 0; i < nameComponent.Count(); i++)
            {
                DataAccess.Component Componentb = nameComponent[i];
                worksheet1[i, 0] = Componentb.SupplierID;
                worksheet1[i, 1] = Componentb.ContainerNo;
                worksheet1[i, 2] = Componentb.Factroy;
                worksheet1[i, 3] = Componentb.WorkPosition;
                worksheet1[i, 4] = Componentb.No;
                worksheet1[i, 5] = Componentb.Name;
                worksheet1[i, 6] = Componentb.SupplierType;
                worksheet1[i, 7] = Componentb.Type;
                worksheet1[i, 8] = Componentb.Size;
                worksheet1[i, 9] = Componentb.Category;
                worksheet1[i, 10] = Componentb.GroupPrincipal;
                worksheet1[i, 11] = Componentb.SingleCarUsageAmount;
                worksheet1[i, 12] = Componentb.ChargeBelow50000;
                worksheet1[i, 13] = Componentb.ChargeAbove50000;
                worksheet1[i, 14] = Componentb.InventoryRequirement1Day;
                worksheet1[i, 15] = Componentb.InventoryRequirement3Day;
                worksheet1[i, 16] = Componentb.InventoryRequirement5Day;
                worksheet1[i, 17] = Componentb.InventoryRequirement10Day;
                worksheet1[i, 18] = Componentb.ID;
                worksheet1[i, 19] = Componentb.WarehouseID;

            }
        }
        private void searchFactroyReoGridControl()
        {
            ReoGridControl grid = this.reoGridControlComponen;
            var worksheet1 = grid.Worksheets[0];
            worksheet1.Reset();

            WMSEntities wms = new WMSEntities();

            var nameComponent = (from s in wms.Component
                                 where s.Factroy == toolStripTextBoxSelect.Text
                                 select s).ToArray();

            for (int i = 0; i < nameComponent.Count(); i++)
            {
                DataAccess.Component Componentb = nameComponent[i];
                worksheet1[i, 0] = Componentb.SupplierID;
                worksheet1[i, 1] = Componentb.ContainerNo;
                worksheet1[i, 2] = Componentb.Factroy;
                worksheet1[i, 3] = Componentb.WorkPosition;
                worksheet1[i, 4] = Componentb.No;
                worksheet1[i, 5] = Componentb.Name;
                worksheet1[i, 6] = Componentb.SupplierType;
                worksheet1[i, 7] = Componentb.Type;
                worksheet1[i, 8] = Componentb.Size;
                worksheet1[i, 9] = Componentb.Category;
                worksheet1[i, 10] = Componentb.GroupPrincipal;
                worksheet1[i, 11] = Componentb.SingleCarUsageAmount;
                worksheet1[i, 12] = Componentb.ChargeBelow50000;
                worksheet1[i, 13] = Componentb.ChargeAbove50000;
                worksheet1[i, 14] = Componentb.InventoryRequirement1Day;
                worksheet1[i, 15] = Componentb.InventoryRequirement3Day;
                worksheet1[i, 16] = Componentb.InventoryRequirement5Day;
                worksheet1[i, 17] = Componentb.InventoryRequirement10Day;
                worksheet1[i, 18] = Componentb.ID;
                worksheet1[i, 19] = Componentb.WarehouseID;

            }
        }
        private void searchWorkPositionReoGridControl()
        {
            ReoGridControl grid = this.reoGridControlComponen;
            var worksheet1 = grid.Worksheets[0];
            worksheet1.Reset();

            WMSEntities wms = new WMSEntities();

            var nameComponent = (from s in wms.Component
                                 where s.WorkPosition == toolStripTextBoxSelect.Text
                                 select s).ToArray();

            for (int i = 0; i < nameComponent.Count(); i++)
            {
                DataAccess.Component Componentb = nameComponent[i];
                worksheet1[i, 0] = Componentb.SupplierID;
                worksheet1[i, 1] = Componentb.ContainerNo;
                worksheet1[i, 2] = Componentb.Factroy;
                worksheet1[i, 3] = Componentb.WorkPosition;
                worksheet1[i, 4] = Componentb.No;
                worksheet1[i, 5] = Componentb.Name;
                worksheet1[i, 6] = Componentb.SupplierType;
                worksheet1[i, 7] = Componentb.Type;
                worksheet1[i, 8] = Componentb.Size;
                worksheet1[i, 9] = Componentb.Category;
                worksheet1[i, 10] = Componentb.GroupPrincipal;
                worksheet1[i, 11] = Componentb.SingleCarUsageAmount;
                worksheet1[i, 12] = Componentb.ChargeBelow50000;
                worksheet1[i, 13] = Componentb.ChargeAbove50000;
                worksheet1[i, 14] = Componentb.InventoryRequirement1Day;
                worksheet1[i, 15] = Componentb.InventoryRequirement3Day;
                worksheet1[i, 16] = Componentb.InventoryRequirement5Day;
                worksheet1[i, 17] = Componentb.InventoryRequirement10Day;
                worksheet1[i, 18] = Componentb.ID;
                worksheet1[i, 19] = Componentb.WarehouseID;

            }
        }
        private void searchNoReoGridControl()
        {
            ReoGridControl grid = this.reoGridControlComponen;
            var worksheet1 = grid.Worksheets[0];
            worksheet1.Reset();

            WMSEntities wms = new WMSEntities();

            var nameComponent = (from s in wms.Component
                                 where s.No == toolStripTextBoxSelect.Text
                                 select s).ToArray();

            for (int i = 0; i < nameComponent.Count(); i++)
            {
                DataAccess.Component Componentb = nameComponent[i];
                worksheet1[i, 0] = Componentb.SupplierID;
                worksheet1[i, 1] = Componentb.ContainerNo;
                worksheet1[i, 2] = Componentb.Factroy;
                worksheet1[i, 3] = Componentb.WorkPosition;
                worksheet1[i, 4] = Componentb.No;
                worksheet1[i, 5] = Componentb.Name;
                worksheet1[i, 6] = Componentb.SupplierType;
                worksheet1[i, 7] = Componentb.Type;
                worksheet1[i, 8] = Componentb.Size;
                worksheet1[i, 9] = Componentb.Category;
                worksheet1[i, 10] = Componentb.GroupPrincipal;
                worksheet1[i, 11] = Componentb.SingleCarUsageAmount;
                worksheet1[i, 12] = Componentb.ChargeBelow50000;
                worksheet1[i, 13] = Componentb.ChargeAbove50000;
                worksheet1[i, 14] = Componentb.InventoryRequirement1Day;
                worksheet1[i, 15] = Componentb.InventoryRequirement3Day;
                worksheet1[i, 16] = Componentb.InventoryRequirement5Day;
                worksheet1[i, 17] = Componentb.InventoryRequirement10Day;
                worksheet1[i, 18] = Componentb.ID;
                worksheet1[i, 19] = Componentb.WarehouseID;

            }
        }
        private void searchName1ReoGridControl()
        {
            ReoGridControl grid = this.reoGridControlComponen;
            var worksheet1 = grid.Worksheets[0];
            worksheet1.Reset();

            WMSEntities wms = new WMSEntities();

            var nameComponent = (from s in wms.Component
                                 where s.Name == toolStripTextBoxSelect.Text
                                 select s).ToArray();

            for (int i = 0; i < nameComponent.Count(); i++)
            {
                DataAccess.Component Componentb = nameComponent[i];
                worksheet1[i, 0] = Componentb.SupplierID;
                worksheet1[i, 1] = Componentb.ContainerNo;
                worksheet1[i, 2] = Componentb.Factroy;
                worksheet1[i, 3] = Componentb.WorkPosition;
                worksheet1[i, 4] = Componentb.No;
                worksheet1[i, 5] = Componentb.Name;
                worksheet1[i, 6] = Componentb.SupplierType;
                worksheet1[i, 7] = Componentb.Type;
                worksheet1[i, 8] = Componentb.Size;
                worksheet1[i, 9] = Componentb.Category;
                worksheet1[i, 10] = Componentb.GroupPrincipal;
                worksheet1[i, 11] = Componentb.SingleCarUsageAmount;
                worksheet1[i, 12] = Componentb.ChargeBelow50000;
                worksheet1[i, 13] = Componentb.ChargeAbove50000;
                worksheet1[i, 14] = Componentb.InventoryRequirement1Day;
                worksheet1[i, 15] = Componentb.InventoryRequirement3Day;
                worksheet1[i, 16] = Componentb.InventoryRequirement5Day;
                worksheet1[i, 17] = Componentb.InventoryRequirement10Day;
                worksheet1[i, 18] = Componentb.ID;
                worksheet1[i, 19] = Componentb.WarehouseID;

            }
        }
        private void searchSupplierTypeReoGridControl()
        {
            ReoGridControl grid = this.reoGridControlComponen;
            var worksheet1 = grid.Worksheets[0];
            worksheet1.Reset();

            WMSEntities wms = new WMSEntities();

            var nameComponent = (from s in wms.Component
                                 where s.SupplierType == toolStripTextBoxSelect.Text
                                 select s).ToArray();

            for (int i = 0; i < nameComponent.Count(); i++)
            {
                DataAccess.Component Componentb = nameComponent[i];
                worksheet1[i, 0] = Componentb.SupplierID;
                worksheet1[i, 1] = Componentb.ContainerNo;
                worksheet1[i, 2] = Componentb.Factroy;
                worksheet1[i, 3] = Componentb.WorkPosition;
                worksheet1[i, 4] = Componentb.No;
                worksheet1[i, 5] = Componentb.Name;
                worksheet1[i, 6] = Componentb.SupplierType;
                worksheet1[i, 7] = Componentb.Type;
                worksheet1[i, 8] = Componentb.Size;
                worksheet1[i, 9] = Componentb.Category;
                worksheet1[i, 10] = Componentb.GroupPrincipal;
                worksheet1[i, 11] = Componentb.SingleCarUsageAmount;
                worksheet1[i, 12] = Componentb.ChargeBelow50000;
                worksheet1[i, 13] = Componentb.ChargeAbove50000;
                worksheet1[i, 14] = Componentb.InventoryRequirement1Day;
                worksheet1[i, 15] = Componentb.InventoryRequirement3Day;
                worksheet1[i, 16] = Componentb.InventoryRequirement5Day;
                worksheet1[i, 17] = Componentb.InventoryRequirement10Day;
                worksheet1[i, 18] = Componentb.ID;
                worksheet1[i, 19] = Componentb.WarehouseID;

            }
        }
        private void searchTypeReoGridControl()
        {
            ReoGridControl grid = this.reoGridControlComponen;
            var worksheet1 = grid.Worksheets[0];
            worksheet1.Reset();

            WMSEntities wms = new WMSEntities();

            var nameComponent = (from s in wms.Component
                                 where s.Type == toolStripTextBoxSelect.Text
                                 select s).ToArray();

            for (int i = 0; i < nameComponent.Count(); i++)
            {
                DataAccess.Component Componentb = nameComponent[i];
                worksheet1[i, 0] = Componentb.SupplierID;
                worksheet1[i, 1] = Componentb.ContainerNo;
                worksheet1[i, 2] = Componentb.Factroy;
                worksheet1[i, 3] = Componentb.WorkPosition;
                worksheet1[i, 4] = Componentb.No;
                worksheet1[i, 5] = Componentb.Name;
                worksheet1[i, 6] = Componentb.SupplierType;
                worksheet1[i, 7] = Componentb.Type;
                worksheet1[i, 8] = Componentb.Size;
                worksheet1[i, 9] = Componentb.Category;
                worksheet1[i, 10] = Componentb.GroupPrincipal;
                worksheet1[i, 11] = Componentb.SingleCarUsageAmount;
                worksheet1[i, 12] = Componentb.ChargeBelow50000;
                worksheet1[i, 13] = Componentb.ChargeAbove50000;
                worksheet1[i, 14] = Componentb.InventoryRequirement1Day;
                worksheet1[i, 15] = Componentb.InventoryRequirement3Day;
                worksheet1[i, 16] = Componentb.InventoryRequirement5Day;
                worksheet1[i, 17] = Componentb.InventoryRequirement10Day;
                worksheet1[i, 18] = Componentb.ID;
                worksheet1[i, 19] = Componentb.WarehouseID;

            }
        }
        private void searchSizeReoGridControl()
        {
            ReoGridControl grid = this.reoGridControlComponen;
            var worksheet1 = grid.Worksheets[0];
            worksheet1.Reset();

            WMSEntities wms = new WMSEntities();

            var nameComponent = (from s in wms.Component
                                 where s.Size == toolStripTextBoxSelect.Text
                                 select s).ToArray();

            for (int i = 0; i < nameComponent.Count(); i++)
            {
                DataAccess.Component Componentb = nameComponent[i];
                worksheet1[i, 0] = Componentb.SupplierID;
                worksheet1[i, 1] = Componentb.ContainerNo;
                worksheet1[i, 2] = Componentb.Factroy;
                worksheet1[i, 3] = Componentb.WorkPosition;
                worksheet1[i, 4] = Componentb.No;
                worksheet1[i, 5] = Componentb.Name;
                worksheet1[i, 6] = Componentb.SupplierType;
                worksheet1[i, 7] = Componentb.Type;
                worksheet1[i, 8] = Componentb.Size;
                worksheet1[i, 9] = Componentb.Category;
                worksheet1[i, 10] = Componentb.GroupPrincipal;
                worksheet1[i, 11] = Componentb.SingleCarUsageAmount;
                worksheet1[i, 12] = Componentb.ChargeBelow50000;
                worksheet1[i, 13] = Componentb.ChargeAbove50000;
                worksheet1[i, 14] = Componentb.InventoryRequirement1Day;
                worksheet1[i, 15] = Componentb.InventoryRequirement3Day;
                worksheet1[i, 16] = Componentb.InventoryRequirement5Day;
                worksheet1[i, 17] = Componentb.InventoryRequirement10Day;
                worksheet1[i, 18] = Componentb.ID;
                worksheet1[i, 19] = Componentb.WarehouseID;

            }
        }
        private void searchCategoryReoGridControl()
        {
            ReoGridControl grid = this.reoGridControlComponen;
            var worksheet1 = grid.Worksheets[0];
            worksheet1.Reset();

            WMSEntities wms = new WMSEntities();

            var nameComponent = (from s in wms.Component
                                 where s.Category == toolStripTextBoxSelect.Text
                                 select s).ToArray();

            for (int i = 0; i < nameComponent.Count(); i++)
            {
                DataAccess.Component Componentb = nameComponent[i];
                worksheet1[i, 0] = Componentb.SupplierID;
                worksheet1[i, 1] = Componentb.ContainerNo;
                worksheet1[i, 2] = Componentb.Factroy;
                worksheet1[i, 3] = Componentb.WorkPosition;
                worksheet1[i, 4] = Componentb.No;
                worksheet1[i, 5] = Componentb.Name;
                worksheet1[i, 6] = Componentb.SupplierType;
                worksheet1[i, 7] = Componentb.Type;
                worksheet1[i, 8] = Componentb.Size;
                worksheet1[i, 9] = Componentb.Category;
                worksheet1[i, 10] = Componentb.GroupPrincipal;
                worksheet1[i, 11] = Componentb.SingleCarUsageAmount;
                worksheet1[i, 12] = Componentb.ChargeBelow50000;
                worksheet1[i, 13] = Componentb.ChargeAbove50000;
                worksheet1[i, 14] = Componentb.InventoryRequirement1Day;
                worksheet1[i, 15] = Componentb.InventoryRequirement3Day;
                worksheet1[i, 16] = Componentb.InventoryRequirement5Day;
                worksheet1[i, 17] = Componentb.InventoryRequirement10Day;
                worksheet1[i, 18] = Componentb.ID;
                worksheet1[i, 19] = Componentb.WarehouseID;

            }
        }
        private void searchGroupPrincipalReoGridControl()
        {
            ReoGridControl grid = this.reoGridControlComponen;
            var worksheet1 = grid.Worksheets[0];
            worksheet1.Reset();

            WMSEntities wms = new WMSEntities();

            var nameComponent = (from s in wms.Component
                                 where s.GroupPrincipal == toolStripTextBoxSelect.Text
                                 select s).ToArray();

            for (int i = 0; i < nameComponent.Count(); i++)
            {
                DataAccess.Component Componentb = nameComponent[i];
                worksheet1[i, 0] = Componentb.SupplierID;
                worksheet1[i, 1] = Componentb.ContainerNo;
                worksheet1[i, 2] = Componentb.Factroy;
                worksheet1[i, 3] = Componentb.WorkPosition;
                worksheet1[i, 4] = Componentb.No;
                worksheet1[i, 5] = Componentb.Name;
                worksheet1[i, 6] = Componentb.SupplierType;
                worksheet1[i, 7] = Componentb.Type;
                worksheet1[i, 8] = Componentb.Size;
                worksheet1[i, 9] = Componentb.Category;
                worksheet1[i, 10] = Componentb.GroupPrincipal;
                worksheet1[i, 11] = Componentb.SingleCarUsageAmount;
                worksheet1[i, 12] = Componentb.ChargeBelow50000;
                worksheet1[i, 13] = Componentb.ChargeAbove50000;
                worksheet1[i, 14] = Componentb.InventoryRequirement1Day;
                worksheet1[i, 15] = Componentb.InventoryRequirement3Day;
                worksheet1[i, 16] = Componentb.InventoryRequirement5Day;
                worksheet1[i, 17] = Componentb.InventoryRequirement10Day;
                worksheet1[i, 18] = Componentb.ID;
                worksheet1[i, 19] = Componentb.WarehouseID;

            }
        }
        private void searchSingleCarUsageAmountReoGridControl()
        {
            ReoGridControl grid = this.reoGridControlComponen;
            var worksheet1 = grid.Worksheets[0];
            worksheet1.Reset();

            WMSEntities wms = new WMSEntities();

            var nameComponent = (from s in wms.Component
                                 where s.SingleCarUsageAmount == Convert.ToDecimal(toolStripTextBoxSelect.Text)
                                 select s).ToArray();

            for (int i = 0; i < nameComponent.Count(); i++)
            {
                DataAccess.Component Componentb = nameComponent[i];
                worksheet1[i, 0] = Componentb.SupplierID;
                worksheet1[i, 1] = Componentb.ContainerNo;
                worksheet1[i, 2] = Componentb.Factroy;
                worksheet1[i, 3] = Componentb.WorkPosition;
                worksheet1[i, 4] = Componentb.No;
                worksheet1[i, 5] = Componentb.Name;
                worksheet1[i, 6] = Componentb.SupplierType;
                worksheet1[i, 7] = Componentb.Type;
                worksheet1[i, 8] = Componentb.Size;
                worksheet1[i, 9] = Componentb.Category;
                worksheet1[i, 10] = Componentb.GroupPrincipal;
                worksheet1[i, 11] = Componentb.SingleCarUsageAmount;
                worksheet1[i, 12] = Componentb.ChargeBelow50000;
                worksheet1[i, 13] = Componentb.ChargeAbove50000;
                worksheet1[i, 14] = Componentb.InventoryRequirement1Day;
                worksheet1[i, 15] = Componentb.InventoryRequirement3Day;
                worksheet1[i, 16] = Componentb.InventoryRequirement5Day;
                worksheet1[i, 17] = Componentb.InventoryRequirement10Day;
                worksheet1[i, 18] = Componentb.ID;
                worksheet1[i, 19] = Componentb.WarehouseID;

            }
        }
        private void searchChargeBelow50000ReoGridControl()
        {
            ReoGridControl grid = this.reoGridControlComponen;
            var worksheet1 = grid.Worksheets[0];
            worksheet1.Reset();

            WMSEntities wms = new WMSEntities();

            var nameComponent = (from s in wms.Component
                                 where s.ChargeBelow50000 == Convert.ToDecimal(toolStripTextBoxSelect.Text)
                                 select s).ToArray();

            for (int i = 0; i < nameComponent.Count(); i++)
            {
                DataAccess.Component Componentb = nameComponent[i];
                worksheet1[i, 0] = Componentb.SupplierID;
                worksheet1[i, 1] = Componentb.ContainerNo;
                worksheet1[i, 2] = Componentb.Factroy;
                worksheet1[i, 3] = Componentb.WorkPosition;
                worksheet1[i, 4] = Componentb.No;
                worksheet1[i, 5] = Componentb.Name;
                worksheet1[i, 6] = Componentb.SupplierType;
                worksheet1[i, 7] = Componentb.Type;
                worksheet1[i, 8] = Componentb.Size;
                worksheet1[i, 9] = Componentb.Category;
                worksheet1[i, 10] = Componentb.GroupPrincipal;
                worksheet1[i, 11] = Componentb.SingleCarUsageAmount;
                worksheet1[i, 12] = Componentb.ChargeBelow50000;
                worksheet1[i, 13] = Componentb.ChargeAbove50000;
                worksheet1[i, 14] = Componentb.InventoryRequirement1Day;
                worksheet1[i, 15] = Componentb.InventoryRequirement3Day;
                worksheet1[i, 16] = Componentb.InventoryRequirement5Day;
                worksheet1[i, 17] = Componentb.InventoryRequirement10Day;
                worksheet1[i, 18] = Componentb.ID;
                worksheet1[i, 19] = Componentb.WarehouseID;

            }
        }
        private void searchChargeAbove50000ReoGridControl()
        {
            ReoGridControl grid = this.reoGridControlComponen;
            var worksheet1 = grid.Worksheets[0];
            worksheet1.Reset();

            WMSEntities wms = new WMSEntities();

            var nameComponent = (from s in wms.Component
                                 where s.ChargeAbove50000 == Convert.ToDecimal(toolStripTextBoxSelect.Text)
                                 select s).ToArray();

            for (int i = 0; i < nameComponent.Count(); i++)
            {
                DataAccess.Component Componentb = nameComponent[i];
                worksheet1[i, 0] = Componentb.SupplierID;
                worksheet1[i, 1] = Componentb.ContainerNo;
                worksheet1[i, 2] = Componentb.Factroy;
                worksheet1[i, 3] = Componentb.WorkPosition;
                worksheet1[i, 4] = Componentb.No;
                worksheet1[i, 5] = Componentb.Name;
                worksheet1[i, 6] = Componentb.SupplierType;
                worksheet1[i, 7] = Componentb.Type;
                worksheet1[i, 8] = Componentb.Size;
                worksheet1[i, 9] = Componentb.Category;
                worksheet1[i, 10] = Componentb.GroupPrincipal;
                worksheet1[i, 11] = Componentb.SingleCarUsageAmount;
                worksheet1[i, 12] = Componentb.ChargeBelow50000;
                worksheet1[i, 13] = Componentb.ChargeAbove50000;
                worksheet1[i, 14] = Componentb.InventoryRequirement1Day;
                worksheet1[i, 15] = Componentb.InventoryRequirement3Day;
                worksheet1[i, 16] = Componentb.InventoryRequirement5Day;
                worksheet1[i, 17] = Componentb.InventoryRequirement10Day;
                worksheet1[i, 18] = Componentb.ID;
                worksheet1[i, 19] = Componentb.WarehouseID;

            }
        }
        private void searchInventoryRequirement1DayReoGridControl()
        {
            ReoGridControl grid = this.reoGridControlComponen;
            var worksheet1 = grid.Worksheets[0];
            worksheet1.Reset();

            WMSEntities wms = new WMSEntities();

            var nameComponent = (from s in wms.Component
                                 where s.InventoryRequirement1Day == Convert.ToDecimal(toolStripTextBoxSelect.Text)
                                 select s).ToArray();

            for (int i = 0; i < nameComponent.Count(); i++)
            {
                DataAccess.Component Componentb = nameComponent[i];
                worksheet1[i, 0] = Componentb.SupplierID;
                worksheet1[i, 1] = Componentb.ContainerNo;
                worksheet1[i, 2] = Componentb.Factroy;
                worksheet1[i, 3] = Componentb.WorkPosition;
                worksheet1[i, 4] = Componentb.No;
                worksheet1[i, 5] = Componentb.Name;
                worksheet1[i, 6] = Componentb.SupplierType;
                worksheet1[i, 7] = Componentb.Type;
                worksheet1[i, 8] = Componentb.Size;
                worksheet1[i, 9] = Componentb.Category;
                worksheet1[i, 10] = Componentb.GroupPrincipal;
                worksheet1[i, 11] = Componentb.SingleCarUsageAmount;
                worksheet1[i, 12] = Componentb.ChargeBelow50000;
                worksheet1[i, 13] = Componentb.ChargeAbove50000;
                worksheet1[i, 14] = Componentb.InventoryRequirement1Day;
                worksheet1[i, 15] = Componentb.InventoryRequirement3Day;
                worksheet1[i, 16] = Componentb.InventoryRequirement5Day;
                worksheet1[i, 17] = Componentb.InventoryRequirement10Day;
                worksheet1[i, 18] = Componentb.ID;
                worksheet1[i, 19] = Componentb.WarehouseID;

            }
        }
        private void searchInventoryRequirement3DayReoGridControl()
        {
            ReoGridControl grid = this.reoGridControlComponen;
            var worksheet1 = grid.Worksheets[0];
            worksheet1.Reset();

            WMSEntities wms = new WMSEntities();

            var nameComponent = (from s in wms.Component
                                 where s.InventoryRequirement3Day == Convert.ToDecimal(toolStripTextBoxSelect.Text)
                                 select s).ToArray();

            for (int i = 0; i < nameComponent.Count(); i++)
            {
                DataAccess.Component Componentb = nameComponent[i];
                worksheet1[i, 0] = Componentb.SupplierID;
                worksheet1[i, 1] = Componentb.ContainerNo;
                worksheet1[i, 2] = Componentb.Factroy;
                worksheet1[i, 3] = Componentb.WorkPosition;
                worksheet1[i, 4] = Componentb.No;
                worksheet1[i, 5] = Componentb.Name;
                worksheet1[i, 6] = Componentb.SupplierType;
                worksheet1[i, 7] = Componentb.Type;
                worksheet1[i, 8] = Componentb.Size;
                worksheet1[i, 9] = Componentb.Category;
                worksheet1[i, 10] = Componentb.GroupPrincipal;
                worksheet1[i, 11] = Componentb.SingleCarUsageAmount;
                worksheet1[i, 12] = Componentb.ChargeBelow50000;
                worksheet1[i, 13] = Componentb.ChargeAbove50000;
                worksheet1[i, 14] = Componentb.InventoryRequirement1Day;
                worksheet1[i, 15] = Componentb.InventoryRequirement3Day;
                worksheet1[i, 16] = Componentb.InventoryRequirement5Day;
                worksheet1[i, 17] = Componentb.InventoryRequirement10Day;
                worksheet1[i, 18] = Componentb.ID;
                worksheet1[i, 19] = Componentb.WarehouseID;

            }
        }
        private void searchInventoryRequirement5DayReoGridControl()
        {
            ReoGridControl grid = this.reoGridControlComponen;
            var worksheet1 = grid.Worksheets[0];
            worksheet1.Reset();

            WMSEntities wms = new WMSEntities();

            var nameComponent = (from s in wms.Component
                                 where s.InventoryRequirement5Day == Convert.ToDecimal(toolStripTextBoxSelect.Text)
                                 select s).ToArray();

            for (int i = 0; i < nameComponent.Count(); i++)
            {
                DataAccess.Component Componentb = nameComponent[i];
                worksheet1[i, 0] = Componentb.SupplierID;
                worksheet1[i, 1] = Componentb.ContainerNo;
                worksheet1[i, 2] = Componentb.Factroy;
                worksheet1[i, 3] = Componentb.WorkPosition;
                worksheet1[i, 4] = Componentb.No;
                worksheet1[i, 5] = Componentb.Name;
                worksheet1[i, 6] = Componentb.SupplierType;
                worksheet1[i, 7] = Componentb.Type;
                worksheet1[i, 8] = Componentb.Size;
                worksheet1[i, 9] = Componentb.Category;
                worksheet1[i, 10] = Componentb.GroupPrincipal;
                worksheet1[i, 11] = Componentb.SingleCarUsageAmount;
                worksheet1[i, 12] = Componentb.ChargeBelow50000;
                worksheet1[i, 13] = Componentb.ChargeAbove50000;
                worksheet1[i, 14] = Componentb.InventoryRequirement1Day;
                worksheet1[i, 15] = Componentb.InventoryRequirement3Day;
                worksheet1[i, 16] = Componentb.InventoryRequirement5Day;
                worksheet1[i, 17] = Componentb.InventoryRequirement10Day;
                worksheet1[i, 18] = Componentb.ID;
                worksheet1[i, 19] = Componentb.WarehouseID;

            }
        }
        private void searchInventoryRequirement10DayReoGridControl()
        {
            ReoGridControl grid = this.reoGridControlComponen;
            var worksheet1 = grid.Worksheets[0];
            worksheet1.Reset();

            WMSEntities wms = new WMSEntities();

            var nameComponent = (from s in wms.Component
                                 where s.InventoryRequirement10Day == Convert.ToDecimal(toolStripTextBoxSelect.Text)
                                 select s).ToArray();

            for (int i = 0; i < nameComponent.Count(); i++)
            {
                DataAccess.Component Componentb = nameComponent[i];
                worksheet1[i, 0] = Componentb.SupplierID;
                worksheet1[i, 1] = Componentb.ContainerNo;
                worksheet1[i, 2] = Componentb.Factroy;
                worksheet1[i, 3] = Componentb.WorkPosition;
                worksheet1[i, 4] = Componentb.No;
                worksheet1[i, 5] = Componentb.Name;
                worksheet1[i, 6] = Componentb.SupplierType;
                worksheet1[i, 7] = Componentb.Type;
                worksheet1[i, 8] = Componentb.Size;
                worksheet1[i, 9] = Componentb.Category;
                worksheet1[i, 10] = Componentb.GroupPrincipal;
                worksheet1[i, 11] = Componentb.SingleCarUsageAmount;
                worksheet1[i, 12] = Componentb.ChargeBelow50000;
                worksheet1[i, 13] = Componentb.ChargeAbove50000;
                worksheet1[i, 14] = Componentb.InventoryRequirement1Day;
                worksheet1[i, 15] = Componentb.InventoryRequirement3Day;
                worksheet1[i, 16] = Componentb.InventoryRequirement5Day;
                worksheet1[i, 17] = Componentb.InventoryRequirement10Day;
                worksheet1[i, 18] = Componentb.ID;
                worksheet1[i, 19] = Componentb.WarehouseID;

            }
        }


        private void toolStripButtonAdd_Click(object sender, EventArgs e)
        {
            FormBase.FormBaseComponenAdd ad = new FormBase.FormBaseComponenAdd();
            ad.ShowDialog();
            fresh();

        }//添加
  

        private void toolStripButtonAlter_Click(object sender, EventArgs e)
        {
            ReoGridControl grid = this.reoGridControlComponen;
            var worksheet1 = grid.Worksheets[0];

            worksheet1.SelectionMode = WorksheetSelectionMode.Row;//选中行操作

            string str = worksheet1.SelectionRange.ToRelativeAddress();//返回选中地址串
            int start = 2, length = 1;
            //MessageBox.Show(str.Substring(start - 1, length));//返回行数
            int i = Convert.ToInt32(str.Substring(start - 1, length));//变为int型



            String name = worksheet1[i - 1, 5].ToString();

            WMSEntities wms = new WMSEntities();
            DataAccess.Component allComponen = (from s in wms.Component
                                                where s.Name == name
                                                select s).First();
            int a = allComponen.ID;
            int b = allComponen.WarehouseID;
            int c = allComponen.SupplierID;
            string d = allComponen.ContainerNo;
            string e1 = allComponen.Factroy;
            string f = allComponen.WorkPosition;
            string g = allComponen.No;
            string h = allComponen.Name;
            string i1 = allComponen.SupplierType;
            string j = allComponen.Type;
            string k = allComponen.Size;
            string l = allComponen.Category;
            string m = allComponen.GroupPrincipal;
            var n = Convert.ToString(allComponen.SingleCarUsageAmount);
            var o = Convert.ToString(allComponen.ChargeBelow50000);
            var p = Convert.ToString(allComponen.ChargeAbove50000);
            var q = Convert.ToString(allComponen.InventoryRequirement1Day);
            var r = Convert.ToString(allComponen.InventoryRequirement3Day);
            var s1 = Convert.ToString(allComponen.InventoryRequirement5Day);
            var t = Convert.ToString(allComponen.InventoryRequirement10Day);
               
            FormBase.FormBaseComponenAlter adc = new FormBase.FormBaseComponenAlter(a,b,c,d,e1,f,g,h,i1,j,k,l,m,n,o,p,q,r,s1,t);
            adc.ShowDialog();
            fresh();
        }//修改

        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            ReoGridControl grid = this.reoGridControlComponen;
            var worksheet1 = grid.Worksheets[0];

            worksheet1.SelectionMode = WorksheetSelectionMode.Row;//选中行操作

            string str = worksheet1.SelectionRange.ToRelativeAddress();//返回选中地址串
            int start = 2, length = 1;
            //MessageBox.Show(str.Substring(start - 1, length));//返回行数
            int i = Convert.ToInt32(str.Substring(start - 1, length));//变为int型

            String name = worksheet1[i - 1, 5].ToString();

            WMSEntities wms = new WMSEntities();
            DataAccess.Component allComponen = (from s in wms.Component
                                              where s.Name == name
                                              select s).First();
            wms.Component.Remove(allComponen);//删除
            wms.SaveChanges();
            worksheet1.Reset();
            showreoGridControl();//显示所有数据

        }//删除
    }
}
