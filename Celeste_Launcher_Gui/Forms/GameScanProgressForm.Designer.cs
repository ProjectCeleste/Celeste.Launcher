namespace Celeste_Launcher_Gui.Forms
{
    partial class GameScanProgressForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameScanProgressForm));
            this.panel13 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_GlobalProgress = new System.Windows.Forms.Label();
            this.pB_GlobalProgress = new System.Windows.Forms.ProgressBar();
            this.panel14 = new System.Windows.Forms.Panel();
            this.pB_SubProgress = new System.Windows.Forms.ProgressBar();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_ProgressDetail = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_ProgressTitle = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBoxButtonCustom1 = new Celeste_AOEO_Controls.PictureBoxButtonCustom();
            this.panel13.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxButtonCustom1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel13
            // 
            this.panel13.Controls.Add(this.tableLayoutPanel1);
            this.panel13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.panel13.Location = new System.Drawing.Point(9, 65);
            this.panel13.Margin = new System.Windows.Forms.Padding(0);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(531, 245);
            this.panel13.TabIndex = 66;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel5, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel14, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.pB_SubProgress, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.lbl_ProgressTitle, 0, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(531, 245);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 78.53107F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.46893F));
            this.tableLayoutPanel5.Controls.Add(this.lbl_GlobalProgress, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.pB_GlobalProgress, 0, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(531, 45);
            this.tableLayoutPanel5.TabIndex = 75;
            // 
            // lbl_GlobalProgress
            // 
            this.lbl_GlobalProgress.AutoEllipsis = true;
            this.lbl_GlobalProgress.BackColor = System.Drawing.Color.Transparent;
            this.lbl_GlobalProgress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_GlobalProgress.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Bold);
            this.lbl_GlobalProgress.Location = new System.Drawing.Point(422, 5);
            this.lbl_GlobalProgress.Margin = new System.Windows.Forms.Padding(5);
            this.lbl_GlobalProgress.Name = "lbl_GlobalProgress";
            this.lbl_GlobalProgress.Size = new System.Drawing.Size(104, 35);
            this.lbl_GlobalProgress.TabIndex = 69;
            this.lbl_GlobalProgress.Text = "000/999";
            this.lbl_GlobalProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pB_GlobalProgress
            // 
            this.pB_GlobalProgress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(198)))), ((int)(((byte)(170)))));
            this.pB_GlobalProgress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pB_GlobalProgress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.pB_GlobalProgress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.pB_GlobalProgress.Location = new System.Drawing.Point(5, 5);
            this.pB_GlobalProgress.Margin = new System.Windows.Forms.Padding(5);
            this.pB_GlobalProgress.Name = "pB_GlobalProgress";
            this.pB_GlobalProgress.Size = new System.Drawing.Size(407, 35);
            this.pB_GlobalProgress.TabIndex = 0;
            // 
            // panel14
            // 
            this.panel14.BackgroundImage = global::Celeste_Launcher_Gui.Properties.Resources.BarSeparator;
            this.panel14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.panel14.Location = new System.Drawing.Point(0, 60);
            this.panel14.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(531, 27);
            this.panel14.TabIndex = 65;
            // 
            // pB_SubProgress
            // 
            this.pB_SubProgress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(198)))), ((int)(((byte)(170)))));
            this.pB_SubProgress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pB_SubProgress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.pB_SubProgress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.pB_SubProgress.Location = new System.Drawing.Point(5, 152);
            this.pB_SubProgress.Margin = new System.Windows.Forms.Padding(5);
            this.pB_SubProgress.Name = "pB_SubProgress";
            this.pB_SubProgress.Size = new System.Drawing.Size(521, 35);
            this.pB_SubProgress.TabIndex = 1;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel4.Controls.Add(this.lbl_ProgressDetail, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 192);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(531, 60);
            this.tableLayoutPanel4.TabIndex = 74;
            // 
            // lbl_ProgressDetail
            // 
            this.lbl_ProgressDetail.AutoEllipsis = true;
            this.lbl_ProgressDetail.BackColor = System.Drawing.Color.Transparent;
            this.lbl_ProgressDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_ProgressDetail.Font = new System.Drawing.Font("Segoe UI Black", 9F, System.Drawing.FontStyle.Bold);
            this.lbl_ProgressDetail.Location = new System.Drawing.Point(318, 0);
            this.lbl_ProgressDetail.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_ProgressDetail.Name = "lbl_ProgressDetail";
            this.lbl_ProgressDetail.Padding = new System.Windows.Forms.Padding(0, 5, 5, 0);
            this.lbl_ProgressDetail.Size = new System.Drawing.Size(213, 60);
            this.lbl_ProgressDetail.TabIndex = 72;
            this.lbl_ProgressDetail.Text = "...";
            this.lbl_ProgressDetail.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label1
            // 
            this.label1.AutoEllipsis = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI Black", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(0, 5);
            this.label1.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(5, 5, 0, 0);
            this.label1.Size = new System.Drawing.Size(318, 55);
            this.label1.TabIndex = 73;
            this.label1.Text = "...";
            // 
            // lbl_ProgressTitle
            // 
            this.lbl_ProgressTitle.AutoEllipsis = true;
            this.lbl_ProgressTitle.BackColor = System.Drawing.Color.Transparent;
            this.lbl_ProgressTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_ProgressTitle.Font = new System.Drawing.Font("Segoe UI Black", 8F, System.Drawing.FontStyle.Bold);
            this.lbl_ProgressTitle.Location = new System.Drawing.Point(5, 107);
            this.lbl_ProgressTitle.Margin = new System.Windows.Forms.Padding(5);
            this.lbl_ProgressTitle.Name = "lbl_ProgressTitle";
            this.lbl_ProgressTitle.Size = new System.Drawing.Size(521, 35);
            this.lbl_ProgressTitle.TabIndex = 70;
            this.lbl_ProgressTitle.Text = "...";
            this.lbl_ProgressTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.BackgroundImage = global::Celeste_Launcher_Gui.Properties.Resources.DialogBox;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Controls.Add(this.pictureBoxButtonCustom1);
            this.panel2.Controls.Add(this.panel13);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(552, 336);
            this.panel2.TabIndex = 14;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(14, 14);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(492, 49);
            this.panel1.TabIndex = 78;
            // 
            // label2
            // 
            this.label2.AutoEllipsis = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Segoe UI Black", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label2.Location = new System.Drawing.Point(52, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(440, 49);
            this.label2.TabIndex = 78;
            this.label2.Text = "Game Scanner";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Image = global::Celeste_Launcher_Gui.Properties.Resources.CelesteWebsiteNormal;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(52, 49);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 77;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBoxButtonCustom1
            // 
            this.pictureBoxButtonCustom1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxButtonCustom1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxButtonCustom1.DisabledImage = null;
            this.pictureBoxButtonCustom1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.pictureBoxButtonCustom1.HoverImage = global::Celeste_Launcher_Gui.Properties.Resources.XButton_Hover;
            this.pictureBoxButtonCustom1.Image = global::Celeste_Launcher_Gui.Properties.Resources.XButton_Normal;
            this.pictureBoxButtonCustom1.Location = new System.Drawing.Point(509, 13);
            this.pictureBoxButtonCustom1.Name = "pictureBoxButtonCustom1";
            this.pictureBoxButtonCustom1.NormalImage = global::Celeste_Launcher_Gui.Properties.Resources.XButton_Normal;
            this.pictureBoxButtonCustom1.ShowToolTip = true;
            this.pictureBoxButtonCustom1.Size = new System.Drawing.Size(32, 32);
            this.pictureBoxButtonCustom1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxButtonCustom1.TabIndex = 72;
            this.pictureBoxButtonCustom1.TabStop = false;
            this.pictureBoxButtonCustom1.ToolTipText = "Close";
            this.pictureBoxButtonCustom1.Click += new System.EventHandler(this.PictureBoxButtonCustom1_Click);
            // 
            // GameScanProgressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(552, 336);
            this.ControlBox = false;
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GameScanProgressForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Celeste Fan Project";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GameScan_FormClosing);
            this.Load += new System.EventHandler(this.GameScanProgressForm_Load);
            this.Shown += new System.EventHandler(this.GameScanProgressForm_Shown);
            this.panel13.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxButtonCustom1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.Panel panel2;
        private Celeste_AOEO_Controls.PictureBoxButtonCustom pictureBoxButtonCustom1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Label lbl_GlobalProgress;
        private System.Windows.Forms.ProgressBar pB_GlobalProgress;
        private System.Windows.Forms.Panel panel14;
        private System.Windows.Forms.ProgressBar pB_SubProgress;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label lbl_ProgressDetail;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_ProgressTitle;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}