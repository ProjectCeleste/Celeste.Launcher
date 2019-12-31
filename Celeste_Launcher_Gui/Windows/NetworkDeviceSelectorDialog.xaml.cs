using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
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
    /// Interaction logic for NetworkDeviceSelectorDialog.xaml
    /// </summary>
    public partial class NetworkDeviceSelectorDialog : Window
    {
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
                    NetworkInterfaceListView.Items.Add(new ListViewItem
                    {
                        Content = $"{networkInterface.Name} ({ip.Address})"
                    });
                }
            }
        }
    }
}
