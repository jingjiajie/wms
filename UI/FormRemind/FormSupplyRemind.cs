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
           
        }



        private void FormSupplyRemind_Resize(object sender, EventArgs e)
        {
            this.Region = null;
            SetWindowRegion();

        }

        private void FormSupplyRemind_Paint(object sender, PaintEventArgs e)
        {
           
        }


        public void SetWindowRegion()
        {
            GraphicsPath FormPath;
            FormPath = new GraphicsPath();
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);//this.Left-10,this.Top-10,this.Width-10,this.Height-10);                 
            FormPath = GetRoundedRectPath(rect, 30);
            this.Region = new Region(FormPath);
        }
        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            int diameter = radius;
            Rectangle arcRect = new Rectangle(rect.Location, new Size(diameter, diameter));
            GraphicsPath path = new GraphicsPath();
            //左上角
            path.AddArc(arcRect, 180, 90);
            //   右上角   
            arcRect.X = rect.Right - diameter;
            path.AddArc(arcRect, 270, 90);
            //右下角
            arcRect.Y = rect.Bottom - diameter;
            path.AddArc(arcRect, 0, 90);
            //   左下角   
            arcRect.X = rect.Left;
            path.AddArc(arcRect, 90, 90);
            path.CloseFigure();
            return path;
        }


    }
}
