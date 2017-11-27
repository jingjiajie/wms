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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView1.SelectedNode.Text == "节点2")
            {
                panel4.Visible = true;
                BSM.BMS4000M b40 = new BSM.BMS4000M();//实例化子窗口
                b40.TopLevel = false;
                b40.Dock = System.Windows.Forms.DockStyle.Fill;//窗口大小
                b40.FormBorderStyle = FormBorderStyle.None;//没有标题栏
                this.panel4.Controls.Add(b40);
                b40.Show();
            }
            if (treeView1.SelectedNode.Text == "节点3")
            {
                panel4.Visible = false;
            }
        }
    }
}
