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
            new KeyAndName(){Name = "序号",  Key = "SerialNumber"},
            new KeyAndName(){Name = "单据类型名称",  Key = "TypeName"},
            new KeyAndName(){Name = "单据号",  Key = "TicketNo"},
            new KeyAndName(){Name = "送货单号（SRM)",  Key = "DeliverTicketNo"},
            new KeyAndName(){Name = "凭证来源",  Key = "VoucherSource"},
            new KeyAndName(){Name = "凭证号",  Key = "VoucherNo"},
            new KeyAndName(){Name = "凭证行号",  Key = "VoucherLineNo"},
            new KeyAndName(){Name = "凭证年",  Key = "VoucherYear"},
            new KeyAndName(){Name = "关联凭证号",  Key = "ReletedVoucherNo"},
            new KeyAndName(){Name = "关联凭证行号",  Key = "ReletedVoucherLineNo"},
            new KeyAndName(){Name = "关联凭证年",  Key = "ReletedVoucherYear"},
            new KeyAndName(){Name = "抬头文本",  Key = "HeadingText"},
            new KeyAndName(){Name = "过账日期",  Key = "PostCountDate"},
            new KeyAndName(){Name = "内向交货单号",  Key = "InwardDeliverTicketNo"},
            new KeyAndName(){Name = "内向交货行号",  Key = "InwardDeliverLineNo"},
            new KeyAndName(){Name = "外向交货单号",  Key = "OutwardDeliverTicketNo"},
            new KeyAndName(){Name = "外向交货行号",  Key = "OutwardDeliverLineNo"},
            new KeyAndName(){Name = "采购订单号",  Key = "PurchaseTicketNo"},
            new KeyAndName(){Name = "采购订单行号",  Key = "PurchaseTicketLineNo"},
            new KeyAndName(){Name = "订单日期",  Key = "OrderDate"},
            new KeyAndName(){Name = "收货库位",  Key = "ReceiptStorageLocation"},
            new KeyAndName(){Name = "托盘号",  Key = "BoardNo"},
            new KeyAndName(){Name = "物料ID",  Key = "ComponentID"},
            new KeyAndName(){Name = "物料代码",  Key = "ComponentNo"},
            new KeyAndName(){Name = "收货包装",  Key = "ReceiptPackage"},
            new KeyAndName(){Name = "期待数量",  Key = "ExpectedAmount"},
            new KeyAndName(){Name = "收货数量",  Key = "ReceiptCount"},
            new KeyAndName(){Name = "库存状态",  Key = "StockState"},
            new KeyAndName(){Name = "存货日期",  Key = "InventoryDate"},
            new KeyAndName(){Name = "收货单号",  Key = "ReceiptTacketNo"},
            new KeyAndName(){Name = "厂商批号",  Key = "ManufactureNo"},
            new KeyAndName(){Name = "生产日期",  Key = "ManufactureDate"},
            new KeyAndName(){Name = "失效日期",  Key = "ExpiryDate"},
            new KeyAndName(){Name = "项目信息",  Key = "ProjectInfo"},
            new KeyAndName(){Name = "项目阶段信息",  Key = "ProjectPhaseInfo"},
            new KeyAndName(){Name = "物权属性",  Key = "RealRightProperty"},
            new KeyAndName(){Name = "供应商ID",  Key = "SupplierID"},
            new KeyAndName(){Name = "供应商",  Key = "Supplier"},
            new KeyAndName(){Name = "作业人员",  Key = "AssignmentPerson"},
            new KeyAndName(){Name = "是否过账",  Key = "PostedCount"},
            new KeyAndName(){Name = "箱号",  Key = "BoxNo"},
            new KeyAndName(){Name = "创建时间",  Key = "CreateTime"},
            new KeyAndName(){Name = "创建者",  Key = "Creater"},
            new KeyAndName(){Name = "最后修改人",  Key = "LastUpdatePerson"},
            new KeyAndName(){Name = "最后修改时间",  Key = "LastUpdateTime"},
            new KeyAndName(){Name = "移动类型",  Key = "MoveType"},
            new KeyAndName(){Name = "单据来源",  Key = "Source"}

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
                ReceiptTicket[] receiptTickets = null;
                if (key == null || value == null)        //搜索所有
                {
                    receiptTickets = wmsEntities.Database.SqlQuery<ReceiptTicket>("SELECT * FROM ReceiptTicket").ToArray();
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
                        receiptTickets = wmsEntities.Database.SqlQuery<ReceiptTicket>(String.Format("SELECT * FROM ReceiptTicket WHERE {0} = {1}", key, value)).ToArray();
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
                    for (int i = 0; i < receiptTickets.Length; i++)
                    {

                        ReceiptTicket curReceiptTicket = receiptTickets[i];
                        object[] columns = Utilities.GetValuesByPropertieNames(curReceiptTicket, (from kn in this.receiptNameKeys select kn.Key).ToArray());
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

        }
    }
}
