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
    public partial class FormPutOutStorageTicketItem : Form
    {
        private int putOutStorageTicketID = -1;
        private Action<int> putOutStorageTicketStateChangedCallback = null;

        private TextBox textBoxUnit = null;
        private TextBox textBoxUnitAmount = null;
        private TextBox textBoxLoadingTime = null;
        private TextBox textBoxReturnQualityAmount = null;
        private TextBox textBoxReturnQualityUnit = null;
        private TextBox textBoxReturnQualityUnitAmount = null;
        private TextBox textBoxReturnRejectAmount = null;
        private TextBox textBoxReturnRejectUnit = null;
        private TextBox textBoxReturnRejectUnitAmount = null;
        private TextBox textBoxReturnTime = null;

        private Func<int> jobPersonGetter = null;
        private Func<int> confirmPersonGetter = null;

        private KeyName[] visibleColumns = (from kn in PutOutStorageTicketItemViewMetaData.KeyNames
                                            where kn.Visible == true
                                            select kn).ToArray();

        public FormPutOutStorageTicketItem(int putOutStorageTicketID = -1)
        {
            InitializeComponent();
            this.putOutStorageTicketID = putOutStorageTicketID;
        }

        public void SetPutOutStorageTicketStateChangedCallback(Action<int> callback)
        {
            this.putOutStorageTicketStateChangedCallback = callback;
        }

        private void FormPutOutStorageTicketItem_Load(object sender, EventArgs e)
        {
            InitComponents();
            this.textBoxReturnQualityAmount = (TextBox)this.Controls.Find("textBoxReturnQualityAmount", true)[0];
            this.textBoxReturnQualityUnit = (TextBox)this.Controls.Find("textBoxReturnQualityUnit", true)[0];
            this.textBoxReturnQualityUnitAmount = (TextBox)this.Controls.Find("textBoxReturnQualityUnitAmount", true)[0];
            this.textBoxReturnRejectAmount = (TextBox)this.Controls.Find("textBoxReturnRejectAmount", true)[0];
            this.textBoxReturnRejectUnit = (TextBox)this.Controls.Find("textBoxReturnRejectUnit", true)[0];
            this.textBoxReturnRejectUnitAmount = (TextBox)this.Controls.Find("textBoxReturnRejectUnitAmount", true)[0];
            this.textBoxReturnTime = (TextBox)this.Controls.Find("textBoxReturnTime", true)[0];
            this.textBoxLoadingTime = (TextBox)this.Controls.Find("textBoxLoadingTime", true)[0];

            WMSEntities wmsEntities = new WMSEntities();
            PutOutStorageTicket putOutStorageTicket = null;
            try
            {
                putOutStorageTicket = (from p in wmsEntities.PutOutStorageTicket where p.ID == putOutStorageTicketID select p).FirstOrDefault();
            }
            catch
            {
                MessageBox.Show("加载数据失败，请检查网络连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            if (putOutStorageTicket == null)
            {
                MessageBox.Show("出库单信息不存在，请刷新查询", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            //已发运不能修改装车信息
            if (putOutStorageTicket.State == PutOutStorageTicketViewMetaData.STRING_STATE_DELIVERED)
            {
                this.buttonAllLoad.Enabled = false;
                this.buttonAllLoad.Text = "已发运不能修改装车状态";
                this.buttonLoad.Enabled = false;
                this.buttonLoad.Text = "已发运不能修改装车状态";
                TextBox textBoxRealAmount = (TextBox)this.Controls.Find("textBoxRealAmount", true)[0];
                textBoxRealAmount.ReadOnly = true;
                this.textBoxLoadingTime.ReadOnly = true;
            }
            else //未发运不能修改退回信息
            {
                this.buttonReturn.Text = "发运前不能修改退回信息";
                this.buttonReturn.Enabled = false;
                this.textBoxReturnQualityAmount.ReadOnly = true;
                this.textBoxReturnQualityUnit.ReadOnly = true;
                this.textBoxReturnQualityUnitAmount.ReadOnly = true;
                this.textBoxReturnRejectAmount.ReadOnly = true;
                this.textBoxReturnRejectUnit.ReadOnly = true;
                this.textBoxReturnRejectUnitAmount.ReadOnly = true;
            }
            this.jobPersonGetter = Utilities.BindTextBoxSelect<FormSelectPerson, Person>(this, "textBoxJobPersonName", "Name");
            this.confirmPersonGetter = Utilities.BindTextBoxSelect<FormSelectPerson, Person>(this, "textBoxConfirmPersonName", "Name");
            this.textBoxUnit = (TextBox)this.Controls.Find("textBoxUnit", true)[0];
            this.textBoxUnitAmount = (TextBox)this.Controls.Find("textBoxUnitAmount", true)[0];
            this.Search();
        }

        private void InitComponents()
        {
            //初始化表格
            this.reoGridControlMain.SetSettings(WorkbookSettings.View_ShowSheetTabControl, false);
            var worksheet = this.reoGridControlMain.Worksheets[0];
            worksheet.SelectionMode = WorksheetSelectionMode.Row;

            for (int i = 0; i < PutOutStorageTicketItemViewMetaData.KeyNames.Length; i++)
            {
                worksheet.ColumnHeaders[i].Text = PutOutStorageTicketItemViewMetaData.KeyNames[i].Name;
                worksheet.ColumnHeaders[i].IsVisible = PutOutStorageTicketItemViewMetaData.KeyNames[i].Visible;
            }
            worksheet.Columns = PutOutStorageTicketItemViewMetaData.KeyNames.Length; //限制表的长度

            Utilities.CreateEditPanel(this.tableLayoutPanelProperties, PutOutStorageTicketItemViewMetaData.KeyNames);
            worksheet.SelectionRangeChanged += worksheet_SelectionRangeChanged;
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
            int[] ids = this.GetSelectedIDs();
            if(ids.Length == 0)
            {
                return;
            }

            int id = ids[0];
            this.labelStatus.Text = "加载中...";
            new Thread(new ThreadStart(()=>
            {
                WMSEntities wmsEntities = new WMSEntities();
                PutOutStorageTicketItemView putOutStorageTicketItemView = null;
                try
                {
                    putOutStorageTicketItemView = (from p in wmsEntities.PutOutStorageTicketItemView
                                                                               where p.ID == id
                                                                               select p).FirstOrDefault();
                }
                catch
                {
                    MessageBox.Show("加载数据失败，请检查网络连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (putOutStorageTicketItemView == null)
                {
                    MessageBox.Show("出库单条目不存在，请重新查询", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                this.Invoke(new Action(()=>
                {
                    this.labelStatus.Text = "加载完成";
                    Utilities.CopyPropertiesToTextBoxes(putOutStorageTicketItemView, this);
                    Utilities.CopyPropertiesToComboBoxes(putOutStorageTicketItemView, this);
                }));
            })).Start();
        }

        private void Search(int selectID = -1)
        {
            var worksheet = this.reoGridControlMain.Worksheets[0];

            worksheet[0, 1] = "加载中...";
            new Thread(new ThreadStart(() =>
            {
                WMSEntities wmsEntities = new WMSEntities();
                PutOutStorageTicketItemView[] putOutStorageTicketItemViews = (from j in wmsEntities.PutOutStorageTicketItemView
                                                                              where j.PutOutStorageTicketID == this.putOutStorageTicketID
                                                                              orderby j.ID descending
                                                                              select j).ToArray();

                this.reoGridControlMain.Invoke(new Action(() =>
                {
                    this.labelStatus.Text = "加载完成";
                    worksheet.DeleteRangeData(RangePosition.EntireRange);
                    worksheet.Rows = putOutStorageTicketItemViews.Length < 10 ? 10 : putOutStorageTicketItemViews.Length;
                    if (putOutStorageTicketItemViews.Length == 0)
                    {
                        worksheet[0, 1] = "没有符合条件的记录";
                    }
                    for (int i = 0; i < putOutStorageTicketItemViews.Length; i++)
                    {
                        var curPutOutStorageTicketViews = putOutStorageTicketItemViews[i];
                        object[] columns = Utilities.GetValuesByPropertieNames(curPutOutStorageTicketViews, (from kn in PutOutStorageTicketItemViewMetaData.KeyNames select kn.Key).ToArray());
                        for (int j = 0; j < columns.Length; j++)
                        {
                            worksheet[i, j] = columns[j] == null ? "" : columns[j].ToString();
                        }
                    }
                    if(selectID != -1)
                    {
                        Utilities.SelectLineByID(this.reoGridControlMain, selectID);
                    }
                    this.RefreshTextBoxes();
                }));
            })).Start();
        }

      
        private int[] GetSelectedIDs()
        {
            List<int> ids = new List<int>();
            var worksheet = this.reoGridControlMain.Worksheets[0];
            for (int row = worksheet.SelectionRange.Row; row <= worksheet.SelectionRange.EndRow; row++)
            {
                if (worksheet[row, 0] == null) continue;
                if (int.TryParse(worksheet[row, 0].ToString(), out int putOutStorageTicketID))
                {
                    ids.Add(putOutStorageTicketID);
                }
            }
            return ids.ToArray();
        }

        private void buttonModify_Click(object sender, EventArgs e)
        {
            int[] ids = Utilities.GetSelectedIDs(this.reoGridControlMain);
            if (ids.Length != 1)
            {
                MessageBox.Show("请选择一项进行修改", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int id = ids[0];

            WMSEntities wmsEntities = new WMSEntities();
            PutOutStorageTicketItem putOutStorageTicketItem = null;
            StockInfo stockInfo = null;
            try
            {
                putOutStorageTicketItem = (from p in wmsEntities.PutOutStorageTicketItem where p.ID == id select p).FirstOrDefault();
                if (putOutStorageTicketItem == null)
                {
                    MessageBox.Show("出库单条目不存在，请重新查询", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                stockInfo = (from s in wmsEntities.StockInfo where s.ID == putOutStorageTicketItem.StockInfoID select s).FirstOrDefault();
            }
            catch
            {
                MessageBox.Show("修改失败，请检查网络连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            decimal oriReturnQualityAmount = putOutStorageTicketItem.ReturnQualityAmount * (putOutStorageTicketItem.ReturnQualityUnitAmount ?? 1);
            decimal oriReturnRejectAmount = putOutStorageTicketItem.ReturnRejectAmount * (putOutStorageTicketItem.ReturnRejectUnitAmount ?? 1);

            if (Utilities.CopyTextBoxTextsToProperties(this, putOutStorageTicketItem, PutOutStorageTicketItemViewMetaData.KeyNames, out string errorMessage) == false)
            {
                MessageBox.Show(errorMessage, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Utilities.CopyComboBoxsToProperties(this, putOutStorageTicketItem, PutOutStorageTicketItemViewMetaData.KeyNames) == false)
            {
                MessageBox.Show("内部错误：读取复选框数据失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(putOutStorageTicketItem.RealAmount < 0 || putOutStorageTicketItem.RealAmount > putOutStorageTicketItem.ScheduledAmount)
            {
                MessageBox.Show("实际装车数量必须大于等于0并且小于计划装车数量", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            decimal? returnQualityAmount = putOutStorageTicketItem.ReturnQualityAmount * putOutStorageTicketItem.ReturnQualityUnitAmount;
            decimal? returnRejectAmount = putOutStorageTicketItem.ReturnRejectAmount * putOutStorageTicketItem.ReturnRejectUnitAmount;
            decimal? deliverAmount = putOutStorageTicketItem.RealAmount * putOutStorageTicketItem.UnitAmount;
            if (returnQualityAmount + returnRejectAmount > deliverAmount)
            {
                MessageBox.Show("正品返回数量与不良品返回数量之和不能超过实际发货数量！","提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int jobPersonID = this.jobPersonGetter();
            int confirmPersonID = this.confirmPersonGetter();
            putOutStorageTicketItem.JobPersonID = jobPersonID == -1 ? null : (int?)jobPersonID;
            putOutStorageTicketItem.ConfirmPersonID = confirmPersonID == -1 ? null : (int?)confirmPersonID;
            if(stockInfo != null)
            {
                decimal deltaReturnQualityAmount = putOutStorageTicketItem.ReturnQualityAmount * (putOutStorageTicketItem.ReturnQualityUnitAmount ?? 1) - oriReturnQualityAmount;
                decimal deltaReturnRejectAmount = putOutStorageTicketItem.ReturnRejectAmount * (putOutStorageTicketItem.ReturnRejectUnitAmount?? 1) - oriReturnRejectAmount;
                stockInfo.ShipmentAreaAmount += deltaReturnQualityAmount;
                stockInfo.RejectAreaAmount += deltaReturnRejectAmount;
            }
            new Thread(() =>
            {
                try
                {
                    wmsEntities.SaveChanges();
                    this.UpdatePutOutStorageTicketStateSync();
                }
                catch
                {
                    MessageBox.Show("修改失败，请检查网络连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                this.Invoke(new Action(() =>
                {
                    this.Search(putOutStorageTicketItem.ID);
                }));
                MessageBox.Show("修改成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }).Start();
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

        private void buttonAllLoad_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要全额装车所有条目吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }
            new Thread(new ThreadStart(() =>
            {
                WMSEntities wmsEntities = new WMSEntities();
                try
                {
                    string sql = String.Format(@"UPDATE PutOutStorageTicketItem SET State = '{0}',
                                        LoadingTime = '{1}',
                                        RealAmount = ScheduledAmount
                                        WHERE PutOutStorageTicketID = {2} AND (RealAmount <> ScheduledAmount OR State<>'{3}');",
                                    PutOutStorageTicketItemViewMetaData.STRING_STATE_ALL_LOADED,
                                    DateTime.Now,
                                    this.putOutStorageTicketID,
                                    PutOutStorageTicketItemViewMetaData.STRING_STATE_ALL_LOADED);
                    wmsEntities.Database.ExecuteSqlCommand(sql);
                    wmsEntities.Database.ExecuteSqlCommand(String.Format("UPDATE PutOutStorageTicket SET State = '{0}' WHERE ID = {1}", PutOutStorageTicketViewMetaData.STRING_STATE_ALL_LOADED, this.putOutStorageTicketID));
                    wmsEntities.SaveChanges();
                }
                catch
                {
                    MessageBox.Show("操作失败，请检查网络连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                this.putOutStorageTicketStateChangedCallback?.Invoke(this.putOutStorageTicketID);
                this.Invoke(new Action(() => this.Search()));
                MessageBox.Show("操作成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            })).Start();
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            ComboBox comboBoxState = (ComboBox)this.Controls.Find("comboBoxState", true)[0];
            TextBox textBoxScheduledAmount = (TextBox)this.Controls.Find("textBoxScheduledAmount", true)[0];
            TextBox textBoxRealAmount = (TextBox)this.Controls.Find("textBoxRealAmount", true)[0];
            TextBox textBoxLoadingTime = (TextBox)this.Controls.Find("textBoxLoadingTime", true)[0];
            if (string.IsNullOrWhiteSpace(textBoxRealAmount.Text))
            {
                MessageBox.Show("请填写实际装车数量！","提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            PutOutStorageTicketItem tmpPutOutStorageTicketItem = new PutOutStorageTicketItem();
            if (Utilities.CopyTextBoxTextsToProperties(this, tmpPutOutStorageTicketItem, PutOutStorageTicketItemViewMetaData.KeyNames, out string errorMessage) == false)
            {
                MessageBox.Show(errorMessage, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if(tmpPutOutStorageTicketItem.RealAmount == 0)
            {
                comboBoxState.SelectedIndex = 0;
            }
            else if (tmpPutOutStorageTicketItem.RealAmount < tmpPutOutStorageTicketItem.ScheduledAmount)
            {
                comboBoxState.SelectedIndex = 1;
            }
            else
            {
                comboBoxState.SelectedIndex = 2;
            }
            if (string.IsNullOrWhiteSpace(textBoxLoadingTime.Text))
            {
                textBoxLoadingTime.Text = DateTime.Now.ToString();
            }
            this.buttonModify.PerformClick();
        }

        private void buttonAllLoad_MouseDown(object sender, MouseEventArgs e)
        {
            buttonAllLoad.BackgroundImage = WMS.UI.Properties.Resources.bottonB3_q;
        }

        private void buttonLoad_MouseDown(object sender, MouseEventArgs e)
        {
            buttonLoad.BackgroundImage = WMS.UI.Properties.Resources.bottonB3_q;
        }

        private void buttonLoad_MouseEnter(object sender, EventArgs e)
        {
            buttonLoad.BackgroundImage = WMS.UI.Properties.Resources.bottonB1_s;
        }

        private void buttonAllLoad_MouseEnter(object sender, EventArgs e)
        {
            buttonAllLoad.BackgroundImage = WMS.UI.Properties.Resources.bottonB1_s;
        }

        private void buttonAllLoad_MouseLeave(object sender, EventArgs e)
        {
            buttonAllLoad.BackgroundImage = WMS.UI.Properties.Resources.bottonB2_s;
        }

        private void buttonLoad_MouseLeave(object sender, EventArgs e)
        {
            buttonLoad.BackgroundImage = WMS.UI.Properties.Resources.bottonB2_s;
        }


        private void UpdatePutOutStorageTicketStateSync()
        {
            WMSEntities wmsEntities = new WMSEntities();
            PutOutStorageTicket putOutStorageTicket = (from p in wmsEntities.PutOutStorageTicket where p.ID == this.putOutStorageTicketID select p).FirstOrDefault();
            if (putOutStorageTicket == null) return;
            //已发运的单子不改变已发运状态
            if (putOutStorageTicket.State == PutOutStorageTicketViewMetaData.STRING_STATE_DELIVERED) return;
            //否则更新装车状态
            int totalItemCount = wmsEntities.Database.SqlQuery<int>(string.Format("SELECT COUNT(*) FROM PutOutStorageTicketItem WHERE PutOutStorageTicketID = {0}", this.putOutStorageTicketID)).Single();
            int allfinishedItemCount = wmsEntities.Database.SqlQuery<int>(String.Format("SELECT COUNT(*) FROM PutOutStorageTicketItem WHERE PutOutStorageTicketID = {0} AND State = '{1}'", this.putOutStorageTicketID, PutOutStorageTicketItemViewMetaData.STRING_STATE_ALL_LOADED)).Single();
            int unfinishedItemCount = wmsEntities.Database.SqlQuery<int>(String.Format("SELECT COUNT(*) FROM PutOutStorageTicketItem WHERE PutOutStorageTicketID = {0} AND State = '{1}'", this.putOutStorageTicketID, PutOutStorageTicketItemViewMetaData.STRING_STATE_WAIT_FOR_LOAD)).Single();
            string putOutStorageTicketState = null;
            if (unfinishedItemCount == totalItemCount)
            {
                putOutStorageTicketState = PutOutStorageTicketViewMetaData.STRING_STATE_NOT_LOADED;
            }
            else if (allfinishedItemCount == totalItemCount)
            {
                putOutStorageTicketState = PutOutStorageTicketViewMetaData.STRING_STATE_ALL_LOADED;
            }
            else
            {
                putOutStorageTicketState = PutOutStorageTicketViewMetaData.STRING_STATE_PART_LOADED;
            }

            try
            {
                wmsEntities.Database.ExecuteSqlCommand(String.Format("UPDATE PutOutStorageTicket SET State = '{0}' WHERE ID = {1}", putOutStorageTicketState, this.putOutStorageTicketID));
                wmsEntities.SaveChanges();
            }
            catch
            {
                MessageBox.Show("更新出库单状态失败，请检查网络连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (this.IsDisposed) return;
            this.Invoke(new Action(() =>
            {
                this.putOutStorageTicketStateChangedCallback?.Invoke(this.putOutStorageTicketID);
            }));
        }

        private void buttonReturn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxReturnTime.Text))
            {
                textBoxReturnTime.Text = DateTime.Now.ToString();
            }
            this.buttonModify.PerformClick();
        }

        private void buttonReturn_MouseDown(object sender, MouseEventArgs e)
        {
            buttonReturn.BackgroundImage = WMS.UI.Properties.Resources.bottonB3_q;
        }

        private void buttonReturn_MouseEnter(object sender, EventArgs e)
        {
            buttonReturn.BackgroundImage = WMS.UI.Properties.Resources.bottonB1_s;
        }

        private void buttonReturn_MouseLeave(object sender, EventArgs e)
        {
            buttonReturn.BackgroundImage = WMS.UI.Properties.Resources.bottonB3_q;
        }
    }
}
