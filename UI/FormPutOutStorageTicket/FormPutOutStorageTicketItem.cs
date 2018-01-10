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
            this.Search();
        }

        private void InitComponents()
        {
            //初始化表格
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
            try
            {
                putOutStorageTicketItem = (from p in wmsEntities.PutOutStorageTicketItem where p.ID == id select p).FirstOrDefault();
            }
            catch
            {
                MessageBox.Show("修改失败，请检查网络连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (putOutStorageTicketItem == null)
            {
                MessageBox.Show("出库单条目不存在，请重新查询", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
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

            new Thread(() =>
            {
                try
                {
                    wmsEntities.SaveChanges();
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

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            int[] ids = Utilities.GetSelectedIDs(this.reoGridControlMain);
            if (ids.Length == 0)
            {
                MessageBox.Show("请选择要删除的项目", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            this.labelStatus.Text = "正在删除...";
            new Thread(new ThreadStart(()=>
            {
                WMSEntities wmsEntities = new WMSEntities();
                try
                {
                    foreach (int id in ids)
                    {
                        wmsEntities.Database.ExecuteSqlCommand("DELETE FROM PutOutStorageTicketItem WHERE ID = @id", new SqlParameter("@id", id));
                    }
                    wmsEntities.SaveChanges();
                }
                catch
                {
                    MessageBox.Show("删除失败，请检查网络连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                this.Search();
                MessageBox.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            })).Start();
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
            if (MessageBox.Show("确定要全额装车所有条目吗？（不会改变已经全部装车的条目）", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }
            new Thread(new ThreadStart(() =>
            {
                WMSEntities wmsEntities = new WMSEntities();
                try
                {
                    wmsEntities.Database.ExecuteSqlCommand(
                        String.Format(@"UPDATE PutOutStorageTicketItem SET State = '{0}',
                                        RealAmount = ScheduledAmount
                                        WHERE PutOutStorageTicketID = {1} AND State<>'{2}';",
                                    PutOutStorageTicketItemViewMetaData.STRING_STATE_ALL_LOAD,
                                    this.putOutStorageTicketID,
                                    PutOutStorageTicketItemViewMetaData.STRING_STATE_ALL_LOAD));
                    wmsEntities.Database.ExecuteSqlCommand(String.Format("UPDATE PutOutStorageTicket SET State = '{0}',TruckLoadingTime='{1}' WHERE ID = {2}", PutOutStorageTicketViewMetaData.STRING_STATE_LOADED,DateTime.Now, this.putOutStorageTicketID));
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
            if (tmpPutOutStorageTicketItem.RealAmount < tmpPutOutStorageTicketItem.ScheduledAmount)
            {
                comboBoxState.SelectedIndex = 1;
            }
            else
            {
                comboBoxState.SelectedIndex = 2;
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
    }
}
