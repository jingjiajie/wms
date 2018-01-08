namespace WMS.UI
{
    partial class FormStockInfoCheckTicket
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormStockInfoCheckTicket));
            this.toolStripTop = new System.Windows.Forms.ToolStrip();
            this.labelSelect = new System.Windows.Forms.ToolStripLabel();
            this.comboBoxSearchCondition = new System.Windows.Forms.ToolStripComboBox();
            this.textBoxSearchValue = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonAdd = new System.Windows.Forms.ToolStripButton();
            this.buttonAlter = new System.Windows.Forms.ToolStripButton();
            this.buttonDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.button_additeam = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.labelStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.reoGridControlMain = new unvell.ReoGrid.ReoGridControl();
            this.paperpanel = new System.Windows.Forms.Panel();
            this.toolStripTop.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripTop
            // 
            this.toolStripTop.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.toolStripTop.BackgroundImage = global::WMS.UI.Properties.Resources.bottonW_q;
            this.toolStripTop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStripTop.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripTop.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labelSelect,
            this.comboBoxSearchCondition,
            this.textBoxSearchValue,
            this.toolStripButton1,
            this.toolStripSeparator1,
            this.buttonAdd,
            this.buttonAlter,
            this.buttonDelete,
            this.toolStripSeparator2,
            this.button_additeam});
            this.toolStripTop.Location = new System.Drawing.Point(0, 0);
            this.toolStripTop.Name = "toolStripTop";
            this.toolStripTop.Size = new System.Drawing.Size(824, 28);
            this.toolStripTop.TabIndex = 2;
            this.toolStripTop.Text = "toolStrip1";
            // 
            // labelSelect
            // 
            this.labelSelect.Name = "labelSelect";
            this.labelSelect.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labelSelect.Size = new System.Drawing.Size(68, 25);
            this.labelSelect.Text = "查询条件：";
            // 
            // comboBoxSearchCondition
            // 
            this.comboBoxSearchCondition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSearchCondition.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.comboBoxSearchCondition.Name = "comboBoxSearchCondition";
            this.comboBoxSearchCondition.Size = new System.Drawing.Size(97, 28);
            this.comboBoxSearchCondition.SelectedIndexChanged += new System.EventHandler(this.comboBoxSearchCondition_SelectedIndexChanged);
            // 
            // textBoxSearchValue
            // 
            this.textBoxSearchValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxSearchValue.Enabled = false;
            this.textBoxSearchValue.Name = "textBoxSearchValue";
            this.textBoxSearchValue.Size = new System.Drawing.Size(146, 28);
            this.textBoxSearchValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxSearchValue_KeyPress);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(56, 25);
            this.toolStripButton1.Text = "查询";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.AutoSize = false;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(15, 28);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Image = ((System.Drawing.Image)(resources.GetObject("buttonAdd.Image")));
            this.buttonAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(56, 25);
            this.buttonAdd.Text = "添加";
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click_1);
            // 
            // buttonAlter
            // 
            this.buttonAlter.Image = ((System.Drawing.Image)(resources.GetObject("buttonAlter.Image")));
            this.buttonAlter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonAlter.Name = "buttonAlter";
            this.buttonAlter.Size = new System.Drawing.Size(56, 25);
            this.buttonAlter.Text = "修改";
            this.buttonAlter.Click += new System.EventHandler(this.buttonAlter_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Image = ((System.Drawing.Image)(resources.GetObject("buttonDelete.Image")));
            this.buttonDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(56, 25);
            this.buttonDelete.Text = "删除";
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click_1);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.AutoSize = false;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(15, 28);
            // 
            // button_additeam
            // 
            this.button_additeam.AutoSize = false;
            this.button_additeam.Image = ((System.Drawing.Image)(resources.GetObject("button_additeam.Image")));
            this.button_additeam.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.button_additeam.Name = "button_additeam";
            this.button_additeam.Size = new System.Drawing.Size(100, 25);
            this.button_additeam.Text = "盘点单条目";
            this.button_additeam.Click += new System.EventHandler(this.button_additeam_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.labelStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 353);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 7, 0);
            this.statusStrip1.Size = new System.Drawing.Size(824, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(44, 17);
            this.toolStripStatusLabel1.Text = "状态：";
            // 
            // labelStatus
            // 
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(56, 17);
            this.labelStatus.Text = "库存管理";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.reoGridControlMain, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.paperpanel, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 28);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(824, 325);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // reoGridControlMain
            // 
            this.reoGridControlMain.BackColor = System.Drawing.Color.White;
            this.reoGridControlMain.ColumnHeaderContextMenuStrip = null;
            this.reoGridControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reoGridControlMain.LeadHeaderContextMenuStrip = null;
            this.reoGridControlMain.Location = new System.Drawing.Point(2, 2);
            this.reoGridControlMain.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.reoGridControlMain.Name = "reoGridControlMain";
            this.reoGridControlMain.Readonly = true;
            this.reoGridControlMain.RowHeaderContextMenuStrip = null;
            this.reoGridControlMain.Script = null;
            this.reoGridControlMain.SheetTabContextMenuStrip = null;
            this.reoGridControlMain.SheetTabNewButtonVisible = true;
            this.reoGridControlMain.SheetTabVisible = true;
            this.reoGridControlMain.SheetTabWidth = 60;
            this.reoGridControlMain.ShowScrollEndSpacing = true;
            this.reoGridControlMain.Size = new System.Drawing.Size(820, 288);
            this.reoGridControlMain.TabIndex = 4;
            this.reoGridControlMain.Text = "reoGridControl1";
            // 
            // paperpanel
            // 
            this.paperpanel.Location = new System.Drawing.Point(2, 294);
            this.paperpanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.paperpanel.Name = "paperpanel";
            this.paperpanel.Size = new System.Drawing.Size(820, 28);
            this.paperpanel.TabIndex = 5;
            // 
            // FormStockInfoCheckTicket
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 375);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStripTop);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FormStockInfoCheckTicket";
            this.Text = "库存盘点";
            this.Load += new System.EventHandler(this.FormStockInfoCheckTicket_Load);
            this.toolStripTop.ResumeLayout(false);
            this.toolStripTop.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStripTop;
        private System.Windows.Forms.ToolStripLabel labelSelect;
        private System.Windows.Forms.ToolStripComboBox comboBoxSearchCondition;
        private System.Windows.Forms.ToolStripTextBox textBoxSearchValue;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel labelStatus;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton buttonAdd;
        private System.Windows.Forms.ToolStripButton buttonAlter;
        private System.Windows.Forms.ToolStripButton buttonDelete;
        private System.Windows.Forms.ToolStripButton button_additeam;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private unvell.ReoGrid.ReoGridControl reoGridControlMain;
        private System.Windows.Forms.Panel paperpanel;
    }
}