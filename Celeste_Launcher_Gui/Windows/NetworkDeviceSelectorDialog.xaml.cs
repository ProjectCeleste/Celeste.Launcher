using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Celeste_Launcher_Gui.Windows
{
    /// <summary>
    /// Interaction logic for NetworkDeviceSelectorDialog.xaml
    /// </summary>
    public partial class NetworkDeviceSelectorDialog : Window
    {
        public NetworkInterface SelectedInterface { get; private set; }

        public NetworkDeviceSelectorDialog()
        {
            InitializeComponent();
            RefreshNetworkDevices();
        }

        private void BorderMoved(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void RefreshNICList(object sender, RoutedEventArgs e)
        {
            RefreshNetworkDevices();
        }

        private void ConfirmBtnClick(object sender, RoutedEventArgs e)
        {
            var selectedNetworkInterface = NetworkInterfaceListView.SelectedItem as ListViewItem;

            if (selectedNetworkInterface == null)
            {
                GenericMessageDialog.Show("Please select a network interface", DialogIcon.Error, DialogOptions.Ok);
                return;
            }

            SelectedInterface = (NetworkInterface)selectedNetworkInterface.Tag;

            DialogResult = true;
            Close();
        }

        private void RefreshNetworkDevices()
        {
            NetworkInterfaceListView.Items.Clear();

            var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

            foreach (var networkInterface in networkInterfaces)
            {
                var ips = networkInterface.GetIPProperties().UnicastAddresses
                    .Where(key => key.Address.AddressFamily == AddressFamily.InterNetwork);

                foreach (var ip in ips)
                {
                    string content = $"{networkInterface.Name} ({ip.Address})";

                    NetworkInterfaceListView.Items.Add(new ListViewItem
                    {
                        Content = content,
                        Tag = networkInterface
                    });
                }
            }
        }
    }
}
