namespace WMS.UI
{
    partial class FormStockInfoCheckTicketComponentModify
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormStockInfoCheckTicketComponentModify));
            this.toolStripTop = new System.Windows.Forms.ToolStrip();
            this.toolStripLabelSelect = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBoxSelect1 = new System.Windows.Forms.ToolStripComboBox();
            this.textBoxSearchValue = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonSearch = new System.Windows.Forms.ToolStripButton();
            this.buttonAdd = new System.Windows.Forms.ToolStripButton();
            this.buttonCancel = new System.Windows.Forms.ToolStripButton();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.labelStatus = new System.Windows.Forms.ToolStripLabel();
            this.reoGridControlComponen = new unvell.ReoGrid.ReoGridControl();
            this.toolStripTop.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripTop
            // 
            this.toolStripTop.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripTop.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabelSelect,
            this.toolStripComboBoxSelect1,
            this.textBoxSearchValue,
            this.toolStripSeparator1,
            this.buttonSearch,
            this.buttonAdd,
            this.buttonCancel});
            this.toolStripTop.Location = new System.Drawing.Point(0, 0);
            this.toolStripTop.Name = "toolStripTop";
            this.toolStripTop.Size = new System.Drawing.Size(840, 28);
            this.toolStripTop.TabIndex = 5;
            this.toolStripTop.Text = "toolStrip1";
            // 
            // toolStripLabelSelect
            // 
            this.toolStripLabelSelect.Name = "toolStripLabelSelect";
            this.toolStripLabelSelect.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripLabelSelect.Size = new System.Drawing.Size(68, 25);
            this.toolStripLabelSelect.Text = "查询条件：";
            // 
            // toolStripComboBoxSelect1
            // 
            this.toolStripComboBoxSelect1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBoxSelect1.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.toolStripComboBoxSelect1.Name = "toolStripComboBoxSelect1";
            this.toolStripComboBoxSelect1.Size = new System.Drawing.Size(114, 28);
            // 
            // textBoxSearchValue
            // 
            this.textBoxSearchValue.Name = "textBoxSearchValue";
            this.textBoxSearchValue.Size = new System.Drawing.Size(151, 28);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.AutoSize = false;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(15, 28);
            // 
            // buttonSearch
            // 
            this.buttonSearch.AutoSize = false;
            this.buttonSearch.Image = ((System.Drawing.Image)(resources.GetObject("buttonSearch.Image")));
            this.buttonSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(60, 25);
            this.buttonSearch.Text = "查询";
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.AutoSize = false;
            this.buttonAdd.Image = ((System.Drawing.Image)(resources.GetObject("buttonAdd.Image")));
            this.buttonAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(60, 25);
            this.buttonAdd.Text = "添加";
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.AutoSize = false;
            this.buttonCancel.Image = ((System.Drawing.Image)(resources.GetObject("buttonCancel.Image")));
            this.buttonCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(60, 25);
            this.buttonCancel.Text = "取消";
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.labelStatus});
            this.toolStrip1.Location = new System.Drawing.Point(0, 485);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(840, 25);
            this.toolStrip1.TabIndex = 7;
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
            this.labelStatus.Size = new System.Drawing.Size(104, 22);
            this.labelStatus.Text = "库存盘点零件信息";
            this.labelStatus.Click += new System.EventHandler(this.labelStatus_Click);
            // 
            // reoGridControlComponen
            // 
            this.reoGridControlComponen.BackColor = System.Drawing.Color.White;
            this.reoGridControlComponen.ColumnHeaderContextMenuStrip = null;
            this.reoGridControlComponen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reoGridControlComponen.LeadHeaderContextMenuStrip = null;
            this.reoGridControlComponen.Location = new System.Drawing.Point(0, 28);
            this.reoGridControlComponen.Margin = new System.Windows.Forms.Padding(2);
            this.reoGridControlComponen.Name = "reoGridControlComponen";
            this.reoGridControlComponen.RowHeaderContextMenuStrip = null;
            this.reoGridControlComponen.Script = null;
            this.reoGridControlComponen.SheetTabContextMenuStrip = null;
            this.reoGridControlComponen.SheetTabNewButtonVisible = true;
            this.reoGridControlComponen.SheetTabVisible = true;
            this.reoGridControlComponen.SheetTabWidth = 60;
            this.reoGridControlComponen.ShowScrollEndSpacing = true;
            this.reoGridControlComponen.Size = new System.Drawing.Size(840, 482);
            this.reoGridControlComponen.TabIndex = 6;
            this.reoGridControlComponen.Text = "reoGridControl1";
            // 
            // FormStockInfoCheckTicketComponenModify
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 510);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.reoGridControlComponen);
            this.Controls.Add(this.toolStripTop);
            this.Name = "FormStockInfoCheckTicketComponenModify";
            this.Text = "FormStockCheckComponenModify";
            this.Load += new System.EventHandler(this.FormStockCheckComponenModify_Load);
            this.toolStripTop.ResumeLayout(false);
            this.toolStripTop.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripTop;
        private System.Windows.Forms.ToolStripLabel toolStripLabelSelect;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxSelect1;
        private System.Windows.Forms.ToolStripTextBox textBoxSearchValue;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripLabel labelStatus;
        private unvell.ReoGrid.ReoGridControl reoGridControlComponen;
        private System.Windows.Forms.ToolStripButton buttonSearch;
        private System.Windows.Forms.ToolStripButton buttonAdd;
        private System.Windows.Forms.ToolStripButton buttonCancel;
    }
}