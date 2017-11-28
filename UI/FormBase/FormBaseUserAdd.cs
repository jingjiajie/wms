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
    public partial class FormBaseUserAdd : Form
    {
        public FormBaseUserAdd()
        {
            InitializeComponent();
        }

        private void buttonEnter_Click(object sender, EventArgs e)
        {
            if (textBoxUsername.Text == string.Empty)
            {
                MessageBox.Show("用户名称不能为空！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (textBoxPassword.Text == string.Empty)
            {
                MessageBox.Show("密码不能为空！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //添加
            User objuser = new User();
            {
                objuser.UserName = textBoxUsername.Text;
                objuser.PassWord = textBoxUsername.Text;
                if(radioButtonBase.Checked==true)
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
            }
            WMSEntities wms = new WMSEntities();
            wms.User.Add(objuser);
            wms.SaveChanges();    
        }

        private void base_useradd_Load(object sender, EventArgs e)
        {

        }

        private void FormBaseUserAdd_Load(object sender, EventArgs e)
        {

        }
    }
}
