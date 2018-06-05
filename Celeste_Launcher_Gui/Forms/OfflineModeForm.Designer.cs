﻿namespace Celeste_Launcher_Gui.Forms
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OfflineModeForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.Btn_Install_Editor = new Celeste_AOEO_Controls.CustomBtn();
            this.label2 = new System.Windows.Forms.Label();
            this.customBtn8 = new Celeste_AOEO_Controls.CustomBtn();
            this.customBtn6 = new Celeste_AOEO_Controls.CustomBtn();
            this.customBtn7 = new Celeste_AOEO_Controls.CustomBtn();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnShow2 = new Celeste_AOEO_Controls.CustomBtn();
            this.btnShow1 = new Celeste_AOEO_Controls.CustomBtn();
            this.customBtn5 = new Celeste_AOEO_Controls.CustomBtn();
            this.customBtn4 = new Celeste_AOEO_Controls.CustomBtn();
            this.customBtn3 = new Celeste_AOEO_Controls.CustomBtn();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.btn_Editor = new Celeste_AOEO_Controls.CustomBtn();
            this.btnPlay = new Celeste_AOEO_Controls.CustomBtn();
            this.pictureBoxButtonCustom1 = new Celeste_AOEO_Controls.PictureBoxButtonCustom();
            this.editorWatcher = new System.IO.FileSystemWatcher();
            this.playWatcher = new System.IO.FileSystemWatcher();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxButtonCustom1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.editorWatcher)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.playWatcher)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = global::Celeste_Launcher_Gui.Properties.Resources.DialogBox1;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Controls.Add(this.customBtn8);
            this.panel1.Controls.Add(this.customBtn6);
            this.panel1.Controls.Add(this.customBtn7);
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.btnShow2);
            this.panel1.Controls.Add(this.btnShow1);
            this.panel1.Controls.Add(this.customBtn5);
            this.panel1.Controls.Add(this.customBtn4);
            this.panel1.Controls.Add(this.customBtn3);
            this.panel1.Controls.Add(this.listBox2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.listBox1);
            this.panel1.Controls.Add(this.btn_Editor);
            this.panel1.Controls.Add(this.btnPlay);
            this.panel1.Controls.Add(this.pictureBoxButtonCustom1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(587, 651);
            this.panel1.TabIndex = 71;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.Btn_Install_Editor, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(155, 594);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(318, 34);
            this.tableLayoutPanel1.TabIndex = 95;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Ashley Crawford CG", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(126, 25);
            this.label4.TabIndex = 86;
            this.label4.Text = "Editor State";
            // 
            // Btn_Install_Editor
            // 
            this.Btn_Install_Editor.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Btn_Install_Editor.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Btn_Install_Editor.BackgroundImage")));
            this.Btn_Install_Editor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Btn_Install_Editor.BtnText = "Install";
            this.Btn_Install_Editor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Install_Editor.Location = new System.Drawing.Point(167, 0);
            this.Btn_Install_Editor.Margin = new System.Windows.Forms.Padding(0);
            this.Btn_Install_Editor.Name = "Btn_Install_Editor";
            this.Btn_Install_Editor.Size = new System.Drawing.Size(143, 34);
            this.Btn_Install_Editor.TabIndex = 85;
            this.Btn_Install_Editor.Click += new System.EventHandler(this.Btn_Install_Editor_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Ashley Crawford CG", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label2.Location = new System.Drawing.Point(135, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 25);
            this.label2.TabIndex = 86;
            this.label2.Text = "?";
            // 
            // customBtn8
            // 
            this.customBtn8.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customBtn8.BackgroundImage")));
            this.customBtn8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.customBtn8.BtnText = "Sync";
            this.customBtn8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customBtn8.Location = new System.Drawing.Point(239, 189);
            this.customBtn8.Margin = new System.Windows.Forms.Padding(0);
            this.customBtn8.Name = "customBtn8";
            this.customBtn8.Size = new System.Drawing.Size(105, 32);
            this.customBtn8.TabIndex = 94;
            this.customBtn8.Click += new System.EventHandler(this.btnSync);
            // 
            // customBtn6
            // 
            this.customBtn6.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customBtn6.BackgroundImage")));
            this.customBtn6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.customBtn6.BtnText = "Help";
            this.customBtn6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customBtn6.Location = new System.Drawing.Point(299, 516);
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
            this.customBtn7.Location = new System.Drawing.Point(24, 516);
            this.customBtn7.Margin = new System.Windows.Forms.Padding(0);
            this.customBtn7.Name = "customBtn7";
            this.customBtn7.Size = new System.Drawing.Size(258, 69);
            this.customBtn7.TabIndex = 92;
            this.customBtn7.Click += new System.EventHandler(this.btnDlMore_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Move",
            "Copy"});
            this.comboBox1.Location = new System.Drawing.Point(248, 122);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(87, 23);
            this.comboBox1.TabIndex = 91;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(263, 89);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 15);
            this.label7.TabIndex = 87;
            this.label7.Text = "Manage";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(353, 71);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(144, 15);
            this.label6.TabIndex = 87;
            this.label6.Text = "Scenario Files for Editing";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(34, 71);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(146, 15);
            this.label5.TabIndex = 87;
            this.label5.Text = "Scenario Files for Playing";
            // 
            // btnShow2
            // 
            this.btnShow2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnShow2.BackgroundImage")));
            this.btnShow2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnShow2.BtnText = "Show Folder";
            this.btnShow2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShow2.Location = new System.Drawing.Point(356, 368);
            this.btnShow2.Margin = new System.Windows.Forms.Padding(0);
            this.btnShow2.Name = "btnShow2";
            this.btnShow2.Size = new System.Drawing.Size(185, 45);
            this.btnShow2.TabIndex = 84;
            this.btnShow2.Click += new System.EventHandler(this.btnShow2_Click);
            // 
            // btnShow1
            // 
            this.btnShow1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnShow1.BackgroundImage")));
            this.btnShow1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnShow1.BtnText = "Show Folder";
            this.btnShow1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShow1.Location = new System.Drawing.Point(37, 368);
            this.btnShow1.Margin = new System.Windows.Forms.Padding(0);
            this.btnShow1.Name = "btnShow1";
            this.btnShow1.Size = new System.Drawing.Size(185, 45);
            this.btnShow1.TabIndex = 83;
            this.btnShow1.Click += new System.EventHandler(this.btnShow1_Click);
            // 
            // customBtn5
            // 
            this.customBtn5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customBtn5.BackgroundImage")));
            this.customBtn5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.customBtn5.BtnText = "Add Scenario";
            this.customBtn5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customBtn5.Location = new System.Drawing.Point(356, 321);
            this.customBtn5.Margin = new System.Windows.Forms.Padding(0);
            this.customBtn5.Name = "customBtn5";
            this.customBtn5.Size = new System.Drawing.Size(185, 45);
            this.customBtn5.TabIndex = 82;
            this.customBtn5.Click += new System.EventHandler(this.importToEditor);
            // 
            // customBtn4
            // 
            this.customBtn4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customBtn4.BackgroundImage")));
            this.customBtn4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.customBtn4.BtnText = "Add Scenario";
            this.customBtn4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customBtn4.Location = new System.Drawing.Point(37, 321);
            this.customBtn4.Margin = new System.Windows.Forms.Padding(0);
            this.customBtn4.Name = "customBtn4";
            this.customBtn4.Size = new System.Drawing.Size(185, 45);
            this.customBtn4.TabIndex = 81;
            this.customBtn4.Click += new System.EventHandler(this.importToOfflinePlayer);
            // 
            // customBtn3
            // 
            this.customBtn3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customBtn3.BackgroundImage")));
            this.customBtn3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.customBtn3.BtnText = "⇄";
            this.customBtn3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customBtn3.Location = new System.Drawing.Point(239, 148);
            this.customBtn3.Margin = new System.Windows.Forms.Padding(0);
            this.customBtn3.Name = "customBtn3";
            this.customBtn3.Size = new System.Drawing.Size(105, 32);
            this.customBtn3.TabIndex = 80;
            this.customBtn3.Click += new System.EventHandler(this.moveScen);
            // 
            // listBox2
            // 
            this.listBox2.BackColor = System.Drawing.SystemColors.Window;
            this.listBox2.FormattingEnabled = true;
            this.listBox2.HorizontalScrollbar = true;
            this.listBox2.ItemHeight = 15;
            this.listBox2.Location = new System.Drawing.Point(356, 89);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(185, 229);
            this.listBox2.TabIndex = 77;
            this.listBox2.Click += new System.EventHandler(this.clearListBox1);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Ashley Crawford CG", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(82, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 31);
            this.label3.TabIndex = 76;
            this.label3.Text = "Play";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Ashley Crawford CG", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(413, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 31);
            this.label1.TabIndex = 76;
            this.label1.Text = "Edit";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.HorizontalScrollbar = true;
            this.listBox1.ItemHeight = 15;
            this.listBox1.Location = new System.Drawing.Point(37, 89);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(185, 229);
            this.listBox1.TabIndex = 75;
            this.listBox1.Click += new System.EventHandler(this.clearListBox2);
            // 
            // btn_Editor
            // 
            this.btn_Editor.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_Editor.BackgroundImage")));
            this.btn_Editor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_Editor.BtnText = "Scenario Editor";
            this.btn_Editor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Editor.Location = new System.Drawing.Point(299, 436);
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
            this.btnPlay.Location = new System.Drawing.Point(24, 436);
            this.btnPlay.Margin = new System.Windows.Forms.Padding(0);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(258, 69);
            this.btnPlay.TabIndex = 72;
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
            // editorWatcher
            // 
            this.editorWatcher.EnableRaisingEvents = true;
            this.editorWatcher.SynchronizingObject = this;
            // 
            // playWatcher
            // 
            this.playWatcher.EnableRaisingEvents = true;
            this.playWatcher.SynchronizingObject = this;
            // 
            // OfflineModeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(587, 651);
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
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxButtonCustom1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.editorWatcher)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.playWatcher)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Celeste_AOEO_Controls.PictureBoxButtonCustom pictureBoxButtonCustom1;
        private System.Windows.Forms.Panel panel1;
        private Celeste_AOEO_Controls.CustomBtn btn_Editor;
        private Celeste_AOEO_Controls.CustomBtn btnPlay;
        private Celeste_AOEO_Controls.CustomBtn btnShow2;
        private Celeste_AOEO_Controls.CustomBtn btnShow1;
        private Celeste_AOEO_Controls.CustomBtn customBtn5;
        private Celeste_AOEO_Controls.CustomBtn customBtn4;
        private Celeste_AOEO_Controls.CustomBtn customBtn3;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private Celeste_AOEO_Controls.CustomBtn Btn_Install_Editor;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBox1;
        private Celeste_AOEO_Controls.CustomBtn customBtn7;
        private Celeste_AOEO_Controls.CustomBtn customBtn6;
        private Celeste_AOEO_Controls.CustomBtn customBtn8;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.IO.FileSystemWatcher editorWatcher;
        private System.IO.FileSystemWatcher playWatcher;
    }
}