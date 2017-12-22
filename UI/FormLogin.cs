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
using System.Data.SqlClient;
using System.Drawing.Drawing2D;//粗糙圆角www

namespace WMS.UI
{
    public partial class FormLogin : Form
    {
        Point mouseOff;//鼠标移动位置变量
        bool leftFlag;//标签是否为左键
        bool networkError = false;
        bool refreshedPossibleUser = false;
        User possibleUser = null;
        Mutex possibleUserMutex = new Mutex();

        int clickCount = 0; //防止用户心情不好时疯狂点击登录按钮

        WMSEntities wmsEntities = new WMSEntities();

        public int ClickCount
        {
            get => clickCount;
            set
            {
                clickCount = value;
                if (clickCount > 1)
                {
                    if (this.IsDisposed)
                    {
                        return;
                    }
                    this.Invoke(new Action(()=>
                    {
                        this.labelClickCount.Text = clickCount.ToString();
                        int size = ((int)Math.Pow(clickCount, 1.5) + 100) / 10;
                        this.labelClickCount.Font = new Font("黑体", size > 40 ? 40 : size);
                        this.labelClickCount.Visible = true;
                    }));

                }
                else
                {
                    if (this.IsDisposed)
                    {
                        return;
                    }
                    this.Invoke(new Action(() =>
                    {
                        this.labelClickCount.Visible = false;
                    }));
                }

            }
        }

        public FormLogin()
        {
            InitializeComponent();
        }

        private void buttonEnter_Click(object sender, EventArgs e)
        {
            ClickCount++;
            if (ClickCount > 1)
            {
                return;
            }
            if (textBoxUsername.Text == string.Empty)
            {
                MessageBox.Show("用户名称不能为空！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ClickCount = 0;
                return;
            }
            if (textBoxPassword.Text == string.Empty)
            {
                MessageBox.Show("密码不能为空！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ClickCount = 0;
                return;
            }
            this.labelStatus.Text = "正在登陆，请耐心等待...";
            new Thread(new ThreadStart(() =>
            {
                this.possibleUserMutex.WaitOne();
                if (this.refreshedPossibleUser == false) //如果没有调用过RefreshPossibleUser，再调用一次
                {
                    this.possibleUserMutex.ReleaseMutex();
                    this.RefreshPossibleUserSync();
                    this.possibleUserMutex.WaitOne();
                }
                if (this.networkError == true) //如果networkError，直接返回
                {
                    if (this.IsDisposed) return;
                    this.Invoke(new Action(() =>
                    {
                        this.labelStatus.Text = "";
                    }));
                    this.possibleUserMutex.ReleaseMutex();
                    this.refreshedPossibleUser = false;
                    ClickCount = 0;
                    return;
                }
                User user = this.possibleUser;
                if (user == null)
                {
                    MessageBox.Show("用户名错误，请重新输入", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.possibleUserMutex.ReleaseMutex();
                    ClickCount = 0;
                    return;
                }
                else if (user.Password != textBoxPassword.Text)
                {
                    MessageBox.Show("密码错误，请重新输入", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.possibleUserMutex.ReleaseMutex();
                    ClickCount = 0;
                    return;
                }
                else
                {
                    this.possibleUserMutex.ReleaseMutex();
                    this.Invoke(new Action(() =>
                    {
                        this.labelStatus.Text = "";
                        FormMain formMain = new FormMain(user.ID);
                        formMain.SetFormClosedCallback(this.Dispose);
                        formMain.Show();
                        this.Hide();
                        ClickCount = 0;
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
            this.refreshedPossibleUser = false;
        }

        private void textBoxUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.textBoxPassword.Focus();
                this.textBoxPassword.SelectAll();
                return;
            }else if (e.KeyChar == 27) //ESC
            {
                this.Close();
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
            else if (e.KeyChar == 27) //ESC
            {
                this.Close();
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
            //让窗口透明www
            this.TransparencyKey = Color.Red;
            this.BackColor = Color.Red;
            //panel1.BackColor = Color.FromRvb(120, Color.Red);
            panel1.BackColor = Color.FromArgb(65, 204, 212, 230);//设置panel白透明www
            //panel1.BackColor = Color.FromArgb(65, 51, 43, 25);//设置panel黑透明www

            this.labelStatus.Text = "";
            this.labelClickCount.Visible = false;
            this.labelClickCount.ForeColor = Color.White;
            this.labelClickCount.Font = new Font("黑体", 12);
            this.CancelButton = buttonClosing;
            //粗糙圆角www
            //this.textBox1.Region = new Region(GetRoundRectPath(new RectangleF(0, 0, this.textBox1.Width, this.textBox1.Height), 10f));
        }
        //粗糙圆角www
        //public GraphicsPath GetRoundRectPath(RectangleF rect, float radius)
        //{
        //    return GetRoundRectPath(rect.X, rect.Y, rect.Width, rect.Height, radius);
        //}

        //public GraphicsPath GetRoundRectPath(float X, float Y, float width, float height, float radius)
        //{
        //    GraphicsPath path = new GraphicsPath();
        //    path.AddLine(X + radius, Y, (X + width) - (radius * 2f), Y);
        //    path.AddArc((X + width) - (radius * 2f), Y, radius * 2f, radius * 2f, 270f, 90f);
        //    path.AddLine((float)(X + width), (float)(Y + radius), (float)(X + width), (float)((Y + height) - (radius * 2f)));
        //    path.AddArc((float)((X + width) - (radius * 2f)), (float)((Y + height) - (radius * 2f)), (float)(radius * 2f), (float)(radius * 2f), 0f, 90f);
        //    path.AddLine((float)((X + width) - (radius * 2f)), (float)(Y + height), (float)(X + radius), (float)(Y + height));
        //    path.AddArc(X, (Y + height) - (radius * 2f), radius * 2f, radius * 2f, 90f, 90f);
        //    path.AddLine(X, (Y + height) - (radius * 2f), X, Y + radius);
        //    path.AddArc(X, Y, radius * 2f, radius * 2f, 180f, 90f);
        //    path.CloseFigure();
        //    return path;
        //}

       //private void AddBtnEvent(buttonEnter btn)
       // {

       // }

        private void textBoxUsername_Leave(object sender, EventArgs e)
        {
            if (this.textBoxUsername.Text.Length == 0)
            {
                return;
            }
            new Thread(() =>
            {
                this.RefreshPossibleUserSync();
            }).Start();
        }

        private void RefreshPossibleUserSync()
        {
            this.refreshedPossibleUser = true;
            this.possibleUserMutex.WaitOne();
            try
            {
                this.possibleUser = (from u in wmsEntities.User
                                     where u.Username == this.textBoxUsername.Text
                                     select u).FirstOrDefault();
                this.networkError = false;
            }
            catch (Exception)
            {
                this.networkError = true;
                if(this.IsDisposed == false)
                {
                    MessageBox.Show("连接数据库失败，请检查网络连接", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }
            finally
            {
                this.possibleUserMutex.ReleaseMutex();
            }
        }

        private void FormLogin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 27) //ESC
            {
                this.Close();
            }
        }

        private void FormLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("确定退出吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void buttonEnter_MouseEnter(object sender, EventArgs e)
        {
            buttonEnter.BackgroundImage = WMS.UI.Properties.Resources.botton1__2_;
        }

        private void buttonEnter_MouseLeave(object sender, EventArgs e)
        {
            buttonEnter.BackgroundImage = WMS.UI.Properties.Resources.botton2__2_;
        }

        private void buttonClosing_MouseEnter(object sender, EventArgs e)
        {
            buttonClosing.BackgroundImage = WMS.UI.Properties.Resources.botton1__3_;
        }

        private void buttonClosing_MouseLeave(object sender, EventArgs e)
        {
            buttonClosing.BackgroundImage = WMS.UI.Properties.Resources.botton2__3_;
        }
    }
}

