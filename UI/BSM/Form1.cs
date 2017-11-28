using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WMS.UI.BSM
{
    public delegate void MyDelegate(string Item1, string Item2);//委托实质上是一个类
    public partial class Form1 : Form
    {


        public MyDelegate myDelegate;//声明一个委托的对象
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSure_Click(object sender, EventArgs e)
        {
            
            myDelegate(this.textBox1.Text, this.textBox2.Text);
            this.Dispose();
        }
    }
}
