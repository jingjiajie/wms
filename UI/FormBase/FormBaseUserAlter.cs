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

            //修改
            WMSEntities wms = new WMSEntities();
            //User nameUsers = (from s in wms.User
            //                  where s.Username == 
            //                  select s).First<User>();

            // nameUsers.Username = textBoxUsername.Text;
            //nameUsers.Password = textBoxPassword.Text;

            wms.SaveChanges();
        }

        private void FormBaseUserAlter_Load(object sender, EventArgs e)
        {
            //textBoxUsername.Text =
            //textBoxPassword.Text =
        }
    }
}
