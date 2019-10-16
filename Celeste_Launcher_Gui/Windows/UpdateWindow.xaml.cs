using Celeste_Launcher_Gui.Helpers;
using Celeste_Launcher_Gui.Services;
using Celeste_Launcher_Gui.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Celeste_Launcher_Gui.Windows
{
    /// <summary>
    /// Interaction logic for UpdateWindow.xaml
    /// </summary>
    public partial class UpdateWindow : Window
    {
        private CancellationTokenSource _cts = new CancellationTokenSource();

        public LauncherVersionInfo LauncherVersionInfo { get; set; }

        public UpdateWindow()
        {
            LauncherVersionInfo = new LauncherVersionInfo();
            InitializeComponent();
            DataContext = LauncherVersionInfo;
        }

        private async void LoadVersionData(object sender, RoutedEventArgs e)
        {
            await UpdateService.LoadUpdateInfo(LauncherVersionInfo);

            if (Version.Parse(LauncherVersionInfo.NewVersion) > Version.Parse(LauncherVersionInfo.CurrentVersion))
            {
                UpdateBtn.Visibility = Visibility.Visible;
            }
        }

        private void OnClose(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OnMoveWindow(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private async void StartUpdate(object sender, RoutedEventArgs e)
        {
            try
            {
                UpdateBtn.Visibility = Visibility.Collapsed;
                UpdateProgressionControls.Visibility = Visibility.Visible;

                _cts.Cancel();
                _cts = new CancellationTokenSource();

                var progress = new Progress<int>();
                progress.ProgressChanged += Progress_ProgressChanged;

                progress.ProgressChanged += (s, value) =>
                {
                    ProgressBar.ProgressBar.Value = value;
                };

                await Updater.DownloadAndInstallUpdate(LegacyBootstrapper.UserConfig.IsSteamVersion, progress, _cts.Token);

                GenericMessageDialog.Show(
                    @"""Celeste Fan Project Launcher"" has been updated, it will now re-start.", DialogIcon.Warning, DialogOptions.Ok);

                Process.Start(Assembly.GetEntryAssembly().Location);

                Environment.Exit(0);
            }
            catch (Exception exception)
            {
                GenericMessageDialog.Show($@"Error: {exception.Message}", DialogIcon.Error, DialogOptions.Ok);
                Environment.Exit(1);
            }
        }

        private void Progress_ProgressChanged(object sender, int e)
        {
            throw new NotImplementedException();
        }
    }
}
