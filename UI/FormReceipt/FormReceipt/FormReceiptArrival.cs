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
        PagerWidget<ReceiptTicketView> pagerWidget;

        private Action<string, string> ToSubmission = null;
        private Action<string, string> ToPutaway = null;
        //private int receiptTicketID;
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
        /// <summary>
        /// form = 0 Submission ; form = 1 Putaway
        /// </summary>
        /// <param name="form"></param>
        /// <param name="action"></param>
        public void SetActionTo(int form, Action<string, string> action)
        {
            if (form == 0)
            {
                this.ToSubmission = action;
            }
            else if (form == 1)
            {
                this.ToPutaway = action;
            }
            else
            {
                throw new Exception("没有这个窗口，0是送检，1是上架");
            }
        }

        private void InitComponents()
        {
            //初始化
            this.comboBoxSelect.Items.Add("无");
            string[] columnNames = (from kn in ReceiptMetaData.receiptNameKeys where kn.Visible == true select kn.Name).ToArray();
            this.comboBoxSelect.Items.AddRange(columnNames);
            this.comboBoxSelect.SelectedIndex = 0;

            //初始化表格
            var worksheet = this.reoGridControlUser.Worksheets[0];
            worksheet.SelectionMode = WorksheetSelectionMode.Row;
            for (int i = 0; i < ReceiptMetaData.receiptNameKeys.Length; i++)
            {
                worksheet.ColumnHeaders[i].Text = ReceiptMetaData.receiptNameKeys[i].Name;
                worksheet.ColumnHeaders[i].IsVisible = ReceiptMetaData.receiptNameKeys[i].Visible;
            }
            worksheet.Columns = ReceiptMetaData.receiptNameKeys.Length;
        }

        private void FormReceiptArrival_Load(object sender, EventArgs e)
        {
            InitComponents();
            pagerWidget = new PagerWidget<ReceiptTicketView>(this.reoGridControlUser, ReceiptMetaData.receiptNameKeys, projectID, warehouseID);
            this.panel1.Controls.Add(pagerWidget);
            
            pagerWidget.Show();
            this.Search();
            //pagerWidget.Show();
            //pagerWidget.Search();
        }

        private void Search(bool savePage = false, int selectID = -1)
        {
            this.pagerWidget.ClearCondition();
            if (this.comboBoxSelect.SelectedIndex != 0)
            {
                this.pagerWidget.AddCondition(this.comboBoxSelect.SelectedItem.ToString(), this.textBoxSelect.Text);
            }
            this.pagerWidget.Search(savePage, selectID);
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
                    receiptTicketViews = wmsEntities.Database.SqlQuery<ReceiptTicketView>("SELECT * FROM ReceiptTicketView WHERE Warehouse = @warehouseID AND ProjectID = @projectID ORDER BY ID DESC", new SqlParameter[] { new SqlParameter("warehouseID", this.warehouseID), new SqlParameter("projectID", this.projectID) }).ToArray();
                }
                else
                {
                    double tmp;
                    //if (Double.TryParse(value, out tmp) == false) //不是数字则加上单引号
                    //{
                    //    //value =  + value + "'";
                    //}
                    try
                    {
                        //   Console.Write(value);
                        string sql = "SELECT * FROM ReceiptTicketView WHERE " + key + " = @key AND Warehouse = @warehouseID AND ProjectID = @projectID ORDER BY ID DESC";
                        //  Console.WriteLine(sql);
                        receiptTicketViews = wmsEntities.Database.SqlQuery<ReceiptTicketView>(sql, new SqlParameter[] { new SqlParameter("@key", value), new SqlParameter("@warehouseID", this.warehouseID), new SqlParameter("@projectID", this.projectID) }).ToArray();

                        //receiptTicketViews = (from rtv in wmsEntities.ReceiptTicketView where rtv.State == )
                        //receiptTicketViews = wmsEntities.Database.SqlQuery<ReceiptTicketView>("SELECT * FROM ReceiptTicketView WHERE ")
                    }
                    catch (EntityCommandExecutionException)
                    {
                        MessageBox.Show("查询的值不合法，请输入正确的值！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("无法连接到数据库，请查看网络连接!", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
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
                            if (columns[j] == null)
                            {
                                worksheet[n, j] = columns[j];
                            }
                            else
                            {
                                worksheet[n, j] = columns[j].ToString();
                            }
                        }
                        n++;
                    }
                }));
                if (receiptTicketViews.Length == 0)
                {
                    int m = ReceiptUtilities.GetFirstColumnIndex(ReceiptMetaData.receiptNameKeys);

                    //this.reoGridControl1.Worksheets[0][6, 8] = "32323";
                    this.reoGridControlUser.Worksheets[0][0, m] = "没有查询到符合条件的记录";
                }

            })).Start();

        }

        private void toolStripTop_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void buttonSelect_Click(object sender, EventArgs e)
        {

            if (comboBoxSelect.SelectedIndex == 0)
            {
                Search();
            }
            else
            {
                string condition = this.comboBoxSelect.Text;
                string key = "";
                foreach (KeyName kn in ReceiptMetaData.receiptNameKeys)
                {
                    if (condition == kn.Name)
                    {
                        key = kn.Key;
                        break;
                    }
                }
                string value = this.textBoxSelect.Text;
                this.pagerWidget.ClearCondition();
                this.pagerWidget.AddCondition(key, value);
                Search();
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            FormReceiptTicketModify receiptTicketModify = new FormReceiptTicketModify(FormMode.ADD, -1, this.projectID, this.warehouseID, this.userID);
            receiptTicketModify.SetModifyFinishedCallback(() =>
            {
                this.Search();
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
                    MessageBox.Show("请选择一项进行修改", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                int receiptTicketID;
                try
                {
                    receiptTicketID = int.Parse(worksheet[worksheet.SelectionRange.Row, 0].ToString());
                }
                catch
                {
                    MessageBox.Show("请选择一项进行修改", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                int count;
                try
                {
                    WMSEntities wmsEntities = new WMSEntities();
                    count = wmsEntities.Database.SqlQuery<int>("SELECT COUNT(*) FROM ReceiptTicket WHERE ID = @receiptTicketID", new SqlParameter("receiptTicketID", receiptTicketID)).FirstOrDefault();
                }
                catch
                {
                    MessageBox.Show("网络连接失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (count != 0)
                {
                    var receiptTicketModify = new FormReceiptTicketModify(FormMode.ALTER, receiptTicketID, this.projectID, this.warehouseID, this.userID);
                    receiptTicketModify.SetModifyFinishedCallback(() =>
                    {
                        this.Search();
                    });
                    receiptTicketModify.Show();
                }
                else
                {
                    MessageBox.Show("该记录已被其他用户删除");
                }
            }
            catch
            {
                MessageBox.Show("无法连接到数据库，请查看网络连接!", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
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
                try
                {
                    foreach (int id in deleteIDs)
                    {
                        try
                        {
                            wmsEntities.Database.ExecuteSqlCommand("DELETE FROM ReceiptTicket WHERE ID=@receiptTicketID", new SqlParameter("receiptTicketID", id));
                        }
                        catch
                        {
                            MessageBox.Show("该收货单已收货或送检，无法删除");
                        }
                        //wmsEntities.Database.ExecuteSqlCommand("UPDATE ReceiptTicket SET State='作废' WHERE ID = @receiptTicketID", new SqlParameter("receiptTicketID", id));
                        //wmsEntities.Database.ExecuteSqlCommand("UPDATE ReceiptTicketItem SET State='作废' WHERE ReceiptTicketID = @receiptTicketID", new SqlParameter("receiptTicketID", id));
                    }
                    wmsEntities.SaveChanges();
                }
                catch
                {
                    MessageBox.Show("无法连接到数据库，请查看网络连接!", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    return;
                }
                this.Search();
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
                    MessageBox.Show("请选择一项进行修改", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                int receiptTicketID;
                try
                {
                    receiptTicketID = int.Parse(worksheet[worksheet.SelectionRange.Row, 0].ToString());
                }
                catch
                {
                    MessageBox.Show("请选择一项进行修改", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                ReceiptTicket receiptTicket = (from rt in wmsEntities.ReceiptTicket where rt.ID == receiptTicketID select rt).Single();
                if (receiptTicket.State == "送检中" || receiptTicket.State == "已收货")
                {
                    MessageBox.Show("该收货单" + receiptTicket.State);
                    return;
                }
                FormAddSubmissionTicket formAddSubmissionTicket = new FormAddSubmissionTicket(receiptTicketID, this.userID, FormMode.ADD);
                formAddSubmissionTicket.Show();
                //var receiptTicketModify = new ReceiptTicketModify(FormMode.ALTER, stockInfoID);
                //var formReceiptArrivalCheck = new FormReceiptArrivalCheck(receiptTicketID, AllOrPartial.ALL);
                formAddSubmissionTicket.SetCallBack(() =>
                {
                    this.Search();
                });
                formAddSubmissionTicket.Show();
            }

            catch
            {
                MessageBox.Show("无法连接到数据库，请查看网络连接!", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
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
                    MessageBox.Show("请选择一项进行修改", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                int receiptTicketID;
                try
                {
                    receiptTicketID = int.Parse(worksheet[worksheet.SelectionRange.Row, 0].ToString());
                }
                catch
                {
                    MessageBox.Show("请选择一项进行修改", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                WMSEntities wmsEntities = new WMSEntities();
                ReceiptTicket receiptTicket = (from rt in wmsEntities.ReceiptTicket where rt.ID == receiptTicketID select rt).Single();
                SubmissionTicket submissionTicket = (from sb in wmsEntities.SubmissionTicket where sb.ReceiptTicketID == receiptTicketID && sb.State != STRING_CANCEL select sb).FirstOrDefault();
                if (submissionTicket == null)
                {
                    MessageBox.Show("该收货单没有送检！");
                }
                else
                {
                    SubmissionTicketItem[] submissionTicketItems = (from sbi in wmsEntities.SubmissionTicketItem where sbi.SubmissionTicketID == submissionTicket.ID select sbi).ToArray();
                    if (receiptTicket.State == "送检中" || receiptTicket.State == "部分送检中")
                    {
                        receiptTicket.State = "待送检";
                        //submissionTicket.State = STRING_CANCEL;
                        wmsEntities.Database.ExecuteSqlCommand("DELETE FROM SubmissionTicket WHERE ReceiptTicketID=@receiptTicketID", new SqlParameter("receiptTicketID", receiptTicketID));
                        wmsEntities.SaveChanges();
                    }
                    else
                    {
                        MessageBox.Show("此收货单并未送检!");
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("无法连接到数据库，请查看网络连接!", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                return;
            }
            Search();
        }

        private void buttonItems_Click(object sender, EventArgs e)
        {
            var worksheet = this.reoGridControlUser.Worksheets[0];
            try
            {
                if (worksheet.SelectionRange.Rows != 1)
                {
                    MessageBox.Show("请选择一项进行修改", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                int receiptTicketID;
                try
                {
                    receiptTicketID = int.Parse(worksheet[worksheet.SelectionRange.Row, 0].ToString());
                }
                catch
                {
                    MessageBox.Show("请选择一项进行修改", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var formReceiptTicketIems = new FormReceiptItems(FormMode.ALTER, receiptTicketID);
                formReceiptTicketIems.SetCallback(() =>
                {
                    this.Search();
                });
                formReceiptTicketIems.Show();
            }

            catch (Exception)
            {
                MessageBox.Show("无法连接到数据库，请查看网络连接!", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
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
                    MessageBox.Show("请选择一项进行修改", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                WMSEntities wmsEntities = new WMSEntities();
                int receiptTicketID;
                try
                {
                    receiptTicketID = int.Parse(worksheet[worksheet.SelectionRange.Row, 0].ToString());
                }
                catch
                {
                    MessageBox.Show("请选择一项进行修改", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //var formReceiptTicketIems = new FormReceiptItems(FormMode.ALTER, receiptTicketID);

                ReceiptTicket receiptTicket = (from rt in wmsEntities.ReceiptTicket where rt.ID == receiptTicketID select rt).Single();
                if (receiptTicket.State != "已收货")
                {
                    MessageBox.Show("该收货单未收货，请先收货!");
                    return;
                }
                /*
                if (receiptTicket.HasPutawayTicket == "是")
                {
                    MessageBox.Show("该收货单已经生成上架单，点击查看对应上架单按钮查看！");
                    return;
                }*/
                if (receiptTicket.ReceiptTicketItem.ToArray().Length == 0)
                {
                    MessageBox.Show("没有为该收货单添加收货单条目，无法生成上架单，请添加后重试");
                    return;
                }
                FormPutawayNew formPutawayNew = new FormPutawayNew(receiptTicket.ID, this.userID, FormMode.ADD);
                formPutawayNew.SetCallBack(new Action(() =>
                {
                    this.Search();
                }));

                formPutawayNew.Show();
                /*
                FormPutaway formPutaway = new FormPutaway(receiptTicketID, this.warehouseID, this.projectID, this.userID);
                formPutaway.Show();
                */

                /*
                if (receiptTicket.State == "收货")
                {
                    MessageBox.Show("已收货");
                    return;
                }
                FormPutwayTicketModify formPutwayTicketModify = new FormPutwayTicketModify(receiptTicketID, FormMode.ADD);
                formPutwayTicketModify.SetCallBack(() =>
                {
                    
                    this.Search();
                });
                formPutwayTicketModify.Show();
                */
            }

            catch (Exception)
            {
                MessageBox.Show("无法连接到数据库，请查看网络连接!", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
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
                    MessageBox.Show("请选择一项进行修改", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                WMSEntities wmsEntities = new WMSEntities();
                int receiptTicketID;
                try
                {
                    receiptTicketID = int.Parse(worksheet[worksheet.SelectionRange.Row, 0].ToString());
                }
                catch
                {
                    MessageBox.Show("请选择一项进行修改", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                //var formReceiptTicketIems = new FormReceiptItems(FormMode.ALTER, receiptTicketID);
                ReceiptTicket receiptTicket = (from rt in wmsEntities.ReceiptTicket where rt.ID == receiptTicketID select rt).FirstOrDefault();
                if (receiptTicket == null)
                {
                    MessageBox.Show("找不到此收货单");
                    return;
                }
                else
                {
                    if (receiptTicket.State != "待送检")
                    {
                        MessageBox.Show("该收货单状态为" + receiptTicket.State + "，无法送检！");
                        return;
                    }
                    if (receiptTicket.ReceiptTicketItem.ToArray().Length == 0)
                    {
                        MessageBox.Show("没有为该收货单添加收货单条目，无法生成送检单，请添加后重试");
                        return;
                    }
                    FormReceiptSubmissionNew formReceiptSubmissionNew = new FormReceiptSubmissionNew(receiptTicketID, this.userID, FormMode.ADD);
                    formReceiptSubmissionNew.SetCallBack(new Action(() =>
                    {
                        this.Search();
                    }));
                    formReceiptSubmissionNew.Show();
                }
                /*
                FormReceiptArrivalCheck formReceiptArrivalCheck = new FormReceiptArrivalCheck(receiptTicketID, this.userID);
                formReceiptArrivalCheck.SetNextCallBack(new Action(() =>
                {
                    this.Search();
                }));
                formReceiptArrivalCheck.Show();
                */
            }
            
            catch (Exception)
            {
                MessageBox.Show("无法连接到数据库，请查看网络连接!", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
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
                    MessageBox.Show("请选择一项进行修改", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                WMSEntities wmsEntities = new WMSEntities();
                int receiptTicketID;
                try
                {
                    receiptTicketID = int.Parse(worksheet[worksheet.SelectionRange.Row, 0].ToString());
                }
                catch
                {
                    MessageBox.Show("请选择一项进行修改", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //var formReceiptTicketIems = new FormReceiptItems(FormMode.ALTER, receiptTicketID);
                ReceiptTicket receiptTicket = (from rt in wmsEntities.ReceiptTicket where rt.ID == receiptTicketID select rt).Single();
                if (receiptTicket.State == "已收货")
                {
                    MessageBox.Show("该收货单已收货");
                }
                else if (receiptTicket.State == "送检中")
                {
                    MessageBox.Show("该收货单正在送检中");
                }
                else
                {
                    int count = wmsEntities.Database.SqlQuery<int>("SELECT COUNT(*) FROM ReceiptTicketItem WHERE ReceiptTicketID = @receiptTicketID", new SqlParameter("receiptTicketID", receiptTicketID)).FirstOrDefault();
                    if (count == 0)
                    {
                        if (MessageBox.Show("该收货单中没有添加条目，是否继续收货", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            return;
                        }
                    }
                    receiptTicket.State = "已收货";
                    wmsEntities.Database.ExecuteSqlCommand(
                        "UPDATE ReceiptTicketItem SET State='已收货' " +
                        "WHERE ReceiptTicketID=@receiptTicketID",
                        new SqlParameter("receiptTicketID", receiptTicketID));
                    new Thread(() =>
                    {
                        wmsEntities.SaveChanges();
                        MessageBox.Show("成功");
                        this.Invoke(new Action(() =>
                        {
                            this.Search();
                        }));
                    }).Start();
                }
            }
            
            catch (Exception)
            {
                MessageBox.Show("无法连接到数据库，请查看网络连接!", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                return;
            }
        }

        private void comboBoxSelect_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            var worksheet = this.reoGridControlUser.Worksheets[0];
            try
            {
                if (worksheet.SelectionRange.Rows != 1)
                {
                    MessageBox.Show("请选择一项进行修改", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                WMSEntities wmsEntities = new WMSEntities();
                int receiptTicketID = int.Parse(worksheet[worksheet.SelectionRange.Row, 0].ToString());
                //var formReceiptTicketIems = new FormReceiptItems(FormMode.ALTER, receiptTicketID);
                ReceiptTicket receiptTicket = (from rt in wmsEntities.ReceiptTicket where rt.ID == receiptTicketID select rt).Single();
                if (receiptTicket.State != "已收货")
                {
                    MessageBox.Show("该收货单未收货");
                }
                else
                {
                    int count1 = wmsEntities.Database.SqlQuery<int>(
                        "SELECT COUNT(*) FROM SubmissionTicket " +
                        "WHERE ReceiptTicketID = @receiptTicketID", new SqlParameter("receiptTicketID", receiptTicketID)).FirstOrDefault();
                    int count2 = wmsEntities.Database.SqlQuery<int>(
                        "SELECT COUNT(*) FROM SubmissionTicket " +
                        "WHERE ReceiptTicketID = @receipTicketID AND State = '待检'", new SqlParameter("receipTicketID", receiptTicketID)).FirstOrDefault();
                    if (count1 == 0)
                    {
                        receiptTicket.State = "待检";
                    }
                    else
                    {
                        if (count2 != count1)
                        {
                            receiptTicket.State = "过检";
                        }
                        else
                        {
                            receiptTicket.State = "送检中";
                        }
                        wmsEntities.Database.ExecuteSqlCommand("UPDATE ReceiptTicketItem SET State = '取消收货' WHERE ReceiptTicketID = @receiptTicketID", new SqlParameter("receiptTicketID", receiptTicketID));

                    }
                    new Thread(() =>
                    {
                        wmsEntities.Database.ExecuteSqlCommand("UPDATE ReceiptTicketItem SET State = '待检' WHERE ReceiptTicketID = @receiptTicketID", new SqlParameter("receiptTicketID", receiptTicketID));
                        wmsEntities.SaveChanges();
                        MessageBox.Show("成功");
                        this.Invoke(new Action(() =>
                        {
                            this.Search();
                        }));
                    }).Start();
                }
            }
           
            catch (Exception)
            {
                MessageBox.Show("无法连接到数据库，请查看网络连接!", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                return;
            }
        }

        private void comboBoxSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBoxSelect.SelectedIndex == 0)
            {
                this.textBoxSelect.Enabled = false;
                this.textBoxSelect.Text = "";
            }
            else
            {
                this.textBoxSelect.Enabled = true;
                this.textBoxSelect.Text = "";
            }
        }

        private void comboBoxSelect_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (this.comboBoxSelect.SelectedIndex == 0)
            {
                this.textBoxSelect.Text = "";
                this.textBoxSelect.Enabled = false;
            }
            else
            {
                this.textBoxSelect.Text = "";
                this.textBoxSelect.Enabled = true;
            }
        }

        private void toolStripButton3_Click_1(object sender, EventArgs e)
        {
            var worksheet = this.reoGridControlUser.Worksheets[0];
            try
            {
                WMSEntities wmsEntities = new WMSEntities();
                if (worksheet.SelectionRange.Rows != 1)
                {
                    MessageBox.Show("请选择一项进行修改", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                int receiptTicketID = int.Parse(worksheet[worksheet.SelectionRange.Row, 0].ToString());
                ReceiptTicket receiptTicket = (from rt in wmsEntities.ReceiptTicket where rt.ID == receiptTicketID select rt).FirstOrDefault();
                if (receiptTicket == null)
                {
                    MessageBox.Show("该收货单不存在");
                    return;
                }
                else
                {
                    FormReceiptItem formReceiptItem = new FormReceiptItem(receiptTicketID);
                    formReceiptItem.SetCallBack(new Action(() =>
                    {
                        this.Search();
                    }));
                    formReceiptItem.Show();
                }
            }
            
            catch (Exception)
            {
                MessageBox.Show("无法连接到数据库，请查看网络连接!", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                return;
            }
        }

        private void ButtonToSubmission_Click(object sender, EventArgs e)
        {
            var worksheet = this.reoGridControlUser.Worksheets[0];
            try
            {
                WMSEntities wmsEntities = new WMSEntities();
                if (worksheet.SelectionRange.Rows != 1)
                {
                    MessageBox.Show("请选择一项进行修改", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                int receiptTicketID;
                try
                {
                    receiptTicketID = int.Parse(worksheet[worksheet.SelectionRange.Row, 0].ToString());
                }
                catch
                {
                    MessageBox.Show("请选择一项进行修改", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                ReceiptTicket receiptTicket = (from rt in wmsEntities.ReceiptTicket where rt.ID == receiptTicketID select rt).FirstOrDefault();
                if (receiptTicket == null)
                {
                    MessageBox.Show("该收货单不存在");
                    return;
                }
                else
                {
                    string key = "ReceiptTicketNo";
                    string name = (from r in ReceiptMetaData.receiptNameKeys where r.Key == key select r.Name).FirstOrDefault();
                    string value = receiptTicket.No;
                    ToSubmission(key, value);
                }
            }
            
            catch (Exception)
            {
                MessageBox.Show("无法连接到数据库，请查看网络连接!", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                return;
            }
        }

        private void ButtonToPutaway_Click(object sender, EventArgs e)
        {
            var worksheet = this.reoGridControlUser.Worksheets[0];
            try
            {
                WMSEntities wmsEntities = new WMSEntities();
                if (worksheet.SelectionRange.Rows != 1)
                {
                    MessageBox.Show("请选择一项进行修改", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                int receiptTicketID;
                try
                {
                    receiptTicketID = int.Parse(worksheet[worksheet.SelectionRange.Row, 0].ToString());
                }
                catch
                {
                    MessageBox.Show("请选择一项进行修改", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                ReceiptTicket receiptTicket = (from rt in wmsEntities.ReceiptTicket where rt.ID == receiptTicketID select rt).FirstOrDefault();
                if (receiptTicket == null)
                {
                    MessageBox.Show("该收货单不存在");
                    return;
                }
                else
                {
                    string key = "ReceiptTicketNo";
                    string name = (from r in ReceiptMetaData.receiptNameKeys where r.Key == key select r.Name).FirstOrDefault();
                    string value = receiptTicket.No;
                    ToPutaway(key, value);
                }
            }
           
            catch (Exception)
            {
                MessageBox.Show("无法连接到数据库，请查看网络连接!", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
