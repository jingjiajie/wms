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
            this.reoGridControlUser = new unvell.ReoGrid.ReoGridControl();
            this.toolStripTop = new System.Windows.Forms.ToolStrip();
            this.toolStripLabelSelect = new System.Windows.Forms.ToolStripLabel();
            this.comboBoxSelect = new System.Windows.Forms.ToolStripComboBox();
            this.textBoxSelect = new System.Windows.Forms.ToolStripTextBox();
            this.buttonSelect = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonAdd = new System.Windows.Forms.ToolStripButton();
            this.buttonAlter = new System.Windows.Forms.ToolStripButton();
            this.buttonDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonCheck = new System.Windows.Forms.ToolStripButton();
            this.buttonCheckCancel = new System.Windows.Forms.ToolStripButton();
            this.buttonPrint = new System.Windows.Forms.ToolStripButton();
            this.buttonReceipt = new System.Windows.Forms.ToolStripButton();
            this.buttonReceiptCancel = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lableStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripTop.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // reoGridControlUser
            // 
            this.reoGridControlUser.BackColor = System.Drawing.Color.White;
            this.reoGridControlUser.ColumnHeaderContextMenuStrip = null;
            this.reoGridControlUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reoGridControlUser.LeadHeaderContextMenuStrip = null;
            this.reoGridControlUser.Location = new System.Drawing.Point(0, 39);
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
            this.reoGridControlUser.Size = new System.Drawing.Size(1360, 741);
            this.reoGridControlUser.TabIndex = 3;
            this.reoGridControlUser.Text = "reoGridControl1";
            this.reoGridControlUser.Click += new System.EventHandler(this.reoGridControlUser_Click);
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
            this.buttonAdd,
            this.buttonAlter,
            this.buttonDelete,
            this.toolStripSeparator2,
            this.buttonCheck,
            this.buttonCheckCancel,
            this.buttonPrint,
            this.buttonReceipt,
            this.buttonReceiptCancel,
            this.toolStripButton1});
            this.toolStripTop.Location = new System.Drawing.Point(0, 0);
            this.toolStripTop.Name = "toolStripTop";
            this.toolStripTop.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStripTop.Size = new System.Drawing.Size(1360, 39);
            this.toolStripTop.TabIndex = 2;
            this.toolStripTop.Text = "toolStrip1";
            this.toolStripTop.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStripTop_ItemClicked);
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
            // buttonAdd
            // 
            this.buttonAdd.AutoSize = false;
            this.buttonAdd.Image = ((System.Drawing.Image)(resources.GetObject("buttonAdd.Image")));
            this.buttonAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(60, 25);
            this.buttonAdd.Text = "添加";
            // 
            // buttonAlter
            // 
            this.buttonAlter.AutoSize = false;
            this.buttonAlter.Image = ((System.Drawing.Image)(resources.GetObject("buttonAlter.Image")));
            this.buttonAlter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonAlter.Name = "buttonAlter";
            this.buttonAlter.Size = new System.Drawing.Size(60, 25);
            this.buttonAlter.Text = "修改";
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
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.AutoSize = false;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(15, 28);
            // 
            // buttonCheck
            // 
            this.buttonCheck.AutoSize = false;
            this.buttonCheck.BackColor = System.Drawing.SystemColors.Control;
            this.buttonCheck.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.buttonCheck.Image = ((System.Drawing.Image)(resources.GetObject("buttonCheck.Image")));
            this.buttonCheck.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonCheck.Name = "buttonCheck";
            this.buttonCheck.Size = new System.Drawing.Size(40, 25);
            this.buttonCheck.Text = "送检";
            this.buttonCheck.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            // 
            // buttonCheckCancel
            // 
            this.buttonCheckCancel.AutoSize = false;
            this.buttonCheckCancel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.buttonCheckCancel.Image = ((System.Drawing.Image)(resources.GetObject("buttonCheckCancel.Image")));
            this.buttonCheckCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonCheckCancel.Name = "buttonCheckCancel";
            this.buttonCheckCancel.Size = new System.Drawing.Size(70, 25);
            this.buttonCheckCancel.Text = "取消送检";
            // 
            // buttonPrint
            // 
            this.buttonPrint.AutoSize = false;
            this.buttonPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.buttonPrint.Image = ((System.Drawing.Image)(resources.GetObject("buttonPrint.Image")));
            this.buttonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonPrint.Name = "buttonPrint";
            this.buttonPrint.Size = new System.Drawing.Size(80, 25);
            this.buttonPrint.Text = "打印送检单";
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
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(24, 36);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.lableStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 744);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1360, 36);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(86, 33);
            this.toolStripStatusLabel1.Text = "状态：";
            // 
            // lableStatus
            // 
            this.lableStatus.Name = "lableStatus";
            this.lableStatus.Size = new System.Drawing.Size(110, 33);
            this.lableStatus.Text = "到货管理";
            // 
            // FormReceiptArrival
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1360, 780);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.reoGridControlUser);
            this.Controls.Add(this.toolStripTop);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FormReceiptArrival";
            this.Text = "FormReceiptArrival";
            this.Load += new System.EventHandler(this.FormReceiptArrival_Load);
            this.toolStripTop.ResumeLayout(false);
            this.toolStripTop.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

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
        private System.Windows.Forms.ToolStripButton buttonPrint;
        private System.Windows.Forms.ToolStripButton buttonReceipt;
        private System.Windows.Forms.ToolStripButton buttonReceiptCancel;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel lableStatus;
    }
}