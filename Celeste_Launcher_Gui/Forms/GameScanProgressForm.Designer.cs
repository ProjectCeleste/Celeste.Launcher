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
            this.panel5 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tB_Report = new System.Windows.Forms.TextBox();
            this.pB_GlobalProgress = new System.Windows.Forms.ProgressBar();
            this.lbl_GlobalProgress = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel14 = new System.Windows.Forms.Panel();
            this.panel13 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_ProgressDetail = new System.Windows.Forms.Label();
            this.lbl_ProgressTitle = new System.Windows.Forms.Label();
            this.panel15 = new System.Windows.Forms.Panel();
            this.pB_SubProgress = new System.Windows.Forms.ProgressBar();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBoxButtonCustom1 = new Celeste_AOEO_Controls.PictureBoxButtonCustom();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel5.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel13.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel15.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxButtonCustom1)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.tableLayoutPanel3);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.panel5.Location = new System.Drawing.Point(3, 3);
            this.panel5.Name = "panel5";
            this.panel5.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.panel5.Size = new System.Drawing.Size(663, 167);
            this.panel5.TabIndex = 67;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tableLayoutPanel3.Controls.Add(this.tB_Report, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.pB_GlobalProgress, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.lbl_GlobalProgress, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.pictureBox1, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(10, 5);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(643, 157);
            this.tableLayoutPanel3.TabIndex = 70;
            // 
            // tB_Report
            // 
            this.tB_Report.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(198)))), ((int)(((byte)(170)))));
            this.tB_Report.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tB_Report.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tB_Report.Location = new System.Drawing.Point(0, 0);
            this.tB_Report.Margin = new System.Windows.Forms.Padding(0);
            this.tB_Report.Multiline = true;
            this.tB_Report.Name = "tB_Report";
            this.tB_Report.ReadOnly = true;
            this.tB_Report.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tB_Report.Size = new System.Drawing.Size(503, 132);
            this.tB_Report.TabIndex = 64;
            // 
            // pB_GlobalProgress
            // 
            this.pB_GlobalProgress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(198)))), ((int)(((byte)(170)))));
            this.pB_GlobalProgress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pB_GlobalProgress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.pB_GlobalProgress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.pB_GlobalProgress.Location = new System.Drawing.Point(0, 132);
            this.pB_GlobalProgress.Margin = new System.Windows.Forms.Padding(0);
            this.pB_GlobalProgress.Name = "pB_GlobalProgress";
            this.pB_GlobalProgress.Size = new System.Drawing.Size(503, 25);
            this.pB_GlobalProgress.TabIndex = 0;
            // 
            // lbl_GlobalProgress
            // 
            this.lbl_GlobalProgress.AutoSize = true;
            this.lbl_GlobalProgress.BackColor = System.Drawing.Color.Transparent;
            this.lbl_GlobalProgress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_GlobalProgress.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.lbl_GlobalProgress.Location = new System.Drawing.Point(503, 132);
            this.lbl_GlobalProgress.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_GlobalProgress.Name = "lbl_GlobalProgress";
            this.lbl_GlobalProgress.Size = new System.Drawing.Size(140, 25);
            this.lbl_GlobalProgress.TabIndex = 69;
            this.lbl_GlobalProgress.Text = "000/999";
            this.lbl_GlobalProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::Celeste_Launcher_Gui.Properties.Resources.CelesteWebsiteNormal;
            this.pictureBox1.Location = new System.Drawing.Point(503, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(140, 132);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 70;
            this.pictureBox1.TabStop = false;
            // 
            // panel14
            // 
            this.panel14.BackgroundImage = global::Celeste_Launcher_Gui.Properties.Resources.BarSeparator;
            this.panel14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.panel14.Location = new System.Drawing.Point(0, 173);
            this.panel14.Margin = new System.Windows.Forms.Padding(0);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(669, 27);
            this.panel14.TabIndex = 65;
            // 
            // panel13
            // 
            this.panel13.Controls.Add(this.tableLayoutPanel1);
            this.panel13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.panel13.Location = new System.Drawing.Point(20, 210);
            this.panel13.Margin = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(629, 154);
            this.panel13.TabIndex = 66;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.lbl_ProgressDetail, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lbl_ProgressTitle, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel15, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(629, 154);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lbl_ProgressDetail
            // 
            this.lbl_ProgressDetail.AutoSize = true;
            this.lbl_ProgressDetail.BackColor = System.Drawing.Color.Transparent;
            this.lbl_ProgressDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_ProgressDetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lbl_ProgressDetail.Location = new System.Drawing.Point(0, 92);
            this.lbl_ProgressDetail.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_ProgressDetail.Name = "lbl_ProgressDetail";
            this.lbl_ProgressDetail.Size = new System.Drawing.Size(629, 62);
            this.lbl_ProgressDetail.TabIndex = 72;
            this.lbl_ProgressDetail.Text = "...";
            this.lbl_ProgressDetail.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_ProgressTitle
            // 
            this.lbl_ProgressTitle.AutoSize = true;
            this.lbl_ProgressTitle.BackColor = System.Drawing.Color.Transparent;
            this.lbl_ProgressTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_ProgressTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lbl_ProgressTitle.Location = new System.Drawing.Point(0, 0);
            this.lbl_ProgressTitle.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_ProgressTitle.Name = "lbl_ProgressTitle";
            this.lbl_ProgressTitle.Size = new System.Drawing.Size(629, 61);
            this.lbl_ProgressTitle.TabIndex = 70;
            this.lbl_ProgressTitle.Text = "...";
            this.lbl_ProgressTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel15
            // 
            this.panel15.Controls.Add(this.pB_SubProgress);
            this.panel15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.panel15.Location = new System.Drawing.Point(0, 61);
            this.panel15.Margin = new System.Windows.Forms.Padding(0);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(629, 31);
            this.panel15.TabIndex = 71;
            // 
            // pB_SubProgress
            // 
            this.pB_SubProgress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(198)))), ((int)(((byte)(170)))));
            this.pB_SubProgress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pB_SubProgress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.pB_SubProgress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.pB_SubProgress.Location = new System.Drawing.Point(0, 0);
            this.pB_SubProgress.Margin = new System.Windows.Forms.Padding(0);
            this.pB_SubProgress.Name = "pB_SubProgress";
            this.pB_SubProgress.Size = new System.Drawing.Size(629, 31);
            this.pB_SubProgress.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.BackgroundImage = global::Celeste_Launcher_Gui.Properties.Resources.DialogBox;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel2.Controls.Add(this.pictureBoxButtonCustom1);
            this.panel2.Controls.Add(this.tableLayoutPanel2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(693, 421);
            this.panel2.TabIndex = 14;
            // 
            // pictureBoxButtonCustom1
            // 
            this.pictureBoxButtonCustom1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxButtonCustom1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxButtonCustom1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxButtonCustom1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.pictureBoxButtonCustom1.HoverImage = global::Celeste_Launcher_Gui.Properties.Resources.XButton_Hover;
            this.pictureBoxButtonCustom1.Image = global::Celeste_Launcher_Gui.Properties.Resources.XButton_Normal;
            this.pictureBoxButtonCustom1.Location = new System.Drawing.Point(646, 12);
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
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.panel5, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel14, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.panel13, 0, 2);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(12, 35);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(669, 374);
            this.tableLayoutPanel2.TabIndex = 68;
            // 
            // GameScanProgressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(693, 421);
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
            this.panel5.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel13.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel15.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxButtonCustom1)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel14;
        private System.Windows.Forms.ProgressBar pB_GlobalProgress;
        private System.Windows.Forms.TextBox tB_Report;
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.Label lbl_GlobalProgress;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lbl_ProgressDetail;
        private System.Windows.Forms.Label lbl_ProgressTitle;
        private System.Windows.Forms.Panel panel15;
        private System.Windows.Forms.ProgressBar pB_SubProgress;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Celeste_AOEO_Controls.PictureBoxButtonCustom pictureBoxButtonCustom1;
    }
}