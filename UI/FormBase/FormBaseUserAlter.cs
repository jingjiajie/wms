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
    public partial class FormBaseUserAlter : Form
    {
        public FormBaseUserAlter()
        {
            InitializeComponent();
        }
        public FormBaseUserAlter(string a,string b)//函数重载 传参
        {
            InitializeComponent();
            textBoxUsername.Text = a;
            textBoxPassword.Text = b;
        }

        private void buttonEnter_Click(object sender, EventArgs e)
        {
            
            if (textBoxPassword.Text == string.Empty)
            {
                MessageBox.Show("密码不能为空！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //修改
            WMSEntities wms = new WMSEntities();
            User objuser = (from s in wms.User
                              where s.UserName ==textBoxUsername.Text
                              select s).First<User>();

            objuser.UserName = textBoxUsername.Text;
            objuser.PassWord = textBoxPassword.Text;
            if (radioButtonBase.Checked == true)
            {
                objuser.Authority = 15;
            }
            if (radioButtonDelivery.Checked == true)
            {
                objuser.Authority = 4;
            }
            if (radioButtonReceive.Checked == true)
            {
                objuser.Authority = 2;
            }
            if (radioButtonStork.Checked == true)
            {
                objuser.Authority = 8;
            }
            wms.SaveChanges();
            MessageBox.Show("修改成功");
            this.Close();
        }

        private void FormBaseUserAlter_Load(object sender, EventArgs e)
        {
            textBoxUsername.ReadOnly = true;//用户名只读
        }

        private void buttonClosing_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
