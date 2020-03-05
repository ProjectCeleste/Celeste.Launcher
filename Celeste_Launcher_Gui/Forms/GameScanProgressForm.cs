#region Using directives

using System;
using System.IO;
using System.Windows.Forms;
using Celeste_AOEO_Controls.Helpers;
using Celeste_AOEO_Controls.MsgBox;
using Celeste_Public_Api.Helpers;
using ProjectCeleste.GameFiles.GameScanner;
using ProjectCeleste.GameFiles.GameScanner.Models;

#endregion

namespace Celeste_Launcher_Gui.Forms
{
    public partial class GameScanProgressForm : Form
    {
        private GameScannerManager _gameScanner;
        private readonly int _concurrentDownload;

        public GameScanProgressForm(string gameFilesPath, bool isSteam = false, int concurrentDownload = 0)
        {
            InitializeComponent();

            if (string.IsNullOrWhiteSpace(gameFilesPath))
                throw new Exception(@"Game files path is empty!");

            SkinHelperFonts.SetFont(Controls);

            if (!Directory.Exists(gameFilesPath))
                Directory.CreateDirectory(gameFilesPath);

            _concurrentDownload = concurrentDownload;
            _gameScanner = new GameScannerManager(gameFilesPath, isSteam);
            lbl_ProgressTitle.Text = string.Empty;
            lbl_ProgressDetail.Text = string.Empty;
            lbl_GlobalProgress.Text = string.Empty;
            label1.Text = string.Empty;
            pB_GlobalProgress.Value = 0;
            pB_SubProgress.Value = 0;
        }

        private void ProgressChanged(object sender, ScanProgress e)
        {
            try
            {
                lbl_ProgressTitle.Text = e.File;
                pB_GlobalProgress.Value = (int) e.ProgressPercentage;
                lbl_GlobalProgress.Text = $@"{e.Index}/{e.TotalIndex}";
            }
            catch (Exception)
            {
                //
            }
        }

        private void SubProgressChanged(object sender, ScanSubProgress e)
        {
            try
            {
                if (e.DownloadProgress != null)
                    lbl_ProgressDetail.Text =
                        $"{BytesSizeExtension.FormatToBytesSizeThreeNonZeroDigits(e.DownloadProgress.SizeCompleted)}/{BytesSizeExtension.FormatToBytesSizeThreeNonZeroDigits(e.DownloadProgress.Size)}\r\n" +
                        $"Speed: {BytesSizeExtension.FormatToBytesSizeThreeNonZeroDigits(e.DownloadProgress.Speed)}ps";
                else if (!string.IsNullOrWhiteSpace(lbl_ProgressDetail.Text))
                    lbl_ProgressDetail.Text = string.Empty;

                int baseProgress;
                int rangeMaxProgress;
                switch (e.Step)
                {
                    case ScanSubProgressStep.Check:
                        label1.Text = $@"[{(int) e.Step + 1}/{(int) ScanSubProgressStep.End}] Checking";
                        baseProgress = 0;
                        rangeMaxProgress = 10;
                        break;
                    case ScanSubProgressStep.Download:
                        label1.Text = $@"[{(int) e.Step + 1}/{(int) ScanSubProgressStep.End}] Downloading";
                        baseProgress = 10;
                        rangeMaxProgress = 59;
                        break;
                    case ScanSubProgressStep.CheckDownload:
                        label1.Text = $@"[{(int) e.Step + 1}/{(int) ScanSubProgressStep.End}] Checking downloaded file";
                        baseProgress = 69;
                        rangeMaxProgress = 10;
                        break;
                    case ScanSubProgressStep.ExtractDownload:
                        label1.Text =
                            $@"[{(int) e.Step + 1}/{(int) ScanSubProgressStep.End}] Extracting downloaded file";
                        baseProgress = 79;
                        rangeMaxProgress = 10;
                        break;
                    case ScanSubProgressStep.CheckExtractDownload:
                        label1.Text = $@"[{(int) e.Step + 1}/{(int) ScanSubProgressStep.End}] Checking extracted file";
                        baseProgress = 89;
                        rangeMaxProgress = 10;
                        break;
                    case ScanSubProgressStep.Finalize:
                        label1.Text = $@"[{(int) e.Step + 1}/{(int) ScanSubProgressStep.End}] Finalize";
                        baseProgress = 99;
                        rangeMaxProgress = 1;
                        break;
                    case ScanSubProgressStep.End:
                        label1.Text = @"End";
                        pB_SubProgress.Value = 100;
                        return;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(e.Step), e.Step, null);
                }

                pB_SubProgress.Value = (int) (baseProgress + rangeMaxProgress * (e.ProgressPercentage / 100));
            }
            catch (Exception)
            {
                //
            }
        }

        private void GameScan_FormClosing(object sender, FormClosingEventArgs e)
        {
            _gameScanner?.Dispose();
            _gameScanner = null;
        }

        private void PictureBoxButtonCustom1_Click(object sender, EventArgs e)
        {
            pictureBoxButtonCustom1.Enabled = false;

            DialogResult = DialogResult.Cancel;
        }

        private async void GameScanProgressForm_Shown(object sender, EventArgs e)
        {
            var progress = new Progress<ScanProgress>();
            progress.ProgressChanged += ProgressChanged;
            var subProgress = new Progress<ScanSubProgress>();
            subProgress.ProgressChanged += SubProgressChanged;
            try
            {
                await _gameScanner.InitializeFromCelesteManifest();
                if (await _gameScanner.ScanAndRepair(progress, subProgress, _concurrentDownload))
                {
                    MsgBox.ShowMessage(@"Game scan completed with success.",
                        @"Celeste Fan Project",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    DialogResult = DialogResult.OK;
                }
                else
                {
                    throw new Exception("Game scan failed");
                }
            }
            catch (Exception ex)
            {
                _gameScanner?.Abort();
                _gameScanner?.Dispose();
                _gameScanner = null;

                MsgBox.ShowMessage("Exception:\r\n" + ex.Message,
                    @"Celeste Fan Project",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                DialogResult = DialogResult.Cancel;
            }
        }

        private void GameScanProgressForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (DwmApi.DwmIsCompositionEnabled())
                    DwmApi.DwmExtendFrameIntoClientArea(Handle, new DwmApi.MARGINS(10, 10, 10, 10));
            }
            catch (Exception)
            {
                //
            }
        }
    }
}