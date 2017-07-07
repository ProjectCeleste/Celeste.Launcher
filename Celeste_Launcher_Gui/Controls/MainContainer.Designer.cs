namespace Celeste_Launcher_Gui.Controls
{
    partial class MainContainer
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
            this.tLP_Main = new System.Windows.Forms.TableLayoutPanel();
            this.tLP_Top = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn_Close = new System.Windows.Forms.PictureBox();
            this.TopLeftFixed = new System.Windows.Forms.Panel();
            this.TopMiddleFluid = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.TopRigthFixed = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tLP_Middle = new System.Windows.Forms.TableLayoutPanel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.MiddleLeftFluid = new System.Windows.Forms.Panel();
            this.MiddleRigthFluid = new System.Windows.Forms.Panel();
            this.tLP_Bottom = new System.Windows.Forms.TableLayoutPanel();
            this.BottomMiddleFluid = new System.Windows.Forms.Panel();
            this.BottomLeftFixed = new System.Windows.Forms.Panel();
            this.BottomRigthFixed = new System.Windows.Forms.Panel();
            this.tLP_Main.SuspendLayout();
            this.tLP_Top.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Close)).BeginInit();
            this.TopMiddleFluid.SuspendLayout();
            this.tLP_Middle.SuspendLayout();
            this.tLP_Bottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // tLP_Main
            // 
            this.tLP_Main.BackColor = System.Drawing.Color.Transparent;
            this.tLP_Main.ColumnCount = 1;
            this.tLP_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tLP_Main.Controls.Add(this.tLP_Top, 0, 0);
            this.tLP_Main.Controls.Add(this.tLP_Middle, 0, 1);
            this.tLP_Main.Controls.Add(this.tLP_Bottom, 0, 2);
            this.tLP_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tLP_Main.Location = new System.Drawing.Point(0, 0);
            this.tLP_Main.Margin = new System.Windows.Forms.Padding(0);
            this.tLP_Main.Name = "tLP_Main";
            this.tLP_Main.RowCount = 3;
            this.tLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tLP_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tLP_Main.Size = new System.Drawing.Size(950, 600);
            this.tLP_Main.TabIndex = 0;
            // 
            // tLP_Top
            // 
            this.tLP_Top.ColumnCount = 5;
            this.tLP_Top.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tLP_Top.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tLP_Top.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 592F));
            this.tLP_Top.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tLP_Top.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tLP_Top.Controls.Add(this.panel2, 3, 0);
            this.tLP_Top.Controls.Add(this.TopLeftFixed, 0, 0);
            this.tLP_Top.Controls.Add(this.TopMiddleFluid, 2, 0);
            this.tLP_Top.Controls.Add(this.TopRigthFixed, 4, 0);
            this.tLP_Top.Controls.Add(this.panel1, 1, 0);
            this.tLP_Top.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tLP_Top.Location = new System.Drawing.Point(0, 0);
            this.tLP_Top.Margin = new System.Windows.Forms.Padding(0);
            this.tLP_Top.Name = "tLP_Top";
            this.tLP_Top.RowCount = 1;
            this.tLP_Top.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tLP_Top.Size = new System.Drawing.Size(950, 75);
            this.tLP_Top.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.BackgroundImage = global::Celeste_Launcher_Gui.Properties.Resources.TopLRFluid;
            this.panel2.Controls.Add(this.btn_Close);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(771, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(149, 75);
            this.panel2.TabIndex = 6;
            // 
            // btn_Close
            // 
            this.btn_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Close.BackColor = System.Drawing.Color.Transparent;
            this.btn_Close.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Close.Image = global::Celeste_Launcher_Gui.Properties.Resources.XButton_Normal;
            this.btn_Close.Location = new System.Drawing.Point(103, 10);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(45, 44);
            this.btn_Close.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.btn_Close.TabIndex = 0;
            this.btn_Close.TabStop = false;
            this.btn_Close.Click += new System.EventHandler(this.pictureBox1_Click);
            this.btn_Close.MouseEnter += new System.EventHandler(this.btn_Close_MouseEnter);
            this.btn_Close.MouseLeave += new System.EventHandler(this.btn_Close_MouseLeave);
            // 
            // TopLeftFixed
            // 
            this.TopLeftFixed.BackColor = System.Drawing.Color.Transparent;
            this.TopLeftFixed.BackgroundImage = global::Celeste_Launcher_Gui.Properties.Resources.TopLeftFixed;
            this.TopLeftFixed.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.TopLeftFixed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TopLeftFixed.Location = new System.Drawing.Point(0, 0);
            this.TopLeftFixed.Margin = new System.Windows.Forms.Padding(0);
            this.TopLeftFixed.Name = "TopLeftFixed";
            this.TopLeftFixed.Size = new System.Drawing.Size(30, 75);
            this.TopLeftFixed.TabIndex = 1;
            // 
            // TopMiddleFluid
            // 
            this.TopMiddleFluid.BackColor = System.Drawing.Color.Transparent;
            this.TopMiddleFluid.BackgroundImage = global::Celeste_Launcher_Gui.Properties.Resources.TopMiddleFluid;
            this.TopMiddleFluid.Controls.Add(this.panel3);
            this.TopMiddleFluid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TopMiddleFluid.Location = new System.Drawing.Point(179, 0);
            this.TopMiddleFluid.Margin = new System.Windows.Forms.Padding(0);
            this.TopMiddleFluid.Name = "TopMiddleFluid";
            this.TopMiddleFluid.Size = new System.Drawing.Size(592, 75);
            this.TopMiddleFluid.TabIndex = 4;
            // 
            // panel3
            // 
            this.panel3.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.panel3.Location = new System.Drawing.Point(37, 6);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(518, 55);
            this.panel3.TabIndex = 2;
            this.panel3.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MoveForm);
            // 
            // TopRigthFixed
            // 
            this.TopRigthFixed.BackColor = System.Drawing.Color.Transparent;
            this.TopRigthFixed.BackgroundImage = global::Celeste_Launcher_Gui.Properties.Resources.TopRigthFixed;
            this.TopRigthFixed.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.TopRigthFixed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TopRigthFixed.Location = new System.Drawing.Point(920, 0);
            this.TopRigthFixed.Margin = new System.Windows.Forms.Padding(0);
            this.TopRigthFixed.Name = "TopRigthFixed";
            this.TopRigthFixed.Size = new System.Drawing.Size(30, 75);
            this.TopRigthFixed.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = global::Celeste_Launcher_Gui.Properties.Resources.TopLRFluid;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(30, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(149, 75);
            this.panel1.TabIndex = 5;
            // 
            // tLP_Middle
            // 
            this.tLP_Middle.ColumnCount = 3;
            this.tLP_Middle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tLP_Middle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tLP_Middle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tLP_Middle.Controls.Add(this.panel9, 0, 0);
            this.tLP_Middle.Controls.Add(this.MiddleLeftFluid, 0, 0);
            this.tLP_Middle.Controls.Add(this.MiddleRigthFluid, 2, 0);
            this.tLP_Middle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tLP_Middle.Location = new System.Drawing.Point(0, 75);
            this.tLP_Middle.Margin = new System.Windows.Forms.Padding(0);
            this.tLP_Middle.Name = "tLP_Middle";
            this.tLP_Middle.RowCount = 1;
            this.tLP_Middle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tLP_Middle.Size = new System.Drawing.Size(950, 500);
            this.tLP_Middle.TabIndex = 2;
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.Color.White;
            this.panel9.BackgroundImage = global::Celeste_Launcher_Gui.Properties.Resources.BackgroundTexture;
            this.panel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel9.Location = new System.Drawing.Point(30, 0);
            this.panel9.Margin = new System.Windows.Forms.Padding(0);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(890, 500);
            this.panel9.TabIndex = 5;
            // 
            // MiddleLeftFluid
            // 
            this.MiddleLeftFluid.BackColor = System.Drawing.Color.Transparent;
            this.MiddleLeftFluid.BackgroundImage = global::Celeste_Launcher_Gui.Properties.Resources.MiddleLeftFluid;
            this.MiddleLeftFluid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MiddleLeftFluid.Location = new System.Drawing.Point(0, 0);
            this.MiddleLeftFluid.Margin = new System.Windows.Forms.Padding(0);
            this.MiddleLeftFluid.Name = "MiddleLeftFluid";
            this.MiddleLeftFluid.Size = new System.Drawing.Size(30, 500);
            this.MiddleLeftFluid.TabIndex = 2;
            // 
            // MiddleRigthFluid
            // 
            this.MiddleRigthFluid.BackColor = System.Drawing.Color.Transparent;
            this.MiddleRigthFluid.BackgroundImage = global::Celeste_Launcher_Gui.Properties.Resources.MiddleRigthFluid;
            this.MiddleRigthFluid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MiddleRigthFluid.Location = new System.Drawing.Point(920, 0);
            this.MiddleRigthFluid.Margin = new System.Windows.Forms.Padding(0);
            this.MiddleRigthFluid.Name = "MiddleRigthFluid";
            this.MiddleRigthFluid.Size = new System.Drawing.Size(30, 500);
            this.MiddleRigthFluid.TabIndex = 3;
            // 
            // tLP_Bottom
            // 
            this.tLP_Bottom.ColumnCount = 3;
            this.tLP_Bottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tLP_Bottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tLP_Bottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tLP_Bottom.Controls.Add(this.BottomMiddleFluid, 0, 0);
            this.tLP_Bottom.Controls.Add(this.BottomLeftFixed, 0, 0);
            this.tLP_Bottom.Controls.Add(this.BottomRigthFixed, 2, 0);
            this.tLP_Bottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tLP_Bottom.Location = new System.Drawing.Point(0, 575);
            this.tLP_Bottom.Margin = new System.Windows.Forms.Padding(0);
            this.tLP_Bottom.Name = "tLP_Bottom";
            this.tLP_Bottom.RowCount = 1;
            this.tLP_Bottom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tLP_Bottom.Size = new System.Drawing.Size(950, 25);
            this.tLP_Bottom.TabIndex = 3;
            // 
            // BottomMiddleFluid
            // 
            this.BottomMiddleFluid.BackColor = System.Drawing.Color.Transparent;
            this.BottomMiddleFluid.BackgroundImage = global::Celeste_Launcher_Gui.Properties.Resources.BottomMiddleFluid;
            this.BottomMiddleFluid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BottomMiddleFluid.Location = new System.Drawing.Point(30, 0);
            this.BottomMiddleFluid.Margin = new System.Windows.Forms.Padding(0);
            this.BottomMiddleFluid.Name = "BottomMiddleFluid";
            this.BottomMiddleFluid.Size = new System.Drawing.Size(890, 25);
            this.BottomMiddleFluid.TabIndex = 4;
            // 
            // BottomLeftFixed
            // 
            this.BottomLeftFixed.BackColor = System.Drawing.Color.Transparent;
            this.BottomLeftFixed.BackgroundImage = global::Celeste_Launcher_Gui.Properties.Resources.BottomLeftFixed;
            this.BottomLeftFixed.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.BottomLeftFixed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BottomLeftFixed.Location = new System.Drawing.Point(0, 0);
            this.BottomLeftFixed.Margin = new System.Windows.Forms.Padding(0);
            this.BottomLeftFixed.Name = "BottomLeftFixed";
            this.BottomLeftFixed.Size = new System.Drawing.Size(30, 25);
            this.BottomLeftFixed.TabIndex = 2;
            // 
            // BottomRigthFixed
            // 
            this.BottomRigthFixed.BackColor = System.Drawing.Color.Transparent;
            this.BottomRigthFixed.BackgroundImage = global::Celeste_Launcher_Gui.Properties.Resources.BottomRigthFixed;
            this.BottomRigthFixed.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.BottomRigthFixed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BottomRigthFixed.Location = new System.Drawing.Point(920, 0);
            this.BottomRigthFixed.Margin = new System.Windows.Forms.Padding(0);
            this.BottomRigthFixed.Name = "BottomRigthFixed";
            this.BottomRigthFixed.Size = new System.Drawing.Size(30, 25);
            this.BottomRigthFixed.TabIndex = 1;
            // 
            // MainContainer
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.tLP_Main);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.MinimumSize = new System.Drawing.Size(310, 250);
            this.Name = "MainContainer";
            this.Size = new System.Drawing.Size(950, 600);
            this.tLP_Main.ResumeLayout(false);
            this.tLP_Top.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Close)).EndInit();
            this.TopMiddleFluid.ResumeLayout(false);
            this.tLP_Middle.ResumeLayout(false);
            this.tLP_Bottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tLP_Main;
        private System.Windows.Forms.TableLayoutPanel tLP_Top;
        private System.Windows.Forms.Panel TopRigthFixed;
        private System.Windows.Forms.Panel TopLeftFixed;
        private System.Windows.Forms.TableLayoutPanel tLP_Middle;
        private System.Windows.Forms.Panel MiddleLeftFluid;
        private System.Windows.Forms.Panel MiddleRigthFluid;
        private System.Windows.Forms.TableLayoutPanel tLP_Bottom;
        private System.Windows.Forms.Panel BottomRigthFixed;
        private System.Windows.Forms.Panel BottomLeftFixed;
        private System.Windows.Forms.Panel BottomMiddleFluid;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.PictureBox btn_Close;
        private System.Windows.Forms.Panel TopMiddleFluid;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
    }
}
