namespace WMS.UI
{
    partial class StandardImportForm<TargetClass>
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
            this.toolStripTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripTop
            // 
            this.toolStripTop.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.toolStripTop.BackgroundImage = global::WMS.UI.Properties.Resources.bottonW_q;
            this.toolStripTop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStripTop.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripTop.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonImport,
            this.toolStripSeparator1,
            this.comboBoxImeMode});
            this.toolStripTop.Location = new System.Drawing.Point(0, 0);
            this.toolStripTop.Name = "toolStripTop";
            this.toolStripTop.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStripTop.Size = new System.Drawing.Size(998, 39);
            this.toolStripTop.TabIndex = 3;
            this.toolStripTop.Text = "toolStrip1";
            // 
            // buttonImport
            // 
            this.buttonImport.Image = global::WMS.UI.Properties.Resources.add;
            this.buttonImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonImport.Name = "buttonImport";
            this.buttonImport.Size = new System.Drawing.Size(134, 36);
            this.buttonImport.Text = "确定导入";
            this.buttonImport.Click += new System.EventHandler(this.buttonImport_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
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
            this.comboBoxImeMode.Size = new System.Drawing.Size(121, 39);
            this.comboBoxImeMode.SelectedIndexChanged += new System.EventHandler(this.comboBoxImeMode_SelectedIndexChanged);
            // 
            // reoGridControlMain
            // 
            this.reoGridControlMain.BackColor = System.Drawing.Color.White;
            this.reoGridControlMain.ColumnHeaderContextMenuStrip = null;
            this.reoGridControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reoGridControlMain.ImeMode = System.Windows.Forms.ImeMode.On;
            this.reoGridControlMain.LeadHeaderContextMenuStrip = null;
            this.reoGridControlMain.Location = new System.Drawing.Point(0, 39);
            this.reoGridControlMain.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.reoGridControlMain.Name = "reoGridControlMain";
            this.reoGridControlMain.RowHeaderContextMenuStrip = null;
            this.reoGridControlMain.Script = null;
            this.reoGridControlMain.SheetTabContextMenuStrip = null;
            this.reoGridControlMain.SheetTabNewButtonVisible = true;
            this.reoGridControlMain.SheetTabVisible = true;
            this.reoGridControlMain.SheetTabWidth = 140;
            this.reoGridControlMain.ShowScrollEndSpacing = true;
            this.reoGridControlMain.Size = new System.Drawing.Size(998, 658);
            this.reoGridControlMain.TabIndex = 4;
            this.reoGridControlMain.Text = "reoGridControl1";
            this.reoGridControlMain.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.reoGridControlMain_PreviewKeyDown);
            // 
            // StandardImportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 27F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(998, 697);
            this.Controls.Add(this.reoGridControlMain);
            this.Controls.Add(this.toolStripTop);
            this.Font = new System.Drawing.Font("黑体", 10F);
            this.Name = "StandardImportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "导入用户信息";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.StandardImportForm_FormClosed);
            this.Load += new System.EventHandler(this.StandardImportForm_Load);
            this.toolStripTop.ResumeLayout(false);
            this.toolStripTop.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripTop;
        private System.Windows.Forms.ToolStripButton buttonImport;
        private unvell.ReoGrid.ReoGridControl reoGridControlMain;
        private System.Windows.Forms.ToolStripComboBox comboBoxImeMode;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}