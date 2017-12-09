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
    public partial class FormBaseProjectModify : Form
    {
        private int projectID = -1;
        private WMSEntities wmsEntities = new WMSEntities();
        private Action modifyFinishedCallback = null;
        private Action addFinishedCallback = null;
        private FormMode mode = FormMode.ALTER;


        public FormBaseProjectModify(int projectID=-1)
        {
            InitializeComponent();
            this.projectID = projectID;
        }

        private void FormBaseProjectModify_Load(object sender, EventArgs e)
        {
            if (this.mode == FormMode.ALTER && this.projectID == -1)
            {
                throw new Exception("未设置源库存信息");
            }
            this.tableLayoutPanel1.Controls.Clear();
            for (int i = 1; i < BaseProjectMetaData.KeyNames.Length; i++)
            {
                KeyName curKeyName = BaseProjectMetaData.KeyNames[i];
                Label label = new Label();
                label.Text = curKeyName.Name;
                this.tableLayoutPanel1.Controls.Add(label);

                TextBox textBox = new TextBox();
                textBox.Name = "textBox" + curKeyName.Key;
                this.tableLayoutPanel1.Controls.Add(textBox);
            }
            if (this.mode == FormMode.ALTER)
            {
                Project Project = (from s in this.wmsEntities.Project
                                     where s.ID == this.projectID
                                    select s).Single();
                Utilities.CopyPropertiesToTextBoxes(Project, this);
            }
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
                this.Text = "修改项目信息";
                this.buttonModify.Text = "修改项目信息";
            }
            else if (mode == FormMode.ADD)
            {
                this.Text = "添加项目信息";
                this.buttonModify.Text = "添加项目信息";
            }
        }

        private void buttonModify_Click(object sender, EventArgs e)
        {
            Project project = null;

            //若修改，则查询原对象。若添加，则新建对象。
            if (this.mode == FormMode.ALTER)
            {
                project = (from s in this.wmsEntities.Project
                            where s.ID == this.projectID
                            select s).Single();
            }
            else if (mode == FormMode.ADD)
            {
                project = new Project();
                this.wmsEntities.Project.Add(project);
            }
            //开始数据库操作
            if (Utilities.CopyTextBoxTextsToProperties(this, project, BaseProjectMetaData.KeyNames, out string errorMessage) == false)
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
    }
}
