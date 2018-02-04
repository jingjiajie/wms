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


namespace WMS.UI
{
    public partial class FormPutOutStorageTicket : Form
    {
        WMSEntities wmsEntities = new WMSEntities();
        private PagerWidget<PutOutStorageTicketView> pagerWidget = null;
        private SearchWidget<PutOutStorageTicketView> searchWidget = null;

        int userID = -1;
        int projectID = -1;
        int warehouseID = -1;

        public FormPutOutStorageTicket(int userID, int projectID, int warehouseID)
        {
            InitializeComponent();
            InitComponents();
            this.userID = userID;
            this.projectID = projectID;
            this.warehouseID = warehouseID;

            this.pagerWidget = new PagerWidget<PutOutStorageTicketView>(this.reoGridControlMain, PutOutStorageTicketViewMetaData.KeyNames, this.projectID, this.warehouseID);
            this.panelPagerWidget.Controls.Add(this.pagerWidget);
            this.pagerWidget.Show();

            this.searchWidget = new SearchWidget<PutOutStorageTicketView>(PutOutStorageTicketViewMetaData.KeyNames, this.pagerWidget);
            this.panelSearchWidget.Controls.Add(this.searchWidget);
        }

        private void FormPutOutStorageTicket_Load(object sender, EventArgs e)
        {
            this.Search();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        private void InitComponents()
        {
            string[] visibleColumnNames = (from kn in PutOutStorageTicketViewMetaData.KeyNames
                                           where kn.Visible == true
                                           select kn.Name).ToArray();

            
        }

        public void SetSearchCondition(string key, string value)
        {
            this.searchWidget.SetSearchCondition(key, value);
        }

        private void Search(bool savePage = false, int selectID = -1)
        {
            this.searchWidget.Search(savePage, selectID);
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            var worksheet = this.reoGridControlMain.Worksheets[0];
            int[] ids = Utilities.GetSelectedIDs(this.reoGridControlMain);

            if (ids.Length != 1)
            {
                MessageBox.Show("请选择一项进行查看", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int putOutStorageTicketID = ids[0];
            var formPutOutStorageTicketItem = new FormPutOutStorageTicketItem(putOutStorageTicketID);
            formPutOutStorageTicketItem.SetPutOutStorageTicketStateChangedCallback(
                (id)=>
                {
                    if (this.IsDisposed) return;
                    this.Invoke(new Action(() =>
                    {
                        this.Search(true, id);
                    }));
                });
            formPutOutStorageTicketItem.Show();
        }

        private void buttonAlter_Click(object sender, EventArgs e)
        {
            var worksheet = this.reoGridControlMain.Worksheets[0];

            int[] ids = Utilities.GetSelectedIDs(this.reoGridControlMain);
            if (ids.Length != 1)
            {
                MessageBox.Show("请选择一项进行修改", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int putOutStorageTicketID = ids[0];
            var formPutOutStorageTicketModify = new FormPutOutStorageTicketModify(this.userID, putOutStorageTicketID);
            formPutOutStorageTicketModify.SetModifyFinishedCallback(() =>
            {
                this.Search();
            });
            formPutOutStorageTicketModify.Show();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            this.Search();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            int[] ids = this.GetSelectedIDs();
            if (ids.Length == 0)
            {
                MessageBox.Show("请选择要删除的项目", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("确定删除选中的项目吗？\n重要提示：\n删除后所有零件的计划装车数量将会退回翻包作业单，无视实际装车数量！\n请谨慎操作！", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            WMSEntities wmsEntities = new WMSEntities();
            try
            {
                //删除每个选中的出库单
                foreach (int id in ids)
                {
                    PutOutStorageTicket putOutStorageTicket = (from p in wmsEntities.PutOutStorageTicket
                                                               where p.ID == id
                                                               select p).FirstOrDefault();
                    if (putOutStorageTicket == null) continue;
                    if (putOutStorageTicket.State != PutOutStorageTicketViewMetaData.STRING_STATE_NOT_LOADED)
                    {
                        MessageBox.Show("删除失败，只能删除未装车的出库单！\n如果需要强行删除，请使用修改功能将出库单的状态改为未装车", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    //把没完成出库的条目在作业单的已分配出库数量里减去
                    var items = putOutStorageTicket.PutOutStorageTicketItem;
                    foreach (var item in items)
                    {
                        JobTicketItem jobTicketItem = (from j in wmsEntities.JobTicketItem
                                                       where j.ID == item.JobTicketItemID
                                                       select j).FirstOrDefault();
                        if (jobTicketItem == null) continue;
                        jobTicketItem.ScheduledPutOutAmount -= ((item.ScheduledAmount ?? 0) * (item.UnitAmount ?? 1)) / (jobTicketItem.UnitAmount ?? 1);
                    }
                    wmsEntities.PutOutStorageTicket.Remove(putOutStorageTicket);
                }
                wmsEntities.SaveChanges();
            }
            catch
            {
                MessageBox.Show("删除失败，请检查网络连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.Invoke(new Action(() =>
            {
                this.Search(true);
            }));
            MessageBox.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private int[] GetSelectedIDs()
        {
            var worksheet = this.reoGridControlMain.Worksheets[0];
            List<int> ids = new List<int>();
            for (int i = 0; i < worksheet.SelectionRange.Rows; i++)
            {
                try
                {
                    int curID = int.Parse(worksheet[i + worksheet.SelectionRange.Row, 0].ToString());
                    ids.Add(curID);
                }
                catch
                {
                    continue;
                }
            }
            return ids.ToArray();
        }

        private void textBoxSearchValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.Search();
            }
        }

        private void buttonDeliver_Click(object sender, EventArgs e)
        {
            int[] ids = Utilities.GetSelectedIDs(this.reoGridControlMain);
            WMSEntities wmsEntities = new WMSEntities();

            try
            {
                foreach (int id in ids)
                {
                    PutOutStorageTicket putOutStorageTicket = (from p in wmsEntities.PutOutStorageTicket
                                                               where p.ID == id
                                                               select p).FirstOrDefault();
                    string no = putOutStorageTicket.No;
                    if (putOutStorageTicket == null)
                    {
                        MessageBox.Show("出库单不存在，可能已被删除，请重新查询", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (putOutStorageTicket.State == PutOutStorageTicketViewMetaData.STRING_STATE_DELIVERED)
                    {
                        MessageBox.Show("单据"+ no +"已经发运，请不要重复发运", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (putOutStorageTicket.State == PutOutStorageTicketViewMetaData.STRING_STATE_PART_LOADED)
                    {
                        MessageBox.Show("单据"+ no +"正在装车中，必须全部装车完成才可以发运", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (putOutStorageTicket.State != PutOutStorageTicketItemViewMetaData.STRING_STATE_ALL_LOADED)
                    {
                        MessageBox.Show("未装车完成的出库单"+ no +"不能发运！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    putOutStorageTicket.State = PutOutStorageTicketViewMetaData.STRING_STATE_DELIVERED;
                    putOutStorageTicket.DeliverTime = DateTime.Now;
                }//End For

                if (MessageBox.Show("确定要发运选中项吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return;
                }
                wmsEntities.SaveChanges();
            }
            catch
            {
                MessageBox.Show("操作失败，请检查网络连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.Search(true);
            MessageBox.Show("发运成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        private void buttonPreview_Click(object sender, EventArgs e)
        {
            int[] ids = Utilities.GetSelectedIDs(this.reoGridControlMain);
            FormPutOutTicketChooseExcelType form = new FormPutOutTicketChooseExcelType(ids);
            form.Show();
        }

    }
}
