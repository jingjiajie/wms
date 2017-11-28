namespace WMS.UI.FormBase
{
    partial class FormBaseReceipt
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
            this.tableAdapterManager1 = new WMS.UI.wmsDataSetTableAdapters.TableAdapterManager();
            this.reoGridControl1 = new unvell.ReoGrid.ReoGridControl();
            this.buttonReceipt = new System.Windows.Forms.Button();
            this.buttonReceiptFillBill = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tableAdapterManager1
            // 
            this.tableAdapterManager1.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager1.Connection = null;
            this.tableAdapterManager1.UpdateOrder = WMS.UI.wmsDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            this.tableAdapterManager1.UserTableAdapter = null;
            // 
            // reoGridControl1
            // 
            this.reoGridControl1.BackColor = System.Drawing.Color.White;
            this.reoGridControl1.ColumnHeaderContextMenuStrip = null;
            this.reoGridControl1.LeadHeaderContextMenuStrip = null;
            this.reoGridControl1.Location = new System.Drawing.Point(0, 237);
            this.reoGridControl1.Name = "reoGridControl1";
            this.reoGridControl1.RowHeaderContextMenuStrip = null;
            this.reoGridControl1.Script = null;
            this.reoGridControl1.SheetTabContextMenuStrip = null;
            this.reoGridControl1.SheetTabNewButtonVisible = true;
            this.reoGridControl1.SheetTabVisible = true;
            this.reoGridControl1.SheetTabWidth = 60;
            this.reoGridControl1.ShowScrollEndSpacing = true;
            this.reoGridControl1.Size = new System.Drawing.Size(1296, 651);
            this.reoGridControl1.TabIndex = 0;
            this.reoGridControl1.Text = "reoGridControl1";
            this.reoGridControl1.Click += new System.EventHandler(this.reoGridControl1_Click);
            // 
            // buttonReceipt
            // 
            this.buttonReceipt.Location = new System.Drawing.Point(862, 894);
            this.buttonReceipt.Name = "buttonReceipt";
            this.buttonReceipt.Size = new System.Drawing.Size(123, 53);
            this.buttonReceipt.TabIndex = 1;
            this.buttonReceipt.Text = "收货";
            this.buttonReceipt.UseVisualStyleBackColor = true;
            this.buttonReceipt.Click += new System.EventHandler(this.buttonReceipt_Click);
            // 
            // buttonReceiptFillBill
            // 
            this.buttonReceiptFillBill.Location = new System.Drawing.Point(1006, 894);
            this.buttonReceiptFillBill.Name = "buttonReceiptFillBill";
            this.buttonReceiptFillBill.Size = new System.Drawing.Size(119, 53);
            this.buttonReceiptFillBill.TabIndex = 2;
            this.buttonReceiptFillBill.Text = "整单收获";
            this.buttonReceiptFillBill.UseVisualStyleBackColor = true;
            this.buttonReceiptFillBill.Click += new System.EventHandler(this.button2_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(1157, 894);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(102, 53);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "取消";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // FormBaseReceipt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1292, 983);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonReceiptFillBill);
            this.Controls.Add(this.buttonReceipt);
            this.Controls.Add(this.reoGridControl1);
            this.Name = "FormBaseReceipt";
            this.Text = "FormBaseAceipt";
            this.ResumeLayout(false);

        }

        #endregion

        private wmsDataSetTableAdapters.TableAdapterManager tableAdapterManager1;
        private unvell.ReoGrid.ReoGridControl reoGridControl1;
        private System.Windows.Forms.Button buttonReceipt;
        private System.Windows.Forms.Button buttonReceiptFillBill;
        private System.Windows.Forms.Button buttonCancel;
    }
}