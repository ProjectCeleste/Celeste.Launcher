namespace Celeste_Launcher_Gui.Forms
{
    partial class RegisterForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegisterForm));
            this.mainContainer1 = new Celeste_Launcher_Gui.Controls.MainContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSmall2 = new Celeste_Launcher_Gui.Controls.BtnSmall();
            this.btnSmall1 = new Celeste_Launcher_Gui.Controls.BtnSmall();
            this.tb_UserName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_InviteCode = new System.Windows.Forms.TextBox();
            this.lb_Password = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbl_Mail = new System.Windows.Forms.Label();
            this.tb_ConfirmPassword = new System.Windows.Forms.TextBox();
            this.tb_Password = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_Mail = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainContainer1
            // 
            this.mainContainer1.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.mainContainer1, "mainContainer1");
            this.mainContainer1.MinimizeBox = false;
            this.mainContainer1.Name = "mainContainer1";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = global::Celeste_Launcher_Gui.Properties.Resources.BackgroundTexture;
            this.panel1.Controls.Add(this.btnSmall2);
            this.panel1.Controls.Add(this.btnSmall1);
            this.panel1.Controls.Add(this.tb_UserName);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.tb_InviteCode);
            this.panel1.Controls.Add(this.lb_Password);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lbl_Mail);
            this.panel1.Controls.Add(this.tb_ConfirmPassword);
            this.panel1.Controls.Add(this.tb_Password);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.tb_Mail);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // btnSmall2
            // 
            resources.ApplyResources(this.btnSmall2, "btnSmall2");
            this.btnSmall2.Name = "btnSmall2";
            this.btnSmall2.Click += new System.EventHandler(this.btnSmall2_Click);
            // 
            // btnSmall1
            // 
            resources.ApplyResources(this.btnSmall1, "btnSmall1");
            this.btnSmall1.Name = "btnSmall1";
            this.btnSmall1.Click += new System.EventHandler(this.btn_Register_Click);
            // 
            // tb_UserName
            // 
            this.tb_UserName.BackColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.tb_UserName, "tb_UserName");
            this.tb_UserName.Name = "tb_UserName";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Name = "label3";
            // 
            // tb_InviteCode
            // 
            this.tb_InviteCode.BackColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.tb_InviteCode, "tb_InviteCode");
            this.tb_InviteCode.Name = "tb_InviteCode";
            // 
            // lb_Password
            // 
            resources.ApplyResources(this.lb_Password, "lb_Password");
            this.lb_Password.BackColor = System.Drawing.Color.Transparent;
            this.lb_Password.Name = "lb_Password";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Name = "label2";
            // 
            // lbl_Mail
            // 
            resources.ApplyResources(this.lbl_Mail, "lbl_Mail");
            this.lbl_Mail.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Mail.Name = "lbl_Mail";
            // 
            // tb_ConfirmPassword
            // 
            this.tb_ConfirmPassword.BackColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.tb_ConfirmPassword, "tb_ConfirmPassword");
            this.tb_ConfirmPassword.Name = "tb_ConfirmPassword";
            this.tb_ConfirmPassword.UseSystemPasswordChar = true;
            // 
            // tb_Password
            // 
            this.tb_Password.BackColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.tb_Password, "tb_Password");
            this.tb_Password.Name = "tb_Password";
            this.tb_Password.UseSystemPasswordChar = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Name = "label1";
            // 
            // tb_Mail
            // 
            this.tb_Mail.BackColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.tb_Mail, "tb_Mail");
            this.tb_Mail.Name = "tb_Mail";
            // 
            // RegisterForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mainContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "RegisterForm";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.RegisterForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.MainContainer mainContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox tb_UserName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_InviteCode;
        private System.Windows.Forms.Label lb_Password;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbl_Mail;
        private System.Windows.Forms.TextBox tb_ConfirmPassword;
        public System.Windows.Forms.TextBox tb_Password;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox tb_Mail;
        private Controls.BtnSmall btnSmall1;
        private Controls.BtnSmall btnSmall2;
    }
}