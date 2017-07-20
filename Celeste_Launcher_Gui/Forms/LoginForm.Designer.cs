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
            this.panel2 = new System.Windows.Forms.Panel();
            this.lb_Ver = new System.Windows.Forms.Label();
            this.btnSmall2 = new Celeste_Launcher_Gui.Controls.BtnSmall();
            this.btnSmall1 = new Celeste_Launcher_Gui.Controls.BtnSmall();
            this.linkLabel4 = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
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
            resources.ApplyResources(this.mainContainer1, "mainContainer1");
            this.mainContainer1.BackColor = System.Drawing.Color.Transparent;
            this.mainContainer1.MinimizeBox = false;
            this.mainContainer1.Name = "mainContainer1";
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = global::Celeste_Launcher_Gui.Properties.Resources.BackgroundTexture;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.lb_Ver);
            this.panel1.Controls.Add(this.btnSmall2);
            this.panel1.Controls.Add(this.btnSmall1);
            this.panel1.Controls.Add(this.linkLabel4);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cb_RememberMe);
            this.panel1.Controls.Add(this.linkLbl_ForgotPwd);
            this.panel1.Controls.Add(this.tb_Mail);
            this.panel1.Controls.Add(this.tb_Password);
            this.panel1.Controls.Add(this.lb_Password);
            this.panel1.Controls.Add(this.lbl_Mail);
            this.panel1.Name = "panel1";
            // 
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.BackgroundImage = global::Celeste_Launcher_Gui.Properties.Resources.BarSeparator;
            this.panel2.Name = "panel2";
            // 
            // lb_Ver
            // 
            resources.ApplyResources(this.lb_Ver, "lb_Ver");
            this.lb_Ver.BackColor = System.Drawing.Color.Transparent;
            this.lb_Ver.ForeColor = System.Drawing.Color.Black;
            this.lb_Ver.Name = "lb_Ver";
            // 
            // btnSmall2
            // 
            resources.ApplyResources(this.btnSmall2, "btnSmall2");
            this.btnSmall2.Name = "btnSmall2";
            this.btnSmall2.Click += new System.EventHandler(this.btn_Login_Click);
            // 
            // btnSmall1
            // 
            resources.ApplyResources(this.btnSmall1, "btnSmall1");
            this.btnSmall1.Name = "btnSmall1";
            this.btnSmall1.Click += new System.EventHandler(this.btn_Register_Click);
            // 
            // linkLabel4
            // 
            resources.ApplyResources(this.linkLabel4, "linkLabel4");
            this.linkLabel4.ActiveLinkColor = System.Drawing.Color.MediumBlue;
            this.linkLabel4.BackColor = System.Drawing.Color.Transparent;
            this.linkLabel4.LinkColor = System.Drawing.Color.DarkBlue;
            this.linkLabel4.Name = "linkLabel4";
            this.linkLabel4.TabStop = true;
            this.linkLabel4.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel4_LinkClicked);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Name = "label2";
            // 
            // cb_RememberMe
            // 
            resources.ApplyResources(this.cb_RememberMe, "cb_RememberMe");
            this.cb_RememberMe.BackColor = System.Drawing.Color.Transparent;
            this.cb_RememberMe.Name = "cb_RememberMe";
            this.cb_RememberMe.UseVisualStyleBackColor = false;
            // 
            // linkLbl_ForgotPwd
            // 
            resources.ApplyResources(this.linkLbl_ForgotPwd, "linkLbl_ForgotPwd");
            this.linkLbl_ForgotPwd.ActiveLinkColor = System.Drawing.Color.MediumBlue;
            this.linkLbl_ForgotPwd.BackColor = System.Drawing.Color.Transparent;
            this.linkLbl_ForgotPwd.DisabledLinkColor = System.Drawing.Color.Blue;
            this.linkLbl_ForgotPwd.LinkColor = System.Drawing.Color.DarkBlue;
            this.linkLbl_ForgotPwd.Name = "linkLbl_ForgotPwd";
            this.linkLbl_ForgotPwd.TabStop = true;
            // 
            // tb_Mail
            // 
            resources.ApplyResources(this.tb_Mail, "tb_Mail");
            this.tb_Mail.BackColor = System.Drawing.Color.Gainsboro;
            this.tb_Mail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_Mail.Name = "tb_Mail";
            // 
            // tb_Password
            // 
            resources.ApplyResources(this.tb_Password, "tb_Password");
            this.tb_Password.BackColor = System.Drawing.Color.Gainsboro;
            this.tb_Password.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_Password.Name = "tb_Password";
            this.tb_Password.UseSystemPasswordChar = true;
            // 
            // lb_Password
            // 
            resources.ApplyResources(this.lb_Password, "lb_Password");
            this.lb_Password.BackColor = System.Drawing.Color.Transparent;
            this.lb_Password.Name = "lb_Password";
            // 
            // lbl_Mail
            // 
            resources.ApplyResources(this.lbl_Mail, "lbl_Mail");
            this.lbl_Mail.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Mail.Name = "lbl_Mail";
            // 
            // LoginForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mainContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "LoginForm";
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
        private System.Windows.Forms.CheckBox cb_RememberMe;
        private System.Windows.Forms.LinkLabel linkLbl_ForgotPwd;
        private System.Windows.Forms.TextBox tb_Mail;
        private System.Windows.Forms.TextBox tb_Password;
        private System.Windows.Forms.Label lb_Password;
        private System.Windows.Forms.Label lbl_Mail;
        private System.Windows.Forms.LinkLabel linkLabel4;
        private System.Windows.Forms.Label label2;
        private Controls.BtnSmall btnSmall2;
        private Controls.BtnSmall btnSmall1;
        private System.Windows.Forms.Label lb_Ver;
        private System.Windows.Forms.Panel panel2;
    }
}