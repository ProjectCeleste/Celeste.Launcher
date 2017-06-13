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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tb_UserName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_InviteCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_ConfirmPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_Register = new System.Windows.Forms.Button();
            this.tb_Mail = new System.Windows.Forms.TextBox();
            this.tb_Password = new System.Windows.Forms.TextBox();
            this.lbl_Mail = new System.Windows.Forms.Label();
            this.lb_Password = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tb_UserName);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tb_InviteCode);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tb_ConfirmPassword);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btn_Register);
            this.groupBox1.Controls.Add(this.tb_Mail);
            this.groupBox1.Controls.Add(this.tb_Password);
            this.groupBox1.Controls.Add(this.lbl_Mail);
            this.groupBox1.Controls.Add(this.lb_Password);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(339, 220);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            // 
            // tb_UserName
            // 
            this.tb_UserName.Location = new System.Drawing.Point(127, 49);
            this.tb_UserName.MaxLength = 16;
            this.tb_UserName.Name = "tb_UserName";
            this.tb_UserName.Size = new System.Drawing.Size(201, 20);
            this.tb_UserName.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 15);
            this.label3.TabIndex = 13;
            this.label3.Text = "User Name:";
            // 
            // tb_InviteCode
            // 
            this.tb_InviteCode.Location = new System.Drawing.Point(127, 140);
            this.tb_InviteCode.MaxLength = 128;
            this.tb_InviteCode.Multiline = true;
            this.tb_InviteCode.Name = "tb_InviteCode";
            this.tb_InviteCode.Size = new System.Drawing.Size(201, 58);
            this.tb_InviteCode.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 140);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 15);
            this.label2.TabIndex = 11;
            this.label2.Text = "Invite Code:";
            // 
            // tb_ConfirmPassword
            // 
            this.tb_ConfirmPassword.Location = new System.Drawing.Point(127, 101);
            this.tb_ConfirmPassword.MaxLength = 32;
            this.tb_ConfirmPassword.Name = "tb_ConfirmPassword";
            this.tb_ConfirmPassword.PasswordChar = '*';
            this.tb_ConfirmPassword.Size = new System.Drawing.Size(201, 20);
            this.tb_ConfirmPassword.TabIndex = 3;
            this.tb_ConfirmPassword.UseSystemPasswordChar = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 103);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 15);
            this.label1.TabIndex = 9;
            this.label1.Text = "Confirm password:";
            // 
            // btn_Register
            // 
            this.btn_Register.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Register.Location = new System.Drawing.Point(9, 171);
            this.btn_Register.Name = "btn_Register";
            this.btn_Register.Size = new System.Drawing.Size(87, 27);
            this.btn_Register.TabIndex = 5;
            this.btn_Register.Text = "Register";
            this.btn_Register.UseVisualStyleBackColor = true;
            this.btn_Register.Click += new System.EventHandler(this.btn_Register_Click);
            // 
            // tb_Mail
            // 
            this.tb_Mail.Location = new System.Drawing.Point(127, 23);
            this.tb_Mail.MaxLength = 128;
            this.tb_Mail.Name = "tb_Mail";
            this.tb_Mail.Size = new System.Drawing.Size(201, 20);
            this.tb_Mail.TabIndex = 0;
            // 
            // tb_Password
            // 
            this.tb_Password.Location = new System.Drawing.Point(127, 75);
            this.tb_Password.MaxLength = 32;
            this.tb_Password.Name = "tb_Password";
            this.tb_Password.PasswordChar = '*';
            this.tb_Password.Size = new System.Drawing.Size(201, 20);
            this.tb_Password.TabIndex = 2;
            this.tb_Password.UseSystemPasswordChar = true;
            // 
            // lbl_Mail
            // 
            this.lbl_Mail.AutoSize = true;
            this.lbl_Mail.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Mail.Location = new System.Drawing.Point(6, 25);
            this.lbl_Mail.Name = "lbl_Mail";
            this.lbl_Mail.Size = new System.Drawing.Size(41, 15);
            this.lbl_Mail.TabIndex = 5;
            this.lbl_Mail.Text = "Email:";
            // 
            // lb_Password
            // 
            this.lb_Password.AutoSize = true;
            this.lb_Password.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Password.Location = new System.Drawing.Point(6, 77);
            this.lb_Password.Name = "lb_Password";
            this.lb_Password.Size = new System.Drawing.Size(68, 15);
            this.lb_Password.TabIndex = 7;
            this.lb_Password.Text = "Password:";
            // 
            // RegisterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(363, 244);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RegisterForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Project Celeste -- Register";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tb_InviteCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_ConfirmPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_Register;
        private System.Windows.Forms.TextBox tb_Mail;
        private System.Windows.Forms.TextBox tb_Password;
        private System.Windows.Forms.Label lbl_Mail;
        private System.Windows.Forms.Label lb_Password;
        private System.Windows.Forms.TextBox tb_UserName;
        private System.Windows.Forms.Label label3;
    }
}