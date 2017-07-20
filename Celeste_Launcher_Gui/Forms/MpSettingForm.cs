#region Using directives

using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using Celeste_Launcher_Gui.Helpers;

#endregion

namespace Celeste_Launcher_Gui.Forms
{
    public partial class MpSettingForm : Form
    {
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
                    DwmApi.DwmExtendFrameIntoClientArea(Handle, new DwmApi.MARGINS(31, 75, 31, 31));
            }
            catch (Exception)
            {
                //
            }
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

        private void btnSmall1_Click(object sender, EventArgs e)
        {
            Program.UserConfig.MpSettings.IsOnline = rb_Wan.Checked;
            Program.UserConfig.MpSettings.AutoPortMapping = rb_Automatic.Checked;
            //Program.UserConfig.MpSettings.PublicPort = Convert.ToInt32(numericUpDown2.Value);
            Program.UserConfig.MpSettings.PublicIp = tb_remoteIp.Text;
            Program.UserConfig.Save(Program.UserConfigFilePath);
            Close();
        }

        private void rb_Wan_CheckedChanged(object sender, EventArgs e)
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

        private void rb_Lan_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_Lan.Checked)
            {
                
                var netDeviceSelectDialog = new NetworkDeviceSelectionDialog();
                netDeviceSelectDialog.ShowDialog(this);

                if(netDeviceSelectDialog.DialogResult == DialogResult.Cancel)
                {
                    rb_Lan.Checked = false;
                    rb_Wan.Checked = true;
                    return;
                }

                if (rb_Wan.Checked)
                    rb_Wan.Checked = false;
                
                tb_remoteIp.Text = netDeviceSelectDialog.SelectedIPAddress?.ToString() ?? @"127.0.0.1";
            }
            else
            {
                if (!rb_Wan.Checked)
                    rb_Wan.Checked = true;
            }
        }

        private void rb_Automatic_CheckedChanged(object sender, EventArgs e)
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

        private void rb_Manual_CheckedChanged(object sender, EventArgs e)
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