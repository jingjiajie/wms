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


        //private FormReceiptArrival formReceiptArrival = new FormReceiptArrival();
        
        public FormMain()
        {
            InitializeComponent();
        }
        private void creatTreeList()
        {
 
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            //窗体大小根据显示屏改变
            int DeskWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int DeskHeight = Screen.PrimaryScreen.WorkingArea.Height;
            this.Width = Convert.ToInt32(DeskWidth * 0.8);
            this.Height = Convert.ToInt32(DeskHeight * 0.8);
            //formReceiptArrival.InitComponents();

            treeViewLeft.ExpandAll();//树形栏显示所有节点   
            new Thread(new ThreadStart(() =>
            {
                var wmsEntities = new WMSEntities(); //初始化EF框架
                wmsEntities.Database.Connection.Open(); //打开EF连接
            })).Start();            
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
    }
}
