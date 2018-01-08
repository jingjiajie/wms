namespace WMS.UI
{
    partial class FormBaseWarehouse
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBaseWarehouse));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.labelStatus = new System.Windows.Forms.ToolStripLabel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelPager1 = new System.Windows.Forms.Panel();
            this.groupBoxWarehouse = new System.Windows.Forms.GroupBox();
            this.reoGridControlWarehouse = new unvell.ReoGrid.ReoGridControl();
            this.groupBoxProject = new System.Windows.Forms.GroupBox();
            this.reoGridControlProject = new unvell.ReoGrid.ReoGridControl();
            this.groupBoxPackageUnit = new System.Windows.Forms.GroupBox();
            this.reoGridControlPackageUnit = new unvell.ReoGrid.ReoGridControl();
            this.panelPager2 = new System.Windows.Forms.Panel();
            this.panelPager3 = new System.Windows.Forms.Panel();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.toolStripLabelSelectWarehouse = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBoxSelectWarehouse = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripTextBoxSelectWarehouse = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButtonSelectWarehouse = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonWarehouseAdd = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonAlterWarehouse = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDeleteWarehouse = new System.Windows.Forms.ToolStripButton();
            this.toolStripTop = new System.Windows.Forms.ToolStrip();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabelSelectProject = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBoxSelectProject = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripTextBoxSelectProject = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButtonSelectProject = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonAddProject = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonAlterProject = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDelectProject = new System.Windows.Forms.ToolStripButton();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabelSelectPackageUnit = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBoxSelectPackageUnit = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripTextBoxSelectPackageUnit = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButtonSelectPackageUnit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonAddPackageUnit = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonAlterPackageUnit = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDelectPackageUnit = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBoxWarehouse.SuspendLayout();
            this.groupBoxProject.SuspendLayout();
            this.groupBoxPackageUnit.SuspendLayout();
            this.toolStripTop.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.labelStatus});
            this.toolStrip1.Location = new System.Drawing.Point(0, 497);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(839, 25);
            this.toolStrip1.TabIndex = 8;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(35, 22);
            this.toolStripStatusLabel1.Text = "状态:";
            // 
            // labelStatus
            // 
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(56, 22);
            this.labelStatus.Text = "仓库信息";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.85625F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.21476F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.85624F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.21476F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.73068F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.127317F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.panelPager1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.groupBoxWarehouse, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.toolStripTop, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBoxProject, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBoxPackageUnit, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.panelPager2, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.panelPager3, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.toolStrip3, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.toolStrip2, 4, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.263158F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 89.47369F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.263158F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(839, 497);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // panelPager1
            // 
            this.panelPager1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPager1.Location = new System.Drawing.Point(3, 473);
            this.panelPager1.Name = "panelPager1";
            this.panelPager1.Size = new System.Drawing.Size(236, 21);
            this.panelPager1.TabIndex = 11;
            // 
            // groupBoxWarehouse
            // 
            this.groupBoxWarehouse.Controls.Add(this.reoGridControlWarehouse);
            this.groupBoxWarehouse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxWarehouse.Location = new System.Drawing.Point(3, 29);
            this.groupBoxWarehouse.Name = "groupBoxWarehouse";
            this.groupBoxWarehouse.Size = new System.Drawing.Size(236, 438);
            this.groupBoxWarehouse.TabIndex = 12;
            this.groupBoxWarehouse.TabStop = false;
            this.groupBoxWarehouse.Text = "仓库信息";
            // 
            // reoGridControlWarehouse
            // 
            this.reoGridControlWarehouse.BackColor = System.Drawing.Color.White;
            this.reoGridControlWarehouse.ColumnHeaderContextMenuStrip = null;
            this.reoGridControlWarehouse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reoGridControlWarehouse.LeadHeaderContextMenuStrip = null;
            this.reoGridControlWarehouse.Location = new System.Drawing.Point(3, 17);
            this.reoGridControlWarehouse.Margin = new System.Windows.Forms.Padding(2);
            this.reoGridControlWarehouse.Name = "reoGridControlWarehouse";
            this.reoGridControlWarehouse.RowHeaderContextMenuStrip = null;
            this.reoGridControlWarehouse.Script = null;
            this.reoGridControlWarehouse.SheetTabContextMenuStrip = null;
            this.reoGridControlWarehouse.SheetTabNewButtonVisible = true;
            this.reoGridControlWarehouse.SheetTabVisible = true;
            this.reoGridControlWarehouse.SheetTabWidth = 60;
            this.reoGridControlWarehouse.ShowScrollEndSpacing = true;
            this.reoGridControlWarehouse.Size = new System.Drawing.Size(230, 418);
            this.reoGridControlWarehouse.TabIndex = 13;
            this.reoGridControlWarehouse.Text = "reoGridControl1";
            // 
            // groupBoxProject
            // 
            this.groupBoxProject.Controls.Add(this.reoGridControlProject);
            this.groupBoxProject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxProject.Location = new System.Drawing.Point(280, 29);
            this.groupBoxProject.Name = "groupBoxProject";
            this.groupBoxProject.Size = new System.Drawing.Size(236, 438);
            this.groupBoxProject.TabIndex = 13;
            this.groupBoxProject.TabStop = false;
            this.groupBoxProject.Text = "项目信息";
            // 
            // reoGridControlProject
            // 
            this.reoGridControlProject.BackColor = System.Drawing.Color.White;
            this.reoGridControlProject.ColumnHeaderContextMenuStrip = null;
            this.reoGridControlProject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reoGridControlProject.LeadHeaderContextMenuStrip = null;
            this.reoGridControlProject.Location = new System.Drawing.Point(3, 17);
            this.reoGridControlProject.Margin = new System.Windows.Forms.Padding(2);
            this.reoGridControlProject.Name = "reoGridControlProject";
            this.reoGridControlProject.RowHeaderContextMenuStrip = null;
            this.reoGridControlProject.Script = null;
            this.reoGridControlProject.SheetTabContextMenuStrip = null;
            this.reoGridControlProject.SheetTabNewButtonVisible = true;
            this.reoGridControlProject.SheetTabVisible = true;
            this.reoGridControlProject.SheetTabWidth = 60;
            this.reoGridControlProject.ShowScrollEndSpacing = true;
            this.reoGridControlProject.Size = new System.Drawing.Size(230, 418);
            this.reoGridControlProject.TabIndex = 14;
            this.reoGridControlProject.Text = "reoGridControl1";
            // 
            // groupBoxPackageUnit
            // 
            this.groupBoxPackageUnit.Controls.Add(this.reoGridControlPackageUnit);
            this.groupBoxPackageUnit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxPackageUnit.Location = new System.Drawing.Point(557, 29);
            this.groupBoxPackageUnit.Name = "groupBoxPackageUnit";
            this.groupBoxPackageUnit.Size = new System.Drawing.Size(243, 438);
            this.groupBoxPackageUnit.TabIndex = 14;
            this.groupBoxPackageUnit.TabStop = false;
            this.groupBoxPackageUnit.Text = "包装单位信息";
            // 
            // reoGridControlPackageUnit
            // 
            this.reoGridControlPackageUnit.BackColor = System.Drawing.Color.White;
            this.reoGridControlPackageUnit.ColumnHeaderContextMenuStrip = null;
            this.reoGridControlPackageUnit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reoGridControlPackageUnit.LeadHeaderContextMenuStrip = null;
            this.reoGridControlPackageUnit.Location = new System.Drawing.Point(3, 17);
            this.reoGridControlPackageUnit.Margin = new System.Windows.Forms.Padding(2);
            this.reoGridControlPackageUnit.Name = "reoGridControlPackageUnit";
            this.reoGridControlPackageUnit.RowHeaderContextMenuStrip = null;
            this.reoGridControlPackageUnit.Script = null;
            this.reoGridControlPackageUnit.SheetTabContextMenuStrip = null;
            this.reoGridControlPackageUnit.SheetTabNewButtonVisible = true;
            this.reoGridControlPackageUnit.SheetTabVisible = true;
            this.reoGridControlPackageUnit.SheetTabWidth = 60;
            this.reoGridControlPackageUnit.ShowScrollEndSpacing = true;
            this.reoGridControlPackageUnit.Size = new System.Drawing.Size(237, 418);
            this.reoGridControlPackageUnit.TabIndex = 15;
            this.reoGridControlPackageUnit.Text = "reoGridControl1";
            // 
            // panelPager2
            // 
            this.panelPager2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPager2.Location = new System.Drawing.Point(280, 473);
            this.panelPager2.Name = "panelPager2";
            this.panelPager2.Size = new System.Drawing.Size(236, 21);
            this.panelPager2.TabIndex = 15;
            // 
            // panelPager3
            // 
            this.panelPager3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPager3.Location = new System.Drawing.Point(557, 473);
            this.panelPager3.Name = "panelPager3";
            this.panelPager3.Size = new System.Drawing.Size(243, 21);
            this.panelPager3.TabIndex = 16;
            // 
            // toolStripLabelSelectWarehouse
            // 
            this.toolStripLabelSelectWarehouse.Name = "toolStripLabelSelectWarehouse";
            this.toolStripLabelSelectWarehouse.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripLabelSelectWarehouse.Size = new System.Drawing.Size(68, 23);
            this.toolStripLabelSelectWarehouse.Text = "查询条件：";
            // 
            // toolStripComboBoxSelectWarehouse
            // 
            this.toolStripComboBoxSelectWarehouse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBoxSelectWarehouse.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.toolStripComboBoxSelectWarehouse.Name = "toolStripComboBoxSelectWarehouse";
            this.toolStripComboBoxSelectWarehouse.Size = new System.Drawing.Size(114, 26);
            this.toolStripComboBoxSelectWarehouse.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxSelect_SelectedIndexChanged);
            // 
            // toolStripTextBoxSelectWarehouse
            // 
            this.toolStripTextBoxSelectWarehouse.Name = "toolStripTextBoxSelectWarehouse";
            this.toolStripTextBoxSelectWarehouse.Size = new System.Drawing.Size(151, 23);
            this.toolStripTextBoxSelectWarehouse.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.toolStripTextBoxSelect_KeyPress);
            // 
            // toolStripButtonSelectWarehouse
            // 
            this.toolStripButtonSelectWarehouse.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSelectWarehouse.Image")));
            this.toolStripButtonSelectWarehouse.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSelectWarehouse.Name = "toolStripButtonSelectWarehouse";
            this.toolStripButtonSelectWarehouse.Size = new System.Drawing.Size(56, 24);
            this.toolStripButtonSelectWarehouse.Text = "查询";
            this.toolStripButtonSelectWarehouse.Click += new System.EventHandler(this.toolStripButtonSelect_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.AutoSize = false;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(15, 28);
            // 
            // toolStripButtonWarehouseAdd
            // 
            this.toolStripButtonWarehouseAdd.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonWarehouseAdd.Image")));
            this.toolStripButtonWarehouseAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonWarehouseAdd.Name = "toolStripButtonWarehouseAdd";
            this.toolStripButtonWarehouseAdd.Size = new System.Drawing.Size(80, 24);
            this.toolStripButtonWarehouseAdd.Text = "仓库添加";
            this.toolStripButtonWarehouseAdd.Click += new System.EventHandler(this.toolStripButtonAdd_Click);
            // 
            // toolStripButtonAlterWarehouse
            // 
            this.toolStripButtonAlterWarehouse.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAlterWarehouse.Image")));
            this.toolStripButtonAlterWarehouse.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAlterWarehouse.Name = "toolStripButtonAlterWarehouse";
            this.toolStripButtonAlterWarehouse.Size = new System.Drawing.Size(56, 24);
            this.toolStripButtonAlterWarehouse.Text = "修改";
            this.toolStripButtonAlterWarehouse.Click += new System.EventHandler(this.toolStripButtonAlter_Click);
            // 
            // toolStripButtonDeleteWarehouse
            // 
            this.toolStripButtonDeleteWarehouse.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonDeleteWarehouse.Image")));
            this.toolStripButtonDeleteWarehouse.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDeleteWarehouse.Name = "toolStripButtonDeleteWarehouse";
            this.toolStripButtonDeleteWarehouse.Size = new System.Drawing.Size(56, 24);
            this.toolStripButtonDeleteWarehouse.Text = "删除";
            this.toolStripButtonDeleteWarehouse.Click += new System.EventHandler(this.toolStripButtonDelete_Click);
            // 
            // toolStripTop
            // 
            this.toolStripTop.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.toolStripTop.BackgroundImage = global::WMS.UI.Properties.Resources.bottonW_q;
            this.toolStripTop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStripTop.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripTop.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabelSelectWarehouse,
            this.toolStripComboBoxSelectWarehouse,
            this.toolStripTextBoxSelectWarehouse,
            this.toolStripButtonSelectWarehouse,
            this.toolStripSeparator1,
            this.toolStripButtonWarehouseAdd,
            this.toolStripButtonAlterWarehouse,
            this.toolStripButtonDeleteWarehouse});
            this.toolStripTop.Location = new System.Drawing.Point(0, 0);
            this.toolStripTop.Name = "toolStripTop";
            this.toolStripTop.Size = new System.Drawing.Size(242, 26);
            this.toolStripTop.TabIndex = 4;
            this.toolStripTop.Text = "toolStrip1";
            // 
            // toolStrip3
            // 
            this.toolStrip3.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.toolStrip3.BackgroundImage = global::WMS.UI.Properties.Resources.bottonW_q;
            this.toolStrip3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStrip3.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabelSelectProject,
            this.toolStripComboBoxSelectProject,
            this.toolStripTextBoxSelectProject,
            this.toolStripButtonSelectProject,
            this.toolStripSeparator2,
            this.toolStripButtonAddProject,
            this.toolStripButtonAlterProject,
            this.toolStripButtonDelectProject});
            this.toolStrip3.Location = new System.Drawing.Point(277, 0);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(242, 26);
            this.toolStrip3.TabIndex = 19;
            this.toolStrip3.Text = "toolStrip1";
            // 
            // toolStripLabelSelectProject
            // 
            this.toolStripLabelSelectProject.Name = "toolStripLabelSelectProject";
            this.toolStripLabelSelectProject.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripLabelSelectProject.Size = new System.Drawing.Size(68, 23);
            this.toolStripLabelSelectProject.Text = "查询条件：";
            // 
            // toolStripComboBoxSelectProject
            // 
            this.toolStripComboBoxSelectProject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBoxSelectProject.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.toolStripComboBoxSelectProject.Name = "toolStripComboBoxSelectProject";
            this.toolStripComboBoxSelectProject.Size = new System.Drawing.Size(114, 26);
            this.toolStripComboBoxSelectProject.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxSelectProject_SelectedIndexChanged);
            // 
            // toolStripTextBoxSelectProject
            // 
            this.toolStripTextBoxSelectProject.Name = "toolStripTextBoxSelectProject";
            this.toolStripTextBoxSelectProject.Size = new System.Drawing.Size(151, 23);
            this.toolStripTextBoxSelectProject.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.toolStripTextBoxSelectProject_KeyPress);
            // 
            // toolStripButtonSelectProject
            // 
            this.toolStripButtonSelectProject.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSelectProject.Image")));
            this.toolStripButtonSelectProject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSelectProject.Name = "toolStripButtonSelectProject";
            this.toolStripButtonSelectProject.Size = new System.Drawing.Size(56, 24);
            this.toolStripButtonSelectProject.Text = "查询";
            this.toolStripButtonSelectProject.Click += new System.EventHandler(this.toolStripButtonSelectProject_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.AutoSize = false;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(15, 28);
            // 
            // toolStripButtonAddProject
            // 
            this.toolStripButtonAddProject.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAddProject.Image")));
            this.toolStripButtonAddProject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAddProject.Name = "toolStripButtonAddProject";
            this.toolStripButtonAddProject.Size = new System.Drawing.Size(80, 24);
            this.toolStripButtonAddProject.Text = "项目添加";
            this.toolStripButtonAddProject.Click += new System.EventHandler(this.toolStripButtonAddProject_Click);
            // 
            // toolStripButtonAlterProject
            // 
            this.toolStripButtonAlterProject.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAlterProject.Image")));
            this.toolStripButtonAlterProject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAlterProject.Name = "toolStripButtonAlterProject";
            this.toolStripButtonAlterProject.Size = new System.Drawing.Size(56, 24);
            this.toolStripButtonAlterProject.Text = "修改";
            this.toolStripButtonAlterProject.Click += new System.EventHandler(this.toolStripButtonAlterProject_Click);
            // 
            // toolStripButtonDelectProject
            // 
            this.toolStripButtonDelectProject.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonDelectProject.Image")));
            this.toolStripButtonDelectProject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDelectProject.Name = "toolStripButtonDelectProject";
            this.toolStripButtonDelectProject.Size = new System.Drawing.Size(56, 24);
            this.toolStripButtonDelectProject.Text = "删除";
            this.toolStripButtonDelectProject.Click += new System.EventHandler(this.toolStripButtonDelectProject_Click);
            // 
            // toolStrip2
            // 
            this.toolStrip2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.toolStrip2.BackgroundImage = global::WMS.UI.Properties.Resources.bottonW_q;
            this.toolStrip2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabelSelectPackageUnit,
            this.toolStripComboBoxSelectPackageUnit,
            this.toolStripTextBoxSelectPackageUnit,
            this.toolStripButtonSelectPackageUnit,
            this.toolStripSeparator3,
            this.toolStripButtonAddPackageUnit,
            this.toolStripButtonAlterPackageUnit,
            this.toolStripButtonDelectPackageUnit});
            this.toolStrip2.Location = new System.Drawing.Point(554, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(249, 26);
            this.toolStrip2.TabIndex = 20;
            this.toolStrip2.Text = "toolStrip1";
            // 
            // toolStripLabelSelectPackageUnit
            // 
            this.toolStripLabelSelectPackageUnit.Name = "toolStripLabelSelectPackageUnit";
            this.toolStripLabelSelectPackageUnit.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripLabelSelectPackageUnit.Size = new System.Drawing.Size(68, 23);
            this.toolStripLabelSelectPackageUnit.Text = "查询条件：";
            // 
            // toolStripComboBoxSelectPackageUnit
            // 
            this.toolStripComboBoxSelectPackageUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBoxSelectPackageUnit.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.toolStripComboBoxSelectPackageUnit.Name = "toolStripComboBoxSelectPackageUnit";
            this.toolStripComboBoxSelectPackageUnit.Size = new System.Drawing.Size(114, 26);
            this.toolStripComboBoxSelectPackageUnit.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxSelectPackageUnit_SelectedIndexChanged);
            // 
            // toolStripTextBoxSelectPackageUnit
            // 
            this.toolStripTextBoxSelectPackageUnit.Name = "toolStripTextBoxSelectPackageUnit";
            this.toolStripTextBoxSelectPackageUnit.Size = new System.Drawing.Size(151, 23);
            this.toolStripTextBoxSelectPackageUnit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.toolStripTextBoxSelectPackageUnit_KeyPress);
            // 
            // toolStripButtonSelectPackageUnit
            // 
            this.toolStripButtonSelectPackageUnit.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSelectPackageUnit.Image")));
            this.toolStripButtonSelectPackageUnit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSelectPackageUnit.Name = "toolStripButtonSelectPackageUnit";
            this.toolStripButtonSelectPackageUnit.Size = new System.Drawing.Size(56, 24);
            this.toolStripButtonSelectPackageUnit.Text = "查询";
            this.toolStripButtonSelectPackageUnit.Click += new System.EventHandler(this.toolStripButtonSelectPackageUnit_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.AutoSize = false;
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(15, 28);
            // 
            // toolStripButtonAddPackageUnit
            // 
            this.toolStripButtonAddPackageUnit.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAddPackageUnit.Image")));
            this.toolStripButtonAddPackageUnit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAddPackageUnit.Name = "toolStripButtonAddPackageUnit";
            this.toolStripButtonAddPackageUnit.Size = new System.Drawing.Size(56, 24);
            this.toolStripButtonAddPackageUnit.Text = "添加";
            this.toolStripButtonAddPackageUnit.Click += new System.EventHandler(this.toolStripButtonAddPackageUnit_Click);
            // 
            // toolStripButtonAlterPackageUnit
            // 
            this.toolStripButtonAlterPackageUnit.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAlterPackageUnit.Image")));
            this.toolStripButtonAlterPackageUnit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAlterPackageUnit.Name = "toolStripButtonAlterPackageUnit";
            this.toolStripButtonAlterPackageUnit.Size = new System.Drawing.Size(56, 24);
            this.toolStripButtonAlterPackageUnit.Text = "修改";
            this.toolStripButtonAlterPackageUnit.Click += new System.EventHandler(this.toolStripButtonAlterPackageUnit_Click);
            // 
            // toolStripButtonDelectPackageUnit
            // 
            this.toolStripButtonDelectPackageUnit.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonDelectPackageUnit.Image")));
            this.toolStripButtonDelectPackageUnit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDelectPackageUnit.Name = "toolStripButtonDelectPackageUnit";
            this.toolStripButtonDelectPackageUnit.Size = new System.Drawing.Size(56, 24);
            this.toolStripButtonDelectPackageUnit.Text = "删除";
            this.toolStripButtonDelectPackageUnit.Click += new System.EventHandler(this.toolStripButtonDelectPackageUnit_Click);
            // 
            // FormBaseWarehouse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(839, 522);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.toolStrip1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormBaseWarehouse";
            this.Text = "仓库信息";
            this.Load += new System.EventHandler(this.base_Warehouse_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBoxWarehouse.ResumeLayout(false);
            this.groupBoxProject.ResumeLayout(false);
            this.groupBoxPackageUnit.ResumeLayout(false);
            this.toolStripTop.ResumeLayout(false);
            this.toolStripTop.PerformLayout();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripLabel labelStatus;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panelPager1;
        private System.Windows.Forms.GroupBox groupBoxWarehouse;
        private unvell.ReoGrid.ReoGridControl reoGridControlWarehouse;
        private System.Windows.Forms.GroupBox groupBoxProject;
        private System.Windows.Forms.GroupBox groupBoxPackageUnit;
        private System.Windows.Forms.Panel panelPager2;
        private System.Windows.Forms.Panel panelPager3;
        private unvell.ReoGrid.ReoGridControl reoGridControlProject;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private unvell.ReoGrid.ReoGridControl reoGridControlPackageUnit;
        private System.Windows.Forms.ToolStrip toolStripTop;
        private System.Windows.Forms.ToolStripLabel toolStripLabelSelectWarehouse;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxSelectWarehouse;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxSelectWarehouse;
        private System.Windows.Forms.ToolStripButton toolStripButtonSelectWarehouse;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonWarehouseAdd;
        private System.Windows.Forms.ToolStripButton toolStripButtonAlterWarehouse;
        private System.Windows.Forms.ToolStripButton toolStripButtonDeleteWarehouse;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripLabel toolStripLabelSelectProject;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxSelectProject;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxSelectProject;
        private System.Windows.Forms.ToolStripButton toolStripButtonSelectProject;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButtonAddProject;
        private System.Windows.Forms.ToolStripButton toolStripButtonAlterProject;
        private System.Windows.Forms.ToolStripButton toolStripButtonDelectProject;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripLabel toolStripLabelSelectPackageUnit;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxSelectPackageUnit;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxSelectPackageUnit;
        private System.Windows.Forms.ToolStripButton toolStripButtonSelectPackageUnit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripButtonAddPackageUnit;
        private System.Windows.Forms.ToolStripButton toolStripButtonAlterPackageUnit;
        private System.Windows.Forms.ToolStripButton toolStripButtonDelectPackageUnit;
    }
}