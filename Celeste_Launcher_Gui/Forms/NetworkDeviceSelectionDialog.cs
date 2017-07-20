using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Celeste_Launcher_Gui.Forms
{
    public partial class NetworkDeviceSelectionDialog : Form
    {
        private IPAddress selectedAddr = null;

        public NetworkDeviceSelectionDialog()
        {
            InitializeComponent();
        }

        public IPAddress SelectedIPAddress { get => selectedAddr; }

        private void RefreshNetDevices()
        {
            lb_netinterfaces.Items.Clear();
            var netInterfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface netInt in netInterfaces)
            {
                lb_netinterfaces.Items.Add(netInt.Name);
            }
        }

        private void NetworkDeviceSelectionDialog_Load(object sender, EventArgs e)
        {
            RefreshNetDevices();
        }

        private void bnt_ok_Click(object sender, EventArgs e)
        {
            var selectedNetInt = (string)lb_netinterfaces.SelectedItem;
            var netInterface = NetworkInterface.GetAllNetworkInterfaces().FirstOrDefault(elem => elem.Name == selectedNetInt);
            if(netInterface == null)
            {
                MessageBox.Show("Network interface does not exist anymore! Select another one!");
                RefreshNetDevices();
                return;
            }

            // Get IPv4 address:
            bool found = false;
            foreach (UnicastIPAddressInformation ip in netInterface.GetIPProperties().UnicastAddresses)
            {
                if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    selectedAddr = ip.Address;
                    found = true;
                    break;
                }
            }

            if(!found)
            {
                MessageBox.Show("Network interface does not have a local IPv4 address assigned! Select another one!");
                RefreshNetDevices();
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void bnt_refresh_Click(object sender, EventArgs e)
        {
            RefreshNetDevices();
        }

        private void NetworkDeviceSelectionDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(selectedAddr == null)
                DialogResult = DialogResult.Cancel;
        }
    }
}
