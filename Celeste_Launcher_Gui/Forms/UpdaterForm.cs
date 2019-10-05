#region Using directives

using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Celeste_AOEO_Controls.Helpers;
using Celeste_AOEO_Controls.MsgBox;
using Celeste_Launcher_Gui.Helpers;

#endregion

namespace Celeste_Launcher_Gui.Forms
{
    public partial class UpdaterForm : Form
    {
        private CancellationTokenSource _cts = new CancellationTokenSource();

        public UpdaterForm()
        {
            InitializeComponent();

            SkinHelperFonts.SetFont(Controls);

            richTextBox1.SetInnerMargins(20, 15, 20, 15);
        }

        private async void UpdaterForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (DwmApi.DwmIsCompositionEnabled())
                    DwmApi.DwmExtendFrameIntoClientArea(Handle, new DwmApi.MARGINS(10, 12, 10, 12));
            }
            catch (Exception)
            {
                //
            }

            lbl_CurrentV.Text = $@"Current Version: v{Assembly.GetEntryAssembly()?.GetName().Version}";

            var gitVersion = await Updater.GetGitHubVersion();
            lbl_LatestV.Text = $@"Latest Version: v{gitVersion}";

            richTextBox1.Text = await Updater.GetChangeLog();

            if (gitVersion > Assembly.GetExecutingAssembly().GetName().Version)
                return;

            btnSmall1.Enabled = false;
        }

        private async void BtnSmall1_Click(object sender, EventArgs e)
        {
            try
            {
                btnSmall1.Enabled = false;
                pictureBoxButtonCustom1.Enabled = false;
                btnSmall1.BtnText = "...";

                _cts.Cancel();
                _cts = new CancellationTokenSource();

                var progress = new Progress<int>();
                progress.ProgressChanged += (s, o) => { pB_Progress.Value = o; };

                await Updater.DownloadAndInstallUpdate(Program.UserConfig.IsSteamVersion, progress, _cts.Token);

                MsgBox.ShowMessage(
                    @"""Celeste Fan Project Launcher"" has been updated, it will now re-start.",
                    @"Celeste Fan Project",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                Process.Start(Assembly.GetEntryAssembly().Location);
            }
            catch (Exception exception)
            {
                MsgBox.ShowMessage(
                    $@"Error: {exception.Message}",
                    @"Celeste Fan Project",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                Environment.Exit(0);
            }
        }

        private void PictureBoxButtonCustom1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}