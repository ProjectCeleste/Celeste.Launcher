namespace Celeste_Launcher_Gui.Forms
{
    partial class NetworkDeviceSelectionDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NetworkDeviceSelectionDialog));
            this.lbl_selectnatdevice = new System.Windows.Forms.Label();
            this.mainContainer1 = new Celeste_AOEO_Controls.MainContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSmall1 = new Celeste_AOEO_Controls.CustomBtn();
            this.btnSmall2 = new Celeste_AOEO_Controls.CustomBtn();
            this.lv_NetInterface = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_selectnatdevice
            // 
            this.lbl_selectnatdevice.AutoSize = true;
            this.lbl_selectnatdevice.Font = new System.Drawing.Font("Ashley Crawford CG", 14.25F);
            this.lbl_selectnatdevice.Location = new System.Drawing.Point(12, 5);
            this.lbl_selectnatdevice.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_selectnatdevice.Name = "lbl_selectnatdevice";
            this.lbl_selectnatdevice.Size = new System.Drawing.Size(274, 30);
            this.lbl_selectnatdevice.TabIndex = 1;
            this.lbl_selectnatdevice.Text = "Select Network Device:";
            // 
            // mainContainer1
            // 
            this.mainContainer1.BackColor = System.Drawing.Color.Transparent;
            this.mainContainer1.CloseButton = true;
            this.mainContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainContainer1.Font = new System.Drawing.Font("Ashley Crawford CG", 8.25F);
            this.mainContainer1.Location = new System.Drawing.Point(0, 0);
            this.mainContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.mainContainer1.MinimizeBox = false;
            this.mainContainer1.MinimumSize = new System.Drawing.Size(310, 250);
            this.mainContainer1.Name = "mainContainer1";
            this.mainContainer1.Size = new System.Drawing.Size(751, 323);
            this.mainContainer1.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = global::Celeste_Launcher_Gui.Properties.Resources.BackgroundTexture;
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Font = new System.Drawing.Font("Ashley Crawford CG", 8.25F);
            this.panel1.Location = new System.Drawing.Point(31, 77);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(685, 219);
            this.panel1.TabIndex = 5;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.Controls.Add(this.lbl_selectnatdevice, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lv_NetInterface, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Font = new System.Drawing.Font("Ashley Crawford CG", 8.25F);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(685, 219);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnSmall1);
            this.panel2.Controls.Add(this.btnSmall2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Font = new System.Drawing.Font("Ashley Crawford CG", 8.25F);
            this.panel2.Location = new System.Drawing.Point(525, 40);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.panel2.Size = new System.Drawing.Size(150, 174);
            this.panel2.TabIndex = 2;
            // 
            // btnSmall1
            // 
            this.btnSmall1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSmall1.BackgroundImage")));
            this.btnSmall1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSmall1.BtnText = "Refresh";
            this.btnSmall1.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSmall1.Font = new System.Drawing.Font("Ashley Crawford CG", 8.25F);
            this.btnSmall1.Location = new System.Drawing.Point(5, 0);
            this.btnSmall1.Margin = new System.Windows.Forms.Padding(0);
            this.btnSmall1.Name = "btnSmall1";
            this.btnSmall1.Size = new System.Drawing.Size(140, 82);
            this.btnSmall1.TabIndex = 4;
            this.btnSmall1.Click += new System.EventHandler(this.Bnt_refresh_Click);
            // 
            // btnSmall2
            // 
            this.btnSmall2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSmall2.BackgroundImage")));
            this.btnSmall2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSmall2.BtnText = "Ok";
            this.btnSmall2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnSmall2.Font = new System.Drawing.Font("Ashley Crawford CG", 8.25F);
            this.btnSmall2.Location = new System.Drawing.Point(5, 92);
            this.btnSmall2.Margin = new System.Windows.Forms.Padding(0);
            this.btnSmall2.Name = "btnSmall2";
            this.btnSmall2.Size = new System.Drawing.Size(140, 82);
            this.btnSmall2.TabIndex = 5;
            this.btnSmall2.Click += new System.EventHandler(this.Bnt_ok_Click);
            // 
            // lv_NetInterface
            // 
            this.lv_NetInterface.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lv_NetInterface.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lv_NetInterface.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lv_NetInterface.Font = new System.Drawing.Font("Ashley Crawford CG", 8.25F);
            this.lv_NetInterface.Location = new System.Drawing.Point(13, 43);
            this.lv_NetInterface.MultiSelect = false;
            this.lv_NetInterface.Name = "lv_NetInterface";
            this.lv_NetInterface.Size = new System.Drawing.Size(509, 168);
            this.lv_NetInterface.TabIndex = 3;
            this.lv_NetInterface.UseCompatibleStateImageBehavior = false;
            this.lv_NetInterface.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 294;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Ip";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 169;
            // 
            // NetworkDeviceSelectionDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(751, 323);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mainContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "NetworkDeviceSelectionDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Project Celeste -- Select Network Device";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NetworkDeviceSelectionDialog_FormClosing);
            this.Load += new System.EventHandler(this.NetworkDeviceSelectionDialog_Load);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lbl_selectnatdevice;
        private Celeste_AOEO_Controls.MainContainer mainContainer1;
        private System.Windows.Forms.Panel panel1;
        private Celeste_AOEO_Controls.CustomBtn btnSmall2;
        private Celeste_AOEO_Controls.CustomBtn btnSmall1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ListView lv_NetInterface;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
    }
}