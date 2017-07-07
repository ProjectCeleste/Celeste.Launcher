namespace Celeste_Launcher_Gui.Forms
{
    partial class MsgBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MsgBox));
            this.boxContainer1 = new Celeste_Launcher_Gui.Controls.BoxContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSmall1 = new Celeste_Launcher_Gui.Controls.BtnSmall();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // boxContainer1
            // 
            this.boxContainer1.BackColor = System.Drawing.Color.Transparent;
            this.boxContainer1.CloseButton = true;
            this.boxContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.boxContainer1.Location = new System.Drawing.Point(0, 0);
            this.boxContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.boxContainer1.MinimumSize = new System.Drawing.Size(310, 250);
            this.boxContainer1.Name = "boxContainer1";
            this.boxContainer1.Size = new System.Drawing.Size(440, 310);
            this.boxContainer1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = global::Celeste_Launcher_Gui.Properties.Resources.BackgroundTexture;
            this.panel1.Controls.Add(this.btnSmall1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(30, 75);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(382, 211);
            this.panel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(382, 153);
            this.label1.TabIndex = 0;
            this.label1.Text = "MsgBox";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSmall1
            // 
            this.btnSmall1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSmall1.BackgroundImage")));
            this.btnSmall1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSmall1.BtnText = "OK";
            this.btnSmall1.Location = new System.Drawing.Point(105, 162);
            this.btnSmall1.Margin = new System.Windows.Forms.Padding(0);
            this.btnSmall1.Name = "btnSmall1";
            this.btnSmall1.Size = new System.Drawing.Size(172, 42);
            this.btnSmall1.TabIndex = 1;
            this.btnSmall1.Click += new System.EventHandler(this.lb_OK_Click);
            // 
            // MsgBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(440, 310);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.boxContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MsgBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MsgBox";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.MsgBox_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.BoxContainer boxContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private Controls.BtnSmall btnSmall1;
    }
}