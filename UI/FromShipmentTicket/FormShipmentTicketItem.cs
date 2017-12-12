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

        private int curStockInfoID = -1;

        private KeyName[] visibleColumns = (from kn in ShipmentTicketItemViewMetaData.KeyNames
                                            where kn.Visible == true
                                            select kn).ToArray();

        public FormShipmentTicketItem(int shipmentTicketID)
        {
            InitializeComponent();
            this.shipmentTicketID = shipmentTicketID;
        }

        public void SetShipmentTicketStateChangedCallback(Action jobTicketStateChangedCallback)
        {
            this.shipmentTicketStateChangedCallback = jobTicketStateChangedCallback;
        }

        private void FormShipmentTicketItem_Load(object sender, EventArgs e)
        {
            InitComponents();
            this.Search();
        }

        private void InitComponents()
        {
            this.wmsEntities.Database.Connection.Open();

            //初始化表格
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

            TextBox textBoxComponentName = (TextBox)this.Controls.Find("textBoxComponentName", true)[0];
            textBoxComponentName.Click += textBoxComponentName_Click;
            textBoxComponentName.ReadOnly = true;
            textBoxComponentName.BackColor = Color.White;
        }

        private void textBoxComponentName_Click(object sender, EventArgs e)
        {
            TextBox textBoxComponentName = (TextBox)this.Controls.Find("textBoxComponentName", true)[0];
            var formSelectStockInfo = new FormSelectSupplier(this.curStockInfoID);
            formSelectStockInfo.SetSelectFinishCallback((selectedStockInfoID)=>
            {
                this.curStockInfoID = selectedStockInfoID;
                new Thread(new ThreadStart(()=>
                {
                    StockInfoView stockInfoView = (from s in this.wmsEntities.StockInfoView
                                                   where s.ID == selectedStockInfoID
                                                   select s).Single();
                    this.Invoke(new Action(() =>
                    {
                        Utilities.CopyPropertiesToTextBoxes(stockInfoView, this);
                    }));
                })).Start();
            });
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
                this.curStockInfoID = -1;
                return;
            }
            int id = ids[0];
            ShipmentTicketItemView shipmentTicketItemView = (from s in this.wmsEntities.ShipmentTicketItemView
                                                             where s.ID == id
                                                             select s).FirstOrDefault();
            if (shipmentTicketItemView == null)
            {
                MessageBox.Show("系统错误，未找到相应发货单项目", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.curStockInfoID = shipmentTicketItemView.StockInfoID;
            Utilities.CopyPropertiesToTextBoxes(shipmentTicketItemView, this);
            Utilities.CopyPropertiesToComboBoxes(shipmentTicketItemView, this);
        }


        private void Search()
        {
            this.wmsEntities = new WMSEntities();
            var worksheet = this.reoGridControlMain.Worksheets[0];

            worksheet[0, 1] = "加载中...";
            new Thread(new ThreadStart(() =>
            {
                ShipmentTicketItemView[] shipmentTicketItemViews = (from s in wmsEntities.ShipmentTicketItemView
                                                                    where s.ShipmentTicketID == this.shipmentTicketID
                                                                    orderby s.ID descending
                                                                    select s).ToArray();

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
                //将状态置为已完成
                foreach (int id in selectedIDs)
                {
                    this.wmsEntities.Database.ExecuteSqlCommand(String.Format("UPDATE ShipmentTicketItem SET State = '{0}' WHERE ID = {1};", STRING_FINISHED, id));
                }
                this.wmsEntities.SaveChanges();

                //如果作业单中所有条目都完成，询问是否将作业单标记为完成
                int unfinishedShipmentTicketItemCount = wmsEntities.Database.SqlQuery<int>(String.Format("SELECT COUNT(*) FROM ShipmentTicketItem WHERE ShipmentTicketID = {0} AND State <> '{1}'", this.shipmentTicketID, STRING_FINISHED)).Single();
                if (unfinishedShipmentTicketItemCount == 0)
                {
                    if (MessageBox.Show("检测到所有的零件都已经收货完成，是否将出库单状态更新为完成？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        this.wmsEntities.Database.ExecuteSqlCommand(String.Format("UPDATE ShipmentTicket SET State = '{0}' WHERE ID = {1}", STRING_FINISHED, this.shipmentTicketID));
                        this.wmsEntities.SaveChanges();
                    }
                    this.shipmentTicketStateChangedCallback?.Invoke();
                }
                this.Invoke(new Action(()=>
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
                foreach (int id in selectedIDs)
                {
                    this.wmsEntities.Database.ExecuteSqlCommand(String.Format("UPDATE ShipmentTicketItem SET State = '{0}' WHERE ID = {1};", STRING_UNFINISHED, id));
                }
                this.wmsEntities.SaveChanges();
                this.Invoke(new Action(this.Search));
                MessageBox.Show("操作成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            })).Start();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (this.curStockInfoID == -1)
            {
                MessageBox.Show("未选择零件！","提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ShipmentTicketItem shipmentTicketItem = new ShipmentTicketItem();
            shipmentTicketItem.StockInfoID = this.curStockInfoID;
            shipmentTicketItem.ShipmentTicketID = this.shipmentTicketID;
          
            if(Utilities.CopyTextBoxTextsToProperties(this,shipmentTicketItem,ShipmentTicketItemViewMetaData.KeyNames,out string errorMessage) == false)
            {
                MessageBox.Show(errorMessage,"提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            new Thread(new ThreadStart(()=>
            {
                this.wmsEntities.ShipmentTicketItem.Add(shipmentTicketItem);
                this.wmsEntities.SaveChanges();
                this.Invoke(new Action(()=>
                {
                    this.Search();
                    Utilities.SelectLineByID(this.reoGridControlMain,shipmentTicketItem.ID);
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
                var shipmentTicketItem = (from s in this.wmsEntities.ShipmentTicketItem where s.ID == id select s).FirstOrDefault();
              
                if (shipmentTicketItem == null)
                {
                    MessageBox.Show("未找到此发货单条目信息","提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                shipmentTicketItem.StockInfoID = this.curStockInfoID;

                if (Utilities.CopyTextBoxTextsToProperties(this, shipmentTicketItem, ShipmentTicketItemViewMetaData.KeyNames, out string errorMessage) == false)
                {
                    MessageBox.Show(errorMessage, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                this.wmsEntities.SaveChanges();
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
            foreach(int id in ids)
            {
                this.wmsEntities.Database.ExecuteSqlCommand("DELETE FROM ShipmentTicketItem WHERE ID = @id",new SqlParameter("@id",id));
            }
            this.wmsEntities.SaveChanges();
            this.Search();
            MessageBox.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
    }
}
