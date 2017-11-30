namespace WMS.UI
{
    partial class FormLogin
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
            this.labelPsaaword = new System.Windows.Forms.Label();
            this.labelusername = new System.Windows.Forms.Label();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.buttonClosing = new System.Windows.Forms.Button();
            this.buttonEnter = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelPsaaword
            // 
            this.labelPsaaword.AutoSize = true;
            this.labelPsaaword.Location = new System.Drawing.Point(21, 165);
            this.labelPsaaword.Name = "labelPsaaword";
            this.labelPsaaword.Size = new System.Drawing.Size(52, 15);
            this.labelPsaaword.TabIndex = 7;
            this.labelPsaaword.Text = "密码：";
            // 
            // labelusername
            // 
            this.labelusername.AutoSize = true;
            this.labelusername.Location = new System.Drawing.Point(21, 116);
            this.labelusername.Name = "labelusername";
            this.labelusername.Size = new System.Drawing.Size(67, 15);
            this.labelusername.TabIndex = 6;
            this.labelusername.Text = "用户名：";
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(118, 162);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(193, 25);
            this.textBoxPassword.TabIndex = 5;
            // 
            // textBoxUsername
            // 
            this.textBoxUsername.Location = new System.Drawing.Point(118, 113);
            this.textBoxUsername.Name = "textBoxUsername";
            this.textBoxUsername.Size = new System.Drawing.Size(193, 25);
            this.textBoxUsername.TabIndex = 4;
            // 
            // buttonClosing
            // 
            this.buttonClosing.Location = new System.Drawing.Point(203, 225);
            this.buttonClosing.Name = "buttonClosing";
            this.buttonClosing.Size = new System.Drawing.Size(75, 23);
            this.buttonClosing.TabIndex = 9;
            this.buttonClosing.Text = "退出";
            this.buttonClosing.UseVisualStyleBackColor = true;
            this.buttonClosing.Click += new System.EventHandler(this.buttonClosing_Click);
            // 
            // buttonEnter
            // 
            this.buttonEnter.Location = new System.Drawing.Point(44, 225);
            this.buttonEnter.Name = "buttonEnter";
            this.buttonEnter.Size = new System.Drawing.Size(75, 23);
            this.buttonEnter.TabIndex = 8;
            this.buttonEnter.Text = "确认";
            this.buttonEnter.UseVisualStyleBackColor = true;
            this.buttonEnter.Click += new System.EventHandler(this.buttonEnter_Click);
            // 
            // FormLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(333, 301);
            this.Controls.Add(this.buttonClosing);
            this.Controls.Add(this.buttonEnter);
            this.Controls.Add(this.labelPsaaword);
            this.Controls.Add(this.labelusername);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.textBoxUsername);
            this.Name = "FormLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormLogin";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelPsaaword;
        private System.Windows.Forms.Label labelusername;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.TextBox textBoxUsername;
        private System.Windows.Forms.Button buttonClosing;
        private System.Windows.Forms.Button buttonEnter;
    }
}