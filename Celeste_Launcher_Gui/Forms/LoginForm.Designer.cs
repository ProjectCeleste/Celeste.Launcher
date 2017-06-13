namespace Celeste_Launcher_Gui.Forms
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.btn_Login = new System.Windows.Forms.Button();
            this.btn_Register = new System.Windows.Forms.Button();
            this.lbl_Mail = new System.Windows.Forms.Label();
            this.tb_Mail = new System.Windows.Forms.TextBox();
            this.lb_Password = new System.Windows.Forms.Label();
            this.tb_Password = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cb_RememberMe = new System.Windows.Forms.CheckBox();
            this.linkLbl_ForgotPwd = new System.Windows.Forms.LinkLabel();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Login
            // 
            this.btn_Login.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Login.Location = new System.Drawing.Point(187, 132);
            this.btn_Login.Name = "btn_Login";
            this.btn_Login.Size = new System.Drawing.Size(87, 27);
            this.btn_Login.TabIndex = 4;
            this.btn_Login.Text = "Login";
            this.btn_Login.UseVisualStyleBackColor = true;
            this.btn_Login.Click += new System.EventHandler(this.btn_Login_Click);
            // 
            // btn_Register
            // 
            this.btn_Register.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Register.Location = new System.Drawing.Point(27, 132);
            this.btn_Register.Name = "btn_Register";
            this.btn_Register.Size = new System.Drawing.Size(87, 27);
            this.btn_Register.TabIndex = 0;
            this.btn_Register.Text = "Register";
            this.btn_Register.UseVisualStyleBackColor = true;
            this.btn_Register.Click += new System.EventHandler(this.btn_Register_Click);
            // 
            // lbl_Mail
            // 
            this.lbl_Mail.AutoSize = true;
            this.lbl_Mail.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Mail.Location = new System.Drawing.Point(6, 30);
            this.lbl_Mail.Name = "lbl_Mail";
            this.lbl_Mail.Size = new System.Drawing.Size(41, 15);
            this.lbl_Mail.TabIndex = 5;
            this.lbl_Mail.Text = "Email:";
            // 
            // tb_Mail
            // 
            this.tb_Mail.Location = new System.Drawing.Point(95, 27);
            this.tb_Mail.MaxLength = 128;
            this.tb_Mail.Name = "tb_Mail";
            this.tb_Mail.Size = new System.Drawing.Size(201, 21);
            this.tb_Mail.TabIndex = 1;
            // 
            // lb_Password
            // 
            this.lb_Password.AutoSize = true;
            this.lb_Password.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Password.Location = new System.Drawing.Point(6, 71);
            this.lb_Password.Name = "lb_Password";
            this.lb_Password.Size = new System.Drawing.Size(68, 15);
            this.lb_Password.TabIndex = 7;
            this.lb_Password.Text = "Password:";
            // 
            // tb_Password
            // 
            this.tb_Password.Location = new System.Drawing.Point(95, 68);
            this.tb_Password.MaxLength = 32;
            this.tb_Password.Name = "tb_Password";
            this.tb_Password.PasswordChar = '*';
            this.tb_Password.Size = new System.Drawing.Size(201, 21);
            this.tb_Password.TabIndex = 2;
            this.tb_Password.UseSystemPasswordChar = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cb_RememberMe);
            this.groupBox1.Controls.Add(this.linkLbl_ForgotPwd);
            this.groupBox1.Controls.Add(this.btn_Register);
            this.groupBox1.Controls.Add(this.tb_Mail);
            this.groupBox1.Controls.Add(this.tb_Password);
            this.groupBox1.Controls.Add(this.lbl_Mail);
            this.groupBox1.Controls.Add(this.lb_Password);
            this.groupBox1.Controls.Add(this.btn_Login);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(307, 165);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            // 
            // cb_RememberMe
            // 
            this.cb_RememberMe.AutoSize = true;
            this.cb_RememberMe.Enabled = false;
            this.cb_RememberMe.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_RememberMe.Location = new System.Drawing.Point(187, 101);
            this.cb_RememberMe.Name = "cb_RememberMe";
            this.cb_RememberMe.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cb_RememberMe.Size = new System.Drawing.Size(109, 19);
            this.cb_RememberMe.TabIndex = 3;
            this.cb_RememberMe.Text = "Remember Me";
            this.cb_RememberMe.UseVisualStyleBackColor = true;
            // 
            // linkLbl_ForgotPwd
            // 
            this.linkLbl_ForgotPwd.AutoSize = true;
            this.linkLbl_ForgotPwd.Enabled = false;
            this.linkLbl_ForgotPwd.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLbl_ForgotPwd.Location = new System.Drawing.Point(6, 102);
            this.linkLbl_ForgotPwd.Name = "linkLbl_ForgotPwd";
            this.linkLbl_ForgotPwd.Size = new System.Drawing.Size(108, 15);
            this.linkLbl_ForgotPwd.TabIndex = 5;
            this.linkLbl_ForgotPwd.TabStop = true;
            this.linkLbl_ForgotPwd.Text = "Forgot Password ?";
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(331, 191);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Project Celeste -- Login";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LoginForm_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Login;
        private System.Windows.Forms.Button btn_Register;
        private System.Windows.Forms.Label lbl_Mail;
        private System.Windows.Forms.TextBox tb_Mail;
        private System.Windows.Forms.Label lb_Password;
        private System.Windows.Forms.TextBox tb_Password;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cb_RememberMe;
        private System.Windows.Forms.LinkLabel linkLbl_ForgotPwd;
    }
}