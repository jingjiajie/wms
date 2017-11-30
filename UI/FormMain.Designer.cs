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
            System.Windows.Forms.TreeNode treeNode33 = new System.Windows.Forms.TreeNode("用户管理");
            System.Windows.Forms.TreeNode treeNode34 = new System.Windows.Forms.TreeNode("供应商");
            System.Windows.Forms.TreeNode treeNode35 = new System.Windows.Forms.TreeNode("零件");
            System.Windows.Forms.TreeNode treeNode36 = new System.Windows.Forms.TreeNode("仓库");
            System.Windows.Forms.TreeNode treeNode37 = new System.Windows.Forms.TreeNode("基本信息", new System.Windows.Forms.TreeNode[] {
            treeNode33,
            treeNode34,
            treeNode35,
            treeNode36});
            System.Windows.Forms.TreeNode treeNode38 = new System.Windows.Forms.TreeNode("到货管理");
            System.Windows.Forms.TreeNode treeNode39 = new System.Windows.Forms.TreeNode("上架管理");
            System.Windows.Forms.TreeNode treeNode40 = new System.Windows.Forms.TreeNode("收货管理", new System.Windows.Forms.TreeNode[] {
            treeNode38,
            treeNode39});
            System.Windows.Forms.TreeNode treeNode41 = new System.Windows.Forms.TreeNode("发货单管理");
            System.Windows.Forms.TreeNode treeNode42 = new System.Windows.Forms.TreeNode("作业单管理");
            System.Windows.Forms.TreeNode treeNode43 = new System.Windows.Forms.TreeNode("出库单管理");
            System.Windows.Forms.TreeNode treeNode44 = new System.Windows.Forms.TreeNode("发货管理", new System.Windows.Forms.TreeNode[] {
            treeNode41,
            treeNode42,
            treeNode43});
            System.Windows.Forms.TreeNode treeNode45 = new System.Windows.Forms.TreeNode("库存信息");
            System.Windows.Forms.TreeNode treeNode46 = new System.Windows.Forms.TreeNode("库存日志");
            System.Windows.Forms.TreeNode treeNode47 = new System.Windows.Forms.TreeNode("库存汇总");
            System.Windows.Forms.TreeNode treeNode48 = new System.Windows.Forms.TreeNode("库存信息", new System.Windows.Forms.TreeNode[] {
            treeNode45,
            treeNode46,
            treeNode47});
            this.panelTop = new System.Windows.Forms.Panel();
            this.comboBoxWarehouse = new System.Windows.Forms.ComboBox();
            this.panelFill = new System.Windows.Forms.Panel();
            this.panelRight = new System.Windows.Forms.Panel();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.treeViewLeft = new System.Windows.Forms.TreeView();
            this.labelUsername = new System.Windows.Forms.Label();
            this.labelAuth = new System.Windows.Forms.Label();
            this.panelTop.SuspendLayout();
            this.panelFill.SuspendLayout();
            this.panelLeft.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.labelAuth);
            this.panelTop.Controls.Add(this.labelUsername);
            this.panelTop.Controls.Add(this.comboBoxWarehouse);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(762, 100);
            this.panelTop.TabIndex = 0;
            // 
            // comboBoxWarehouse
            // 
            this.comboBoxWarehouse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxWarehouse.FormattingEnabled = true;
            this.comboBoxWarehouse.Location = new System.Drawing.Point(606, 74);
            this.comboBoxWarehouse.Name = "comboBoxWarehouse";
            this.comboBoxWarehouse.Size = new System.Drawing.Size(153, 23);
            this.comboBoxWarehouse.TabIndex = 0;
            this.comboBoxWarehouse.SelectedIndexChanged += new System.EventHandler(this.comboBoxWarehouse_SelectedIndexChanged);
            // 
            // panelFill
            // 
            this.panelFill.Controls.Add(this.panelRight);
            this.panelFill.Controls.Add(this.panelLeft);
            this.panelFill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFill.Location = new System.Drawing.Point(0, 100);
            this.panelFill.Name = "panelFill";
            this.panelFill.Size = new System.Drawing.Size(762, 439);
            this.panelFill.TabIndex = 1;
            // 
            // panelRight
            // 
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRight.Location = new System.Drawing.Point(200, 0);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(562, 439);
            this.panelRight.TabIndex = 1;
            // 
            // panelLeft
            // 
            this.panelLeft.Controls.Add(this.treeViewLeft);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(200, 439);
            this.panelLeft.TabIndex = 0;
            // 
            // treeViewLeft
            // 
            this.treeViewLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewLeft.Location = new System.Drawing.Point(0, 0);
            this.treeViewLeft.Name = "treeViewLeft";
            treeNode33.Name = "节点11";
            treeNode33.Text = "用户管理";
            treeNode34.Name = "节点12";
            treeNode34.Text = "供应商";
            treeNode35.Name = "节点13";
            treeNode35.Text = "零件";
            treeNode36.Name = "节点14";
            treeNode36.Text = "仓库";
            treeNode37.Name = "节点1";
            treeNode37.Text = "基本信息";
            treeNode38.Name = "节点21";
            treeNode38.Text = "到货管理";
            treeNode39.Name = "节点22";
            treeNode39.Text = "上架管理";
            treeNode40.Name = "节点2";
            treeNode40.Text = "收货管理";
            treeNode41.Name = "节点31";
            treeNode41.Text = "发货单管理";
            treeNode42.Name = "节点32";
            treeNode42.Text = "作业单管理";
            treeNode43.Name = "节点33";
            treeNode43.Text = "出库单管理";
            treeNode44.Name = "节点3";
            treeNode44.Text = "发货管理";
            treeNode45.Name = "节点41";
            treeNode45.Text = "库存信息";
            treeNode46.Name = "节点42";
            treeNode46.Text = "库存日志";
            treeNode47.Name = "节点43";
            treeNode47.Text = "库存汇总";
            treeNode48.Name = "节点4";
            treeNode48.Text = "库存信息";
            this.treeViewLeft.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode37,
            treeNode40,
            treeNode44,
            treeNode48});
            this.treeViewLeft.Size = new System.Drawing.Size(200, 439);
            this.treeViewLeft.TabIndex = 0;
            this.treeViewLeft.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewLeft_AfterSelect);
            // 
            // labelUsername
            // 
            this.labelUsername.AutoSize = true;
            this.labelUsername.Location = new System.Drawing.Point(3, 82);
            this.labelUsername.Name = "labelUsername";
            this.labelUsername.Size = new System.Drawing.Size(55, 15);
            this.labelUsername.TabIndex = 1;
            this.labelUsername.Text = "label1";
            // 
            // labelAuth
            // 
            this.labelAuth.AutoSize = true;
            this.labelAuth.Location = new System.Drawing.Point(64, 82);
            this.labelAuth.Name = "labelAuth";
            this.labelAuth.Size = new System.Drawing.Size(55, 15);
            this.labelAuth.TabIndex = 2;
            this.labelAuth.Text = "label1";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(762, 539);
            this.Controls.Add(this.panelFill);
            this.Controls.Add(this.panelTop);
            this.Name = "FormMain";
            this.Text = "主窗口";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
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
        private System.Windows.Forms.ComboBox comboBoxWarehouse;
        private System.Windows.Forms.Label labelUsername;
        private System.Windows.Forms.Label labelAuth;
    }
}