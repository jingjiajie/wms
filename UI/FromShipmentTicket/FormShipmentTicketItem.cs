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

        private StandardImportForm<ShipmentTicketItem> standardImportForm = null;

        private KeyName[] usedKeyNames = (from kn in ShipmentTicketItemViewMetaData.KeyNames
                                            where kn.Visible == true || kn.Key == "ID"
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
                WMSEntities wmsEntities = new WMSEntities();
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

            for (int i = 0; i < usedKeyNames.Length; i++)
            {
                worksheet.ColumnHeaders[i].Text = usedKeyNames[i].Name;
                worksheet.ColumnHeaders[i].IsVisible = usedKeyNames[i].Visible;
            }
            worksheet.Columns = usedKeyNames.Length; //限制表的长度

            Utilities.CreateEditPanel(this.tableLayoutPanelProperties, usedKeyNames);

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
                        WMSEntities wmsEntities = new WMSEntities();
                        StockInfoView stockInfoView = null;
                        Supply supply = null;
                        try
                        {
                            stockInfoView = (from s in wmsEntities.StockInfoView
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

            formSelectStockInfo.ShowDialog();
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
                Utilities.FillTextBoxDefaultValues(this.tableLayoutPanelProperties, usedKeyNames);
                return;
            }
            this.buttonAdd.Text = "复制条目";
            int id = ids[0];
            ShipmentTicketItemView shipmentTicketItemView = null;
            try
            {
                WMSEntities wmsEntities = new WMSEntities();
                var tmp = (from s in wmsEntities.ShipmentTicketItemView
                           where s.ID == id
                           select s);
                shipmentTicketItemView = tmp.FirstOrDefault();
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
            WMSEntities wmsEntities = new WMSEntities();
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
                        object[] columns = Utilities.GetValuesByPropertieNames(
                            curShipmentTicketViews,
                            (from kn in usedKeyNames where(kn.Visible == true || kn.Key=="ID") select kn.Key).ToArray()
                            );
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
                WMSEntities wmsEntities = new WMSEntities();
                try
                {
                    //将状态置为已完成
                    foreach (int id in selectedIDs)
                    {
                        wmsEntities.Database.ExecuteSqlCommand(String.Format("UPDATE ShipmentTicketItem SET State = '{0}' WHERE ID = {1};", STRING_FINISHED, id));
                    }
                    wmsEntities.SaveChanges();
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
                            wmsEntities.Database.ExecuteSqlCommand(String.Format("UPDATE ShipmentTicket SET State = '{0}' WHERE ID = {1}", STRING_FINISHED, this.shipmentTicketID));
                            wmsEntities.SaveChanges();
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
                WMSEntities wmsEntities = new WMSEntities();
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
                    wmsEntities.ShipmentTicketItem.Add(shipmentTicketItem);
                    wmsEntities.SaveChanges();
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
            if(this.curStockInfoID == -1)
            {
                MessageBox.Show("请选择零件！","提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            new Thread(new ThreadStart(() =>
            {
                WMSEntities wmsEntities = new WMSEntities();
                int id = ids[0];
                ShipmentTicketItem shipmentTicketItem = null;
                try
                {
                    shipmentTicketItem = (from s in wmsEntities.ShipmentTicketItem where s.ID == id select s).FirstOrDefault();
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
                StockInfo oriStockInfo = (from s in wmsEntities.StockInfo where s.ID == shipmentTicketItem.StockInfoID select s).FirstOrDefault();
                shipmentTicketItem.StockInfoID = this.curStockInfoID;
                StockInfo newStockInfo = (from s in wmsEntities.StockInfo where s.ID == this.curStockInfoID select s).FirstOrDefault();
                if(newStockInfo == null)
                {
                    MessageBox.Show("零件不存在，请重新选择！","提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                shipmentTicketItem.ConfirmPersonID = this.curConfirmPersonID == -1 ? null : (int?)this.curConfirmPersonID;
                shipmentTicketItem.JobPersonID = this.curJobPersonID == -1 ? null : (int?)this.curJobPersonID;
                decimal oriShipmentAmount = shipmentTicketItem.ShipmentAmount * (shipmentTicketItem.UnitAmount ?? 1) ?? 0;
                if (Utilities.CopyTextBoxTextsToProperties(this, shipmentTicketItem, ShipmentTicketItemViewMetaData.KeyNames, out string errorMessage) == false)
                {
                    MessageBox.Show(errorMessage, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if(shipmentTicketItem.ShipmentAmount <= 0)
                {
                    MessageBox.Show("发货数量必须大于0！","提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                decimal? deltaStockAmount = (shipmentTicketItem.ShipmentAmount * shipmentTicketItem.UnitAmount) - oriShipmentAmount;
                if (deltaStockAmount > newStockInfo.ShipmentAreaAmount)
                {
                    MessageBox.Show("库存不足！当前库存数量：" + Utilities.DecimalToString(newStockInfo.ShipmentAreaAmount ?? 0) + "个", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //把库存数量加回来！
                if (oriStockInfo != newStockInfo)
                {
                    if(oriStockInfo != null)
                    {
                        oriStockInfo.ShipmentAreaAmount += oriShipmentAmount;
                    }
                    if(newStockInfo != null)
                    {
                        newStockInfo.ShipmentAreaAmount -= (shipmentTicketItem.ShipmentAmount * shipmentTicketItem.UnitAmount) ?? 0;
                    }
                }
                else
                {
                    newStockInfo.ShipmentAreaAmount -= deltaStockAmount ?? 0;
                }

                try
                {
                    wmsEntities.SaveChanges();
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
                WMSEntities wmsEntities = new WMSEntities();
                foreach (int id in ids)
                {
                    ShipmentTicketItem item = (from s in wmsEntities.ShipmentTicketItem where s.ID == id select s).FirstOrDefault();
                    if (item == null) continue;
                    if(item.ScheduledJobAmount > 0)
                    {
                        MessageBox.Show("不能删除已分配翻包的零件！","提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    //把库存数量加回去
                    decimal? amount = item.ShipmentAmount * item.UnitAmount;
                    StockInfo stockInfo = (from s in wmsEntities.StockInfo where s.ID == item.StockInfoID select s).FirstOrDefault();
                    if (stockInfo == null) continue;
                    stockInfo.ShipmentAreaAmount += amount ?? 0;
                    wmsEntities.ShipmentTicketItem.Remove(item);
                }
                wmsEntities.SaveChanges();
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

        private void buttonImport_Click(object sender, EventArgs e)
        {
            this.standardImportForm = new StandardImportForm<ShipmentTicketItem>(
                ShipmentTicketItemViewMetaData.KeyNames,
                importItemHandler,
                null,
                "导入发货单条目"
                );
            standardImportForm.Show();
        }

        private bool importItemHandler(ShipmentTicketItem[] results,Dictionary<string,string[]> unimportedColumns)
        {
            List<ShipmentTicketItem> realImportList = new List<ShipmentTicketItem>(); //真正要导入的ShipmentTicketItem（有的一个result项可能对应多个导入项）
            try
            {
                WMSEntities wmsEntities = new WMSEntities();
                for (int i = 0; i < results.Length; i++)
                {
                    string supplyNo = unimportedColumns["SupplyNoOrComponentName"][i];
                    string componentName = unimportedColumns["SupplyNoOrComponentName"][i];
                    string jobPersonName = unimportedColumns["JobPersonName"][i];
                    string confirmPersonName = unimportedColumns["ConfirmPersonName"][i];
                    Supply supply = (from s in wmsEntities.Supply where s.No == supplyNo select s).FirstOrDefault();
                    DataAccess.Component component = null;
                    if (supply == null)
                    {
                        component = (from c in wmsEntities.Component where c.Name == componentName select c).FirstOrDefault();
                        if (component == null)
                        {
                            MessageBox.Show(string.Format("行{0}：不存在零件\"{1}\"！", i + 1, componentName), "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                    }
                    StockInfoView[] stockInfoViews = null;
                    if(supply != null)
                    {
                        stockInfoViews = (from s in wmsEntities.StockInfoView
                                          where s.SupplyNo == supplyNo
                                                && s.ShipmentAreaAmount > 0
                                          orderby s.InventoryDate ascending
                                          select s).ToArray();
                    }
                    else if (component != null)
                    {
                        stockInfoViews = (from s in wmsEntities.StockInfoView
                                          where s.ComponentName == componentName
                                                && s.ShipmentAreaAmount > 0
                                          orderby s.InventoryDate ascending
                                          select s).ToArray();
                    }
                    int jobPersonID = -1;
                    int confirmPersonID = -1;
                    //搜索作业人名
                    if (string.IsNullOrWhiteSpace(jobPersonName) == false)
                    {
                        Person jobPerson = (from p in wmsEntities.Person where p.Name == jobPersonName select p).FirstOrDefault();
                        if (jobPerson == null)
                        {
                            MessageBox.Show(string.Format("行{0}：作业人员\"{1}\"不存在！", i + 1, jobPersonName), "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                        jobPersonID = jobPerson.ID;
                    }
                    //搜索确认人名
                    if (string.IsNullOrWhiteSpace(confirmPersonName) == false)
                    {
                        Person confirmPerson = (from p in wmsEntities.Person where p.Name == confirmPersonName select p).FirstOrDefault();
                        if (confirmPerson == null)
                        {
                            MessageBox.Show(string.Format("行{0}：确认人员\"{1}\"不存在！", i + 1, confirmPersonName), "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                        confirmPersonID = confirmPerson.ID;
                    }

                    decimal stockAmount = stockInfoViews.Sum((stockInfoView) => stockInfoView.ShipmentAreaAmount) ?? 0;
                    if (stockAmount < results[i].ShipmentAmount * results[i].UnitAmount)
                    {
                        MessageBox.Show(string.Format("行{0}：零件\"{1}\"库存不足（库存数：{2}）", i + 1, componentName, Utilities.DecimalToString(stockAmount)), "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    results[i].ShipmentTicketID = this.shipmentTicketID;
                    results[i].JobPersonID = jobPersonID == -1 ? null : (int?)jobPersonID;
                    results[i].ConfirmPersonID = confirmPersonID == -1 ? null : (int?)confirmPersonID;
                    decimal curAmount = 0;
                    for (int j = 0; j < stockInfoViews.Length; j++)
                    {
                        ShipmentTicketItem newItem = new ShipmentTicketItem();
                        Utilities.CopyProperties(results[i], newItem);
                        newItem.StockInfoID = stockInfoViews[j].ID;
                        //当前StockInfo的数量小于要发货的数量
                        if (curAmount + stockInfoViews[j].ShipmentAreaAmount < results[i].ShipmentAmount)
                        {
                            newItem.ShipmentAmount = stockInfoViews[j].ShipmentAreaAmount;
                            realImportList.Add(newItem);
                            curAmount += newItem.ShipmentAmount.Value;
                        }
                        else //当前StockInfo数量大于等于需要发货的数量
                        {
                            newItem.ShipmentAmount = results[i].ShipmentAmount - curAmount;
                            realImportList.Add(newItem);
                            curAmount += newItem.ShipmentAmount.Value;
                            break;
                        }
                    }
                }

                foreach (ShipmentTicketItem item in realImportList)
                {
                    //增加条目，扣除库存
                    StockInfo stockInfo = (from s in wmsEntities.StockInfo where s.ID == item.StockInfoID select s).FirstOrDefault();
                    stockInfo.ShipmentAreaAmount -= item.ShipmentAmount;
                    //设置发货单条目初始信息
                    item.ScheduledJobAmount = 0;
                    wmsEntities.ShipmentTicketItem.Add(item);
                }
                wmsEntities.SaveChanges();
                this.Search();
                MessageBox.Show("导入成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.standardImportForm.Close();
            }
            catch
            {
                MessageBox.Show("操作失败，请检查网络连接！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return false;
        }

        private void buttonImport_MouseEnter(object sender, EventArgs e)
        {
            buttonImport.BackgroundImage = WMS.UI.Properties.Resources.bottonW_s;
        }

        private void buttonImport_MouseLeave(object sender, EventArgs e)
        {
            buttonImport.BackgroundImage = WMS.UI.Properties.Resources.bottonW_q;
        }

        private void buttonImport_MouseDown(object sender, MouseEventArgs e)
        {
            buttonImport.BackgroundImage = WMS.UI.Properties.Resources.bottonB3_q;
        }


    }
}
