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
            lv_NetInterface.Items.Clear();
            var netInterfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (var netInt in netInterfaces)
            {
                var ips = netInt.GetIPProperties().UnicastAddresses
                    .Where(key => key.Address.AddressFamily == AddressFamily.InterNetwork);

                foreach (var ip in ips)
                {
                    var lvi = new ListViewItem
                    {
                        Text = netInt.Name,
                        Tag = ip.Address
                    };

                    lvi.SubItems.Add(ip.Address.ToString());

                    lv_NetInterface.Items.Add(lvi);
                }
            }
        }

        private void NetworkDeviceSelectionDialog_Load(object sender, EventArgs e)
        {
            RefreshNetDevices();
        }

        private void Bnt_ok_Click(object sender, EventArgs e)
        {
            if (lv_NetInterface.SelectedItems.Count <= 0)
            {
                MsgBox.ShowMessage(@"Error: You need to select an network interface first!",
                    @"Project Celeste -- MP Settings",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            SelectedIpAddress = (IPAddress) lv_NetInterface.SelectedItems[0].Tag;
            SelectedInterfaceName = lv_NetInterface.SelectedItems[0].Text;

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