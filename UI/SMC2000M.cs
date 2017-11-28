using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WMS.DataAccess;

namespace WMS.UI
{
    public partial class SMC2000M : Form
    {
        public SMC2000M()
        {
            InitializeComponent();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if(treeView1.SelectedNode.Text=="用户管理")
            {
                panel1.Visible = true;
                BSM.BMS4000M b40 = new BSM.BMS4000M();//实例化子窗口
                b40.TopLevel = false;
                b40.Dock = System.Windows.Forms.DockStyle.Fill;//窗口大小
                b40.FormBorderStyle = FormBorderStyle.None;//没有标题栏
                this.panel1.Controls.Add(b40);
                b40.Show();
            }
            if (treeView1.SelectedNode.Text == "供应商")
            {

                panel1.Visible = true;
                BSM.BMS4100M b41 = new BSM.BMS4100M();//实例化子窗口
                b41.TopLevel = false;
                b41.Dock = System.Windows.Forms.DockStyle.Fill;//窗口大小
                b41.FormBorderStyle = FormBorderStyle.None;//没有标题栏
                this.panel1.Controls.Add(b41);
                b41.Show();
            }
            if (treeView1.SelectedNode.Text == "零件")
            {
                panel1.Visible = false;
            }
            if (treeView1.SelectedNode.Text == "仓库面积")
            {
                panel1.Visible = false;
            }
            if (treeView1.SelectedNode.Text == "到货管理")
            {
                panel1.Visible = true;
            }
        }



        private void SMC2000M_Load(object sender, EventArgs e)
        {
            int DeskWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int DeskHeight = Screen.PrimaryScreen.WorkingArea.Height;
            this.Width = Convert.ToInt32(DeskWidth * 0.8);
            this.Height = Convert.ToInt32(DeskHeight * 0.8);

            //panel2.Height=Convert.ToInt32(DeskHeight * 0.8*0.2);
            //panel2.Width = Convert.ToInt32(DeskWidth * 0.8);

            //panel1.Height = Convert.ToInt32(DeskHeight * 0.8 * 0.8);
            //panel1.Width = Convert.ToInt32(DeskWidth * 0.8*0.85);

            //treeView1.Height= Convert.ToInt32(DeskHeight * 0.8 * 0.8);
            //treeView1.Width = Convert.ToInt32(DeskWidth * 0.8 * 0.15);
            //treeView1.Location.Y= Convert.ToInt32(DeskHeight * 0.8 * 0.2);


            treeView1.ExpandAll();//树形栏显示所有节点

            //this.IsMdiContainer = true;//当前窗口设置成mdi容器
            //BSM4000M b40 = new BSM4000M();//实例化子窗口  
            //b40.MdiParent = this;//设置mdi父容器为当前窗口 
            //b40.Parent = panel1;//设置父窗体为panel         
            //b40.Size = panel1.Size;//统一大小     
            //b40.Show();//显示





            //panel1.Visible = false;
            //this.Resize += new EventHandler(SMC2000M_Resize);//窗体调整大小时引发事件

            //X = this.Width;//获取窗体的宽度

            //Y = this.Height;//获取窗体的高度

            //setTag(this);//调用方法
        }
    }
}
