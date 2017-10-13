#region Using directives

using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Celeste_Launcher_Gui.Helpers;
using Open.Nat;
using System.Collections.Generic;
using System.Drawing;

#endregion

namespace Celeste_Launcher_Gui.Forms
{
    public partial class MainForm : Form
    {

        private List<Image> arrayImg = new List<Image>();
        private int curImage = 0;

        public MainForm()
        {
            InitializeComponent();

            //
            lb_Ver.Text = $@"v{Assembly.GetEntryAssembly().GetName().Version}";

            // set a background
            SetRandomBackgroundImageX();

            arrayImg.Add(Properties.Resources.news_advisors2);
            arrayImg.Add(Properties.Resources.news_athens2);
            arrayImg.Add(Properties.Resources.news_sparta2);
            curImage = 0;
            NextNewsImage();
            //Configure Fonts
            SkinHelper.SetFont(Controls);

            //Game Lang
            if (Program.UserConfig != null)
                comboBox2.SelectedIndex = (int) Program.UserConfig.GameLanguage;
            else
                comboBox2.SelectedIndex = (int) GameLanguage.enUS;

            //Login
            using (var form = new LoginForm())
            {
                var dr = form.ShowDialog();

                if (dr != DialogResult.OK)
                {
                    try
                    {
                        Program.WebSocketClient.AgentWebSocket.Close();
                        NatDiscoverer.ReleaseAll();
                    }
                    catch
                    {
                        //
                    }
                    Environment.Exit(0);
                }
            }

            //User Info
            if (Program.WebSocketClient.UserInformation == null) return;

            lbl_Mail.Text += $@" {Program.WebSocketClient.UserInformation.Mail}";
            lbl_UserName.Text += $@" {Program.WebSocketClient.UserInformation.ProfileName}";
            lbl_Rank.Text += $@" {Program.WebSocketClient.UserInformation.Rank}";

            //AutoDisconnect
            try
            {
                Program.WebSocketClient.AgentWebSocket.Close();
            }
            catch
            {
                //
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Program.WebSocketClient.AgentWebSocket.Close();
                NatDiscoverer.ReleaseAll();
            }
            catch
            {
                //
            }
        }

        private void Linklbl_ReportBug_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/ProjectCeleste/Celeste_Server/issues");
        }

        private void LinkLbl_ProjectCelesteCom_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://projectceleste.com");
        }

        private void Linklbl_Wiki_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://ageofempiresonline.wikia.com/wiki/Age_of_Empires_Online_Wiki");
        }

        private void LinkLabel3_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://eso-community.net/");
        }

        private void LinkLbl_ChangePwd_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (var form = new ChangePwdForm())
            {
                Hide();
                form.ShowDialog();
                Show();
            }
        }

        private void Pb_Avatar_Click(object sender, EventArgs e)
        {
            //TODO
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //TODO
        }

        private void LinkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //TODO
        }

        private async void Btn_Play_Click(object sender, EventArgs e)
        {
            btnSmall1.Enabled = false;
            var pname = Process.GetProcessesByName("spartan");
            if (pname.Length > 0)
            {
                SkinHelper.ShowMessage(@"Game already runing!");
                btnSmall1.Enabled = true;
                return;
            }

            //MpSettings
            try
            {
                if (Program.UserConfig.MpSettings != null)
                    if (Program.UserConfig.MpSettings.IsOnline)
                    {
                        Program.UserConfig.MpSettings.PublicIp = Program.WebSocketClient.UserInformation.Ip;

                        if (Program.UserConfig.MpSettings.AutoPortMapping)
                        {
                            var mapPortTask = OpenNat.MapPortTask(1000, 1000);
                            try
                            {
                                await mapPortTask;
                                NatDiscoverer.TraceSource.Close();
                            }
                            catch (AggregateException ex)
                            {
                                NatDiscoverer.TraceSource.Close();

                                if (!(ex.InnerException is NatDeviceNotFoundException)) throw;

                                SkinHelper.ShowMessage(
                                    "Error: Upnp device not found! Set \"Port mapping\" to manual in \"Mp Settings\" and configure your router.",
                                    @"Project Celeste",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                btnSmall1.Enabled = true;
                                return;
                            }
                        }
                    }
            }
            catch
            {
                SkinHelper.ShowMessage(
                    "Error: Upnp device not found! Set \"Port mapping\" to manual in \"Mp Settings\" and configure your router.",
                    @"Project Celeste",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSmall1.Enabled = true;
                return;
            }

            try
            {
                //Save UserConfig
                Program.UserConfig.GameLanguage = (GameLanguage) comboBox2.SelectedIndex;
                Program.UserConfig.Save(Program.UserConfigFilePath);
            }
            catch
            {
                //
            }

            try
            {
                //Launch Game
                var path = $"{AppDomain.CurrentDomain.BaseDirectory}Spartan.exe";

                if (!File.Exists(path))
                {
                    SkinHelper.ShowMessage(
                        "Error: Spartan.exe not found!",
                        @"Project Celeste",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnSmall1.Enabled = true;
                    return;
                }

                var arg = Program.UserConfig?.MpSettings == null || Program.UserConfig.MpSettings.IsOnline
                    ? $"--email \"{Program.UserConfig.LoginInfo.Email}\"  --password \"{Program.UserConfig.LoginInfo.Password}\" --ignore_rest LauncherLang={comboBox2.Text} LauncherLocale=1033"
                    : $"--email \"{Program.UserConfig.LoginInfo.Email}\"  --password \"{Program.UserConfig.LoginInfo.Password}\" --online-ip \"{Program.UserConfig.MpSettings.PublicIp}\" --ignore_rest LauncherLang={comboBox2.Text} LauncherLocale=1033";

                Process.Start(path, arg);

                WindowState = FormWindowState.Minimized;
            }
            catch (Exception exception)
            {
                SkinHelper.ShowMessage(
                    $"Error: {exception.Message}",
                    @"Project Celeste",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            btnSmall1.Enabled = true;
        }

    /*    private void Label6_Click(object sender, EventArgs e)
        {
            using (var form = new MpSettingForm(Program.UserConfig.MpSettings))
            {
                Hide();
                form.ShowDialog();
                Show();
            }
        }
        */
        private void MainForm_Load(object sender, EventArgs e)
        {

            // Apply tool tips
            SetToolTipsX(); // Applies tooltips to all buttons 
            try
            {
                if (DwmApi.DwmIsCompositionEnabled())
                    DwmApi.DwmExtendFrameIntoClientArea(Handle, new DwmApi.MARGINS(31, 75, 31, 26));
            }
            catch (Exception)
            {
                //
            }
        }

        private void LinkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://aoedb.net/");
        }

        private void LinkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://discord.gg/pkM2RAm");
        }

        private void BtnSmall1_Load(object sender, EventArgs e)
        {
        }

        private void LinkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.reddit.com/r/projectceleste/");
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=EZ3SSAJRRUYFY");
        }

        private void mainContainer1_Load(object sender, EventArgs e)
        {

        }


        // ## Set Tooltips for buttons
        private void SetToolTipsX() {
            // Sets tool times for all buttons with icons
            // Add tool tips to controls
            ToolTip ToolTip1 = new ToolTip();
            ToolTip1.SetToolTip(btnCeleFanProject, "Project Celeste Website"); // Celetstle fan project tool tip

            ToolTip ToolTip2 = new ToolTip();
            ToolTip2.SetToolTip(btnProfileView, "Profile");

            ToolTip ToolTip3 = new ToolTip();
            ToolTip3.SetToolTip(btnAoeoDb, "AOEO DB");

            ToolTip ToolTip4 = new ToolTip();
            ToolTip4.SetToolTip(btnReddit, "Reddit");

            ToolTip ToolTip5 = new ToolTip();
            ToolTip5.SetToolTip(btnDiscord, "Discord");

            ToolTip ToolTip6 = new ToolTip();
            ToolTip6.SetToolTip(btnSettings, "Settings");

            //#
            ToolTip ToolTip7 = new ToolTip();
            ToolTip7.SetToolTip(btnDonate, "Donate");

            ToolTip ToolTip8 = new ToolTip();
            ToolTip8.SetToolTip(btnEsoCom, "ESOCommunity");

            ToolTip ToolTip9 = new ToolTip();
            ToolTip9.SetToolTip(btnFriends, "Friends (Disabled)");

            ToolTip ToolTip10 = new ToolTip();
            ToolTip10.SetToolTip(btnPvpPatch, "PvP Patch Notes");

            ToolTip ToolTip11 = new ToolTip();
            ToolTip11.SetToolTip(btnPatch, "Patch Notes");

            ToolTip ToolTip12 = new ToolTip();
            ToolTip12.SetToolTip(btnBugReport, "Bug Report");

        }



        private void SetRandomBackgroundImageX() {
            // randomly set a background image!
            Random r = new Random();
            int rn = r.Next(1, 8);
            
            switch (rn) {
                case 1: panelMainContainer.BackgroundImage = Properties.Resources.bg1; break;
                case 2: panelMainContainer.BackgroundImage = Properties.Resources.bg2; break;
                case 3: panelMainContainer.BackgroundImage = Properties.Resources.bg3; break;
                case 4: panelMainContainer.BackgroundImage = Properties.Resources.bg4; break;
                case 5: panelMainContainer.BackgroundImage = Properties.Resources.bg5; break;
                case 6: panelMainContainer.BackgroundImage = Properties.Resources.bg6; break;
                case 7: panelMainContainer.BackgroundImage = Properties.Resources.bg7; break;
                case 8: panelMainContainer.BackgroundImage = Properties.Resources.bg7; break;
                default: panelMainContainer.BackgroundImage = Properties.Resources.bg1; break;
            }
        }

       

        private void NextNewsImage() {
            if (curImage >= arrayImg.Count) {
                curImage = 0; 
            }
            pictureBoxNews.Image = arrayImg[curImage];
        }
        private void PreviousNewsImage() {
            if (curImage < 0)
            {
                curImage = arrayImg.Count-1;
                
            }
            pictureBoxNews.Image = arrayImg[curImage];
        }

        private void btnSmall3_Load(object sender, EventArgs e)
        {

        }
         

        private void buttonCloseAccountInfo_Click(object sender, EventArgs e)
        {
            // close the profile info page
            panelAccountInfo.Visible = false;
        }

        private void buttonCloseAccountInfo_MouseHover(object sender, EventArgs e)
        {
            buttonCloseAccountInfo.BackgroundImage = Properties.Resources.XButton_Hover;
        }

        private void buttonCloseAccountInfo_MouseLeave(object sender, EventArgs e)
        {
            buttonCloseAccountInfo.BackgroundImage = Properties.Resources.XButton_Normal;
        }
  
 
        private void btnSmall2_Click_1(object sender, EventArgs e)
        {
            // when mp settings is clicked
            using (var form = new MpSettingForm(Program.UserConfig.MpSettings))
            {
                Hide();
                form.ShowDialog();
                Show();
            }
        }

        private void btnProfileView_Click(object sender, EventArgs e)
        {
            // view profile 
            panelAccountInfo.Visible = true;
            if (panelSettings.Visible) {
                panelSettings.Visible = false;
            }
        }

        private void btnFriends_Click(object sender, EventArgs e)
        {
            // when friends button is clicked, not implemented yet
        }

        private void btnDonate_Click(object sender, EventArgs e)
        {
            // when donate button is clicked open following link
            Process.Start("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=EZ3SSAJRRUYFY");
        }

        private void btnPatch_Click(object sender, EventArgs e)
        {
            // normal patch notes
            Process.Start("https://www.reddit.com/r/projectceleste/comments/6q7qdj/celeste_fan_project_patch_note/");
        }

        private void btnPvpPatch_Click(object sender, EventArgs e)
        {
            // when pvp patch button is clicked
            Process.Start("https://imgur.com/a/ooJO8");
        }

        private void btnAoeoDb_Click(object sender, EventArgs e)
        {
            // when aoeo db button is clicked visit
            Process.Start("http://aoedb.net/");
        }

        private void btnCeleFanProject_Click(object sender, EventArgs e)
        {
            // when celestlefan project btn clicked
            Process.Start("https://projectceleste.com");
        }

        private void btnReddit_Click(object sender, EventArgs e)
        {
            // when reddit button is clicked
            Process.Start("https://www.reddit.com/r/projectceleste/");
        }

        private void btnDiscord_Click(object sender, EventArgs e)
        {
            // when discord button is clicked
            Process.Start("https://discord.gg/pkM2RAm");
        }

        private void btnEsoCom_Click(object sender, EventArgs e)
        {
            // when eso community button is clicked go to
            Process.Start("http://eso-community.net/");
        }

        private void btnBugReport_Click(object sender, EventArgs e)
        {
            // when report button is clicked
            Process.Start("https://github.com/ProjectCeleste/Celeste_Server/issues");
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            // when settings button is pressed
            panelSettings.Visible = true;
            if (panelAccountInfo.Visible == true) {
                panelAccountInfo.Visible = false;
            }
        }

        private void btnCloseSettings_Click(object sender, EventArgs e)
        {
            // when close settings is clicked
            panelSettings.Visible = false;
        }

        private void btnMinimizX_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnCloseX_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnNewsRight_Click(object sender, EventArgs e)
        {
            curImage++;
            NextNewsImage();
        }

        private void btnNewsLeft_Click(object sender, EventArgs e)
        {
            curImage--;
            PreviousNewsImage();
        }

        private void timerSlideShow_Tick(object sender, EventArgs e)
        {
            curImage++;
            NextNewsImage();
        }
         
    }
}