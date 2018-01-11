using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WMS.DataAccess;
using unvell.ReoGrid;
using System.Threading;
using System.Data.SqlClient;

namespace WMS.UI.FormReceipt
{
    public partial class FormShelvesItem : Form
    {
        private int projectID;
        private int warehouseID;
        private int putawayTicketItemID;
        private int putawayTicketID;
        WMSEntities wmsEntities = new WMSEntities();
        private Action CallBack = null;
        public FormShelvesItem()
        {
            InitializeComponent();
        }

        public FormShelvesItem(int putawayTicketID)
        {
            InitializeComponent();
            this.putawayTicketID = putawayTicketID;
        }

        public FormShelvesItem(int projectID, int warehouseID)
        {
            InitializeComponent();
            this.projectID = projectID;
            this.warehouseID = warehouseID;
        }

        public void SetCallBack(Action action)
        {
            this.CallBack = action;
        }

        private void FormShelvesItem_Load(object sender, EventArgs e)
        {
            InitComponents();
            InitPanel();
            WMSEntities wmsEntities = new WMSEntities();

            Search();
        }

        private void InitPanel()
        {
            WMSEntities wmsEntities = new WMSEntities();
            //this.Controls.Clear();
            Utilities.CreateEditPanel(this.tableLayoutPanelProperties, ReceiptMetaData.putawayTicketItemKeyName);

            this.reoGridControlPutaway.Worksheets[0].SelectionRangeChanged += worksheet_SelectionRangeChanged;

            //TextBox textBoxComponentName = (TextBox)this.Controls.Find("textBoxComponentName", true)[0];
            //textBoxComponentName.Click += textBoxComponentName_Click;
            //textBoxComponentName.ReadOnly = true;
            //textBoxComponentName.BackColor = Color.White;
        }

        private void worksheet_SelectionRangeChanged(object sender, unvell.ReoGrid.Events.RangeEventArgs e)
        {
            this.RefreshTextBoxes();
        }

        private void RefreshTextBoxes()
        {
            this.ClearTextBoxes();
            var worksheet = this.reoGridControlPutaway.Worksheets[0];
            int[] ids = Utilities.GetSelectedIDs(this.reoGridControlPutaway);
            if (ids.Length == 0)
            {
                this.putawayTicketItemID = -1;
                return;
            }
            int id = ids[0];
            WMSEntities wmsEntities = new WMSEntities();
            PutawayTicketItemView putawayTicketItemView =
                        (from s in wmsEntities.PutawayTicketItemView
                        where s.ID == id
                        select s).FirstOrDefault();
            if (putawayTicketItemView == null)
            {
                MessageBox.Show("系统错误，未找到相应上架单项目", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.putawayTicketItemID = int.Parse(putawayTicketItemView.ID.ToString());
            Utilities.CopyPropertiesToTextBoxes(putawayTicketItemView, this);
            //Utilities.CopyPropertiesToComboBoxes(shipmentTicketItemView, this);
        }

        private void ClearTextBoxes()
        {
            foreach (Control control in this.tableLayoutPanelProperties.Controls)
            {
                if (control is TextBox)
                {
                    TextBox textBox = control as TextBox;
                    textBox.Text = "";
                }
            }
        }

        private void InitComponents()
        {
            //初始化
            //string[] columnNames = (from kn in ReceiptMetaData.putawayTicketItemKeyName select kn.Name).ToArray();
            //初始化表格
            var worksheet = this.reoGridControlPutaway.Worksheets[0];
            worksheet.SelectionMode = WorksheetSelectionMode.Row;
            for (int i = 0; i < ReceiptMetaData.putawayTicketItemKeyName.Length; i++)
            {
                worksheet.ColumnHeaders[i].Text = ReceiptMetaData.putawayTicketItemKeyName[i].Name;
                worksheet.ColumnHeaders[i].IsVisible = ReceiptMetaData.putawayTicketItemKeyName[i].Visible;
            }
            worksheet.Columns = ReceiptMetaData.putawayTicketItemKeyName.Length;
        }

        private void Search()
        {
            this.labelStatus.Text = "搜索中...";

            new Thread(new ThreadStart(() =>
            {
                var wmsEntities = new WMSEntities();
                //ReceiptTicketView[] receiptTicketViews = null;
                PutawayTicketItemView[] putawayTicketItemView = null;
                /*
                try
                {
                    putawayTicketItemView = wmsEntities.Database.SqlQuery<PutawayTicketItemView>("SELECT * FROM PutawayTicketItemView WHERE PutawayTicketID=@putawayTicketID", new SqlParameter("putawayTicketID", putawayTicketID)).ToArray();
                }
                catch
                {
                    MessageBox.Show("无法连接到数据库，请查看网络连接!", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    return;
                }*/
                putawayTicketItemView = 
                (from ptiv in wmsEntities.PutawayTicketItemView
                 where ptiv.PutawayTicketProjectID == this.projectID && ptiv.PutawayTicketWarehouseID == this.warehouseID
                 orderby ptiv.StockInfoShipmentAreaAmount / (ptiv.ComponentDailyProduction * ptiv.ComponentSingleCarUsageAmount) ascending,
                 ptiv.ReceiptTicketItemInventoryDate ascending
                 select ptiv).ToArray();


                string sql = (from ptiv in wmsEntities.PutawayTicketItemView
                                where ptiv.PutawayTicketProjectID == this.projectID && ptiv.PutawayTicketWarehouseID == this.warehouseID
                              orderby ptiv.StockInfoShipmentAreaAmount / (ptiv.ComponentDailyProduction * ptiv.ComponentSingleCarUsageAmount) ascending,
                              ptiv.ReceiptTicketItemInventoryDate ascending
                              select ptiv).ToString();
                Console.WriteLine(sql);
                this.reoGridControlPutaway.Invoke(new Action(() =>
                {
                    this.labelStatus.Text = "搜索完成";
                    var worksheet = this.reoGridControlPutaway.Worksheets[0];
                    worksheet.DeleteRangeData(RangePosition.EntireRange);
                    int n = 0;
                    for (int i = 0; i < putawayTicketItemView.Length; i++)
                    {
                        if (putawayTicketItemView[i].State == "作废")
                        {
                            continue;
                        }
                        PutawayTicketItemView curputawayTicketItemView = putawayTicketItemView[i];
                        object[] columns = Utilities.GetValuesByPropertieNames(curputawayTicketItemView, (from kn in ReceiptMetaData.putawayTicketItemKeyName select kn.Key).ToArray());
                        for (int j = 0; j < worksheet.Columns; j++)
                        {
                            worksheet[n, j] = columns[j];
                        }
                        n++;
                    }
                }));
                this.Invoke(new Action(() => { this.RefreshTextBoxes(); }));
            })).Start();

        }

        private void buttonModify_Click(object sender, EventArgs e)
        {
            var worksheet = this.reoGridControlPutaway.Worksheets[0];
            try
            {
                if (worksheet.SelectionRange.Rows != 1)
                {
                    throw new EntityCommandExecutionException();
                }
                int putawayTicketItemID = int.Parse(worksheet[worksheet.SelectionRange.Row, 0].ToString());
                PutawayTicketItem putawayTicketItem = (from pti in wmsEntities.PutawayTicketItem where pti.ID == putawayTicketItemID select pti).FirstOrDefault();
                if (putawayTicketItem == null)
                {
                    MessageBox.Show("错误 无法修改此条目");
                }
                else
                {
                    string errorInfo;
                    if (Utilities.CopyTextBoxTextsToProperties(this, putawayTicketItem, ReceiptMetaData.putawayTicketItemKeyName, out errorInfo) == false)
                    {
                        MessageBox.Show(errorInfo);
                        return;
                    }
                    else
                    {
                        new Thread(() =>
                        {

                            wmsEntities.SaveChanges();
                            this.Invoke(new Action(() =>
                            {
                                Search();
                            }));
                            MessageBox.Show("成功");


                        }).Start();
                    }
                }
            }
            catch (EntityCommandExecutionException)
            {
                MessageBox.Show("请选择一项进行修改", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            catch (Exception)
            {
                MessageBox.Show("无法连接到数据库，请查看网络连接!", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                return;
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            var worksheet = this.reoGridControlPutaway.Worksheets[0];
            try
            {
                if (worksheet.SelectionRange.Rows != 1)
                {
                    throw new EntityCommandExecutionException();
                }
                int putawayTicketItemID = int.Parse(worksheet[worksheet.SelectionRange.Row, 0].ToString());
                PutawayTicketItem putawayTicketItem = (from pti in wmsEntities.PutawayTicketItem where pti.ID == putawayTicketItemID select pti).FirstOrDefault();
                if (putawayTicketItem == null)
                {
                    MessageBox.Show("错误 无法修改此条目");
                }
                else
                {
                    putawayTicketItem.State = "作废";
                    ReceiptTicketItem receiptTicketItem = (from rti in wmsEntities.ReceiptTicketItem where rti.ID == putawayTicketItem.ReceiptTicketItemID select rti).FirstOrDefault();
                    if (receiptTicketItem != null)
                    {
                        receiptTicketItem.State = "待收货";
                    }
                    new Thread(() =>
                    {
                        wmsEntities.SaveChanges();
                        this.Invoke(new Action(() =>
                        {
                            Search();
                        }));
                        MessageBox.Show("成功");
                    }).Start();
                }
            }
            catch (EntityCommandExecutionException)
            {
                MessageBox.Show("请选择一项进行修改", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            catch (Exception)
            {
                MessageBox.Show("无法连接到数据库，请查看网络连接!", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                return;
            }
        }

        private void buttonFInished_Click(object sender, EventArgs e)
        {
            var worksheet = this.reoGridControlPutaway.Worksheets[0];
            try
            {
                if (worksheet.SelectionRange.Rows != 1)
                {
                    throw new EntityCommandExecutionException();
                }
                int putawayTicketItemID = int.Parse(worksheet[worksheet.SelectionRange.Row, 0].ToString());
                PutawayTicketItem putawayTicketItem = (from pti in wmsEntities.PutawayTicketItem where pti.ID == putawayTicketItemID select pti).FirstOrDefault();
                new Thread(() => 
                {
                    if (putawayTicketItem == null)
                    {
                        MessageBox.Show("此上架单条目不存在");
                        return;
                    }
                    else
                    {
                        putawayTicketItem.State = "已上架";
                        //StockInfo stockInfo = ReceiptUtilities.PutawayTicketItemToStockInfo(putawayTicketItem);
                        //wmsEntities.StockInfo.Add(stockInfo);
                        wmsEntities.SaveChanges();
                        int count = wmsEntities.Database.SqlQuery<int>(
                        "SELECT COUNT(*) FROM PutawayTicketItem " +
                        "WHERE PutawayTicketID = @putawayTicketID AND State <> '已上架'",
                        new SqlParameter("putawayTicketID", putawayTicketItem.PutawayTicketID)).FirstOrDefault();

                        if (count == 0)
                        {
                            wmsEntities.Database.ExecuteSqlCommand(
                                "UPDATE PutawayTicket SET State = '已上架' " +
                                "WHERE ID = @putawayTicketID",
                                new SqlParameter("putawayTicketID", putawayTicketItem.PutawayTicketID));
                        }
                        else
                        {
                            wmsEntities.Database.ExecuteSqlCommand(
                                "UPDATE PutawayTicket SET State = '部分上架' " +
                                "WHERE ID = @putawayTicketID",
                                new SqlParameter("putawayTicketID", putawayTicketItem.PutawayTicketID));
                        }
                        StockInfo stockInfo = (from si in wmsEntities.StockInfo where si.ReceiptTicketItemID == putawayTicketItem.ReceiptTicketItemID select si).FirstOrDefault();
                        if (stockInfo != null)
                        {
                            if (stockInfo.ReceiptAreaAmount != null)
                            {
                                int amount = (int)stockInfo.ReceiptAreaAmount;
                                if (stockInfo.OverflowAreaAmount != null)
                                {
                                    stockInfo.OverflowAreaAmount += amount;
                                }
                                else
                                {
                                    stockInfo.OverflowAreaAmount = amount;
                                }
                                stockInfo.ReceiptAreaAmount = 0;
                            }

                        }
                        wmsEntities.SaveChanges();
                        this.Invoke(new Action(() =>
                        {
                            this.Search();
                            CallBack();

                        }));
                    }
                }).Start();
            }
            catch(EntityCommandExecutionException)
            {
                MessageBox.Show("请选择一项进行修改", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            catch (Exception)
            {
                MessageBox.Show("无法连接到数据库，请查看网络连接!", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                return;
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            var worksheet = this.reoGridControlPutaway.Worksheets[0];
            try
            {
                if (worksheet.SelectionRange.Rows != 1)
                {
                    throw new EntityCommandExecutionException();
                }
                int putawayTicketItemID = int.Parse(worksheet[worksheet.SelectionRange.Row, 0].ToString());
                PutawayTicketItem putawayTicketItem = (from pti in wmsEntities.PutawayTicketItem where pti.ID == putawayTicketItemID select pti).FirstOrDefault();
                new Thread(() =>
                {
                    if (putawayTicketItem == null)
                    {
                        MessageBox.Show("找不到该上架单，可能已被删除");
                        return;
                    }
                    else
                    {
                        putawayTicketItem.State = "待上架";
                        wmsEntities.SaveChanges();
                        int count = wmsEntities.Database.SqlQuery<int>(
                            "SELECT COUNT(*) FROM PutawayTicketItem " +
                        "WHERE PutawayTicketID = @putawayTicketID AND State <> '待上架'",
                        new SqlParameter("putawayTicketID", putawayTicketItem.PutawayTicketID)).FirstOrDefault();
                        

                        if (count == 0)
                        {
                            wmsEntities.Database.ExecuteSqlCommand(
                                "UPDATE PutawayTicket SET State = '待上架' " +
                                "WHERE ID = @putawayTicketID",
                                new SqlParameter("putawayTicketID", putawayTicketItem.PutawayTicketID));
                        }
                        else
                        {
                            wmsEntities.Database.ExecuteSqlCommand(
                                 "UPDATE PutawayTicket SET State = '部分上架' " +
                                 "WHERE ID = @putawayTicketID",
                                 new SqlParameter("putawayTicketID", putawayTicketItem.PutawayTicketID));
                        }
                    }
                    StockInfo stockInfo = (from si in wmsEntities.StockInfo where si.ReceiptTicketItemID == putawayTicketItem.ReceiptTicketItemID select si).FirstOrDefault();
                    if (stockInfo != null)
                    {
                        if (stockInfo.OverflowAreaAmount != null)
                        {
                            int amount = (int)stockInfo.OverflowAreaAmount;
                            if (stockInfo.ReceiptAreaAmount != null)
                            {
                                stockInfo.ReceiptAreaAmount += amount;
                            }
                            else
                            {
                                stockInfo.ReceiptAreaAmount = amount;
                            }
                            stockInfo.OverflowAreaAmount = 0;
                        }
                    }
                    wmsEntities.SaveChanges();
                    //MessageBox.Show("成功");
                    this.Invoke(new Action(() =>
                    {
                        this.Search();
                        CallBack();
                    }));
                }).Start();
            }
            catch(EntityCommandExecutionException)
            {
                MessageBox.Show("请选择一项进行修改", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            catch (Exception)
            {
                MessageBox.Show("无法连接到数据库，请查看网络连接!", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                return;
            }
        }

        private void buttonDelete_MouseEnter(object sender, EventArgs e)
        {
            buttonDelete.BackgroundImage = WMS.UI.Properties.Resources.bottonW_s;
        }

        private void buttonDelete_MouseLeave(object sender, EventArgs e)
        {
            buttonDelete.BackgroundImage = WMS.UI.Properties.Resources.bottonW_q;
        }

        private void buttonDelete_MouseDown(object sender, MouseEventArgs e)
        {
            buttonDelete.BackgroundImage = WMS.UI.Properties.Resources.bottonB3_q;
        }

        private void buttonModify_MouseEnter(object sender, EventArgs e)
        {
            buttonModify.BackgroundImage = WMS.UI.Properties.Resources.bottonW_s;
        }

        private void buttonModify_MouseLeave(object sender, EventArgs e)
        {
            buttonModify.BackgroundImage = WMS.UI.Properties.Resources.bottonW_q;
        }

        private void buttonModify_MouseDown(object sender, MouseEventArgs e)
        {
            buttonModify.BackgroundImage = WMS.UI.Properties.Resources.bottonB3_q;
        }


        private void buttonFInished_MouseEnter(object sender, EventArgs e)
        {
            buttonFInished.BackgroundImage = WMS.UI.Properties.Resources.bottonB1_s;
        }

        private void buttonFinish_MouseLeave(object sender, EventArgs e)
        {
            buttonFInished.BackgroundImage = WMS.UI.Properties.Resources.bottonB2_s;
        }

        private void buttonFInished_MouseDown(object sender, MouseEventArgs e)
        {
            buttonFInished.BackgroundImage = WMS.UI.Properties.Resources.bottonB3_q;
        }

        private void buttonCancel_MouseEnter(object sender, EventArgs e)
        {
            buttonCancel.BackgroundImage = WMS.UI.Properties.Resources.bottonB4_s;
        }

        private void buttonCancel_MouseLeave(object sender, EventArgs e)
        {
            buttonCancel.BackgroundImage = WMS.UI.Properties.Resources.bottonB4_q;
        }

        private void buttonCancel_MouseDown(object sender, MouseEventArgs e)
        {
            buttonCancel.BackgroundImage = WMS.UI.Properties.Resources.bottonB3_s;
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
