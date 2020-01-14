﻿using System.Linq;
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
            if (!(NetworkInterfaceListView.SelectedItem is ListViewItem selectedNetworkInterface))
            {
                GenericMessageDialog.Show(Properties.Resources.NetworkDeviceSelectorNoDeviceSelected, DialogIcon.Error, DialogOptions.Ok);
                return;
            }

            SelectedInterface = (NetworkInterface)selectedNetworkInterface.Tag;

            DialogResult = true;
            Close();
        }

        private void RefreshNetworkDevices()
        {
            NetworkInterfaceListView.Items.Clear();

            foreach (NetworkInterface networkInterface in NetworkInterface.GetAllNetworkInterfaces())
            {
                System.Collections.Generic.IEnumerable<UnicastIPAddressInformation> ips = networkInterface.GetIPProperties().UnicastAddresses
                    .Where(key => key.Address.AddressFamily == AddressFamily.InterNetwork);

                foreach (UnicastIPAddressInformation ip in ips)
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
