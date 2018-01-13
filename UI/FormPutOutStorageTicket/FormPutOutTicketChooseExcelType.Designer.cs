namespace WMS.UI
{
    partial class FormPutOutTicketChooseExcelType
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
            this.buttonCover = new System.Windows.Forms.Button();
            this.buttonNormal = new System.Windows.Forms.Button();
            this.buttonMoBiSi = new System.Windows.Forms.Button();
            this.buttonZhongDu = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonCover
            // 
            this.buttonCover.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonCover.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.buttonCover.Location = new System.Drawing.Point(147, 4);
            this.buttonCover.Margin = new System.Windows.Forms.Padding(4);
            this.buttonCover.Name = "buttonCover";
            this.buttonCover.Size = new System.Drawing.Size(135, 91);
            this.buttonCover.TabIndex = 1;
            this.buttonCover.Text = "顺义套单";
            this.buttonCover.UseVisualStyleBackColor = true;
            this.buttonCover.Click += new System.EventHandler(this.buttonInner_Click);
            // 
            // buttonNormal
            // 
            this.buttonNormal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonNormal.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.buttonNormal.Location = new System.Drawing.Point(4, 4);
            this.buttonNormal.Margin = new System.Windows.Forms.Padding(4);
            this.buttonNormal.Name = "buttonNormal";
            this.buttonNormal.Size = new System.Drawing.Size(135, 91);
            this.buttonNormal.TabIndex = 0;
            this.buttonNormal.Text = "普通出库单";
            this.buttonNormal.UseVisualStyleBackColor = true;
            this.buttonNormal.Click += new System.EventHandler(this.buttonCover_Click);
            // 
            // buttonMoBiSi
            // 
            this.buttonMoBiSi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonMoBiSi.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.buttonMoBiSi.Location = new System.Drawing.Point(290, 4);
            this.buttonMoBiSi.Margin = new System.Windows.Forms.Padding(4);
            this.buttonMoBiSi.Name = "buttonMoBiSi";
            this.buttonMoBiSi.Size = new System.Drawing.Size(135, 91);
            this.buttonMoBiSi.TabIndex = 2;
            this.buttonMoBiSi.Text = "摩比斯出库单";
            this.buttonMoBiSi.UseVisualStyleBackColor = true;
            this.buttonMoBiSi.Click += new System.EventHandler(this.buttonMoBiSi_Click);
            // 
            // buttonZhongDu
            // 
            this.buttonZhongDu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonZhongDu.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.buttonZhongDu.Location = new System.Drawing.Point(433, 4);
            this.buttonZhongDu.Margin = new System.Windows.Forms.Padding(4);
            this.buttonZhongDu.Name = "buttonZhongDu";
            this.buttonZhongDu.Size = new System.Drawing.Size(137, 91);
            this.buttonZhongDu.TabIndex = 3;
            this.buttonZhongDu.Text = "中都出库单";
            this.buttonZhongDu.UseVisualStyleBackColor = true;
            this.buttonZhongDu.Click += new System.EventHandler(this.buttonZhongDu_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.buttonNormal, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonZhongDu, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonCover, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonMoBiSi, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(574, 99);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // FormPutOutTicketChooseExcelType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 35F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 99);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormPutOutTicketChooseExcelType";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "选择报表类型";
            this.Load += new System.EventHandler(this.FormPutOutTicketChooseExcelType_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonCover;
        private System.Windows.Forms.Button buttonNormal;
        private System.Windows.Forms.Button buttonMoBiSi;
        private System.Windows.Forms.Button buttonZhongDu;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}