namespace Celeste_Launcher_Gui.Forms
{
    partial class ManageInviteForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManageInviteForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lv_AvInvite = new System.Windows.Forms.ListView();
            this.ch_InviteNum = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lv_UsedInvite = new System.Windows.Forms.ListView();
            this.ch_InviteId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch_UsedBy = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.linkLabel1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.lv_AvInvite);
            this.groupBox1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(456, 130);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Available invite(s)";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Enabled = false;
            this.linkLabel1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.Location = new System.Drawing.Point(379, 17);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(58, 15);
            this.linkLabel1.TabIndex = 20;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Get more";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(169, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Invite Code:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(172, 41);
            this.textBox1.MaxLength = 128;
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(265, 79);
            this.textBox1.TabIndex = 2;
            // 
            // lv_AvInvite
            // 
            this.lv_AvInvite.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lv_AvInvite.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ch_InviteNum});
            this.lv_AvInvite.Dock = System.Windows.Forms.DockStyle.Left;
            this.lv_AvInvite.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lv_AvInvite.FullRowSelect = true;
            this.lv_AvInvite.Location = new System.Drawing.Point(3, 17);
            this.lv_AvInvite.MultiSelect = false;
            this.lv_AvInvite.Name = "lv_AvInvite";
            this.lv_AvInvite.Size = new System.Drawing.Size(149, 110);
            this.lv_AvInvite.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lv_AvInvite.TabIndex = 1;
            this.lv_AvInvite.UseCompatibleStateImageBehavior = false;
            this.lv_AvInvite.View = System.Windows.Forms.View.Details;
            this.lv_AvInvite.SelectedIndexChanged += new System.EventHandler(this.lv_AvInvite_SelectedIndexChanged);
            // 
            // ch_InviteNum
            // 
            this.ch_InviteNum.Text = "# Id";
            this.ch_InviteNum.Width = 112;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lv_UsedInvite);
            this.groupBox2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(12, 159);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(456, 130);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Used invite(s)";
            // 
            // lv_UsedInvite
            // 
            this.lv_UsedInvite.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lv_UsedInvite.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ch_InviteId,
            this.ch_UsedBy});
            this.lv_UsedInvite.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lv_UsedInvite.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lv_UsedInvite.FullRowSelect = true;
            this.lv_UsedInvite.Location = new System.Drawing.Point(3, 20);
            this.lv_UsedInvite.Name = "lv_UsedInvite";
            this.lv_UsedInvite.Size = new System.Drawing.Size(450, 107);
            this.lv_UsedInvite.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lv_UsedInvite.TabIndex = 0;
            this.lv_UsedInvite.UseCompatibleStateImageBehavior = false;
            this.lv_UsedInvite.View = System.Windows.Forms.View.Details;
            // 
            // ch_InviteId
            // 
            this.ch_InviteId.Text = "# Id";
            this.ch_InviteId.Width = 209;
            // 
            // ch_UsedBy
            // 
            this.ch_UsedBy.Text = "Used By";
            this.ch_UsedBy.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ch_UsedBy.Width = 186;
            // 
            // ManageInviteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(483, 301);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ManageInviteForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Project Celeste -- Manage Invite";
            this.TopMost = true;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ListView lv_AvInvite;
        private System.Windows.Forms.ColumnHeader ch_InviteNum;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView lv_UsedInvite;
        private System.Windows.Forms.ColumnHeader ch_InviteId;
        private System.Windows.Forms.ColumnHeader ch_UsedBy;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}