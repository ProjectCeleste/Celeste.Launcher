namespace Celeste_Launcher_Gui.Forms
{
    partial class MpSettingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MpSettingForm));
            this.mainContainer1 = new Celeste_Launcher_Gui.Controls.MainContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSmall1 = new Celeste_Launcher_Gui.Controls.BtnSmall();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rb_Lan = new System.Windows.Forms.RadioButton();
            this.rb_Wan = new System.Windows.Forms.RadioButton();
            this.tb_remoteIp = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.rb_Manual = new System.Windows.Forms.RadioButton();
            this.rb_Automatic = new System.Windows.Forms.RadioButton();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
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
            this.mainContainer1.Size = new System.Drawing.Size(656, 281);
            this.mainContainer1.TabIndex = 0;
            this.mainContainer1.Title = "PROJECT CELESTE - MP SETTINGS";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = global::Celeste_Launcher_Gui.Properties.Resources.BackgroundTexture;
            this.panel1.Controls.Add(this.btnSmall1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Location = new System.Drawing.Point(17, 76);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(621, 173);
            this.panel1.TabIndex = 1;
            // 
            // btnSmall1
            // 
            this.btnSmall1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSmall1.BackgroundImage")));
            this.btnSmall1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSmall1.BtnText = "Save";
            this.btnSmall1.Location = new System.Drawing.Point(75, 118);
            this.btnSmall1.Name = "btnSmall1";
            this.btnSmall1.Size = new System.Drawing.Size(161, 46);
            this.btnSmall1.TabIndex = 53;
            this.btnSmall1.Load += new System.EventHandler(this.btnSmall1_Load);
            this.btnSmall1.Click += new System.EventHandler(this.btnSmall1_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.rb_Lan);
            this.panel2.Controls.Add(this.rb_Wan);
            this.panel2.Controls.Add(this.tb_remoteIp);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Location = new System.Drawing.Point(10, 9);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(293, 102);
            this.panel2.TabIndex = 52;
            // 
            // rb_Lan
            // 
            this.rb_Lan.AutoSize = true;
            this.rb_Lan.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb_Lan.Location = new System.Drawing.Point(141, 39);
            this.rb_Lan.Name = "rb_Lan";
            this.rb_Lan.Size = new System.Drawing.Size(88, 22);
            this.rb_Lan.TabIndex = 52;
            this.rb_Lan.Text = "LAN Only";
            this.rb_Lan.UseVisualStyleBackColor = true;
            this.rb_Lan.CheckedChanged += new System.EventHandler(this.rb_Lan_CheckedChanged);
            // 
            // rb_Wan
            // 
            this.rb_Wan.AutoSize = true;
            this.rb_Wan.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb_Wan.Location = new System.Drawing.Point(52, 39);
            this.rb_Wan.Name = "rb_Wan";
            this.rb_Wan.Size = new System.Drawing.Size(61, 22);
            this.rb_Wan.TabIndex = 51;
            this.rb_Wan.Text = "WAN";
            this.rb_Wan.UseVisualStyleBackColor = true;
            this.rb_Wan.CheckedChanged += new System.EventHandler(this.rb_Wan_CheckedChanged);
            // 
            // tb_remoteIp
            // 
            this.tb_remoteIp.Location = new System.Drawing.Point(135, 72);
            this.tb_remoteIp.Name = "tb_remoteIp";
            this.tb_remoteIp.ReadOnly = true;
            this.tb_remoteIp.Size = new System.Drawing.Size(129, 20);
            this.tb_remoteIp.TabIndex = 50;
            this.tb_remoteIp.Text = "127.0.0.1";
            this.tb_remoteIp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 16);
            this.label2.TabIndex = 43;
            this.label2.Text = "Connection Type:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(33, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 16);
            this.label4.TabIndex = 46;
            this.label4.Text = "Remote Ip:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.rb_Manual);
            this.panel3.Controls.Add(this.rb_Automatic);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.numericUpDown2);
            this.panel3.Controls.Add(this.numericUpDown1);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Location = new System.Drawing.Point(314, 9);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(293, 155);
            this.panel3.TabIndex = 51;
            // 
            // rb_Manual
            // 
            this.rb_Manual.AutoSize = true;
            this.rb_Manual.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb_Manual.Location = new System.Drawing.Point(172, 39);
            this.rb_Manual.Name = "rb_Manual";
            this.rb_Manual.Size = new System.Drawing.Size(74, 22);
            this.rb_Manual.TabIndex = 51;
            this.rb_Manual.Text = "Manual";
            this.rb_Manual.UseVisualStyleBackColor = true;
            this.rb_Manual.CheckedChanged += new System.EventHandler(this.rb_Manual_CheckedChanged);
            // 
            // rb_Automatic
            // 
            this.rb_Automatic.AutoSize = true;
            this.rb_Automatic.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb_Automatic.Location = new System.Drawing.Point(43, 39);
            this.rb_Automatic.Name = "rb_Automatic";
            this.rb_Automatic.Size = new System.Drawing.Size(92, 22);
            this.rb_Automatic.TabIndex = 50;
            this.rb_Automatic.Text = "Automatic";
            this.rb_Automatic.UseVisualStyleBackColor = true;
            this.rb_Automatic.CheckedChanged += new System.EventHandler(this.rb_Automatic_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(11, 10);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(106, 16);
            this.label7.TabIndex = 43;
            this.label7.Text = "Port Mapping:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(169, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 17);
            this.label1.TabIndex = 48;
            this.label1.Text = "Public port:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDown2.Location = new System.Drawing.Point(178, 109);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericUpDown2.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(84, 22);
            this.numericUpDown2.TabIndex = 47;
            this.numericUpDown2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDown2.Value = new decimal(new int[] {
            1337,
            0,
            0,
            0});
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Enabled = false;
            this.numericUpDown1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDown1.Location = new System.Drawing.Point(51, 109);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(84, 22);
            this.numericUpDown1.TabIndex = 45;
            this.numericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDown1.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(40, 80);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(86, 17);
            this.label8.TabIndex = 46;
            this.label8.Text = "Private port:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // MpSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(656, 281);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mainContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MpSettingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.MpSettingBox_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.MainContainer mainContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label8;
        private Controls.BtnSmall btnSmall1;
        private System.Windows.Forms.TextBox tb_remoteIp;
        private System.Windows.Forms.RadioButton rb_Manual;
        private System.Windows.Forms.RadioButton rb_Automatic;
        private System.Windows.Forms.RadioButton rb_Wan;
        private System.Windows.Forms.RadioButton rb_Lan;
    }
}