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
            this.lb_netinterfaces = new System.Windows.Forms.ListBox();
            this.lbl_selectnatdevice = new System.Windows.Forms.Label();
            this.mainContainer1 = new Celeste_Launcher_Gui.Controls.MainContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSmall1 = new Celeste_Launcher_Gui.Controls.BtnSmall();
            this.btnSmall2 = new Celeste_Launcher_Gui.Controls.BtnSmall();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lb_netinterfaces
            // 
            this.lb_netinterfaces.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_netinterfaces.FormattingEnabled = true;
            this.lb_netinterfaces.ItemHeight = 15;
            this.lb_netinterfaces.Location = new System.Drawing.Point(23, 39);
            this.lb_netinterfaces.Margin = new System.Windows.Forms.Padding(2);
            this.lb_netinterfaces.Name = "lb_netinterfaces";
            this.lb_netinterfaces.ScrollAlwaysVisible = true;
            this.lb_netinterfaces.Size = new System.Drawing.Size(390, 169);
            this.lb_netinterfaces.TabIndex = 0;
            // 
            // lbl_selectnatdevice
            // 
            this.lbl_selectnatdevice.AutoSize = true;
            this.lbl_selectnatdevice.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_selectnatdevice.Location = new System.Drawing.Point(20, 12);
            this.lbl_selectnatdevice.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_selectnatdevice.Name = "lbl_selectnatdevice";
            this.lbl_selectnatdevice.Size = new System.Drawing.Size(155, 16);
            this.lbl_selectnatdevice.TabIndex = 1;
            this.lbl_selectnatdevice.Text = "Select Network Device:";
            // 
            // mainContainer1
            // 
            this.mainContainer1.BackColor = System.Drawing.Color.Transparent;
            this.mainContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
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
            this.panel1.Controls.Add(this.btnSmall2);
            this.panel1.Controls.Add(this.btnSmall1);
            this.panel1.Controls.Add(this.lbl_selectnatdevice);
            this.panel1.Controls.Add(this.lb_netinterfaces);
            this.panel1.Location = new System.Drawing.Point(31, 77);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(685, 219);
            this.panel1.TabIndex = 5;
            // 
            // btnSmall1
            // 
            this.btnSmall1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSmall1.BackgroundImage")));
            this.btnSmall1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSmall1.BtnText = "Refresh";
            this.btnSmall1.Location = new System.Drawing.Point(467, 63);
            this.btnSmall1.Margin = new System.Windows.Forms.Padding(0);
            this.btnSmall1.Name = "btnSmall1";
            this.btnSmall1.Size = new System.Drawing.Size(172, 42);
            this.btnSmall1.TabIndex = 4;
            this.btnSmall1.Click += new System.EventHandler(this.Bnt_refresh_Click);
            // 
            // btnSmall2
            // 
            this.btnSmall2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSmall2.BackgroundImage")));
            this.btnSmall2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSmall2.BtnText = "Ok";
            this.btnSmall2.Location = new System.Drawing.Point(467, 131);
            this.btnSmall2.Margin = new System.Windows.Forms.Padding(0);
            this.btnSmall2.Name = "btnSmall2";
            this.btnSmall2.Size = new System.Drawing.Size(172, 42);
            this.btnSmall2.TabIndex = 5;
            this.btnSmall2.Click += new System.EventHandler(this.Bnt_ok_Click);
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
            this.Text = "Project Celeste -- Select Network Device";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NetworkDeviceSelectionDialog_FormClosing);
            this.Load += new System.EventHandler(this.NetworkDeviceSelectionDialog_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lb_netinterfaces;
        private System.Windows.Forms.Label lbl_selectnatdevice;
        private Controls.MainContainer mainContainer1;
        private System.Windows.Forms.Panel panel1;
        private Controls.BtnSmall btnSmall2;
        private Controls.BtnSmall btnSmall1;
    }
}