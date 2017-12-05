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
using WMS.UI.FormReceipt;


namespace WMS.UI
{
    public partial class FormReceiptArrival : Form
    {
        //Dictionary<string, string> receiptNameKeys = new Dictionary<string, string>() { { "ID", "ID" }, { "仓库ID", "Warehouse" }, { "序号", "SerialNumber" }, { "单据类型名称", "TypeName" }, { "单据号", "TicketNo" }, { "送货单号（SRM)", "DeliverTicketNo" }, { "凭证来源", "VoucherSource" }, { "凭证号", "VoucherNo" }, { "凭证行号", "VoucherLineNo" }, { "凭证年", "VoucherYear" }, { "关联凭证号", "ReletedVoucherNo" }, { "关联凭证行号", "ReletedVoucherLineNo" }, { "关联凭证年", "ReletedVoucherYear" }, { "抬头文本", "HeadingText" }, { "过账日期", "PostCountDate" }, { "内向交货单号", "InwardDeliverTicketNo" }, { "内向交货行号", "InwardDeliverLineNo" }, { "外向交货单号", "OutwardDeliverTicketNo" }, { "外向交货行号", "OutwardDeliverLineNo" }, { "采购订单号", "PurchaseTicketNo" }, { "采购订单行号", "PurchaseTicketLineNo" }, { "订单日期", "OrderDate" }, { "收货库位", "ReceiptStorageLocation" }, { "托盘号", "BoardNo" }, { "物料ID", "ComponentID" }, { "物料代码", "ComponentNo" }, { "收货包装", "ReceiptPackage" }, { "期待数量", "ExpectedAmount" }, { "收货数量", "ReceiptCount" }, { "库存状态", "StockState" }, { "存货日期", "InventoryDate" }, { "收货单号", "ReceiptTacketNo" }, { "厂商批号", "ManufactureNo" }, { "生产日期", "ManufactureDate" }, { "失效日期", "ExpiryDate" }, { "项目信息", "ProjectInfo" }, { "项目阶段信息", "ProjectPhaseInfo" }, { "物权属性", "RealRightProperty" }, { "供应商ID", "SupplierID" }, { "供应商", "Supplier" }, { "作业人员", "AssignmentPerson" }, { "是否过账", "PostedCount" }, { "箱号", "BoxNo" }, { "创建时间", "CreateTime" }, { "创建者", "Creater" }, { "最后修改人", "LastUpdatePerson" }, { "最后修改时间", "LastUpdateTime" }, { "移动类型", "MoveType" }, { "单据来源", "Source" } };
     
        public FormReceiptArrival()
        {
            InitializeComponent();
        }
        private void InitComponents()
        {
            //初始化
            this.comboBoxSelect.Items.Add("无");
            string[] columnNames = (from kn in ReceiptMetaData.receiptNameKeys select kn.Name).ToArray();
            this.comboBoxSelect.Items.AddRange(columnNames);
            this.comboBoxSelect.SelectedIndex = 0;

            //初始化表格
            var worksheet = this.reoGridControlUser.Worksheets[0];
            worksheet.SelectionMode = WorksheetSelectionMode.Row;
            for (int i = 0; i < columnNames.Length; i++)
            {
                worksheet.ColumnHeaders[i].Text = columnNames[i];
                worksheet.ColumnHeaders[i].IsVisible = ReceiptMetaData.receiptNameKeys[i].Visible;
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
                        receiptTicketViews = wmsEntities.Database.SqlQuery<ReceiptTicketView>(String.Format("SELECT * FROM ReceiptTicketView WHERE {0} = {1}", key, value)).ToArray();
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
                        object[] columns = Utilities.GetValuesByPropertieNames(curReceiptTicketView, (from kn in ReceiptMetaData.receiptNameKeys select kn.Key).ToArray());
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

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            
            ReceiptTicketModify receiptTicketModify = new ReceiptTicketModify();
            receiptTicketModify.Show();
        }

        private void buttonAlter_Click(object sender, EventArgs e)
        {
            var worksheet = this.reoGridControlUser.Worksheets[0];
            try
            {
                if (worksheet.SelectionRange.Rows != 1)
                {
                    throw new Exception();
                }
                int stockInfoID = int.Parse(worksheet[worksheet.SelectionRange.Row, 0].ToString());
                var receiptTicketModify = new ReceiptTicketModify(FormMode.ALTER,stockInfoID);
                receiptTicketModify.SetModifyFinishedCallback(() =>
                {
                    this.Search(null, null);
                });
                receiptTicketModify.Show();
            }
            catch
            {
                MessageBox.Show("请选择一项进行修改", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }
    }
}
