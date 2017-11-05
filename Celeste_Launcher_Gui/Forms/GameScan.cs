#region Using directives

using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using Celeste_AOEO_Controls;
using Celeste_Public_Api;
using Celeste_Public_Api.GameFileInfo.Progress;
using Celeste_Public_Api.Logger;

#endregion

namespace Celeste_Launcher_Gui.Forms
{
    public partial class GameScan : Form
    {
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();

        private bool _scanRunning;

        public GameScan()
        {
            InitializeComponent();
        }

        private void LinkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://www.xbox.com/en-us/developers/rules");
        }

        public static string ToFileSize(double value)
        {
            string[] suffixes =
            {
                "bytes", "KB", "MB", "GB",
                "TB", "PB", "EB", "ZB", "YB"
            };
            for (var i = 0; i < suffixes.Length; i++)
                if (value <= Math.Pow(1024, i + 1))
                    return ThreeNonZeroDigits(value /
                                              Math.Pow(1024, i)) +
                           " " + suffixes[i];

            return ThreeNonZeroDigits(value /
                                      Math.Pow(1024, suffixes.Length - 1)) +
                   " " + suffixes[suffixes.Length - 1];
        }

        private static string ThreeNonZeroDigits(double value)
        {
            if (value >= 100)
                return value.ToString("0,0");
            if (value >= 10)
                return value.ToString("0.0");
            return value.ToString("0.00");
        }

        public void ProgressChanged(object sender, ExProgressGameFiles e)
        {
            pB_GlobalProgress.Value = e.ProgressPercentage;
            lbl_GlobalProgress.Text =
                $@"{e.CurrentIndex}/{e.TotalFile}";
            if (e.ProgressGameFile != null)
            {
                lbl_ProgressTitle.Text = e.ProgressGameFile.FileName;

                pB_SubProgress.Value = e.ProgressGameFile.TotalProgressPercentage;

                if (e.ProgressGameFile.DownloadProgress != null)
                {
                    var speed = e.ProgressGameFile.DownloadProgress.BytesReceived /
                                (e.ProgressGameFile.DownloadProgress.TotalMilliseconds / 1000);

                    lbl_ProgressDetail.Text =
                        $@"Download Speed: {ToFileSize(speed)}/s{Environment.NewLine}" +
                        $@"Progress: {ToFileSize(e.ProgressGameFile.DownloadProgress.BytesReceived)}/{
                                ToFileSize(e.ProgressGameFile.DownloadProgress.TotalBytesToReceive)
                            }";
                }
                else
                {
                    lbl_ProgressDetail.Text = string.Empty;
                }


                if (e.ProgressGameFile.ProgressLog != null)
                {
                    switch (e.ProgressGameFile.ProgressLog.LogLevel)
                    {
                        case LogLevel.Info:
                            tB_Report.Text += e.ProgressGameFile.ProgressLog.Message + Environment.NewLine;
                            break;
                        case LogLevel.Warn:
                            tB_Report.Text += e.ProgressGameFile.ProgressLog.Message + Environment.NewLine;
                            break;
                        case LogLevel.Error:
                            tB_Report.Text += e.ProgressGameFile.ProgressLog.Message + Environment.NewLine;
                            break;
                        case LogLevel.Fatal:
                            tB_Report.Text += e.ProgressGameFile.ProgressLog.Message + Environment.NewLine;
                            break;
                        case LogLevel.Debug:
                            //tB_Report.Text += e.ProgressGameFile.ProgressLog.Message + Environment.NewLine;
                            break;
                        case LogLevel.All:
                            tB_Report.Text += e.ProgressGameFile.ProgressLog.Message + Environment.NewLine;
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

                pB_SubProgress.Value = 0;
            }
        }

        private async void BtnRunScan_Click(object sender, EventArgs e)
        {
            if (_scanRunning)
            {
                if (_cts.IsCancellationRequested)
                    return;

                _cts.Cancel();
                btnRunScan.BtnText = @"...";
                btnRunScan.Enabled = false;

                return;
            }

            try
            {
                _scanRunning = true;
                tB_Report.Text = string.Empty;
                panel9.Enabled = false;
                panel10.Enabled = false;
                btnRunScan.BtnText = @"Cancel";
                if (await Api.GameFiles.FullScanAndRepair(tb_GamePath.Text, ProgressChanged, _cts))
                {
                    //
                }
            }
            catch (Exception ex)
            {
                CustomMsgBox.ShowMessage($@"Error: {ex.Message}",
                    @"Project Celeste -- Game Scan",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _scanRunning = false;
                btnRunScan.BtnText = @"Run Game Scan";
                panel9.Enabled = true;
                panel10.Enabled = true;
                btnRunScan.Enabled = true;
            }
        }

        private void Btn_Browse_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog(){ShowNewFolderButton = true})
            {
                tb_GamePath.Text = fbd.SelectedPath;
            }
        }
    }
}