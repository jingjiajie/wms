﻿namespace WMS.UI
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
            this.reoGridControl1 = new unvell.ReoGrid.ReoGridControl();
            this.toolStripLabelSelect = new System.Windows.Forms.ToolStripLabel();
            this.comboBoxSelect = new System.Windows.Forms.ToolStripComboBox();
            this.textBoxSelect = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonPass = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripTop = new System.Windows.Forms.ToolStrip();
            this.buttonSelect = new System.Windows.Forms.ToolStripButton();
            this.buttonItem = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.ButtonDelete = new System.Windows.Forms.ToolStripButton();
            this.buttonItems = new System.Windows.Forms.ToolStripButton();
            this.buttonNoPass = new System.Windows.Forms.ToolStripButton();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip2 = new System.Windows.Forms.StatusStrip();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStripTop.SuspendLayout();
            this.statusStrip2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // reoGridControl1
            // 
            this.reoGridControl1.BackColor = System.Drawing.Color.White;
            this.reoGridControl1.ColumnHeaderContextMenuStrip = null;
            this.reoGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reoGridControl1.LeadHeaderContextMenuStrip = null;
            this.reoGridControl1.Location = new System.Drawing.Point(3, 34);
            this.reoGridControl1.Name = "reoGridControl1";
            this.reoGridControl1.Readonly = true;
            this.reoGridControl1.RowHeaderContextMenuStrip = null;
            this.reoGridControl1.Script = null;
            this.reoGridControl1.SheetTabContextMenuStrip = null;
            this.reoGridControl1.SheetTabNewButtonVisible = true;
            this.reoGridControl1.SheetTabVisible = true;
            this.reoGridControl1.SheetTabWidth = 60;
            this.reoGridControl1.ShowScrollEndSpacing = true;
            this.reoGridControl1.Size = new System.Drawing.Size(1093, 547);
            this.reoGridControl1.TabIndex = 9;
            this.reoGridControl1.Text = "reoGridControl1";
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
            this.comboBoxSelect.SelectedIndexChanged += new System.EventHandler(this.comboBoxSelect_SelectedIndexChanged);
            // 
            // textBoxSelect
            // 
            this.textBoxSelect.Name = "textBoxSelect";
            this.textBoxSelect.Size = new System.Drawing.Size(200, 31);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.AutoSize = false;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(15, 28);
            // 
            // buttonPass
            // 
            this.buttonPass.Image = ((System.Drawing.Image)(resources.GetObject("buttonPass.Image")));
            this.buttonPass.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonPass.Name = "buttonPass";
            this.buttonPass.Size = new System.Drawing.Size(63, 28);
            this.buttonPass.Text = "合格";
            this.buttonPass.Click += new System.EventHandler(this.buttonPass_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.AutoSize = false;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(15, 28);
            // 
            // toolStripTop
            // 
            this.toolStripTop.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.toolStripTop.BackgroundImage = global::WMS.UI.Properties.Resources.bottonW_q;
            this.toolStripTop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStripTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripTop.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripTop.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabelSelect,
            this.comboBoxSelect,
            this.textBoxSelect,
            this.buttonSelect,
            this.toolStripSeparator1,
            this.buttonItem,
            this.toolStripSeparator3,
            this.buttonItems,
            this.ButtonDelete,
            this.toolStripSeparator2,
            this.buttonPass,
            this.buttonNoPass});
            this.toolStripTop.Location = new System.Drawing.Point(0, 0);
            this.toolStripTop.Name = "toolStripTop";
            this.toolStripTop.Size = new System.Drawing.Size(1099, 31);
            this.toolStripTop.TabIndex = 8;
            this.toolStripTop.Text = "toolStrip1";
            this.toolStripTop.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStripTop_ItemClicked);
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
            // buttonItem
            // 
            this.buttonItem.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.buttonItem.Image = global::WMS.UI.Properties.Resources.find;
            this.buttonItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonItem.Name = "buttonItem";
            this.buttonItem.Size = new System.Drawing.Size(93, 28);
            this.buttonItem.Text = "查看详细";
            this.buttonItem.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.buttonItem.Click += new System.EventHandler(this.buttonItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.AutoSize = false;
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(15, 28);
            // 
            // ButtonDelete
            // 
            this.ButtonDelete.Image = global::WMS.UI.Properties.Resources.delete;
            this.ButtonDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ButtonDelete.Name = "ButtonDelete";
            this.ButtonDelete.Size = new System.Drawing.Size(108, 28);
            this.ButtonDelete.Text = "删除送检单";
            this.ButtonDelete.ToolTipText = "查看零件条目";
            this.ButtonDelete.Click += new System.EventHandler(this.ButtonDelete_Click);
            // 
            // buttonItems
            // 
            this.buttonItems.Image = global::WMS.UI.Properties.Resources.cancle;
            this.buttonItems.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonItems.Name = "buttonItems";
            this.buttonItems.Size = new System.Drawing.Size(108, 28);
            this.buttonItems.Text = "修改送检单";
            this.buttonItems.ToolTipText = "查看零件条目";
            this.buttonItems.Click += new System.EventHandler(this.buttonItems_Click);
            // 
            // buttonNoPass
            // 
            this.buttonNoPass.Image = ((System.Drawing.Image)(resources.GetObject("buttonNoPass.Image")));
            this.buttonNoPass.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonNoPass.Name = "buttonNoPass";
            this.buttonNoPass.Size = new System.Drawing.Size(78, 28);
            this.buttonNoPass.Text = "不合格";
            this.buttonNoPass.Click += new System.EventHandler(this.buttonNoPass_Click);
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(54, 20);
            this.toolStripStatusLabel3.Text = "状态：";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(69, 20);
            this.toolStripStatusLabel2.Text = "到货管理";
            // 
            // statusStrip2
            // 
            this.statusStrip2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusStrip2.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.statusStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel3,
            this.toolStripStatusLabel2});
            this.statusStrip2.Location = new System.Drawing.Point(0, 584);
            this.statusStrip2.Name = "statusStrip2";
            this.statusStrip2.Padding = new System.Windows.Forms.Padding(1, 0, 9, 0);
            this.statusStrip2.Size = new System.Drawing.Size(1099, 25);
            this.statusStrip2.TabIndex = 10;
            this.statusStrip2.Text = "statusStrip2";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 13F));
            this.tableLayoutPanel1.Controls.Add(this.toolStripTop, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.statusStrip2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.reoGridControl1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1099, 609);
            this.tableLayoutPanel1.TabIndex = 11;
            // 
            // FormSubmissionManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1099, 609);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "FormSubmissionManage";
            this.Text = "FormSubmissionManage";
            this.Load += new System.EventHandler(this.FormSubmissionManage_Load);
            this.toolStripTop.ResumeLayout(false);
            this.toolStripTop.PerformLayout();
            this.statusStrip2.ResumeLayout(false);
            this.statusStrip2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
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
        private System.Windows.Forms.ToolStrip toolStripTop;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.StatusStrip statusStrip2;
        private System.Windows.Forms.ToolStripButton buttonItems;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton ButtonDelete;
    }
}