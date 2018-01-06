namespace WMS.UI
{
    partial class SupplierStorageInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SupplierStorageInfo));
            this.toolStripLabelSelect = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBoxSelect = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripTextBoxSelect = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButtonSelect = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonAdd = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonAlter = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripTop = new System.Windows.Forms.ToolStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.labelStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelPager = new System.Windows.Forms.Panel();
            this.reoGridControlUser = new unvell.ReoGrid.ReoGridControl();
            this.toolStripTop.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripLabelSelect
            // 
            this.toolStripLabelSelect.Name = "toolStripLabelSelect";
            this.toolStripLabelSelect.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripLabelSelect.Size = new System.Drawing.Size(84, 25);
            this.toolStripLabelSelect.Text = "查询条件：";
            // 
            // toolStripComboBoxSelect
            // 
            this.toolStripComboBoxSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBoxSelect.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.toolStripComboBoxSelect.Name = "toolStripComboBoxSelect";
            this.toolStripComboBoxSelect.Size = new System.Drawing.Size(151, 28);
            this.toolStripComboBoxSelect.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxSelect_SelectedIndexChanged);
            // 
            // toolStripTextBoxSelect
            // 
            this.toolStripTextBoxSelect.Name = "toolStripTextBoxSelect";
            this.toolStripTextBoxSelect.Size = new System.Drawing.Size(200, 28);
            // 
            // toolStripButtonSelect
            // 
            this.toolStripButtonSelect.AutoSize = false;
            this.toolStripButtonSelect.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSelect.Image")));
            this.toolStripButtonSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSelect.Name = "toolStripButtonSelect";
            this.toolStripButtonSelect.Size = new System.Drawing.Size(60, 25);
            this.toolStripButtonSelect.Text = "查询";
            this.toolStripButtonSelect.Click += new System.EventHandler(this.toolStripButtonSelect_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.AutoSize = false;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(15, 28);
            // 
            // toolStripButtonAdd
            // 
            this.toolStripButtonAdd.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAdd.Image")));
            this.toolStripButtonAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAdd.Name = "toolStripButtonAdd";
            this.toolStripButtonAdd.Size = new System.Drawing.Size(63, 25);
            this.toolStripButtonAdd.Text = "添加";
            this.toolStripButtonAdd.Click += new System.EventHandler(this.toolStripButtonAdd_Click);
            // 
            // toolStripButtonAlter
            // 
            this.toolStripButtonAlter.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAlter.Image")));
            this.toolStripButtonAlter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAlter.Name = "toolStripButtonAlter";
            this.toolStripButtonAlter.Size = new System.Drawing.Size(63, 25);
            this.toolStripButtonAlter.Text = "修改";
            this.toolStripButtonAlter.Click += new System.EventHandler(this.toolStripButtonAlter_Click);
            // 
            // toolStripButtonDelete
            // 
            this.toolStripButtonDelete.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonDelete.Image")));
            this.toolStripButtonDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDelete.Name = "toolStripButtonDelete";
            this.toolStripButtonDelete.Size = new System.Drawing.Size(63, 25);
            this.toolStripButtonDelete.Text = "删除";
            this.toolStripButtonDelete.Click += new System.EventHandler(this.toolStripButtonDelete_Click);
            // 
            // toolStripTop
            // 
            this.toolStripTop.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.toolStripTop.BackgroundImage = global::WMS.UI.Properties.Resources.bottonW_q;
            this.toolStripTop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStripTop.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripTop.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabelSelect,
            this.toolStripComboBoxSelect,
            this.toolStripTextBoxSelect,
            this.toolStripButtonSelect,
            this.toolStripSeparator1,
            this.toolStripButtonAdd,
            this.toolStripButtonAlter,
            this.toolStripButtonDelete});
            this.toolStripTop.Location = new System.Drawing.Point(0, 0);
            this.toolStripTop.Name = "toolStripTop";
            this.toolStripTop.Size = new System.Drawing.Size(1359, 28);
            this.toolStripTop.TabIndex = 6;
            this.toolStripTop.Text = "toolStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(54, 20);
            this.toolStripStatusLabel1.Text = "状态：";
            // 
            // labelStatus
            // 
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(99, 20);
            this.labelStatus.Text = "供应商年信息";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.labelStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 427);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 13, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1359, 25);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panelPager, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.reoGridControlUser, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 28);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1359, 399);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // panelPager
            // 
            this.panelPager.BackColor = System.Drawing.SystemColors.Control;
            this.panelPager.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPager.Location = new System.Drawing.Point(0, 348);
            this.panelPager.Margin = new System.Windows.Forms.Padding(0);
            this.panelPager.Name = "panelPager";
            this.panelPager.Size = new System.Drawing.Size(1359, 51);
            this.panelPager.TabIndex = 9;
            // 
            // reoGridControlUser
            // 
            this.reoGridControlUser.BackColor = System.Drawing.Color.White;
            this.reoGridControlUser.ColumnHeaderContextMenuStrip = null;
            this.reoGridControlUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reoGridControlUser.LeadHeaderContextMenuStrip = null;
            this.reoGridControlUser.Location = new System.Drawing.Point(3, 2);
            this.reoGridControlUser.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.reoGridControlUser.Name = "reoGridControlUser";
            this.reoGridControlUser.RowHeaderContextMenuStrip = null;
            this.reoGridControlUser.Script = null;
            this.reoGridControlUser.SheetTabContextMenuStrip = null;
            this.reoGridControlUser.SheetTabNewButtonVisible = true;
            this.reoGridControlUser.SheetTabVisible = true;
            this.reoGridControlUser.SheetTabWidth = 80;
            this.reoGridControlUser.ShowScrollEndSpacing = true;
            this.reoGridControlUser.Size = new System.Drawing.Size(1353, 344);
            this.reoGridControlUser.TabIndex = 8;
            this.reoGridControlUser.Text = "reoGridControl1";
            // 
            // SupplierStorageInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1359, 452);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStripTop);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "SupplierStorageInfo";
            this.Text = "供应商年信息";
            this.Load += new System.EventHandler(this.FormSupplierAnnualInfo_Load);
            this.toolStripTop.ResumeLayout(false);
            this.toolStripTop.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStripLabel toolStripLabelSelect;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxSelect;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxSelect;
        private System.Windows.Forms.ToolStripButton toolStripButtonSelect;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonAdd;
        private System.Windows.Forms.ToolStripButton toolStripButtonAlter;
        private System.Windows.Forms.ToolStripButton toolStripButtonDelete;
        private System.Windows.Forms.ToolStrip toolStripTop;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel labelStatus;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private unvell.ReoGrid.ReoGridControl reoGridControlUser;
        private System.Windows.Forms.Panel panelPager;
    }
}