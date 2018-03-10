#region Using directives

using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using WindowsFirewallHelper;
using Celeste_AOEO_Controls.Helpers;
using Celeste_AOEO_Controls.MsgBox;
using Celeste_Launcher_Gui.Helpers;
using Celeste_Public_Api.GameScanner_Api;

#endregion

namespace Celeste_Launcher_Gui.Forms
{
    public partial class FirewallForm : Form
    {
        public FirewallForm()
        {
            InitializeComponent();

            SkinHelper.SetFont(Controls);

            label3.Text = $@"API: {FirewallManager.Version}";
        }

        private void PictureBoxButtonCustom1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FirewallForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (DwmApi.DwmIsCompositionEnabled())
                    DwmApi.DwmExtendFrameIntoClientArea(Handle, new DwmApi.MARGINS(10, 10, 10, 10));
            }
            catch (Exception)
            {
                //
            }

            RefreshForm();
        }

        private void RefreshForm()
        {
            try
            {
                if (!FirewallHelper.RuleExist("celeste_launcher_inbound_tcp"))
                {
                    l_State_L_In.Text = @"Invalid";
                    l_State_L_In.ForeColor = Color.Red;
                }
                else
                {
                    l_State_L_In.Text = @"Valid";
                    l_State_L_In.ForeColor = Color.Green;
                }

                if (!FirewallHelper.RuleExist("celeste_launcher_outbound_tcp"))
                {
                    l_State_L_Out.Text = @"Invalid";
                    l_State_L_Out.ForeColor = Color.Red;
                }
                else
                {
                    l_State_L_Out.Text = @"Valid";
                    l_State_L_Out.ForeColor = Color.Green;
                }

                if (!FirewallHelper.RuleExist("celeste_spartan_inbound_tcp") ||
                    !FirewallHelper.RuleExist("celeste_spartan_inbound_udp"))
                {
                    l_State_S_In.Text = @"Invalid";
                    l_State_S_In.ForeColor = Color.Red;
                }
                else
                {
                    l_State_S_In.Text = @"Valid";
                    l_State_S_In.ForeColor = Color.Green;
                }

                if (!FirewallHelper.RuleExist("celeste_spartan_outbound_udp") ||
                    !FirewallHelper.RuleExist("celeste_spartan_outbound_udp"))
                {
                    l_State_S_Out.Text = @"Invalid";
                    l_State_S_Out.ForeColor = Color.Red;
                }
                else
                {
                    l_State_S_Out.Text = @"Valid";
                    l_State_S_Out.ForeColor = Color.Green;
                }

                if (!FirewallHelper.RuleExist("celeste_port1000_inbound_udp"))
                {
                    l_State_MP_In.Text = @"Invalid";
                    l_State_MP_In.ForeColor = Color.Red;
                }
                else
                {
                    l_State_MP_In.Text = @"Valid";
                    l_State_MP_In.ForeColor = Color.Green;
                }

                if (!FirewallHelper.RuleExist("celeste_port1000_outbound_udp"))
                {
                    l_State_MP_Out.Text = @"Invalid";
                    l_State_MP_Out.ForeColor = Color.Red;
                }
                else
                {
                    l_State_MP_Out.Text = @"Valid";
                    l_State_MP_Out.ForeColor = Color.Green;
                }
            }
            catch (Exception ex)
            {
                MsgBox.ShowMessage(
                    $"Error: {ex.Message}",
                    @"Celeste Fan Project",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btn_Fix_LauncherRules(object sender, EventArgs e)
        {
            Enabled = false;
            try
            {
                var launcherPath = Assembly.GetEntryAssembly().Location;

                if (!File.Exists(launcherPath))
                    throw new FileNotFoundException("Launcher not found!", launcherPath);

                //inbound_tcp
                var rule = FirewallHelper.RuleExist("celeste_launcher_inbound_tcp");
                if (rule)
                    FirewallHelper.RemoveRules("celeste_launcher_inbound_tcp");

                FirewallHelper.AddApplicationRule("celeste_launcher_inbound_tcp", launcherPath,
                    FirewallDirection.Inbound, FirewallProtocol.TCP);

                //outbound_tcp
                rule = FirewallHelper.RuleExist("celeste_launcher_outbound_tcp");
                if (rule)
                    FirewallHelper.RemoveRules("celeste_launcher_outbound_tcp");

                FirewallHelper.AddApplicationRule("celeste_launcher_outbound_tcp", launcherPath,
                    FirewallDirection.Outbound, FirewallProtocol.TCP);
            }
            catch (Exception ex)
            {
                MsgBox.ShowMessage(
                    $"Error: {ex.Message}",
                    @"Celeste Fan Project",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            RefreshForm();

            Enabled = true;
        }

        private void Btn_Fix_SpartanRules_Click(object sender, EventArgs e)
        {
            Enabled = false;
            try
            {
                var path = !string.IsNullOrEmpty(Program.UserConfig?.GameFilesPath)
                    ? Program.UserConfig?.GameFilesPath
                    : GameScannnerApi.GetGameFilesRootPath();

                var spartanPath = Path.Combine(path, "Spartan.exe");

                if (!File.Exists(spartanPath))
                    throw new FileNotFoundException("Spartan.exe not found!", spartanPath);

                //inbound_tcp
                var rule = FirewallHelper.RuleExist("celeste_spartan_inbound_tcp");
                if (rule)
                    FirewallHelper.RemoveRules("celeste_spartan_inbound_tcp");

                FirewallHelper.AddApplicationRule("celeste_spartan_inbound_tcp", spartanPath,
                    FirewallDirection.Inbound, FirewallProtocol.TCP);

                //outbound_tcp
                rule = FirewallHelper.RuleExist("celeste_spartan_outbound_tcp");
                if (rule)
                    FirewallHelper.RemoveRules("celeste_spartan_outbound_tcp");

                FirewallHelper.AddApplicationRule("celeste_spartan_outbound_tcp", spartanPath,
                    FirewallDirection.Outbound, FirewallProtocol.TCP);

                //inbound_udp
                rule = FirewallHelper.RuleExist("celeste_spartan_inbound_udp");
                if (rule)
                    FirewallHelper.RemoveRules("celeste_spartan_inbound_udp");

                FirewallHelper.AddApplicationRule("celeste_spartan_inbound_udp", spartanPath,
                    FirewallDirection.Inbound, FirewallProtocol.UDP);

                //outbound_udp
                rule = FirewallHelper.RuleExist("celeste_spartan_outbound_udp");
                if (rule)
                    FirewallHelper.RemoveRules("celeste_spartan_outbound_udp");

                FirewallHelper.AddApplicationRule("celeste_spartan_outbound_udp", spartanPath,
                    FirewallDirection.Outbound, FirewallProtocol.UDP);
            }
            catch (Exception ex)
            {
                MsgBox.ShowMessage(
                    $"Error: {ex.Message}",
                    @"Celeste Fan Project",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            RefreshForm();

            Enabled = true;
        }

        private void Btn_Fix_MPRules_Click(object sender, EventArgs e)
        {
            Enabled = false;
            try
            {
                //inbound_udp
                var rule = FirewallHelper.RuleExist("celeste_port1000_inbound_udp");
                if (rule)
                    FirewallHelper.RemoveRules("celeste_port1000_inbound_udp");

                FirewallHelper.AddPortRule("celeste_port1000_inbound_udp", 1000,
                    FirewallDirection.Inbound, FirewallProtocol.UDP);

                //outbound_udp
                rule = FirewallHelper.RuleExist("celeste_port1000_outbound_udp");
                if (rule)
                    FirewallHelper.RemoveRules("celeste_port1000_outbound_udp");

                FirewallHelper.AddPortRule("celeste_port1000_outbound_udp", 1000,
                    FirewallDirection.Outbound, FirewallProtocol.UDP);
            }
            catch (Exception ex)
            {
                MsgBox.ShowMessage(
                    $"Error: {ex.Message}",
                    @"Celeste Fan Project",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            RefreshForm();

            Enabled = true;
        }
    }
}