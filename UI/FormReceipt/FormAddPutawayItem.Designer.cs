namespace WMS.UI.FormReceipt
{
    partial class FormAddPutawayItem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAddPutawayItem));
            this.lableStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.buttonReceiptCancel = new System.Windows.Forms.ToolStripButton();
            this.buttonPutaway = new System.Windows.Forms.ToolStripButton();
            this.buttonDelete = new System.Windows.Forms.ToolStripButton();
            this.buttonAdd = new System.Windows.Forms.ToolStripButton();
            this.toolStripTop = new System.Windows.Forms.ToolStrip();
            this.reoGridControlUser = new unvell.ReoGrid.ReoGridControl();
            this.statusStrip1.SuspendLayout();
            this.toolStripTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // lableStatus
            // 
            this.lableStatus.Name = "lableStatus";
            this.lableStatus.Size = new System.Drawing.Size(110, 31);
            this.lableStatus.Text = "到货管理";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.lableStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 913);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1428, 36);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(86, 31);
            this.toolStripStatusLabel1.Text = "状态：";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(24, 35);
            this.toolStripButton1.Text = "toolStripButton1";
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
            this.buttonReceiptCancel.Click += new System.EventHandler(this.buttonReceiptCancel_Click);
            // 
            // buttonPutaway
            // 
            this.buttonPutaway.AutoSize = false;
            this.buttonPutaway.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.buttonPutaway.Image = ((System.Drawing.Image)(resources.GetObject("buttonPutaway.Image")));
            this.buttonPutaway.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonPutaway.Name = "buttonPutaway";
            this.buttonPutaway.Size = new System.Drawing.Size(40, 25);
            this.buttonPutaway.Text = "收货";
            this.buttonPutaway.Click += new System.EventHandler(this.buttonPutaway_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.AutoSize = false;
            this.buttonDelete.Image = ((System.Drawing.Image)(resources.GetObject("buttonDelete.Image")));
            this.buttonDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(60, 25);
            this.buttonDelete.Text = "删除";
            // 
            // buttonAdd
            // 
            this.buttonAdd.AutoSize = false;
            this.buttonAdd.Image = ((System.Drawing.Image)(resources.GetObject("buttonAdd.Image")));
            this.buttonAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(60, 25);
            this.buttonAdd.Text = "添加";
            // 
            // toolStripTop
            // 
            this.toolStripTop.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripTop.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonAdd,
            this.buttonDelete,
            this.buttonPutaway,
            this.buttonReceiptCancel,
            this.toolStripButton1});
            this.toolStripTop.Location = new System.Drawing.Point(0, 0);
            this.toolStripTop.Name = "toolStripTop";
            this.toolStripTop.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStripTop.Size = new System.Drawing.Size(1428, 38);
            this.toolStripTop.TabIndex = 5;
            this.toolStripTop.Text = "toolStrip1";
            // 
            // reoGridControlUser
            // 
            this.reoGridControlUser.BackColor = System.Drawing.Color.White;
            this.reoGridControlUser.ColumnHeaderContextMenuStrip = null;
            this.reoGridControlUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reoGridControlUser.LeadHeaderContextMenuStrip = null;
            this.reoGridControlUser.Location = new System.Drawing.Point(0, 38);
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
            this.reoGridControlUser.Size = new System.Drawing.Size(1428, 875);
            this.reoGridControlUser.TabIndex = 8;
            this.reoGridControlUser.Text = "reoGridControl1";
            // 
            // FormAddPutawayItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1428, 949);
            this.Controls.Add(this.reoGridControlUser);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStripTop);
            this.Name = "FormAddPutawayItem";
            this.Text = "FormAddPutawayItem";
            this.Load += new System.EventHandler(this.FormAddPutawayItem_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStripTop.ResumeLayout(false);
            this.toolStripTop.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripStatusLabel lableStatus;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton buttonReceiptCancel;
        private System.Windows.Forms.ToolStripButton buttonPutaway;
        private System.Windows.Forms.ToolStripButton buttonDelete;
        private System.Windows.Forms.ToolStripButton buttonAdd;
        private System.Windows.Forms.ToolStrip toolStripTop;
        private unvell.ReoGrid.ReoGridControl reoGridControlUser;
    }
}