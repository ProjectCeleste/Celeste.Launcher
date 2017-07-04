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
            this.mainContainer1 = new Celeste_Launcher_Gui.Controls.MainContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lb_Ver = new System.Windows.Forms.Label();
            this.btnSmall2 = new Celeste_Launcher_Gui.Controls.BtnSmall();
            this.btnSmall1 = new Celeste_Launcher_Gui.Controls.BtnSmall();
            this.cb_RememberMe = new System.Windows.Forms.CheckBox();
            this.linkLbl_ForgotPwd = new System.Windows.Forms.LinkLabel();
            this.tb_Mail = new System.Windows.Forms.TextBox();
            this.tb_Password = new System.Windows.Forms.TextBox();
            this.lb_Password = new System.Windows.Forms.Label();
            this.lbl_Mail = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainContainer1
            // 
            this.mainContainer1.BackColor = System.Drawing.Color.Transparent;
            this.mainContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainContainer1.Location = new System.Drawing.Point(0, 0);
            this.mainContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.mainContainer1.MinimumSize = new System.Drawing.Size(310, 250);
            this.mainContainer1.Name = "mainContainer1";
            this.mainContainer1.Size = new System.Drawing.Size(493, 382);
            this.mainContainer1.TabIndex = 12;
            this.mainContainer1.Title = "PROJECT CELESTE - LOGIN";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = global::Celeste_Launcher_Gui.Properties.Resources.BackgroundTexture;
            this.panel1.Controls.Add(this.lb_Ver);
            this.panel1.Controls.Add(this.btnSmall2);
            this.panel1.Controls.Add(this.btnSmall1);
            this.panel1.Controls.Add(this.cb_RememberMe);
            this.panel1.Controls.Add(this.linkLbl_ForgotPwd);
            this.panel1.Controls.Add(this.tb_Mail);
            this.panel1.Controls.Add(this.tb_Password);
            this.panel1.Controls.Add(this.lb_Password);
            this.panel1.Controls.Add(this.lbl_Mail);
            this.panel1.Location = new System.Drawing.Point(18, 74);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(457, 276);
            this.panel1.TabIndex = 13;
            // 
            // lb_Ver
            // 
            this.lb_Ver.AutoSize = true;
            this.lb_Ver.BackColor = System.Drawing.Color.Transparent;
            this.lb_Ver.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Ver.Location = new System.Drawing.Point(411, 254);
            this.lb_Ver.Name = "lb_Ver";
            this.lb_Ver.Size = new System.Drawing.Size(39, 16);
            this.lb_Ver.TabIndex = 20;
            this.lb_Ver.Text = "v130";
            // 
            // btnSmall2
            // 
            this.btnSmall2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSmall2.BackgroundImage")));
            this.btnSmall2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSmall2.BtnText = "Login";
            this.btnSmall2.Location = new System.Drawing.Point(250, 203);
            this.btnSmall2.Name = "btnSmall2";
            this.btnSmall2.Size = new System.Drawing.Size(186, 54);
            this.btnSmall2.TabIndex = 22;
            this.btnSmall2.Click += new System.EventHandler(this.btn_Login_Click);
            // 
            // btnSmall1
            // 
            this.btnSmall1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSmall1.BackgroundImage")));
            this.btnSmall1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSmall1.BtnText = "Register";
            this.btnSmall1.Location = new System.Drawing.Point(25, 203);
            this.btnSmall1.Name = "btnSmall1";
            this.btnSmall1.Size = new System.Drawing.Size(197, 54);
            this.btnSmall1.TabIndex = 21;
            this.btnSmall1.Click += new System.EventHandler(this.btn_Register_Click);
            // 
            // cb_RememberMe
            // 
            this.cb_RememberMe.AutoSize = true;
            this.cb_RememberMe.BackColor = System.Drawing.Color.Transparent;
            this.cb_RememberMe.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_RememberMe.Location = new System.Drawing.Point(270, 157);
            this.cb_RememberMe.Name = "cb_RememberMe";
            this.cb_RememberMe.Size = new System.Drawing.Size(156, 28);
            this.cb_RememberMe.TabIndex = 16;
            this.cb_RememberMe.Text = "Remember Me";
            this.cb_RememberMe.UseVisualStyleBackColor = false;
            // 
            // linkLbl_ForgotPwd
            // 
            this.linkLbl_ForgotPwd.ActiveLinkColor = System.Drawing.Color.MediumBlue;
            this.linkLbl_ForgotPwd.AutoSize = true;
            this.linkLbl_ForgotPwd.BackColor = System.Drawing.Color.Transparent;
            this.linkLbl_ForgotPwd.DisabledLinkColor = System.Drawing.Color.Blue;
            this.linkLbl_ForgotPwd.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLbl_ForgotPwd.LinkColor = System.Drawing.Color.DarkBlue;
            this.linkLbl_ForgotPwd.Location = new System.Drawing.Point(14, 158);
            this.linkLbl_ForgotPwd.Name = "linkLbl_ForgotPwd";
            this.linkLbl_ForgotPwd.Size = new System.Drawing.Size(162, 24);
            this.linkLbl_ForgotPwd.TabIndex = 17;
            this.linkLbl_ForgotPwd.TabStop = true;
            this.linkLbl_ForgotPwd.Text = "Forgot Password?";
            // 
            // tb_Mail
            // 
            this.tb_Mail.BackColor = System.Drawing.Color.Gainsboro;
            this.tb_Mail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_Mail.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_Mail.Location = new System.Drawing.Point(169, 24);
            this.tb_Mail.MaxLength = 128;
            this.tb_Mail.Name = "tb_Mail";
            this.tb_Mail.Size = new System.Drawing.Size(267, 26);
            this.tb_Mail.TabIndex = 14;
            // 
            // tb_Password
            // 
            this.tb_Password.BackColor = System.Drawing.Color.Gainsboro;
            this.tb_Password.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_Password.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_Password.Location = new System.Drawing.Point(169, 88);
            this.tb_Password.MaxLength = 32;
            this.tb_Password.Name = "tb_Password";
            this.tb_Password.PasswordChar = '*';
            this.tb_Password.Size = new System.Drawing.Size(267, 26);
            this.tb_Password.TabIndex = 15;
            this.tb_Password.UseSystemPasswordChar = true;
            // 
            // lb_Password
            // 
            this.lb_Password.AutoSize = true;
            this.lb_Password.BackColor = System.Drawing.Color.Transparent;
            this.lb_Password.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Password.Location = new System.Drawing.Point(14, 92);
            this.lb_Password.Name = "lb_Password";
            this.lb_Password.Size = new System.Drawing.Size(97, 24);
            this.lb_Password.TabIndex = 19;
            this.lb_Password.Text = "Password:";
            // 
            // lbl_Mail
            // 
            this.lbl_Mail.AutoSize = true;
            this.lbl_Mail.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Mail.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Mail.Location = new System.Drawing.Point(14, 26);
            this.lbl_Mail.Name = "lbl_Mail";
            this.lbl_Mail.Size = new System.Drawing.Size(62, 24);
            this.lbl_Mail.TabIndex = 18;
            this.lbl_Mail.Text = "Email:";
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(493, 382);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mainContainer1);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Project Celeste -- Login";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LoginForm_FormClosing);
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private Controls.MainContainer mainContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lb_Ver;
        private Controls.BtnSmall btnSmall2;
        private Controls.BtnSmall btnSmall1;
        private System.Windows.Forms.CheckBox cb_RememberMe;
        private System.Windows.Forms.LinkLabel linkLbl_ForgotPwd;
        private System.Windows.Forms.TextBox tb_Mail;
        private System.Windows.Forms.TextBox tb_Password;
        private System.Windows.Forms.Label lb_Password;
        private System.Windows.Forms.Label lbl_Mail;
    }
}