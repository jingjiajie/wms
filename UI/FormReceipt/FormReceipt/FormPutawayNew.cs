using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using unvell.ReoGrid;
using unvell.ReoGrid.CellTypes;
using System.Data.SqlClient;
using System.Threading;
using WMS.DataAccess;
using unvell.ReoGrid.Events;

namespace WMS.UI.FormReceipt
{
    public partial class FormPutawayNew : Form
    {
        private int receiptTicketID;
        private FormMode formMode;
        private int putawayTicketID;
        private int editableColumn = 1;
        private Action CallBack = null;
        private int countRow;
        private int userID;
        private Func<int> PersonIDGetter = null;
        private KeyName[] KeyName;
        StandardImportForm<PutawayTicketItem> standardImportForm = null;

        public FormPutawayNew()
        {
            InitializeComponent();
        }

        public FormPutawayNew(int ID, int userID, FormMode formMode)
        {
            InitializeComponent();
            this.formMode = formMode;
            this.userID = userID;
            if (formMode == FormMode.ADD)
            {
                this.receiptTicketID = ID;
            }
            else
            {
                this.putawayTicketID = ID;
            }
        }

        private void FormPutawayNew_Load(object sender, EventArgs e)
        {
            this.KeyName = (from kn in ReceiptMetaData.itemsKeyName where kn.Key != "Component" select kn).ToArray();
            InitComponents();
            Search();
            InitPanel();
            WMSEntities wmsEntities = new WMSEntities();
            ReceiptTicket receiptTicket = (from rt in wmsEntities.ReceiptTicket where rt.ID == this.receiptTicketID select rt).FirstOrDefault();
            User user = (from u in wmsEntities.User where u.ID == this.userID select u).FirstOrDefault();
            if (user != null)
            {
                this.Controls.Find("textBoxCreateUserUsername", true)[0].Text = user.Username;
                this.Controls.Find("textBoxLastUpdateUserUsername", true)[0].Text = user.Username;
            }
            if (receiptTicket != null)
            {
                this.Controls.Find("textBoxReceiptTicketNo", true)[0].Text = receiptTicket.No;
            }
            this.Controls.Find("textBoxCreateTime", true)[0].Text = DateTime.Now.ToString();
            this.Controls.Find("textBoxLastUpdateTime", true)[0].Text = DateTime.Now.ToString();
            this.Controls.Find("textBoxState", true)[0].Text = "待上架";
        }

        private void InitComponents()
        {
            //初始化
            //string[] columnNames = (from kn in ReceiptMetaData.itemsKeyName select kn.Name).ToArray();
            //初始化表格
            var worksheet = this.reoGridControlUser.Worksheets[0];
            //worksheet.SelectionMode = WorksheetSelectionMode.Cell;
            int n = 0;
            for (int i = 0; i < this.KeyName.Length + 1; i++)
            {
                if (i == this.editableColumn)
                {
                    worksheet.ColumnHeaders[i].Text = "计划移位数量";
                    worksheet.ColumnHeaders[i].IsVisible = true;
                }
                else
                {
                    worksheet.ColumnHeaders[i].Text = this.KeyName[n].Name;
                    worksheet.ColumnHeaders[i].IsVisible = this.KeyName[n].Visible;
                    n++;
                }
            }
            //worksheet.ColumnHeaders[columnNames.Length].Text = "是否送检";
            worksheet.Columns = this.KeyName.Length + 1;
            worksheet.CellMouseEnter += ClickOnCell;
        }

        private void ClickOnCell(object sender, CellMouseEventArgs e)
        {
            var worksheet = this.reoGridControlUser.Worksheets[0];
            var position = e.CellPosition;
            if ((position.Col != this.editableColumn && position.Row < countRow) || position.Row >= countRow)
            {
                worksheet.CreateAndGetCell(position).IsReadOnly = true;
            }
            else
            {

            }
        }

        private void InitPanel()
        {
            Utilities.CreateEditPanel(this.tableLayoutPanel2, ReceiptMetaData.putawayTicketKeyName);

            this.PersonIDGetter = Utilities.BindTextBoxSelect<FormSelectPerson, Person>(this, "textBoxPersonName", "Name");
            if (this.formMode == FormMode.ADD)
            {

            }
            else
            {
                WMSEntities wmsEntities = new WMSEntities();
                PutawayTicketView putawayTicketView = (from stv in wmsEntities.PutawayTicketView where stv.ID == this.putawayTicketID select stv).FirstOrDefault();
                if (putawayTicketView == null)
                {
                    MessageBox.Show("找不到此上架单", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                Utilities.CopyPropertiesToTextBoxes(putawayTicketView, this);

            }
        }

        private void Search()
        {
            //this.labelStatus.Text = "搜索中...";

            new Thread(new ThreadStart(() =>
            {
                var wmsEntities = new WMSEntities();
                ReceiptTicketItemView[] receiptTicketItemViews = null;
                receiptTicketItemViews = wmsEntities.Database.SqlQuery<ReceiptTicketItemView>("SELECT * FROM ReceiptTicketItemView WHERE ReceiptTicketID = @receiptTicketID", new SqlParameter("receiptTicketID", this.receiptTicketID)).ToArray();
                this.countRow = receiptTicketItemViews.Length;
                this.reoGridControlUser.Invoke(new Action(() =>
                {
                    //this.labelStatus.Text = "搜索完成";
                    var worksheet = this.reoGridControlUser.Worksheets[0];
                    worksheet.DeleteRangeData(RangePosition.EntireRange);
                    int n = 0;
                    for (int i = 0; i < receiptTicketItemViews.Length; i++)
                    {
                        ReceiptTicketItemView curReceiptTicketItemView = receiptTicketItemViews[i];
                        object[] columns = Utilities.GetValuesByPropertieNames(curReceiptTicketItemView, (from kn in this.KeyName select kn.Key).ToArray());

                        int m = 0;
                        for (int j = 0; j < worksheet.Columns; j++)
                        {
                            if (j == this.editableColumn)
                            {
                                //worksheet[i, j]
                                worksheet.CreateAndGetCell(i, j).Style.BackColor = Color.AliceBlue;
                                //worksheet[i, j] = "0";
                            }
                            else
                            {
                                if (columns[m] != null)
                                {
                                    worksheet[i, j] = columns[m].ToString();
                                }
                                else
                                {
                                    worksheet[i, j] = columns[m];
                                }
                                m++;
                            }
                        }
                    }
                    /*
                    for (int i = 0; i < receiptTicketItemViews.Length; i++)
                    {
                        ReceiptTicketItemView curReceiptTicketItemView = receiptTicketItemViews[i];
                        object[] columns = Utilities.GetValuesByPropertieNames(curReceiptTicketItemView, (from kn in ReceiptMetaData.itemsKeyName select kn.Key).ToArray());

                        int m = 0;
                        for (int j = 0; j < worksheet.Columns - 1; j++)
                        {
                            if (j == this.checkBoxColumn)
                            {
                                CheckBoxCell checkboxCell;
                                worksheet[i, m] = new object[] { checkboxCell = new CheckBoxCell() };
                                m += 2;
                            }
                            else
                            {
                                worksheet[i, m] = columns[j];
                                m++;
                            }
                        }
                    }*/
                }));

            })).Start();
        }

        public void SetCallBack(Action action)
        {
            this.CallBack = action;
        }

        private SortedDictionary<int, decimal> SelectReceiptTicketItem()
        {
            //List<ReceiptTicketItem> receiptTicketItems = new List<ReceiptTicketItem>();
            /*
            WMSEntities wmsEntities = new WMSEntities();
            var worksheet = this.reoGridControlUser.Worksheets[0];
            SortedDictionary<int, decimal> ids = new SortedDictionary<int, decimal>();
            decimal result;
            for (int i = 0; i < this.countRow; i++)
            {
                //result = (worksheet[i, this.editableColumn] as decimal?) ?? -1;
                string strSubmissionAmount = worksheet[i, this.editableColumn] == null ? null : worksheet[i, this.editableColumn].ToString();

                int id;
                if (int.TryParse(worksheet[i, 0].ToString(), out id) == false)
                {
                    MessageBox.Show(worksheet[i, 0].ToString() + "加入失败");
                }
                else
                {
                    ReceiptTicketItem receiptTicketItem = (from rti in wmsEntities.ReceiptTicketItem where rti.ID == id select rti).FirstOrDefault();
                    if (receiptTicketItem.State != "已收货")
                    {
                        MessageBox.Show(receiptTicketItem.ID + " " + receiptTicketItem.State + " 请先收货");
                        continue;
                    }
                    else
                    {
                        if (worksheet[i, this.editableColumn] == null)
                        {
                            result = receiptTicketItem.UnitCount == null ? 0 : (int)receiptTicketItem.UnitCount;
                        }
                        else
                        {
                            result = (worksheet[i, this.editableColumn] as decimal?) ?? -1;
                        }
                        ids.Add(receiptTicketItem.ID, result);
                    }
                }
                */
            WMSEntities wmsEntities = new WMSEntities();
            List<int> ids = new List<int>();
            SortedDictionary<int, decimal> idsAndSubmissionAmount = new SortedDictionary<int, decimal>();
            var worksheet = this.reoGridControlUser.Worksheets[0];
            decimal submissionAmount;
            for (int i = 0; i < this.countRow; i++)
            {
                int id;
                string strSubmissionAmount = worksheet[i, this.editableColumn] == null ? null : worksheet[i, this.editableColumn].ToString();


                if (strSubmissionAmount == null)
                {
                    //strSubmissionAmount = "0";
                    continue;
                }
                else
                {
                    if (decimal.TryParse(strSubmissionAmount, out submissionAmount) && int.TryParse(worksheet[i, 0].ToString(), out id))
                    {
                        if (submissionAmount <= 0)
                        {
                            MessageBox.Show("上架数量必须大于0！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return null;
                        }
                        ReceiptTicketItem receiptTicketItem = (from rti in wmsEntities.ReceiptTicketItem where rti.ID == id select rti).FirstOrDefault();
                        if (receiptTicketItem == null)
                        {
                            MessageBox.Show("该收货单中，某收货单条目未找到，可能已被删除！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return null;
                        }
                        if ((receiptTicketItem.UnitCount == null ? 0 : (decimal)receiptTicketItem.UnitCount) - submissionAmount < (receiptTicketItem.HasPutwayAmount == null ? 0 : (decimal)receiptTicketItem.HasPutwayAmount))
                        {
                            MessageBox.Show("上架失败，计划上架数量不能多于收货数量！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                            return null;
                        }

                        idsAndSubmissionAmount.Add(id, submissionAmount);
                    }
                    else
                    {
                        MessageBox.Show("送检数量必须为数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return null;
                    }
                }
            }
            return idsAndSubmissionAmount;
        }

        private void OK_Click(object sender, EventArgs e)
        {
            WMSEntities wmsEntities = new WMSEntities();

            /*
            List<int> ids = SelectReceiptTicketItem();
            if (ids.Count == 0)
            {
                MessageBox.Show("请选择您要上架的零件");
                return;
            }
            List<ReceiptTicketItem> receiptTicketItems = new List<ReceiptTicketItem>();
            foreach (int id in ids)
            {
                ReceiptTicketItem receiptTicketItem = (from rti in wmsEntities.ReceiptTicketItem where rti.ID == id select rti).FirstOrDefault();
                if (receiptTicketItem != null)
                {
                    receiptTicketItems.Add(receiptTicketItem);
                }
            }
            */
            SortedDictionary<int, decimal> receiptItemPutawayAmount = this.SelectReceiptTicketItem();
            if (receiptItemPutawayAmount == null)
            {
                return;
            }
            if (this.formMode == FormMode.ADD)
            {
                PutawayTicket putawayTicket = new PutawayTicket();
                string errorInfo;
                if (receiptItemPutawayAmount.Count == 0)
                {
                    MessageBox.Show("至少选中一条上架！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (Utilities.CopyTextBoxTextsToProperties(this, putawayTicket, ReceiptMetaData.putawayTicketKeyName, out errorInfo) == false)
                {
                    MessageBox.Show(errorInfo, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    ReceiptTicket receiptTicket = (from rt in wmsEntities.ReceiptTicket where rt.ID == this.receiptTicketID select rt).FirstOrDefault();
                    if (receiptTicket == null)
                    {
                        MessageBox.Show("收货单不存在", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (receiptTicket.HasPutawayTicket == "没有生成上架单")
                    {
                        if (MessageBox.Show("生成上架单后，该收货单条目、状态都不能更改，是否继续", "提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification) == DialogResult.No)
                        {
                            return;
                        }
                    }
                    //receiptTicket.HasPutawayTicket = "是";
                    putawayTicket.CreateTime = DateTime.Now;
                    putawayTicket.ReceiptTicketID = this.receiptTicketID;
                    putawayTicket.CreateUserID = this.userID;
                    putawayTicket.LastUpdateTime = DateTime.Now;
                    putawayTicket.LastUpdateUserID = this.userID;
                    putawayTicket.ProjectID = receiptTicket.ProjectID;
                    putawayTicket.WarehouseID = receiptTicket.WarehouseID;
                    putawayTicket.State = "待上架";
                    putawayTicket.PersonID = this.PersonIDGetter();
                    wmsEntities.PutawayTicket.Add(putawayTicket);
                    new Thread(() =>
                    {
                        try
                        {
                            wmsEntities.SaveChanges();
                            //putawayTicket.No = Utilities.GenerateNo("P", putawayTicket.ID);

                            ////////////////////////////
                            if (string.IsNullOrWhiteSpace(putawayTicket.No))
                            {
                                if (putawayTicket.CreateTime.HasValue == false)
                                {
                                    MessageBox.Show("单号生成失败（未知创建日期）！请手动填写单号", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }

                                DateTime createDay = new DateTime(putawayTicket.CreateTime.Value.Year, putawayTicket.CreateTime.Value.Month, putawayTicket.CreateTime.Value.Day);
                                DateTime nextDay = createDay.AddDays(1);
                                int maxRankOfToday = Utilities.GetMaxTicketRankOfDay((from s in wmsEntities.PutawayTicket
                                                                                      where s.CreateTime >= createDay && s.CreateTime < nextDay
                                                                                      select s.No).ToArray());
                                if (maxRankOfToday == -1)
                                {
                                    MessageBox.Show("单号生成失败！请手动填写单号", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                                putawayTicket.No = Utilities.GenerateTicketNo("P", putawayTicket.CreateTime.Value, maxRankOfToday + 1);
                            }

                            ///////////////////////////////////////////////////////////////

                            wmsEntities.SaveChanges();
                            foreach (KeyValuePair<int, decimal> ripa in receiptItemPutawayAmount)
                            {
                                ReceiptTicketItem receiptTicketItem = (from rti in wmsEntities.ReceiptTicketItem where rti.ID == ripa.Key select rti).FirstOrDefault();
                                if (ripa.Value < 0)
                                {
                                    MessageBox.Show("计划上架数量不能是负数", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                                if (receiptTicketItem != null)
                                {
                                    PutawayTicketItem putawayTicketItem = new PutawayTicketItem();
                                    putawayTicketItem.ReceiptTicketItemID = receiptTicketItem.ID;
                                    putawayTicketItem.State = "待上架";
                                    putawayTicketItem.ScheduledMoveCount = ripa.Value;
                                    if ((receiptTicketItem.UnitCount == null ? 0 : (decimal)receiptTicketItem.UnitCount) - putawayTicketItem.ScheduledMoveCount < (receiptTicketItem.HasPutwayAmount == null ? 0 : (decimal)receiptTicketItem.HasPutwayAmount))
                                    {
                                        MessageBox.Show("上架失败，计划上架数量不能多于收货数量！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                                        return;
                                    }
                                    if (receiptTicketItem.HasPutwayAmount == null)
                                    {
                                        receiptTicketItem.HasPutwayAmount = 0;
                                    }

                                    if (putawayTicketItem.ScheduledMoveCount != null)
                                    {
                                        receiptTicketItem.HasPutwayAmount += putawayTicketItem.ScheduledMoveCount;
                                    }

                                    //receiptTicketItem.HasPutwayAmount += putawayTicketItem.ScheduledMoveCount;
                                    //putawayTicketItem.HasPutawayAmount += ripa.Value;
                                    putawayTicketItem.PutawayTicketID = putawayTicket.ID;
                                    putawayTicketItem.Unit = receiptTicketItem.Unit;
                                    putawayTicketItem.UnitAmount = receiptTicketItem.UnitAmount;
                                    //if (putawayTicketItem.UnitCount)
                                    wmsEntities.PutawayTicketItem.Add(putawayTicketItem);
                                }
                            }
                            /*
                            foreach (ReceiptTicketItem rti in receiptTicket.ReceiptTicketItem)
                            {
                                PutawayTicketItem putawayTicketItem = new PutawayTicketItem();
                                putawayTicketItem.ReceiptTicketItemID = rti.ID;
                                putawayTicketItem.State = "待上架";
                                putawayTicketItem.PutawayTicketID = putawayTicket.ID;
                                ReceiptTicketItem receiptTicketItem = (from rtii in wmsEntities.ReceiptTicketItem where rtii.ID == putawayTicketItem.ReceiptTicketItemID select rtii).FirstOrDefault();
                                if (receiptTicketItem != null)
                                {
                                    putawayTicketItem.Unit = receiptTicketItem.Unit;
                                    putawayTicketItem.UnitAmount = receiptTicketItem.UnitAmount;
                                }
                               
                                //StockInfo stockInfo = (from si in wmsEntities.StockInfo where si.ReceiptTicketItemID == rti.ID select si).FirstOrDefault();
                                //if (stockInfo != null)
                                //{
                                //    stockInfo.ShipmentAreaAmount += stockInfo.OverflowAreaAmount;
                                //    stockInfo.OverflowAreaAmount -= stockInfo.OverflowAreaAmount;
                                //}
                                wmsEntities.PutawayTicketItem.Add(putawayTicketItem);
                            }*/
                            wmsEntities.SaveChanges();
                            int n = 0;
                            foreach (ReceiptTicketItem rti in receiptTicket.ReceiptTicketItem)
                            {
                                if (rti.HasPutwayAmount == rti.UnitCount)
                                {
                                    n++;
                                }
                            }
                            if (n == receiptTicket.ReceiptTicketItem.ToArray().Length)
                            {
                                receiptTicket.HasPutawayTicket = "全部生成上架单";
                            }
                            else
                            {
                                receiptTicket.HasPutawayTicket = "部分生成上架单";
                            }
                            wmsEntities.SaveChanges();
                            /*
                            int count = wmsEntities.Database.SqlQuery<int>(
                                "SELECT COUNT(*) FROM ReceiptTicketItem " +
                                "WHERE ReceiptTicketID = @receiptTicketID AND State <> '送检中'",
                                new SqlParameter("receiptTicketID", receiptTicketID)).FirstOrDefault();
                            if (count == 0)
                            {
                                wmsEntities.Database.ExecuteSqlCommand(
                                    "UPDATE ReceiptTicket SET State='送检中' " +
                                    "WHERE ID = @receiptTicketID",
                                    new SqlParameter("receiptTicketID", receiptTicketID));
                            }
                            else
                            {
                                wmsEntities.Database.ExecuteSqlCommand(
                                    "UPDATE ReceiptTicket SET State='部分送检中' " +
                                    "WHERE ID = @receiptTicketID",
                                    new SqlParameter("receiptTicketID", receiptTicketID));
                            }*/
                            this.Invoke(new Action(() =>
                            {
                                this.Search();
                                CallBack();
                                this.Hide();
                            }));
                            MessageBox.Show("成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch
                        {
                            MessageBox.Show("无法连接到数据库，请查看网络连接!", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                            return;
                        }
                    }).Start();
                }
            }
        }

        private void OK_MouseEnter(object sender, EventArgs e)
        {
            OK.BackgroundImage = WMS.UI.Properties.Resources.bottonB2_s;
        }

        private void OK_MouseLeave(object sender, EventArgs e)
        {
            OK.BackgroundImage = WMS.UI.Properties.Resources.bottonB2_q;
        }

        private void OK_MouseDown(object sender, MouseEventArgs e)
        {
            OK.BackgroundImage = WMS.UI.Properties.Resources.bottonB3_q;
        }

        private void buttonImport_MouseEnter(object sender, EventArgs e)
        {
            buttonImport.BackgroundImage = WMS.UI.Properties.Resources.bottonB2_s;
        }

        private void buttonImport_MouseLeave(object sender, EventArgs e)
        {
            buttonImport.BackgroundImage = WMS.UI.Properties.Resources.bottonB2_q;
        }

        private void buttonImport_MouseDown(object sender, MouseEventArgs e)
        {
            buttonImport.BackgroundImage = WMS.UI.Properties.Resources.bottonB3_q;
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.BackgroundImage = WMS.UI.Properties.Resources.bottonB2_s;
        }
        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackgroundImage = WMS.UI.Properties.Resources.bottonB2_q;
        }

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            button1.BackgroundImage = WMS.UI.Properties.Resources.bottonB3_q;
        }

        private void OK_MouseMove(object sender, MouseEventArgs e)
        {
            this.OK.Focus();
            this.reoGridControlUser.Worksheets[0].FocusPos = new CellPosition(0, 0);
            var worksheet = this.reoGridControlUser.Worksheets[0];
            worksheet.EndEdit(new EndEditReason());
            //worksheet.CreateAndGetCell(worksheet.GetEditingCell()).EndEdit();
            /*
            for (int i = 0; i < this.countRow; i++)
            {
                worksheet.Cells[i, this.checkBoxColumn].EndEdit();
            }
            */
            //worksheet.EditingCell.EndEdit();
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            this.standardImportForm = new StandardImportForm<PutawayTicketItem>(ReceiptMetaData.importPutawayTicket, importItemHandler, importFinishedCallback, "导入收货单条目");
            this.standardImportForm.ShowDialog();
        }

        private bool importItemHandler(List<PutawayTicketItem> results, Dictionary<string, string[]> unimportedColumns)
        {
            WMSEntities wmsEntities = new WMSEntities();
            string[] supplyNoNames = (from s in unimportedColumns where s.Key == "Component" select s.Value).FirstOrDefault();
            string[] jobPersonNames = (from s in unimportedColumns where s.Key == "JobPersonName" select s.Value).FirstOrDefault();
            string[] confirmPersonNames = (from s in unimportedColumns where s.Key == "ConfirmPersonName" select s.Value).FirstOrDefault();
            List<PutawayTicketItem> putawayTicketItemList = new List<PutawayTicketItem>();
            List<int> supplyIDs = new List<int>();
            for (int i = 0; i < results.Count; i++)
            {
                string supplyNoName = supplyNoNames[i];
                if (supplyNoName == null)
                {
                    MessageBox.Show("第" + (i + 1).ToString() + "行中，没有填写零件编号/名称！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                ReceiptTicketItemView receiptTicketItemViewNo = (from sv in wmsEntities.ReceiptTicketItemView where sv.SupplyNo == supplyNoName && sv.ReceiptTicketID == this.receiptTicketID select sv).FirstOrDefault();
                if (receiptTicketItemViewNo == null)
                {
                    ReceiptTicketItemView[] ReceiptTicketItemViewName = (from sv in wmsEntities.ReceiptTicketItemView where sv.ComponentName == supplyNoName && sv.ReceiptTicketID == this.receiptTicketID select sv).ToArray();
                    if (ReceiptTicketItemViewName.Length == 0)
                    {
                        MessageBox.Show("第" + (i + 1).ToString() + "行中，无法无法找到该供应商提供的该零件！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    else if (ReceiptTicketItemViewName.Length > 1)
                    {
                        MessageBox.Show("第" + (i + 1).ToString() + "行中，有多个零件重名，请输入零件编号！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    else
                    {
                        results[i].ReceiptTicketItemID = ReceiptTicketItemViewName[0].ID;
                    }
                }
                else
                {
                    results[i].ReceiptTicketItemID = receiptTicketItemViewNo.ID;
                }
                if (results[i].ReceiptTicketItemID == null)
                {
                    MessageBox.Show("第" + (i + 1) + "行，无法在该收货单中找到该零件！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                int receiptTicketItemID = (int)results[i].ReceiptTicketItemID;
                ReceiptTicketItem receiptTicketItem = (from rti in wmsEntities.ReceiptTicketItem where rti.ID == receiptTicketItemID select rti).FirstOrDefault();
                PutawayTicketItem result = results[i];
                if (receiptTicketItem == null)
                {
                    MessageBox.Show("第" + (i + 1) + "行中，无法找到该收货单条目！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                string jobPersonName = jobPersonNames[i];
                string confirmPersonName = confirmPersonNames[i];
                if (jobPersonName == "" || jobPersonName == null)
                {
                    results[i].JobPersonID = -1;
                }
                else
                {
                    PersonView personView = (from p in wmsEntities.PersonView where p.Name == jobPersonName select p).FirstOrDefault();
                    if (personView == null)
                    {
                        MessageBox.Show("第" + (i + 1).ToString() + "中，没有“" + jobPersonName + "”这个人", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    results[i].JobPersonID = personView.ID;
                }
                if (confirmPersonName == "" || confirmPersonName == null)
                {
                    results[i].ConfirmPersonID = -1;
                }
                else
                {
                    PersonView personView = (from p in wmsEntities.PersonView where p.Name == confirmPersonName select p).FirstOrDefault();
                    if (personView == null)
                    {
                        MessageBox.Show("第" + (i + 1).ToString() + "中，没有“" + confirmPersonName + "”这个人", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    results[i].ConfirmPersonID = personView.ID;
                }

                results[i].State = "待上架";
                if ((receiptTicketItem.UnitCount == null ? 0 : (decimal)receiptTicketItem.UnitCount) - results[i].ScheduledMoveCount < (receiptTicketItem.HasPutwayAmount == null ? 0 : (decimal)receiptTicketItem.HasPutwayAmount))
                {
                    MessageBox.Show("第" + (i + 1) + "行中，计划上架数量不能多于收货数量！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                    return false;
                }
                if (receiptTicketItem.HasPutwayAmount == null)
                {
                    receiptTicketItem.HasPutwayAmount = 0;
                }

                if (results[i].ScheduledMoveCount != null)
                {
                    receiptTicketItem.HasPutwayAmount += results[i].ScheduledMoveCount;
                }
                else
                {
                    MessageBox.Show("第" + (i + 1) + "行中，没有填写计划上架数量！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                //receiptTicketItem.HasPutwayAmount += putawayTicketItem.ScheduledMoveCount;
                //putawayTicketItem.HasPutawayAmount += ripa.Value;
                //results[i].PutawayTicketID = putawayTicket.ID;
                results[i].Unit = receiptTicketItem.Unit;
                results[i].UnitAmount = receiptTicketItem.UnitAmount;
                //if (putawayTicketItem.UnitCount)
                putawayTicketItemList.Add(results[i]);
                wmsEntities.PutawayTicketItem.Add(results[i]);
            }
            PutawayTicket putawayTicket = CreatePutawayTicket();
            foreach (PutawayTicketItem pti in putawayTicketItemList)
            {
                pti.PutawayTicketID = putawayTicket.ID;
            }
            wmsEntities.SaveChanges();
            ReceiptTicket receiptTicket = (from rt in wmsEntities.ReceiptTicket where rt.ID == this.receiptTicketID select rt).FirstOrDefault();
            if (receiptTicket != null)
            {
                int n = 0;
                foreach (ReceiptTicketItem rti in receiptTicket.ReceiptTicketItem)
                {
                    if (rti.HasPutwayAmount == rti.UnitCount)
                    {
                        n++;
                    }
                }
                if (n == receiptTicket.ReceiptTicketItem.ToArray().Length)
                {
                    receiptTicket.HasPutawayTicket = "全部生成上架单";
                }
                else
                {
                    receiptTicket.HasPutawayTicket = "部分生成上架单";
                }
            }
            wmsEntities.SaveChanges();
            this.Invoke(new Action(() =>
            {
                this.Search();
                MessageBox.Show("导入成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.standardImportForm.Close();
                CallBack();
            }));
            return false;
        }

        private void importFinishedCallback()
        {

        }

        private PutawayTicket CreatePutawayTicket()
        {
            WMSEntities wmsEntities = new WMSEntities();
            PutawayTicket putawayTicket = new PutawayTicket();
            ReceiptTicket receiptTicket = (from rt in wmsEntities.ReceiptTicket where rt.ID == this.receiptTicketID select rt).FirstOrDefault();
            string errorMessage;
            if (Utilities.CopyTextBoxTextsToProperties(this, putawayTicket, ReceiptMetaData.putawayTicketKeyName, out errorMessage) == false)
            {
                MessageBox.Show(errorMessage, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
            if (Utilities.CopyComboBoxsToProperties(this, putawayTicket, ReceiptMetaData.putawayTicketKeyName) == false)
            {
                MessageBox.Show("获取下拉框信息失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
            if (receiptTicket == null)
            {
                MessageBox.Show("没有找到该收货单，请刷新后查看！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
            putawayTicket.CreateTime = DateTime.Now;
            putawayTicket.ReceiptTicketID = this.receiptTicketID;
            putawayTicket.CreateUserID = this.userID;
            putawayTicket.LastUpdateTime = DateTime.Now;
            putawayTicket.LastUpdateUserID = this.userID;
            putawayTicket.ProjectID = receiptTicket.ProjectID;
            putawayTicket.WarehouseID = receiptTicket.WarehouseID;
            putawayTicket.State = "待上架";
            putawayTicket.PersonID = this.PersonIDGetter();
            wmsEntities.PutawayTicket.Add(putawayTicket);
            wmsEntities.SaveChanges();
            try
            {
                wmsEntities.SaveChanges();
                //putawayTicket.No = Utilities.GenerateNo("P", putawayTicket.ID);

                ////////////////////////////
                if (string.IsNullOrWhiteSpace(putawayTicket.No))
                {
                    if (putawayTicket.CreateTime.HasValue == false)
                    {
                        MessageBox.Show("单号生成失败（未知创建日期）！请手动填写单号", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return putawayTicket;
                    }

                    DateTime createDay = new DateTime(putawayTicket.CreateTime.Value.Year, putawayTicket.CreateTime.Value.Month, putawayTicket.CreateTime.Value.Day);
                    DateTime nextDay = createDay.AddDays(1);
                    int maxRankOfToday = Utilities.GetMaxTicketRankOfDay((from s in wmsEntities.PutawayTicket
                                                                          where s.CreateTime >= createDay && s.CreateTime < nextDay
                                                                          select s.No).ToArray());
                    if (maxRankOfToday == -1)
                    {
                        MessageBox.Show("单号生成失败！请手动填写单号", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return putawayTicket;
                    }
                    putawayTicket.No = Utilities.GenerateTicketNo("P", putawayTicket.CreateTime.Value, maxRankOfToday + 1);
                }

                ///////////////////////////////////////////////////////////////

                wmsEntities.SaveChanges();
                return putawayTicket;
            }
            catch
            {
                MessageBox.Show("无法连接到数据库，请查看网络连接!", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                return null;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WMSEntities wmsEntities = new WMSEntities();
            ReceiptTicket receiptTicket = (from rti in wmsEntities.ReceiptTicket where rti.ID == this.receiptTicketID select rti).FirstOrDefault();
            if (receiptTicket == null)
            {
                MessageBox.Show("该收货单已被删除，请刷新后查看！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            List<PutawayTicketItem> putawayTicketItemList = new List<PutawayTicketItem>();
            foreach (ReceiptTicketItem rti in receiptTicket.ReceiptTicketItem)
            {
                if (rti.HasPutwayAmount < rti.UnitCount || rti.HasPutwayAmount == null)
                {
                    PutawayTicketItem putawayTicketItem = new PutawayTicketItem();
                    
                    putawayTicketItem.ReceiptTicketItemID = rti.ID;
                    putawayTicketItem.ScheduledMoveCount = rti.UnitCount - (rti.HasPutwayAmount == null ? 0 : (decimal)rti.HasPutwayAmount);
                    rti.HasPutwayAmount = rti.UnitCount;
                    putawayTicketItem.State = "待上架";
                    putawayTicketItem.UnitCount = rti.UnitCount;
                    putawayTicketItem.UnitAmount = rti.UnitAmount;
                    putawayTicketItemList.Add(putawayTicketItem);
                    wmsEntities.PutawayTicketItem.Add(putawayTicketItem);
                }
            }
            if (putawayTicketItemList.Count != 0)
            {
                PutawayTicket putawayTicket = new PutawayTicket();
                string errorMessage;
                if (Utilities.CopyTextBoxTextsToProperties(this, putawayTicket, ReceiptMetaData.putawayTicketKeyName, out errorMessage) == false)
                {
                    MessageBox.Show(errorMessage, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                Utilities.CopyComboBoxsToProperties(this, putawayTicket, ReceiptMetaData.putawayTicketKeyName);
                putawayTicket.CreateTime = DateTime.Now;
                putawayTicket.ReceiptTicketID = this.receiptTicketID;
                putawayTicket.CreateUserID = this.userID;
                putawayTicket.LastUpdateTime = DateTime.Now;
                putawayTicket.LastUpdateUserID = this.userID;
                putawayTicket.ProjectID = receiptTicket.ProjectID;
                putawayTicket.WarehouseID = receiptTicket.WarehouseID;
                putawayTicket.State = "待上架";
                putawayTicket.PersonID = this.PersonIDGetter();
                receiptTicket.HasPutawayTicket = "全部生成上架单";
                wmsEntities.PutawayTicket.Add(putawayTicket);
                new Thread(() =>
                {
                    try
                    {
                        wmsEntities.SaveChanges();
                        //putawayTicket.No = Utilities.GenerateNo("P", putawayTicket.ID);

                        ////////////////////////////
                        if (string.IsNullOrWhiteSpace(putawayTicket.No))
                        {
                            if (putawayTicket.CreateTime.HasValue == false)
                            {
                                MessageBox.Show("单号生成失败（未知创建日期）！请手动填写单号", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            DateTime createDay = new DateTime(putawayTicket.CreateTime.Value.Year, putawayTicket.CreateTime.Value.Month, putawayTicket.CreateTime.Value.Day);
                            DateTime nextDay = createDay.AddDays(1);
                            int maxRankOfToday = Utilities.GetMaxTicketRankOfDay((from s in wmsEntities.PutawayTicket
                                                                                  where s.CreateTime >= createDay && s.CreateTime < nextDay
                                                                                  select s.No).ToArray());
                            if (maxRankOfToday == -1)
                            {
                                MessageBox.Show("单号生成失败！请手动填写单号", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            putawayTicket.No = Utilities.GenerateTicketNo("P", putawayTicket.CreateTime.Value, maxRankOfToday + 1);
                        }

                        ///////////////////////////////////////////////////////////////
                        foreach(PutawayTicketItem pti in putawayTicketItemList)
                        {
                            pti.PutawayTicketID = putawayTicket.ID;
                        }
                        wmsEntities.SaveChanges();
                        this.Invoke(new Action(() =>
                        {
                            this.Search();
                            this.CallBack();
                            MessageBox.Show("成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }));
                    }
                    catch
                    {
                        MessageBox.Show("无法连接到数据库，请查看网络连接!", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                        return;
                    }
                }).Start();

            }

        }


    }
}
