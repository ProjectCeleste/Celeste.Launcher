#region Using directives

using System;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Windows.Forms;
using Celeste_Launcher_Gui.Helpers;

#endregion

namespace Celeste_Launcher_Gui.Forms
{
    public partial class MpSettingForm : Form
    {
        private bool _isFirstRun = true;
        private string _selectedInterfaceName;

        public MpSettingForm(MpSettings mpSettings)
        {
            InitializeComponent();

            //Configure Fonts
            SkinHelper.SetFont(Controls);

            //MpSettings
            if (rb_Wan.Checked != mpSettings.IsOnline)
                rb_Wan.Checked = mpSettings.IsOnline;

            if (rb_Lan.Checked != !mpSettings.IsOnline)
                rb_Lan.Checked = !mpSettings.IsOnline;

            if (rb_Automatic.Checked != mpSettings.AutoPortMapping)
                rb_Automatic.Checked = mpSettings.AutoPortMapping;

            if (rb_Manual.Checked != !mpSettings.AutoPortMapping)
                rb_Manual.Checked = !mpSettings.AutoPortMapping;

            //numericUpDown2.Value = mpSettings.PublicPort;
        }


        private void MpSettingBox_Load(object sender, EventArgs e)
        {
            try
            {
                if (DwmApi.DwmIsCompositionEnabled())
                    DwmApi.DwmExtendFrameIntoClientArea(Handle, new DwmApi.MARGINS(31, 75, 31, 26));
            }
            catch (Exception)
            {
                //
            }

            _isFirstRun = false;
        }

        //private string getExternalIp()
        //{
        //    try
        //    {
        //        var externalIp = new WebClient().DownloadString("http://checkip.dyndns.org/");
        //        externalIp = new Regex(@"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}")
        //            .Matches(externalIp)[0].ToString();
        //        return externalIp;
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        private void BtnSmall1_Click(object sender, EventArgs e)
        {
            Program.UserConfig.MpSettings.IsOnline = rb_Wan.Checked;
            Program.UserConfig.MpSettings.LanNetworkInterface = _selectedInterfaceName;
            Program.UserConfig.MpSettings.AutoPortMapping = rb_Automatic.Checked;
            //Program.UserConfig.MpSettings.PublicPort = Convert.ToInt32(numericUpDown2.Value);
            Program.UserConfig.MpSettings.PublicIp = tb_remoteIp.Text;
            Program.UserConfig.Save(Program.UserConfigFilePath);
            Close();
        }

        private void Rb_Wan_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_Wan.Checked)
            {
                if (rb_Lan.Checked)
                    rb_Lan.Checked = false;

                tb_remoteIp.Text = Program.RemoteUser?.Ip ?? @"127.0.0.1";

                panel3.Enabled = true;
            }
            else
            {
                if (!rb_Lan.Checked)
                    rb_Lan.Checked = true;

                panel3.Enabled = false;
            }
        }

        private void Rb_Lan_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_Lan.Checked)
            {
                if (!_isFirstRun)
                {
                    using (var netDeviceSelectDialog = new NetworkDeviceSelectionDialog())
                    {
                        netDeviceSelectDialog.ShowDialog(this);

                        if (netDeviceSelectDialog.DialogResult != DialogResult.OK)
                        {
                            rb_Lan.Checked = false;
                            rb_Wan.Checked = true;
                            return;
                        }

                        if (rb_Wan.Checked)
                            rb_Wan.Checked = false;
                        _selectedInterfaceName = netDeviceSelectDialog.SelectedInterfaceName;
                        tb_remoteIp.Text = netDeviceSelectDialog.SelectedIpAddress?.ToString() ?? @"127.0.0.1";
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(Program.UserConfig.MpSettings.LanNetworkInterface))
                    {
                        var selectedNetInt = Program.UserConfig.MpSettings.LanNetworkInterface;
                        var netInterface = NetworkInterface.GetAllNetworkInterfaces()
                            .FirstOrDefault(elem => elem.Name == selectedNetInt);

                        // Get IPv4 address:
                        if (netInterface != null)
                            foreach (var ip in netInterface.GetIPProperties().UnicastAddresses)
                                if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                                {
                                    tb_remoteIp.Text = ip.Address.ToString();
                                    return;
                                }
                    }
                    var firstOrDefault = Dns.GetHostEntry(Dns.GetHostName()).AddressList
                        .FirstOrDefault(key => key.AddressFamily == AddressFamily.InterNetwork);

                    tb_remoteIp.Text = firstOrDefault?.ToString() ?? @"127.0.0.1";
                }
            }
            else
            {
                if (!rb_Wan.Checked)
                    rb_Wan.Checked = true;
            }
        }

        private void Rb_Automatic_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_Automatic.Checked)
            {
                if (rb_Manual.Checked)
                    rb_Manual.Checked = false;
            }
            else
            {
                if (!rb_Manual.Checked)
                    rb_Manual.Checked = true;
            }
        }

        private void Rb_Manual_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_Manual.Checked)
            {
                if (rb_Automatic.Checked)
                    rb_Automatic.Checked = false;
            }
            else
            {
                if (!rb_Automatic.Checked)
                    rb_Automatic.Checked = true;
            }
        }
    }
}