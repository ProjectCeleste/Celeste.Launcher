using Celeste_Launcher_Gui.Services;
using Celeste_Launcher_Gui.ViewModels;
using ProjectCeleste.Launcher.PublicApi.Logging;
using Serilog;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace Celeste_Launcher_Gui.Windows
{
    /// <summary>
    /// Interaction logic for UpdateWindow.xaml
    /// </summary>
    public partial class UpdateWindow : Window
    {
        private static readonly ILogger Logger = LoggerFactory.GetLogger();

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

                Progress<int> progress = new Progress<int>();

                progress.ProgressChanged += (s, value) => ProgressBar.ProgressBar.Value = value;

                await UpdateService.DownloadAndInstallUpdate(LegacyBootstrapper.UserConfig.IsSteamVersion, progress, _cts.Token);

                GenericMessageDialog.Show(Properties.Resources.LauncherUpdaterUpdateSuccess, DialogIcon.Warning, DialogOptions.Ok);

                Process.Start(Assembly.GetEntryAssembly().Location);

                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, ex.Message);
                GenericMessageDialog.Show(Properties.Resources.LauncherUpdaterError, DialogIcon.Error, DialogOptions.Ok);
                Environment.Exit(1);
            }
        }
    }
}