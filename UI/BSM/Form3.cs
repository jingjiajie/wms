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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //赋值为12

            //Class1.A = 12;
            //Form4 m_form = new Form4(this);
            //m_form.Show();
        }
        public class Class1
        {
            public static int a;
            static Class1()
            {
                a = 0;
            }
            //属性
            public static int A
            {
                get
                {
                    return a;
                }
                set
                {
                    a = value;
                }
            }
        }
    }
    
}
