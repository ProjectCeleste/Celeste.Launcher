#region Using directives

using System;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Windows.Forms;
using Celeste_AOEO_Controls;

#endregion

namespace Celeste_Launcher_Gui.Forms
{
    public partial class NetworkDeviceSelectionDialog : Form
    {
        public NetworkDeviceSelectionDialog()
        {
            InitializeComponent();
        }


        public string SelectedInterfaceName { get; private set; }
        public IPAddress SelectedIpAddress { get; private set; }

        private void RefreshNetDevices()
        {
            lb_netinterfaces.Items.Clear();
            var netInterfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (var netInt in netInterfaces)
                lb_netinterfaces.Items.Add(netInt.Name);
        }

        private void NetworkDeviceSelectionDialog_Load(object sender, EventArgs e)
        {
            RefreshNetDevices();
        }

        private void Bnt_ok_Click(object sender, EventArgs e)
        {
            var selectedNetInt = (string) lb_netinterfaces.SelectedItem;
            var netInterface = NetworkInterface.GetAllNetworkInterfaces()
                .FirstOrDefault(elem => elem.Name == selectedNetInt);
            if (netInterface == null)
            {
                CustomMsgBox.ShowMessage(@"Network interface does not exist anymore. Select another one!",
                    "Celeste Fan Project", MessageBoxButtons.OK, MessageBoxIcon.Error);
                RefreshNetDevices();
                return;
            }

            // Get IPv4 address:
            var found = false;
            foreach (var ip in netInterface.GetIPProperties().UnicastAddresses)
                if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                {
                    SelectedIpAddress = ip.Address;
                    SelectedInterfaceName = selectedNetInt;
                    found = true;
                    break;
                }

            if (!found)
            {
                CustomMsgBox.ShowMessage(
                    @"Network interface does not have a local IPv4 address assigned. Select another one!",
                    "Celeste Fan Project", MessageBoxButtons.OK, MessageBoxIcon.Error);
                RefreshNetDevices();
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void Bnt_refresh_Click(object sender, EventArgs e)
        {
            RefreshNetDevices();
        }

        private void NetworkDeviceSelectionDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (SelectedIpAddress == null)
                DialogResult = DialogResult.Cancel;
        }
    }
}