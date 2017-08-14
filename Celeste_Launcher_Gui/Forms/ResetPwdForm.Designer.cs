namespace Celeste_Launcher_Gui.Forms
{
    partial class ResetPwdForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ResetPwdForm));
            this.mainContainer1 = new Celeste_AOEO_Controls.MainContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.p_Verify = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnSmall3 = new Celeste_AOEO_Controls.BtnSmall();
            this.lbl_Mail = new System.Windows.Forms.Label();
            this.tb_Mail = new System.Windows.Forms.TextBox();
            this.p_ResetPassword = new System.Windows.Forms.Panel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.btnSmall1 = new Celeste_AOEO_Controls.BtnSmall();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnSmall4 = new Celeste_AOEO_Controls.BtnSmall();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_InviteCode = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.p_Verify.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.p_ResetPassword.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
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
            this.mainContainer1.Size = new System.Drawing.Size(756, 322);
            this.mainContainer1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = global::Celeste_Launcher_Gui.Properties.Resources.BackgroundTexture;
            this.panel1.Controls.Add(this.p_Verify);
            this.panel1.Controls.Add(this.p_ResetPassword);
            this.panel1.Location = new System.Drawing.Point(26, 72);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(705, 226);
            this.panel1.TabIndex = 1;
            // 
            // p_Verify
            // 
            this.p_Verify.Controls.Add(this.panel3);
            this.p_Verify.Controls.Add(this.tableLayoutPanel2);
            this.p_Verify.Dock = System.Windows.Forms.DockStyle.Top;
            this.p_Verify.Location = new System.Drawing.Point(0, 0);
            this.p_Verify.Margin = new System.Windows.Forms.Padding(0);
            this.p_Verify.Name = "p_Verify";
            this.p_Verify.Size = new System.Drawing.Size(705, 103);
            this.p_Verify.TabIndex = 28;
            // 
            // panel3
            // 
            this.panel3.BackgroundImage = global::Celeste_Launcher_Gui.Properties.Resources.BarSeparator;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 76);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(705, 27);
            this.panel3.TabIndex = 54;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 185F));
            this.tableLayoutPanel2.Controls.Add(this.btnSmall3, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.lbl_Mail, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tb_Mail, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.Padding = new System.Windows.Forms.Padding(10, 10, 0, 5);
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(705, 76);
            this.tableLayoutPanel2.TabIndex = 55;
            // 
            // btnSmall3
            // 
            this.btnSmall3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSmall3.BackgroundImage")));
            this.btnSmall3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSmall3.BtnText = "VERIFY";
            this.btnSmall3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSmall3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSmall3.Location = new System.Drawing.Point(530, 10);
            this.btnSmall3.Margin = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btnSmall3.Name = "btnSmall3";
            this.btnSmall3.Size = new System.Drawing.Size(165, 61);
            this.btnSmall3.TabIndex = 26;
            this.btnSmall3.Click += new System.EventHandler(this.Btn_Verify_Click);
            // 
            // lbl_Mail
            // 
            this.lbl_Mail.AutoSize = true;
            this.lbl_Mail.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Mail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Mail.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Mail.Location = new System.Drawing.Point(13, 10);
            this.lbl_Mail.Name = "lbl_Mail";
            this.lbl_Mail.Size = new System.Drawing.Size(58, 61);
            this.lbl_Mail.TabIndex = 19;
            this.lbl_Mail.Text = "Email:";
            this.lbl_Mail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tb_Mail
            // 
            this.tb_Mail.BackColor = System.Drawing.Color.Gainsboro;
            this.tb_Mail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_Mail.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_Mail.Location = new System.Drawing.Point(74, 28);
            this.tb_Mail.Margin = new System.Windows.Forms.Padding(0, 18, 0, 0);
            this.tb_Mail.MaxLength = 128;
            this.tb_Mail.Name = "tb_Mail";
            this.tb_Mail.Size = new System.Drawing.Size(446, 26);
            this.tb_Mail.TabIndex = 14;
            // 
            // p_ResetPassword
            // 
            this.p_ResetPassword.Controls.Add(this.tableLayoutPanel3);
            this.p_ResetPassword.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.p_ResetPassword.Enabled = false;
            this.p_ResetPassword.Location = new System.Drawing.Point(0, 7);
            this.p_ResetPassword.Margin = new System.Windows.Forms.Padding(0);
            this.p_ResetPassword.Name = "p_ResetPassword";
            this.p_ResetPassword.Size = new System.Drawing.Size(705, 219);
            this.p_ResetPassword.TabIndex = 27;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.btnSmall1, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 96);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(705, 123);
            this.tableLayoutPanel3.TabIndex = 29;
            // 
            // btnSmall1
            // 
            this.btnSmall1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSmall1.BackgroundImage")));
            this.btnSmall1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSmall1.BtnText = "RESET PASSWORD";
            this.btnSmall1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSmall1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSmall1.Location = new System.Drawing.Point(0, 63);
            this.btnSmall1.Margin = new System.Windows.Forms.Padding(0);
            this.btnSmall1.Name = "btnSmall1";
            this.btnSmall1.Size = new System.Drawing.Size(705, 60);
            this.btnSmall1.TabIndex = 29;
            this.btnSmall1.Click += new System.EventHandler(this.Btn_ResetPassword_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 185F));
            this.tableLayoutPanel1.Controls.Add(this.btnSmall4, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tb_InviteCode, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(10, 10, 0, 5);
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 34F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(705, 63);
            this.tableLayoutPanel1.TabIndex = 28;
            // 
            // btnSmall4
            // 
            this.btnSmall4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSmall4.BackgroundImage")));
            this.btnSmall4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSmall4.BtnText = "RESEND";
            this.btnSmall4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSmall4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSmall4.Location = new System.Drawing.Point(530, 10);
            this.btnSmall4.Margin = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btnSmall4.Name = "btnSmall4";
            this.btnSmall4.Size = new System.Drawing.Size(165, 48);
            this.btnSmall4.TabIndex = 27;
            this.btnSmall4.Click += new System.EventHandler(this.Btn_Verify_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 48);
            this.label2.TabIndex = 22;
            this.label2.Text = "Email verify key:";
            // 
            // tb_InviteCode
            // 
            this.tb_InviteCode.BackColor = System.Drawing.Color.Gainsboro;
            this.tb_InviteCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_InviteCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_InviteCode.Location = new System.Drawing.Point(153, 10);
            this.tb_InviteCode.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.tb_InviteCode.MaxLength = 32;
            this.tb_InviteCode.Multiline = true;
            this.tb_InviteCode.Name = "tb_InviteCode";
            this.tb_InviteCode.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_InviteCode.Size = new System.Drawing.Size(367, 43);
            this.tb_InviteCode.TabIndex = 18;
            // 
            // ResetPwdForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(756, 322);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mainContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ResetPwdForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Project Celeste -- Reset Password";
            this.Load += new System.EventHandler(this.ResetPasswordForm_Load);
            this.panel1.ResumeLayout(false);
            this.p_Verify.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.p_ResetPassword.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Celeste_AOEO_Controls.MainContainer mainContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox tb_InviteCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbl_Mail;
        public System.Windows.Forms.TextBox tb_Mail;
        private Celeste_AOEO_Controls.BtnSmall btnSmall3;
        private System.Windows.Forms.Panel p_ResetPassword;
        private Celeste_AOEO_Controls.BtnSmall btnSmall4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel p_Verify;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private Celeste_AOEO_Controls.BtnSmall btnSmall1;
    }
}