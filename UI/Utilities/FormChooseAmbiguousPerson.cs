using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WMS.DataAccess;

namespace WMS.UI
{
    public partial class FormChooseAmbiguousPerson : Form
    {
        private class ItemAndString
        {
            public Person Item;
            public string String;

            public override string ToString()
            {
                return this.String;
            }
        }

        private bool clickedButtonOK = false; //点击确定按钮才保存，点击叉子虽然也是Close事件，但是取消选择
        private Person selectedItem = null;

        public FormChooseAmbiguousPerson()
        {
            InitializeComponent();
        }

        private void FormChooseAmbiguousPerson_Load(object sender, EventArgs e)
        {

        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (this.selectedItem == null)
            {
                MessageBox.Show("请选择一项！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            this.clickedButtonOK = true;
            this.Close();
        }

        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listBox.SelectedItem == null) return;
            this.selectedItem = (this.listBox.SelectedItem as ItemAndString).Item;
        }

        private void FormChooseAmbiguousPerson_FormClosing(object sender, FormClosingEventArgs e)
        {
            //如果是点击右上角叉子关闭窗口，无视选择项，直接取消导入
            if (this.clickedButtonOK == false)
            {
                this.selectedItem = null;
            }
        }

        public static Person ChoosePerson(Person[] persons, string srcNameAmbiguous)
        {
            //传入空数组直接返回空
            if ((persons == null || persons.Length == 0))
            {
                return null;
            }
            //否则新建一个选择窗口，开始选择
            FormChooseAmbiguousPerson form = new FormChooseAmbiguousPerson();
            form.listBox.Items.AddRange((from p in persons
                                         select new ItemAndString()
                                         {
                                             Item = p,
                                             String = p.Name
                                         }).ToArray());

            form.label1.Text = string.Format("您输入的人员\"{0}\"不明确，请选择下列零件中的一个", srcNameAmbiguous);
            form.ShowDialog();
            return form.selectedItem;
        }
    }
}
