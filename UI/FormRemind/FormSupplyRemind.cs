using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace WMS.UI
{
    public partial class FormSupplyRemind : Form
    {
        public FormSupplyRemind()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            
            //this.Opacity = 109;
        }

        private void FormSupplyRemind_Load(object sender, EventArgs e)
        {
            double a = 0.35;
            this.Top = 0;//25
            this.Left = (int)(a * Screen.PrimaryScreen.Bounds.Width);
            this.Width = 400;
            this.Height = 100;//75
            this.textBox1.Text = "";
            this.TransparencyKey = System.Drawing.Color.Black;//设置黑的是透明色
            this.BackColor = System.Drawing.Color.Black;//把窗口的背景色设置为黑色

        }






        

    }
}
