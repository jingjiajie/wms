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
    public partial class FormPutOutStorageTicketModify : Form
    {
        private int putOutStorageTicketID = -1;
        private int userID = -1;

        private WMSEntities wmsEntities = new WMSEntities();
        private Action modifyFinishedCallback = null;
        private Action addFinishedCallback = null;

        public FormPutOutStorageTicketModify(int userID,int putOutStorageTicketID)
        {
            InitializeComponent();
            this.putOutStorageTicketID = putOutStorageTicketID;
            this.userID = userID;
        }

        private void FormPutOutStorageTicketModify_Load(object sender, EventArgs e)
        {
            if (this.putOutStorageTicketID == -1)
            {
                throw new Exception("未设置源库存信息");
            }

            this.tableLayoutPanelTextBoxes.Controls.Clear();
            for (int i = 0; i < PutOutStorageTicketViewMetaData.KeyNames.Length; i++)
            {
                KeyName curKeyName = PutOutStorageTicketViewMetaData.KeyNames[i];
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

            PutOutStorageTicketView putOutStorageTicketView = (from s in this.wmsEntities.PutOutStorageTicketView
                                                               where s.ID == this.putOutStorageTicketID
                                                               select s).Single();
            Utilities.CopyPropertiesToTextBoxes(putOutStorageTicketView, this);
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            PutOutStorageTicket putOutStorageTicket = null;
            putOutStorageTicket = (from s in this.wmsEntities.PutOutStorageTicket
                                   where s.ID == this.putOutStorageTicketID
                                   select s).FirstOrDefault();
            if (putOutStorageTicket == null)
            {
                MessageBox.Show("未找到出货单信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            putOutStorageTicket.LastUpdateUserID = this.userID;
            putOutStorageTicket.LastUpdateTime = DateTime.Now;


            //开始数据库操作
            if (Utilities.CopyTextBoxTextsToProperties(this, putOutStorageTicket, PutOutStorageTicketViewMetaData.KeyNames, out string errorMessage) == false)
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
