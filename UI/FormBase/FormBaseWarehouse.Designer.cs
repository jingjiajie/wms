namespace WMS.UI
{
    partial class FormBaseWarehouse
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBaseWarehouse));
            this.reoGridControlWarehouse = new unvell.ReoGrid.ReoGridControl();
            this.toolStripTop = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonAdd = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // reoGridControlWarehouse
            // 
            this.reoGridControlWarehouse.BackColor = System.Drawing.Color.White;
            this.reoGridControlWarehouse.ColumnHeaderContextMenuStrip = null;
            this.reoGridControlWarehouse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reoGridControlWarehouse.LeadHeaderContextMenuStrip = null;
            this.reoGridControlWarehouse.Location = new System.Drawing.Point(0, 28);
            this.reoGridControlWarehouse.Name = "reoGridControlWarehouse";
            this.reoGridControlWarehouse.RowHeaderContextMenuStrip = null;
            this.reoGridControlWarehouse.Script = null;
            this.reoGridControlWarehouse.SheetTabContextMenuStrip = null;
            this.reoGridControlWarehouse.SheetTabNewButtonVisible = true;
            this.reoGridControlWarehouse.SheetTabVisible = true;
            this.reoGridControlWarehouse.SheetTabWidth = 60;
            this.reoGridControlWarehouse.ShowScrollEndSpacing = true;
            this.reoGridControlWarehouse.Size = new System.Drawing.Size(791, 441);
            this.reoGridControlWarehouse.TabIndex = 5;
            this.reoGridControlWarehouse.Text = "reoGridControl1";
            // 
            // toolStripTop
            // 
            this.toolStripTop.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripTop.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonAdd,
            this.toolStripButtonDelete});
            this.toolStripTop.Location = new System.Drawing.Point(0, 0);
            this.toolStripTop.Name = "toolStripTop";
            this.toolStripTop.Size = new System.Drawing.Size(791, 28);
            this.toolStripTop.TabIndex = 4;
            this.toolStripTop.Text = "toolStrip1";
            // 
            // toolStripButtonAdd
            // 
            this.toolStripButtonAdd.AutoSize = false;
            this.toolStripButtonAdd.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAdd.Image")));
            this.toolStripButtonAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAdd.Name = "toolStripButtonAdd";
            this.toolStripButtonAdd.Size = new System.Drawing.Size(60, 25);
            this.toolStripButtonAdd.Text = "添加";
            this.toolStripButtonAdd.Click += new System.EventHandler(this.toolStripButtonAdd_Click);
            // 
            // toolStripButtonDelete
            // 
            this.toolStripButtonDelete.AutoSize = false;
            this.toolStripButtonDelete.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonDelete.Image")));
            this.toolStripButtonDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDelete.Name = "toolStripButtonDelete";
            this.toolStripButtonDelete.Size = new System.Drawing.Size(60, 25);
            this.toolStripButtonDelete.Text = "删除";
            this.toolStripButtonDelete.Click += new System.EventHandler(this.toolStripButtonDelete_Click);
            // 
            // FormBaseWarehouse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(791, 469);
            this.Controls.Add(this.reoGridControlWarehouse);
            this.Controls.Add(this.toolStripTop);
            this.Name = "FormBaseWarehouse";
            this.Text = "仓库信息";
            this.Load += new System.EventHandler(this.base_Warehouse_Load);
            this.toolStripTop.ResumeLayout(false);
            this.toolStripTop.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private unvell.ReoGrid.ReoGridControl reoGridControlWarehouse;
        private System.Windows.Forms.ToolStrip toolStripTop;
        private System.Windows.Forms.ToolStripButton toolStripButtonAdd;
        private System.Windows.Forms.ToolStripButton toolStripButtonDelete;
    }
}