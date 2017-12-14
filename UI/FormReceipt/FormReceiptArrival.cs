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
using System.Data.SqlClient;


namespace WMS.UI
{
    public partial class FormReceiptArrival : Form
    {
        const string STRING_WAIT_CHECK = "待检";
        const string STRING_SEND_CHECK = "送检中";
        const string STRING_FINISHED = "捡毕";
        const string STRING_RECEIPT = "收货";
        const string STRING_REFUSE = "拒收";

        const string STRING_CANCEL = "作废";
        private int projectID;
        private int warehouseID;
        private int userID;
        //Dictionary<string, string> receiptNameKeys = new Dictionary<string, string>() { { "ID", "ID" }, { "仓库ID", "Warehouse" }, { "序号", "SerialNumber" }, { "单据类型名称", "TypeName" }, { "单据号", "TicketNo" }, { "送货单号（SRM)", "DeliverTicketNo" }, { "凭证来源", "VoucherSource" }, { "凭证号", "VoucherNo" }, { "凭证行号", "VoucherLineNo" }, { "凭证年", "VoucherYear" }, { "关联凭证号", "ReletedVoucherNo" }, { "关联凭证行号", "ReletedVoucherLineNo" }, { "关联凭证年", "ReletedVoucherYear" }, { "抬头文本", "HeadingText" }, { "过账日期", "PostCountDate" }, { "内向交货单号", "InwardDeliverTicketNo" }, { "内向交货行号", "InwardDeliverLineNo" }, { "外向交货单号", "OutwardDeliverTicketNo" }, { "外向交货行号", "OutwardDeliverLineNo" }, { "采购订单号", "PurchaseTicketNo" }, { "采购订单行号", "PurchaseTicketLineNo" }, { "订单日期", "OrderDate" }, { "收货库位", "ReceiptStorageLocation" }, { "托盘号", "BoardNo" }, { "物料ID", "ComponentID" }, { "物料代码", "ComponentNo" }, { "收货包装", "ReceiptPackage" }, { "期待数量", "ExpectedAmount" }, { "收货数量", "ReceiptCount" }, { "库存状态", "StockState" }, { "存货日期", "InventoryDate" }, { "收货单号", "ReceiptTacketNo" }, { "厂商批号", "ManufactureNo" }, { "生产日期", "ManufactureDate" }, { "失效日期", "ExpiryDate" }, { "项目信息", "ProjectInfo" }, { "项目阶段信息", "ProjectPhaseInfo" }, { "物权属性", "RealRightProperty" }, { "供应商ID", "SupplierID" }, { "供应商", "Supplier" }, { "作业人员", "AssignmentPerson" }, { "是否过账", "PostedCount" }, { "箱号", "BoxNo" }, { "创建时间", "CreateTime" }, { "创建者", "Creater" }, { "最后修改人", "LastUpdatePerson" }, { "最后修改时间", "LastUpdateTime" }, { "移动类型", "MoveType" }, { "单据来源", "Source" } };
        public FormReceiptArrival()
        {
            InitializeComponent();
        }

        public FormReceiptArrival(int projectID, int warehouseID, int userID)
        {
            InitializeComponent();
            this.projectID = projectID;
            this.warehouseID = warehouseID;
            this.userID = userID;
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
                    receiptTicketViews = wmsEntities.Database.SqlQuery<ReceiptTicketView>("SELECT * FROM ReceiptTicketView WHERE Warehouse = @warehouseID AND ProjectID = @projectID", new SqlParameter[] { new SqlParameter("warehouseID", this.warehouseID), new SqlParameter("projectID", this.projectID)}).ToArray();
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
                        receiptTicketViews = wmsEntities.Database.SqlQuery<ReceiptTicketView>(String.Format("SELECT * FROM ReceiptTicketView WHERE {0} = @key AND Warehouse = @warehouseID AND ProjectID = @projectID ", key), new SqlParameter[] { new SqlParameter("@key", value), new SqlParameter("@warehouseID", this.warehouseID), new SqlParameter("@projectID", this.projectID)}).ToArray();
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
                    int n = 0;
                    for (int i = 0; i < receiptTicketViews.Length; i++)
                    {

                        ReceiptTicketView curReceiptTicketView = receiptTicketViews[i];
                        if (curReceiptTicketView.State == "作废")
                        {
                            continue;
                        }
                        object[] columns = Utilities.GetValuesByPropertieNames(curReceiptTicketView, (from kn in ReceiptMetaData.receiptNameKeys select kn.Key).ToArray());
                        for (int j = 0; j < worksheet.Columns; j++)
                        {
                            
                            worksheet[n, j] = columns[j];
                        }
                        n++;
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
                string key = "";
                foreach(KeyName kn in ReceiptMetaData.receiptNameKeys)
                {
                    if (condition == kn.Name)
                    {
                        key = kn.Key;
                        break;
                    }
                }
                string value = this.textBoxSelect.Text;
                Search(key, value);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            
            FormReceiptTicketModify receiptTicketModify = new FormReceiptTicketModify(FormMode.ADD, -1, this.projectID, this.warehouseID,this.userID);
            receiptTicketModify.SetModifyFinishedCallback(() =>
            {
                this.Search(null, null);
            });
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
                int receiptTicketID = int.Parse(worksheet[worksheet.SelectionRange.Row, 0].ToString());
                var receiptTicketModify = new FormReceiptTicketModify(FormMode.ALTER, receiptTicketID, this.projectID, this.warehouseID,this.userID);
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

        private void buttonDelete_Click(object sender, EventArgs e)
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
            //this.labelStatus.Text = "正在删除...";


            new Thread(new ThreadStart(() =>
            {
                WMSEntities wmsEntities = new WMSEntities();
                foreach (int id in deleteIDs)
                {
                    wmsEntities.Database.ExecuteSqlCommand("DELETE FROM ReceiptTicket WHERE ID=@receiptTicketID", new SqlParameter("receiptTicketID", id));

                    //wmsEntities.Database.ExecuteSqlCommand("UPDATE ReceiptTicket SET State='作废' WHERE ID = @receiptTicketID", new SqlParameter("receiptTicketID", id));
                    //wmsEntities.Database.ExecuteSqlCommand("UPDATE ReceiptTicketItem SET State='作废' WHERE ReceiptTicketID = @receiptTicketID", new SqlParameter("receiptTicketID", id));
                }
                wmsEntities.SaveChanges();
                this.Search(null, null);
            })).Start();
        }

        private void buttonCheck_Click(object sender, EventArgs e)
        {
            var worksheet = this.reoGridControlUser.Worksheets[0];
            try
            {
                WMSEntities wmsEntities = new WMSEntities();
                if (worksheet.SelectionRange.Rows != 1)
                {
                    throw new Exception();
                }
                int receiptTicketID = int.Parse(worksheet[worksheet.SelectionRange.Row, 0].ToString());
                ReceiptTicket receiptTicket = (from rt in wmsEntities.ReceiptTicket where rt.ID == receiptTicketID select rt).Single();
                if (receiptTicket.State == "送检中" || receiptTicket.State == "已收货")
                {
                    MessageBox.Show("该收货单"+ receiptTicket.State);
                    return;
                }
                FormAddSubmissionTicket formAddSubmissionTicket = new FormAddSubmissionTicket(receiptTicketID);
                formAddSubmissionTicket.Show();
                //var receiptTicketModify = new ReceiptTicketModify(FormMode.ALTER, stockInfoID);
                //var formReceiptArrivalCheck = new FormReceiptArrivalCheck(receiptTicketID, AllOrPartial.ALL);
                formAddSubmissionTicket.SetCallBack(() =>
                {
                    this.Search(null, null);
                });
                formAddSubmissionTicket.Show();
            }
            catch
            {
                MessageBox.Show("请选择一项进行送检", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void buttonCheckCancel_Click(object sender, EventArgs e)
        {
            var worksheet = this.reoGridControlUser.Worksheets[0];
            try
            {
                if (worksheet.SelectionRange.Rows != 1)
                {
                    throw new Exception();
                }
                int receiptTicketID = int.Parse(worksheet[worksheet.SelectionRange.Row, 0].ToString());
                WMSEntities wmsEntities = new WMSEntities();
                ReceiptTicket receiptTicket = (from rt in wmsEntities.ReceiptTicket where rt.ID == receiptTicketID select rt).Single();
                SubmissionTicket submissionTicket = (from sb in wmsEntities.SubmissionTicket where sb.ReceiptTicketID == receiptTicketID && sb.State != STRING_CANCEL select sb).Single();
                SubmissionTicketItem[] submissionTicketItems = (from sbi in wmsEntities.SubmissionTicketItem where sbi.SubmissionTicketID == submissionTicket.ID select sbi).ToArray();
                if (receiptTicket.State == "送检中" || receiptTicket.State == "部分送检中")
                {
                    receiptTicket.State = "待检";
                    //submissionTicket.State = STRING_CANCEL;
                    wmsEntities.Database.ExecuteSqlCommand("DELETE FROM SubmissionTicket WHERE ReceiptTicketID=@receiptTicketID", new SqlParameter("receiptTicket", receiptTicketID));
                    wmsEntities.SaveChanges();
                }
                else
                {
                    MessageBox.Show("此收货单并未送检!");
                }
            }
            catch
            {
                MessageBox.Show("请选择收货单");
            }
            Search(null, null);
        }

        private void buttonItems_Click(object sender, EventArgs e)
        {
            var worksheet = this.reoGridControlUser.Worksheets[0];
            try
            {
                if (worksheet.SelectionRange.Rows != 1)
                {
                    throw new Exception();
                }
                int receiptTicketID = int.Parse(worksheet[worksheet.SelectionRange.Row, 0].ToString());
                var formReceiptTicketIems = new FormReceiptItems(FormMode.ALTER, receiptTicketID);
                formReceiptTicketIems.SetCallback(() =>
                {
                    this.Search(null, null);
                });
                formReceiptTicketIems.Show();
            }
            catch
            {
                MessageBox.Show("请选择一项进行修改", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void buttonMakePutaway_Click(object sender, EventArgs e)
        {   
            var worksheet = this.reoGridControlUser.Worksheets[0];
            try
            {
                if (worksheet.SelectionRange.Rows != 1)
                {
                    throw new Exception();
                }
                WMSEntities wmsEntities = new WMSEntities();
                int receiptTicketID = int.Parse(worksheet[worksheet.SelectionRange.Row, 0].ToString());
                //var formReceiptTicketIems = new FormReceiptItems(FormMode.ALTER, receiptTicketID);
                ReceiptTicket receiptTicket = (from rt in wmsEntities.ReceiptTicket where rt.ID == receiptTicketID select rt).Single();
                FormPutaway formPutaway = new FormPutaway(receiptTicketID);
                formPutaway.Show();
                /*
                if (receiptTicket.State == "收货")
                {
                    MessageBox.Show("已收货");
                    return;
                }
                FormPutwayTicketModify formPutwayTicketModify = new FormPutwayTicketModify(receiptTicketID, FormMode.ADD);
                formPutwayTicketModify.SetCallBack(() =>
                {
                    
                    this.Search(null, null);
                });
                formPutwayTicketModify.Show();
                */
            }
            catch
            {
                MessageBox.Show("请选择一项进行修改", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
        }

        private void buttonReceiptCancel_Click(object sender, EventArgs e)
        {
            var worksheet = this.reoGridControlUser.Worksheets[0];
            try
            {
                if (worksheet.SelectionRange.Rows != 1)
                {
                    throw new Exception();
                }
                WMSEntities wmsEntities = new WMSEntities();
                int receiptTicketID = int.Parse(worksheet[worksheet.SelectionRange.Row, 0].ToString());
                //var formReceiptTicketIems = new FormReceiptItems(FormMode.ALTER, receiptTicketID);
                ReceiptTicket receiptTicket = (from rt in wmsEntities.ReceiptTicket where rt.ID == receiptTicketID select rt).Single();
                FormReceiptArrivalCheck formReceiptArrivalCheck = new FormReceiptArrivalCheck(receiptTicketID);
                formReceiptArrivalCheck.Show();
            }
            catch
            {
                MessageBox.Show("请选择一项进行修改", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }


        /***********************
         * 只收货 不生产成上架单
        ************************/
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            var worksheet = this.reoGridControlUser.Worksheets[0];
            try
            {
                if (worksheet.SelectionRange.Rows != 1)
                {
                    throw new Exception();
                }
                WMSEntities wmsEntities = new WMSEntities();
                int receiptTicketID = int.Parse(worksheet[worksheet.SelectionRange.Row, 0].ToString());
                //var formReceiptTicketIems = new FormReceiptItems(FormMode.ALTER, receiptTicketID);
                ReceiptTicket receiptTicket = (from rt in wmsEntities.ReceiptTicket where rt.ID == receiptTicketID select rt).Single();
                if (receiptTicket.State == "已收货")
                {
                    MessageBox.Show("该收货单已收货");
                }
                else
                {
                    receiptTicket.State = "已收货";
                    wmsEntities.Database.ExecuteSqlCommand(
                        "UPDATE ReceiptTicketItem SET State='已收货' " +
                        "WHERE ReceiptTicketID=@receiptTicketID", 
                        new SqlParameter("receiptTicketID", receiptTicketID));
                    new Thread(() =>
                    {
                        wmsEntities.SaveChanges();
                        MessageBox.Show("成功");
                        this.Invoke(new Action(() => {
                            this.Search(null,null);
                        }));
                    }).Start();
                }
            }
            catch
            {
                MessageBox.Show("请选择一项进行修改", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }
    }
}
