using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WMS.DataAccess;
using System.Reflection;

namespace WMS.UI
{
    public partial class FormJobTicketModify : Form
    {
        private int jobTicketID = -1;
        private int userID = -1;

        private WMSEntities wmsEntities = new WMSEntities();
        private Action modifyFinishedCallback = null;

        public FormJobTicketModify(int userID,int jobTicketID)
        {
            InitializeComponent();
            this.jobTicketID = jobTicketID;
            this.userID = userID;
        }

        private void FormJobTicketModify_Load(object sender, EventArgs e)
        {
            if (this.jobTicketID == -1)
            {
                throw new Exception("未设置源作业单信息");
            }
            Utilities.CreateEditPanel(this.tableLayoutPanelTextBoxes, JobTicketViewMetaData.KeyNames);

            JobTicketView jobTicketView = null;
            try
            {
                jobTicketView = (from s in this.wmsEntities.JobTicketView
                                               where s.ID == this.jobTicketID
                                               select s).FirstOrDefault();
            }
            catch
            {
                MessageBox.Show("加载数据失败，请检查网络连接","提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            if(jobTicketView == null)
            {
                MessageBox.Show("作业单不存在，请刷新查询", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            Utilities.CopyPropertiesToTextBoxes(jobTicketView, this);
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            JobTicket JobTicket = null;
            try
            {
                JobTicket = (from s in this.wmsEntities.JobTicket
                             where s.ID == this.jobTicketID
                             select s).FirstOrDefault();
            }
            catch
            {
                MessageBox.Show("修改失败，请检查网络连接","提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (JobTicket == null)
            {
                MessageBox.Show("作业单不存在，请刷新查询", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            JobTicket.LastUpdateUserID = this.userID;
            JobTicket.LastUpdateTime = DateTime.Now;


            //开始数据库操作
            if (Utilities.CopyTextBoxTextsToProperties(this, JobTicket, JobTicketViewMetaData.KeyNames, out string errorMessage) == false)
            {
                MessageBox.Show(errorMessage, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                wmsEntities.SaveChanges();
            }
            catch
            {
                MessageBox.Show("修改失败，请检查网络连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //调用回调函数
            if (this.modifyFinishedCallback != null)
            {
                this.modifyFinishedCallback();
            }
            this.Close();

        }

        public void SetModifyFinishedCallback(Action callback)
        {
            this.modifyFinishedCallback = callback;
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
