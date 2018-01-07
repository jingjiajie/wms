using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using unvell.ReoGrid;
using unvell.ReoGrid.Events;
using System.Threading;
using WMS.DataAccess;
using System.Data.SqlClient;
using unvell.ReoGrid.CellTypes;

namespace WMS.UI.FormReceipt
{
    public partial class FormReceiptSubmissionNew : Form
    {
        private int receiptTicketID;
        private FormMode formMode;
        private int submissionTicketID;
        private int checkBoxColumn = 1;
        private Action CallBack = null;
        private int countRow;
        private int userID;

        public FormReceiptSubmissionNew()
        {
            InitializeComponent();
        }

        /// <summary>
        /// formMode == ADD receiptTicketID ; formMode = ALTER submissionTicketID
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="formMode"></param>

        public FormReceiptSubmissionNew(int ID, int userID, FormMode formMode)
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
                this.submissionTicketID = ID;
            }
        }

        private void FormReceiptSubmissionNew_Load(object sender, EventArgs e)
        {
            InitComponents();
            Search();
            InitPanel();
        }

        private void InitComponents()
        {
            //初始化
            //string[] columnNames = (from kn in ReceiptMetaData.itemsKeyName select kn.Name).ToArray();
            //初始化表格
            var worksheet = this.reoGridControlUser.Worksheets[0];
            //worksheet.SelectionMode = WorksheetSelectionMode.Cell;
            int n = 0;
            for (int i = 0; i < ReceiptMetaData.itemsKeyName.Length + 1; i++)
            {
                if (i == this.checkBoxColumn)
                {
                    worksheet.ColumnHeaders[i].Text = "送检数量";
                }
                else
                {
                    worksheet.ColumnHeaders[i].Text = ReceiptMetaData.itemsKeyName[n].Name;
                    worksheet.ColumnHeaders[i].IsVisible = ReceiptMetaData.itemsKeyName[n].Visible;
                    n++;
                }
            }
            //worksheet.ColumnHeaders[columnNames.Length].Text = "是否送检";
            worksheet.Columns = ReceiptMetaData.itemsKeyName.Length + 1;
            worksheet.CellMouseEnter += ClickOnCell;
        }

        private void ClickOnCell(object sender, CellMouseEventArgs e)
        {
            var worksheet = this.reoGridControlUser.Worksheets[0];
            var position = e.CellPosition;
            if ((position.Col != this.checkBoxColumn && position.Row < countRow) || position.Row >= countRow)
            {
                worksheet.CreateAndGetCell(position).IsReadOnly = true;
            }
            else
            {
                
            }
        }


        private void InitPanel()
        {
            Utilities.CreateEditPanel(this.tableLayoutPanel2, ReceiptMetaData.submissionTicketKeyName);
            if (this.formMode == FormMode.ADD)
            {

            }
            else
            {
                WMSEntities wmsEntities = new WMSEntities();
                SubmissionTicketView submissionTicketView = (from stv in wmsEntities.SubmissionTicketView where stv.ID == this.submissionTicketID select stv).FirstOrDefault();
                if (submissionTicketView == null)
                {
                    MessageBox.Show("找不到此送检单");
                    return;
                }
                Utilities.CopyPropertiesToTextBoxes(submissionTicketView, this);
                Utilities.CopyPropertiesToComboBoxes(submissionTicketView, this);
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
                    //this.reoGridControlUser.Readonly = false;
                    var worksheet = this.reoGridControlUser.Worksheets[0];
                    worksheet.DeleteRangeData(RangePosition.EntireRange);
                    //int n = 0;
                    for (int i = 0; i < receiptTicketItemViews.Length; i++)
                    {
                        ReceiptTicketItemView curReceiptTicketItemView = receiptTicketItemViews[i];
                        object[] columns = Utilities.GetValuesByPropertieNames(curReceiptTicketItemView, (from kn in ReceiptMetaData.itemsKeyName select kn.Key).ToArray());

                        int m = 0;
                        for (int j = 0; j < worksheet.Columns - 1; j++)
                        {
                            if (j == this.checkBoxColumn)
                            {
                                //CheckBoxCell checkboxCell;
                                //TextBox textbox = new TextBox();
                                //DataGridViewTextBoxCell;
                                //worksheet[i, m]
                                m += 2;
                            }
                            else
                            {
                                //worksheet.CreateAndGetCell(i, m).IsReadOnly = true;
                                worksheet[i, m] = columns[j];
                                m++;
                            }
                        }
                    }
                }));

            })).Start();
        }


        public void SetCallBack(Action action)
        {
            this.CallBack = action;
        }

        private SortedDictionary<int, int> SelectReceiptTicketItem()
        {
            /*
            //List<ReceiptTicketItem> receiptTicketItems = new List<ReceiptTicketItem>();
            WMSEntities wmsEntities = new WMSEntities();
            var worksheet = this.reoGridControlUser.Worksheets[0];
            List<int> ids = new List<int>();
            bool result;
            for (int i = 0; i < this.countRow; i++)
            {
                result = (worksheet[i, this.checkBoxColumn] as bool?) ?? false;
                if (result == true)
                {
                    int id;
                    if (int.TryParse(worksheet[i, 0].ToString(), out id) == false)
                    {
                        MessageBox.Show(worksheet[i, 0].ToString() + "加入失败");
                    }
                    else
                    {
                        ReceiptTicketItem receiptTicketItem = (from rti in wmsEntities.ReceiptTicketItem where rti.ID == id select rti).FirstOrDefault();
                        if (receiptTicketItem.State == "已收货" || receiptTicketItem.State == "送检中")
                        {
                            MessageBox.Show(receiptTicketItem.ID + " " + receiptTicketItem.State);
                            continue;
                        }
                        else
                        {
                            ids.Add(receiptTicketItem.ID);
                        }
                    }
                }
            }*/
            List<int> ids = new List<int>();
            SortedDictionary<int, int> idsAndSubmissionAmount = new SortedDictionary<int, int>();
            var worksheet = this.reoGridControlUser.Worksheets[0];
            int submissionAmount;
            for (int i = 0; i < this.countRow; i++)
            {
                int id;
                string strSubmissionAmount = worksheet[i, this.checkBoxColumn].ToString();
                
                if (strSubmissionAmount == null)
                {
                    strSubmissionAmount = "0";
                }
                else
                {
                    if (int.TryParse(strSubmissionAmount, out submissionAmount) && int.TryParse(worksheet[i, 0].ToString(), out id))
                    {
                        idsAndSubmissionAmount.Add(id, submissionAmount);
                    }
                    else
                    {
                        MessageBox.Show("送检数量必须为数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return null;
                    }
                }
                /*
                if (result == true)
                {
                    int id;
                    if (int.TryParse(worksheet[i, 0].ToString(), out id) == false)
                    {
                        MessageBox.Show(worksheet[i, 0].ToString() + "加入失败");
                    }
                    else
                    {
                        ReceiptTicketItem receiptTicketItem = (from rti in wmsEntities.ReceiptTicketItem where rti.ID == id select rti).FirstOrDefault();
                        if (receiptTicketItem.State == "已收货" || receiptTicketItem.State == "送检中")
                        {
                            MessageBox.Show(receiptTicketItem.ID + " " + receiptTicketItem.State);
                            continue;
                        }
                        else
                        {
                            ids.Add(receiptTicketItem.ID);
                        }
                    }*/
            }

            return idsAndSubmissionAmount;
        }

        private void OK_Click(object sender, EventArgs e)
        {
            var worksheet = this.reoGridControlUser.Worksheets[0];
            worksheet.FocusPos = new CellPosition(0, 0);
            WMSEntities wmsEntities = new WMSEntities();

            SortedDictionary<int, int> idsAndSubmissionAmount = SelectReceiptTicketItem();
            if (idsAndSubmissionAmount == null)
            {
                return;
            }
            Dictionary<ReceiptTicketItem, int> receiptTicketItemsAndSubmissionAmount = new Dictionary<ReceiptTicketItem, int>();
            foreach (KeyValuePair<int, int> kv in idsAndSubmissionAmount)
            {
                ReceiptTicketItem receiptTicketItem = (from rti in wmsEntities.ReceiptTicketItem where rti.ID == kv.Key select rti).FirstOrDefault();
                if (receiptTicketItem != null)
                {
                    receiptTicketItemsAndSubmissionAmount.Add(receiptTicketItem, kv.Value);
                }
            }
            if (this.formMode == FormMode.ADD)
            {
                SubmissionTicket submissionTicket = new SubmissionTicket();
                string errorInfo;
                if (Utilities.CopyTextBoxTextsToProperties(this, submissionTicket, ReceiptMetaData.submissionTicketKeyName, out errorInfo) == false)
                {
                    MessageBox.Show(errorInfo);
                    return;
                }
                else
                {
                    Utilities.CopyComboBoxsToProperties(this, submissionTicket, ReceiptMetaData.submissionTicketKeyName);
                    ReceiptTicket receiptTicket = (from rt in wmsEntities.ReceiptTicket where rt.ID == this.receiptTicketID select rt).FirstOrDefault();
                    if (receiptTicket == null)
                    {
                        MessageBox.Show("收货单不存在");
                        return;
                    }
                    submissionTicket.CreateTime = DateTime.Now;
                    submissionTicket.ReceiptTicketID = this.receiptTicketID;
                    submissionTicket.CreateUserID = this.userID;
                    submissionTicket.LastUpdateTime = DateTime.Now;
                    submissionTicket.LastUpdateUserID = this.userID;
                    submissionTicket.ProjectID = receiptTicket.ProjectID;
                    submissionTicket.WarehouseID = receiptTicket.Warehouse;
                    submissionTicket.State = "待检";
                    wmsEntities.SubmissionTicket.Add(submissionTicket);

                    new Thread(() =>
                    {
                        try
                        {
                            wmsEntities.SaveChanges();
                            //submissionTicket.No = Utilities.GenerateTicketNo()

                            wmsEntities.SaveChanges();
                            foreach (KeyValuePair<ReceiptTicketItem, int> vp in receiptTicketItemsAndSubmissionAmount)
                            {
                                SubmissionTicketItem submissionTicketItem = new SubmissionTicketItem();
                                StockInfo stockInfo = (from si in wmsEntities.StockInfo where si.ReceiptTicketItemID == vp.Key.ID select si).FirstOrDefault();
                                if (stockInfo == null)
                                {
                                    MessageBox.Show("找不到对应的库存信息");
                                }
                                else
                                {

                                    if (stockInfo.ReceiptAreaAmount != null)
                                    {
                                        submissionTicketItem.ArriveAmount = stockInfo.ReceiptAreaAmount;
                                    }
                                    stockInfo.SubmissionAmount = vp.Value;
                                    stockInfo.ReceiptAreaAmount -= vp.Value;
                                    submissionTicketItem.ArriveAmount = vp.Key.ReceiviptAmount;
                                    submissionTicketItem.ReceiptTicketItemID = vp.Key.ID;
                                    //submissionTicketItem.RejectAmount = 0;
                                    //submissionTicketItem.ReturnAmount = 0;
                                    submissionTicketItem.State = "待检";
                                    vp.Key.State = "送检中";
                                    submissionTicketItem.SubmissionAmount = vp.Value;
                                    submissionTicketItem.SubmissionTicketID = submissionTicket.ID;
                                    wmsEntities.SubmissionTicketItem.Add(submissionTicketItem);
                                    receiptTicket.State = "送检中";
                                }
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
                            }
                            */
                            this.Invoke(new Action(() =>
                            {
                                this.Search();
                                CallBack();
                            }));
                            MessageBox.Show("收货单条目送检成功");
                        }
                        catch
                        {
                            MessageBox.Show("无法连接到数据库，请查看网络连接!", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                            return;
                        }
                    }).Start();
                }
            }
            else
            {

            }
        }


        private void OK_MouseEnter(object sender, EventArgs e)
        {
            buttonOK.BackgroundImage = WMS.UI.Properties.Resources.bottonB2_s;
        }

        private void OK_MouseLeave(object sender, EventArgs e)
        {
            buttonOK.BackgroundImage = WMS.UI.Properties.Resources.bottonB2_q;
        }

        private void OK_MouseDown(object sender, MouseEventArgs e)
        {
            buttonOK.BackgroundImage = WMS.UI.Properties.Resources.bottonB3_q;
        }

        private void buttonOK_MouseMove(object sender, MouseEventArgs e)
        {
            this.buttonOK.Focus();
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
    }
}
