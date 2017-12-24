namespace WMS.UI.FormReceipt
{
    partial class FormReceiptItem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormReceiptItem));
            this.lableStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripTop = new System.Windows.Forms.ToolStrip();
            this.reoGridControlUser = new unvell.ReoGrid.ReoGridControl();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonNoReceipt = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.ButtonReceipt = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1.SuspendLayout();
            this.toolStripTop.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lableStatus
            // 
            this.lableStatus.Name = "lableStatus";
            this.lableStatus.Size = new System.Drawing.Size(110, 35);
            this.lableStatus.Text = "到货管理";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.lableStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 855);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1254, 40);
            this.statusStrip1.TabIndex = 10;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(86, 35);
            this.toolStripStatusLabel1.Text = "状态：";
            // 
            // toolStripTop
            // 
            this.toolStripTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripTop.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripTop.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ButtonReceipt,
            this.buttonNoReceipt,
            this.toolStripButton1});
            this.toolStripTop.Location = new System.Drawing.Point(0, 0);
            this.toolStripTop.Name = "toolStripTop";
            this.toolStripTop.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStripTop.Size = new System.Drawing.Size(1254, 40);
            this.toolStripTop.TabIndex = 9;
            this.toolStripTop.Text = "toolStrip1";
            // 
            // reoGridControlUser
            // 
            this.reoGridControlUser.BackColor = System.Drawing.Color.White;
            this.reoGridControlUser.ColumnHeaderContextMenuStrip = null;
            this.reoGridControlUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reoGridControlUser.LeadHeaderContextMenuStrip = null;
            this.reoGridControlUser.Location = new System.Drawing.Point(4, 45);
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
            this.reoGridControlUser.Size = new System.Drawing.Size(1246, 805);
            this.reoGridControlUser.TabIndex = 11;
            this.reoGridControlUser.Text = "reoGridControl1";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.toolStripTop, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.statusStrip1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.reoGridControlUser, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1254, 895);
            this.tableLayoutPanel1.TabIndex = 13;
            // 
            // buttonNoReceipt
            // 
            this.buttonNoReceipt.AutoSize = false;
            this.buttonNoReceipt.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.buttonNoReceipt.Image = ((System.Drawing.Image)(resources.GetObject("buttonNoReceipt.Image")));
            this.buttonNoReceipt.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonNoReceipt.Name = "buttonNoReceipt";
            this.buttonNoReceipt.Size = new System.Drawing.Size(40, 25);
            this.buttonNoReceipt.Text = "拒收";
            this.buttonNoReceipt.Click += new System.EventHandler(this.buttonNoReceipt_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(24, 37);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // ButtonReceipt
            // 
            this.ButtonReceipt.AutoSize = false;
            this.ButtonReceipt.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ButtonReceipt.Image = ((System.Drawing.Image)(resources.GetObject("ButtonReceipt.Image")));
            this.ButtonReceipt.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonReceipt.Name = "ButtonReceipt";
            this.ButtonReceipt.Size = new System.Drawing.Size(40, 25);
            this.ButtonReceipt.Text = "收货";
            this.ButtonReceipt.Click += new System.EventHandler(this.ButtonReceipt_Click);
            // 
            // FormReceiptItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1254, 895);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FormReceiptItem";
            this.Text = "FormReceiptItem";
            this.Load += new System.EventHandler(this.FormReceiptItem_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStripTop.ResumeLayout(false);
            this.toolStripTop.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripStatusLabel lableStatus;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton buttonNoReceipt;
        private System.Windows.Forms.ToolStrip toolStripTop;
        private unvell.ReoGrid.ReoGridControl reoGridControlUser;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStripButton ButtonReceipt;
    }
}