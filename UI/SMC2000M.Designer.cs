namespace WMS.UI
{
    partial class SMC2000M
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
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("库存管理", new System.Windows.Forms.TreeNode[] {
            treeNode13,
            treeNode14,
            treeNode15});
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Left;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "节点04";
            treeNode1.Text = "用户管理";
            treeNode2.Name = "节点01";
            treeNode2.Text = "供应商";
            treeNode3.Name = "节点02";
            treeNode3.Text = "零件";
            treeNode4.Name = "节点03";
            treeNode4.Text = "仓库面积";
            treeNode5.Name = "节点0";
            treeNode5.Text = "基本信息";
            treeNode6.Name = "节点11";
            treeNode6.Text = "到货管理";
            treeNode7.Name = "节点12";
            treeNode7.Text = "上架管理";
            treeNode8.Name = "节点1";
            treeNode8.Text = "收货管理";
            treeNode9.Name = "节点21";
            treeNode9.Text = "发货单管理";
            treeNode10.Name = "节点22";
            treeNode10.Text = "作业单管理";
            treeNode11.Name = "节点23";
            treeNode11.Text = "出库单管理";
            treeNode12.Name = "节点2";
            treeNode12.Text = "发货管理";
            treeNode13.Name = "节点31";
            treeNode13.Text = "库存信息";
            treeNode14.Name = "节点32";
            treeNode14.Text = "库存日志";
            treeNode15.Name = "节点33";
            treeNode15.Text = "库存汇总";
            treeNode16.Name = "节点3";
            treeNode16.Text = "库存管理";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode5,
            treeNode8,
            treeNode12,
            treeNode16});
            this.treeView1.Size = new System.Drawing.Size(201, 648);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(201, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(812, 648);
            this.panel1.TabIndex = 1;
            // 
            // SMC2000M
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1013, 648);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.treeView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.IsMdiContainer = true;
            this.Name = "SMC2000M";
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
            this.Text = "SMC2000M";
            this.Load += new System.EventHandler(this.SMC2000M_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Panel panel1;
    }
}