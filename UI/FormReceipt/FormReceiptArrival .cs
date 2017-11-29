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


namespace WMS.UI
{
    public partial class FormReceiptArrival : Form
    {
        string[] receiptArrivalKeys = { "ID", "仓库ID", "序号", "单据类型名称", "单据号", "送货单号", "凭证来源", "凭证号", "凭证行号", "凭证年", "关联凭证号", "关联凭证行号", "关联凭证年", "抬头文本", "过账日期", "内向交货单号", "内向交货行号", "外向交货单号", "外向交货行号", "采购订单号", "采购订单行号", "订单日期", "收货库位", "托盘号", "物料ID", "物料代码", "收货包装", "期待数量", "收货数量", "库存状态", "存货日期", "收货单号", "厂商批号", "生产日期", "失效日期", "项目信息", "项目阶段信息", "物权属性", "供应商ID", "供应商", "作业人员", "是否过账", "箱号", "创建时间", "创建者", "最后修改人", "最后修改时间", "移动类型", "单据来源" };
        public FormReceiptArrival()
        {
            InitializeComponent();
        }
        private void InitComponents()
        {
            //初始化
            this.comboBoxSelect.Items.Add("无");
            this.comboBoxSelect.Items.AddRange(this.receiptArrivalKeys);


            //初始化表格
            var worksheet = this.reoGridControlUser.Worksheets[0];
            worksheet.SelectionMode = WorksheetSelectionMode.Row;
            for (int i = 0; i < receiptArrivalKeys.Length; i++)
            {
                worksheet.ColumnHeaders[i].Text = receiptArrivalKeys[i];
            }
            worksheet.Columns = receiptArrivalKeys.Length;
        }

        private void FormReceiptArrival_Load(object sender, EventArgs e)
        {
            InitComponents();
            SearchAll();
        }

        private void reoGridControlUser_Click(object sender, EventArgs e)
        {
            
        }

        private void SearchAll()
        {
            var wmsEntities = new WMSEntities();
            var result = (from r in wmsEntities.ReceiptTicket select r).ToArray();
            var worksheet = this.reoGridControlUser.Worksheets[0];
            worksheet.SelectionMode = WorksheetSelectionMode.Row;
            for (int i = 0; i < result.Length; i++)
            {
                ReceiptTicket receiptTicket = result[i];
                object[] item = { result[i].ID, result[i].Warehouse, result[i].SerialNumber, result[i].TypeName, result[i].TicketNo, result[i].DeliverTicketNo, result[i].VoucherSource, result[i].VoucherNo, result[i].VoucherLineNo, result[i].VoucherYear, result[i].ReletedVoucherNo, result[i].ReletedVoucherLineNo, result[i].ReletedVoucherYear, result[i].HeadingText, result[i].PostCountDate, result[i].InwardDeliverTicketNo, result[i].InwardDeliverLineNo, result[i].OutwardDeliverTicketNo, result[i].OutwardDeliverLineNo, result[i].PurchaseTicketNo, result[i].PurchaseTicketLineNo, result[i].OrderDate, result[i].ReceiptStorageLocation, result[i].BoardNo, result[i].ComponentID, result[i].ComponentNo, result[i].ReceiptPackage, result[i].ExpectedAmount, result[i].ReceiptCount, result[i].StockState, result[i].InventoryDate, result[i].ReceiptTacketNo, result[i].ManufactureNo, result[i].ManufactureDate, result[i].ExpiryDate, result[i].ProjectInfo, result[i].ProjectPhaseInfo, result[i].RealRightProperty, result[i].SupplierID, result[i].Supplier, result[i].AssignmentPerson, result[i].PostedCount, result[i].BoxNo, result[i].CreateTime, result[i].Creater, result[i].LastUpdatePerson, result[i].LastUpdateTime, result[i].MoveType, result[i].Source };
                for (int j = 0; j < item.Length; j++)
                {
                    worksheet[i, j] = item[j];
                }

            }
        }

        private void toolStripTop_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void buttonSelect_Click(object sender, EventArgs e)
        {

        }
    }
}
