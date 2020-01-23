using Celeste_Launcher_Gui.Helpers;
using Celeste_Public_Api.Logging;
using Microsoft.Dism;
using Serilog;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Celeste_Launcher_Gui.Windows
{
    /// <summary>
    /// Interaction logic for WindowsFeatureHelper.xaml
    /// </summary>
    public partial class WindowsFeatureHelper : Window
    {
        private static readonly ILogger Logger = LoggerFactory.GetLogger();

        public WindowsFeatureHelper()
        {
            InitializeComponent();
        }

        private void BorderMoved(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void EnableDirectPlayBtnClick(object sender, RoutedEventArgs e)
        {
            IsEnabled = false;
            try
            {
                var feature = await Dism.EnableWindowsFeatures("DirectPlay", OnDismInstallProgress);
                var (statusText, colorLabel, canBeEnabled) = GetLabelStatusForDismFeature(feature);

                DirectPlayStatusLabel.Text = statusText;
                DirectPlayStatusLabel.Foreground = new SolidColorBrush(colorLabel);
                EnableDirectPlayBtn.IsEnabled = canBeEnabled;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, ex.Message);
                GenericMessageDialog.Show(Properties.Resources.GenericUnexpectedErrorMessage, DialogIcon.Error);
            }
            IsEnabled = true;
        }

        private async void EnableNetFrameworkBtnClick(object sender, RoutedEventArgs e)
        {
            IsEnabled = false;
            try
            {
                var feature = await Dism.EnableWindowsFeatures("NetFx3", OnDismInstallProgress);
                var (statusText, colorLabel, canBeEnabled) = GetLabelStatusForDismFeature(feature);

                NetFrameworkStatusLabel.Text = statusText;
                NetFrameworkStatusLabel.Foreground = new SolidColorBrush(colorLabel);
                EnableNetFrameworkBtn.IsEnabled = canBeEnabled;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, ex.Message);
                GenericMessageDialog.Show(Properties.Resources.GenericUnexpectedErrorMessage, DialogIcon.Error);
            }
            IsEnabled = true;
        }

        private void OnDismInstallProgress(DismProgress e)
        {
            ProgressBarIndicator.ProgressBar.Value = Convert.ToInt32(Math.Floor((double)e.Current / e.Total * 100));
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var osInfo = OsVersionInfo.GetOsVersionInfo();
            if (osInfo.Major < 6 || osInfo.Major == 6 && osInfo.Minor < 2)
            {
                GenericMessageDialog.Show(string.Format(Properties.Resources.WindowsFeatureHelperUnsupportedOS, osInfo.FullName),
                    DialogIcon.Warning);

                Close();
                return;
            }

            try
            {
                foreach (var feature in await Dism.GetWindowsFeatureInfo(new[] { "DirectPlay", "NetFx3" }))
                {
                    if (string.Equals(feature.Key, "DirectPlay", StringComparison.CurrentCultureIgnoreCase))
                    {
                        var (statusText, colorLabel, canBeEnabled) = GetLabelStatusForDismFeature(feature.Value);

                        DirectPlayStatusLabel.Text = statusText;
                        DirectPlayStatusLabel.Foreground = new SolidColorBrush(colorLabel);
                        EnableDirectPlayBtn.IsEnabled = canBeEnabled;
                    }
                    else if (string.Equals(feature.Key, "NetFx3", StringComparison.CurrentCultureIgnoreCase))
                    {
                        var (statusText, colorLabel, canBeEnabled) = GetLabelStatusForDismFeature(feature.Value);

                        NetFrameworkStatusLabel.Text = statusText;
                        NetFrameworkStatusLabel.Foreground = new SolidColorBrush(colorLabel);
                        EnableNetFrameworkBtn.IsEnabled = canBeEnabled;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, ex.Message);
                NetFrameworkStatusLabel.Text = Properties.Resources.WindowsFeatureHelperFeatureNotSupportedError;
                NetFrameworkStatusLabel.Foreground = new SolidColorBrush(Colors.Red);
            }
        }

        private static (string statusText, Color labelColor, bool canBeEnabled) GetLabelStatusForDismFeature(DismFeatureInfo featureInfo)
        {
            switch (featureInfo.FeatureState)
            {
                case DismPackageFeatureState.Staged:
                    return (Properties.Resources.WindowsFeatureHelperFeatureStaged, Colors.Chocolate, true);
                case DismPackageFeatureState.PartiallyInstalled:
                    return (Properties.Resources.WindowsFeatureHelperFeaturePartiallyInstalled, Colors.Chocolate, true);
                case DismPackageFeatureState.Installed:
                    return (Properties.Resources.WindowsFeatureHelperFeatureInstalled, Colors.DarkGreen, false);
                case DismPackageFeatureState.InstallPending:
                    return (Properties.Resources.WindowsFeatureHelperFeatureIsPendingInstall, Colors.DarkGreen, false);
                default:
                    return (string.Format(Properties.Resources.WindowsFeatureHelperFeatureUnsupported, featureInfo.FeatureState), Colors.Red, false);
            }
        }
    }
}
