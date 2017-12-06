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
using WMS.UI.FromShipmentTicket;
using WMS.UI.PutOutStorageTicket;
using WMS.DataAccess;

namespace WMS.UI
{
    public partial class FormMain : Form
    {
        private User user = null;
        private WMSEntities wmsEntities = new WMSEntities();

        private Action formClosedCallback;

        public FormMain(int userID)
        {
            InitializeComponent();
            User user = (from u in this.wmsEntities.User
                         where u.ID == userID
                         select u).Single();
            this.user = user;
        }

        public void SetFormClosedCallback(Action callback)
        {
            this.formClosedCallback = callback;
        }

        private void RefreshTreeView()
        {
            TreeNode[] treeNodes = new TreeNode[]
            {
                MakeTreeNode("基本信息",new TreeNode[]{
                    MakeTreeNode("用户管理"),
                    MakeTreeNode("供应商管理"),
                    MakeTreeNode("零件管理"),
                    MakeTreeNode("仓库管理"),
                    MakeTreeNode("项目管理"),
                    }),
                MakeTreeNode("收货管理",new TreeNode[]{
                    MakeTreeNode("到货管理"),
                    MakeTreeNode("上架管理"),
                    }),
                MakeTreeNode("发货管理",new TreeNode[]{
                    MakeTreeNode("发货单管理"),
                    MakeTreeNode("作业单管理"),
                    MakeTreeNode("出库单管理"),
                    }),
                MakeTreeNode("库存管理",new TreeNode[]{
                    MakeTreeNode("库存信息")
                    })
            };

            this.treeViewLeft.Nodes.Clear();
            this.treeViewLeft.Nodes.AddRange((from node in
                                                  (from node in treeNodes
                                                   where HasAuthority(node.Text)
                                                   select GetAuthenticatedSubTreeNodes(node))
                                              where node.Nodes.Count > 0
                                              select node).ToArray());
        }

        //检测用户是否有相应功能的权限
        private bool HasAuthority(string funcName)
        {
            var searchResult = (from fa in FormMainMetaData.FunctionAuthorities
                                where fa.FunctionName == funcName
                                select fa.Authority).ToArray();
            if (searchResult.Length == 0) {
                return true;
            }
            Authority authority = searchResult[0];
            if (((int)authority & this.user.Authority) > 0)
            {
                return true;
            }
            return false;
        }

        //获取有权限的所有子节点
        private TreeNode GetAuthenticatedSubTreeNodes(TreeNode node)
        {
            if (HasAuthority(node.Text) == false)
            {
                return null;
            }

            TreeNode newNode = (TreeNode)node.Clone();
            newNode.Nodes.Clear();

            foreach (TreeNode curNode in node.Nodes)
            {
                if (HasAuthority(curNode.Text))
                {
                    newNode.Nodes.Add(GetAuthenticatedSubTreeNodes(curNode));
                }
            }
            return newNode;
        }

        private static TreeNode MakeTreeNode(string text, TreeNode[] subNodes = null)
        {
            TreeNode node = new TreeNode() { Text = text };
            if (subNodes == null)
            {
                return node;
            }
            foreach (TreeNode subNode in subNodes)
            {
                node.Nodes.Add(subNode);
            }
            return node;
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

        private void FormMain_Load(object sender, EventArgs e)
        {
            //刷新左边树形框
            this.RefreshTreeView();

            //刷新顶部
            this.labelUsername.Text = this.user.Username;
            this.labelAuth.Text = this.user.AuthorityName;

            //窗体大小根据显示屏改变
            int DeskWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int DeskHeight = Screen.PrimaryScreen.WorkingArea.Height;
            this.Width = Convert.ToInt32(DeskWidth * 0.8);
            this.Height = Convert.ToInt32(DeskHeight * 0.8);

            //下拉栏显示仓库
            WMSEntities wms = new WMSEntities();
            var allWare = (from s in wms.Warehouse select s).ToArray();
            for (int i = 0; i < allWare.Count(); i++)
            {
                Warehouse ware = allWare[i];
                comboBoxWarehouse.Items.Add(ware.Name);
            }

            var allProject = (from s in wms.Project select s).ToArray();
            for (int i = 0; i < allWare.Count(); i++)
            {
                Project objpro = allProject[i];
                comboBoxProject.Items.Add(objpro.Name);
            }
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
                FormShipmentTicket l = new FormShipmentTicket();//实例化子窗口
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
                FormJobTicket l = new FormJobTicket();//实例化子窗口
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
                FormPutOutStorageTicket l = new FormPutOutStorageTicket();//实例化子窗口
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

        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.formClosedCallback?.Invoke();
        }
    }
}
