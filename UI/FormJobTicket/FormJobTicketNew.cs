using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WMS.DataAccess;
using System.Threading;
using unvell.ReoGrid;
using unvell.ReoGrid.CellTypes;

namespace WMS.UI
{
    public partial class FormJobTicketNew : Form
    {
        private int shipmentTicketID = -1;
        private int userID = -1;
        private int projectID = -1;
        private int warehouseID = -1;

        private int curPersonID = -1;

        private Action<string> toJobTicketCallback = null;
        private int[] editableColumns = new int[] { 1, 2 };
        private int validRows = 0;

        private static KeyName[] newJobTicketKeyNames =
        {
            new KeyName(){Key="SupplyNoOrComponentName",Name="零件代号/名称"},
            new KeyName(){Key="ScheduleJobAmount",Name="计划翻包数量",NotNull=true,Positive=true},
            new KeyName(){Key="UnitAmount",Name="单位数量",NotNull=true,Positive=true}
        };

        class NewJobTicketItemData
        {
            private string supplyNoOrComponentName;
            private decimal scheduleJobAmount;
            private decimal unitAmount;

            public string SupplyNoOrComponentName { get => supplyNoOrComponentName; set => supplyNoOrComponentName = value; }
            public decimal ScheduleJobAmount { get => scheduleJobAmount; set => scheduleJobAmount = value; }
            public decimal UnitAmount { get => unitAmount; set => unitAmount = value; }
        }

        private StandardImportForm<NewJobTicketItemData> standardImportForm = null;

        public void SetToJobTicketCallback(Action<string> callback)
        {
            this.toJobTicketCallback = callback;
        }

        public FormJobTicketNew(int shipmentTicketID,int userID,int projectID,int warehouseID)
        {
            InitializeComponent();
            this.shipmentTicketID = shipmentTicketID;
            this.userID = userID;
            this.projectID = projectID;
            this.warehouseID = warehouseID;
        }

        private void FormJobTicketNew_Load(object sender, EventArgs e)
        {
            this.InitComponents();
        }

        private void InitComponents()
        {
            Utilities.InitReoGrid(this.reoGridControlMain, ShipmentTicketItemViewMetaData.KeyNames,WorksheetSelectionMode.Cell);
            this.reoGridControlMain.SetSettings(WorkbookSettings.View_ShowSheetTabControl, false);
            var worksheet = this.reoGridControlMain.Worksheets[0];
            worksheet.InsertColumns(1, 2);
            worksheet.ColumnHeaders[1].Text = "选择";
            worksheet.ColumnHeaders[2].Text = "计划翻包数量";
            worksheet.BeforeCellEdit += (s, e) =>
            {
                e.IsCancelled = !(editableColumns.Contains(e.Cell.Column) && e.Cell.Row < validRows);
            };

            Utilities.CreateEditPanel(this.tableLayoutEditPanel, JobTicketViewMetaData.KeyNames);
            ShipmentTicket shipmentTicket = null;
            User user = null;
            try
            {
                WMSEntities wmsEntities = new WMSEntities();
                shipmentTicket = (from s in wmsEntities.ShipmentTicket
                                  where s.ID == shipmentTicketID
                                  select s).FirstOrDefault();
                user = (from u in wmsEntities.User
                        where u.ID == this.userID
                        select u).FirstOrDefault();
            }
            catch
            {
                MessageBox.Show("无法连接到服务器，请检查网络连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            if (shipmentTicket == null)
            {
                MessageBox.Show("发货单信息不存在，请刷新显示", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            if (user == null)
            {
                MessageBox.Show("登录失效，操作失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.Controls.Find("textBoxCreateUserUsername", true)[0].Text = user.Username;
            this.Controls.Find("textBoxCreateTime", true)[0].Text = DateTime.Now.ToString();
            this.Controls.Find("textBoxShipmentTicketNo", true)[0].Text = shipmentTicket.No;
            this.Controls.Find("textBoxPersonName", true)[0].Click += textBoxPersonName_Click;
            this.Search();
        }

        private void textBoxPersonName_Click(object sender, EventArgs e)
        {
            TextBox textBoxPersonName = (TextBox)this.Controls.Find("textBoxPersonName", true)[0];
            FormSelectPerson formSelectPerson = new FormSelectPerson();
            formSelectPerson.SetSelectFinishedCallback((id)=>
            {
                WMSEntities wmsEntities = new WMSEntities();
                Person person = (from p in wmsEntities.Person
                                 where p.ID == id
                                 select p).FirstOrDefault();
                if(person == null)
                {
                    MessageBox.Show("人员不存在，请重新查询","提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                this.curPersonID = id;
                if (this.IsDisposed) return;
                this.Invoke(new Action(() =>
                {
                    textBoxPersonName.Text = person.Name;
                }));
            });
            formSelectPerson.Show();
        }

        private void Search()
        {
            WMSEntities wmsEntities = new WMSEntities();
            ShipmentTicketItemView[] shipmentTicketItemViews = (from s in wmsEntities.ShipmentTicketItemView
                                                                where s.ShipmentTicketID == this.shipmentTicketID
                                                                select s).ToArray();
            validRows = shipmentTicketItemViews.Length;
            var worksheet = this.reoGridControlMain.Worksheets[0];
            worksheet.DeleteRangeData(RangePosition.EntireRange);
            worksheet.Rows = (shipmentTicketItemViews.Length < 10 ? 10 : shipmentTicketItemViews.Length);
            //给第二列上颜色，加上边框
            worksheet.SetRangeBorders(0, 2, validRows, 1, BorderPositions.All, RangeBorderStyle.SilverSolid);

            if (shipmentTicketItemViews.Length == 0)
            {
                worksheet[0, 1] = "没有查询到符合条件的记录";
            }
            for (int i = 0; i < shipmentTicketItemViews.Length; i++)
            {
                ShipmentTicketItemView cur = shipmentTicketItemViews[i];
                if (cur.ShipmentAmount <= cur.ScheduledJobAmount)
                {
                    worksheet.Cells[i, 1].Style.BackColor = Color.LightGray;
                    worksheet.Cells[i, 1].IsReadOnly = true;
                }
                else
                {
                    worksheet[i, 1] = new CheckBoxCell(false); //显示复选框
                }
                //上颜色
                worksheet.Cells[i, 2].Style.BackColor = Color.AliceBlue;
                //计划翻包数量
                worksheet.Cells[i, 2].DataFormat = unvell.ReoGrid.DataFormat.CellDataFormatFlag.Text;
                decimal defaultScheduledJobAmount = (cur.ShipmentAmount - (cur.ScheduledJobAmount ?? 0)) ?? 0; //计划翻包数量默认等于发货数量-已经计划翻包过的数量
                worksheet[i, 2] = Utilities.DecimalToString(defaultScheduledJobAmount < 0 ? 0 : defaultScheduledJobAmount);
                object[] columns = Utilities.GetValuesByPropertieNames(cur, (from kn in ShipmentTicketItemViewMetaData.KeyNames where kn.Visible==true || kn.Key=="ID" select kn.Key).ToArray());
                int offsetColumn = 0;
                for (int j = 0; j < columns.Length; j++)
                {
                    while (this.editableColumns.Contains(j + offsetColumn)) offsetColumn++;
                    if (columns[j] == null) continue;
                    worksheet.Cells[i, offsetColumn + j].DataFormat = unvell.ReoGrid.DataFormat.CellDataFormatFlag.Text;
                    string text = null;
                    if(columns[j] is decimal)
                    {
                        text = Utilities.DecimalToString((decimal)columns[j]);
                    }
                    else
                    {
                        text = columns[j].ToString();
                    }
                    worksheet[i, offsetColumn + j] = text;
                }
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.reoGridControlMain.CurrentWorksheet.EndEdit(new EndEditReason());
            JobTicket newJobTicket = new JobTicket();
            string errorMessage = null;
            if(Utilities.CopyTextBoxTextsToProperties(this, newJobTicket, JobTicketViewMetaData.KeyNames, out errorMessage) == false)
            {
                MessageBox.Show(errorMessage, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            new Thread(()=>
            {
                WMSEntities wmsEntities = new WMSEntities();
                wmsEntities.JobTicket.Add(newJobTicket);

                ShipmentTicket shipmentTicket = (from s in wmsEntities.ShipmentTicket
                                  where s.ID == shipmentTicketID
                                  select s).FirstOrDefault();
                if(shipmentTicket == null)
                {
                    MessageBox.Show("发货单不存在，请重新查询", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                shipmentTicket.State = ShipmentTicketViewMetaData.STRING_STATE_PART_ASSIGNED_JOB;
                newJobTicket.State = JobTicketViewMetaData.STRING_STATE_UNFINISHED;
                newJobTicket.ShipmentTicketID = shipmentTicket.ID;
                newJobTicket.ProjectID = this.projectID;
                newJobTicket.WarehouseID = this.warehouseID;
                newJobTicket.CreateUserID = this.userID;
                newJobTicket.CreateTime = DateTime.Now;
                newJobTicket.PersonID = this.curPersonID == -1 ? null : (int?)this.curPersonID;

                var worksheet = this.reoGridControlMain.Worksheets[0];
                for (int i=0;i<worksheet.Rows;i++)
                {
                    if ((worksheet[i, 1] as bool? ?? false) == false) continue;
                    int shipmentTicketItemID = int.Parse(worksheet[i, 0].ToString());
                    ShipmentTicketItem shipmentTicketItem = (from s in wmsEntities.ShipmentTicketItem
                                                             where s.ID == shipmentTicketItemID
                                                             select s).FirstOrDefault();
                    if(shipmentTicketItem == null)
                    {
                        MessageBox.Show("发货单条目不存在，请重新查询","提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    var jobTicketItem = new JobTicketItem();
                    if (Utilities.CopyTextToProperty(worksheet[i,2]?.ToString(), "ScheduledAmount", jobTicketItem, JobTicketItemViewMetaData.KeyNames, out errorMessage) == false)
                    {
                        MessageBox.Show(string.Format("行{0}：{1}", i + 1, errorMessage), "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (jobTicketItem.ScheduledAmount + (shipmentTicketItem.ScheduledJobAmount ?? 0) > shipmentTicketItem.ShipmentAmount)
                    {
                        MessageBox.Show(string.Format("行{0}：{1}", i + 1, "计划翻包数量总和不可以超过发货数量！"), "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    jobTicketItem.StockInfoID = shipmentTicketItem.StockInfoID;
                    jobTicketItem.ShipmentTicketItemID = shipmentTicketItem.ID;
                    jobTicketItem.State = JobTicketItemViewMetaData.STRING_STATE_UNFINISHED;
                    jobTicketItem.Unit = shipmentTicketItem.Unit;
                    jobTicketItem.UnitAmount = shipmentTicketItem.UnitAmount;
                    jobTicketItem.RealAmount = 0;
                    shipmentTicketItem.ScheduledJobAmount = (shipmentTicketItem.ScheduledJobAmount ?? 0) + jobTicketItem.ScheduledAmount;
                    newJobTicket.JobTicketItem.Add(jobTicketItem);
                }
                if(newJobTicket.JobTicketItem.Count == 0)
                {
                    MessageBox.Show("至少选择一项！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(newJobTicket.JobTicketNo))
                {
                    DateTime createDay = new DateTime(shipmentTicket.CreateTime.Value.Year, shipmentTicket.CreateTime.Value.Month, shipmentTicket.CreateTime.Value.Day);
                    DateTime nextDay = createDay.AddDays(1);
                    int maxRankOfToday = Utilities.GetMaxTicketRankOfDay((from j in wmsEntities.JobTicket
                                                                          where j.CreateTime >= createDay && j.CreateTime < nextDay
                                                                          select j.JobTicketNo).ToArray());
                    newJobTicket.JobTicketNo = Utilities.GenerateTicketNo("Z", newJobTicket.CreateTime.Value, maxRankOfToday + 1);
                }
                wmsEntities.SaveChanges();
                ShipmentTicketUtilities.UpdateShipmentTicketStateSync(this.shipmentTicketID);
                if(MessageBox.Show("生成作业单成功，是否查看作业单？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    if(this.toJobTicketCallback == null)
                    {
                        throw new Exception("ToJobTicketCallback不允许为空！");
                    }
                    this.toJobTicketCallback(shipmentTicket.No);
                }
                if (!this.IsDisposed)
                {
                    this.Invoke(new Action(() =>
                    {
                        this.Close();
                    }));
                }
            }).Start();
        }

        private void buttonOK_MouseEnter(object sender, EventArgs e)
        {
            buttonOK.BackgroundImage = WMS.UI.Properties.Resources.bottonB2_s;
        }

        private void buttonOK_MouseLeave(object sender, EventArgs e)
        {
            buttonOK.BackgroundImage = WMS.UI.Properties.Resources.bottonB2_q;
        }

        private void buttonOK_MouseDown(object sender, MouseEventArgs e)
        {
            buttonOK.BackgroundImage = WMS.UI.Properties.Resources.bottonB3_q;
        }

        private void buttonSelectAll_Click(object sender, EventArgs e)
        {
            var worksheet = this.reoGridControlMain.CurrentWorksheet;
            for(int i = 0; i < this.validRows; i++)
            {
                worksheet[i, 1] = true;
            }
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            this.standardImportForm = new StandardImportForm<NewJobTicketItemData>(
                newJobTicketKeyNames,
                importHandler,
                null,
                "导入作业单条目"
                );
            this.standardImportForm.Show();
        }

        private bool importHandler(NewJobTicketItemData[] results, Dictionary<string, string[]> unimportedColumns)
        {
            List<Tuple<int, decimal>> idAndAmountList = new List<Tuple<int, decimal>>();
            try
            {
                WMSEntities wmsEntities = new WMSEntities();
                ShipmentTicketItemView[] shipmentTicketItemViews = (from s in wmsEntities.ShipmentTicketItemView where s.ShipmentTicketID == this.shipmentTicketID select s).ToArray();
                for (int i = 0; i < results.Length; i++)
                {
                    string supplyNoOrComponentName = results[i].SupplyNoOrComponentName;
                    decimal scheduleAmountNoUnit = results[i].ScheduleJobAmount * results[i].UnitAmount;
                    Supply supply = (from s in wmsEntities.Supply where s.No == supplyNoOrComponentName select s).FirstOrDefault();
                    DataAccess.Component component = null;
                    if (supply == null)
                    {
                        component = (from c in wmsEntities.Component where c.Name == supplyNoOrComponentName select c).FirstOrDefault();
                        if (component == null)
                        {
                            MessageBox.Show(string.Format("行{0}：不存在零件\"{1}\"！", i + 1, supplyNoOrComponentName), "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                    }
                    ShipmentTicketItemView[] selectedItems = null;
                    if (supply != null)
                    {
                        selectedItems = (from s in wmsEntities.ShipmentTicketItemView
                                         where s.ShipmentTicketID == this.shipmentTicketID
                                         && (s.ScheduledJobAmount ?? 0) < s.ShipmentAmount
                                         && s.SupplyNo == supplyNoOrComponentName
                                         orderby s.StockInfoInventoryDate ascending
                                         select s).ToArray();
                    }
                    else if (component != null)
                    {
                        selectedItems = (from s in wmsEntities.ShipmentTicketItemView
                                          where s.ShipmentTicketID == this.shipmentTicketID
                                          && (s.ScheduledJobAmount ?? 0) < s.ShipmentAmount
                                          && s.ComponentName == supplyNoOrComponentName
                                          orderby s.StockInfoInventoryDate ascending
                                          select s).ToArray();
                    }
                    decimal totalStockAmountNoUnit = selectedItems.Sum((item) => (item.ShipmentAmount - (item.ScheduledJobAmount ?? 0)) * (item.UnitAmount ?? 1)) ?? 0;
                    if(scheduleAmountNoUnit > totalStockAmountNoUnit)
                    {
                        MessageBox.Show(string.Format("行{0}：发货单剩余待分配翻包数量不足，剩余量：{1}", i + 1, totalStockAmountNoUnit), "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    decimal curAmountNoUnit = 0;
                    for (int j = 0; j < selectedItems.Length; j++)
                    {
                        decimal curItemRestAmount = (selectedItems[j].ShipmentAmount - (selectedItems[j].ScheduledJobAmount ?? 0)) ?? 0;
                        //当前项的剩余数量（不带单位）
                        decimal curItemRestAmountNoUnit = curItemRestAmount * (selectedItems[j].UnitAmount ?? 1);
                        //当前项的剩余数量小于要分配的数量
                        if (curAmountNoUnit + curItemRestAmountNoUnit < scheduleAmountNoUnit)
                        {
                            idAndAmountList.Add(new Tuple<int, decimal>(selectedItems[j].ID, curItemRestAmount));
                            curAmountNoUnit += curItemRestAmountNoUnit;
                        }
                        else //当前项的剩余数量大于等于要分配的数量
                        {
                            idAndAmountList.Add(new Tuple<int, decimal>(selectedItems[j].ID, ((scheduleAmountNoUnit - curAmountNoUnit) / selectedItems[i].UnitAmount ?? 1)));
                            curAmountNoUnit = scheduleAmountNoUnit;
                            break;
                        }
                    }
                }
                this.CheckItemsAndFillScheduleAmountByIDs(idAndAmountList);
                this.standardImportForm.Close();
                MessageBox.Show("导入成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            catch
            {
                MessageBox.Show("操作失败，请检查网络连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void CheckItemsAndFillScheduleAmountByIDs(List<Tuple<int,decimal>> idAndAmountList)
        {
            var worksheet = this.reoGridControlMain.CurrentWorksheet;
            for(int i = 0; i < this.validRows; i++)
            {
                if(int.TryParse(worksheet[i, 0].ToString(), out int id) == false)
                {
                    continue;
                }
                Tuple<int, decimal> item = (from t in idAndAmountList where t.Item1 == id select t).FirstOrDefault();
                if(item == null)
                {
                    continue;
                }
                worksheet[i, 1] = true;
                worksheet[i, 2] = item.Item2;
            }
        }
    }
}
