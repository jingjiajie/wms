namespace WMS.UI
{
    partial class FormImport
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
            this.toolStripTop = new System.Windows.Forms.ToolStrip();
            this.buttonImport = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.comboBoxImeMode = new System.Windows.Forms.ToolStripComboBox();
            this.reoGridControlMain = new unvell.ReoGrid.ReoGridControl();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStripTop.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripTop
            // 
            this.toolStripTop.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.toolStripTop.BackgroundImage = global::WMS.UI.Properties.Resources.bottonW_q;
            this.toolStripTop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStripTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripTop.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripTop.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonImport,
            this.toolStripSeparator1,
            this.comboBoxImeMode});
            this.toolStripTop.Location = new System.Drawing.Point(0, 0);
            this.toolStripTop.Name = "toolStripTop";
            this.toolStripTop.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStripTop.Size = new System.Drawing.Size(1072, 50);
            this.toolStripTop.TabIndex = 5;
            this.toolStripTop.Text = "toolStrip1";
            // 
            // buttonImport
            // 
            this.buttonImport.Image = global::WMS.UI.Properties.Resources.add;
            this.buttonImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonImport.Name = "buttonImport";
            this.buttonImport.Size = new System.Drawing.Size(134, 47);
            this.buttonImport.Text = "确定导入";
            this.buttonImport.Click += new System.EventHandler(this.buttonImport_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 50);
            // 
            // comboBoxImeMode
            // 
            this.comboBoxImeMode.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.comboBoxImeMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxImeMode.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.comboBoxImeMode.Items.AddRange(new object[] {
            "默认中文输入",
            "默认英文输入"});
            this.comboBoxImeMode.Name = "comboBoxImeMode";
            this.comboBoxImeMode.Size = new System.Drawing.Size(121, 50);
            // 
            // reoGridControlMain
            // 
            this.reoGridControlMain.BackColor = System.Drawing.Color.White;
            this.reoGridControlMain.ColumnHeaderContextMenuStrip = null;
            this.reoGridControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reoGridControlMain.ImeMode = System.Windows.Forms.ImeMode.On;
            this.reoGridControlMain.LeadHeaderContextMenuStrip = null;
            this.reoGridControlMain.Location = new System.Drawing.Point(5, 53);
            this.reoGridControlMain.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.reoGridControlMain.Name = "reoGridControlMain";
            this.reoGridControlMain.RowHeaderContextMenuStrip = null;
            this.reoGridControlMain.Script = null;
            this.reoGridControlMain.SheetTabContextMenuStrip = null;
            this.reoGridControlMain.SheetTabNewButtonVisible = true;
            this.reoGridControlMain.SheetTabVisible = true;
            this.reoGridControlMain.SheetTabWidth = 140;
            this.reoGridControlMain.ShowScrollEndSpacing = true;
            this.reoGridControlMain.Size = new System.Drawing.Size(1062, 701);
            this.reoGridControlMain.TabIndex = 6;
            this.reoGridControlMain.Text = "reoGridControl1";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.toolStripTop, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.reoGridControlMain, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1072, 757);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // FormImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1072, 757);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FormImport";
            this.Text = "FormImport";
            this.Load += new System.EventHandler(this.FormImport_Load);
            this.toolStripTop.ResumeLayout(false);
            this.toolStripTop.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripTop;
        private System.Windows.Forms.ToolStripButton buttonImport;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripComboBox comboBoxImeMode;
        private unvell.ReoGrid.ReoGridControl reoGridControlMain;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}