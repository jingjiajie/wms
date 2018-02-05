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
            this.reoGridControlMain = new unvell.ReoGrid.ReoGridControl();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelTop = new System.Windows.Forms.TableLayoutPanel();
            this.checkBoxLockEnglish = new System.Windows.Forms.CheckBox();
            this.buttonImport = new System.Windows.Forms.Button();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // reoGridControlMain
            // 
            this.reoGridControlMain.BackColor = System.Drawing.Color.White;
            this.reoGridControlMain.ColumnHeaderContextMenuStrip = null;
            this.reoGridControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reoGridControlMain.ImeMode = System.Windows.Forms.ImeMode.On;
            this.reoGridControlMain.LeadHeaderContextMenuStrip = null;
            this.reoGridControlMain.Location = new System.Drawing.Point(5, 36);
            this.reoGridControlMain.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.reoGridControlMain.Name = "reoGridControlMain";
            this.reoGridControlMain.RowHeaderContextMenuStrip = null;
            this.reoGridControlMain.Script = null;
            this.reoGridControlMain.SheetTabContextMenuStrip = null;
            this.reoGridControlMain.SheetTabNewButtonVisible = true;
            this.reoGridControlMain.SheetTabVisible = true;
            this.reoGridControlMain.SheetTabWidth = 60;
            this.reoGridControlMain.ShowScrollEndSpacing = true;
            this.reoGridControlMain.Size = new System.Drawing.Size(988, 658);
            this.reoGridControlMain.TabIndex = 4;
            this.reoGridControlMain.Text = "reoGridControl1";
            this.reoGridControlMain.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.reoGridControlMain_PreviewKeyDown);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.reoGridControlMain, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanelTop, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(998, 697);
            this.tableLayoutPanel2.TabIndex = 7;
            // 
            // tableLayoutPanelTop
            // 
            this.tableLayoutPanelTop.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tableLayoutPanelTop.ColumnCount = 3;
            this.tableLayoutPanelTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanelTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 230F));
            this.tableLayoutPanelTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelTop.Controls.Add(this.checkBoxLockEnglish, 1, 0);
            this.tableLayoutPanelTop.Controls.Add(this.buttonImport, 0, 0);
            this.tableLayoutPanelTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelTop.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelTop.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanelTop.Name = "tableLayoutPanelTop";
            this.tableLayoutPanelTop.RowCount = 1;
            this.tableLayoutPanelTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelTop.Size = new System.Drawing.Size(998, 33);
            this.tableLayoutPanelTop.TabIndex = 6;
            this.tableLayoutPanelTop.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanelTop_Paint);
            // 
            // checkBoxLockEnglish
            // 
            this.checkBoxLockEnglish.AutoSize = true;
            this.checkBoxLockEnglish.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.checkBoxLockEnglish.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkBoxLockEnglish.Location = new System.Drawing.Point(103, 1);
            this.checkBoxLockEnglish.Margin = new System.Windows.Forms.Padding(3, 1, 0, 0);
            this.checkBoxLockEnglish.Name = "checkBoxLockEnglish";
            this.checkBoxLockEnglish.Size = new System.Drawing.Size(227, 32);
            this.checkBoxLockEnglish.TabIndex = 5;
            this.checkBoxLockEnglish.Text = "锁定英文输入（Ctrl+Alt切换）";
            this.checkBoxLockEnglish.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBoxLockEnglish.UseVisualStyleBackColor = false;
            this.checkBoxLockEnglish.CheckedChanged += new System.EventHandler(this.checkBoxLockEnglish_CheckedChanged);
            // 
            // buttonImport
            // 
            this.buttonImport.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.buttonImport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonImport.FlatAppearance.BorderSize = 0;
            this.buttonImport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonImport.Image = global::WMS.UI.Properties.Resources.add;
            this.buttonImport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonImport.Location = new System.Drawing.Point(6, 3);
            this.buttonImport.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
            this.buttonImport.Name = "buttonImport";
            this.buttonImport.Size = new System.Drawing.Size(91, 27);
            this.buttonImport.TabIndex = 6;
            this.buttonImport.Text = "  批量导入";
            this.buttonImport.UseVisualStyleBackColor = false;
            this.buttonImport.Click += new System.EventHandler(this.buttonImport_Click);
            // 
            // StandardImportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 27F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(998, 697);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Font = new System.Drawing.Font("黑体", 10F);
            this.Name = "StandardImportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "导入用户信息";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.StandardImportForm_FormClosed);
            this.Load += new System.EventHandler(this.StandardImportForm_Load);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanelTop.ResumeLayout(false);
            this.tableLayoutPanelTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private unvell.ReoGrid.ReoGridControl reoGridControlMain;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelTop;
        private System.Windows.Forms.CheckBox checkBoxLockEnglish;
        private System.Windows.Forms.Button buttonImport;
    }
}