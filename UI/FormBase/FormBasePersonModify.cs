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
    public partial class FormBasePersonModify : Form
    {
        private int personID = -1;
        private WMSEntities wmsEntities = new WMSEntities();
        private Action<int> modifyFinishedCallback = null;
        private Action<int> addFinishedCallback = null;
        private FormMode mode = FormMode.ALTER;


        public FormBasePersonModify(int personID=-1)
        {
            InitializeComponent();
            this.personID = personID;
        }

        private void FormBasePersonModify_Load(object sender, EventArgs e)
        {
            if (this.mode == FormMode.ALTER && this.personID == -1)
            {
                throw new Exception("未设置源库存信息");
            }
            this.tableLayoutPanel1.Controls.Clear();
            for (int i = 1; i < BasePersonMetaData.KeyNames.Length; i++)
            {
                KeyName curKeyName = BasePersonMetaData.KeyNames[i];
                Label label = new Label();
                label.Text = curKeyName.Name;
                this.tableLayoutPanel1.Controls.Add(label);

                TextBox textBox = new TextBox();
                textBox.Name = "textBox" + curKeyName.Key;
                this.tableLayoutPanel1.Controls.Add(textBox);
            }
            if (this.mode == FormMode.ALTER)
            {
                try
                {
                    Person Person = (from s in this.wmsEntities.Person
                                       where s.ID == this.personID
                                       select s).FirstOrDefault();
                    Utilities.CopyPropertiesToTextBoxes(Person, this);
                }
                catch (Exception)
                {
                    MessageBox.Show("修改失败，请检查网络连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

            }
        }





        public void SetModifyFinishedCallback(Action<int> callback)
        {
            this.modifyFinishedCallback = callback;
        }
        public void SetAddFinishedCallback(Action<int> callback)
        {
            this.addFinishedCallback = callback;
        }

        public void SetMode(FormMode mode)
        {
            this.mode = mode;
            if (mode == FormMode.ALTER)
            {
                this.Text = "修改人员信息";
                this.buttonModify.Text = "修改人员信息";
                this.groupBoxMain.Text = "修改人员信息";
            }
            else if (mode == FormMode.ADD)
            {
                this.Text = "添加人员信息";
                this.buttonModify.Text = "添加人员信息";
                this.groupBoxMain.Text = "添加人员信息";
            }
        }

        private void buttonModify_Click(object sender, EventArgs e)
        {
            var textBoxName = this.Controls.Find("textBoxName", true)[0];
            if (textBoxName.Text == string.Empty)
            {
                MessageBox.Show("人员名称不能为空！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Person person = null;

            //若修改，则查询原对象。若添加，则新建对象。
            if (this.mode == FormMode.ALTER)
            {
                try
                {
                    person = (from s in this.wmsEntities.Person
                               where s.ID == this.personID
                               select s).FirstOrDefault();
                }
                catch
                {
                    MessageBox.Show("修改失败，请检查网络连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (person == null)
                {
                    MessageBox.Show("人员不存在，请重新查询", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                try
                {
                    var sameNamePerson = (from u in wmsEntities.Person
                                             where u.Name == textBoxName.Text
                                                && u.ID != person.ID
                                             select u).ToArray();
                    if (sameNamePerson.Length > 0)
                    {
                        MessageBox.Show("修改人员名失败，已存在同名人员：" + textBoxName.Text, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                catch
                {

                    MessageBox.Show("操作失败，请检查网络连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

            }
            else if (mode == FormMode.ADD)
            {
                try
                {
                    var sameNamePerson = (from u in wmsEntities.Person
                                          where u.Name == textBoxName.Text
                                          select u).ToArray();
                    if (sameNamePerson.Length > 0)
                    {
                        MessageBox.Show("修改人员名失败，已存在同名人员：" + textBoxName.Text, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                catch
                {

                    MessageBox.Show("操作失败，请检查网络连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                person = new Person();
                this.wmsEntities.Person.Add(person);
            }
            //开始数据库操作
            if (Utilities.CopyTextBoxTextsToProperties(this, person, BasePersonMetaData.KeyNames, out string errorMessage) == false)
            {
                MessageBox.Show(errorMessage, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                wmsEntities.SaveChanges();
            }
            catch (Exception)
            {
                if (this.mode == FormMode.ALTER)
                {
                    MessageBox.Show("修改失败，请检查网络连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (mode == FormMode.ADD)
                {
                    MessageBox.Show("添加失败，请检查网络连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

            }
            //调用回调函数
            if (this.mode == FormMode.ALTER && this.modifyFinishedCallback != null)
            {
                this.modifyFinishedCallback(person.ID);
                MessageBox.Show("修改成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else if (this.mode == FormMode.ADD && this.addFinishedCallback != null)
            {
                this.addFinishedCallback(person.ID);
                MessageBox.Show("添加成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void buttonModify_MouseEnter(object sender, EventArgs e)
        {
            buttonModify.BackgroundImage = WMS.UI.Properties.Resources.bottonB2_s;
        }

        private void buttonModify_MouseLeave(object sender, EventArgs e)
        {
            buttonModify.BackgroundImage = WMS.UI.Properties.Resources.bottonB2_q;
        }

        private void buttonModify_MouseDown(object sender, MouseEventArgs e)
        {
            buttonModify.BackgroundImage = WMS.UI.Properties.Resources.bottonB3_q;
        }
    }
}
