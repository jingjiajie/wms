namespace WMS.UI
{
    partial class FormSubmissionChooseExcelType
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSubmissionChooseExcelType));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonPass = new System.Windows.Forms.Button();
            this.buttonAll = new System.Windows.Forms.Button();
            this.buttonNoPass = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.buttonPass, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonAll, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonNoPass, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(578, 157);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // buttonPass
            // 
            this.buttonPass.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonPass.BackgroundImage")));
            this.buttonPass.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonPass.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonPass.FlatAppearance.BorderSize = 0;
            this.buttonPass.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPass.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.buttonPass.Location = new System.Drawing.Point(4, 4);
            this.buttonPass.Margin = new System.Windows.Forms.Padding(4);
            this.buttonPass.Name = "buttonPass";
            this.buttonPass.Size = new System.Drawing.Size(184, 149);
            this.buttonPass.TabIndex = 0;
            this.buttonPass.Text = "合格条目";
            this.buttonPass.UseVisualStyleBackColor = true;
            this.buttonPass.Click += new System.EventHandler(this.buttonPass_Click);
            // 
            // buttonAll
            // 
            this.buttonAll.BackgroundImage = global::WMS.UI.Properties.Resources.bottonB2_q;
            this.buttonAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonAll.FlatAppearance.BorderSize = 0;
            this.buttonAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAll.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.buttonAll.Location = new System.Drawing.Point(388, 4);
            this.buttonAll.Margin = new System.Windows.Forms.Padding(4);
            this.buttonAll.Name = "buttonAll";
            this.buttonAll.Size = new System.Drawing.Size(186, 149);
            this.buttonAll.TabIndex = 3;
            this.buttonAll.Text = "所有条目";
            this.buttonAll.UseVisualStyleBackColor = true;
            this.buttonAll.Click += new System.EventHandler(this.buttonAll_Click);
            // 
            // buttonNoPass
            // 
            this.buttonNoPass.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonNoPass.BackgroundImage")));
            this.buttonNoPass.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonNoPass.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonNoPass.FlatAppearance.BorderSize = 0;
            this.buttonNoPass.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonNoPass.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.buttonNoPass.Location = new System.Drawing.Point(196, 4);
            this.buttonNoPass.Margin = new System.Windows.Forms.Padding(4);
            this.buttonNoPass.Name = "buttonNoPass";
            this.buttonNoPass.Size = new System.Drawing.Size(184, 149);
            this.buttonNoPass.TabIndex = 2;
            this.buttonNoPass.Text = "不合格条目";
            this.buttonNoPass.UseVisualStyleBackColor = true;
            this.buttonNoPass.Click += new System.EventHandler(this.buttonNoPass_Click);
            // 
            // FormSubmissionChooseExcelType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 157);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FormSubmissionChooseExcelType";
            this.Text = "FormSubmissionChooseExcelType";
            this.Load += new System.EventHandler(this.FormSubmissionChooseExcelType_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonPass;
        private System.Windows.Forms.Button buttonAll;
        private System.Windows.Forms.Button buttonNoPass;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}