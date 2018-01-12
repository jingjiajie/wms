using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using WMS.DataAccess;
using unvell.ReoGrid;
using System.Data.SqlClient;

namespace WMS.UI
{
    public partial class FormShipmentTicketItem : Form
    {
        private int shipmentTicketID = -1;
        private WMSEntities wmsEntities = new WMSEntities();
        Action shipmentTicketStateChangedCallback = null;
        private int projectID = -1;
        private int warehouseID = -1;

        private int curStockInfoID = -1;
        private int curConfirmPersonID = -1;
        private int curJobPersonID = -1;

        private TextBox textBoxConfirmPersonName = null;
        private TextBox textBoxJobPersonName = null;
        private TextBox textBoxComponentName = null;
        private FormSelectStockInfo formSelectStockInfo = null;

        private KeyName[] visibleColumns = (from kn in ShipmentTicketItemViewMetaData.KeyNames
                                            where kn.Visible == true
                                            select kn).ToArray();

        public FormShipmentTicketItem(int shipmentTicketID,int projectID,int warehouseID)
        {
            InitializeComponent();
            this.shipmentTicketID = shipmentTicketID;
            this.projectID = projectID;
            this.warehouseID = warehouseID;
        }

        public void SetShipmentTicketStateChangedCallback(Action jobTicketStateChangedCallback)
        {
            this.shipmentTicketStateChangedCallback = jobTicketStateChangedCallback;
        }

        private void FormShipmentTicketItem_Load(object sender, EventArgs e)
        {
            ShipmentTicket shipmentTicket = null;
            try
            {
                shipmentTicket = (from s in wmsEntities.ShipmentTicket where s.ID == shipmentTicketID select s).FirstOrDefault();
            }
            catch
            {
                MessageBox.Show("加载数据失败，请检查网络连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            if(shipmentTicket == null)
            {
                MessageBox.Show("发货单信息不存在，请刷新查询", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            InitComponents();
            this.Search();
        }

        private void InitComponents()
        {
            //初始化表格
            this.reoGridControlMain.SetSettings(WorkbookSettings.View_ShowSheetTabControl, false);
            var worksheet = this.reoGridControlMain.Worksheets[0];
            worksheet.SelectionMode = WorksheetSelectionMode.Row;

            for (int i = 0; i < ShipmentTicketItemViewMetaData.KeyNames.Length; i++)
            {
                worksheet.ColumnHeaders[i].Text = ShipmentTicketItemViewMetaData.KeyNames[i].Name;
                worksheet.ColumnHeaders[i].IsVisible = ShipmentTicketItemViewMetaData.KeyNames[i].Visible;
            }
            worksheet.Columns = ShipmentTicketItemViewMetaData.KeyNames.Length; //限制表的长度

            Utilities.CreateEditPanel(this.tableLayoutPanelProperties, ShipmentTicketItemViewMetaData.KeyNames);

            this.reoGridControlMain.Worksheets[0].SelectionRangeChanged += worksheet_SelectionRangeChanged;

            this.textBoxComponentName = (TextBox)this.Controls.Find("textBoxComponentName", true)[0];
            textBoxComponentName.Click += textBoxComponentName_Click;
            textBoxComponentName.ReadOnly = true;
            textBoxComponentName.BackColor = Color.White;

            this.textBoxJobPersonName = (TextBox)this.Controls.Find("textBoxJobPersonName", true)[0];
            textBoxJobPersonName.ReadOnly = true;
            textBoxJobPersonName.BackColor = Color.White;
            textBoxJobPersonName.Click += textBoxJobPersonName_Click;

            this.textBoxConfirmPersonName = (TextBox)this.Controls.Find("textBoxConfirmPersonName", true)[0];
            textBoxConfirmPersonName.ReadOnly = true;
            textBoxConfirmPersonName.BackColor = Color.White;
            textBoxConfirmPersonName.Click += textBoxConfirmPersonName_Click;
        }

        private void textBoxConfirmPersonName_Click(object sender, EventArgs e)
        {
            FormSelectPerson form = new FormSelectPerson();
            form.SetSelectFinishedCallback((id)=>
            {
                this.curConfirmPersonID = id;
                if (!this.IsDisposed)
                {
                    WMSEntities wmsEntities = new WMSEntities();
                    Person person = (from p in wmsEntities.Person
                                     where p.ID == id
                                     select p).FirstOrDefault();
                    if(person == null)
                    {
                        MessageBox.Show("选中人员不存在，请重新查询", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    this.curConfirmPersonID = id;
                    this.textBoxConfirmPersonName.Text = person.Name;
                }
            });
            form.Show();
        }

        private void textBoxJobPersonName_Click(object sender, EventArgs e)
        {
            FormSelectPerson form = new FormSelectPerson();
            form.SetSelectFinishedCallback((id) =>
            {
                this.curJobPersonID = id;
                if (!this.IsDisposed)
                {
                    WMSEntities wmsEntities = new WMSEntities();
                    Person person = (from p in wmsEntities.Person
                                     where p.ID == id
                                     select p).FirstOrDefault();
                    if (person == null)
                    {
                        MessageBox.Show("选中人员不存在，请重新查询", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    this.curJobPersonID = id;
                    this.textBoxJobPersonName.Text = person.Name;
                }
            });
            form.Show();
        }

        private void textBoxComponentName_Click(object sender, EventArgs e)
        {
            if (this.formSelectStockInfo == null)
            {
                this.formSelectStockInfo = new FormSelectStockInfo(this.projectID, this.warehouseID);

                formSelectStockInfo.SetSelectFinishedCallback((selectedStockInfoID) =>
                {
                    this.curStockInfoID = selectedStockInfoID;
                    if (!this.IsDisposed)
                    {
                        this.Invoke(new Action(() =>
                        {
                            textBoxComponentName.Text = "加载中...";
                        }));
                    }
                    new Thread(new ThreadStart(() =>
                    {
                        StockInfoView stockInfoView = null;
                        Supply supply = null;
                        try
                        {
                            stockInfoView = (from s in this.wmsEntities.StockInfoView
                                             where s.ID == selectedStockInfoID
                                             select s).FirstOrDefault();
                            supply = wmsEntities.Database.SqlQuery<Supply>(String.Format("SELECT * FROM Supply WHERE ID = (SELECT SupplyID FROM ReceiptTicketItem AS RI WHERE RI.ID = {0})",stockInfoView.ReceiptTicketItemID)).FirstOrDefault();
                        }
                        catch
                        {
                            MessageBox.Show("刷新数据失败，请检查网络连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        this.Invoke(new Action(() =>
                        {
                            Utilities.CopyPropertiesToTextBoxes(stockInfoView, this);
                            TextBox textBoxUnit = (TextBox)this.Controls.Find("textBoxUnit",true)[0];
                            TextBox textBoxUnitAmount = (TextBox)this.Controls.Find("textBoxUnitAmount", true)[0];
                            if (supply != null && string.IsNullOrWhiteSpace(textBoxUnit.Text) && string.IsNullOrWhiteSpace(textBoxUnitAmount.Text))
                            {
                                textBoxUnit.Text = supply.DefaultShipmentUnit;
                                if (supply.DefaultShipmentUnitAmount != null)
                                {
                                    textBoxUnitAmount.Text = Utilities.DecimalToString(supply.DefaultShipmentUnitAmount.Value);
                                }
                            }

                        }));
                    })).Start();
                });
            }

            formSelectStockInfo.Show();
        }

        private void worksheet_SelectionRangeChanged(object sender, unvell.ReoGrid.Events.RangeEventArgs e)
        {
            this.RefreshTextBoxes();
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

        private void RefreshTextBoxes()
        {
            this.ClearTextBoxes();
            var worksheet = this.reoGridControlMain.Worksheets[0];
            int[] ids = Utilities.GetSelectedIDs(this.reoGridControlMain);
            if (ids.Length == 0)
            {
                this.buttonAdd.Text = "添加条目";
                this.curStockInfoID = -1;
                this.curConfirmPersonID = -1;
                this.curJobPersonID = -1;
                //为编辑框填写默认值
                Utilities.FillTextBoxDefaultValues(this.tableLayoutPanelProperties, ShipmentTicketItemViewMetaData.KeyNames);
                return;
            }
            this.buttonAdd.Text = "复制条目";
            int id = ids[0];
            ShipmentTicketItemView shipmentTicketItemView = null;
            try
            {
                shipmentTicketItemView = (from s in this.wmsEntities.ShipmentTicketItemView
                                          where s.ID == id
                                          select s).FirstOrDefault();
            }
            catch
            {
                MessageBox.Show("刷新数据失败，请检查网络连接","提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (shipmentTicketItemView == null)
            {
                MessageBox.Show("系统错误，未找到相应发货单项目", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (shipmentTicketItemView.StockInfoID != null)
            {
                this.curStockInfoID = shipmentTicketItemView.StockInfoID.Value;
            }
            else
            {
                this.curStockInfoID = -1;
            }
            Utilities.CopyPropertiesToTextBoxes(shipmentTicketItemView, this);
            Utilities.CopyPropertiesToComboBoxes(shipmentTicketItemView, this);
        }


        private void Search(int selectID = -1)
        {
            this.wmsEntities = new WMSEntities();
            var worksheet = this.reoGridControlMain.Worksheets[0];

            worksheet[0, 1] = "加载中...";
            new Thread(new ThreadStart(() =>
            {
                ShipmentTicketItemView[] shipmentTicketItemViews = null;
                try
                {
                    shipmentTicketItemViews = (from s in wmsEntities.ShipmentTicketItemView
                                                                        where s.ShipmentTicketID == this.shipmentTicketID
                                                                        orderby s.ID descending
                                                                        select s).ToArray();
                }
                catch
                {
                    MessageBox.Show("查询数据失败，请检查网络连接","提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (this.IsDisposed == false)
                    {
                        this.Invoke(new Action(this.Close));
                    }
                    return;
                }

                this.Invoke(new Action(() =>
                {
                    this.labelStatus.Text = "加载完成";
                    worksheet.DeleteRangeData(RangePosition.EntireRange);
                    if (shipmentTicketItemViews.Length == 0)
                    {
                        worksheet[0, 1] = "没有符合条件的记录";
                    }
                    for (int i = 0; i < shipmentTicketItemViews.Length; i++)
                    {
                        var curShipmentTicketViews = shipmentTicketItemViews[i];
                        object[] columns = Utilities.GetValuesByPropertieNames(curShipmentTicketViews, (from kn in ShipmentTicketItemViewMetaData.KeyNames select kn.Key).ToArray());
                        for (int j = 0; j < columns.Length; j++)
                        {
                            worksheet[i, j] = columns[j] == null ? "" : columns[j].ToString();
                        }
                    }
                    if(selectID != -1)
                    {
                        Utilities.SelectLineByID(this.reoGridControlMain, selectID);
                    }
                    this.Invoke(new Action(this.RefreshTextBoxes));
                }));
            })).Start();
        }

        private void buttonFinish_Click(object sender, EventArgs e)
        {
            const string STRING_FINISHED = "已完成";
            int[] selectedIDs = Utilities.GetSelectedIDs(this.reoGridControlMain);
            if (selectedIDs.Length == 0)
            {
                MessageBox.Show("请选择您要操作的条目", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            new Thread(new ThreadStart(() =>
            {
                try
                {
                    //将状态置为已完成
                    foreach (int id in selectedIDs)
                    {
                        this.wmsEntities.Database.ExecuteSqlCommand(String.Format("UPDATE ShipmentTicketItem SET State = '{0}' WHERE ID = {1};", STRING_FINISHED, id));
                    }
                    this.wmsEntities.SaveChanges();
                }
                catch
                {
                    MessageBox.Show("操作失败，请检查网络连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //如果作业单中所有条目都完成，询问是否将作业单标记为完成
                int unfinishedShipmentTicketItemCount = wmsEntities.Database.SqlQuery<int>(String.Format("SELECT COUNT(*) FROM ShipmentTicketItem WHERE ShipmentTicketID = {0} AND State <> '{1}'", this.shipmentTicketID, STRING_FINISHED)).Single();
                if (unfinishedShipmentTicketItemCount == 0)
                {
                    if (MessageBox.Show("检测到所有的零件都已经收货完成，是否将出库单状态更新为完成？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        try
                        {
                            this.wmsEntities.Database.ExecuteSqlCommand(String.Format("UPDATE ShipmentTicket SET State = '{0}' WHERE ID = {1}", STRING_FINISHED, this.shipmentTicketID));
                            this.wmsEntities.SaveChanges();
                        }
                        catch
                        {
                            MessageBox.Show("操作失败，请检查网络连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    this.shipmentTicketStateChangedCallback?.Invoke();
                }

                this.Invoke(new Action(() =>
                {
                    this.Search();
                }));
                MessageBox.Show("操作成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            })).Start();

        }

        private void buttonUnfinish_Click(object sender, EventArgs e)
        {
            const string STRING_UNFINISHED = "未完成";
            int[] selectedIDs = Utilities.GetSelectedIDs(this.reoGridControlMain);
            if (selectedIDs.Length == 0)
            {
                MessageBox.Show("请选择您要操作的条目", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            new Thread(new ThreadStart(() =>
            {
                try
                {
                    foreach (int id in selectedIDs)
                    {
                        this.wmsEntities.Database.ExecuteSqlCommand(String.Format("UPDATE ShipmentTicketItem SET State = '{0}' WHERE ID = {1};", STRING_UNFINISHED, id));
                    }
                    this.wmsEntities.SaveChanges();
                }
                catch
                {
                    MessageBox.Show("操作失败，请检查网络连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                this.Invoke(new Action<int>(this.Search));
                MessageBox.Show("操作成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            })).Start();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (this.curStockInfoID == -1)
            {
                MessageBox.Show("请选择零件！","提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ShipmentTicketItem shipmentTicketItem = new ShipmentTicketItem();
            shipmentTicketItem.StockInfoID = this.curStockInfoID;
            shipmentTicketItem.ShipmentTicketID = this.shipmentTicketID;
            shipmentTicketItem.ConfirmPersonID = this.curConfirmPersonID == -1 ? null : (int?)this.curConfirmPersonID;
            shipmentTicketItem.JobPersonID = this.curJobPersonID == -1 ? null : (int?)this.curJobPersonID;


            if (Utilities.CopyTextBoxTextsToProperties(this,shipmentTicketItem,ShipmentTicketItemViewMetaData.KeyNames,out string errorMessage) == false)
            {
                MessageBox.Show(errorMessage,"提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if(shipmentTicketItem.ShipmentAmount < shipmentTicketItem.ScheduledJobAmount)
            {
                MessageBox.Show("发货数量不能小于已分配翻包作业数量！","提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            new Thread(new ThreadStart(()=>
            {
                try
                {
                    //扣除库存数量
                    StockInfo stockInfo = (from s in wmsEntities.StockInfo
                                           where s.ID == shipmentTicketItem.StockInfoID
                                           select s).FirstOrDefault();
                    if (stockInfo == null)
                    {
                        MessageBox.Show("零件不存在，请重新选择", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (stockInfo.ShipmentAreaAmount < shipmentTicketItem.ShipmentAmount*shipmentTicketItem.UnitAmount)
                    {
                        MessageBox.Show("添加失败，零件库存不足！发货区存货数：" + Utilities.DecimalToString(stockInfo.ShipmentAreaAmount ?? 0), "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    stockInfo.ShipmentAreaAmount -= shipmentTicketItem.ShipmentAmount * shipmentTicketItem.UnitAmount;
                    this.wmsEntities.ShipmentTicketItem.Add(shipmentTicketItem);
                    this.wmsEntities.SaveChanges();
                }
                catch
                {
                    MessageBox.Show("操作失败，请检查网络连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                this.Invoke(new Action(()=>
                {
                    this.Search(shipmentTicketItem.ID);
                }));
                MessageBox.Show("添加成功！","提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            })).Start();
        }

        private void buttonAlter_Click(object sender, EventArgs e)
        {
            int[] ids = Utilities.GetSelectedIDs(this.reoGridControlMain);
            if(ids.Length != 1)
            {
                MessageBox.Show("请选择一项进行修改！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            new Thread(new ThreadStart(() =>
            {
                int id = ids[0];
                ShipmentTicketItem shipmentTicketItem = null;
                try
                {
                    shipmentTicketItem = (from s in this.wmsEntities.ShipmentTicketItem where s.ID == id select s).FirstOrDefault();
                }
                catch
                {
                    MessageBox.Show("操作失败，请检查网络连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (shipmentTicketItem == null)
                {
                    MessageBox.Show("发货单条目不存在，请重新查询","提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                shipmentTicketItem.StockInfoID = this.curStockInfoID;
                shipmentTicketItem.ConfirmPersonID = this.curConfirmPersonID == -1 ? null : (int?)this.curConfirmPersonID;
                shipmentTicketItem.JobPersonID = this.curJobPersonID == -1 ? null : (int?)this.curJobPersonID;

                if (Utilities.CopyTextBoxTextsToProperties(this, shipmentTicketItem, ShipmentTicketItemViewMetaData.KeyNames, out string errorMessage) == false)
                {
                    MessageBox.Show(errorMessage, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    this.wmsEntities.SaveChanges();
                }
                catch
                {
                    MessageBox.Show("操作失败，请检查网络连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                this.Invoke(new Action(() =>
                {
                    this.Search();
                    Utilities.SelectLineByID(this.reoGridControlMain, shipmentTicketItem.ID);
                }));
                MessageBox.Show("修改成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            })).Start();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            int[] ids = Utilities.GetSelectedIDs(this.reoGridControlMain);
            if(ids.Length == 0)
            {
                MessageBox.Show("请选择要删除的项目","提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                foreach (int id in ids)
                {
                    this.wmsEntities.Database.ExecuteSqlCommand("DELETE FROM ShipmentTicketItem WHERE ID = @id", new SqlParameter("@id", id));
                }
                this.wmsEntities.SaveChanges();
            }
            catch
            {
                MessageBox.Show("操作失败，请检查网络连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.Search();
            MessageBox.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        private void buttonAdd_MouseEnter(object sender, EventArgs e)
        {
            buttonAdd.BackgroundImage = WMS.UI.Properties.Resources.bottonW_s;
        }

        private void buttonAdd_MouseLeave(object sender, EventArgs e)
        {
            buttonAdd.BackgroundImage = WMS.UI.Properties.Resources.bottonW_q;
        }

        private void buttonAdd_MouseDown(object sender, MouseEventArgs e)
        {
            buttonAdd.BackgroundImage = WMS.UI.Properties.Resources.bottonB3_q;
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

        private void buttonAlter_MouseEnter(object sender, EventArgs e)
        {
            buttonAlter.BackgroundImage = WMS.UI.Properties.Resources.bottonW_s;
        }

        private void buttonAlter_MouseLeave(object sender, EventArgs e)
        {
            buttonAlter.BackgroundImage = WMS.UI.Properties.Resources.bottonW_q;
        }

        private void buttonAlter_MouseDown(object sender, MouseEventArgs e)
        {
            buttonAlter.BackgroundImage = WMS.UI.Properties.Resources.bottonB3_q;
        }
    }
}
