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
            this.Top = 25;
            this.Left = (int)(a * Screen.PrimaryScreen.Bounds.Width);
            this.Width = 200;
            this.Height = 75;
            this.textBox1.Text = "";
          


        }






        

    }
}
