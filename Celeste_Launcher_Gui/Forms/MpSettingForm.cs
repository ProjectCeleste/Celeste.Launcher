#region Using directives

using System;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Windows.Forms;
using Celeste_AOEO_Controls.Helpers;

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

            SkinHelperFonts.SetFont(Controls);

            //MpSettings
            if (mpSettings.IsOnline)
                rb_Wan.Checked = true;
            else
                rb_Lan.Checked = true;

            if (mpSettings.AutoPortMapping)
                rb_Automatic.Checked = true;
            else
                rb_Manual.Checked = true;

            //numericUpDown2.Value = mpSettings.PublicPort;
        }


        private void MpSettingBox_Load(object sender, EventArgs e)
        {
            try
            {
                if (DwmApi.DwmIsCompositionEnabled())
                    DwmApi.DwmExtendFrameIntoClientArea(Handle, new DwmApi.MARGINS(18, 10, 18, 10));
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
            try
            {
                Program.UserConfig.Save(Program.UserConfigFilePath);
            }
            catch (Exception)
            {
                //
            }
            Close();
        }

        private void Rb_Wan_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_Wan.Checked)
            {
                if (Program.CurrentUser != null &&
                    !string.IsNullOrWhiteSpace(Program.CurrentUser.Ip))
                {
                    tb_remoteIp.Text = Program.CurrentUser.Ip;
                }
                else
                {
                    //FallBack to Other
                    tb_remoteIp.Text = @"127.0.0.1";
                    rb_Other.Checked = true;
                    return;
                }

                _selectedInterfaceName = string.Empty;

                panel3.Enabled = true;
            }
            else
            {
                panel3.Enabled = false;
            }
        }

        private void Rb_Lan_CheckedChanged(object sender, EventArgs e)
        {
            if (!rb_Lan.Checked)
                return;

            if (!_isFirstRun)
            {
                using (var netDeviceSelectDialog = new NetworkDeviceSelectionDialog())
                {
                    netDeviceSelectDialog.ShowDialog();

                    if (netDeviceSelectDialog.DialogResult != DialogResult.OK)
                    {
                        //FallBack to Wan
                        rb_Wan.Checked = true;
                        return;
                    }

                    _selectedInterfaceName = netDeviceSelectDialog.SelectedInterfaceName;
                    if (string.IsNullOrWhiteSpace(netDeviceSelectDialog.SelectedIpAddress?.ToString()))
                        rb_Wan.Checked = true;
                    else
                        tb_remoteIp.Text = netDeviceSelectDialog.SelectedIpAddress?.ToString();
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(Program.UserConfig?.MpSettings?.LanNetworkInterface))
                {
                    _selectedInterfaceName = Program.UserConfig.MpSettings.LanNetworkInterface;
                    var netInterface = NetworkInterface.GetAllNetworkInterfaces()
                        .FirstOrDefault(elem => elem.Name == _selectedInterfaceName);

                    // Get IPv4 address:
                    if (netInterface != null)
                        foreach (var ip in netInterface.GetIPProperties().UnicastAddresses)
                            if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                            {
                                tb_remoteIp.Text = ip.Address.ToString();
                                return;
                            }
                }

                //FallBack to Wan
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

        private void Rb_Other_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_Other.Checked)
            {
                _selectedInterfaceName = "Other";
                tb_remoteIp.ReadOnly = false;
            }
            else
            {
                _selectedInterfaceName = string.Empty;
                tb_remoteIp.ReadOnly = true;
            }
        }

        private void PictureBoxButtonCustom1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}