#region Using directives

using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Celeste_AOEO_Controls.Helpers;
using Celeste_AOEO_Controls.MsgBox;
using Celeste_Public_Api.GameScanner_Api;
using Celeste_Public_Api.GameScanner_Api.Models;
using Celeste_Public_Api.Helpers;

#endregion

namespace Celeste_Launcher_Gui.Forms
{
    public partial class GameScanProgressForm : Form
    {
        public GameScanProgressForm(string gameFilesPath, bool isBetaUpdate)
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(gameFilesPath))
            {
                throw  new Exception(@"Game files path is empty!");
            }

            SkinHelper.SetFont(Controls);
            
            if (!Directory.Exists(gameFilesPath))
            {
                Directory.CreateDirectory(gameFilesPath);
            }

            GameScannner = new GameScannnerApi(isBetaUpdate, gameFilesPath);
            lbl_ProgressTitle.Text = string.Empty;
            lbl_ProgressDetail.Text = string.Empty;
            lbl_GlobalProgress.Text = $@"0/{GameScannner.FilesInfo.Count()}";
            pB_GlobalProgress.Value = 0;
            pB_SubProgress.Value = 0;
            tB_Report.Text = string.Empty;
        }

        private GameScannnerApi GameScannner { get; }

        public void ProgressChanged(object sender, ScanAndRepairProgress e)
        {
            pB_GlobalProgress.Value = e.ProgressPercentage;
            lbl_GlobalProgress.Text =
                $@"{e.CurrentIndex}/{e.TotalFile}";
            if (e.ScanAndRepairFileProgress != null)
            {
                lbl_ProgressTitle.Text = e.ScanAndRepairFileProgress.FileName;

                pB_SubProgress.Value = e.ScanAndRepairFileProgress.TotalProgressPercentage;

                if (e.ScanAndRepairFileProgress.DownloadFileProgress != null)
                {
                    var speed = e.ScanAndRepairFileProgress.DownloadFileProgress.BytesReceived /
                                (e.ScanAndRepairFileProgress.DownloadFileProgress.TotalMilliseconds / 1000);

                    lbl_ProgressDetail.Text =
                        $@"Download Speed: {Misc.ToFileSize(speed)}/s{Environment.NewLine}" +
                        $@"Progress: {Misc.ToFileSize(e.ScanAndRepairFileProgress.DownloadFileProgress.BytesReceived)}/{
                                Misc.ToFileSize(e.ScanAndRepairFileProgress.DownloadFileProgress.TotalBytesToReceive)
                            }";
                }
                else if (e.ScanAndRepairFileProgress.L33TZipExtractProgress != null)
                {
                    var speed = e.ScanAndRepairFileProgress.L33TZipExtractProgress.BytesProcessed /
                                (e.ScanAndRepairFileProgress.L33TZipExtractProgress.TotalMilliseconds / 1000);

                    lbl_ProgressDetail.Text =
                        $@"Extract Speed: {Misc.ToFileSize(speed)}/s{Environment.NewLine}" +
                        $@"Progress: {
                                Misc.ToFileSize(e.ScanAndRepairFileProgress.L33TZipExtractProgress.BytesProcessed)
                            }/{
                                Misc.ToFileSize(e.ScanAndRepairFileProgress.L33TZipExtractProgress.TotalBytesToProcess)
                            }";
                }
                else
                {
                    lbl_ProgressDetail.Text = string.Empty;
                }

                if (e.ProgressLog != null)
                {
                    switch (e.ProgressLog.LogLevel)
                    {
                        case LogLevel.Info:
                            tB_Report.Text += e.ProgressLog.Message + Environment.NewLine;
                            break;
                        case LogLevel.Warn:
                            tB_Report.Text += e.ProgressLog.Message + Environment.NewLine;
                            break;
                        case LogLevel.Error:
                            tB_Report.Text += e.ProgressLog.Message + Environment.NewLine;
                            break;
                        case LogLevel.Fatal:
                            tB_Report.Text += e.ProgressLog.Message + Environment.NewLine;
                            break;
                        case LogLevel.Debug:
                            //tB_Report.Text += e.ProgressLog.Message + Environment.NewLine;
                            break;
                        case LogLevel.All:
                            tB_Report.Text += e.ProgressLog.Message + Environment.NewLine;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    tB_Report.SelectionStart = tB_Report.TextLength;
                    tB_Report.ScrollToCaret();
                }

                if (e.ScanAndRepairFileProgress.ProgressLog == null)
                    return;

                switch (e.ScanAndRepairFileProgress.ProgressLog.LogLevel)
                {
                    case LogLevel.Info:
                        tB_Report.Text += e.ScanAndRepairFileProgress.ProgressLog.Message + Environment.NewLine;
                        break;
                    case LogLevel.Warn:
                        tB_Report.Text += e.ScanAndRepairFileProgress.ProgressLog.Message + Environment.NewLine;
                        break;
                    case LogLevel.Error:
                        tB_Report.Text += e.ScanAndRepairFileProgress.ProgressLog.Message + Environment.NewLine;
                        break;
                    case LogLevel.Fatal:
                        tB_Report.Text += e.ScanAndRepairFileProgress.ProgressLog.Message + Environment.NewLine;
                        break;
                    case LogLevel.Debug:
                        //tB_Report.Text += e.ProgressGameFile.ProgressLog.Message + Environment.NewLine;
                        break;
                    case LogLevel.All:
                        tB_Report.Text += e.ScanAndRepairFileProgress.ProgressLog.Message + Environment.NewLine;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                tB_Report.SelectionStart = tB_Report.TextLength;
                tB_Report.ScrollToCaret();
            }
            else
            {
                lbl_ProgressTitle.Text = string.Empty;
                lbl_ProgressDetail.Text = string.Empty;
                pB_SubProgress.Value = 0;
            }
        }
               
        private void GameScan_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!GameScannner.IsScanRunning)
                return;

            pictureBoxButtonCustom1.Enabled = false;
            GameScannner.CancelScan();
            while (GameScannner.IsScanRunning)
            {
                //
            }
        }

        private void PictureBoxButtonCustom1_Click(object sender, EventArgs e)
        {
            pictureBoxButtonCustom1.Enabled = false;
            Close();
        }

        private async void GameScanProgressForm_Shown(object sender, EventArgs e)
        {
            try
            {
                var progress = new Progress<ScanAndRepairProgress>();
                progress.ProgressChanged += ProgressChanged;
                if (await GameScannner.ScanAndRepair(progress))
                {
                    MsgBox.ShowMessage(@"Game scan completed with success.",
                        @"Project Celeste -- Game Scan",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    DialogResult = DialogResult.OK;
                }
                else
                {
                    MsgBox.ShowMessage(@"Game scan failed!",
                        @"Project Celeste -- Game Scan",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    DialogResult = DialogResult.Abort;
                }
            }
            catch (Exception ex)
            {
                MsgBox.ShowMessage($@"Error: {ex.Message}",
                    @"Project Celeste -- Game Scan",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                DialogResult = DialogResult.Abort;
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