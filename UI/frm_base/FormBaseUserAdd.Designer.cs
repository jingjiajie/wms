namespace WMS.UI.frm_base
{
    partial class FormBaseUserAdd
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
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.labelusername = new System.Windows.Forms.Label();
            this.labelPsaaword = new System.Windows.Forms.Label();
            this.labelNotice = new System.Windows.Forms.Label();
            this.buttonEnter = new System.Windows.Forms.Button();
            this.buttonClosing = new System.Windows.Forms.Button();
            this.radioButtonBase = new System.Windows.Forms.RadioButton();
            this.radioButtonStork = new System.Windows.Forms.RadioButton();
            this.radioButtonReceive = new System.Windows.Forms.RadioButton();
            this.radioButtonDelivery = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // textBoxUsername
            // 
            this.textBoxUsername.Location = new System.Drawing.Point(167, 110);
            this.textBoxUsername.Name = "textBoxUsername";
            this.textBoxUsername.Size = new System.Drawing.Size(193, 25);
            this.textBoxUsername.TabIndex = 0;
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(167, 159);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(193, 25);
            this.textBoxPassword.TabIndex = 1;
            // 
            // labelusername
            // 
            this.labelusername.AutoSize = true;
            this.labelusername.Location = new System.Drawing.Point(70, 113);
            this.labelusername.Name = "labelusername";
            this.labelusername.Size = new System.Drawing.Size(67, 15);
            this.labelusername.TabIndex = 2;
            this.labelusername.Text = "用户名：";
            // 
            // labelPsaaword
            // 
            this.labelPsaaword.AutoSize = true;
            this.labelPsaaword.Location = new System.Drawing.Point(70, 162);
            this.labelPsaaword.Name = "labelPsaaword";
            this.labelPsaaword.Size = new System.Drawing.Size(52, 15);
            this.labelPsaaword.TabIndex = 3;
            this.labelPsaaword.Text = "密码：";
            // 
            // labelNotice
            // 
            this.labelNotice.AutoSize = true;
            this.labelNotice.Location = new System.Drawing.Point(43, 53);
            this.labelNotice.Name = "labelNotice";
            this.labelNotice.Size = new System.Drawing.Size(142, 15);
            this.labelNotice.TabIndex = 4;
            this.labelNotice.Text = "请输入用户名和密码";
            // 
            // buttonEnter
            // 
            this.buttonEnter.Location = new System.Drawing.Point(73, 291);
            this.buttonEnter.Name = "buttonEnter";
            this.buttonEnter.Size = new System.Drawing.Size(75, 23);
            this.buttonEnter.TabIndex = 5;
            this.buttonEnter.Text = "确认";
            this.buttonEnter.UseVisualStyleBackColor = true;
            this.buttonEnter.Click += new System.EventHandler(this.buttonEnter_Click);
            // 
            // buttonClosing
            // 
            this.buttonClosing.Location = new System.Drawing.Point(232, 291);
            this.buttonClosing.Name = "buttonClosing";
            this.buttonClosing.Size = new System.Drawing.Size(75, 23);
            this.buttonClosing.TabIndex = 6;
            this.buttonClosing.Text = "取消";
            this.buttonClosing.UseVisualStyleBackColor = true;
            // 
            // radioButtonBase
            // 
            this.radioButtonBase.AutoSize = true;
            this.radioButtonBase.Location = new System.Drawing.Point(75, 224);
            this.radioButtonBase.Name = "radioButtonBase";
            this.radioButtonBase.Size = new System.Drawing.Size(73, 19);
            this.radioButtonBase.TabIndex = 7;
            this.radioButtonBase.TabStop = true;
            this.radioButtonBase.Text = "管理员";
            this.radioButtonBase.UseVisualStyleBackColor = true;
            // 
            // radioButtonStork
            // 
            this.radioButtonStork.AutoSize = true;
            this.radioButtonStork.Location = new System.Drawing.Point(232, 224);
            this.radioButtonStork.Name = "radioButtonStork";
            this.radioButtonStork.Size = new System.Drawing.Size(73, 19);
            this.radioButtonStork.TabIndex = 8;
            this.radioButtonStork.TabStop = true;
            this.radioButtonStork.Text = "结算员";
            this.radioButtonStork.UseVisualStyleBackColor = true;
            // 
            // radioButtonReceive
            // 
            this.radioButtonReceive.AutoSize = true;
            this.radioButtonReceive.Location = new System.Drawing.Point(75, 250);
            this.radioButtonReceive.Name = "radioButtonReceive";
            this.radioButtonReceive.Size = new System.Drawing.Size(73, 19);
            this.radioButtonReceive.TabIndex = 9;
            this.radioButtonReceive.TabStop = true;
            this.radioButtonReceive.Text = "收货员";
            this.radioButtonReceive.UseVisualStyleBackColor = true;
            // 
            // radioButtonDelivery
            // 
            this.radioButtonDelivery.AutoSize = true;
            this.radioButtonDelivery.Location = new System.Drawing.Point(232, 250);
            this.radioButtonDelivery.Name = "radioButtonDelivery";
            this.radioButtonDelivery.Size = new System.Drawing.Size(73, 19);
            this.radioButtonDelivery.TabIndex = 10;
            this.radioButtonDelivery.TabStop = true;
            this.radioButtonDelivery.Text = "发货员";
            this.radioButtonDelivery.UseVisualStyleBackColor = true;
            // 
            // FormBaseUserAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(390, 326);
            this.Controls.Add(this.radioButtonDelivery);
            this.Controls.Add(this.radioButtonReceive);
            this.Controls.Add(this.radioButtonStork);
            this.Controls.Add(this.radioButtonBase);
            this.Controls.Add(this.buttonClosing);
            this.Controls.Add(this.buttonEnter);
            this.Controls.Add(this.labelNotice);
            this.Controls.Add(this.labelPsaaword);
            this.Controls.Add(this.labelusername);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.textBoxUsername);
            this.Name = "FormBaseUserAdd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "base_useradd";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxUsername;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label labelusername;
        private System.Windows.Forms.Label labelPsaaword;
        private System.Windows.Forms.Label labelNotice;
        private System.Windows.Forms.Button buttonEnter;
        private System.Windows.Forms.Button buttonClosing;
        private System.Windows.Forms.RadioButton radioButtonBase;
        private System.Windows.Forms.RadioButton radioButtonStork;
        private System.Windows.Forms.RadioButton radioButtonReceive;
        private System.Windows.Forms.RadioButton radioButtonDelivery;
    }
}