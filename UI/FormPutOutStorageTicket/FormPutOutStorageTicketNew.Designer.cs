namespace WMS.UI
{
    partial class FormPutOutStorageTicketNew
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
            this.reoGridControlMain = new unvell.ReoGrid.ReoGridControl();
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonOK = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutEditPanel = new System.Windows.Forms.TableLayoutPanel();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.tableLayoutPanelMain.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // reoGridControlMain
            // 
            this.reoGridControlMain.BackColor = System.Drawing.Color.White;
            this.reoGridControlMain.ColumnHeaderContextMenuStrip = null;
            this.reoGridControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reoGridControlMain.LeadHeaderContextMenuStrip = null;
            this.reoGridControlMain.Location = new System.Drawing.Point(5, 6);
            this.reoGridControlMain.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.reoGridControlMain.Name = "reoGridControlMain";
            this.reoGridControlMain.RowHeaderContextMenuStrip = null;
            this.reoGridControlMain.Script = null;
            this.reoGridControlMain.SheetTabContextMenuStrip = null;
            this.reoGridControlMain.SheetTabNewButtonVisible = true;
            this.reoGridControlMain.SheetTabVisible = true;
            this.reoGridControlMain.SheetTabWidth = 105;
            this.reoGridControlMain.ShowScrollEndSpacing = true;
            this.reoGridControlMain.Size = new System.Drawing.Size(964, 237);
            this.reoGridControlMain.TabIndex = 12;
            this.reoGridControlMain.Text = "reoGridControl1";
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tableLayoutPanelMain.ColumnCount = 1;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.Controls.Add(this.reoGridControlMain, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.tableLayoutPanel3, 0, 2);
            this.tableLayoutPanelMain.Controls.Add(this.groupBox1, 0, 1);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 3;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 220F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(974, 529);
            this.tableLayoutPanelMain.TabIndex = 2;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.tableLayoutPanel3.ColumnCount = 5;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.24154F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.75846F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.75846F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.24154F));
            this.tableLayoutPanel3.Controls.Add(this.buttonOK, 2, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 472);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(968, 54);
            this.tableLayoutPanel3.TabIndex = 14;
            // 
            // buttonOK
            // 
            this.buttonOK.BackgroundImage = global::WMS.UI.Properties.Resources.bottonB2_q;
            this.buttonOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonOK.FlatAppearance.BorderSize = 0;
            this.buttonOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOK.Font = new System.Drawing.Font("黑体", 10F);
            this.buttonOK.Location = new System.Drawing.Point(396, 3);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(174, 54);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "创建出库单";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            this.buttonOK.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonOK_MouseDown);
            this.buttonOK.MouseEnter += new System.EventHandler(this.buttonOK_MouseEnter);
            this.buttonOK.MouseLeave += new System.EventHandler(this.buttonOK_MouseLeave);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.groupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.groupBox1.Controls.Add(this.tableLayoutEditPanel);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("黑体", 10F);
            this.groupBox1.Location = new System.Drawing.Point(3, 252);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(968, 214);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "出库单信息";
            // 
            // tableLayoutEditPanel
            // 
            this.tableLayoutEditPanel.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.tableLayoutEditPanel.ColumnCount = 8;
            this.tableLayoutEditPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.tableLayoutEditPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tableLayoutEditPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.tableLayoutEditPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tableLayoutEditPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.tableLayoutEditPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tableLayoutEditPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.tableLayoutEditPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tableLayoutEditPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutEditPanel.Font = new System.Drawing.Font("黑体", 10F);
            this.tableLayoutEditPanel.Location = new System.Drawing.Point(3, 34);
            this.tableLayoutEditPanel.Name = "tableLayoutEditPanel";
            this.tableLayoutEditPanel.RowCount = 4;
            this.tableLayoutEditPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutEditPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutEditPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutEditPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutEditPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutEditPanel.Size = new System.Drawing.Size(962, 177);
            this.tableLayoutEditPanel.TabIndex = 13;
            // 
            // FormPutOutStorageTicketNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 27F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(974, 529);
            this.Controls.Add(this.tableLayoutPanelMain);
            this.Font = new System.Drawing.Font("黑体", 10F);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormPutOutStorageTicketNew";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "创建出库单";
            this.Load += new System.EventHandler(this.FormPutOutStorageTicketNew_Load);
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private unvell.ReoGrid.ReoGridControl reoGridControlMain;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutEditPanel;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}