namespace WMS.UI
{
    partial class FormSubmissionManage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSubmissionManage));
            this.reoGridControlUser = new unvell.ReoGrid.ReoGridControl();
            this.reoGridControl1 = new unvell.ReoGrid.ReoGridControl();
            this.toolStripLabelSelect = new System.Windows.Forms.ToolStripLabel();
            this.comboBoxSelect = new System.Windows.Forms.ToolStripComboBox();
            this.textBoxSelect = new System.Windows.Forms.ToolStripTextBox();
            this.buttonSelect = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonPass = new System.Windows.Forms.ToolStripButton();
            this.buttonNoPass = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonItem = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripTop = new System.Windows.Forms.ToolStrip();
            this.buttonItems = new System.Windows.Forms.ToolStripButton();
            this.buttonReceipt = new System.Windows.Forms.ToolStripButton();
            this.buttonReceiptCancel = new System.Windows.Forms.ToolStripButton();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip2 = new System.Windows.Forms.StatusStrip();
            this.toolStripTop.SuspendLayout();
            this.statusStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // reoGridControlUser
            // 
            this.reoGridControlUser.BackColor = System.Drawing.Color.White;
            this.reoGridControlUser.ColumnHeaderContextMenuStrip = null;
            this.reoGridControlUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reoGridControlUser.LeadHeaderContextMenuStrip = null;
            this.reoGridControlUser.Location = new System.Drawing.Point(0, 0);
            this.reoGridControlUser.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.reoGridControlUser.Name = "reoGridControlUser";
            this.reoGridControlUser.Readonly = true;
            this.reoGridControlUser.RowHeaderContextMenuStrip = null;
            this.reoGridControlUser.Script = null;
            this.reoGridControlUser.SheetTabContextMenuStrip = null;
            this.reoGridControlUser.SheetTabNewButtonVisible = true;
            this.reoGridControlUser.SheetTabVisible = true;
            this.reoGridControlUser.SheetTabWidth = 90;
            this.reoGridControlUser.ShowScrollEndSpacing = true;
            this.reoGridControlUser.Size = new System.Drawing.Size(1602, 725);
            this.reoGridControlUser.TabIndex = 6;
            this.reoGridControlUser.Text = "reoGridControl1";
            // 
            // reoGridControl1
            // 
            this.reoGridControl1.BackColor = System.Drawing.Color.White;
            this.reoGridControl1.ColumnHeaderContextMenuStrip = null;
            this.reoGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reoGridControl1.LeadHeaderContextMenuStrip = null;
            this.reoGridControl1.Location = new System.Drawing.Point(0, 39);
            this.reoGridControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.reoGridControl1.Name = "reoGridControl1";
            this.reoGridControl1.Readonly = true;
            this.reoGridControl1.RowHeaderContextMenuStrip = null;
            this.reoGridControl1.Script = null;
            this.reoGridControl1.SheetTabContextMenuStrip = null;
            this.reoGridControl1.SheetTabNewButtonVisible = true;
            this.reoGridControl1.SheetTabVisible = true;
            this.reoGridControl1.SheetTabWidth = 90;
            this.reoGridControl1.ShowScrollEndSpacing = true;
            this.reoGridControl1.Size = new System.Drawing.Size(1602, 686);
            this.reoGridControl1.TabIndex = 9;
            this.reoGridControl1.Text = "reoGridControl1";
            // 
            // toolStripLabelSelect
            // 
            this.toolStripLabelSelect.Name = "toolStripLabelSelect";
            this.toolStripLabelSelect.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripLabelSelect.Size = new System.Drawing.Size(134, 36);
            this.toolStripLabelSelect.Text = "查询条件：";
            // 
            // comboBoxSelect
            // 
            this.comboBoxSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSelect.Name = "comboBoxSelect";
            this.comboBoxSelect.Size = new System.Drawing.Size(223, 39);
            // 
            // textBoxSelect
            // 
            this.textBoxSelect.Name = "textBoxSelect";
            this.textBoxSelect.Size = new System.Drawing.Size(298, 39);
            // 
            // buttonSelect
            // 
            this.buttonSelect.AutoSize = false;
            this.buttonSelect.Image = ((System.Drawing.Image)(resources.GetObject("buttonSelect.Image")));
            this.buttonSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonSelect.Name = "buttonSelect";
            this.buttonSelect.Size = new System.Drawing.Size(60, 25);
            this.buttonSelect.Text = "查询";
            this.buttonSelect.Click += new System.EventHandler(this.buttonSelect_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.AutoSize = false;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(15, 28);
            // 
            // buttonPass
            // 
            this.buttonPass.AutoSize = false;
            this.buttonPass.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonPass.Name = "buttonPass";
            this.buttonPass.Size = new System.Drawing.Size(60, 25);
            this.buttonPass.Text = "合格";
            this.buttonPass.Click += new System.EventHandler(this.buttonPass_Click);
            // 
            // buttonNoPass
            // 
            this.buttonNoPass.AutoSize = false;
            this.buttonNoPass.Image = ((System.Drawing.Image)(resources.GetObject("buttonNoPass.Image")));
            this.buttonNoPass.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonNoPass.Name = "buttonNoPass";
            this.buttonNoPass.Size = new System.Drawing.Size(60, 25);
            this.buttonNoPass.Text = "不合格";
            this.buttonNoPass.Click += new System.EventHandler(this.buttonNoPass_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.AutoSize = false;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(15, 28);
            // 
            // buttonItem
            // 
            this.buttonItem.AutoSize = false;
            this.buttonItem.BackColor = System.Drawing.SystemColors.Control;
            this.buttonItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.buttonItem.Image = ((System.Drawing.Image)(resources.GetObject("buttonItem.Image")));
            this.buttonItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonItem.Name = "buttonItem";
            this.buttonItem.Size = new System.Drawing.Size(80, 25);
            this.buttonItem.Text = "查看详细";
            this.buttonItem.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.buttonItem.Click += new System.EventHandler(this.buttonItem_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(24, 36);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // toolStripTop
            // 
            this.toolStripTop.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripTop.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabelSelect,
            this.comboBoxSelect,
            this.textBoxSelect,
            this.buttonSelect,
            this.toolStripSeparator1,
            this.buttonPass,
            this.buttonNoPass,
            this.toolStripSeparator2,
            this.buttonItem,
            this.buttonItems,
            this.buttonReceipt,
            this.buttonReceiptCancel,
            this.toolStripButton1});
            this.toolStripTop.Location = new System.Drawing.Point(0, 0);
            this.toolStripTop.Name = "toolStripTop";
            this.toolStripTop.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStripTop.Size = new System.Drawing.Size(1602, 39);
            this.toolStripTop.TabIndex = 8;
            this.toolStripTop.Text = "toolStrip1";
            // 
            // buttonItems
            // 
            this.buttonItems.AutoSize = false;
            this.buttonItems.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.buttonItems.Image = ((System.Drawing.Image)(resources.GetObject("buttonItems.Image")));
            this.buttonItems.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonItems.Name = "buttonItems";
            this.buttonItems.Size = new System.Drawing.Size(80, 25);
            this.buttonItems.Text = "修改送检单";
            this.buttonItems.ToolTipText = "查看零件条目";
            this.buttonItems.Click += new System.EventHandler(this.buttonItems_Click);
            // 
            // buttonReceipt
            // 
            this.buttonReceipt.AutoSize = false;
            this.buttonReceipt.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.buttonReceipt.Image = ((System.Drawing.Image)(resources.GetObject("buttonReceipt.Image")));
            this.buttonReceipt.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonReceipt.Name = "buttonReceipt";
            this.buttonReceipt.Size = new System.Drawing.Size(40, 25);
            this.buttonReceipt.Text = "收货";
            // 
            // buttonReceiptCancel
            // 
            this.buttonReceiptCancel.AutoSize = false;
            this.buttonReceiptCancel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.buttonReceiptCancel.Image = ((System.Drawing.Image)(resources.GetObject("buttonReceiptCancel.Image")));
            this.buttonReceiptCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonReceiptCancel.Name = "buttonReceiptCancel";
            this.buttonReceiptCancel.Size = new System.Drawing.Size(70, 25);
            this.buttonReceiptCancel.Text = "取消收货";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(86, 31);
            this.toolStripStatusLabel3.Text = "状态：";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(110, 31);
            this.toolStripStatusLabel2.Text = "到货管理";
            // 
            // statusStrip2
            // 
            this.statusStrip2.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.statusStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel3,
            this.toolStripStatusLabel2});
            this.statusStrip2.Location = new System.Drawing.Point(0, 689);
            this.statusStrip2.Name = "statusStrip2";
            this.statusStrip2.Size = new System.Drawing.Size(1602, 36);
            this.statusStrip2.TabIndex = 10;
            this.statusStrip2.Text = "statusStrip2";
            // 
            // FormSubmissionManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1602, 725);
            this.Controls.Add(this.statusStrip2);
            this.Controls.Add(this.reoGridControl1);
            this.Controls.Add(this.toolStripTop);
            this.Controls.Add(this.reoGridControlUser);
            this.Name = "FormSubmissionManage";
            this.Text = "FormSubmissionManage";
            this.Load += new System.EventHandler(this.FormSubmissionManage_Load);
            this.toolStripTop.ResumeLayout(false);
            this.toolStripTop.PerformLayout();
            this.statusStrip2.ResumeLayout(false);
            this.statusStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private unvell.ReoGrid.ReoGridControl reoGridControlUser;
        private unvell.ReoGrid.ReoGridControl reoGridControl1;
        private System.Windows.Forms.ToolStripLabel toolStripLabelSelect;
        private System.Windows.Forms.ToolStripComboBox comboBoxSelect;
        private System.Windows.Forms.ToolStripTextBox textBoxSelect;
        private System.Windows.Forms.ToolStripButton buttonSelect;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton buttonPass;
        private System.Windows.Forms.ToolStripButton buttonNoPass;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton buttonItem;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStrip toolStripTop;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.StatusStrip statusStrip2;
        private System.Windows.Forms.ToolStripButton buttonItems;
        private System.Windows.Forms.ToolStripButton buttonReceipt;
        private System.Windows.Forms.ToolStripButton buttonReceiptCancel;
    }
}