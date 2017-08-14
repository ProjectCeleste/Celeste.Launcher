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
            this.mainContainer1 = new Celeste_AOEO_Controls.MainContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tb_Password = new System.Windows.Forms.TextBox();
            this.tb_Mail = new System.Windows.Forms.TextBox();
            this.lbl_Mail = new System.Windows.Forms.Label();
            this.lb_Password = new System.Windows.Forms.Label();
            this.cb_RememberMe = new System.Windows.Forms.CheckBox();
            this.linkLbl_ForgotPwd = new System.Windows.Forms.LinkLabel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lb_Ver = new System.Windows.Forms.Label();
            this.btnSmall2 = new Celeste_AOEO_Controls.BtnSmall();
            this.btnSmall1 = new Celeste_AOEO_Controls.BtnSmall();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.linkLabel4 = new System.Windows.Forms.LinkLabel();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainContainer1
            // 
            this.mainContainer1.BackColor = System.Drawing.Color.Transparent;
            this.mainContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainContainer1.Location = new System.Drawing.Point(0, 0);
            this.mainContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.mainContainer1.MinimizeBox = false;
            this.mainContainer1.MinimumSize = new System.Drawing.Size(310, 250);
            this.mainContainer1.Name = "mainContainer1";
            this.mainContainer1.Size = new System.Drawing.Size(808, 399);
            this.mainContainer1.TabIndex = 12;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = global::Celeste_Launcher_Gui.Properties.Resources.BackgroundTexture;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.tableLayoutPanel3);
            this.panel1.Location = new System.Drawing.Point(24, 72);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(758, 302);
            this.panel1.TabIndex = 13;
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = global::Celeste_Launcher_Gui.Properties.Resources.BarSeparator;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 157);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(758, 27);
            this.panel2.TabIndex = 54;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.tableLayoutPanel1);
            this.panel5.Controls.Add(this.tableLayoutPanel2);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Margin = new System.Windows.Forms.Padding(0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(758, 162);
            this.panel5.TabIndex = 58;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tb_Password, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.tb_Mail, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbl_Mail, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lb_Password, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.cb_RememberMe, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.linkLbl_ForgotPwd, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(10);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(10, 10, 0, 5);
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(564, 162);
            this.tableLayoutPanel1.TabIndex = 55;
            // 
            // tb_Password
            // 
            this.tb_Password.BackColor = System.Drawing.Color.Gainsboro;
            this.tb_Password.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_Password.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_Password.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_Password.Location = new System.Drawing.Point(170, 68);
            this.tb_Password.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.tb_Password.MaxLength = 32;
            this.tb_Password.Name = "tb_Password";
            this.tb_Password.PasswordChar = '*';
            this.tb_Password.Size = new System.Drawing.Size(394, 26);
            this.tb_Password.TabIndex = 20;
            this.tb_Password.UseSystemPasswordChar = true;
            // 
            // tb_Mail
            // 
            this.tb_Mail.BackColor = System.Drawing.Color.Gainsboro;
            this.tb_Mail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_Mail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_Mail.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_Mail.Location = new System.Drawing.Point(170, 20);
            this.tb_Mail.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.tb_Mail.MaxLength = 128;
            this.tb_Mail.Name = "tb_Mail";
            this.tb_Mail.Size = new System.Drawing.Size(394, 26);
            this.tb_Mail.TabIndex = 14;
            // 
            // lbl_Mail
            // 
            this.lbl_Mail.AutoSize = true;
            this.lbl_Mail.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Mail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Mail.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Mail.Location = new System.Drawing.Point(13, 10);
            this.lbl_Mail.Name = "lbl_Mail";
            this.lbl_Mail.Size = new System.Drawing.Size(154, 48);
            this.lbl_Mail.TabIndex = 18;
            this.lbl_Mail.Text = "Email:";
            this.lbl_Mail.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lb_Password
            // 
            this.lb_Password.AutoSize = true;
            this.lb_Password.BackColor = System.Drawing.Color.Transparent;
            this.lb_Password.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_Password.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Password.Location = new System.Drawing.Point(13, 58);
            this.lb_Password.Name = "lb_Password";
            this.lb_Password.Size = new System.Drawing.Size(154, 49);
            this.lb_Password.TabIndex = 19;
            this.lb_Password.Text = "Password:";
            this.lb_Password.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cb_RememberMe
            // 
            this.cb_RememberMe.AutoSize = true;
            this.cb_RememberMe.BackColor = System.Drawing.Color.Transparent;
            this.cb_RememberMe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cb_RememberMe.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_RememberMe.Location = new System.Drawing.Point(173, 107);
            this.cb_RememberMe.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.cb_RememberMe.Name = "cb_RememberMe";
            this.cb_RememberMe.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cb_RememberMe.Size = new System.Drawing.Size(388, 50);
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
            this.linkLbl_ForgotPwd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.linkLbl_ForgotPwd.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLbl_ForgotPwd.LinkColor = System.Drawing.Color.DarkBlue;
            this.linkLbl_ForgotPwd.Location = new System.Drawing.Point(13, 107);
            this.linkLbl_ForgotPwd.Name = "linkLbl_ForgotPwd";
            this.linkLbl_ForgotPwd.Size = new System.Drawing.Size(154, 50);
            this.linkLbl_ForgotPwd.TabIndex = 17;
            this.linkLbl_ForgotPwd.TabStop = true;
            this.linkLbl_ForgotPwd.Text = "Forgot Password?";
            this.linkLbl_ForgotPwd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.linkLbl_ForgotPwd.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLbl_ForgotPwd_LinkClicked);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.lb_Ver, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.btnSmall2, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.btnSmall1, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(564, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.Padding = new System.Windows.Forms.Padding(10, 10, 10, 5);
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(194, 162);
            this.tableLayoutPanel2.TabIndex = 56;
            // 
            // lb_Ver
            // 
            this.lb_Ver.AutoSize = true;
            this.lb_Ver.BackColor = System.Drawing.Color.Transparent;
            this.lb_Ver.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_Ver.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Ver.ForeColor = System.Drawing.Color.Black;
            this.lb_Ver.Location = new System.Drawing.Point(13, 108);
            this.lb_Ver.Name = "lb_Ver";
            this.lb_Ver.Size = new System.Drawing.Size(168, 49);
            this.lb_Ver.TabIndex = 43;
            this.lb_Ver.Text = "v0.0.0.0";
            this.lb_Ver.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSmall2
            // 
            this.btnSmall2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSmall2.BackgroundImage")));
            this.btnSmall2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSmall2.BtnText = "LOGIN";
            this.btnSmall2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSmall2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSmall2.Location = new System.Drawing.Point(13, 59);
            this.btnSmall2.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnSmall2.Name = "btnSmall2";
            this.btnSmall2.Size = new System.Drawing.Size(168, 49);
            this.btnSmall2.TabIndex = 42;
            this.btnSmall2.Click += new System.EventHandler(this.Btn_Login_Click);
            // 
            // btnSmall1
            // 
            this.btnSmall1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSmall1.BackgroundImage")));
            this.btnSmall1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSmall1.BtnText = "REGISTER";
            this.btnSmall1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSmall1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSmall1.Location = new System.Drawing.Point(13, 10);
            this.btnSmall1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnSmall1.Name = "btnSmall1";
            this.btnSmall1.Size = new System.Drawing.Size(168, 49);
            this.btnSmall1.TabIndex = 41;
            this.btnSmall1.Click += new System.EventHandler(this.Btn_Register_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.linkLabel4, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 184);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(758, 118);
            this.tableLayoutPanel3.TabIndex = 57;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(3);
            this.label2.Size = new System.Drawing.Size(752, 88);
            this.label2.TabIndex = 39;
            this.label2.Text = resources.GetString("label2.Text");
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // linkLabel4
            // 
            this.linkLabel4.ActiveLinkColor = System.Drawing.Color.MediumBlue;
            this.linkLabel4.AutoSize = true;
            this.linkLabel4.BackColor = System.Drawing.Color.Transparent;
            this.linkLabel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.linkLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel4.LinkColor = System.Drawing.Color.DarkBlue;
            this.linkLabel4.Location = new System.Drawing.Point(3, 88);
            this.linkLabel4.Name = "linkLabel4";
            this.linkLabel4.Padding = new System.Windows.Forms.Padding(3);
            this.linkLabel4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.linkLabel4.Size = new System.Drawing.Size(752, 30);
            this.linkLabel4.TabIndex = 40;
            this.linkLabel4.TabStop = true;
            this.linkLabel4.Text = "View Microsoft\'s \"Game Content Usage Rules\"";
            this.linkLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.linkLabel4.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel4_LinkClicked);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(808, 399);
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
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private Celeste_AOEO_Controls.MainContainer mainContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox cb_RememberMe;
        private System.Windows.Forms.LinkLabel linkLbl_ForgotPwd;
        private System.Windows.Forms.TextBox tb_Mail;
        private System.Windows.Forms.Label lb_Password;
        private System.Windows.Forms.Label lbl_Mail;
        private System.Windows.Forms.LinkLabel linkLabel4;
        private System.Windows.Forms.Label label2;
        private Celeste_AOEO_Controls.BtnSmall btnSmall2;
        private Celeste_AOEO_Controls.BtnSmall btnSmall1;
        private System.Windows.Forms.Label lb_Ver;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TextBox tb_Password;
    }
}