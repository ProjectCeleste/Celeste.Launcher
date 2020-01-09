using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Input;

namespace Celeste_Launcher_Gui.Windows
{
    /// <summary>
    /// Interaction logic for MultiplayerSettings.xaml
    /// </summary>
    public partial class MultiplayerSettings : Window
    {
        private string _selectedInterfaceName;

        public MultiplayerSettings()
        {
            InitializeComponent();

            var mpSettings = LegacyBootstrapper.UserConfig.MpSettings;
            switch (mpSettings.ConnectionType)
            {
                case ConnectionType.Wan:
                    WanConnectionTypeCheckBox.IsChecked = true;
                    break;
                case ConnectionType.Lan:
                    LanConnectionTypeCheckBox.IsChecked = true;
                    break;
                case ConnectionType.Other:
                    OtherConnectionTypeCheckBox.IsChecked = true;
                    break;
            }

            switch (mpSettings.PortMappingType)
            {
                case PortMappingType.NatPunch:
                    NatPunchthroughPortMappingCheckBox.IsChecked = true;
                    break;
                case PortMappingType.Upnp:
                    UPnPCPortMappingheckBox.IsChecked = true;
                    break;
                case PortMappingType.Manual:
                    ManualPortMappingCheckBox.IsChecked = true;
                    break;
            }
        }

        private void BorderMoved(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SaveButtonClick(object sender, RoutedEventArgs e)
        {

            if (WanConnectionTypeCheckBox.IsChecked == true)
            {
                LegacyBootstrapper.UserConfig.MpSettings.ConnectionType = ConnectionType.Wan;
                LegacyBootstrapper.UserConfig.MpSettings.LanNetworkInterface = null;
            }
            else if (LanConnectionTypeCheckBox.IsChecked == true)
            {
                LegacyBootstrapper.UserConfig.MpSettings.ConnectionType = ConnectionType.Lan;
                LegacyBootstrapper.UserConfig.MpSettings.LanNetworkInterface = _selectedInterfaceName;
            }
            else if (OtherConnectionTypeCheckBox.IsChecked == true)
            {
                LegacyBootstrapper.UserConfig.MpSettings.ConnectionType = ConnectionType.Other;
                LegacyBootstrapper.UserConfig.MpSettings.LanNetworkInterface = null;
            }

            if (NatPunchthroughPortMappingCheckBox.IsChecked == true)
                LegacyBootstrapper.UserConfig.MpSettings.PortMappingType = PortMappingType.NatPunch;
            else if (UPnPCPortMappingheckBox.IsChecked == true)
                LegacyBootstrapper.UserConfig.MpSettings.PortMappingType = PortMappingType.Upnp;
            else if (ManualPortMappingCheckBox.IsChecked == true)
                LegacyBootstrapper.UserConfig.MpSettings.PortMappingType = PortMappingType.Manual;

            LegacyBootstrapper.UserConfig.MpSettings.PublicIp = RemoteIPField.InputContent;

            LegacyBootstrapper.UserConfig.Save(LegacyBootstrapper.UserConfigFilePath);

            Close();
        }

        private void WanConnectionTypeCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (WanConnectionTypeCheckBox.IsChecked == true)
            {
                if (LegacyBootstrapper.CurrentUser != null &&
                    !string.IsNullOrWhiteSpace(LegacyBootstrapper.CurrentUser.Ip))
                {
                    RemoteIPField.InputContent = LegacyBootstrapper.CurrentUser.Ip;
                }
                else
                {
                    //FallBack to Other
                    RemoteIPField.InputContent = "127.0.0.1";
                    OtherConnectionTypeCheckBox.IsChecked = true;
                    return;
                }

                _selectedInterfaceName = string.Empty;
            }
        }

        private void OtherConnectionTypeCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (OtherConnectionTypeCheckBox.IsChecked == true)
            {
                _selectedInterfaceName = Properties.Resources.MultiplayerSettingsOtherNetworkDevice;
            }
            else
            {
                _selectedInterfaceName = string.Empty;
            }
        }

        private void LanConnectionTypeCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (LanConnectionTypeCheckBox.IsChecked == false)
                return;

            var networkInterface = NetworkInterface.GetAllNetworkInterfaces()
                .FirstOrDefault(t => t.Name == LegacyBootstrapper.UserConfig?.MpSettings?.LanNetworkInterface);

            if (networkInterface == null)
            {
                var netDeviceSelectDialog = new NetworkDeviceSelectorDialog();
                netDeviceSelectDialog.Owner = this;
                netDeviceSelectDialog.ShowDialog();

                if (netDeviceSelectDialog.DialogResult != true)
                {
                    // Fallback to Wan
                    WanConnectionTypeCheckBox.IsChecked = true;
                    return;
                }

                _selectedInterfaceName = netDeviceSelectDialog.SelectedInterface.Name;
                networkInterface = netDeviceSelectDialog.SelectedInterface;
            }

            // Get IPv4 address of the network interface:
            if (networkInterface != null)
            {
                foreach (var ip in networkInterface.GetIPProperties().UnicastAddresses)
                {
                    if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        RemoteIPField.InputContent = ip.Address.ToString();
                        return;
                    }
                }
            }
        }
    }
}
