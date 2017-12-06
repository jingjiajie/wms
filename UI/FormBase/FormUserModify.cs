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

namespace WMS.UI.FormBase
{
    public partial class FormUserModify : Form
    {
        private int userID = -1;
        private WMSEntities wmsEntities = new WMSEntities();
        private Action modifyFinishedCallback = null;
        private Action addFinishedCallback = null;
        private FormMode mode = FormMode.ALTER;

        public FormUserModify(int userID = -1)
        {
            InitializeComponent();
            this.userID = userID;
        }

        private void FormUserModify_Load(object sender, EventArgs e)
        {
            if (this.mode == FormMode.ALTER && this.userID == -1)
            {
                throw new Exception("未设置源库存信息");
            }

            this.tableLayoutPanelTextBoxes.Controls.Clear();
            for (int i = 0; i < UserMetaData.KeyNames.Length; i++)
            {
                KeyName curKeyName = UserMetaData.KeyNames[i];
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

            if (this.mode == FormMode.ALTER)
            {
                UserView userView = (from s in this.wmsEntities.UserView
                                               where s.ID == this.userID
                                               select s).Single();
                Utilities.CopyPropertiesToTextBoxes(userView, this);
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            User user = null;

            //若修改，则查询原StockInfo对象。若添加，则新建一个StockInfo对象。
            if (this.mode == FormMode.ALTER)
            {
                user = (from s in this.wmsEntities.User
                             where s.ID == this.userID
                             select s).Single();
            }
            else if (mode == FormMode.ADD)
            {
                user = new User();
                this.wmsEntities.User.Add(user);
            }

            //开始数据库操作
            if (Utilities.CopyTextBoxTextsToProperties(this, user, UserMetaData.KeyNames, out string errorMessage) == false)
            {
                MessageBox.Show(errorMessage, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            wmsEntities.SaveChanges();
            //调用回调函数
            if (this.mode == FormMode.ALTER && this.modifyFinishedCallback != null)
            {
                this.modifyFinishedCallback();
            }
            else if (this.mode == FormMode.ADD && this.addFinishedCallback != null)
            {
                this.addFinishedCallback();
            }
            this.Close();
        }

        public void SetModifyFinishedCallback(Action callback)
        {
            this.modifyFinishedCallback = callback;
        }

        public void SetAddFinishedCallback(Action callback)
        {
            this.addFinishedCallback = callback;
        }

        public void SetMode(FormMode mode)
        {
            this.mode = mode;
            if (mode == FormMode.ALTER)
            {
                this.Text = "修改用户信息";
                this.buttonOK.Text = "修改用户信息";
            }
            else if (mode == FormMode.ADD)
            {
                this.Text = "添加用户信息";
                this.buttonOK.Text = "添加用户信息";
            }
        }
    }
}
