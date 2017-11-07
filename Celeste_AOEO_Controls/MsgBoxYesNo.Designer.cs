namespace Celeste_AOEO_Controls
{
    partial class MsgBoxYesNo
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
            this.boxContainer1 = new Celeste_AOEO_Controls.BoxContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSmall2 = new Celeste_AOEO_Controls.BtnSmall();
            this.btnSmall1 = new Celeste_AOEO_Controls.BtnSmall();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // boxContainer1
            // 
            this.boxContainer1.BackColor = System.Drawing.Color.Transparent;
            this.boxContainer1.CloseButton = true;
            this.boxContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.boxContainer1.Font = new System.Drawing.Font("Ashley Crawford CG", 8.25F);
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
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Font = new System.Drawing.Font("Ashley Crawford CG", 8.25F);
            this.panel1.Location = new System.Drawing.Point(30, 75);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(382, 211);
            this.panel1.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackgroundImage = global::Celeste_AOEO_Controls.Properties.Resources.BackgroundTexture;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Font = new System.Drawing.Font("Ashley Crawford CG", 8.25F);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(382, 211);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Ashley Crawford CG", 9.75F);
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(382, 166);
            this.label1.TabIndex = 0;
            this.label1.Text = "MsgBox";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnSmall2);
            this.panel2.Controls.Add(this.btnSmall1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Font = new System.Drawing.Font("Ashley Crawford CG", 8.25F);
            this.panel2.Location = new System.Drawing.Point(3, 169);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(376, 39);
            this.panel2.TabIndex = 1;
            // 
            // btnSmall2
            // 
            this.btnSmall2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSmall2.BackgroundImage")));
            this.btnSmall2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSmall2.BtnText = "No";
            this.btnSmall2.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSmall2.Font = new System.Drawing.Font("Ashley Crawford CG", 8.25F);
            this.btnSmall2.Location = new System.Drawing.Point(191, 0);
            this.btnSmall2.Margin = new System.Windows.Forms.Padding(0);
            this.btnSmall2.Name = "btnSmall2";
            this.btnSmall2.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.btnSmall2.Size = new System.Drawing.Size(185, 39);
            this.btnSmall2.TabIndex = 3;
            this.btnSmall2.Click += new System.EventHandler(this.BtnSmall2_Click);
            // 
            // btnSmall1
            // 
            this.btnSmall1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSmall1.BackgroundImage")));
            this.btnSmall1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSmall1.BtnText = "Yes";
            this.btnSmall1.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnSmall1.Font = new System.Drawing.Font("Ashley Crawford CG", 8.25F);
            this.btnSmall1.Location = new System.Drawing.Point(0, 0);
            this.btnSmall1.Margin = new System.Windows.Forms.Padding(0);
            this.btnSmall1.Name = "btnSmall1";
            this.btnSmall1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.btnSmall1.Size = new System.Drawing.Size(185, 39);
            this.btnSmall1.TabIndex = 2;
            this.btnSmall1.Click += new System.EventHandler(this.BtnSmall1_Click);
            // 
            // MsgBoxYesNo
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
            this.Name = "MsgBoxYesNo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MsgBox";
            this.TopMost = true;
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Celeste_AOEO_Controls.BoxContainer boxContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private BtnSmall btnSmall2;
        private BtnSmall btnSmall1;
    }
}