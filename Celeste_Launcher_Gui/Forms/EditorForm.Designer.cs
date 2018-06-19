namespace Celeste_Launcher_Gui.Forms
{
    partial class EditorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditorForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lbl_Mail = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Btn_Install_Editor = new Celeste_AOEO_Controls.CustomBtn();
            this.btn_Browse = new Celeste_AOEO_Controls.CustomBtn();
            this.pictureBoxButtonCustom1 = new Celeste_AOEO_Controls.PictureBoxButtonCustom();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxButtonCustom1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = global::Celeste_Launcher_Gui.Properties.Resources.DialogBoxSmall;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.tableLayoutPanel3);
            this.panel1.Controls.Add(this.pictureBoxButtonCustom1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(511, 288);
            this.panel1.TabIndex = 71;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel1, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.lbl_Mail, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.panel2, 0, 2);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(12, 39);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(487, 237);
            this.tableLayoutPanel3.TabIndex = 71;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 135F));
            this.tableLayoutPanel1.Controls.Add(this.Btn_Install_Editor, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 97);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(487, 50);
            this.tableLayoutPanel1.TabIndex = 69;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Segoe UI Black", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label2.ForeColor = System.Drawing.Color.Chocolate;
            this.label2.Location = new System.Drawing.Point(61, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(288, 50);
            this.label2.TabIndex = 77;
            this.label2.Text = "Unknow";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Segoe UI Black", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label6.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label6.Location = new System.Drawing.Point(3, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 50);
            this.label6.TabIndex = 88;
            this.label6.Text = "State";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_Mail
            // 
            this.lbl_Mail.AutoSize = true;
            this.lbl_Mail.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Mail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Mail.Font = new System.Drawing.Font("Segoe UI Black", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Mail.Location = new System.Drawing.Point(3, 0);
            this.lbl_Mail.Name = "lbl_Mail";
            this.lbl_Mail.Size = new System.Drawing.Size(481, 97);
            this.lbl_Mail.TabIndex = 67;
            this.lbl_Mail.Text = "Use the \"Game Editor\" to start designing map for offline playing and maybe see th" +
    "em include in \"Celeste Fan Project\".";
            this.lbl_Mail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btn_Browse);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 162);
            this.panel2.Margin = new System.Windows.Forms.Padding(0, 15, 0, 15);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(487, 60);
            this.panel2.TabIndex = 68;
            // 
            // Btn_Install_Editor
            // 
            this.Btn_Install_Editor.BackColor = System.Drawing.Color.Transparent;
            this.Btn_Install_Editor.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Btn_Install_Editor.BackgroundImage")));
            this.Btn_Install_Editor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Btn_Install_Editor.BtnText = "Install";
            this.Btn_Install_Editor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Btn_Install_Editor.Enabled = false;
            this.Btn_Install_Editor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Btn_Install_Editor.Location = new System.Drawing.Point(357, 5);
            this.Btn_Install_Editor.Margin = new System.Windows.Forms.Padding(5);
            this.Btn_Install_Editor.Name = "Btn_Install_Editor";
            this.Btn_Install_Editor.Size = new System.Drawing.Size(125, 40);
            this.Btn_Install_Editor.TabIndex = 89;
            this.Btn_Install_Editor.Click += new System.EventHandler(this.Btn_Install_Editor_Click);
            // 
            // btn_Browse
            // 
            this.btn_Browse.BackColor = System.Drawing.Color.Transparent;
            this.btn_Browse.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_Browse.BackgroundImage")));
            this.btn_Browse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_Browse.BtnText = "Run Game Editor";
            this.btn_Browse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Browse.Enabled = false;
            this.btn_Browse.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btn_Browse.Location = new System.Drawing.Point(0, 0);
            this.btn_Browse.Margin = new System.Windows.Forms.Padding(0);
            this.btn_Browse.Name = "btn_Browse";
            this.btn_Browse.Size = new System.Drawing.Size(487, 60);
            this.btn_Browse.TabIndex = 16;
            this.btn_Browse.Click += new System.EventHandler(this.Btn_RunEditor_Click);
            // 
            // pictureBoxButtonCustom1
            // 
            this.pictureBoxButtonCustom1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxButtonCustom1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxButtonCustom1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxButtonCustom1.DisabledImage = null;
            this.pictureBoxButtonCustom1.HoverImage = global::Celeste_Launcher_Gui.Properties.Resources.XButton_Hover;
            this.pictureBoxButtonCustom1.Image = global::Celeste_Launcher_Gui.Properties.Resources.XButton_Normal;
            this.pictureBoxButtonCustom1.Location = new System.Drawing.Point(469, 10);
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
            // EditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(511, 288);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditorForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Celeste Fan Project";
            this.Load += new System.EventHandler(this.EditorForm_Load);
            this.Shown += new System.EventHandler(this.EditorForm_Shown);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxButtonCustom1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Celeste_AOEO_Controls.PictureBoxButtonCustom pictureBoxButtonCustom1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label lbl_Mail;
        private System.Windows.Forms.Panel panel2;
        private Celeste_AOEO_Controls.CustomBtn btn_Browse;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private Celeste_AOEO_Controls.CustomBtn Btn_Install_Editor;
    }
}