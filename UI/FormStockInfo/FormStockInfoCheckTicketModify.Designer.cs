namespace WMS.UI
{
    partial class FormStockInfoCheckTicketModify
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormStockInfoCheckTicketModify));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.labelStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonfinish = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.reoGridControlMain = new unvell.ReoGrid.ReoGridControl();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.labelStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 550);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 29, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1032, 22);
            this.statusStrip1.TabIndex = 14;
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
            this.labelStatus.Size = new System.Drawing.Size(68, 17);
            this.labelStatus.Text = "添加盘点单";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.panel1.Controls.Add(this.buttonDelete);
            this.panel1.Controls.Add(this.buttonfinish);
            this.panel1.Controls.Add(this.tableLayoutPanel2);
            this.panel1.Controls.Add(this.buttonAdd);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(6, 8);
            this.panel1.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1020, 162);
            this.panel1.TabIndex = 0;
            // 
            // buttonAdd
            // 
            this.buttonAdd.BackColor = System.Drawing.Color.White;
            this.buttonAdd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonAdd.BackgroundImage")));
            this.buttonAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonAdd.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonAdd.FlatAppearance.BorderSize = 0;
            this.buttonAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAdd.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.buttonAdd.Image = ((System.Drawing.Image)(resources.GetObject("buttonAdd.Image")));
            this.buttonAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAdd.Location = new System.Drawing.Point(576, 8);
            this.buttonAdd.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(115, 54);
            this.buttonAdd.TabIndex = 0;
            this.buttonAdd.Text = "    添加";
            this.buttonAdd.UseVisualStyleBackColor = false;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);

            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.tableLayoutPanel2.ColumnCount = 6;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 6);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(500, 153);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // buttonfinish
            // 
            this.buttonfinish.BackColor = System.Drawing.Color.White;
            this.buttonfinish.BackgroundImage = global::WMS.UI.Properties.Resources.bottonB2_s;
            this.buttonfinish.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonfinish.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonfinish.FlatAppearance.BorderSize = 0;
            this.buttonfinish.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonfinish.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.buttonfinish.Image = ((System.Drawing.Image)(resources.GetObject("buttonfinish.Image")));
            this.buttonfinish.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonfinish.Location = new System.Drawing.Point(576, 91);
            this.buttonfinish.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.buttonfinish.Name = "buttonfinish";
            this.buttonfinish.Size = new System.Drawing.Size(242, 54);
            this.buttonfinish.TabIndex = 7;
            this.buttonfinish.Text = "完成";
            this.buttonfinish.UseVisualStyleBackColor = false;
            this.buttonfinish.Click += new System.EventHandler(this.buttonfinish_Click);

            // 
            // buttonDelete
            // 
            this.buttonDelete.BackColor = System.Drawing.Color.White;
            this.buttonDelete.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonDelete.BackgroundImage")));
            this.buttonDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonDelete.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonDelete.FlatAppearance.BorderSize = 0;
            this.buttonDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDelete.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.buttonDelete.Image = ((System.Drawing.Image)(resources.GetObject("buttonDelete.Image")));
            this.buttonDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonDelete.Location = new System.Drawing.Point(722, 9);
            this.buttonDelete.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(115, 54);
            this.buttonDelete.TabIndex = 8;
            this.buttonDelete.Text = "    删除";
            this.buttonDelete.UseVisualStyleBackColor = false;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click_1);

            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.reoGridControlMain, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 178F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1032, 550);
            this.tableLayoutPanel1.TabIndex = 15;

            // 
            // reoGridControlMain
            // 
            this.reoGridControlMain.BackColor = System.Drawing.Color.White;
            this.reoGridControlMain.ColumnHeaderContextMenuStrip = null;
            this.reoGridControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reoGridControlMain.LeadHeaderContextMenuStrip = null;
            this.reoGridControlMain.Location = new System.Drawing.Point(9, 186);
            this.reoGridControlMain.Margin = new System.Windows.Forms.Padding(9, 8, 9, 8);
            this.reoGridControlMain.Name = "reoGridControlMain";
            this.reoGridControlMain.Readonly = true;
            this.reoGridControlMain.RowHeaderContextMenuStrip = null;
            this.reoGridControlMain.Script = null;
            this.reoGridControlMain.SheetTabContextMenuStrip = null;
            this.reoGridControlMain.SheetTabNewButtonVisible = true;
            this.reoGridControlMain.SheetTabVisible = true;
            this.reoGridControlMain.SheetTabWidth = 60;
            this.reoGridControlMain.ShowScrollEndSpacing = true;
            this.reoGridControlMain.Size = new System.Drawing.Size(1014, 356);
            this.reoGridControlMain.TabIndex = 13;
            this.reoGridControlMain.Text = "reoGridControl1";
            // 
            // FormStockInfoCheckTicketModify
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1032, 572);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormStockInfoCheckTicketModify";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "添加盘点单";
            this.Load += new System.EventHandler(this.FormStockCheckModify_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel labelStatus;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonfinish;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private unvell.ReoGrid.ReoGridControl reoGridControlMain;
    }
}