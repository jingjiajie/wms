namespace WMS.UI
{
    partial class FormReceiptArrival
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormReceiptArrival));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lableStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.reoGridControlUser = new unvell.ReoGrid.ReoGridControl();
            this.toolStripTop = new System.Windows.Forms.ToolStrip();
            this.toolStripLabelSelect = new System.Windows.Forms.ToolStripLabel();
            this.comboBoxSelect = new System.Windows.Forms.ToolStripComboBox();
            this.textBoxSelect = new System.Windows.Forms.ToolStripTextBox();
            this.buttonSelect = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonItems = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonAdd = new System.Windows.Forms.ToolStripButton();
            this.buttonAlter = new System.Windows.Forms.ToolStripButton();
            this.buttonDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonItemSubmission = new System.Windows.Forms.ToolStripButton();
            this.buttonCheck = new System.Windows.Forms.ToolStripButton();
            this.buttonCheckCancel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonPutaway = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripPutawayTicket = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.toolStripTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.statusStrip1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.reoGridControlUser, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.toolStripTop, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1455, 566);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.lableStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 541);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 9, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1455, 25);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(54, 20);
            this.toolStripStatusLabel1.Text = "状态：";
            // 
            // lableStatus
            // 
            this.lableStatus.Name = "lableStatus";
            this.lableStatus.Size = new System.Drawing.Size(69, 20);
            this.lableStatus.Text = "到货管理";
            // 
            // reoGridControlUser
            // 
            this.reoGridControlUser.BackColor = System.Drawing.Color.White;
            this.reoGridControlUser.ColumnHeaderContextMenuStrip = null;
            this.reoGridControlUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reoGridControlUser.LeadHeaderContextMenuStrip = null;
            this.reoGridControlUser.Location = new System.Drawing.Point(3, 34);
            this.reoGridControlUser.Name = "reoGridControlUser";
            this.reoGridControlUser.Readonly = true;
            this.reoGridControlUser.RowHeaderContextMenuStrip = null;
            this.reoGridControlUser.Script = null;
            this.reoGridControlUser.SheetTabContextMenuStrip = null;
            this.reoGridControlUser.SheetTabNewButtonVisible = true;
            this.reoGridControlUser.SheetTabVisible = true;
            this.reoGridControlUser.SheetTabWidth = 60;
            this.reoGridControlUser.ShowScrollEndSpacing = true;
            this.reoGridControlUser.Size = new System.Drawing.Size(1449, 504);
            this.reoGridControlUser.TabIndex = 10;
            this.reoGridControlUser.Text = "reoGridControl1";
            // 
            // toolStripTop
            // 
            this.toolStripTop.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.toolStripTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripTop.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripTop.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabelSelect,
            this.comboBoxSelect,
            this.textBoxSelect,
            this.buttonSelect,
            this.toolStripSeparator1,
            this.buttonItems,
            this.toolStripSeparator2,
            this.buttonAdd,
            this.buttonAlter,
            this.buttonDelete,
            this.toolStripSeparator4,
            this.buttonItemSubmission,
            this.buttonCheck,
            this.buttonCheckCancel,
            this.toolStripSeparator5,
            this.toolStripButton3,
            this.toolStripPutawayTicket,
            this.toolStripButton2,
            this.toolStripSeparator3,
            this.buttonPutaway});
            this.toolStripTop.Location = new System.Drawing.Point(0, 0);
            this.toolStripTop.Name = "toolStripTop";
            this.toolStripTop.Size = new System.Drawing.Size(1455, 31);
            this.toolStripTop.TabIndex = 3;
            this.toolStripTop.Text = "toolStrip1";
            // 
            // toolStripLabelSelect
            // 
            this.toolStripLabelSelect.Name = "toolStripLabelSelect";
            this.toolStripLabelSelect.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripLabelSelect.Size = new System.Drawing.Size(84, 28);
            this.toolStripLabelSelect.Text = "查询条件：";
            // 
            // comboBoxSelect
            // 
            this.comboBoxSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSelect.Name = "comboBoxSelect";
            this.comboBoxSelect.Size = new System.Drawing.Size(150, 31);
            this.comboBoxSelect.SelectedIndexChanged += new System.EventHandler(this.comboBoxSelect_SelectedIndexChanged_1);
            // 
            // textBoxSelect
            // 
            this.textBoxSelect.Name = "textBoxSelect";
            this.textBoxSelect.Size = new System.Drawing.Size(200, 31);
            // 
            // buttonSelect
            // 
            this.buttonSelect.Image = ((System.Drawing.Image)(resources.GetObject("buttonSelect.Image")));
            this.buttonSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonSelect.Name = "buttonSelect";
            this.buttonSelect.Size = new System.Drawing.Size(63, 28);
            this.buttonSelect.Text = "查询";
            this.buttonSelect.Click += new System.EventHandler(this.buttonSelect_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.AutoSize = false;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(15, 28);
            // 
            // buttonItems
            // 
            this.buttonItems.Image = global::WMS.UI.Properties.Resources.find;
            this.buttonItems.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonItems.Name = "buttonItems";
            this.buttonItems.Size = new System.Drawing.Size(123, 28);
            this.buttonItems.Text = "查看零件条目";
            this.buttonItems.ToolTipText = "查看零件条目";
            this.buttonItems.Click += new System.EventHandler(this.buttonItems_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.AutoSize = false;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(15, 28);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Image = ((System.Drawing.Image)(resources.GetObject("buttonAdd.Image")));
            this.buttonAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(63, 28);
            this.buttonAdd.Text = "添加";
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonAlter
            // 
            this.buttonAlter.Image = ((System.Drawing.Image)(resources.GetObject("buttonAlter.Image")));
            this.buttonAlter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonAlter.Name = "buttonAlter";
            this.buttonAlter.Size = new System.Drawing.Size(63, 28);
            this.buttonAlter.Text = "修改";
            this.buttonAlter.Click += new System.EventHandler(this.buttonAlter_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Image = ((System.Drawing.Image)(resources.GetObject("buttonDelete.Image")));
            this.buttonDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(63, 28);
            this.buttonDelete.Text = "删除";
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.AutoSize = false;
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(15, 28);
            // 
            // buttonItemSubmission
            // 
            this.buttonItemSubmission.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.buttonItemSubmission.Image = ((System.Drawing.Image)(resources.GetObject("buttonItemSubmission.Image")));
            this.buttonItemSubmission.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonItemSubmission.Name = "buttonItemSubmission";
            this.buttonItemSubmission.Size = new System.Drawing.Size(43, 28);
            this.buttonItemSubmission.Text = "送检";
            this.buttonItemSubmission.Click += new System.EventHandler(this.buttonReceiptCancel_Click);
            // 
            // buttonCheck
            // 
            this.buttonCheck.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.buttonCheck.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.buttonCheck.Image = ((System.Drawing.Image)(resources.GetObject("buttonCheck.Image")));
            this.buttonCheck.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonCheck.Name = "buttonCheck";
            this.buttonCheck.Size = new System.Drawing.Size(73, 28);
            this.buttonCheck.Text = "整单送检";
            this.buttonCheck.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.buttonCheck.Click += new System.EventHandler(this.buttonCheck_Click);
            // 
            // buttonCheckCancel
            // 
            this.buttonCheckCancel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.buttonCheckCancel.Image = ((System.Drawing.Image)(resources.GetObject("buttonCheckCancel.Image")));
            this.buttonCheckCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonCheckCancel.Name = "buttonCheckCancel";
            this.buttonCheckCancel.Size = new System.Drawing.Size(73, 28);
            this.buttonCheckCancel.Text = "取消送检";
            this.buttonCheckCancel.Click += new System.EventHandler(this.buttonCheckCancel_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.AutoSize = false;
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(15, 28);
            // 
            // buttonPutaway
            // 
            this.buttonPutaway.Image = ((System.Drawing.Image)(resources.GetObject("buttonPutaway.Image")));
            this.buttonPutaway.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonPutaway.Name = "buttonPutaway";
            this.buttonPutaway.Size = new System.Drawing.Size(108, 28);
            this.buttonPutaway.Text = "生成上架单";
            this.buttonPutaway.Click += new System.EventHandler(this.buttonMakePutaway_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.AutoSize = false;
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(15, 28);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(43, 28);
            this.toolStripButton3.Text = "收货";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click_1);
            // 
            // toolStripPutawayTicket
            // 
            this.toolStripPutawayTicket.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripPutawayTicket.Image = ((System.Drawing.Image)(resources.GetObject("toolStripPutawayTicket.Image")));
            this.toolStripPutawayTicket.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripPutawayTicket.Name = "toolStripPutawayTicket";
            this.toolStripPutawayTicket.Size = new System.Drawing.Size(73, 28);
            this.toolStripPutawayTicket.Text = "整单收货";
            this.toolStripPutawayTicket.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(73, 28);
            this.toolStripButton2.Text = "取消收货";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // FormReceiptArrival
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1455, 566);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FormReceiptArrival";
            this.Text = "FormReceiptArrival";
            this.Load += new System.EventHandler(this.FormReceiptArrival_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStripTop.ResumeLayout(false);
            this.toolStripTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel lableStatus;
        private unvell.ReoGrid.ReoGridControl reoGridControlUser;
        private System.Windows.Forms.ToolStrip toolStripTop;
        private System.Windows.Forms.ToolStripLabel toolStripLabelSelect;
        private System.Windows.Forms.ToolStripComboBox comboBoxSelect;
        private System.Windows.Forms.ToolStripTextBox textBoxSelect;
        private System.Windows.Forms.ToolStripButton buttonSelect;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton buttonAdd;
        private System.Windows.Forms.ToolStripButton buttonAlter;
        private System.Windows.Forms.ToolStripButton buttonDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton buttonCheck;
        private System.Windows.Forms.ToolStripButton buttonCheckCancel;
        private System.Windows.Forms.ToolStripButton buttonItems;
        private System.Windows.Forms.ToolStripButton toolStripPutawayTicket;
        private System.Windows.Forms.ToolStripButton buttonPutaway;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton buttonItemSubmission;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    }
}