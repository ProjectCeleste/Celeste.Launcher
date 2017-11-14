namespace Celeste_Launcher_Gui.Forms
{
    partial class WindowsFeaturesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WindowsFeaturesForm));
            this.label1 = new System.Windows.Forms.Label();
            this.panel12 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBoxButtonCustom1 = new Celeste_AOEO_Controls.PictureBoxButtonCustom();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btn_FixDirectPlay = new Celeste_AOEO_Controls.CustomBtn();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_FixNet35 = new Celeste_AOEO_Controls.CustomBtn();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.l_DirectPlayState = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxButtonCustom1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI Black", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label1.Location = new System.Drawing.Point(93, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(464, 100);
            this.label1.TabIndex = 68;
            this.label1.Text = "Windows Features";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel12
            // 
            this.panel12.BackgroundImage = global::Celeste_Launcher_Gui.Properties.Resources.BarSeparator;
            this.panel12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.panel12.Location = new System.Drawing.Point(0, 100);
            this.panel12.Margin = new System.Windows.Forms.Padding(0);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(560, 27);
            this.panel12.TabIndex = 67;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.BackgroundImage = global::Celeste_Launcher_Gui.Properties.Resources.DialogBox;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel2.Controls.Add(this.pictureBoxButtonCustom1);
            this.panel2.Controls.Add(this.tableLayoutPanel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(592, 357);
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
            this.pictureBoxButtonCustom1.Location = new System.Drawing.Point(545, 13);
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
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel12, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(16, 13);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(560, 324);
            this.tableLayoutPanel1.TabIndex = 66;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.pictureBox1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label1, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(560, 100);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.pictureBox1.Image = global::Celeste_Launcher_Gui.Properties.Resources.CelesteWebsiteNormal;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(90, 100);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 75;
            this.pictureBox1.TabStop = false;
            // 
            // btn_FixDirectPlay
            // 
            this.btn_FixDirectPlay.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_FixDirectPlay.BackgroundImage")));
            this.btn_FixDirectPlay.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_FixDirectPlay.BtnText = "Fix";
            this.btn_FixDirectPlay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_FixDirectPlay.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btn_FixDirectPlay.Location = new System.Drawing.Point(460, 0);
            this.btn_FixDirectPlay.Margin = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.btn_FixDirectPlay.Name = "btn_FixDirectPlay";
            this.btn_FixDirectPlay.Size = new System.Drawing.Size(95, 98);
            this.btn_FixDirectPlay.TabIndex = 16;
            this.btn_FixDirectPlay.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Segoe UI Black", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label3.ForeColor = System.Drawing.Color.Chocolate;
            this.label3.Location = new System.Drawing.Point(113, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(344, 99);
            this.label3.TabIndex = 68;
            this.label3.Text = "Unknow";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btn_FixNet35
            // 
            this.btn_FixNet35.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_FixNet35.BackgroundImage")));
            this.btn_FixNet35.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_FixNet35.BtnText = "Fix";
            this.btn_FixNet35.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_FixNet35.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btn_FixNet35.Location = new System.Drawing.Point(460, 98);
            this.btn_FixNet35.Margin = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.btn_FixNet35.Name = "btn_FixNet35";
            this.btn_FixNet35.Size = new System.Drawing.Size(95, 99);
            this.btn_FixNet35.TabIndex = 71;
            this.btn_FixNet35.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Segoe UI Black", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label4.Location = new System.Drawing.Point(5, 0);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 98);
            this.label4.TabIndex = 70;
            this.label4.Text = "Direct Play:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Segoe UI Black", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label2.Location = new System.Drawing.Point(5, 98);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 99);
            this.label2.TabIndex = 69;
            this.label2.Text = ".Net 3.5:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // l_DirectPlayState
            // 
            this.l_DirectPlayState.AutoSize = true;
            this.l_DirectPlayState.BackColor = System.Drawing.Color.Transparent;
            this.l_DirectPlayState.Dock = System.Windows.Forms.DockStyle.Fill;
            this.l_DirectPlayState.Font = new System.Drawing.Font("Segoe UI Black", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.l_DirectPlayState.ForeColor = System.Drawing.Color.Chocolate;
            this.l_DirectPlayState.Location = new System.Drawing.Point(113, 0);
            this.l_DirectPlayState.Name = "l_DirectPlayState";
            this.l_DirectPlayState.Size = new System.Drawing.Size(344, 98);
            this.l_DirectPlayState.TabIndex = 76;
            this.l_DirectPlayState.Text = "Unknow";
            this.l_DirectPlayState.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel3.Controls.Add(this.l_DirectPlayState, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.btn_FixNet35, 2, 1);
            this.tableLayoutPanel3.Controls.Add(this.label3, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.btn_FixDirectPlay, 2, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 127);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(560, 197);
            this.tableLayoutPanel3.TabIndex = 68;
            // 
            // HelpCenterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(592, 357);
            this.ControlBox = false;
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HelpCenterForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Celeste Fan Project";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.GameScan_Load);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxButtonCustom1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Celeste_AOEO_Controls.PictureBoxButtonCustom pictureBoxButtonCustom1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label l_DirectPlayState;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private Celeste_AOEO_Controls.CustomBtn btn_FixNet35;
        private System.Windows.Forms.Label label3;
        private Celeste_AOEO_Controls.CustomBtn btn_FixDirectPlay;
    }
}