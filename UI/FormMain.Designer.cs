namespace WMS.UI
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("用户管理");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("供应商");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("零件");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("仓库面积");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("基本信息", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4});
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("到货管理");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("上架管理");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("收货管理", new System.Windows.Forms.TreeNode[] {
            treeNode6,
            treeNode7});
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("发货单管理");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("作业单管理");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("出库单管理");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("发货管理", new System.Windows.Forms.TreeNode[] {
            treeNode9,
            treeNode10,
            treeNode11});
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("库存信息");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("库存日志");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("库存汇总");
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("库存信息", new System.Windows.Forms.TreeNode[] {
            treeNode13,
            treeNode14,
            treeNode15});
            this.panelTop = new System.Windows.Forms.Panel();
            this.panelFill = new System.Windows.Forms.Panel();
            this.panelRight = new System.Windows.Forms.Panel();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.treeViewLeft = new System.Windows.Forms.TreeView();
            this.panelFill.SuspendLayout();
            this.panelLeft.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1143, 160);
            this.panelTop.TabIndex = 0;
            // 
            // panelFill
            // 
            this.panelFill.Controls.Add(this.panelRight);
            this.panelFill.Controls.Add(this.panelLeft);
            this.panelFill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFill.Location = new System.Drawing.Point(0, 160);
            this.panelFill.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelFill.Name = "panelFill";
            this.panelFill.Size = new System.Drawing.Size(1143, 702);
            this.panelFill.TabIndex = 1;
            // 
            // panelRight
            // 
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRight.Location = new System.Drawing.Point(300, 0);
            this.panelRight.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(843, 702);
            this.panelRight.TabIndex = 1;
            // 
            // panelLeft
            // 
            this.panelLeft.Controls.Add(this.treeViewLeft);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(300, 702);
            this.panelLeft.TabIndex = 0;
            // 
            // treeViewLeft
            // 
            this.treeViewLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewLeft.Location = new System.Drawing.Point(0, 0);
            this.treeViewLeft.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.treeViewLeft.Name = "treeViewLeft";
            treeNode1.Name = "节点11";
            treeNode1.Text = "用户管理";
            treeNode2.Name = "节点12";
            treeNode2.Text = "供应商";
            treeNode3.Name = "节点13";
            treeNode3.Text = "零件";
            treeNode4.Name = "节点14";
            treeNode4.Text = "仓库面积";
            treeNode5.Name = "节点1";
            treeNode5.Text = "基本信息";
            treeNode6.Name = "节点21";
            treeNode6.Text = "到货管理";
            treeNode7.Name = "节点22";
            treeNode7.Text = "上架管理";
            treeNode8.Name = "节点2";
            treeNode8.Text = "收货管理";
            treeNode9.Name = "节点31";
            treeNode9.Text = "发货单管理";
            treeNode10.Name = "节点32";
            treeNode10.Text = "作业单管理";
            treeNode11.Name = "节点33";
            treeNode11.Text = "出库单管理";
            treeNode12.Name = "节点3";
            treeNode12.Text = "发货管理";
            treeNode13.Name = "节点41";
            treeNode13.Text = "库存信息";
            treeNode14.Name = "节点42";
            treeNode14.Text = "库存日志";
            treeNode15.Name = "节点43";
            treeNode15.Text = "库存汇总";
            treeNode16.Name = "节点4";
            treeNode16.Text = "库存信息";
            this.treeViewLeft.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode5,
            treeNode8,
            treeNode12,
            treeNode16});
            this.treeViewLeft.Size = new System.Drawing.Size(300, 702);
            this.treeViewLeft.TabIndex = 0;
            this.treeViewLeft.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewLeft_AfterSelect);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1143, 862);
            this.Controls.Add(this.panelFill);
            this.Controls.Add(this.panelTop);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FormMain";
            this.Text = "主窗口";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.panelFill.ResumeLayout(false);
            this.panelLeft.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panelFill;
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.TreeView treeViewLeft;
    }
}