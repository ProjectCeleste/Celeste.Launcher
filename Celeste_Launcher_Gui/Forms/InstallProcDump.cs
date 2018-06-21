#region Using directives

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Celeste_AOEO_Controls.Helpers;
using Celeste_Public_Api.Helpers;

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
            await DoDownloadAndInstallProcdump();
        }

        private async Task DoDownloadAndInstallProcdump(CancellationToken ct = new CancellationToken())
        {
            const string downloadLink = @"https://download.sysinternals.com/files/Procdump.zip";

            var tempFileName = Path.GetTempFileName();

            try
            {
                var dowloadProgress = new Progress<DownloadFileProgress>();
                dowloadProgress.ProgressChanged += (o, ea) =>
                {
                    progressBar1.Value = Convert.ToInt32(Math.Floor(75 * ((double) ea.ProgressPercentage / 100)));
                };
                var downloadFileAsync = new DownloadFileUtils(new Uri(downloadLink), tempFileName, dowloadProgress);
                await downloadFileAsync.DoDownload(ct);

                var extractProgress = new Progress<ZipFileProgress>();
                extractProgress.ProgressChanged += (o, ea) =>
                {
                    progressBar1.Value =
                        75 + Convert.ToInt32(Math.Floor(25 * ((double) ea.ProgressPercentage / 100)));
                };
                await ZipUtils.DoExtractZipFile(tempFileName, AppDomain.CurrentDomain.BaseDirectory, extractProgress,
                    ct);
            }
            finally
            {
                if (File.Exists(tempFileName))
                    File.Delete(tempFileName);
            }
            //
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}