using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;

using System.Windows.Forms;
using WMS.UI.FormReceipt;
using WMS.UI.FormDelivery;
using WMS.DataAccess;

namespace WMS.UI
{
    public partial class FormMain : Form
    { 



        public FormMain()
        {
            InitializeComponent();
        }
        public FormMain(string a, int b)
        {
            InitializeComponent();
            labelUsername.Text = a;
            string auth = "无";
            if(b==2)
            {
                auth = "收货员";
            }
            if (b == 4)
            {
                auth = "发货员";
            }
            if (b == 8)
            {
                auth = "结算员";
            }
            if (b == 15)
            {
                auth = "管理员";
            }
            labelAuth.Text = auth;

        }
        private void ShowAllSubNodes(TreeNode node)
        {
            foreach (TreeNode curSubNode in node.Nodes)
            {
                curSubNode.ForeColor = Color.Black;
                this.ShowAllSubNodes(curSubNode);
            }
        }

        private void ShowAll()
        {
            foreach (TreeNode curNode in this.treeViewLeft.Nodes)
            {
                curNode.ForeColor = Color.Black;
                this.ShowAllSubNodes(curNode);
            }
        }

        private void HideAll()
        {
            this.HideBase();
            this.HideDelivery();
            this.HideReceipt();
            this.HideStock();
        }

        private void HideBase()
        {
            treeViewLeft.Nodes[0].ForeColor = SystemColors.Control;
            treeViewLeft.Nodes[0].Nodes[0].ForeColor = SystemColors.Control;
            treeViewLeft.Nodes[0].Nodes[1].ForeColor = SystemColors.Control;
            treeViewLeft.Nodes[0].Nodes[2].ForeColor = SystemColors.Control;
            treeViewLeft.Nodes[0].Nodes[3].ForeColor = SystemColors.Control;
        }
        private void HideReceipt()
        {
            treeViewLeft.Nodes[1].ForeColor = SystemColors.Control;
            treeViewLeft.Nodes[1].Nodes[0].ForeColor = SystemColors.Control;
            treeViewLeft.Nodes[1].Nodes[1].ForeColor = SystemColors.Control;
        }
        private void HideDelivery()
        {
            treeViewLeft.Nodes[2].ForeColor = SystemColors.Control;
            treeViewLeft.Nodes[2].Nodes[0].ForeColor = SystemColors.Control;
            treeViewLeft.Nodes[2].Nodes[1].ForeColor = SystemColors.Control;
            treeViewLeft.Nodes[2].Nodes[2].ForeColor = SystemColors.Control;
        }
        private void HideStock()
        {
            treeViewLeft.Nodes[3].ForeColor = SystemColors.Control;
            treeViewLeft.Nodes[3].Nodes[0].ForeColor = SystemColors.Control;
            treeViewLeft.Nodes[3].Nodes[1].ForeColor = SystemColors.Control;
            treeViewLeft.Nodes[3].Nodes[2].ForeColor = SystemColors.Control;
        }

        private void treeViewLeft_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node != null)
            {
                if (e.Node.ForeColor == SystemColors.Control)
                {
                    e.Cancel = true;  //不让选中禁用节点
                }
            }
        }
        private void treeViewLeft_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node != null)
            {
                if (e.Node.ForeColor == SystemColors.Control)
                {
                    e.Cancel = true; //不让选中禁用节点

                }
            }
        }

        private void creatTreeList()
        {
 
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            //panelLeft.Visible = false;
            //panelFill.Visible = false;

            this.HideAll();

            //窗体大小根据显示屏改变
            int DeskWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int DeskHeight = Screen.PrimaryScreen.WorkingArea.Height;
            this.Width = Convert.ToInt32(DeskWidth * 0.8);
            this.Height = Convert.ToInt32(DeskHeight * 0.8);
            //formReceiptArrival.InitComponents();

            //下拉栏显示仓库
            WMSEntities wms = new WMSEntities();
            var allWare = (from s in wms.Warehouse select s).ToArray();
            for (int i = 0; i < allWare.Count(); i++)
            {
                Warehouse ware = allWare[i];
                comboBoxWarehouse.Items.Add(ware.Name);
            }
            
            //MessageBox.Show("请选择仓库");




//            treeViewLeft.ExpandAll();//树形栏显示所有节点   
//            new Thread(new ThreadStart(() =>
//            {
//                var wmsEntities = new WMSEntities(); //初始化EF框架
//                wmsEntities.Database.Connection.Open(); //打开EF连接
//            })).Start();            
        }

        private void treeViewLeft_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Utilities.SendMessage(this.panelRight.Handle, Utilities.WM_SETREDRAW, 0, IntPtr.Zero);
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
            if (treeViewLeft.SelectedNode.Text == "仓库")
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
            if (treeViewLeft.SelectedNode.Text == "项目")
            {
                this.panelRight.Controls.Clear();//清空
                panelRight.Visible = true;
                FormBase.FormBaseProject l = new FormBase.FormBaseProject();//实例化子窗口
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
            Utilities.SendMessage(this.panelRight.Handle, Utilities.WM_SETREDRAW, 1, IntPtr.Zero);
        }

        private void comboBoxWarehouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            //panelLeft.Visible = true;
            //panelFill.Visible = true;
            treeViewLeft.ExpandAll();//树形栏显示所有节点   

            //判断用户身份显示树节点
            this.ShowAll();
            if (labelAuth.Text == "无")//游客
                {
                    HideBase();
                    HideReceipt();
                    HideDelivery();
                    HideStock();
                }
            if (labelAuth.Text == "收货员")//收货员
                {
                    HideBase();
                    HideDelivery();
                    HideStock();
                }
            if (labelAuth.Text == "发货员")//发货员
                {
                    HideBase();
                    HideReceipt();
                    HideStock();
                }
            if (labelAuth.Text == "结算员")
                {
                    HideBase();
                    HideReceipt();
                    HideDelivery();
                }

        }
    }
}
