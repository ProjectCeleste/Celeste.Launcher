namespace PopupTest
{
    partial class SearchControl
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
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
                if (searchProviders != null)
                {
                    searchProviders.Dispose();
                    searchProviders = null;
                }
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonDropDown = new System.Windows.Forms.Button();
            this.textBox = new System.Windows.Forms.TextBox();
            this.borderLabel = new System.Windows.Forms.Label();
            this.buttonGo = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonDropDown
            // 
            this.buttonDropDown.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonDropDown.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonDropDown.Font = new System.Drawing.Font("Marlett", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.buttonDropDown.Location = new System.Drawing.Point(130, 0);
            this.buttonDropDown.Name = "buttonDropDown";
            this.buttonDropDown.Size = new System.Drawing.Size(20, 25);
            this.buttonDropDown.TabIndex = 3;
            this.buttonDropDown.Text = "u";
            this.buttonDropDown.UseVisualStyleBackColor = true;
            this.buttonDropDown.Click += new System.EventHandler(this.buttonDropDown_Click);
            // 
            // textBox
            // 
            this.textBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox.BackColor = System.Drawing.SystemColors.Window;
            this.textBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox.Location = new System.Drawing.Point(3, 5);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(104, 15);
            this.textBox.TabIndex = 1;
            this.textBox.Visible = false;
            this.textBox.Enter += new System.EventHandler(this.textBox_Enter);
            this.textBox.Leave += new System.EventHandler(this.textBox_Leave);
            this.textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // borderLabel
            // 
            this.borderLabel.BackColor = System.Drawing.SystemColors.Window;
            this.borderLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.borderLabel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.borderLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.borderLabel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.borderLabel.ForeColor = System.Drawing.SystemColors.GrayText;
            this.borderLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.borderLabel.Location = new System.Drawing.Point(0, 0);
            this.borderLabel.Name = "borderLabel";
            this.borderLabel.Size = new System.Drawing.Size(110, 25);
            this.borderLabel.TabIndex = 0;
            this.borderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.borderLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.borderLabel_MouseDown);
            // 
            // buttonGo
            // 
            this.buttonGo.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonGo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonGo.Font = new System.Drawing.Font("Marlett", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.buttonGo.Location = new System.Drawing.Point(110, 0);
            this.buttonGo.Name = "buttonGo";
            this.buttonGo.Size = new System.Drawing.Size(20, 25);
            this.buttonGo.TabIndex = 2;
            this.buttonGo.Text = "4";
            this.buttonGo.UseVisualStyleBackColor = true;
            this.buttonGo.Click += new System.EventHandler(this.fireSearch_Event);
            // 
            // SearchControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.borderLabel);
            this.Controls.Add(this.buttonGo);
            this.Controls.Add(this.buttonDropDown);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Name = "SearchControl";
            this.Size = new System.Drawing.Size(150, 25);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonDropDown;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.Label borderLabel;
        private System.Windows.Forms.Button buttonGo;
    }
}
