﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WMS.DataAccess;
using System.Threading;
using unvell.ReoGrid.CellTypes;
using unvell.ReoGrid;

namespace WMS.UI
{
    public partial class FormPutOutStorageTicketNew : Form
    {
        private PagerWidget<JobTicketItemView> pagerWidget = null;
        private int jobTicketID = -1;
        private int userID = -1;
        private int projectID = -1;
        private int warehouseID = -1;

        private Func<int> personIDGetter = null;
        private volatile int validRows = 0;

        private int[] editableColumns = new int[] { 1, 2 };

        private Action<string,string> toPutOutStorageTicketCallback = null;

        private static KeyName[] newPutOutStorageTicketKeyNames =
        {
            new KeyName(){Key="SupplyNoOrComponentName",Name="零件代号/名称",NotNull=true},
            new KeyName(){Key="SchedulePutOutAmount",Name="计划装车数量",NotNull=true,Positive=true},
            new KeyName(){Key="UnitAmount",Name="单位数量",NotNull=true,Positive=true}
        };

        class NewPutOutStorageTicketItemData
        {
            private string supplyNoOrComponentName;
            private decimal schedulePutOutAmount;
            private decimal unitAmount;

            public string SupplyNoOrComponentName { get => supplyNoOrComponentName; set => supplyNoOrComponentName = value; }
            public decimal UnitAmount { get => unitAmount; set => unitAmount = value; }
            public decimal SchedulePutOutAmount { get => schedulePutOutAmount; set => schedulePutOutAmount = value; }
        }

        private StandardImportForm<NewPutOutStorageTicketItemData> standardImportForm = null;


        public void SetToPutOutStorageTicketCallback(Action<string,string> callback)
        {
            this.toPutOutStorageTicketCallback = callback;
        }

        public FormPutOutStorageTicketNew(int jobTicketID, int userID, int projectID, int warehouseID)
        {
            InitializeComponent();
            this.jobTicketID = jobTicketID;
            this.userID = userID;
            this.projectID = projectID;
            this.warehouseID = warehouseID;
        }

        private void BindButtonStyle(Button button)
        {
            button.MouseEnter += (obj, e) =>
            {
                button.BackgroundImage = WMS.UI.Properties.Resources.bottonB2_s;
            };
            button.MouseLeave += (obj, e) =>
            {
                button.BackgroundImage = WMS.UI.Properties.Resources.bottonB2_q;
            };
            button.MouseDown += (obj, e) =>
            {
                button.BackgroundImage = WMS.UI.Properties.Resources.bottonB3_q;
            };
        }

        private void FormPutOutStorageTicketNew_Load(object sender, EventArgs e)
        {
            this.InitComponents();
            Utilities.CreateEditPanel(this.tableLayoutEditPanel, PutOutStorageTicketViewMetaData.KeyNames);
            this.personIDGetter = Utilities.BindTextBoxSelect<FormSelectPerson, Person>(this, "textBoxPersonName", "Name");
            JobTicket jobTicket = null;
            User user = null;
            try
            {
                WMSEntities wmsEntities = new WMSEntities();
                jobTicket = (from s in wmsEntities.JobTicket
                                  where s.ID == jobTicketID
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
            if (jobTicket == null)
            {
                MessageBox.Show("作业单信息不存在，请刷新显示", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            if (user == null)
            {
                MessageBox.Show("登录失效，操作失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.Controls.Find("textBoxCreateUserUsername", true)[0].Text = user.Username;
            this.Controls.Find("textBoxCreateTime", true)[0].Text = DateTime.Now.ToString();
            this.Controls.Find("textBoxJobTicketJobTicketNo", true)[0].Text = jobTicket.JobTicketNo;
            this.Search();
        }

        private void InitComponents()
        {
            BindButtonStyle(this.buttonImport);
            BindButtonStyle(this.buttonSelectAll);
            this.reoGridControlMain.SetSettings(unvell.ReoGrid.WorkbookSettings.View_ShowSheetTabControl, false);
            this.pagerWidget = new PagerWidget<JobTicketItemView>(this.reoGridControlMain, JobTicketItemViewMetaData.KeyNames);
            this.pagerWidget.SetPageSize(-1);
        }

        private void Search()
        {
            this.pagerWidget.AddStaticCondition("JobTicketID", this.jobTicketID.ToString());
            this.pagerWidget.Search(false,-1,(results)=>
            {
                this.validRows = results.Length;

                if (this.IsDisposed) return;
                this.Invoke(new Action(() =>
                {
                    var worksheet = this.reoGridControlMain.Worksheets[0];
                    worksheet.SelectionMode = unvell.ReoGrid.WorksheetSelectionMode.Cell;
                    worksheet.InsertColumns(1, 2);
                    worksheet.ColumnHeaders[1].Text = "选择";
                    worksheet.ColumnHeaders[2].Text = "计划出库数量";
                    worksheet.BeforeCellEdit += (s, e) =>
                    {
                        e.IsCancelled = !(editableColumns.Contains(e.Cell.Column) && e.Cell.Row < validRows);
                    };
                    for (int i = 0; i < results.Length; i++)
                    {
                        JobTicketItemView curJobTicketItemView = results[i];
                        if (curJobTicketItemView.RealAmount == curJobTicketItemView.ScheduledPutOutAmount)
                        {
                            worksheet.Cells[i, 1].Style.BackColor = Color.LightGray;
                            worksheet.Cells[i, 1].IsReadOnly = true;
                        }
                        else
                        {
                            worksheet[i, 1] = new CheckBoxCell(false); //显示复选框
                        }
                        //上颜色，边框
                        worksheet.Cells[i, 2].Style.BackColor = Color.AliceBlue;
                        worksheet.SetRangeBorders(i, 2, 1, 1, unvell.ReoGrid.BorderPositions.All, unvell.ReoGrid.RangeBorderStyle.SilverSolid);
                        //计划翻包数量
                        worksheet.Cells[i, 2].DataFormat = unvell.ReoGrid.DataFormat.CellDataFormatFlag.Text;
                        worksheet[i, 2] = Utilities.DecimalToString((curJobTicketItemView.RealAmount - (curJobTicketItemView.ScheduledPutOutAmount ?? 0)) ?? 0); //计划出库数量默认等于实际作业数量-已经计划出库过的数量
                    }
                }));
            });
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.reoGridControlMain.CurrentWorksheet.EndEdit(new EndEditReason());
            PutOutStorageTicket newPutOutStorageTicket = new PutOutStorageTicket();
            if (Utilities.CopyTextBoxTextsToProperties(this, newPutOutStorageTicket, PutOutStorageTicketViewMetaData.KeyNames, out string errorMesage) == false)
            {
                MessageBox.Show(errorMesage, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if(Utilities.CopyComboBoxsToProperties(this,newPutOutStorageTicket, PutOutStorageTicketViewMetaData.KeyNames) == false)
            {
                MessageBox.Show("内部错误：读取复选框数据失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            List<int> checkedLines = new List<int>();
            var worksheet = this.reoGridControlMain.Worksheets[0];
            for (int i = 0; i < this.reoGridControlMain.Worksheets[0].Rows; i++)
            {
                if ((worksheet[i, 1] as bool? ?? false) == false)
                {
                    continue;
                }
                checkedLines.Add(i);
            }
            if(checkedLines.Count == 0)
            {
                MessageBox.Show("至少选择一项！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            new Thread(() =>
            {
                WMSEntities wmsEntities = new WMSEntities();
                wmsEntities.PutOutStorageTicket.Add(newPutOutStorageTicket);

                JobTicket jobTicket = (from s in wmsEntities.JobTicket
                                                 where s.ID == this.jobTicketID
                                                 select s).FirstOrDefault();

                if (jobTicket == null)
                {
                    MessageBox.Show("作业单不存在，请重新查询", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                newPutOutStorageTicket.State = PutOutStorageTicketViewMetaData.STRING_STATE_NOT_LOADED;
                newPutOutStorageTicket.JobTicketID = jobTicket.ID;
                newPutOutStorageTicket.ProjectID = this.projectID;
                newPutOutStorageTicket.WarehouseID = this.warehouseID;
                newPutOutStorageTicket.CreateUserID = this.userID;
                newPutOutStorageTicket.CreateTime = DateTime.Now;
                int personID = this.personIDGetter();
                if(personID != -1)
                {
                    newPutOutStorageTicket.PersonID = personID;
                }

                //把选中的条目加进出库单
                foreach (int line in checkedLines)
                {
                    int id = int.Parse(worksheet[line, 0].ToString());
                    JobTicketItem jobTicketItem = (from j in wmsEntities.JobTicketItem
                                                   where j.ID == id
                                                   select j).FirstOrDefault();
                    if (jobTicket == null)
                    {
                        MessageBox.Show("无法找到作业单条目，请重新查询", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    PutOutStorageTicketItem newPutOutStorageTicketItem = new PutOutStorageTicketItem();

                    if (Utilities.CopyTextToProperty(worksheet[line, 2].ToString(), "ScheduledAmount", newPutOutStorageTicketItem, PutOutStorageTicketItemViewMetaData.KeyNames, out string errorMessage) == false)
                    {
                        MessageBox.Show(errorMesage, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if(newPutOutStorageTicketItem.ScheduledAmount <= 0)
                    {
                        MessageBox.Show("行" + (line + 1) + "：计划出库数量必须大于0！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (newPutOutStorageTicketItem.ScheduledAmount + (jobTicketItem.ScheduledPutOutAmount ?? 0) > jobTicketItem.RealAmount)
                    {
                        MessageBox.Show("行" + (line+1) + "：计划出库数量不能大于实际翻包完成数量！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    jobTicketItem.ScheduledPutOutAmount = (jobTicketItem.ScheduledPutOutAmount ?? 0) + newPutOutStorageTicketItem.ScheduledAmount;
                    newPutOutStorageTicketItem.StockInfoID = jobTicketItem.StockInfoID;
                    newPutOutStorageTicketItem.JobTicketItemID = jobTicketItem.ID;
                    newPutOutStorageTicketItem.RealAmount = 0;
                    newPutOutStorageTicketItem.Unit = jobTicketItem.Unit;
                    newPutOutStorageTicketItem.UnitAmount = jobTicketItem.UnitAmount;
                    newPutOutStorageTicketItem.ReturnQualityUnit = "个";
                    newPutOutStorageTicketItem.ReturnQualityUnitAmount = 1;
                    newPutOutStorageTicketItem.ReturnRejectUnit = "个";
                    newPutOutStorageTicketItem.ReturnRejectUnitAmount = 1;
                    newPutOutStorageTicket.PutOutStorageTicketItem.Add(newPutOutStorageTicketItem);
                }
                //生成出库单号
                if (string.IsNullOrWhiteSpace(newPutOutStorageTicket.No))
                {
                    DateTime createDay = new DateTime(jobTicket.CreateTime.Value.Year, jobTicket.CreateTime.Value.Month, jobTicket.CreateTime.Value.Day);
                    DateTime nextDay = createDay.AddDays(1);
                    int maxRankOfToday = Utilities.GetMaxTicketRankOfDay((from p in wmsEntities.PutOutStorageTicket
                                                                          where p.CreateTime >= createDay && p.CreateTime < nextDay
                                                                          select p.No).ToArray());
                    newPutOutStorageTicket.No = Utilities.GenerateTicketNo("C", newPutOutStorageTicket.CreateTime.Value, maxRankOfToday + 1);
                }
                wmsEntities.SaveChanges();
                if (MessageBox.Show("生成出库单成功，是否查看出库单？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    if (this.toPutOutStorageTicketCallback == null)
                    {
                        throw new Exception("toPutOutStorageTicketCallback不可以为空！");
                    }
                    this.toPutOutStorageTicketCallback("No",newPutOutStorageTicket.No);
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

        private enum SelectButtonMode { SELECT_ALL, SELECT_NONE };
        SelectButtonMode selectButtonMode = SelectButtonMode.SELECT_ALL;
        private void buttonSelectAll_Click(object sender, EventArgs e)
        {
            var worksheet = this.reoGridControlMain.CurrentWorksheet;
            if (selectButtonMode == SelectButtonMode.SELECT_ALL)
            {
                this.buttonSelectAll.Text = "全不选";
                selectButtonMode = SelectButtonMode.SELECT_NONE;
                for (int i = 0; i < this.validRows; i++)
                {
                    worksheet[i, 1] = true;
                }
            }
            else
            {
                this.buttonSelectAll.Text = "全选";
                selectButtonMode = SelectButtonMode.SELECT_ALL;
                for (int i = 0; i < this.validRows; i++)
                {
                    worksheet[i, 1] = false;
                }
            }

        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            this.standardImportForm = new StandardImportForm<NewPutOutStorageTicketItemData>(
                newPutOutStorageTicketKeyNames,
                importHandler,
                null,
                "导入出库单条目"
                );
            this.standardImportForm.Show();
        }

        private bool importHandler(NewPutOutStorageTicketItemData[] results, Dictionary<string, string[]> unimportedColumns)
        {
            List<Tuple<int, decimal>> idAndAmountList = new List<Tuple<int, decimal>>();
            try
            {
                WMSEntities wmsEntities = new WMSEntities();
                JobTicketItemView[] jobTicketItemViews = (from j in wmsEntities.JobTicketItemView where j.JobTicketID == this.jobTicketID select j).ToArray();
                for (int i = 0; i < results.Length; i++)
                {
                    string supplyNoOrComponentName = results[i].SupplyNoOrComponentName;
                    decimal scheduleAmountNoUnit = results[i].SchedulePutOutAmount * results[i].UnitAmount;
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
                    JobTicketItemView[] selectedItems = null;
                    if (supply != null)
                    {
                        selectedItems = (from s in wmsEntities.JobTicketItemView
                                         where s.JobTicketID == this.jobTicketID
                                         && (s.ScheduledPutOutAmount ?? 0) < s.ScheduledAmount
                                         && s.SupplyNo == supplyNoOrComponentName
                                         orderby s.StockInfoInventoryDate ascending
                                         select s).ToArray();
                    }
                    else if (component != null)
                    {
                        selectedItems = (from s in wmsEntities.JobTicketItemView
                                         where s.JobTicketID == this.jobTicketID
                                         && (s.ScheduledPutOutAmount ?? 0) < s.ScheduledAmount
                                         && s.ComponentName == supplyNoOrComponentName
                                         orderby s.StockInfoInventoryDate ascending
                                         select s).ToArray();
                    }
                    decimal totalStockAmountNoUnit = selectedItems.Sum((item) => (item.ScheduledAmount - (item.ScheduledPutOutAmount ?? 0)) * (item.UnitAmount ?? 1)) ?? 0;
                    if (scheduleAmountNoUnit > totalStockAmountNoUnit)
                    {
                        MessageBox.Show(string.Format("行{0}：翻包作业单剩余待分配翻包数量不足，剩余量：{1}", i + 1, totalStockAmountNoUnit), "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    decimal curAmountNoUnit = 0;
                    for (int j = 0; j < selectedItems.Length; j++)
                    {
                        decimal curItemRestAmount = (selectedItems[j].ScheduledAmount - (selectedItems[j].ScheduledPutOutAmount ?? 0)) ?? 0;
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

        private void CheckItemsAndFillScheduleAmountByIDs(List<Tuple<int, decimal>> idAndAmountList)
        {
            var worksheet = this.reoGridControlMain.CurrentWorksheet;
            for (int i = 0; i < this.validRows; i++)
            {
                if (int.TryParse(worksheet[i, 0].ToString(), out int id) == false)
                {
                    continue;
                }
                Tuple<int, decimal> item = (from t in idAndAmountList where t.Item1 == id select t).FirstOrDefault();
                if (item == null)
                {
                    continue;
                }
                worksheet[i, 1] = true;
                worksheet[i, 2] = item.Item2;
            }
        }
    }
}