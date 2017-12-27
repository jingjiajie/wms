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

namespace WMS.UI
{
    public partial class FormPutOutStorageTicketNew : Form
    {
        private PagerWidget<JobTicketItemView> pagerWidget = null;
        private int jobTicketID = -1;
        private int userID = -1;
        private int projectID = -1;
        private int warehouseID = -1;

        private Action<string> toPutOutStorageTicketCallback = null;

        public void SetToPutOutStorageTicketCallback(Action<string> callback)
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

        private void FormPutOutStorageTicketNew_Load(object sender, EventArgs e)
        {
            this.InitComponents();
            Utilities.CreateEditPanel(this.tableLayoutEditPanel, PutOutStorageTicketViewMetaData.KeyNames);
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
            this.pagerWidget = new PagerWidget<JobTicketItemView>(this.reoGridControlMain, JobTicketItemViewMetaData.KeyNames);
            this.panelPagerWidget.Controls.Add(this.pagerWidget);
            this.pagerWidget.Show();
        }

        private void Search()
        {
            this.pagerWidget.AddStaticCondition("JobTicketID", this.jobTicketID.ToString());
            this.pagerWidget.Search();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            PutOutStorageTicket newPutOutStorageTicket = new PutOutStorageTicket();
            if (Utilities.CopyTextBoxTextsToProperties(this, newPutOutStorageTicket, PutOutStorageTicketViewMetaData.KeyNames, out string errorMesage) == false)
            {
                MessageBox.Show(errorMesage, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                newPutOutStorageTicket.JobTicketID = jobTicket.ID;
                newPutOutStorageTicket.ProjectID = this.projectID;
                newPutOutStorageTicket.WarehouseID = this.warehouseID;
                newPutOutStorageTicket.CreateUserID = this.userID;
                newPutOutStorageTicket.CreateTime = DateTime.Now;

                foreach (var jobTicketItem in jobTicket.JobTicketItem)
                {
                    var putOutStorageTicketItem = new PutOutStorageTicketItem();
                    putOutStorageTicketItem.StockInfoID = jobTicketItem.StockInfoID;

                    newPutOutStorageTicket.PutOutStorageTicketItem.Add(putOutStorageTicketItem);
                }
                wmsEntities.SaveChanges();
                newPutOutStorageTicket.No = Utilities.GenerateNo("C", newPutOutStorageTicket.ID);
                wmsEntities.SaveChanges();
                if (MessageBox.Show("生成出库单成功，是否查看出库单？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    if (this.toPutOutStorageTicketCallback == null)
                    {
                        throw new Exception("toPutOutStorageTicketCallback不可以为空！");
                    }
                    this.toPutOutStorageTicketCallback(newPutOutStorageTicket.No);
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
    }
}