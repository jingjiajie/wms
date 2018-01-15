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
                if (i == this.editableColumn)
                {
                    worksheet.ColumnHeaders[i].Text = "计划移位数量";
                    worksheet.ColumnHeaders[i].IsVisible = true;
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
                    MessageBox.Show("找不到此上架单");
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
                        object[] columns = Utilities.GetValuesByPropertieNames(curReceiptTicketItemView, (from kn in ReceiptMetaData.itemsKeyName select kn.Key).ToArray());

                        int m = 0;
                        for (int j = 0; j < worksheet.Columns; j++)
                        {
                            if (j == this.editableColumn)
                            {
                                //worksheet[i, j]

                            }
                            else
                            {
                                worksheet[i, j] = columns[m];
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
            if (this.formMode == FormMode.ADD)
            {
                PutawayTicket putawayTicket = new PutawayTicket();
                string errorInfo;
                if (Utilities.CopyTextBoxTextsToProperties(this, putawayTicket, ReceiptMetaData.putawayTicketKeyName, out errorInfo) == false)
                {
                    MessageBox.Show(errorInfo);
                    return;
                }
                else
                {
                    ReceiptTicket receiptTicket = (from rt in wmsEntities.ReceiptTicket where rt.ID == this.receiptTicketID select rt).FirstOrDefault();
                    if (receiptTicket == null)
                    {
                        MessageBox.Show("收货单不存在");
                        return;
                    }
                    receiptTicket.HasPutawayTicket = "是";
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
                                    MessageBox.Show("单号生成失败（未知创建日期）！请手动填写单号");
                                    return;
                                }

                                DateTime createDay = new DateTime(putawayTicket.CreateTime.Value.Year, putawayTicket.CreateTime.Value.Month, putawayTicket.CreateTime.Value.Day);
                                DateTime nextDay = createDay.AddDays(1);
                                int maxRankOfToday = Utilities.GetMaxTicketRankOfDay((from s in wmsEntities.PutawayTicket
                                                                                      where s.CreateTime >= createDay && s.CreateTime < nextDay
                                                                                      select s.No).ToArray());
                                if (maxRankOfToday == -1)
                                {
                                    MessageBox.Show("单号生成失败！请手动填写单号");
                                    return;
                                }
                                putawayTicket.No = Utilities.GenerateTicketNo("P", putawayTicket.CreateTime.Value, maxRankOfToday + 1);
                            }

                            ///////////////////////////////////////////////////////////////

                            wmsEntities.SaveChanges();
                            foreach (KeyValuePair<int ,decimal> ripa in receiptItemPutawayAmount)
                            {
                                ReceiptTicketItem receiptTicketItem = (from rti in wmsEntities.ReceiptTicketItem where rti.ID == ripa.Key select rti).FirstOrDefault();
                                if (receiptTicketItem != null)
                                {
                                    PutawayTicketItem putawayTicketItem = new PutawayTicketItem();
                                    putawayTicketItem.ReceiptTicketItemID = receiptTicketItem.ID;
                                    putawayTicketItem.State = "待上架";
                                    putawayTicketItem.ScheduledMoveCount = ripa.Value;
                                    if ((receiptTicketItem.UnitCount == null ? 0 : (decimal)receiptTicketItem.UnitCount) - putawayTicketItem.ScheduledMoveCount < (receiptTicketItem.HasPutwayAmount == null ? 0 : (decimal)receiptTicketItem.HasPutwayAmount))
                                    {
                                        MessageBox.Show("上架失败，计划上架数量过多！","提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
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
                            foreach(ReceiptTicketItem rti in receiptTicket.ReceiptTicketItem)
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
                            MessageBox.Show("成功");
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
    

    }
}
