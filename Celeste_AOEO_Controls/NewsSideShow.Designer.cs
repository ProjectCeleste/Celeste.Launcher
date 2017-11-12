namespace Celeste_AOEO_Controls
{
    partial class NewsSideShow
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBoxButtonCustom1 = new Celeste_AOEO_Controls.PictureBoxButtonCustom();
            this.pictureBoxButtonCustom2 = new Celeste_AOEO_Controls.PictureBoxButtonCustom();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxButtonCustom1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxButtonCustom2)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 43F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(428, 247);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // panel3
            // 
            this.panel3.BackgroundImage = global::Celeste_AOEO_Controls.Properties.Resources.NewBoxHeader;
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Controls.Add(this.pictureBoxButtonCustom1);
            this.panel3.Controls.Add(this.pictureBoxButtonCustom2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(428, 43);
            this.panel3.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::Celeste_AOEO_Controls.Properties.Resources.NewBoxBody;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 43);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(428, 204);
            this.panel1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = global::Celeste_AOEO_Controls.Properties.Resources.ForumsNews;
            this.pictureBox1.Location = new System.Drawing.Point(7, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(415, 197);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.PictureBox1_Click);
            // 
            // pictureBoxButtonCustom1
            // 
            this.pictureBoxButtonCustom1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxButtonCustom1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBoxButtonCustom1.HoverImage = global::Celeste_AOEO_Controls.Properties.Resources.NewsPrevHover;
            this.pictureBoxButtonCustom1.Image = global::Celeste_AOEO_Controls.Properties.Resources.NewsPrevNormal;
            this.pictureBoxButtonCustom1.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxButtonCustom1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBoxButtonCustom1.Name = "pictureBoxButtonCustom1";
            this.pictureBoxButtonCustom1.NormalImage = global::Celeste_AOEO_Controls.Properties.Resources.NewsPrevNormal;
            this.pictureBoxButtonCustom1.ShowToolTip = true;
            this.pictureBoxButtonCustom1.Size = new System.Drawing.Size(24, 43);
            this.pictureBoxButtonCustom1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxButtonCustom1.TabIndex = 0;
            this.pictureBoxButtonCustom1.TabStop = false;
            this.pictureBoxButtonCustom1.ToolTipText = "Previous";
            // 
            // pictureBoxButtonCustom2
            // 
            this.pictureBoxButtonCustom2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxButtonCustom2.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBoxButtonCustom2.HoverImage = global::Celeste_AOEO_Controls.Properties.Resources.NewsNextHover;
            this.pictureBoxButtonCustom2.Image = global::Celeste_AOEO_Controls.Properties.Resources.NewsNextNormal;
            this.pictureBoxButtonCustom2.Location = new System.Drawing.Point(404, 0);
            this.pictureBoxButtonCustom2.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBoxButtonCustom2.Name = "pictureBoxButtonCustom2";
            this.pictureBoxButtonCustom2.NormalImage = global::Celeste_AOEO_Controls.Properties.Resources.NewsNextNormal;
            this.pictureBoxButtonCustom2.ShowToolTip = true;
            this.pictureBoxButtonCustom2.Size = new System.Drawing.Size(24, 43);
            this.pictureBoxButtonCustom2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxButtonCustom2.TabIndex = 1;
            this.pictureBoxButtonCustom2.TabStop = false;
            this.pictureBoxButtonCustom2.ToolTipText = "Next";
            // 
            // NewsSideShow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "NewsSideShow";
            this.Size = new System.Drawing.Size(428, 247);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxButtonCustom1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxButtonCustom2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private PictureBoxButtonCustom pictureBoxButtonCustom2;
        private PictureBoxButtonCustom pictureBoxButtonCustom1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
    }
}
