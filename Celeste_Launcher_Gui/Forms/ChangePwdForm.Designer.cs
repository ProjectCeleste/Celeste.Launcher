namespace Celeste_Launcher_Gui.Forms
{
    partial class ChangePwdForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChangePwdForm));
            this.mainContainer1 = new Celeste_AOEO_Controls.MainContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnSmall1 = new Celeste_AOEO_Controls.BtnSmall();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lb_Password = new System.Windows.Forms.Label();
            this.tb_Password = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainContainer1
            // 
            this.mainContainer1.BackColor = System.Drawing.Color.Transparent;
            this.mainContainer1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("mainContainer1.BackgroundImage")));
            this.mainContainer1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.mainContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainContainer1.Location = new System.Drawing.Point(0, 0);
            this.mainContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.mainContainer1.MinimizeBox = false;
            this.mainContainer1.MinimumSize = new System.Drawing.Size(310, 250);
            this.mainContainer1.Name = "mainContainer1";
            this.mainContainer1.Size = new System.Drawing.Size(759, 262);
            this.mainContainer1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = global::Celeste_Launcher_Gui.Properties.Resources.BackgroundTexture;
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Location = new System.Drawing.Point(30, 77);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(699, 158);
            this.panel1.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 185F));
            this.tableLayoutPanel1.Controls.Add(this.btnSmall1, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.textBox2, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBox1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lb_Password, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tb_Password, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(10, 10, 0, 5);
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(699, 158);
            this.tableLayoutPanel1.TabIndex = 21;
            // 
            // btnSmall1
            // 
            this.btnSmall1.BackgroundImage = global::Celeste_Launcher_Gui.Properties.Resources.Play_Button___Normal;
            this.btnSmall1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSmall1.BtnText = "SAVE";
            this.btnSmall1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSmall1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSmall1.Location = new System.Drawing.Point(524, 57);
            this.btnSmall1.Margin = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.btnSmall1.Name = "btnSmall1";
            this.btnSmall1.Size = new System.Drawing.Size(165, 47);
            this.btnSmall1.TabIndex = 20;
            this.btnSmall1.Click += new System.EventHandler(this.BtnSmall1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(258, 49);
            this.label2.TabIndex = 19;
            this.label2.Text = "Confirm new password:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox2.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.Location = new System.Drawing.Point(274, 114);
            this.textBox2.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.textBox2.MaxLength = 32;
            this.textBox2.Name = "textBox2";
            this.textBox2.PasswordChar = '*';
            this.textBox2.Size = new System.Drawing.Size(240, 30);
            this.textBox2.TabIndex = 16;
            this.textBox2.UseSystemPasswordChar = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(258, 47);
            this.label1.TabIndex = 18;
            this.label1.Text = "New password:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(274, 67);
            this.textBox1.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.textBox1.MaxLength = 32;
            this.textBox1.Name = "textBox1";
            this.textBox1.PasswordChar = '*';
            this.textBox1.Size = new System.Drawing.Size(240, 30);
            this.textBox1.TabIndex = 15;
            this.textBox1.UseSystemPasswordChar = true;
            // 
            // lb_Password
            // 
            this.lb_Password.AutoSize = true;
            this.lb_Password.BackColor = System.Drawing.Color.Transparent;
            this.lb_Password.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_Password.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Password.Location = new System.Drawing.Point(13, 10);
            this.lb_Password.Name = "lb_Password";
            this.lb_Password.Size = new System.Drawing.Size(258, 47);
            this.lb_Password.TabIndex = 17;
            this.lb_Password.Text = "Old password:";
            this.lb_Password.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tb_Password
            // 
            this.tb_Password.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tb_Password.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_Password.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_Password.Location = new System.Drawing.Point(274, 20);
            this.tb_Password.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.tb_Password.MaxLength = 32;
            this.tb_Password.Name = "tb_Password";
            this.tb_Password.PasswordChar = '*';
            this.tb_Password.Size = new System.Drawing.Size(240, 30);
            this.tb_Password.TabIndex = 14;
            this.tb_Password.UseSystemPasswordChar = true;
            // 
            // ChangePwdForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(759, 262);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mainContainer1);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ChangePwdForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Project Celeste -- Change Password";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ChangePwdForm_Load);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Celeste_AOEO_Controls.MainContainer mainContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_Password;
        private System.Windows.Forms.Label lb_Password;
        private Celeste_AOEO_Controls.BtnSmall btnSmall1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}