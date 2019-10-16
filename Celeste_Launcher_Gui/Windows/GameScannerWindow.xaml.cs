using Celeste_Public_Api.Helpers;
using ProjectCeleste.GameFiles.GameScanner;
using ProjectCeleste.GameFiles.GameScanner.Models;
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

        public GameScannerWindow(string gameFilesPath, bool isSteam)
        {
            InitializeComponent();

            if (string.IsNullOrWhiteSpace(gameFilesPath))
                throw new Exception(@"Game files path is empty!");

            if (!Directory.Exists(gameFilesPath))
                Directory.CreateDirectory(gameFilesPath);

            GameScanner = new GameScannnerManager(gameFilesPath, false, isSteam);
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
                    MainProgressLabel.Content = string.Empty;
                    FileProgress.ProgressBar.IsIndeterminate = false;
                    tB_Report.Text += "Done";
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
            CurrentFileLabel.Content = $"{e.File} ({e.Index}/{e.TotalIndex})";
            FileProgress.ProgressBar.Value = (int)e.ProgressPercentage;
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
            if (e.DownloadProgress != null)
                MainProgressLabel.Content =
                    $"{BytesSizeExtension.FormatToBytesSizeThreeNonZeroDigits(e.DownloadProgress.SizeCompleted)}/{BytesSizeExtension.FormatToBytesSizeThreeNonZeroDigits(e.DownloadProgress.Size)}\r\n" +
                    $"Speed: {BytesSizeExtension.FormatToBytesSizeThreeNonZeroDigits(e.DownloadProgress.Speed)}ps";
            else if (!string.IsNullOrWhiteSpace(MainProgressLabel.Content?.ToString()))
                MainProgressLabel.Content = string.Empty;

            int baseProgress;
            int rangeMaxProgress;
            switch (e.Step)
            {
                case ScanSubProgressStep.Check:
                    tB_Report.Text += $@"[{(int)e.Step + 1}/{(int)ScanSubProgressStep.End}] Checking";
                    FileProgress.ProgressBar.IsIndeterminate = true;
                    baseProgress = 0;
                    rangeMaxProgress = 10;
                    break;
                case ScanSubProgressStep.Download:
                    tB_Report.Text += $@"[{(int)e.Step + 1}/{(int)ScanSubProgressStep.End}] Downloading";
                    FileProgress.ProgressBar.IsIndeterminate = false;
                    baseProgress = 10;
                    rangeMaxProgress = 59;
                    break;
                case ScanSubProgressStep.CheckDownload:
                    tB_Report.Text += $@"[{(int)e.Step + 1}/{(int)ScanSubProgressStep.End}] Checking downloaded file";
                    FileProgress.ProgressBar.IsIndeterminate = true;
                    baseProgress = 69;
                    rangeMaxProgress = 10;
                    break;
                case ScanSubProgressStep.ExtractDownload:
                    tB_Report.Text +=
                        $@"[{(int)e.Step + 1}/{(int)ScanSubProgressStep.End}] Extracting downloaded file";
                    FileProgress.ProgressBar.IsIndeterminate = false;
                    baseProgress = 79;
                    rangeMaxProgress = 10;
                    break;
                case ScanSubProgressStep.CheckExtractDownload:
                    tB_Report.Text += $@"[{(int)e.Step + 1}/{(int)ScanSubProgressStep.End}] Checking extracted file";
                    FileProgress.ProgressBar.IsIndeterminate = true;
                    baseProgress = 89;
                    rangeMaxProgress = 10;
                    break;
                case ScanSubProgressStep.Finalize:
                    tB_Report.Text += $@"[{(int)e.Step + 1}/{(int)ScanSubProgressStep.End}] Finalize";
                    baseProgress = 99;
                    rangeMaxProgress = 1;
                    break;
                case ScanSubProgressStep.End:
                    tB_Report.Text += @"End";
                    ScanTotalProgress.ProgressBar.Value = 100;
                    return;
                default:
                    throw new ArgumentOutOfRangeException(nameof(e.Step), e.Step, null);
            }

            ScanTotalProgress.ProgressBar.Value = (int)(baseProgress + rangeMaxProgress * (e.ProgressPercentage / 100));
            TaskbarItemInfo.ProgressValue = ScanTotalProgress.ProgressBar.Value;
        }
    }
}
