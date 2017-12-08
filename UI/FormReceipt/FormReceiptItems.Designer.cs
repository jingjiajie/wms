namespace WMS.UI
{
    partial class FormReceiptItems
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
            this.reoGridControlReceiptItems = new unvell.ReoGrid.ReoGridControl();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.labelStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelProperties = new System.Windows.Forms.TableLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxProjectID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxID = new System.Windows.Forms.TextBox();
            this.textBoxWarehouse = new System.Windows.Forms.TextBox();
            this.textBoxSupplierID = new System.Windows.Forms.TextBox();
            this.textBoxNo = new System.Windows.Forms.TextBox();
            this.textBoxState = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonUnfinish = new System.Windows.Forms.Button();
            this.buttonModify = new System.Windows.Forms.Button();
            this.buttonItemCheck = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.statusStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanelProperties.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // reoGridControlReceiptItems
            // 
            this.reoGridControlReceiptItems.BackColor = System.Drawing.Color.White;
            this.reoGridControlReceiptItems.ColumnHeaderContextMenuStrip = null;
            this.reoGridControlReceiptItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reoGridControlReceiptItems.LeadHeaderContextMenuStrip = null;
            this.reoGridControlReceiptItems.Location = new System.Drawing.Point(0, 0);
            this.reoGridControlReceiptItems.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.reoGridControlReceiptItems.Name = "reoGridControlReceiptItems";
            this.reoGridControlReceiptItems.Readonly = true;
            this.reoGridControlReceiptItems.RowHeaderContextMenuStrip = null;
            this.reoGridControlReceiptItems.Script = null;
            this.reoGridControlReceiptItems.SheetTabContextMenuStrip = null;
            this.reoGridControlReceiptItems.SheetTabNewButtonVisible = true;
            this.reoGridControlReceiptItems.SheetTabVisible = true;
            this.reoGridControlReceiptItems.SheetTabWidth = 140;
            this.reoGridControlReceiptItems.ShowScrollEndSpacing = true;
            this.reoGridControlReceiptItems.Size = new System.Drawing.Size(1200, 645);
            this.reoGridControlReceiptItems.TabIndex = 9;
            this.reoGridControlReceiptItems.Text = "reoGridControlRecieptItems";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.labelStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 815);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 16, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1206, 36);
            this.statusStrip1.TabIndex = 12;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(86, 31);
            this.toolStripStatusLabel1.Text = "状态：";
            // 
            // labelStatus
            // 
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(182, 31);
            this.labelStatus.Text = "查看收货单条目";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1206, 851);
            this.tableLayoutPanel1.TabIndex = 13;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1200, 194);
            this.panel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 400F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanelProperties, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel3, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1200, 194);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // tableLayoutPanelProperties
            // 
            this.tableLayoutPanelProperties.ColumnCount = 6;
            this.tableLayoutPanelProperties.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanelProperties.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.04762F));
            this.tableLayoutPanelProperties.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanelProperties.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.04762F));
            this.tableLayoutPanelProperties.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanelProperties.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.04762F));
            this.tableLayoutPanelProperties.Controls.Add(this.label6, 4, 1);
            this.tableLayoutPanelProperties.Controls.Add(this.label5, 2, 1);
            this.tableLayoutPanelProperties.Controls.Add(this.label4, 0, 1);
            this.tableLayoutPanelProperties.Controls.Add(this.label3, 4, 0);
            this.tableLayoutPanelProperties.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanelProperties.Controls.Add(this.textBoxProjectID, 3, 0);
            this.tableLayoutPanelProperties.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanelProperties.Controls.Add(this.textBoxID, 1, 0);
            this.tableLayoutPanelProperties.Controls.Add(this.textBoxWarehouse, 5, 0);
            this.tableLayoutPanelProperties.Controls.Add(this.textBoxSupplierID, 1, 1);
            this.tableLayoutPanelProperties.Controls.Add(this.textBoxNo, 3, 1);
            this.tableLayoutPanelProperties.Controls.Add(this.textBoxState, 5, 1);
            this.tableLayoutPanelProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelProperties.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanelProperties.Name = "tableLayoutPanelProperties";
            this.tableLayoutPanelProperties.RowCount = 2;
            this.tableLayoutPanelProperties.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelProperties.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelProperties.Size = new System.Drawing.Size(794, 188);
            this.tableLayoutPanelProperties.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(531, 94);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 24);
            this.label6.TabIndex = 12;
            this.label6.Text = "状态";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(267, 94);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 24);
            this.label5.TabIndex = 11;
            this.label5.Text = "单号";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 24);
            this.label4.TabIndex = 10;
            this.label4.Text = "供应商ID";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(531, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 24);
            this.label3.TabIndex = 9;
            this.label3.Text = "仓库ID";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 24);
            this.label1.TabIndex = 8;
            this.label1.Text = "收货单ID";
            // 
            // textBoxProjectID
            // 
            this.textBoxProjectID.Location = new System.Drawing.Point(380, 3);
            this.textBoxProjectID.Name = "textBoxProjectID";
            this.textBoxProjectID.ReadOnly = true;
            this.textBoxProjectID.Size = new System.Drawing.Size(100, 35);
            this.textBoxProjectID.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(267, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 24);
            this.label2.TabIndex = 6;
            this.label2.Text = "项目ID";
            // 
            // textBoxID
            // 
            this.textBoxID.Location = new System.Drawing.Point(116, 3);
            this.textBoxID.Name = "textBoxID";
            this.textBoxID.ReadOnly = true;
            this.textBoxID.Size = new System.Drawing.Size(100, 35);
            this.textBoxID.TabIndex = 2;
            // 
            // textBoxWarehouse
            // 
            this.textBoxWarehouse.Location = new System.Drawing.Point(644, 3);
            this.textBoxWarehouse.Name = "textBoxWarehouse";
            this.textBoxWarehouse.ReadOnly = true;
            this.textBoxWarehouse.Size = new System.Drawing.Size(100, 35);
            this.textBoxWarehouse.TabIndex = 1;
            // 
            // textBoxSupplierID
            // 
            this.textBoxSupplierID.Location = new System.Drawing.Point(116, 97);
            this.textBoxSupplierID.Name = "textBoxSupplierID";
            this.textBoxSupplierID.ReadOnly = true;
            this.textBoxSupplierID.Size = new System.Drawing.Size(100, 35);
            this.textBoxSupplierID.TabIndex = 3;
            // 
            // textBoxNo
            // 
            this.textBoxNo.Location = new System.Drawing.Point(380, 97);
            this.textBoxNo.Name = "textBoxNo";
            this.textBoxNo.ReadOnly = true;
            this.textBoxNo.Size = new System.Drawing.Size(100, 35);
            this.textBoxNo.TabIndex = 4;
            // 
            // textBoxState
            // 
            this.textBoxState.Location = new System.Drawing.Point(644, 97);
            this.textBoxState.Name = "textBoxState";
            this.textBoxState.ReadOnly = true;
            this.textBoxState.Size = new System.Drawing.Size(100, 35);
            this.textBoxState.TabIndex = 5;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.tableLayoutPanel3);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(803, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(394, 188);
            this.panel3.TabIndex = 1;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 4;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 165F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.buttonAdd, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.buttonUnfinish, 2, 2);
            this.tableLayoutPanel3.Controls.Add(this.buttonModify, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.buttonItemCheck, 2, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 4;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(394, 188);
            this.tableLayoutPanel3.TabIndex = 4;
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(42, 42);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(159, 49);
            this.buttonAdd.TabIndex = 0;
            this.buttonAdd.Text = "添加零件条目";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonUnfinish
            // 
            this.buttonUnfinish.Location = new System.Drawing.Point(207, 97);
            this.buttonUnfinish.Name = "buttonUnfinish";
            this.buttonUnfinish.Size = new System.Drawing.Size(144, 49);
            this.buttonUnfinish.TabIndex = 3;
            this.buttonUnfinish.Text = "任务未完成";
            this.buttonUnfinish.UseVisualStyleBackColor = true;
            // 
            // buttonModify
            // 
            this.buttonModify.Location = new System.Drawing.Point(42, 97);
            this.buttonModify.Name = "buttonModify";
            this.buttonModify.Size = new System.Drawing.Size(159, 49);
            this.buttonModify.TabIndex = 1;
            this.buttonModify.Text = "修改零件条目";
            this.buttonModify.UseVisualStyleBackColor = true;
            this.buttonModify.Click += new System.EventHandler(this.buttonModify_Click);
            // 
            // buttonItemCheck
            // 
            this.buttonItemCheck.Location = new System.Drawing.Point(207, 42);
            this.buttonItemCheck.Name = "buttonItemCheck";
            this.buttonItemCheck.Size = new System.Drawing.Size(144, 49);
            this.buttonItemCheck.TabIndex = 2;
            this.buttonItemCheck.Text = "条目送检";
            this.buttonItemCheck.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.reoGridControlReceiptItems);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 203);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1200, 645);
            this.panel2.TabIndex = 1;
            // 
            // FormReceiptItems
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1206, 851);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FormReceiptItems";
            this.Text = "FormReceiptItemsArrival";
            this.Load += new System.EventHandler(this.FormReceiptArrivalItems_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanelProperties.ResumeLayout(false);
            this.tableLayoutPanelProperties.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private unvell.ReoGrid.ReoGridControl reoGridControlReceiptItems;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel labelStatus;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelProperties;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonUnfinish;
        private System.Windows.Forms.Button buttonModify;
        private System.Windows.Forms.Button buttonItemCheck;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxProjectID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxID;
        private System.Windows.Forms.TextBox textBoxWarehouse;
        private System.Windows.Forms.TextBox textBoxSupplierID;
        private System.Windows.Forms.TextBox textBoxNo;
        private System.Windows.Forms.TextBox textBoxState;
    }
}