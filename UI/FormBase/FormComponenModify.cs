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
    public partial class FormComponenModify : Form
    {
        private int componenID = -1;
        private WMSEntities wmsEntities = new WMSEntities();
        private Action modifyFinishedCallback = null;
        private Action addFinishedCallback = null;
        private FormMode mode = FormMode.ALTER;

        public FormComponenModify(int componenID = -1)
        {
            InitializeComponent();
            this.componenID = componenID;
        }
        
        private void FormComponenModify_Load(object sender, EventArgs e)
        {
            if (this.mode == FormMode.ALTER && this.componenID == -1)
            {
                throw new Exception("未设置源库存信息");
            }

            this.tableLayoutPanelTextBoxes.Controls.Clear();
            for (int i = 0; i < ComponenMetaData.KeyNames.Length; i++)
            {
                KeyName curKeyName = ComponenMetaData.KeyNames[i];
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

            if(this.mode == FormMode.ALTER)
            {
                DataAccess.Component componen = (from s in this.wmsEntities.Component
                                       where s.ID == this.componenID
                                       select s).Single();
                Utilities.CopyPropertiesToTextBoxes(componen, this);
            }
            this.Controls.Find("textBoxtextBoxProjectID", true)[0].LostFocus += textBoxtextBoxProjectID_LostFocus;
        }
        private void textBoxtextBoxProjectID_LostFocus(object sender, EventArgs e)
        {
            TextBox textBoxtextBoxProjectID = (TextBox)this.Controls.Find("textBoxtextBoxProjectID", true)[0];
            if (textBoxtextBoxProjectID.Text.Length == 0) return;
            CheckForeignKeyProject();
        }

        private bool CheckForeignKeyProject()
        {
            TextBox textBoxtextBoxProjectID = (TextBox)this.Controls.Find("textBoxtextBoxProjectID", true)[0];
            if (textBoxtextBoxProjectID.Text.Length == 0)
            {
                MessageBox.Show("上架单条目ID 不可以为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            int putawayTicketItemID;
            if (int.TryParse(textBoxtextBoxProjectID.Text, out putawayTicketItemID) == false)
            {
                MessageBox.Show("上架单条目ID 只接受数值类型", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            PutawayTicketItemView[] result = (from p in wmsEntities.PutawayTicketItemView
                                              where p.ID == putawayTicketItemID
                                              select p).ToArray();
            if (result.Length == 0)
            {
                MessageBox.Show("未找到上架单条目ID为" + putawayTicketItemID + "的上架单条目，请重新输入", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            PutawayTicketItemView putawayTicketView = result[0];
            Utilities.CopyPropertiesToTextBoxes(putawayTicketView, this, "textBox");
            Utilities.CopyPropertiesToTextBoxes(putawayTicketView, this, "textBoxPutawayTicketItem");
            return true;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (CheckForeignKeyProject() == false) return;


            DataAccess.Component componen = null;           
            if (this.mode == FormMode.ALTER)
            {
                componen = (from s in this.wmsEntities.Component
                             where s.ID == this.componenID
                             select s).Single();
            }
            else if (mode == FormMode.ADD)
            {
                componen = new DataAccess.Component();
                this.wmsEntities.Component.Add(componen);
            }

            //开始数据库操作
            if (Utilities.CopyTextBoxTextsToProperties(this, componen, ComponenMetaData.KeyNames, out string errorMessage) == false)
            {
                MessageBox.Show(errorMessage, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            wmsEntities.SaveChanges();
            //调用回调函数
            if (this.mode == FormMode.ALTER && this.modifyFinishedCallback != null)
            {
                this.modifyFinishedCallback();
            }else if(this.mode == FormMode.ADD && this.addFinishedCallback != null)
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
            if(mode == FormMode.ALTER)
            {
                this.Text = "修改零件信息";
                this.buttonOK.Text = "修改零件信息";
            }else if (mode == FormMode.ADD)
            {
                this.Text = "添加零件信息";
                this.buttonOK.Text = "添加零件信息";
            }
        }

    }
}
