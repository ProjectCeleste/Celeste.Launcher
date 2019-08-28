using Celeste_Public_Api.GameScanner_Api;
using Celeste_Public_Api.GameScanner_Api.Models;
using Celeste_Public_Api.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Celeste_Launcher_Gui.Windows
{
    /// <summary>
    /// Interaction logic for GameScannerWindow.xaml
    /// </summary>
    public partial class GameScannerWindow : Window
    {
        private GameScannnerApi GameScanner;

        public GameScannerWindow(string gameFilesPath, bool isSteam)
        {
            InitializeComponent();

            if (string.IsNullOrWhiteSpace(gameFilesPath))
                throw new Exception(@"Game files path is empty!");

            if (!Directory.Exists(gameFilesPath))
                Directory.CreateDirectory(gameFilesPath);

            GameScanner = new GameScannnerApi(gameFilesPath, isSteam);
        }

        private void OnClose(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OnMoveWindow(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void OpenPatchNotes(object sender, RoutedEventArgs e)
        {
            Process.Start("https://forums.projectceleste.com/forums/announcements.12/");
        }

        private void OpenDiscord(object sender, RoutedEventArgs e)
        {
            Process.Start("https://discord.gg/xXFUvWA");
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (GameScanner.IsScanRunning)
                GameScanner.CancelScan();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                await GameScanner.InitializeAsync();
                var progress = new Progress<ScanAndRepairProgress>();
                progress.ProgressChanged += ProgressChanged;
                if (await GameScanner.ScanAndRepair(progress))
                {
                    GenericMessageDialog.Show($@"Game scan completed with success.", DialogIcon.None, DialogOptions.Ok);
                    DialogResult = true;
                }
                else
                {
                    GenericMessageDialog.Show($@"Game scan failed", DialogIcon.Error, DialogOptions.Ok);
                }
            }
            catch (Exception ex)
            {
                GenericMessageDialog.Show($@"Error: {ex.Message}", DialogIcon.Error, DialogOptions.Ok);
            }
        }

        public void ProgressChanged(object sender, ScanAndRepairProgress e)
        {
            pB_GlobalProgress.ProgressBar.Value = e.ProgressPercentage;
            if (e.ScanAndRepairFileProgress != null)
            {
                lbl_ProgressTitle.Content = $"{e.ScanAndRepairFileProgress.FileName} ({e.CurrentIndex}/{e.TotalFile})";

                pB_SubProgress.ProgressBar.Value = e.ScanAndRepairFileProgress.TotalProgressPercentage;

                if (e.ScanAndRepairFileProgress.DownloadFileProgress != null)
                {
                    var speed = e.ScanAndRepairFileProgress.DownloadFileProgress.BytesReceived /
                                (e.ScanAndRepairFileProgress.DownloadFileProgress.TotalMilliseconds / 1000);

                    lbl_ProgressDetail.Content = $"Downloading {e.ScanAndRepairFileProgress.FileName} ({Misc.ToFileSize(speed)}/s)"; ;
                }
                else if (e.ScanAndRepairFileProgress.L33TZipExtractProgress != null)
                {
                    lbl_ProgressDetail.Content = $@"Extracting {
                                Misc.ToFileSize(e.ScanAndRepairFileProgress.L33TZipExtractProgress.BytesProcessed)
                            }/{
                                Misc.ToFileSize(e.ScanAndRepairFileProgress.L33TZipExtractProgress.TotalBytesToProcess)
                            }";
                }
                else
                {
                    lbl_ProgressDetail.Content = "Waiting";
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

                    tB_Report.ScrollToEnd();
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

                tB_Report.ScrollToEnd();
            }
            else
            {
                lbl_ProgressTitle.Content = string.Empty;
                lbl_ProgressDetail.Content = string.Empty;
                pB_SubProgress.ProgressBar.Value = 0;
            }
        }
    }
}
