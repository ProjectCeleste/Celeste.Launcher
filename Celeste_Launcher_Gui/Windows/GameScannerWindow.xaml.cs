using Celeste_Launcher_Gui.Helpers;
using Celeste_Public_Api.Helpers;
using Celeste_Public_Api.Logging;
using ProjectCeleste.GameFiles.GameScanner;
using ProjectCeleste.GameFiles.GameScanner.Models;
using Serilog;
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
        private GameScannnerManager GameScanner;
        private static readonly ILogger Logger = LoggerFactory.GetLogger();

        public GameScannerWindow(string gameFilesPath, bool isSteam)
        {
            InitializeComponent();

            if (string.IsNullOrWhiteSpace(gameFilesPath))
                throw new Exception(@"Game files path is empty!");

            if (!Directory.Exists(gameFilesPath))
                Directory.CreateDirectory(gameFilesPath);

            GameScanner = new GameScannnerManager(gameFilesPath, true, isSteam);
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
            GameScanner.Dispose();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                await GameScanner.InitializeFromCelesteManifest();
                var progress = new Progress<ScanProgress>();
                var subProgress = new Progress<ScanSubProgress>();

                progress.ProgressChanged += ProgressChanged;
                subProgress.ProgressChanged += SubProgressChanged;

                if (await GameScanner.ScanAndRepair(progress, subProgress))
                {
                    CurrentFileLabel.Content = string.Empty;
                    MainProgressLabel.Content = "Done";
                    FileProgress.ProgressBar.IsIndeterminate = false;
                    GenericMessageDialog.Show($@"Game scan has succesfully completed", DialogIcon.None, DialogOptions.Ok);
                    DialogResult = true;
                }
                else
                {
                    FailGameScan("Game scan failed");
                }
            }
            catch (Exception ex)
            {
                FailGameScan(ex.Message);
            }
        }

        private void ProgressChanged(object sender, ScanProgress e)
        {
            var wrappedFileName = e.File.WrapIfLengthIsLongerThan(35, "...");
            CurrentFileLabel.Content = $"{wrappedFileName} ({e.Index}/{e.TotalIndex})";
            ScanTotalProgress.ProgressBar.Value = e.ProgressPercentage;
            TaskbarItemInfo.ProgressValue = (e.ProgressPercentage / 100);
        }

        private void FailGameScan(string reason)
        {
            FileProgress.ProgressBar.Foreground = Brushes.Red;
            ScanTotalProgress.ProgressBar.Foreground = Brushes.Red;
            CurrentFileLabel.Content = string.Empty;
            MainProgressLabel.Content = string.Empty;
            TaskbarItemInfo.ProgressState = System.Windows.Shell.TaskbarItemProgressState.Error;
            GenericMessageDialog.Show($@"Error: {reason}", DialogIcon.Error, DialogOptions.Ok);
        }

        private void SubProgressChanged(object sender, ScanSubProgress e)
        {
            switch (e.Step)
            {
                case ScanSubProgressStep.Check:
                    MainProgressLabel.Content = $@"Verifying file integrity";
                    FileProgress.ProgressBar.IsIndeterminate = false;
                    FileProgress.ProgressBar.Value = e.ProgressPercentage;
                    break;
                case ScanSubProgressStep.Download:
                    if (e.DownloadProgress != null)
                    {
                        if (e.DownloadProgress.Size == 0)
                        {
                            MainProgressLabel.Content = "Starting download";
                        }
                        else
                        {
                            var downloaded = BytesSizeExtension.FormatToBytesSizeThreeNonZeroDigits(e.DownloadProgress.SizeCompleted);
                            var leftToDownload = BytesSizeExtension.FormatToBytesSizeThreeNonZeroDigits(e.DownloadProgress.Size);

                            var downloadSpeed = double.IsInfinity(e.DownloadProgress.Speed) ?
                                string.Empty : $"({BytesSizeExtension.FormatToBytesSizeThreeNonZeroDigits(e.DownloadProgress.Speed)}/s)";

                            MainProgressLabel.Content = $"Downloading {downloaded}/{leftToDownload} {downloadSpeed}";
                        }
                    }

                    FileProgress.ProgressBar.Value = e.ProgressPercentage;
                    FileProgress.ProgressBar.IsIndeterminate = false;
                    break;
                case ScanSubProgressStep.CheckDownload:
                    MainProgressLabel.Content = $@"Checking downloaded file";
                    FileProgress.ProgressBar.IsIndeterminate = true;
                    break;
                case ScanSubProgressStep.ExtractDownload:
                    MainProgressLabel.Content = $@"Extracting downloaded file";
                    FileProgress.ProgressBar.Value = e.ProgressPercentage;
                    FileProgress.ProgressBar.IsIndeterminate = false;
                    break;
                case ScanSubProgressStep.CheckExtractDownload:
                    MainProgressLabel.Content = $@"Checking extracted file";
                    FileProgress.ProgressBar.IsIndeterminate = true;
                    break;
                case ScanSubProgressStep.Finalize:
                    MainProgressLabel.Content = $@"Finalizing";
                    FileProgress.ProgressBar.IsIndeterminate = true;
                    break;
                case ScanSubProgressStep.End:
                    FileProgress.ProgressBar.Value = 100;
                    ScanTotalProgress.ProgressBar.Value = 100;
                    TaskbarItemInfo.ProgressValue = 100;
                    return;
                default:
                    throw new ArgumentOutOfRangeException(nameof(e.Step), e.Step, null);
            }
        }
    }
}
