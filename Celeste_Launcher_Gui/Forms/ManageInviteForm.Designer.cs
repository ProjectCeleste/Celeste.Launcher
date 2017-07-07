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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lv_AvInvite = new System.Windows.Forms.ListView();
            this.ch_InviteNum = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lv_UsedInvite = new System.Windows.Forms.ListView();
            this.ch_InviteId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch_UsedBy = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.mainContainer1 = new Celeste_Launcher_Gui.Controls.MainContainer();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(20, 258);
            this.textBox1.MaxLength = 128;
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(302, 91);
            this.textBox1.TabIndex = 2;
            // 
            // lv_AvInvite
            // 
            this.lv_AvInvite.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lv_AvInvite.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ch_InviteNum});
            this.lv_AvInvite.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lv_AvInvite.FullRowSelect = true;
            this.lv_AvInvite.Location = new System.Drawing.Point(20, 48);
            this.lv_AvInvite.MultiSelect = false;
            this.lv_AvInvite.Name = "lv_AvInvite";
            this.lv_AvInvite.Size = new System.Drawing.Size(302, 158);
            this.lv_AvInvite.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lv_AvInvite.TabIndex = 1;
            this.lv_AvInvite.UseCompatibleStateImageBehavior = false;
            this.lv_AvInvite.View = System.Windows.Forms.View.Details;
            this.lv_AvInvite.SelectedIndexChanged += new System.EventHandler(this.lv_AvInvite_SelectedIndexChanged);
            // 
            // ch_InviteNum
            // 
            this.ch_InviteNum.Text = "# Id";
            this.ch_InviteNum.Width = 250;
            // 
            // lv_UsedInvite
            // 
            this.lv_UsedInvite.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lv_UsedInvite.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ch_InviteId,
            this.ch_UsedBy});
            this.lv_UsedInvite.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lv_UsedInvite.FullRowSelect = true;
            this.lv_UsedInvite.Location = new System.Drawing.Point(20, 48);
            this.lv_UsedInvite.Name = "lv_UsedInvite";
            this.lv_UsedInvite.Size = new System.Drawing.Size(305, 301);
            this.lv_UsedInvite.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lv_UsedInvite.TabIndex = 0;
            this.lv_UsedInvite.UseCompatibleStateImageBehavior = false;
            this.lv_UsedInvite.View = System.Windows.Forms.View.Details;
            // 
            // ch_InviteId
            // 
            this.ch_InviteId.Text = "# Id";
            this.ch_InviteId.Width = 71;
            // 
            // ch_UsedBy
            // 
            this.ch_UsedBy.Text = "Used By";
            this.ch_UsedBy.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ch_UsedBy.Width = 186;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = global::Celeste_Launcher_Gui.Properties.Resources.BackgroundTexture;
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.lv_AvInvite);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Location = new System.Drawing.Point(26, 72);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(715, 375);
            this.panel1.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(17, 223);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(149, 18);
            this.label4.TabIndex = 60;
            this.label4.Text = "Selected invite code";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(17, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(130, 18);
            this.label3.TabIndex = 59;
            this.label3.Text = "Available invite(s)";
            // 
            // panel4
            // 
            this.panel4.BackgroundImage = global::Celeste_Launcher_Gui.Properties.Resources.BarSeparatorVertical;
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(343, 0);
            this.panel4.Margin = new System.Windows.Forms.Padding(0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(27, 375);
            this.panel4.TabIndex = 58;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.lv_UsedInvite);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(370, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(345, 375);
            this.panel3.TabIndex = 57;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(17, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 18);
            this.label2.TabIndex = 56;
            this.label2.Text = "Used invite(s)";
            // 
            // mainContainer1
            // 
            this.mainContainer1.BackColor = System.Drawing.Color.Transparent;
            this.mainContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainContainer1.Location = new System.Drawing.Point(0, 0);
            this.mainContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.mainContainer1.MinimumSize = new System.Drawing.Size(310, 250);
            this.mainContainer1.Name = "mainContainer1";
            this.mainContainer1.Size = new System.Drawing.Size(767, 470);
            this.mainContainer1.TabIndex = 3;
            // 
            // ManageInviteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(767, 470);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mainContainer1);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ManageInviteForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Project Celeste -- Manage Invite";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ManageInviteForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ListView lv_AvInvite;
        private System.Windows.Forms.ColumnHeader ch_InviteNum;
        private System.Windows.Forms.ListView lv_UsedInvite;
        private System.Windows.Forms.ColumnHeader ch_InviteId;
        private System.Windows.Forms.ColumnHeader ch_UsedBy;
        private System.Windows.Forms.Panel panel1;
        private Controls.MainContainer mainContainer1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
    }
}