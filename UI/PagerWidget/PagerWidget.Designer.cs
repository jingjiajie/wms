namespace WMS.UI
{
    partial class PagerWidget<TargetClass>
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonGoto = new System.Windows.Forms.Button();
            this.textBoxPage = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonNextPage = new System.Windows.Forms.Button();
            this.buttonPreviousPage = new System.Windows.Forms.Button();
            this.labelPage = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 800F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1276, 93);
            this.tableLayoutPanel1.TabIndex = 0;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.buttonGoto);
            this.panel1.Controls.Add(this.textBoxPage);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.buttonNextPage);
            this.panel1.Controls.Add(this.buttonPreviousPage);
            this.panel1.Controls.Add(this.labelPage);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(380, 16);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 60);
            this.panel1.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("黑体", 10F);
            this.label3.Location = new System.Drawing.Point(455, 16);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 27);
            this.label3.TabIndex = 6;
            this.label3.Text = "第";
            // 
            // buttonGoto
            // 
            this.buttonGoto.Font = new System.Drawing.Font("黑体", 9F);
            this.buttonGoto.Location = new System.Drawing.Point(642, 7);
            this.buttonGoto.Margin = new System.Windows.Forms.Padding(0);
            this.buttonGoto.Name = "buttonGoto";
            this.buttonGoto.Size = new System.Drawing.Size(102, 45);
            this.buttonGoto.TabIndex = 5;
            this.buttonGoto.Text = "跳转";
            this.buttonGoto.UseVisualStyleBackColor = true;
            this.buttonGoto.Click += new System.EventHandler(this.buttonGoto_Click);
            // 
            // textBoxPage
            // 
            this.textBoxPage.Font = new System.Drawing.Font("黑体", 10F);
            this.textBoxPage.Location = new System.Drawing.Point(501, 8);
            this.textBoxPage.Margin = new System.Windows.Forms.Padding(0);
            this.textBoxPage.Name = "textBoxPage";
            this.textBoxPage.Size = new System.Drawing.Size(89, 38);
            this.textBoxPage.TabIndex = 4;
            this.textBoxPage.Text = "1";
            this.textBoxPage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxPage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxPage_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("黑体", 10F);
            this.label2.Location = new System.Drawing.Point(596, 16);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 27);
            this.label2.TabIndex = 3;
            this.label2.Text = "页";
            // 
            // buttonNextPage
            // 
            this.buttonNextPage.Font = new System.Drawing.Font("黑体", 9F);
            this.buttonNextPage.Location = new System.Drawing.Point(265, 7);
            this.buttonNextPage.Margin = new System.Windows.Forms.Padding(0);
            this.buttonNextPage.Name = "buttonNextPage";
            this.buttonNextPage.Size = new System.Drawing.Size(102, 45);
            this.buttonNextPage.TabIndex = 2;
            this.buttonNextPage.Text = "下一页";
            this.buttonNextPage.UseVisualStyleBackColor = true;
            this.buttonNextPage.Click += new System.EventHandler(this.buttonNextPage_Click);
            // 
            // buttonPreviousPage
            // 
            this.buttonPreviousPage.Font = new System.Drawing.Font("黑体", 9F);
            this.buttonPreviousPage.Location = new System.Drawing.Point(27, 7);
            this.buttonPreviousPage.Margin = new System.Windows.Forms.Padding(0);
            this.buttonPreviousPage.Name = "buttonPreviousPage";
            this.buttonPreviousPage.Size = new System.Drawing.Size(102, 45);
            this.buttonPreviousPage.TabIndex = 1;
            this.buttonPreviousPage.Text = "上一页";
            this.buttonPreviousPage.UseVisualStyleBackColor = true;
            this.buttonPreviousPage.Click += new System.EventHandler(this.buttonPreviousPage_Click);
            // 
            // labelPage
            // 
            this.labelPage.AutoSize = true;
            this.labelPage.Font = new System.Drawing.Font("黑体", 10F);
            this.labelPage.Location = new System.Drawing.Point(135, 15);
            this.labelPage.Margin = new System.Windows.Forms.Padding(0);
            this.labelPage.Name = "labelPage";
            this.labelPage.Size = new System.Drawing.Size(82, 27);
            this.labelPage.TabIndex = 0;
            this.labelPage.Text = "1/1页";
            this.labelPage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PagerWidget
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.MenuBar;
            this.ClientSize = new System.Drawing.Size(1276, 93);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PagerWidget";
            this.Text = "PagerWidget";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonGoto;
        private System.Windows.Forms.TextBox textBoxPage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonNextPage;
        private System.Windows.Forms.Button buttonPreviousPage;
        private System.Windows.Forms.Label labelPage;
    }
}