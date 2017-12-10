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
        private Action addFinishedCallback = null;

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

            this.tableLayoutPanelTextBoxes.Controls.Clear();
            for (int i = 0; i < JobTicketViewMetaData.KeyNames.Length; i++)
            {
                KeyName curKeyName = JobTicketViewMetaData.KeyNames[i];
                if (curKeyName.Visible == false && curKeyName.Editable == false)
                {
                    continue;
                }
                Label label = new Label();
                label.Text = curKeyName.Name;
                this.tableLayoutPanelTextBoxes.Controls.Add(label);

                TextBox textBox = new TextBox();
                textBox.Name = "textBox" + curKeyName.Key;
                if (curKeyName.Editable == false)
                {
                    textBox.Enabled = false;
                }
                this.tableLayoutPanelTextBoxes.Controls.Add(textBox);
            }

            JobTicketView JobTicketView = (from s in this.wmsEntities.JobTicketView
                                                               where s.ID == this.jobTicketID
                                                               select s).Single();
            Utilities.CopyPropertiesToTextBoxes(JobTicketView, this);
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            JobTicket JobTicket = null;
            JobTicket = (from s in this.wmsEntities.JobTicket
                                   where s.ID == this.jobTicketID
                                   select s).FirstOrDefault();
            if (JobTicket == null)
            {
                MessageBox.Show("未找到出货单信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            wmsEntities.SaveChanges();
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
    }
}
