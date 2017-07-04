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
            this.mainContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainContainer1.Location = new System.Drawing.Point(0, 0);
            this.mainContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.mainContainer1.MinimumSize = new System.Drawing.Size(310, 250);
            this.mainContainer1.Name = "mainContainer1";
            this.mainContainer1.Size = new System.Drawing.Size(521, 392);
            this.mainContainer1.TabIndex = 0;
            this.mainContainer1.Title = "PROJECT CELESTE - REGISTER";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = global::Celeste_Launcher_Gui.Properties.Resources.BackgroundTexture;
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
            this.panel1.Location = new System.Drawing.Point(17, 76);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(485, 284);
            this.panel1.TabIndex = 1;
            // 
            // btnSmall1
            // 
            this.btnSmall1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSmall1.BackgroundImage")));
            this.btnSmall1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSmall1.BtnText = "Register";
            this.btnSmall1.Location = new System.Drawing.Point(16, 215);
            this.btnSmall1.Name = "btnSmall1";
            this.btnSmall1.Size = new System.Drawing.Size(177, 54);
            this.btnSmall1.TabIndex = 24;
            this.btnSmall1.Load += new System.EventHandler(this.btnSmall1_Load);
            this.btnSmall1.Click += new System.EventHandler(this.btn_Register_Click);
            // 
            // tb_UserName
            // 
            this.tb_UserName.BackColor = System.Drawing.Color.Gainsboro;
            this.tb_UserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_UserName.Location = new System.Drawing.Point(210, 52);
            this.tb_UserName.MaxLength = 16;
            this.tb_UserName.Name = "tb_UserName";
            this.tb_UserName.Size = new System.Drawing.Size(248, 23);
            this.tb_UserName.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(26, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 19);
            this.label3.TabIndex = 23;
            this.label3.Text = "User Name:";
            // 
            // tb_InviteCode
            // 
            this.tb_InviteCode.BackColor = System.Drawing.Color.Gainsboro;
            this.tb_InviteCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_InviteCode.Location = new System.Drawing.Point(210, 175);
            this.tb_InviteCode.MaxLength = 128;
            this.tb_InviteCode.Multiline = true;
            this.tb_InviteCode.Name = "tb_InviteCode";
            this.tb_InviteCode.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_InviteCode.Size = new System.Drawing.Size(248, 92);
            this.tb_InviteCode.TabIndex = 18;
            // 
            // lb_Password
            // 
            this.lb_Password.AutoSize = true;
            this.lb_Password.BackColor = System.Drawing.Color.Transparent;
            this.lb_Password.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Password.Location = new System.Drawing.Point(26, 95);
            this.lb_Password.Name = "lb_Password";
            this.lb_Password.Size = new System.Drawing.Size(92, 19);
            this.lb_Password.TabIndex = 20;
            this.lb_Password.Text = "Password:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(26, 177);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 19);
            this.label2.TabIndex = 22;
            this.label2.Text = "Invite Code:";
            // 
            // lbl_Mail
            // 
            this.lbl_Mail.AutoSize = true;
            this.lbl_Mail.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Mail.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Mail.Location = new System.Drawing.Point(25, 13);
            this.lbl_Mail.Name = "lbl_Mail";
            this.lbl_Mail.Size = new System.Drawing.Size(57, 19);
            this.lbl_Mail.TabIndex = 19;
            this.lbl_Mail.Text = "Email:";
            // 
            // tb_ConfirmPassword
            // 
            this.tb_ConfirmPassword.BackColor = System.Drawing.Color.Gainsboro;
            this.tb_ConfirmPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_ConfirmPassword.Location = new System.Drawing.Point(210, 134);
            this.tb_ConfirmPassword.MaxLength = 32;
            this.tb_ConfirmPassword.Name = "tb_ConfirmPassword";
            this.tb_ConfirmPassword.PasswordChar = '*';
            this.tb_ConfirmPassword.Size = new System.Drawing.Size(248, 23);
            this.tb_ConfirmPassword.TabIndex = 17;
            this.tb_ConfirmPassword.UseSystemPasswordChar = true;
            // 
            // tb_Password
            // 
            this.tb_Password.BackColor = System.Drawing.Color.Gainsboro;
            this.tb_Password.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_Password.Location = new System.Drawing.Point(210, 93);
            this.tb_Password.MaxLength = 32;
            this.tb_Password.Name = "tb_Password";
            this.tb_Password.PasswordChar = '*';
            this.tb_Password.Size = new System.Drawing.Size(248, 23);
            this.tb_Password.TabIndex = 16;
            this.tb_Password.UseSystemPasswordChar = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(26, 136);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(156, 19);
            this.label1.TabIndex = 21;
            this.label1.Text = "Confirm password:";
            // 
            // tb_Mail
            // 
            this.tb_Mail.BackColor = System.Drawing.Color.Gainsboro;
            this.tb_Mail.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_Mail.Location = new System.Drawing.Point(210, 11);
            this.tb_Mail.MaxLength = 128;
            this.tb_Mail.Name = "tb_Mail";
            this.tb_Mail.Size = new System.Drawing.Size(248, 23);
            this.tb_Mail.TabIndex = 14;
            // 
            // RegisterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(521, 392);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mainContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "RegisterForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Project Celeste -- Register";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.RegisterForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.MainContainer mainContainer1;
        private System.Windows.Forms.Panel panel1;
        private Controls.BtnSmall btnSmall1;
        private System.Windows.Forms.TextBox tb_UserName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_InviteCode;
        private System.Windows.Forms.Label lb_Password;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbl_Mail;
        private System.Windows.Forms.TextBox tb_ConfirmPassword;
        private System.Windows.Forms.TextBox tb_Password;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_Mail;
    }
}