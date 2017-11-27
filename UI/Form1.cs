using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WMS.UI
{
    public partial class Form1 : Form
    {
        //BaseClass.BaseInfo baseinfo = new WMS.BaseClass.BaseInfo();
        //BaseClass.cPopedom popedom = new WMS.BaseClass.cPopedom();
        public Form1()
        {
            InitializeComponent();
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text == string.Empty)
            {
                MessageBox.Show("用户名称不能为空！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DataSet ds = null;
            popedom.SysUser = txtUserName.Text;
            popedom.Password = txtUserPwd.Text;
            ds = baseinfo.Login(popedom);
            if (ds.Tables[0].Rows.Count > 0)
            {
                frmMain frm_main = new frmMain();
                frm_main.Show();
                if (Convert.ToBoolean(ds.Tables[0].Rows[0]["stock"])) frm_main.toolStripMenuItem1.Enabled = true;
                if (Convert.ToBoolean(ds.Tables[0].Rows[0]["vendition"])) frm_main.toolStripMenuItem7.Enabled = true;
                if (Convert.ToBoolean(ds.Tables[0].Rows[0]["storage"])) frm_main.toolStripMenuItem15.Enabled = true;
                if (Convert.ToBoolean(ds.Tables[0].Rows[0]["system"])) frm_main.toolStripMenuItem24.Enabled = true;
                if (Convert.ToBoolean(ds.Tables[0].Rows[0]["base"])) frm_main.toolStripMenuItem20.Enabled = true;
                this.Visible = false;
            }
            else
            {
                MessageBox.Show("用户名称或密码不正确！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtUserName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13) //判断是否按下Enter键
                txtUserPwd.Focus();//将鼠标焦点移动到“密码”文本框
        }

        private void txtUserPwd_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)//判断是否按下Enter键
                btnLogin.Focus();//将鼠标焦点移动到“登录”按钮
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
