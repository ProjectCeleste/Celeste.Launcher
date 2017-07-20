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
            this.lb_netinterfaces = new System.Windows.Forms.ListBox();
            this.lbl_selectnatdevice = new System.Windows.Forms.Label();
            this.bnt_ok = new System.Windows.Forms.Button();
            this.bnt_refresh = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lb_netinterfaces
            // 
            this.lb_netinterfaces.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_netinterfaces.FormattingEnabled = true;
            this.lb_netinterfaces.ItemHeight = 16;
            this.lb_netinterfaces.Location = new System.Drawing.Point(12, 29);
            this.lb_netinterfaces.Name = "lb_netinterfaces";
            this.lb_netinterfaces.Size = new System.Drawing.Size(384, 180);
            this.lb_netinterfaces.TabIndex = 0;
            // 
            // lbl_selectnatdevice
            // 
            this.lbl_selectnatdevice.AutoSize = true;
            this.lbl_selectnatdevice.Location = new System.Drawing.Point(9, 9);
            this.lbl_selectnatdevice.Name = "lbl_selectnatdevice";
            this.lbl_selectnatdevice.Size = new System.Drawing.Size(153, 17);
            this.lbl_selectnatdevice.TabIndex = 1;
            this.lbl_selectnatdevice.Text = "Select Network Device:";
            // 
            // bnt_ok
            // 
            this.bnt_ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bnt_ok.Location = new System.Drawing.Point(321, 224);
            this.bnt_ok.Name = "bnt_ok";
            this.bnt_ok.Size = new System.Drawing.Size(75, 23);
            this.bnt_ok.TabIndex = 2;
            this.bnt_ok.Text = "OK";
            this.bnt_ok.UseVisualStyleBackColor = true;
            this.bnt_ok.Click += new System.EventHandler(this.bnt_ok_Click);
            // 
            // bnt_refresh
            // 
            this.bnt_refresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bnt_refresh.Location = new System.Drawing.Point(240, 224);
            this.bnt_refresh.Name = "bnt_refresh";
            this.bnt_refresh.Size = new System.Drawing.Size(75, 23);
            this.bnt_refresh.TabIndex = 3;
            this.bnt_refresh.Text = "Refresh";
            this.bnt_refresh.UseVisualStyleBackColor = true;
            this.bnt_refresh.Click += new System.EventHandler(this.bnt_refresh_Click);
            // 
            // NetworkDeviceSelectionDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(408, 259);
            this.Controls.Add(this.bnt_refresh);
            this.Controls.Add(this.bnt_ok);
            this.Controls.Add(this.lbl_selectnatdevice);
            this.Controls.Add(this.lb_netinterfaces);
            this.Name = "NetworkDeviceSelectionDialog";
            this.Text = "Select Network Device";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NetworkDeviceSelectionDialog_FormClosing);
            this.Load += new System.EventHandler(this.NetworkDeviceSelectionDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lb_netinterfaces;
        private System.Windows.Forms.Label lbl_selectnatdevice;
        private System.Windows.Forms.Button bnt_ok;
        private System.Windows.Forms.Button bnt_refresh;
    }
}