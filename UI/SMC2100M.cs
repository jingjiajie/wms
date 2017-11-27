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
    public partial class SMC2100M : Form
    {
        public SMC2100M()
        {
            InitializeComponent();
        }
        private void timer1_Tick(object sender, EventArgs e)

        {

            this.Opacity += 0.1;                    //设置窗体的不透明级别

        }

        private void SMC2100M_Load(object sender, EventArgs e)
        {
            //panel1.Visible = true;
            //SMC2000M s20 = new SMC2000M();//实例化子窗口
            ////s20.TopLevel = false;
            //s20.Dock = System.Windows.Forms.DockStyle.Fill;//窗口大小
            //s20.FormBorderStyle = FormBorderStyle.None;//没有标题栏
            //this.panel1.Controls.Add(s20);
            //s20.Show();
            
            ////当前窗口设置成mdi容器  
            //this.IsMdiContainer = true;
            ////实例化子窗口  
            //SMC2000M s20 = new SMC2000M();
            ////设置mdi父容器为当前窗口  
            //s20.MdiParent = this;
            ////设置父窗体为panel  
            //s20.Parent = panel1;
            ////统一大小  
            //s20.Size = panel1.Size;
            ////显示  
            //s20.Show();
        }
    }
}
