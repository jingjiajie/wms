namespace WMS.UI
{
    partial class FormBaseUserAlter
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
            this.labelUsername = new System.Windows.Forms.Label();
            this.labelPassword = new System.Windows.Forms.Label();
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.buttonEnter = new System.Windows.Forms.Button();
            this.buttonClosing = new System.Windows.Forms.Button();
            this.radioButtonDelivery = new System.Windows.Forms.RadioButton();
            this.radioButtonReceive = new System.Windows.Forms.RadioButton();
            this.radioButtonStork = new System.Windows.Forms.RadioButton();
            this.radioButtonBase = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // labelUsername
            // 
            this.labelUsername.AutoSize = true;
            this.labelUsername.Location = new System.Drawing.Point(114, 93);
            this.labelUsername.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelUsername.Name = "labelUsername";
            this.labelUsername.Size = new System.Drawing.Size(106, 24);
            this.labelUsername.TabIndex = 0;
            this.labelUsername.Text = "用户名：";
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(136, 179);
            this.labelPassword.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(82, 24);
            this.labelPassword.TabIndex = 1;
            this.labelPassword.Text = "密码：";
            // 
            // textBoxUsername
            // 
            this.textBoxUsername.Location = new System.Drawing.Point(268, 93);
            this.textBoxUsername.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxUsername.Name = "textBoxUsername";
            this.textBoxUsername.Size = new System.Drawing.Size(342, 35);
            this.textBoxUsername.TabIndex = 2;
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(268, 163);
            this.textBoxPassword.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(342, 35);
            this.textBoxPassword.TabIndex = 3;
            // 
            // buttonEnter
            // 
            this.buttonEnter.Location = new System.Drawing.Point(158, 390);
            this.buttonEnter.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonEnter.Name = "buttonEnter";
            this.buttonEnter.Size = new System.Drawing.Size(112, 37);
            this.buttonEnter.TabIndex = 4;
            this.buttonEnter.Text = "确认";
            this.buttonEnter.UseVisualStyleBackColor = true;
            this.buttonEnter.Click += new System.EventHandler(this.buttonEnter_Click);
            // 
            // buttonClosing
            // 
            this.buttonClosing.Location = new System.Drawing.Point(414, 390);
            this.buttonClosing.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonClosing.Name = "buttonClosing";
            this.buttonClosing.Size = new System.Drawing.Size(112, 37);
            this.buttonClosing.TabIndex = 5;
            this.buttonClosing.Text = "取消";
            this.buttonClosing.UseVisualStyleBackColor = true;
            // 
            // radioButtonDelivery
            // 
            this.radioButtonDelivery.AutoSize = true;
            this.radioButtonDelivery.Location = new System.Drawing.Point(396, 296);
            this.radioButtonDelivery.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioButtonDelivery.Name = "radioButtonDelivery";
            this.radioButtonDelivery.Size = new System.Drawing.Size(113, 28);
            this.radioButtonDelivery.TabIndex = 14;
            this.radioButtonDelivery.TabStop = true;
            this.radioButtonDelivery.Text = "发货员";
            this.radioButtonDelivery.UseVisualStyleBackColor = true;
            // 
            // radioButtonReceive
            // 
            this.radioButtonReceive.AutoSize = true;
            this.radioButtonReceive.Location = new System.Drawing.Point(160, 296);
            this.radioButtonReceive.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioButtonReceive.Name = "radioButtonReceive";
            this.radioButtonReceive.Size = new System.Drawing.Size(113, 28);
            this.radioButtonReceive.TabIndex = 13;
            this.radioButtonReceive.TabStop = true;
            this.radioButtonReceive.Text = "收货员";
            this.radioButtonReceive.UseVisualStyleBackColor = true;
            // 
            // radioButtonStork
            // 
            this.radioButtonStork.AutoSize = true;
            this.radioButtonStork.Location = new System.Drawing.Point(396, 254);
            this.radioButtonStork.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioButtonStork.Name = "radioButtonStork";
            this.radioButtonStork.Size = new System.Drawing.Size(113, 28);
            this.radioButtonStork.TabIndex = 12;
            this.radioButtonStork.TabStop = true;
            this.radioButtonStork.Text = "结算员";
            this.radioButtonStork.UseVisualStyleBackColor = true;
            // 
            // radioButtonBase
            // 
            this.radioButtonBase.AutoSize = true;
            this.radioButtonBase.Location = new System.Drawing.Point(160, 254);
            this.radioButtonBase.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioButtonBase.Name = "radioButtonBase";
            this.radioButtonBase.Size = new System.Drawing.Size(113, 28);
            this.radioButtonBase.TabIndex = 11;
            this.radioButtonBase.TabStop = true;
            this.radioButtonBase.Text = "管理员";
            this.radioButtonBase.UseVisualStyleBackColor = true;
            // 
            // FormBaseUserAlter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 510);
            this.Controls.Add(this.radioButtonDelivery);
            this.Controls.Add(this.radioButtonReceive);
            this.Controls.Add(this.radioButtonStork);
            this.Controls.Add(this.radioButtonBase);
            this.Controls.Add(this.buttonClosing);
            this.Controls.Add(this.buttonEnter);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.textBoxUsername);
            this.Controls.Add(this.labelPassword);
            this.Controls.Add(this.labelUsername);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FormBaseUserAlter";
            this.Text = "修改用户";
            this.Load += new System.EventHandler(this.FormBaseUserAlter_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelUsername;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.TextBox textBoxUsername;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Button buttonEnter;
        private System.Windows.Forms.Button buttonClosing;
        private System.Windows.Forms.RadioButton radioButtonDelivery;
        private System.Windows.Forms.RadioButton radioButtonReceive;
        private System.Windows.Forms.RadioButton radioButtonStork;
        private System.Windows.Forms.RadioButton radioButtonBase;
    }
}