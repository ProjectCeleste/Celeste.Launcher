namespace Celeste_Launcher_Gui.Forms
{
    partial class QuickGameScan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QuickGameScan));
            this.mainContainer1 = new Celeste_AOEO_Controls.MainContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel15 = new System.Windows.Forms.Panel();
            this.lbl_GlobalProgress = new System.Windows.Forms.Label();
            this.pB_Progress = new System.Windows.Forms.ProgressBar();
            this.panel1.SuspendLayout();
            this.panel15.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainContainer1
            // 
            this.mainContainer1.BackColor = System.Drawing.Color.Transparent;
            this.mainContainer1.CloseButton = false;
            this.mainContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainContainer1.Font = new System.Drawing.Font("Ashley Crawford CG", 8.25F);
            this.mainContainer1.Location = new System.Drawing.Point(0, 0);
            this.mainContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.mainContainer1.MinimizeBox = false;
            this.mainContainer1.MinimumSize = new System.Drawing.Size(310, 250);
            this.mainContainer1.Name = "mainContainer1";
            this.mainContainer1.Size = new System.Drawing.Size(685, 250);
            this.mainContainer1.TabIndex = 12;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = global::Celeste_Launcher_Gui.Properties.Resources.BackgroundTexture;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.panel15);
            this.panel1.Font = new System.Drawing.Font("Ashley Crawford CG", 8.25F);
            this.panel1.Location = new System.Drawing.Point(25, 72);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(634, 155);
            this.panel1.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Ashley Crawford CG", 9.75F);
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(634, 93);
            this.label1.TabIndex = 73;
            this.label1.Text = "Run a \"Game Scan\" to install/update/repair Age Of Empires Online Celeste Fan Proj" +
    "ect";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = global::Celeste_Launcher_Gui.Properties.Resources.BarSeparator;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Font = new System.Drawing.Font("Ashley Crawford CG", 8.25F);
            this.panel2.Location = new System.Drawing.Point(0, 93);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(634, 27);
            this.panel2.TabIndex = 54;
            // 
            // panel15
            // 
            this.panel15.Controls.Add(this.lbl_GlobalProgress);
            this.panel15.Controls.Add(this.pB_Progress);
            this.panel15.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel15.Font = new System.Drawing.Font("Ashley Crawford CG", 8.25F);
            this.panel15.Location = new System.Drawing.Point(0, 120);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(634, 35);
            this.panel15.TabIndex = 72;
            // 
            // lbl_GlobalProgress
            // 
            this.lbl_GlobalProgress.BackColor = System.Drawing.Color.Transparent;
            this.lbl_GlobalProgress.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbl_GlobalProgress.Font = new System.Drawing.Font("Ashley Crawford CG", 9.75F);
            this.lbl_GlobalProgress.Location = new System.Drawing.Point(536, 0);
            this.lbl_GlobalProgress.Name = "lbl_GlobalProgress";
            this.lbl_GlobalProgress.Size = new System.Drawing.Size(98, 35);
            this.lbl_GlobalProgress.TabIndex = 70;
            this.lbl_GlobalProgress.Text = "000/999";
            this.lbl_GlobalProgress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pB_Progress
            // 
            this.pB_Progress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(198)))), ((int)(((byte)(170)))));
            this.pB_Progress.Font = new System.Drawing.Font("Ashley Crawford CG", 8.25F);
            this.pB_Progress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.pB_Progress.Location = new System.Drawing.Point(7, 6);
            this.pB_Progress.Name = "pB_Progress";
            this.pB_Progress.Size = new System.Drawing.Size(525, 23);
            this.pB_Progress.TabIndex = 1;
            // 
            // QuickGameScan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(685, 249);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mainContainer1);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "QuickGameScan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Project Celeste -- Login";
            this.TopMost = true;
            this.panel1.ResumeLayout(false);
            this.panel15.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Celeste_AOEO_Controls.MainContainer mainContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel15;
        private System.Windows.Forms.ProgressBar pB_Progress;
        private System.Windows.Forms.Label lbl_GlobalProgress;
        private System.Windows.Forms.Label label1;
    }
}