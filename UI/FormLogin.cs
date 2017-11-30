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
    public partial class FormLogin : Form
    {
        public FormLogin()
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
            WMSEntities wms = new WMSEntities();
            User allUsers = (from s in wms.User
                             where s.Username == textBoxUsername.Text
                             select s).FirstOrDefault<User>();
            if (allUsers == null)
            {
                MessageBox.Show("查无此人");
            }
            else
            {
                if (allUsers.Password == textBoxPassword.Text)
                {
                    string a = allUsers.Username;
                    int b=allUsers.Authority;
                    //MessageBox.Show(allUsers.Username + "+" + allUsers.Password);
                    FormMain fm = new FormMain(a,b);
                    fm.ShowDialog();
                    this.Close();  
                }
                else
                {
                    MessageBox.Show("密码错误");
                }
            }
            
        }

        private void buttonClosing_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
