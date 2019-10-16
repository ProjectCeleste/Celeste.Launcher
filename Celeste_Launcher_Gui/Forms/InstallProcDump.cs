#region Using directives

using System;
using System.Windows.Forms;
using Celeste_AOEO_Controls.Helpers;
using Celeste_AOEO_Controls.MsgBox;
using Celeste_Launcher_Gui.Helpers;

#endregion

namespace Celeste_Launcher_Gui.Forms
{
    public partial class InstallProcDump : Form
    {
        public InstallProcDump()
        {
            InitializeComponent();

            SkinHelperFonts.SetFont(Controls);
        }

        private void QuickGameScan_Load(object sender, EventArgs e)
        {
            try
            {
                if (DwmApi.DwmIsCompositionEnabled())
                    DwmApi.DwmExtendFrameIntoClientArea(Handle, new DwmApi.MARGINS(8, 8, 8, 8));
            }
            catch (Exception)
            {
                //
            }
        }

        private async void InstallProcDump_Shown(object sender, EventArgs e)
        {
            try
            {
                var progress = new Progress<int>();
                progress.ProgressChanged += (s, o) => { progressBar1.Value = o; };

                await ProcDump.DoDownloadAndInstallProcDump(progress);
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
                Close();
            }
        }
    }
}