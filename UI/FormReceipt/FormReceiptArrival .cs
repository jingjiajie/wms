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
using System.Reflection;
using System.Threading;


namespace WMS.UI
{
    public partial class FormReceiptArrival : Form
    {
        //Dictionary<string, string> receiptNameKeys = new Dictionary<string, string>() { { "ID", "ID" }, { "仓库ID", "Warehouse" }, { "序号", "SerialNumber" }, { "单据类型名称", "TypeName" }, { "单据号", "TicketNo" }, { "送货单号（SRM)", "DeliverTicketNo" }, { "凭证来源", "VoucherSource" }, { "凭证号", "VoucherNo" }, { "凭证行号", "VoucherLineNo" }, { "凭证年", "VoucherYear" }, { "关联凭证号", "ReletedVoucherNo" }, { "关联凭证行号", "ReletedVoucherLineNo" }, { "关联凭证年", "ReletedVoucherYear" }, { "抬头文本", "HeadingText" }, { "过账日期", "PostCountDate" }, { "内向交货单号", "InwardDeliverTicketNo" }, { "内向交货行号", "InwardDeliverLineNo" }, { "外向交货单号", "OutwardDeliverTicketNo" }, { "外向交货行号", "OutwardDeliverLineNo" }, { "采购订单号", "PurchaseTicketNo" }, { "采购订单行号", "PurchaseTicketLineNo" }, { "订单日期", "OrderDate" }, { "收货库位", "ReceiptStorageLocation" }, { "托盘号", "BoardNo" }, { "物料ID", "ComponentID" }, { "物料代码", "ComponentNo" }, { "收货包装", "ReceiptPackage" }, { "期待数量", "ExpectedAmount" }, { "收货数量", "ReceiptCount" }, { "库存状态", "StockState" }, { "存货日期", "InventoryDate" }, { "收货单号", "ReceiptTacketNo" }, { "厂商批号", "ManufactureNo" }, { "生产日期", "ManufactureDate" }, { "失效日期", "ExpiryDate" }, { "项目信息", "ProjectInfo" }, { "项目阶段信息", "ProjectPhaseInfo" }, { "物权属性", "RealRightProperty" }, { "供应商ID", "SupplierID" }, { "供应商", "Supplier" }, { "作业人员", "AssignmentPerson" }, { "是否过账", "PostedCount" }, { "箱号", "BoxNo" }, { "创建时间", "CreateTime" }, { "创建者", "Creater" }, { "最后修改人", "LastUpdatePerson" }, { "最后修改时间", "LastUpdateTime" }, { "移动类型", "MoveType" }, { "单据来源", "Source" } };
        struct KeyAndName
        {
            public string Key;
            public string Name;
        };
        KeyAndName[] receiptNameKeys =
        {
            new KeyAndName(){Name = "ID",  Key = "ID"},
            new KeyAndName(){Name = "仓库ID",  Key = "Warehouse"},
            new KeyAndName(){Name = "仓库名称", Key = "WarehouseName"},
            new KeyAndName(){Name = "单据类型名称",  Key = "Type"},
            new KeyAndName(){Name = "单据号",  Key = "ReceiptTicketTicketNo"},
            new KeyAndName(){Name = "送货单号（SRM)",  Key = "ReceiptTicketDeliverTicketNoSRM"},
            new KeyAndName(){Name = "凭证来源",  Key = "ReceiptTicketVoucherSource"},
            new KeyAndName(){Name = "凭证号",  Key = "ReceiptTicketVoucherNo"},
            new KeyAndName(){Name = "凭证行号",  Key = "ReceiptTicketVoucherLineNo"},
            new KeyAndName(){Name = "凭证年",  Key = "ReceiptTicketVoucherYear"},
            new KeyAndName(){Name = "关联凭证号",  Key = "ReceiptTicketReletedVoucherNo"},
            new KeyAndName(){Name = "关联凭证行号",  Key = "ReceiptTicketReletedVoucherLineNo"},
            new KeyAndName(){Name = "关联凭证年",  Key = "ReceiptTicketReletedVoucherYear"},
            new KeyAndName(){Name = "抬头文本",  Key = "ReceiptTicketHeadingText"},
            new KeyAndName(){Name = "过账日期",  Key = "ReceiptTicketPostCountDate"},
            new KeyAndName(){Name = "内向交货单号",  Key = "ReceiptTicketInwardDeliverTicketNo"},
            new KeyAndName(){Name = "内向交货行号",  Key = "ReceiptTicketInwardDeliverLineNo"},
            new KeyAndName(){Name = "外向交货单号",  Key = "ReceiptTicketOutwardDeliverTicketNo"},
            new KeyAndName(){Name = "外向交货行号",  Key = "ReceiptTicketOutwardDeliverLineNo"},
            new KeyAndName(){Name = "采购订单号",  Key = "ReceiptTicketPurchaseTicketNo"},
            new KeyAndName(){Name = "采购订单行号",  Key = "ReceiptTicketPurchaseTicketLineNo"},
            new KeyAndName(){Name = "订单日期",  Key = "ReceiptTicketOrderDate"},
            new KeyAndName(){Name = "收货库位",  Key = "ReceiptTicketReceiptStorageLocation"},
            new KeyAndName(){Name = "托盘号",  Key = "ReceiptTicketBoardNo"},
            new KeyAndName(){Name = "物料ID",  Key = "ReceiptTicketComponentID"},
            new KeyAndName(){Name = "物料代码",  Key = "ReceiptTicketComponentNo"},
            new KeyAndName(){Name = "收货包装",  Key = "ReceiptTicketReceiptPackage"},
            new KeyAndName(){Name = "期待数量",  Key = "ReceiptTicketExpectedAmount"},
            new KeyAndName(){Name = "收货数量",  Key = "ReceiptTicketReceiptCount"},
            new KeyAndName(){Name = "库存状态",  Key = "ReceiptTicketStockState"},
            new KeyAndName(){Name = "存货日期",  Key = "ReceiptTicketInventoryDate"},
            new KeyAndName(){Name = "收货单号",  Key = "ReceiptTicketReceiptTacketNo"},
            new KeyAndName(){Name = "厂商批号",  Key = "ReceiptTicketManufactureNo"},
            new KeyAndName(){Name = "生产日期",  Key = "ReceiptTicketManufactureDate"},
            new KeyAndName(){Name = "失效日期",  Key = "ReceiptTicketExpiryDate"},
            new KeyAndName(){Name = "项目信息",  Key = "ReceiptTicketProjectInfo"},
            new KeyAndName(){Name = "项目阶段信息",  Key = "ReceiptTicketProjectPhaseInfo"},
            new KeyAndName(){Name = "物权属性",  Key = "ReceiptTicketRealRightProperty"},
            new KeyAndName(){Name = "供应商ID",  Key = "ReceiptTicketSupplierID"},
            new KeyAndName(){Name = "供应商",  Key = "ReceiptTicketSupplier"},
            new KeyAndName(){Name = "作业人员",  Key = "ReceiptTicketAssignmentPerson"},
            new KeyAndName(){Name = "是否过账",  Key = "ReceiptTicketPostedCount"},
            new KeyAndName(){Name = "箱号",  Key = "ReceiptTicketBoxNo"},
            new KeyAndName(){Name = "创建时间",  Key = "ReceiptTicketCreateTime"},
            new KeyAndName(){Name = "创建者",  Key = "ReceiptTicketCreater"},
            new KeyAndName(){Name = "最后修改人",  Key = "ReceiptTicketLastUpdatePerson"},
            new KeyAndName(){Name = "最后修改时间",  Key = "ReceiptTicketLastUpdateTime"},
            new KeyAndName(){Name = "移动类型",  Key = "ReceiptTicketMoveType"},
            new KeyAndName(){Name = "单据来源",  Key = "ReceiptTicketSource"}

        };
        public FormReceiptArrival()
        {
            InitializeComponent();
        }
        private void InitComponents()
        {
            //初始化
            this.comboBoxSelect.Items.Add("无");
            string[] columnNames = (from kn in this.receiptNameKeys select kn.Name).ToArray();
            this.comboBoxSelect.Items.AddRange(columnNames);
            this.comboBoxSelect.SelectedIndex = 0;

            //初始化表格
            var worksheet = this.reoGridControlUser.Worksheets[0];
            worksheet.SelectionMode = WorksheetSelectionMode.Row;
            for (int i = 0; i < columnNames.Length; i++)
            {
                worksheet.ColumnHeaders[i].Text = columnNames[i];
            }
            worksheet.Columns = columnNames.Length;
        }

        private void FormReceiptArrival_Load(object sender, EventArgs e)
        {
            InitComponents();
            this.Search(null, null);
        }

        private void reoGridControlUser_Click(object sender, EventArgs e)
        {
            
        }

        private void Search(string key, string value)
        {
            this.lableStatus.Text = "搜索中...";
            
            new Thread(new ThreadStart(() =>
            {
                var wmsEntities = new WMSEntities();
                ReceiptTicketView[] receiptTicketViews = null;
                if (key == null || value == null)        //搜索所有
                {
                    receiptTicketViews = wmsEntities.Database.SqlQuery<ReceiptTicketView>("SELECT * FROM ReceiptTicketView").ToArray();
                }
                else
                {
                    double tmp;
                    if (Double.TryParse(value, out tmp) == false) //不是数字则加上单引号
                    {
                        value = "'" + value + "'";
                    }
                    try
                    {
                        receiptTicketViews = wmsEntities.Database.SqlQuery<ReceiptTicketView>(String.Format("SELECT * FROM ReceiptTicket WHERE {0} = {1}", key, value)).ToArray();
                    }
                    catch
                    {
                        MessageBox.Show("查询的值不合法，请输入正确的值！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                this.reoGridControlUser.Invoke(new Action(() =>
                {
                    this.lableStatus.Text = "搜索完成";
                    var worksheet = this.reoGridControlUser.Worksheets[0];
                    worksheet.DeleteRangeData(RangePosition.EntireRange);
                    for (int i = 0; i < receiptTicketViews.Length; i++)
                    {

                        ReceiptTicketView curReceiptTicketView = receiptTicketViews[i];
                        object[] columns = Utilities.GetValuesByPropertieNames(curReceiptTicketView, (from kn in this.receiptNameKeys select kn.Key).ToArray());
                        for (int j = 0; j < worksheet.Columns; j++)
                        {
                            worksheet[i, j] = columns[j];
                        }
                    }
                }));
                    
             })).Start();
             
        }

        private void toolStripTop_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void buttonSelect_Click(object sender, EventArgs e)
        {
            
            if (comboBoxSelect.SelectedIndex == 0)
            {
                Search(null, null);
            }
            else
            {
                string condition = this.comboBoxSelect.Text;
                string value = this.textBoxSelect.Text;
                Search(condition, value);
            }
        }
    }
}
