namespace WMS.UI.FormReceipt
{
    partial class FormSubmissionItem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSubmissionItem));
            this.panel2 = new System.Windows.Forms.Panel();
            this.reoGridControlSubmissionItems = new unvell.ReoGrid.ReoGridControl();
            this.buttonItemPass = new System.Windows.Forms.Button();
            this.buttonItemNoPass = new System.Windows.Forms.Button();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonModify = new System.Windows.Forms.Button();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelProperties = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labelStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.reoGridControlSubmissionItems);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(2, 147);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1018, 560);
            this.panel2.TabIndex = 1;
            // 
            // reoGridControlSubmissionItems
            // 
            this.reoGridControlSubmissionItems.BackColor = System.Drawing.Color.White;
            this.reoGridControlSubmissionItems.ColumnHeaderContextMenuStrip = null;
            this.reoGridControlSubmissionItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reoGridControlSubmissionItems.LeadHeaderContextMenuStrip = null;
            this.reoGridControlSubmissionItems.Location = new System.Drawing.Point(0, 0);
            this.reoGridControlSubmissionItems.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.reoGridControlSubmissionItems.Name = "reoGridControlSubmissionItems";
            this.reoGridControlSubmissionItems.Readonly = true;
            this.reoGridControlSubmissionItems.RowHeaderContextMenuStrip = null;
            this.reoGridControlSubmissionItems.Script = null;
            this.reoGridControlSubmissionItems.SheetTabContextMenuStrip = null;
            this.reoGridControlSubmissionItems.SheetTabNewButtonVisible = true;
            this.reoGridControlSubmissionItems.SheetTabVisible = true;
            this.reoGridControlSubmissionItems.SheetTabWidth = 93;
            this.reoGridControlSubmissionItems.ShowScrollEndSpacing = true;
            this.reoGridControlSubmissionItems.Size = new System.Drawing.Size(1018, 560);
            this.reoGridControlSubmissionItems.TabIndex = 9;
            this.reoGridControlSubmissionItems.Text = "reoGridControlSubmissionItems";
            // 
            // buttonItemPass
            // 
            this.buttonItemPass.BackColor = System.Drawing.Color.White;
            this.buttonItemPass.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonItemPass.BackgroundImage")));
            this.buttonItemPass.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonItemPass.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonItemPass.FlatAppearance.BorderSize = 0;
            this.buttonItemPass.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonItemPass.Font = new System.Drawing.Font("黑体", 10F);
            this.buttonItemPass.Image = ((System.Drawing.Image)(resources.GetObject("buttonItemPass.Image")));
            this.buttonItemPass.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonItemPass.Location = new System.Drawing.Point(2, 2);
            this.buttonItemPass.Margin = new System.Windows.Forms.Padding(2);
            this.buttonItemPass.Name = "buttonItemPass";
            this.buttonItemPass.Size = new System.Drawing.Size(121, 40);
            this.buttonItemPass.TabIndex = 0;
            this.buttonItemPass.Text = "合格";
            this.buttonItemPass.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonItemPass.UseVisualStyleBackColor = false;
            this.buttonItemPass.Click += new System.EventHandler(this.buttonItemPass_Click);
            this.buttonItemPass.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonItemPass_MouseDown);
            this.buttonItemPass.MouseEnter += new System.EventHandler(this.buttonItemPass_MouseEnter);
            this.buttonItemPass.MouseLeave += new System.EventHandler(this.buttonItemPass_MouseLeave);
            // 
            // buttonItemNoPass
            // 
            this.buttonItemNoPass.BackColor = System.Drawing.Color.White;
            this.buttonItemNoPass.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonItemNoPass.BackgroundImage")));
            this.buttonItemNoPass.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonItemNoPass.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonItemNoPass.FlatAppearance.BorderSize = 0;
            this.buttonItemNoPass.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonItemNoPass.Font = new System.Drawing.Font("黑体", 10F);
            this.buttonItemNoPass.Image = ((System.Drawing.Image)(resources.GetObject("buttonItemNoPass.Image")));
            this.buttonItemNoPass.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonItemNoPass.Location = new System.Drawing.Point(140, 2);
            this.buttonItemNoPass.Margin = new System.Windows.Forms.Padding(2);
            this.buttonItemNoPass.Name = "buttonItemNoPass";
            this.buttonItemNoPass.Size = new System.Drawing.Size(121, 40);
            this.buttonItemNoPass.TabIndex = 1;
            this.buttonItemNoPass.Text = "不合格";
            this.buttonItemNoPass.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonItemNoPass.UseVisualStyleBackColor = false;
            this.buttonItemNoPass.Click += new System.EventHandler(this.buttonItemNoPass_Click);
            this.buttonItemNoPass.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonItemNoPass_MouseDown);
            this.buttonItemNoPass.MouseEnter += new System.EventHandler(this.buttonItemNoPass_MouseEnter);
            this.buttonItemNoPass.MouseLeave += new System.EventHandler(this.buttonItemNoPass_MouseLeave);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel5, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 4;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(282, 137);
            this.tableLayoutPanel3.TabIndex = 4;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3.11284F));
            this.tableLayoutPanel4.Controls.Add(this.buttonModify, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 72);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(276, 53);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // buttonModify
            // 
            this.buttonModify.BackColor = System.Drawing.Color.White;
            this.buttonModify.BackgroundImage = global::WMS.UI.Properties.Resources.bottonB2_s;
            this.buttonModify.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonModify.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonModify.FlatAppearance.BorderSize = 0;
            this.buttonModify.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonModify.Font = new System.Drawing.Font("黑体", 10F);
            this.buttonModify.Image = ((System.Drawing.Image)(resources.GetObject("buttonModify.Image")));
            this.buttonModify.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonModify.Location = new System.Drawing.Point(2, 2);
            this.buttonModify.Margin = new System.Windows.Forms.Padding(2);
            this.buttonModify.Name = "buttonModify";
            this.buttonModify.Size = new System.Drawing.Size(259, 42);
            this.buttonModify.TabIndex = 2;
            this.buttonModify.Text = "确认修改";
            this.buttonModify.UseVisualStyleBackColor = false;
            this.buttonModify.Click += new System.EventHandler(this.buttonModify_Click);
            this.buttonModify.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonModify_MouseDown);
            this.buttonModify.MouseEnter += new System.EventHandler(this.buttonModify_MouseEnter);
            this.buttonModify.MouseLeave += new System.EventHandler(this.buttonModify_MouseLeave);
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Controls.Add(this.buttonItemPass, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.buttonItemNoPass, 1, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 12);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(276, 54);
            this.tableLayoutPanel5.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.tableLayoutPanel3);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(734, 2);
            this.panel3.Margin = new System.Windows.Forms.Padding(2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(282, 137);
            this.panel3.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 286F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanelProperties, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel3, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1018, 141);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // tableLayoutPanelProperties
            // 
            this.tableLayoutPanelProperties.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.tableLayoutPanelProperties.ColumnCount = 8;
            this.tableLayoutPanelProperties.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.87363F));
            this.tableLayoutPanelProperties.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.98901F));
            this.tableLayoutPanelProperties.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.42308F));
            this.tableLayoutPanelProperties.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.43956F));
            this.tableLayoutPanelProperties.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.32418F));
            this.tableLayoutPanelProperties.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.67582F));
            this.tableLayoutPanelProperties.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.73626F));
            this.tableLayoutPanelProperties.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.36264F));
            this.tableLayoutPanelProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelProperties.Location = new System.Drawing.Point(2, 2);
            this.tableLayoutPanelProperties.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanelProperties.Name = "tableLayoutPanelProperties";
            this.tableLayoutPanelProperties.RowCount = 3;
            this.tableLayoutPanelProperties.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelProperties.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelProperties.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelProperties.Size = new System.Drawing.Size(728, 137);
            this.tableLayoutPanelProperties.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(2, 2);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1018, 141);
            this.panel1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 145F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1022, 709);
            this.tableLayoutPanel1.TabIndex = 15;
            // 
            // labelStatus
            // 
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(114, 20);
            this.labelStatus.Text = "查看收货单条目";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(54, 20);
            this.toolStripStatusLabel1.Text = "状态：";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.labelStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 709);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 11, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1022, 25);
            this.statusStrip1.TabIndex = 14;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // FormSubmissionItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1022, 734);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.statusStrip1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSubmissionItem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "查看明细";
            this.Load += new System.EventHandler(this.FormSubmissionItem_Load);
            this.panel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private unvell.ReoGrid.ReoGridControl reoGridControlSubmissionItems;
        private System.Windows.Forms.Button buttonItemPass;
        private System.Windows.Forms.Button buttonItemNoPass;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelProperties;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStripStatusLabel labelStatus;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button buttonModify;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
    }
}