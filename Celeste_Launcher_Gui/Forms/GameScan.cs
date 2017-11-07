#region Using directives

using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Celeste_AOEO_Controls;
using Celeste_Public_Api.GameScanner;
using Celeste_Public_Api.GameScanner.Models;
using Celeste_Public_Api.Helpers;
using Celeste_Public_Api.Logger;

#endregion

namespace Celeste_Launcher_Gui.Forms
{
    public partial class GameScan : Form
    {
        private GameScannnerApi GameScannner { get; set; }

        public GameScan()
        {
            InitializeComponent();

            if (Program.UserConfig != null && !string.IsNullOrEmpty(Program.UserConfig.GameFilesPath))
            {
                tb_GamePath.Text = Program.UserConfig.GameFilesPath;
            }
            else
            {
                tb_GamePath.Text = GameScannnerApi.GetGameFilesRootPath();
            }
            GameScannner = new GameScannnerApi(null, tb_GamePath.Text);

        }

        private void LinkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://www.xbox.com/en-us/developers/rules");
        }

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
                    var speed = e.ScanAndRepairFileProgress.L33TZipExtractProgress.BytesExtracted /
                                (e.ScanAndRepairFileProgress.L33TZipExtractProgress.TotalMilliseconds / 1000);

                    lbl_ProgressDetail.Text =
                        $@"Extract Speed: {Misc.ToFileSize(speed)}/s{Environment.NewLine}" +
                        $@"Progress: {Misc.ToFileSize(e.ScanAndRepairFileProgress.L33TZipExtractProgress.BytesExtracted)}/{
                                Misc.ToFileSize(e.ScanAndRepairFileProgress.L33TZipExtractProgress.TotalBytesToExtract)
                            }";
                }
                else
                {
                    lbl_ProgressDetail.Text = string.Empty;
                }

                if (e.ScanAndRepairFileProgress.ProgressLog != null)
                {
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
            }
            else
            {
                lbl_ProgressTitle.Text = string.Empty;
                lbl_ProgressDetail.Text = string.Empty;
                pB_SubProgress.Value = 0;
            }
        }
        
        private async void BtnRunScan_Click(object sender, EventArgs e)
        {
            if (GameScannner.IsScanRunning)
            {
                GameScannner.CancelScan();

                btnRunScan.BtnText = @"...";
                btnRunScan.Enabled = false;

                return;
            }

            if(string.IsNullOrEmpty(tb_GamePath.Text))
            {
                MsgBox.ShowMessage(@"Error: Game files path is empty!",
                    @"Project Celeste -- Game Scan",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (!Directory.Exists(tb_GamePath.Text))
            {
                MsgBox.ShowMessage(@"Error: Game files path don't exist!",
                    @"Project Celeste -- Game Scan",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            try
            {
                tB_Report.Text = string.Empty;
                panel9.Enabled = false;
                panel10.Enabled = false;
                btnRunScan.BtnText = @"Cancel";
                var progress = new Progress<ScanAndRepairProgress>();
                progress.ProgressChanged += ProgressChanged;
                if (await GameScannner.ScanAndRepair(progress))
                {
                    //
                }
            }
            catch (Exception ex)
            {
                MsgBox.ShowMessage($@"Error: {ex.Message}",
                    @"Project Celeste -- Game Scan",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnRunScan.BtnText = @"Run Game Scan";
                panel9.Enabled = true;
                panel10.Enabled = true;
                btnRunScan.Enabled = true;
            }
        }

        private void Btn_Browse_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog {ShowNewFolderButton = true})
            {
                fbd.ShowDialog();
                tb_GamePath.Text = fbd.SelectedPath;
            }
        }

        private void GameScan_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!GameScannner.IsScanRunning)
                return;

            MsgBox.ShowMessage(@"Error: You need to cancel the ""Game Scan"" first!",
                @"Project Celeste -- Game Scan",
                MessageBoxButtons.OK, MessageBoxIcon.Error);

            e.Cancel = true;
        }
    }
}