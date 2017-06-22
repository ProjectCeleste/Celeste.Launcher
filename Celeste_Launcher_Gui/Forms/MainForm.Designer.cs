namespace Celeste_Launcher_Gui.Forms
{
    partial class MainForm
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

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.linkLabel6 = new System.Windows.Forms.LinkLabel();
            this.linkLabel5 = new System.Windows.Forms.LinkLabel();
            this.lbl_Friens_PendingReq = new System.Windows.Forms.Label();
            this.lbl_Friens_PendingInvite = new System.Windows.Forms.Label();
            this.lv_Friends = new System.Windows.Forms.ListView();
            this.ch_Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ch_Online = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.p_UserInfo = new System.Windows.Forms.Panel();
            this.lb_Play = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.linkLabel3 = new System.Windows.Forms.LinkLabel();
            this.lb_ManageInvite = new System.Windows.Forms.Label();
            this.linkLbl_aoeo4evernet = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.linklbl_Wiki = new System.Windows.Forms.LinkLabel();
            this.linkLbl_ReportUser = new System.Windows.Forms.LinkLabel();
            this.linkLbl_ProjectCelesteCom = new System.Windows.Forms.LinkLabel();
            this.lbl_Connected = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbl_Banned = new System.Windows.Forms.Label();
            this.linklbl_ReportIssue = new System.Windows.Forms.LinkLabel();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.linkLbl_Upgrade = new System.Windows.Forms.LinkLabel();
            this.lbl_AllowCiv = new System.Windows.Forms.Label();
            this.linkLbl_ChangePwd = new System.Windows.Forms.LinkLabel();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_Rank = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.lbl_UserName = new System.Windows.Forms.Label();
            this.pb_Avatar = new System.Windows.Forms.PictureBox();
            this.lbl_Mail = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lb_Close = new System.Windows.Forms.Label();
            this.lb_Title = new System.Windows.Forms.Label();
            this.p_UserInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Avatar)).BeginInit();
            this.SuspendLayout();
            // 
            // linkLabel6
            // 
            this.linkLabel6.AutoSize = true;
            this.linkLabel6.BackColor = System.Drawing.Color.Transparent;
            this.linkLabel6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel6.LinkColor = System.Drawing.Color.DarkBlue;
            this.linkLabel6.Location = new System.Drawing.Point(793, 145);
            this.linkLabel6.Name = "linkLabel6";
            this.linkLabel6.Size = new System.Drawing.Size(59, 17);
            this.linkLabel6.TabIndex = 21;
            this.linkLabel6.TabStop = true;
            this.linkLabel6.Text = "Manage";
            this.linkLabel6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // linkLabel5
            // 
            this.linkLabel5.AutoSize = true;
            this.linkLabel5.BackColor = System.Drawing.Color.Transparent;
            this.linkLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel5.LinkColor = System.Drawing.Color.DarkBlue;
            this.linkLabel5.Location = new System.Drawing.Point(793, 114);
            this.linkLabel5.Name = "linkLabel5";
            this.linkLabel5.Size = new System.Drawing.Size(59, 17);
            this.linkLabel5.TabIndex = 20;
            this.linkLabel5.TabStop = true;
            this.linkLabel5.Text = "Manage";
            this.linkLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_Friens_PendingReq
            // 
            this.lbl_Friens_PendingReq.AutoSize = true;
            this.lbl_Friens_PendingReq.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Friens_PendingReq.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Friens_PendingReq.Location = new System.Drawing.Point(575, 146);
            this.lbl_Friens_PendingReq.Name = "lbl_Friens_PendingReq";
            this.lbl_Friens_PendingReq.Size = new System.Drawing.Size(178, 16);
            this.lbl_Friens_PendingReq.TabIndex = 15;
            this.lbl_Friens_PendingReq.Text = "Pending Friends Request (0)";
            this.lbl_Friens_PendingReq.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Friens_PendingInvite
            // 
            this.lbl_Friens_PendingInvite.AutoSize = true;
            this.lbl_Friens_PendingInvite.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Friens_PendingInvite.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Friens_PendingInvite.Location = new System.Drawing.Point(575, 114);
            this.lbl_Friens_PendingInvite.Name = "lbl_Friens_PendingInvite";
            this.lbl_Friens_PendingInvite.Size = new System.Drawing.Size(158, 16);
            this.lbl_Friens_PendingInvite.TabIndex = 7;
            this.lbl_Friens_PendingInvite.Text = "Pending Friends Invite (0)";
            this.lbl_Friens_PendingInvite.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lv_Friends
            // 
            this.lv_Friends.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lv_Friends.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ch_Name,
            this.ch_Online});
            this.lv_Friends.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lv_Friends.Location = new System.Drawing.Point(578, 176);
            this.lv_Friends.Name = "lv_Friends";
            this.lv_Friends.Size = new System.Drawing.Size(274, 185);
            this.lv_Friends.TabIndex = 0;
            this.lv_Friends.UseCompatibleStateImageBehavior = false;
            this.lv_Friends.View = System.Windows.Forms.View.Details;
            // 
            // ch_Name
            // 
            this.ch_Name.Text = "User Name";
            this.ch_Name.Width = 161;
            // 
            // ch_Online
            // 
            this.ch_Online.Text = "Online";
            this.ch_Online.Width = 63;
            // 
            // p_UserInfo
            // 
            this.p_UserInfo.BackgroundImage = global::Celeste_Launcher_Gui.Properties.Resources.mainform;
            this.p_UserInfo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.p_UserInfo.Controls.Add(this.lb_Close);
            this.p_UserInfo.Controls.Add(this.lv_Friends);
            this.p_UserInfo.Controls.Add(this.lb_Title);
            this.p_UserInfo.Controls.Add(this.linkLabel6);
            this.p_UserInfo.Controls.Add(this.label5);
            this.p_UserInfo.Controls.Add(this.lbl_Friens_PendingReq);
            this.p_UserInfo.Controls.Add(this.linkLabel5);
            this.p_UserInfo.Controls.Add(this.lb_Play);
            this.p_UserInfo.Controls.Add(this.label4);
            this.p_UserInfo.Controls.Add(this.lbl_Friens_PendingInvite);
            this.p_UserInfo.Controls.Add(this.linkLabel3);
            this.p_UserInfo.Controls.Add(this.lb_ManageInvite);
            this.p_UserInfo.Controls.Add(this.linkLbl_aoeo4evernet);
            this.p_UserInfo.Controls.Add(this.label3);
            this.p_UserInfo.Controls.Add(this.linklbl_Wiki);
            this.p_UserInfo.Controls.Add(this.linkLbl_ReportUser);
            this.p_UserInfo.Controls.Add(this.linkLbl_ProjectCelesteCom);
            this.p_UserInfo.Controls.Add(this.lbl_Connected);
            this.p_UserInfo.Controls.Add(this.label2);
            this.p_UserInfo.Controls.Add(this.lbl_Banned);
            this.p_UserInfo.Controls.Add(this.linklbl_ReportIssue);
            this.p_UserInfo.Controls.Add(this.comboBox1);
            this.p_UserInfo.Controls.Add(this.linkLbl_Upgrade);
            this.p_UserInfo.Controls.Add(this.lbl_AllowCiv);
            this.p_UserInfo.Controls.Add(this.linkLbl_ChangePwd);
            this.p_UserInfo.Controls.Add(this.linkLabel2);
            this.p_UserInfo.Controls.Add(this.label1);
            this.p_UserInfo.Controls.Add(this.lbl_Rank);
            this.p_UserInfo.Controls.Add(this.comboBox2);
            this.p_UserInfo.Controls.Add(this.linkLabel1);
            this.p_UserInfo.Controls.Add(this.lbl_UserName);
            this.p_UserInfo.Controls.Add(this.pb_Avatar);
            this.p_UserInfo.Controls.Add(this.lbl_Mail);
            this.p_UserInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.p_UserInfo.Location = new System.Drawing.Point(0, 0);
            this.p_UserInfo.Name = "p_UserInfo";
            this.p_UserInfo.Size = new System.Drawing.Size(904, 492);
            this.p_UserInfo.TabIndex = 1;
            // 
            // lb_Play
            // 
            this.lb_Play.BackColor = System.Drawing.Color.Transparent;
            this.lb_Play.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Play.ForeColor = System.Drawing.Color.White;
            this.lb_Play.Location = new System.Drawing.Point(724, 429);
            this.lb_Play.Name = "lb_Play";
            this.lb_Play.Size = new System.Drawing.Size(148, 29);
            this.lb_Play.TabIndex = 34;
            this.lb_Play.Text = "PLAY AOEO";
            this.lb_Play.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lb_Play.Click += new System.EventHandler(this.btn_Play_Click);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(45, 291);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(144, 23);
            this.label4.TabIndex = 33;
            this.label4.Text = "Useful Links";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // linkLabel3
            // 
            this.linkLabel3.AutoSize = true;
            this.linkLabel3.BackColor = System.Drawing.Color.Transparent;
            this.linkLabel3.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel3.LinkColor = System.Drawing.Color.DarkBlue;
            this.linkLabel3.Location = new System.Drawing.Point(386, 345);
            this.linkLabel3.Name = "linkLabel3";
            this.linkLabel3.Size = new System.Drawing.Size(126, 16);
            this.linkLabel3.TabIndex = 32;
            this.linkLabel3.TabStop = true;
            this.linkLabel3.Text = "eso-community.net";
            this.linkLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.linkLabel3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel3_LinkClicked_1);
            // 
            // lb_ManageInvite
            // 
            this.lb_ManageInvite.BackColor = System.Drawing.Color.Transparent;
            this.lb_ManageInvite.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_ManageInvite.ForeColor = System.Drawing.Color.White;
            this.lb_ManageInvite.Location = new System.Drawing.Point(398, 241);
            this.lb_ManageInvite.Name = "lb_ManageInvite";
            this.lb_ManageInvite.Size = new System.Drawing.Size(140, 29);
            this.lb_ManageInvite.TabIndex = 26;
            this.lb_ManageInvite.Text = "Manage Invite";
            this.lb_ManageInvite.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lb_ManageInvite.Click += new System.EventHandler(this.btn_ManageInvite_Click);
            // 
            // linkLbl_aoeo4evernet
            // 
            this.linkLbl_aoeo4evernet.AutoSize = true;
            this.linkLbl_aoeo4evernet.BackColor = System.Drawing.Color.Transparent;
            this.linkLbl_aoeo4evernet.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLbl_aoeo4evernet.LinkColor = System.Drawing.Color.DarkBlue;
            this.linkLbl_aoeo4evernet.Location = new System.Drawing.Point(414, 318);
            this.linkLbl_aoeo4evernet.Name = "linkLbl_aoeo4evernet";
            this.linkLbl_aoeo4evernet.Size = new System.Drawing.Size(98, 16);
            this.linkLbl_aoeo4evernet.TabIndex = 31;
            this.linkLbl_aoeo4evernet.TabStop = true;
            this.linkLbl_aoeo4evernet.Text = "aoeo4ever.net";
            this.linkLbl_aoeo4evernet.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.linkLbl_aoeo4evernet.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLbl_aoeo4evernet_LinkClicked);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(46, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(160, 23);
            this.label3.TabIndex = 25;
            this.label3.Text = "My Account";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // linklbl_Wiki
            // 
            this.linklbl_Wiki.AutoSize = true;
            this.linklbl_Wiki.BackColor = System.Drawing.Color.Transparent;
            this.linklbl_Wiki.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linklbl_Wiki.LinkColor = System.Drawing.Color.DarkBlue;
            this.linklbl_Wiki.Location = new System.Drawing.Point(227, 345);
            this.linklbl_Wiki.Name = "linklbl_Wiki";
            this.linklbl_Wiki.Size = new System.Drawing.Size(85, 16);
            this.linklbl_Wiki.TabIndex = 26;
            this.linklbl_Wiki.TabStop = true;
            this.linklbl_Wiki.Text = "AoEO Wikia";
            this.linklbl_Wiki.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linklbl_Wiki.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linklbl_Wiki_LinkClicked);
            // 
            // linkLbl_ReportUser
            // 
            this.linkLbl_ReportUser.AutoSize = true;
            this.linkLbl_ReportUser.BackColor = System.Drawing.Color.Transparent;
            this.linkLbl_ReportUser.Enabled = false;
            this.linkLbl_ReportUser.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLbl_ReportUser.LinkColor = System.Drawing.Color.DarkBlue;
            this.linkLbl_ReportUser.Location = new System.Drawing.Point(227, 318);
            this.linkLbl_ReportUser.Name = "linkLbl_ReportUser";
            this.linkLbl_ReportUser.Size = new System.Drawing.Size(85, 16);
            this.linkLbl_ReportUser.TabIndex = 27;
            this.linkLbl_ReportUser.TabStop = true;
            this.linkLbl_ReportUser.Text = "Report User";
            this.linkLbl_ReportUser.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkLbl_ReportUser.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLbl_ReportUser_LinkClicked);
            // 
            // linkLbl_ProjectCelesteCom
            // 
            this.linkLbl_ProjectCelesteCom.AutoSize = true;
            this.linkLbl_ProjectCelesteCom.BackColor = System.Drawing.Color.Transparent;
            this.linkLbl_ProjectCelesteCom.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLbl_ProjectCelesteCom.LinkColor = System.Drawing.Color.DarkBlue;
            this.linkLbl_ProjectCelesteCom.Location = new System.Drawing.Point(38, 345);
            this.linkLbl_ProjectCelesteCom.Name = "linkLbl_ProjectCelesteCom";
            this.linkLbl_ProjectCelesteCom.Size = new System.Drawing.Size(130, 16);
            this.linkLbl_ProjectCelesteCom.TabIndex = 29;
            this.linkLbl_ProjectCelesteCom.TabStop = true;
            this.linkLbl_ProjectCelesteCom.Text = "ProjectCeleste.com";
            this.linkLbl_ProjectCelesteCom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkLbl_ProjectCelesteCom.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLbl_ProjectCelesteCom_LinkClicked);
            // 
            // lbl_Connected
            // 
            this.lbl_Connected.AutoSize = true;
            this.lbl_Connected.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Connected.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Connected.Location = new System.Drawing.Point(227, 248);
            this.lbl_Connected.Name = "lbl_Connected";
            this.lbl_Connected.Size = new System.Drawing.Size(130, 17);
            this.lbl_Connected.TabIndex = 24;
            this.lbl_Connected.Text = "Connected: false";
            this.lbl_Connected.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(28, 414);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(554, 32);
            this.label2.TabIndex = 24;
            this.label2.Text = "\"Age of Empires” is a registered trademark of Microsoft Corporation.\r\nProject Cel" +
    "este is not associated with or sponsored by Microsoft Corporation.";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Banned
            // 
            this.lbl_Banned.AutoSize = true;
            this.lbl_Banned.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Banned.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Banned.Location = new System.Drawing.Point(38, 248);
            this.lbl_Banned.Name = "lbl_Banned";
            this.lbl_Banned.Size = new System.Drawing.Size(108, 17);
            this.lbl_Banned.TabIndex = 23;
            this.lbl_Banned.Text = "Banned: false";
            this.lbl_Banned.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // linklbl_ReportIssue
            // 
            this.linklbl_ReportIssue.AutoSize = true;
            this.linklbl_ReportIssue.BackColor = System.Drawing.Color.Transparent;
            this.linklbl_ReportIssue.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linklbl_ReportIssue.LinkColor = System.Drawing.Color.DarkBlue;
            this.linklbl_ReportIssue.Location = new System.Drawing.Point(38, 318);
            this.linklbl_ReportIssue.Name = "linklbl_ReportIssue";
            this.linklbl_ReportIssue.Size = new System.Drawing.Size(89, 16);
            this.linklbl_ReportIssue.TabIndex = 25;
            this.linklbl_ReportIssue.TabStop = true;
            this.linklbl_ReportIssue.Text = "Report Issue";
            this.linklbl_ReportIssue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linklbl_ReportIssue.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linklbl_ReportBug_LinkClicked);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(238, 206);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(167, 23);
            this.comboBox1.TabIndex = 12;
            // 
            // linkLbl_Upgrade
            // 
            this.linkLbl_Upgrade.AutoSize = true;
            this.linkLbl_Upgrade.BackColor = System.Drawing.Color.Transparent;
            this.linkLbl_Upgrade.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLbl_Upgrade.LinkColor = System.Drawing.Color.DarkBlue;
            this.linkLbl_Upgrade.Location = new System.Drawing.Point(342, 178);
            this.linkLbl_Upgrade.Name = "linkLbl_Upgrade";
            this.linkLbl_Upgrade.Size = new System.Drawing.Size(70, 17);
            this.linkLbl_Upgrade.TabIndex = 21;
            this.linkLbl_Upgrade.TabStop = true;
            this.linkLbl_Upgrade.Text = "Upgrade";
            this.linkLbl_Upgrade.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.linkLbl_Upgrade.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel3_LinkClicked);
            // 
            // lbl_AllowCiv
            // 
            this.lbl_AllowCiv.AutoSize = true;
            this.lbl_AllowCiv.BackColor = System.Drawing.Color.Transparent;
            this.lbl_AllowCiv.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_AllowCiv.Location = new System.Drawing.Point(38, 208);
            this.lbl_AllowCiv.Name = "lbl_AllowCiv";
            this.lbl_AllowCiv.Size = new System.Drawing.Size(151, 17);
            this.lbl_AllowCiv.TabIndex = 9;
            this.lbl_AllowCiv.Text = "Allowed Civilization:";
            this.lbl_AllowCiv.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // linkLbl_ChangePwd
            // 
            this.linkLbl_ChangePwd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLbl_ChangePwd.AutoSize = true;
            this.linkLbl_ChangePwd.BackColor = System.Drawing.Color.Transparent;
            this.linkLbl_ChangePwd.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLbl_ChangePwd.LinkColor = System.Drawing.Color.DarkBlue;
            this.linkLbl_ChangePwd.Location = new System.Drawing.Point(415, 213);
            this.linkLbl_ChangePwd.Name = "linkLbl_ChangePwd";
            this.linkLbl_ChangePwd.Size = new System.Drawing.Size(123, 16);
            this.linkLbl_ChangePwd.TabIndex = 22;
            this.linkLbl_ChangePwd.TabStop = true;
            this.linkLbl_ChangePwd.Text = "Change Password";
            this.linkLbl_ChangePwd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.linkLbl_ChangePwd.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLbl_ChangePwd_LinkClicked);
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.BackColor = System.Drawing.Color.Transparent;
            this.linkLabel2.Enabled = false;
            this.linkLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel2.LinkColor = System.Drawing.Color.DarkBlue;
            this.linkLabel2.Location = new System.Drawing.Point(369, 146);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(36, 17);
            this.linkLabel2.TabIndex = 20;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "Edit";
            this.linkLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(592, 390);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 16);
            this.label1.TabIndex = 14;
            this.label1.Text = "Game Language:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_Rank
            // 
            this.lbl_Rank.AutoSize = true;
            this.lbl_Rank.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Rank.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Rank.Location = new System.Drawing.Point(38, 176);
            this.lbl_Rank.Name = "lbl_Rank";
            this.lbl_Rank.Size = new System.Drawing.Size(50, 17);
            this.lbl_Rank.TabIndex = 8;
            this.lbl_Rank.Text = "Rank:";
            this.lbl_Rank.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "de-DE",
            "en-US",
            "es-ES",
            "fr-FR",
            "it-IT",
            "zh-CHT"});
            this.comboBox2.Location = new System.Drawing.Point(761, 388);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(101, 23);
            this.comboBox2.TabIndex = 13;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.BackColor = System.Drawing.Color.Transparent;
            this.linkLabel1.Enabled = false;
            this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.LinkColor = System.Drawing.Color.DarkBlue;
            this.linkLabel1.Location = new System.Drawing.Point(369, 114);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(36, 17);
            this.linkLabel1.TabIndex = 19;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Edit";
            this.linkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.linkLabel1.VisitedLinkColor = System.Drawing.Color.Sienna;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // lbl_UserName
            // 
            this.lbl_UserName.AutoSize = true;
            this.lbl_UserName.BackColor = System.Drawing.Color.Transparent;
            this.lbl_UserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_UserName.Location = new System.Drawing.Point(38, 146);
            this.lbl_UserName.Name = "lbl_UserName";
            this.lbl_UserName.Size = new System.Drawing.Size(93, 17);
            this.lbl_UserName.TabIndex = 7;
            this.lbl_UserName.Text = "User Name:";
            this.lbl_UserName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pb_Avatar
            // 
            this.pb_Avatar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pb_Avatar.Cursor = System.Windows.Forms.Cursors.Default;
            this.pb_Avatar.ErrorImage = null;
            this.pb_Avatar.Image = global::Celeste_Launcher_Gui.Properties.Resources.no_avatar;
            this.pb_Avatar.InitialImage = null;
            this.pb_Avatar.Location = new System.Drawing.Point(442, 102);
            this.pb_Avatar.Name = "pb_Avatar";
            this.pb_Avatar.Size = new System.Drawing.Size(96, 96);
            this.pb_Avatar.TabIndex = 10;
            this.pb_Avatar.TabStop = false;
            this.pb_Avatar.Click += new System.EventHandler(this.pb_Avatar_Click);
            // 
            // lbl_Mail
            // 
            this.lbl_Mail.AutoSize = true;
            this.lbl_Mail.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Mail.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Mail.Location = new System.Drawing.Point(38, 114);
            this.lbl_Mail.Name = "lbl_Mail";
            this.lbl_Mail.Size = new System.Drawing.Size(52, 17);
            this.lbl_Mail.TabIndex = 6;
            this.lbl_Mail.Text = "Email:";
            this.lbl_Mail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(591, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(144, 23);
            this.label5.TabIndex = 35;
            this.label5.Text = "Friends (Soon)";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lb_Close
            // 
            this.lb_Close.BackColor = System.Drawing.Color.Transparent;
            this.lb_Close.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Close.ForeColor = System.Drawing.Color.Black;
            this.lb_Close.Location = new System.Drawing.Point(847, 21);
            this.lb_Close.Name = "lb_Close";
            this.lb_Close.Size = new System.Drawing.Size(41, 38);
            this.lb_Close.TabIndex = 13;
            this.lb_Close.Text = "X";
            this.lb_Close.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lb_Title
            // 
            this.lb_Title.BackColor = System.Drawing.Color.Transparent;
            this.lb_Title.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.lb_Title.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Title.ForeColor = System.Drawing.Color.Gainsboro;
            this.lb_Title.Location = new System.Drawing.Point(12, 9);
            this.lb_Title.Name = "lb_Title";
            this.lb_Title.Size = new System.Drawing.Size(829, 50);
            this.lb_Title.TabIndex = 12;
            this.lb_Title.Text = "PROJECT CELESTE - CONNECTED";
            this.lb_Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Magenta;
            this.ClientSize = new System.Drawing.Size(904, 492);
            this.ControlBox = false;
            this.Controls.Add(this.p_UserInfo);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Project Celeste -- Connected";
            this.TransparencyKey = System.Drawing.Color.Magenta;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.p_UserInfo.ResumeLayout(false);
            this.p_UserInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Avatar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListView lv_Friends;
        private System.Windows.Forms.ColumnHeader ch_Name;
        private System.Windows.Forms.ColumnHeader ch_Online;
        private System.Windows.Forms.Panel p_UserInfo;
        private System.Windows.Forms.Label lbl_Mail;
        private System.Windows.Forms.Label lbl_Rank;
        private System.Windows.Forms.Label lbl_UserName;
        private System.Windows.Forms.Label lbl_Friens_PendingInvite;
        private System.Windows.Forms.Label lbl_AllowCiv;
        private System.Windows.Forms.PictureBox pb_Avatar;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label lbl_Friens_PendingReq;
        private System.Windows.Forms.LinkLabel linkLabel6;
        private System.Windows.Forms.LinkLabel linkLabel5;
        private System.Windows.Forms.LinkLabel linkLbl_ChangePwd;
        private System.Windows.Forms.LinkLabel linkLbl_Upgrade;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.LinkLabel linkLbl_ReportUser;
        private System.Windows.Forms.LinkLabel linklbl_Wiki;
        private System.Windows.Forms.LinkLabel linklbl_ReportIssue;
        private System.Windows.Forms.Label lbl_Connected;
        private System.Windows.Forms.Label lbl_Banned;
        private System.Windows.Forms.LinkLabel linkLbl_ProjectCelesteCom;
        private System.Windows.Forms.LinkLabel linkLbl_aoeo4evernet;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.LinkLabel linkLabel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lb_Play;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lb_ManageInvite;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lb_Close;
        private System.Windows.Forms.Label lb_Title;
    }
}

