namespace Celeste_Launcher_Gui.Forms
{
    partial class OfflineModeForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OfflineModeForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.Btn_Install_Editor = new Celeste_AOEO_Controls.CustomBtn();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.customBtn6 = new Celeste_AOEO_Controls.CustomBtn();
            this.customBtn7 = new Celeste_AOEO_Controls.CustomBtn();
            this.btnShow1 = new Celeste_AOEO_Controls.CustomBtn();
            this.customBtn4 = new Celeste_AOEO_Controls.CustomBtn();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.btn_Editor = new Celeste_AOEO_Controls.CustomBtn();
            this.btnPlay = new Celeste_AOEO_Controls.CustomBtn();
            this.pictureBoxButtonCustom1 = new Celeste_AOEO_Controls.PictureBoxButtonCustom();
            this.folderListener = new System.IO.FileSystemWatcher();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxButtonCustom1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.folderListener)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = global::Celeste_Launcher_Gui.Properties.Resources.DialogBox1;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.Btn_Install_Editor);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.customBtn6);
            this.panel1.Controls.Add(this.customBtn7);
            this.panel1.Controls.Add(this.btnShow1);
            this.panel1.Controls.Add(this.customBtn4);
            this.panel1.Controls.Add(this.listBox1);
            this.panel1.Controls.Add(this.btn_Editor);
            this.panel1.Controls.Add(this.btnPlay);
            this.panel1.Controls.Add(this.pictureBoxButtonCustom1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(587, 512);
            this.panel1.TabIndex = 71;
            // 
            // Btn_Install_Editor
            // 
            this.Btn_Install_Editor.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Btn_Install_Editor.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Btn_Install_Editor.BackgroundImage")));
            this.Btn_Install_Editor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Btn_Install_Editor.BtnText = "Install";
            this.Btn_Install_Editor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Install_Editor.Location = new System.Drawing.Point(65, 448);
            this.Btn_Install_Editor.Margin = new System.Windows.Forms.Padding(0);
            this.Btn_Install_Editor.Name = "Btn_Install_Editor";
            this.Btn_Install_Editor.Size = new System.Drawing.Size(143, 34);
            this.Btn_Install_Editor.TabIndex = 85;
            this.Btn_Install_Editor.Click += new System.EventHandler(this.Btn_Install_Editor_Click);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Cursor = System.Windows.Forms.Cursors.Help;
            this.label4.Font = new System.Drawing.Font("Ashley Crawford CG", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(64, 426);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(126, 25);
            this.label4.TabIndex = 86;
            this.label4.Text = "Editor State";
            this.toolTip1.SetToolTip(this.label4, "✓ = Editor is installed\r\n✕ = Editor not installed/outdated\r\n? = Unable to check s" +
        "tatus (Network Error)");
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Cursor = System.Windows.Forms.Cursors.Help;
            this.label2.Font = new System.Drawing.Font("Ashley Crawford CG", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label2.Location = new System.Drawing.Point(185, 426);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 25);
            this.label2.TabIndex = 86;
            this.label2.Text = "?";
            this.toolTip1.SetToolTip(this.label2, "✓ = Editor is installed\r\n✕ = Editor not installed/outdated\r\n? = Unable to check s" +
        "tatus (Network Error)");
            // 
            // label1
            // 
            this.label1.Cursor = System.Windows.Forms.Cursors.Default;
            this.label1.Font = new System.Drawing.Font("Ashley Crawford CG", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(24, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(228, 23);
            this.label1.TabIndex = 96;
            this.label1.Text = "Scenarios";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // customBtn6
            // 
            this.customBtn6.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customBtn6.BackgroundImage")));
            this.customBtn6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.customBtn6.BtnText = "Help";
            this.customBtn6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customBtn6.Location = new System.Drawing.Point(299, 207);
            this.customBtn6.Margin = new System.Windows.Forms.Padding(0);
            this.customBtn6.Name = "customBtn6";
            this.customBtn6.Size = new System.Drawing.Size(258, 69);
            this.customBtn6.TabIndex = 93;
            this.customBtn6.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // customBtn7
            // 
            this.customBtn7.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customBtn7.BackgroundImage")));
            this.customBtn7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.customBtn7.BtnText = "Download Scenarios";
            this.customBtn7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customBtn7.Location = new System.Drawing.Point(299, 136);
            this.customBtn7.Margin = new System.Windows.Forms.Padding(0);
            this.customBtn7.Name = "customBtn7";
            this.customBtn7.Size = new System.Drawing.Size(258, 69);
            this.customBtn7.TabIndex = 92;
            this.customBtn7.Click += new System.EventHandler(this.btnDlMore_Click);
            // 
            // btnShow1
            // 
            this.btnShow1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnShow1.BackgroundImage")));
            this.btnShow1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnShow1.BtnText = "Show Folder";
            this.btnShow1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShow1.Location = new System.Drawing.Point(24, 354);
            this.btnShow1.Margin = new System.Windows.Forms.Padding(0);
            this.btnShow1.Name = "btnShow1";
            this.btnShow1.Size = new System.Drawing.Size(228, 54);
            this.btnShow1.TabIndex = 83;
            this.btnShow1.Click += new System.EventHandler(this.btnShow1_Click);
            // 
            // customBtn4
            // 
            this.customBtn4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customBtn4.BackgroundImage")));
            this.customBtn4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.customBtn4.BtnText = "Add Scenario";
            this.customBtn4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customBtn4.Location = new System.Drawing.Point(24, 295);
            this.customBtn4.Margin = new System.Windows.Forms.Padding(0);
            this.customBtn4.Name = "customBtn4";
            this.customBtn4.Size = new System.Drawing.Size(228, 54);
            this.customBtn4.TabIndex = 81;
            this.customBtn4.Click += new System.EventHandler(this.importScenario);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.HorizontalScrollbar = true;
            this.listBox1.ItemHeight = 15;
            this.listBox1.Location = new System.Drawing.Point(24, 48);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(228, 244);
            this.listBox1.TabIndex = 75;
            // 
            // btn_Editor
            // 
            this.btn_Editor.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_Editor.BackgroundImage")));
            this.btn_Editor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_Editor.BtnText = "Scenario Editor";
            this.btn_Editor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Editor.Location = new System.Drawing.Point(299, 426);
            this.btn_Editor.Margin = new System.Windows.Forms.Padding(0);
            this.btn_Editor.Name = "btn_Editor";
            this.btn_Editor.Size = new System.Drawing.Size(258, 69);
            this.btn_Editor.TabIndex = 74;
            this.btn_Editor.Click += new System.EventHandler(this.Btn_Browse_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPlay.BackgroundImage")));
            this.btnPlay.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnPlay.BtnText = "Play Scenarios";
            this.btnPlay.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPlay.Location = new System.Drawing.Point(299, 65);
            this.btnPlay.Margin = new System.Windows.Forms.Padding(0);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(258, 69);
            this.btnPlay.TabIndex = 72;
            this.toolTip1.SetToolTip(this.btnPlay, "Tooltip");
            this.btnPlay.Click += new System.EventHandler(this.btnOfflineLaunch);
            // 
            // pictureBoxButtonCustom1
            // 
            this.pictureBoxButtonCustom1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxButtonCustom1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxButtonCustom1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxButtonCustom1.HoverImage = global::Celeste_Launcher_Gui.Properties.Resources.XButton_Hover;
            this.pictureBoxButtonCustom1.Image = global::Celeste_Launcher_Gui.Properties.Resources.XButton_Normal;
            this.pictureBoxButtonCustom1.Location = new System.Drawing.Point(545, 10);
            this.pictureBoxButtonCustom1.Name = "pictureBoxButtonCustom1";
            this.pictureBoxButtonCustom1.NormalImage = global::Celeste_Launcher_Gui.Properties.Resources.XButton_Normal;
            this.pictureBoxButtonCustom1.ShowToolTip = true;
            this.pictureBoxButtonCustom1.Size = new System.Drawing.Size(32, 32);
            this.pictureBoxButtonCustom1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxButtonCustom1.TabIndex = 70;
            this.pictureBoxButtonCustom1.TabStop = false;
            this.pictureBoxButtonCustom1.ToolTipText = "Close";
            this.pictureBoxButtonCustom1.Click += new System.EventHandler(this.PictureBoxButtonCustom1_Click);
            // 
            // folderListener
            // 
            this.folderListener.EnableRaisingEvents = true;
            this.folderListener.SynchronizingObject = this;
            // 
            // OfflineModeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(587, 512);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OfflineModeForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Celeste Fan Project";
            this.Load += new System.EventHandler(this.EditorForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxButtonCustom1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.folderListener)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Celeste_AOEO_Controls.PictureBoxButtonCustom pictureBoxButtonCustom1;
        private System.Windows.Forms.Panel panel1;
        private Celeste_AOEO_Controls.CustomBtn btn_Editor;
        private Celeste_AOEO_Controls.CustomBtn btnPlay;
        private Celeste_AOEO_Controls.CustomBtn btnShow1;
        private Celeste_AOEO_Controls.CustomBtn customBtn4;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private Celeste_AOEO_Controls.CustomBtn Btn_Install_Editor;
        private Celeste_AOEO_Controls.CustomBtn customBtn7;
        private Celeste_AOEO_Controls.CustomBtn customBtn6;
        private System.IO.FileSystemWatcher folderListener;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}