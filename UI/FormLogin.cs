using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WMS.DataAccess;
using System.Threading;

namespace WMS.UI
{
    public partial class FormLogin : Form
    {
        Point mouseOff;//鼠标移动位置变量
        bool leftFlag;//标签是否为左键

        public FormLogin()
        {
            InitializeComponent();
        }

        private void buttonEnter_Click(object sender, EventArgs e)
        {
            this.labelStatus.Text = "正在登陆，请耐心等待...";
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
            new Thread(new ThreadStart(()=>
            {
                WMSEntities wms = new WMSEntities();
                User user = (from s in wms.User
                                 where s.Username == textBoxUsername.Text
                                 select s).FirstOrDefault<User>();
                if (user == null)
                {
                    MessageBox.Show("用户名错误，请重新输入");
                    return;
                }
                else if (user.Password != textBoxPassword.Text)
                {
                    MessageBox.Show("密码错误，请重新输入");
                }
                else
                {
                    this.Invoke(new Action(()=>
                    {
                        this.labelStatus.Text = "";
                        FormMain formMain = new FormMain(user.ID);
                        formMain.SetFormClosedCallback(this.Close);
                        formMain.Show();
                        this.Hide();
                    }));
                }
            })).Start();
        }

        private void buttonClosing_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBoxUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) 
            {
                this.textBoxPassword.Focus();
                this.textBoxPassword.SelectAll();
                return;
            }
        }

        private void textBoxPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.buttonEnter.Focus();
                this.buttonEnter.PerformClick();
                return;
            }
        }

        private void FormLogin_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point workAreaPosition = this.PointToClient(Control.MousePosition);
                mouseOff = new Point(-workAreaPosition.X, -workAreaPosition.Y); //得到鼠标偏移量
                leftFlag = true;   //点击左键按下时标注为true;
            }
        }

        private void FormLogin_MouseMove(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                Point mouseSet = Control.MousePosition;
                mouseSet.Offset(mouseOff.X, mouseOff.Y);  //设置移动后的位置
                Location = mouseSet;
            }
        }

        private void FormLogin_MouseUp(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                leftFlag = false;//释放鼠标后标注为false;
            }
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            this.labelStatus.Text = "";
        }
    }
}
