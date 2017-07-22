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
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
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
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.btnSmall1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Name = "panel1";
            // 
            // panel5
            // 
            resources.ApplyResources(this.panel5, "panel5");
            this.panel5.BackgroundImage = global::Celeste_Launcher_Gui.Properties.Resources.BarSeparator;
            this.panel5.Name = "panel5";
            // 
            // panel4
            // 
            resources.ApplyResources(this.panel4, "panel4");
            this.panel4.BackgroundImage = global::Celeste_Launcher_Gui.Properties.Resources.BarSeparatorVertical;
            this.panel4.Name = "panel4";
            // 
            // btnSmall1
            // 
            resources.ApplyResources(this.btnSmall1, "btnSmall1");
            this.btnSmall1.Name = "btnSmall1";
            this.btnSmall1.Size = new System.Drawing.Size(172, 42);
            this.btnSmall1.TabIndex = 53;
            this.btnSmall1.Click += new System.EventHandler(this.BtnSmall1_Click);
            // 
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.rb_Lan);
            this.panel2.Controls.Add(this.rb_Wan);
            this.panel2.Controls.Add(this.tb_remoteIp);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Name = "panel2";
            // 
            // rb_Lan
            // 
            resources.ApplyResources(this.rb_Lan, "rb_Lan");
            this.rb_Lan.Name = "rb_Lan";
            this.rb_Lan.UseVisualStyleBackColor = true;
            this.rb_Lan.CheckedChanged += new System.EventHandler(this.Rb_Lan_CheckedChanged);
            // 
            // rb_Wan
            // 
            resources.ApplyResources(this.rb_Wan, "rb_Wan");
            this.rb_Wan.Name = "rb_Wan";
            this.rb_Wan.UseVisualStyleBackColor = true;
            this.rb_Wan.CheckedChanged += new System.EventHandler(this.Rb_Wan_CheckedChanged);
            // 
            // tb_remoteIp
            // 
            resources.ApplyResources(this.tb_remoteIp, "tb_remoteIp");
            this.tb_remoteIp.Name = "tb_remoteIp";
            this.tb_remoteIp.ReadOnly = true;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Name = "label2";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Name = "label4";
            // 
            // panel3
            // 
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.Controls.Add(this.rb_Manual);
            this.panel3.Controls.Add(this.rb_Automatic);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.numericUpDown2);
            this.panel3.Controls.Add(this.numericUpDown1);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Name = "panel3";
            // 
            // rb_Manual
            // 
            resources.ApplyResources(this.rb_Manual, "rb_Manual");
            this.rb_Manual.Name = "rb_Manual";
            this.rb_Manual.UseVisualStyleBackColor = true;
            this.rb_Manual.CheckedChanged += new System.EventHandler(this.Rb_Manual_CheckedChanged);
            // 
            // rb_Automatic
            // 
            resources.ApplyResources(this.rb_Automatic, "rb_Automatic");
            this.rb_Automatic.Name = "rb_Automatic";
            this.rb_Automatic.UseVisualStyleBackColor = true;
            this.rb_Automatic.CheckedChanged += new System.EventHandler(this.Rb_Automatic_CheckedChanged);
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Name = "label7";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Name = "label1";
            // 
            // numericUpDown2
            // 
            resources.ApplyResources(this.numericUpDown2, "numericUpDown2");
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
            this.numericUpDown2.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // numericUpDown1
            // 
            resources.ApplyResources(this.numericUpDown1, "numericUpDown1");
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
            this.numericUpDown1.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Name = "label8";
            // 
            // MpSettingForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mainContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "MpSettingForm";
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
        private System.Windows.Forms.TextBox tb_remoteIp;
        private System.Windows.Forms.RadioButton rb_Manual;
        private System.Windows.Forms.RadioButton rb_Automatic;
        private System.Windows.Forms.RadioButton rb_Wan;
        private System.Windows.Forms.RadioButton rb_Lan;
        private Controls.BtnSmall btnSmall1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
    }
}