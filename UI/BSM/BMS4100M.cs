using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WMS.DataAccess;

namespace WMS.UI.BSM
{
    public partial class BMS4100M : Form
    {
        

        public BMS4100M()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if(textBox1.Text== string.Empty)
            {
                MessageBox.Show("用户名称不能为空！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (textBox2.Text == string.Empty)
            {
                MessageBox.Show("密码不能为空！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //添加
            User objuser = new User();
            {

                objuser.UserName = textBox1.Text;
                objuser.PassWord = textBox2.Text;
                objuser.Authority = 0;
            }
            WMSEntities wms = new WMSEntities();
            wms.User.Add(objuser);
            wms.SaveChanges();
            //查找
            //User obuser = (from s in wms.User
            //               where s.UserName == "WWW"
            //               select s).First<User>();
            //MessageBox.Show(obuser.UserName + "+" + obuser.PassWord);
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //e.Cancel = false;
        }
    }
}
