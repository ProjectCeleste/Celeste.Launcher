namespace Celeste_AOEO_Controls
{
    partial class CustomBtnSplit
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
            this.lb_Btn = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBoxButtonCustom1 = new Celeste_AOEO_Controls.PictureBoxButtonCustom();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxButtonCustom1)).BeginInit();
            this.SuspendLayout();
            // 
            // lb_Btn
            // 
            this.lb_Btn.AutoSize = true;
            this.lb_Btn.BackColor = System.Drawing.Color.Transparent;
            this.lb_Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lb_Btn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_Btn.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Btn.ForeColor = System.Drawing.Color.White;
            this.lb_Btn.Location = new System.Drawing.Point(22, 5);
            this.lb_Btn.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lb_Btn.Name = "lb_Btn";
            this.lb_Btn.Size = new System.Drawing.Size(93, 31);
            this.lb_Btn.TabIndex = 13;
            this.lb_Btn.Text = "BtnSmall";
            this.lb_Btn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lb_Btn.Click += new System.EventHandler(this.Lb_Btn_Click);
            this.lb_Btn.MouseEnter += new System.EventHandler(this.Lb_Btn_MouseEnter);
            this.lb_Btn.MouseLeave += new System.EventHandler(this.Lb_Btn_MouseLeave);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 7;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.99999F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 1F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 1F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.00001F));
            this.tableLayoutPanel1.Controls.Add(this.lb_Btn, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.pictureBoxButtonCustom1, 4, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 74F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(172, 42);
            this.tableLayoutPanel1.TabIndex = 14;
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::Celeste_AOEO_Controls.Properties.Resources.BarSeparatorVertical;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(120, 5);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(12, 31);
            this.panel1.TabIndex = 15;
            // 
            // pictureBoxButtonCustom1
            // 
            this.pictureBoxButtonCustom1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxButtonCustom1.DisabledImage = null;
            this.pictureBoxButtonCustom1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxButtonCustom1.HoverImage = global::Celeste_AOEO_Controls.Properties.Resources.arrow2;
            this.pictureBoxButtonCustom1.Image = global::Celeste_AOEO_Controls.Properties.Resources.arrow;
            this.pictureBoxButtonCustom1.Location = new System.Drawing.Point(135, 5);
            this.pictureBoxButtonCustom1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBoxButtonCustom1.Name = "pictureBoxButtonCustom1";
            this.pictureBoxButtonCustom1.NormalImage = global::Celeste_AOEO_Controls.Properties.Resources.arrow;
            this.pictureBoxButtonCustom1.ShowToolTip = false;
            this.pictureBoxButtonCustom1.Size = new System.Drawing.Size(20, 31);
            this.pictureBoxButtonCustom1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxButtonCustom1.TabIndex = 16;
            this.pictureBoxButtonCustom1.TabStop = false;
            this.pictureBoxButtonCustom1.ToolTipText = null;
            this.pictureBoxButtonCustom1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureBox1_MouseDown);
            this.pictureBoxButtonCustom1.MouseEnter += new System.EventHandler(this.PictureBox1_MouseEnter);
            this.pictureBoxButtonCustom1.MouseLeave += new System.EventHandler(this.PictureBox1_MouseLeave);
            // 
            // CustomBtnSplit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BackgroundImage = global::Celeste_AOEO_Controls.Properties.Resources.BtnSmallNormal;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "CustomBtnSplit";
            this.Size = new System.Drawing.Size(172, 42);
            this.Load += new System.EventHandler(this.CustomBtn_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxButtonCustom1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lb_Btn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private PictureBoxButtonCustom pictureBoxButtonCustom1;
    }
}
