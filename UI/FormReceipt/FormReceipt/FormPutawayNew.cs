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

namespace WMS.UI.FormReceipt
{
    public partial class FormPutawayNew : Form
    {
        private int receiptTicketID;
        private FormMode formMode;
        private int putawayTicketID;
        private int checkBoxColumn = 1;
        private Action CallBack = null;
        private int countRow;
        private int userID;

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
            worksheet.SelectionMode = WorksheetSelectionMode.Row;
            int n = 0;
            for (int i = 0; i < ReceiptMetaData.itemsKeyName.Length; i++)
            {
                worksheet.ColumnHeaders[i].Text = ReceiptMetaData.itemsKeyName[i].Name;
                worksheet.ColumnHeaders[i].IsVisible = ReceiptMetaData.itemsKeyName[i].Visible;
            }
            //worksheet.ColumnHeaders[columnNames.Length].Text = "是否送检";
            worksheet.Columns = ReceiptMetaData.itemsKeyName.Length;
        }

        private void InitPanel()
        {
            Utilities.CreateEditPanel(this.tableLayoutPanel2, ReceiptMetaData.putawayTicketKeyName);
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
                    //int n = 0;
                    for (int i = 0; i < receiptTicketItemViews.Length; i++)
                    {
                        ReceiptTicketItemView curReceiptTicketItemView = receiptTicketItemViews[i];
                        object[] columns = Utilities.GetValuesByPropertieNames(curReceiptTicketItemView, (from kn in ReceiptMetaData.itemsKeyName select kn.Key).ToArray());

                        int m = 0;
                        for (int j = 0; j < worksheet.Columns - 1; j++)
                        {
                            worksheet[i, j] = columns[j];
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

        private List<int> SelectReceiptTicketItem()
        {
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
                        if (receiptTicketItem.State != "已收货")
                        {
                            MessageBox.Show(receiptTicketItem.ID + " " + receiptTicketItem.State + " 请先收货");
                            continue;
                        }
                        else
                        {
                            ids.Add(receiptTicketItem.ID);
                        }
                    }
                }
            }
            return ids;
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
                    putawayTicket.WarehouseID = receiptTicket.Warehouse;
                    putawayTicket.State = "待上架";
                    wmsEntities.PutawayTicket.Add(putawayTicket);
                    new Thread(() =>
                    {
                        try
                        {
                            wmsEntities.SaveChanges();
                            putawayTicket.No = Utilities.GenerateNo("P", putawayTicket.ID);
                            wmsEntities.SaveChanges();
                            foreach (ReceiptTicketItem rti in receiptTicket.ReceiptTicketItem)
                            {
                                PutawayTicketItem putawayTicketItem = new PutawayTicketItem();
                                putawayTicketItem.ReceiptTicketItemID = rti.ID;
                                putawayTicketItem.State = "待上架";
                                putawayTicketItem.PutawayTicketID = putawayTicket.ID;
                                wmsEntities.PutawayTicketItem.Add(putawayTicketItem);
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

    }
}
