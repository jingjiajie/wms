using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WMS.UI.FormReceipt;
using WMS.UI.FormDelivery;

namespace WMS.UI
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }
        private void creatTreeList()
        {
            ////建立父节点
            //TreeNode tn1 = treeViewLeft.Nodes.Add("基本信息");
            //TreeNode tn2 = treeViewLeft.Nodes.Add("收货管理");
            //TreeNode tn3 = treeViewLeft.Nodes.Add("发货管理");
            //TreeNode tn4 = treeViewLeft.Nodes.Add("库存信息");
            ////建立子节点-1
            //TreeNode Jtn1 = treeViewLeft.Nodes.Add("用户管理");
            //TreeNode Jtn2 = treeViewLeft.Nodes.Add("供应商");
            //TreeNode Jtn3 = treeViewLeft.Nodes.Add("零件");
            //TreeNode Jtn4 = treeViewLeft.Nodes.Add("仓库面积");
            ////添加进父节点-1
            //tn1.Nodes.Add(Jtn1);
            //tn1.Nodes.Add(Jtn2);
            //tn1.Nodes.Add(Jtn3);
            //tn1.Nodes.Add(Jtn4);
            ////建立子节点-2
            //TreeNode Stn1 = treeViewLeft.Nodes.Add("到货管理");
            //TreeNode Stn2 = treeViewLeft.Nodes.Add("上架管理");
            ////添加进父节点-2
            //tn2.Nodes.Add(Stn1);
            //tn2.Nodes.Add(Stn2);
            ////建立子节点-3
            //TreeNode Ftn1 = treeViewLeft.Nodes.Add("发货单管理");
            //TreeNode Ftn2 = treeViewLeft.Nodes.Add("作业单管理");
            //TreeNode Ftn3 = treeViewLeft.Nodes.Add("出库单管理");
            ////添加进父节点-3
            //tn3.Nodes.Add(Ftn1);
            //tn3.Nodes.Add(Ftn2);
            //tn3.Nodes.Add(Ftn3);
            ////建立子节点-4
            //TreeNode Ktn1 = treeViewLeft.Nodes.Add("库存管理");
            //TreeNode Ktn2 = treeViewLeft.Nodes.Add("库存日志");
            //TreeNode Ktn3 = treeViewLeft.Nodes.Add("库存汇总");
            ////添加进父节点-4
            //tn4.Nodes.Add(Ktn1);
            //tn4.Nodes.Add(Ktn2);
            //tn4.Nodes.Add(Ktn3);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            //窗体大小根据显示屏改变
            int DeskWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int DeskHeight = Screen.PrimaryScreen.WorkingArea.Height;
            this.Width = Convert.ToInt32(DeskWidth * 0.8);
            this.Height = Convert.ToInt32(DeskHeight * 0.8);

            treeViewLeft.ExpandAll();//树形栏显示所有节点   

        }

        private void treeViewLeft_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeViewLeft.SelectedNode.Text == "用户管理")
            {
                this.panelRight.Controls.Clear();//清空
                panelRight.Visible = true;
                FromBaseUser l = new FromBaseUser();//实例化子窗口
                l.TopLevel = false;
                l.Dock = System.Windows.Forms.DockStyle.Fill;//窗口大小
                l.FormBorderStyle = FormBorderStyle.None;//没有标题栏
                this.panelRight.Controls.Add(l);
                l.Show();
            }
            if (treeViewLeft.SelectedNode.Text == "供应商")
            {
                this.panelRight.Controls.Clear();//清空
                panelRight.Visible = true;
                FormBaseSupplier l = new FormBaseSupplier();//实例化子窗口
                l.TopLevel = false;
                l.Dock = System.Windows.Forms.DockStyle.Fill;//窗口大小
                l.FormBorderStyle = FormBorderStyle.None;//没有标题栏
                this.panelRight.Controls.Add(l);
                l.Show();
            }
            if (treeViewLeft.SelectedNode.Text == "零件")
            {
                this.panelRight.Controls.Clear();//清空
                panelRight.Visible = true;
                FormBaseComponent l = new FormBaseComponent();//实例化子窗口
                l.TopLevel = false;
                l.Dock = System.Windows.Forms.DockStyle.Fill;//窗口大小
                l.FormBorderStyle = FormBorderStyle.None;//没有标题栏
                this.panelRight.Controls.Add(l);
                l.Show();
            }
            if (treeViewLeft.SelectedNode.Text == "仓库面积")
            {
                this.panelRight.Controls.Clear();//清空
                panelRight.Visible = true;
                FormBaseWarehouse l = new FormBaseWarehouse();//实例化子窗口
                l.TopLevel = false;
                l.Dock = System.Windows.Forms.DockStyle.Fill;//窗口大小
                l.FormBorderStyle = FormBorderStyle.None;//没有标题栏
                this.panelRight.Controls.Add(l);
                l.Show();
            }
            if (treeViewLeft.SelectedNode.Text == "到货管理")
            {
                this.panelRight.Controls.Clear();//清空
                panelRight.Visible = true;
                FormBaseWarehouse l = new FormBaseWarehouse();//实例化子窗口
                l.TopLevel = false;
                l.Dock = System.Windows.Forms.DockStyle.Fill;//窗口大小
                l.FormBorderStyle = FormBorderStyle.None;//没有标题栏
                this.panelRight.Controls.Add(l);
                l.Show();
            }
            if (treeViewLeft.SelectedNode.Text == "到货管理")
            {
                this.panelRight.Controls.Clear();//清空
                panelRight.Visible = true;
                FormReceiptArrival l = new FormReceiptArrival();//实例化子窗口
                l.TopLevel = false;
                l.Dock = System.Windows.Forms.DockStyle.Fill;//窗口大小
                l.FormBorderStyle = FormBorderStyle.None;//没有标题栏
                this.panelRight.Controls.Add(l);
                l.Show();
            }
            if (treeViewLeft.SelectedNode.Text == "上架管理")
            {
                this.panelRight.Controls.Clear();//清空
                panelRight.Visible = true;
                FormReceiptShelves l = new FormReceiptShelves();//实例化子窗口
                l.TopLevel = false;
                l.Dock = System.Windows.Forms.DockStyle.Fill;//窗口大小
                l.FormBorderStyle = FormBorderStyle.None;//没有标题栏
                this.panelRight.Controls.Add(l);
                l.Show();
            }
            if (treeViewLeft.SelectedNode.Text == "发货单管理")
            {
                this.panelRight.Controls.Clear();//清空
                panelRight.Visible = true;
                FormDeliverySend l = new FormDeliverySend();//实例化子窗口
                l.TopLevel = false;
                l.Dock = System.Windows.Forms.DockStyle.Fill;//窗口大小
                l.FormBorderStyle = FormBorderStyle.None;//没有标题栏
                this.panelRight.Controls.Add(l);
                l.Show();
            }
            if (treeViewLeft.SelectedNode.Text == "作业单管理")
            {
                this.panelRight.Controls.Clear();//清空
                panelRight.Visible = true;
                FormDeliveryJob l = new FormDeliveryJob();//实例化子窗口
                l.TopLevel = false;
                l.Dock = System.Windows.Forms.DockStyle.Fill;//窗口大小
                l.FormBorderStyle = FormBorderStyle.None;//没有标题栏
                this.panelRight.Controls.Add(l);
                l.Show();
            }
            if (treeViewLeft.SelectedNode.Text == "出库单管理")
            {
                this.panelRight.Controls.Clear();//清空
                panelRight.Visible = true;
                FormDeliveryOutput l = new FormDeliveryOutput();//实例化子窗口
                l.TopLevel = false;
                l.Dock = System.Windows.Forms.DockStyle.Fill;//窗口大小
                l.FormBorderStyle = FormBorderStyle.None;//没有标题栏
                this.panelRight.Controls.Add(l);
                l.Show();
            }
            if (treeViewLeft.SelectedNode.Text == "库存信息")
            {
                this.panelRight.Controls.Clear();//清空
                panelRight.Visible = true;
                var formBaseStock = new FormStockInfo();//实例化子窗口
                formBaseStock.TopLevel = false;
                formBaseStock.Dock = DockStyle.Fill;//窗口大小
                formBaseStock.FormBorderStyle = FormBorderStyle.None;//没有标题栏
                this.panelRight.Controls.Add(formBaseStock);
                formBaseStock.Show();
            }
        }
    }
}
