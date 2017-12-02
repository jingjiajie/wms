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
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelPsaaword
            // 
            this.labelPsaaword.AutoSize = true;
            this.labelPsaaword.Font = new System.Drawing.Font("微软雅黑", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelPsaaword.ForeColor = System.Drawing.Color.White;
            this.labelPsaaword.Location = new System.Drawing.Point(218, 271);
            this.labelPsaaword.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPsaaword.Name = "labelPsaaword";
            this.labelPsaaword.Size = new System.Drawing.Size(96, 36);
            this.labelPsaaword.TabIndex = 7;
            this.labelPsaaword.Text = "密码：";
            this.labelPsaaword.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormLogin_MouseDown);
            this.labelPsaaword.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormLogin_MouseMove);
            this.labelPsaaword.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormLogin_MouseUp);
            // 
            // labelusername
            // 
            this.labelusername.AutoSize = true;
            this.labelusername.Font = new System.Drawing.Font("微软雅黑", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelusername.ForeColor = System.Drawing.Color.White;
            this.labelusername.Location = new System.Drawing.Point(218, 193);
            this.labelusername.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelusername.Name = "labelusername";
            this.labelusername.Size = new System.Drawing.Size(123, 36);
            this.labelusername.TabIndex = 6;
            this.labelusername.Text = "用户名：";
            this.labelusername.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormLogin_MouseDown);
            this.labelusername.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormLogin_MouseMove);
            this.labelusername.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormLogin_MouseUp);
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.textBoxPassword.Location = new System.Drawing.Point(363, 266);
            this.textBoxPassword.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(288, 43);
            this.textBoxPassword.TabIndex = 2;
            this.textBoxPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxPassword_KeyPress);
            // 
            // textBoxUsername
            // 
            this.textBoxUsername.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.textBoxUsername.Location = new System.Drawing.Point(363, 188);
            this.textBoxUsername.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxUsername.Name = "textBoxUsername";
            this.textBoxUsername.Size = new System.Drawing.Size(288, 43);
            this.textBoxUsername.TabIndex = 1;
            this.textBoxUsername.TextChanged += new System.EventHandler(this.textBoxUsername_TextChanged);
            this.textBoxUsername.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxUsername_KeyPress);
            // 
            // buttonClosing
            // 
            this.buttonClosing.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.buttonClosing.Location = new System.Drawing.Point(474, 344);
            this.buttonClosing.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonClosing.Name = "buttonClosing";
            this.buttonClosing.Size = new System.Drawing.Size(175, 57);
            this.buttonClosing.TabIndex = 4;
            this.buttonClosing.Text = "退出";
            this.buttonClosing.UseVisualStyleBackColor = true;
            this.buttonClosing.Click += new System.EventHandler(this.buttonClosing_Click);
            // 
            // buttonEnter
            // 
            this.buttonEnter.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.buttonEnter.Location = new System.Drawing.Point(236, 344);
            this.buttonEnter.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonEnter.Name = "buttonEnter";
            this.buttonEnter.Size = new System.Drawing.Size(175, 57);
            this.buttonEnter.TabIndex = 3;
            this.buttonEnter.Text = "确认";
            this.buttonEnter.UseVisualStyleBackColor = true;
            this.buttonEnter.Click += new System.EventHandler(this.buttonEnter_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 20F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(104, 60);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(716, 72);
            this.label1.TabIndex = 10;
            this.label1.Text = "安途丰达WMS物流管理系统";
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormLogin_MouseDown);
            this.label1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormLogin_MouseMove);
            this.label1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormLogin_MouseUp);
            // 
            // FormLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.RoyalBlue;
            this.ClientSize = new System.Drawing.Size(914, 481);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonClosing);
            this.Controls.Add(this.buttonEnter);
            this.Controls.Add(this.labelPsaaword);
            this.Controls.Add(this.labelusername);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.textBoxUsername);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FormLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormLogin";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormLogin_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormLogin_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormLogin_MouseUp);
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
        private System.Windows.Forms.Label label1;
    }
}