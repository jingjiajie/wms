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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.myDelegate += new MyDelegate(Add);
            f1.Owner = this;            
            f1.Show();
        }
        public void Add(string Item1, string Item2)
        {
            this.listView1.Items.Add(Item1);
            this.listView1.Items[listView1.Items.Count - 1].SubItems.Add(Item2);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int i = 0;
            if (listView1.SelectedItems.Count > 0)
            {
                i = listView1.SelectedItems[0].Index;
                listView1.Items[i].Remove();
            }
        }
    }
}
