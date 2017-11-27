using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WMS.DataAccess;

namespace WMS.UI.frm_base
{
    public partial class base_useradd : Form
    {
        public base_useradd()
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
                objuser.Authority = 0;
            }
            WMSEntities wms = new WMSEntities();
            wms.User.Add(objuser);
            wms.SaveChanges();
            
            
        }
    }
}
