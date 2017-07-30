namespace Celeste_Launcher_Gui.Controls
{
    partial class BtnSmall
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
            this.SuspendLayout();
            // 
            // lb_Btn
            // 
            this.lb_Btn.BackColor = System.Drawing.Color.Transparent;
            this.lb_Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lb_Btn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_Btn.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Btn.ForeColor = System.Drawing.Color.White;
            this.lb_Btn.Location = new System.Drawing.Point(0, 0);
            this.lb_Btn.Margin = new System.Windows.Forms.Padding(0);
            this.lb_Btn.Name = "lb_Btn";
            this.lb_Btn.Size = new System.Drawing.Size(201, 42);
            this.lb_Btn.TabIndex = 13;
            this.lb_Btn.Text = "BtnSmall";
            this.lb_Btn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lb_Btn.Click += new System.EventHandler(this.Lb_Btn_Click);
            this.lb_Btn.MouseEnter += new System.EventHandler(this.Lb_Btn_MouseEnter);
            this.lb_Btn.MouseLeave += new System.EventHandler(this.Lb_Btn_MouseLeave);
            // 
            // BtnSmall
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Celeste_Launcher_Gui.Properties.Resources.BtnSmallNormal;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Controls.Add(this.lb_Btn);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "BtnSmall";
            this.Size = new System.Drawing.Size(201, 42);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lb_Btn;
    }
}
