#region Using directives

using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using WindowsFirewallHelper;
using WindowsFirewallHelper.FirewallAPIv2.Rules;
using Celeste_AOEO_Controls.Helpers;
using Celeste_AOEO_Controls.MsgBox;
using Celeste_Launcher_Gui.Helpers;
using ProjectCeleste.GameFiles.GameScanner;

#endregion

namespace Celeste_Launcher_Gui.Forms
{
    public partial class FirewallForm : Form
    {
        public FirewallForm()
        {
            InitializeComponent();

            SkinHelperFonts.SetFont(Controls);

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
                //Launcher
                var launcherPath = Assembly.GetEntryAssembly().Location;

                if (!File.Exists(launcherPath))
                    throw new FileNotFoundException("Launcher not found!", launcherPath);

                var rule = (StandardRuleWin7) FirewallHelper.FindRule("celeste_launcher_inbound_tcp");
                if (rule == null)
                {
                    l_State_L_In.Text = @"Not Found";
                    l_State_L_In.ForeColor = Color.Red;
                }
                else
                {
                    if (rule.Protocol != FirewallProtocol.TCP || rule.ApplicationName != launcherPath ||
                        rule.LocalPortType != FirewallPortType.All || rule.Direction != FirewallDirection.Inbound)
                    {
                        l_State_L_In.Text = @"Invalid";
                        l_State_L_In.ForeColor = Color.Red;
                    }
                    else
                    {
                        l_State_L_In.Text = @"Valid";
                        l_State_L_In.ForeColor = Color.Green;
                    }
                }

                rule = (StandardRuleWin7) FirewallHelper.FindRule("celeste_launcher_outbound_tcp");
                if (rule == null)
                {
                    l_State_L_Out.Text = @"Not Found";
                    l_State_L_Out.ForeColor = Color.Red;
                }
                else
                {
                    if (rule.Protocol != FirewallProtocol.TCP || rule.ApplicationName != launcherPath ||
                        rule.LocalPortType != FirewallPortType.All || rule.Direction != FirewallDirection.Outbound)
                    {
                        l_State_L_Out.Text = @"Invalid";
                        l_State_L_Out.ForeColor = Color.Red;
                    }
                    else
                    {
                        l_State_L_Out.Text = @"Valid";
                        l_State_L_Out.ForeColor = Color.Green;
                    }
                }

                var path = !string.IsNullOrWhiteSpace(Program.UserConfig?.GameFilesPath)
                    ? Program.UserConfig?.GameFilesPath
                    : GameScannnerManager.GetGameFilesRootPath();

                var spartanPath = Path.Combine(path, "Spartan.exe");

                if (!File.Exists(spartanPath))
                    throw new FileNotFoundException("Spartan.exe not found!", spartanPath);

                //Spartan
                var rule1 = (StandardRuleWin7) FirewallHelper.FindRule("celeste_spartan_inbound_tcp");
                var rule2 = (StandardRuleWin7) FirewallHelper.FindRule("celeste_spartan_inbound_udp");
                if (rule1 == null || rule2 == null)
                {
                    l_State_S_In.Text = @"Not Found";
                    l_State_S_In.ForeColor = Color.Red;
                }
                else
                {
                    if (rule1.Protocol != FirewallProtocol.TCP || rule1.ApplicationName != spartanPath ||
                        rule1.LocalPortType != FirewallPortType.All || rule1.Direction != FirewallDirection.Inbound ||
                        rule2.Protocol != FirewallProtocol.UDP || rule2.ApplicationName != spartanPath ||
                        rule2.LocalPortType != FirewallPortType.All || rule2.Direction != FirewallDirection.Inbound)
                    {
                        l_State_S_In.Text = @"Invalid";
                        l_State_S_In.ForeColor = Color.Red;
                    }
                    else
                    {
                        l_State_S_In.Text = @"Valid";
                        l_State_S_In.ForeColor = Color.Green;
                    }
                }

                rule1 = (StandardRuleWin7) FirewallHelper.FindRule("celeste_spartan_outbound_tcp");
                rule2 = (StandardRuleWin7) FirewallHelper.FindRule("celeste_spartan_outbound_udp");
                if (rule1 == null || rule2 == null)
                {
                    l_State_S_Out.Text = @"Not Found";
                    l_State_S_Out.ForeColor = Color.Red;
                }
                else
                {
                    if (rule1.Protocol != FirewallProtocol.TCP || rule1.ApplicationName != spartanPath ||
                        rule1.LocalPortType != FirewallPortType.All || rule1.Direction != FirewallDirection.Outbound ||
                        rule2.Protocol != FirewallProtocol.UDP || rule2.ApplicationName != spartanPath ||
                        rule2.LocalPortType != FirewallPortType.All || rule2.Direction != FirewallDirection.Outbound)
                    {
                        l_State_S_Out.Text = @"Invalid";
                        l_State_S_Out.ForeColor = Color.Red;
                    }
                    else
                    {
                        l_State_S_Out.Text = @"Valid";
                        l_State_S_Out.ForeColor = Color.Green;
                    }
                }

                //Port 1000
                rule = (StandardRuleWin7) FirewallHelper.FindRule("celeste_port1000_inbound_udp");
                if (rule == null)
                {
                    l_State_MP_In.Text = @"Not Found";
                    l_State_MP_In.ForeColor = Color.Red;
                }
                else
                {
                    if (rule.Protocol != FirewallProtocol.UDP || rule.LocalPorts.All(key => key != 1000) ||
                        rule.LocalPortType != FirewallPortType.Specific || rule.Direction != FirewallDirection.Inbound)
                    {
                        l_State_MP_In.Text = @"Invalid";
                        l_State_MP_In.ForeColor = Color.Red;
                    }
                    else
                    {
                        l_State_MP_In.Text = @"Valid";
                        l_State_MP_In.ForeColor = Color.Green;
                    }
                }

                rule = (StandardRuleWin7) FirewallHelper.FindRule("celeste_port1000_outbound_udp");
                if (rule == null)
                {
                    l_State_MP_Out.Text = @"Not Found";
                    l_State_MP_Out.ForeColor = Color.Red;
                }
                else
                {
                    if (rule.Protocol != FirewallProtocol.UDP || rule.LocalPorts.All(key => key != 1000) ||
                        rule.LocalPortType != FirewallPortType.Specific || rule.Direction != FirewallDirection.Outbound)
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
                var path = !string.IsNullOrWhiteSpace(Program.UserConfig?.GameFilesPath)
                    ? Program.UserConfig?.GameFilesPath
                    : GameScannnerManager.GetGameFilesRootPath();

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