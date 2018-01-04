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
using System.Reflection;
using System.Data.SqlClient;

namespace WMS.UI
{
    public partial class FormShipmentTicket : Form
    {
        PagerWidget<ShipmentTicketView> pagerWidget = null;
        WMSEntities wmsEntities = new WMSEntities();
        int userID = -1;
        int projectID = -1;
        int warehouseID = -1;

        private Action<string> toJobTicketCallback = null;

        public void SetToJobTicketCallback(Action<string> callback)
        {
            this.toJobTicketCallback = callback;
        }

        public FormShipmentTicket(int userID,int projectID,int warehouseID)
        {
            InitializeComponent();
            this.userID = userID;
            this.projectID = projectID;
            this.warehouseID = warehouseID;
        }

        private void FormShipmentTicket_Load(object sender, EventArgs e)
        {
            InitComponents();
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
            this.wmsEntities.Database.Connection.Open();

            string[] visibleColumnNames = (from kn in ShipmentTicketViewMetaData.KeyNames
                                           where kn.Visible == true
                                           select kn.Name).ToArray();

            //初始化
            this.comboBoxSearchCondition.Items.Add("无");
            this.comboBoxSearchCondition.Items.AddRange(visibleColumnNames);
            this.comboBoxSearchCondition.SelectedIndex = 0;

            this.pagerWidget = new PagerWidget<ShipmentTicketView>(this.reoGridControlMain, ShipmentTicketViewMetaData.KeyNames, this.projectID, this.warehouseID);
            this.panelPagerWidget.Controls.Add(pagerWidget);
            this.pagerWidget.Show();
        }

        private void Search()
        {
            this.pagerWidget.ClearCondition();
            if(this.comboBoxSearchCondition.SelectedIndex != 0)
            {
                this.pagerWidget.AddCondition(this.comboBoxSearchCondition.SelectedItem.ToString(),this.textBoxSearchValue.Text);
            }
            this.pagerWidget.Search();
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            var worksheet = this.reoGridControlMain.Worksheets[0];
            try
            {
                if (worksheet.SelectionRange.Rows != 1)
                {
                    throw new Exception();
                }
                int shipmentTicketID = int.Parse(worksheet[worksheet.SelectionRange.Row, 0].ToString());
                var formShipmentTicketItem = new FormShipmentTicketItem(shipmentTicketID);
                formShipmentTicketItem.SetShipmentTicketStateChangedCallback(() =>
                {
                    this.Invoke(new Action(this.Search));
                });
                formShipmentTicketItem.Show();
            }
            catch
            {
                MessageBox.Show("请选择一项进行查看", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = new FormShipmentTicketModify(this.projectID,this.warehouseID,this.userID);
            form.SetMode(FormMode.ADD);
            form.SetAddFinishedCallback(() =>
            {
                this.Search();
            });
            form.Show();
        }

        private void buttonAlter_Click(object sender, EventArgs e)
        {
            var worksheet = this.reoGridControlMain.Worksheets[0];
            try
            {
                if (worksheet.SelectionRange.Rows != 1)
                {
                    throw new Exception();
                }
                int shipmentTicketID = int.Parse(worksheet[worksheet.SelectionRange.Row, 0].ToString());
                var formShipmentTicketModify = new FormShipmentTicketModify(this.projectID,this.warehouseID,this.userID,shipmentTicketID);
                formShipmentTicketModify.SetModifyFinishedCallback(() =>
                {
                    this.Search();
                });
                formShipmentTicketModify.Show();
            }
            catch
            {
                MessageBox.Show("请选择一项进行修改", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            int[] deleteIDs = this.GetSelectedIDs();
            if (deleteIDs.Length == 0)
            {
                MessageBox.Show("请选择您要删除的记录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("您真的要删除这些记录吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }
            this.labelStatus.Text = "正在删除...";


            new Thread(new ThreadStart(() =>
            {
                try
                {
                    foreach (int id in deleteIDs)
                    {
                        this.wmsEntities.Database.ExecuteSqlCommand("DELETE FROM ShipmentTicket WHERE ID = @shipmentTicketID", new SqlParameter("shipmentTicketID", id));
                    }
                    this.wmsEntities.SaveChanges();
                }
                catch
                {
                    MessageBox.Show("删除失败，请检查网络连接","提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                this.Invoke(new Action(this.Search));
            })).Start();
        }

        private void buttonGenerateJobTicket_Click(object sender, EventArgs e)
        {
            int[] ids = this.GetSelectedIDs();
            if(ids.Length != 1)
            {
                MessageBox.Show("请选择一项进行操作","提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int shipmentTicketID = ids[0];

            FormJobTicketNew form = new FormJobTicketNew(shipmentTicketID,this.userID,this.projectID,this.warehouseID);
            form.SetToJobTicketCallback(this.toJobTicketCallback);
            form.Show();
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

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            this.Search();
        }

        private void comboBoxSearchCondition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBoxSearchCondition.SelectedIndex == 0)
            {
                this.textBoxSearchValue.Text = "";
                this.textBoxSearchValue.Enabled = false;
            }
            else
            {
                this.textBoxSearchValue.Enabled = true;
            }
        }

        private void textBoxSearchValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                this.Search();
            }
        }

        private void toolStripButtonToJobTicket_Click(object sender, EventArgs e)
        {
            int[] ids = Utilities.GetSelectedIDs(this.reoGridControlMain);
            if(ids.Length != 1)
            {
                MessageBox.Show("请选择一项进行操作","提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int id = ids[0];
            new Thread(()=>
            {
                try
                {
                    WMSEntities wmsEntities = new WMSEntities();
                    ShipmentTicket shipmentTicket = (from s in wmsEntities.ShipmentTicket
                                                     where s.ID == id
                                                     select s).FirstOrDefault();
                    if (shipmentTicket == null)
                    {
                        MessageBox.Show("发货单不存在，请重新查询", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    this.toJobTicketCallback(shipmentTicket.No);
                }
                catch (Exception)
                {
                    MessageBox.Show("查询失败，请检查网络连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }).Start();

        }
    }
}
