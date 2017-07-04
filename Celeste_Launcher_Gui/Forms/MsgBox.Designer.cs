namespace Celeste_Launcher_Gui.Forms
{
    partial class MsgBox
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
            this.lb_Message = new System.Windows.Forms.Label();
            this.lb_Close = new System.Windows.Forms.Label();
            this.lb_Title = new System.Windows.Forms.Label();
            this.lb_OK = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lb_Message
            // 
            this.lb_Message.BackColor = System.Drawing.Color.Transparent;
            this.lb_Message.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Message.Location = new System.Drawing.Point(36, 83);
            this.lb_Message.Name = "lb_Message";
            this.lb_Message.Size = new System.Drawing.Size(372, 141);
            this.lb_Message.TabIndex = 0;
            this.lb_Message.Text = "Message";
            this.lb_Message.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lb_Close
            // 
            this.lb_Close.BackColor = System.Drawing.Color.Transparent;
            this.lb_Close.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Close.ForeColor = System.Drawing.Color.Black;
            this.lb_Close.Location = new System.Drawing.Point(379, 13);
            this.lb_Close.Name = "lb_Close";
            this.lb_Close.Size = new System.Drawing.Size(41, 38);
            this.lb_Close.TabIndex = 20;
            this.lb_Close.Text = "X";
            this.lb_Close.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lb_Title
            // 
            this.lb_Title.BackColor = System.Drawing.Color.Transparent;
            this.lb_Title.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.lb_Title.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Title.ForeColor = System.Drawing.Color.Gainsboro;
            this.lb_Title.Location = new System.Drawing.Point(12, 13);
            this.lb_Title.Name = "lb_Title";
            this.lb_Title.Size = new System.Drawing.Size(354, 38);
            this.lb_Title.TabIndex = 19;
            this.lb_Title.Text = "Project Celeste - Message";
            this.lb_Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lb_OK
            // 
            this.lb_OK.BackColor = System.Drawing.Color.Transparent;
            this.lb_OK.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_OK.ForeColor = System.Drawing.Color.White;
            this.lb_OK.Location = new System.Drawing.Point(149, 243);
            this.lb_OK.Name = "lb_OK";
            this.lb_OK.Size = new System.Drawing.Size(150, 36);
            this.lb_OK.TabIndex = 21;
            this.lb_OK.Text = "OK";
            this.lb_OK.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lb_OK.Click += new System.EventHandler(this.lb_OK_Click);
            // 
            // MsgBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Magenta;
            this.BackgroundImage = global::Celeste_Launcher_Gui.Properties.Resources.message;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(440, 310);
            this.ControlBox = false;
            this.Controls.Add(this.lb_OK);
            this.Controls.Add(this.lb_Close);
            this.Controls.Add(this.lb_Title);
            this.Controls.Add(this.lb_Message);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MsgBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MsgBox";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.Magenta;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lb_Message;
        private System.Windows.Forms.Label lb_Close;
        private System.Windows.Forms.Label lb_Title;
        private System.Windows.Forms.Label lb_OK;
    }
}