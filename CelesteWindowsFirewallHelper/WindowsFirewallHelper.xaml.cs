using Celeste_Launcher_Gui;
using Celeste_Launcher_Gui.Helpers;
using Celeste_Launcher_Gui.Windows;
using Celeste_Public_Api.Logging;
using ProjectCeleste.GameFiles.GameScanner;
using Serilog;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using WindowsFirewallHelper;

namespace CelesteWindowsFirewallHelper
{
    /// <summary>
    /// Interaction logic for WindowsFirewallHelper.xaml
    /// </summary>
    public partial class WindowsFirewallHelper : Window
    {
        private static readonly ILogger Logger = LoggerFactory.GetLogger();

        public WindowsFirewallHelper()
        {
            InitializeComponent();
        }

        private void BorderMoved(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void LauncherOpenBtn_Click(object sender, RoutedEventArgs e)
        {
            IsEnabled = false;
            try
            {
                var launcherPath = Assembly.GetEntryAssembly().Location;

                if (!File.Exists(launcherPath))
                {
                    GenericMessageDialog.Show(Celeste_Launcher_Gui.Properties.Resources.WindowsFirewallHelperLauncherNotFound, DialogIcon.Error, DialogOptions.Ok);
                    Close();
                    return;
                }

                //outbound_tcp
                var rule = FirewallHelper.RuleExist("celeste_launcher_outbound_tcp");
                if (rule)
                    FirewallHelper.RemoveRules("celeste_launcher_outbound_tcp");

                FirewallHelper.AddApplicationRule("celeste_launcher_outbound_tcp", launcherPath,
                    FirewallDirection.Outbound, FirewallProtocol.TCP);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, ex.Message);
                GenericMessageDialog.Show(Celeste_Launcher_Gui.Properties.Resources.GenericUnexpectedErrorMessage, DialogIcon.Error, DialogOptions.OkWithLog, Celeste_Public_Api.Logging.LogHelper.FindMostRecentLogFile(System.IO.Path.Combine("Logs", "firewall-helper.log")));
            }

            LoadFirewallRules();

            IsEnabled = true;
        }

        private void SpartanOpenBtn_Click(object sender, RoutedEventArgs e)
        {
            IsEnabled = false;
            try
            {
                var path = !string.IsNullOrWhiteSpace(LegacyBootstrapper.UserConfig?.GameFilesPath)
                    ? LegacyBootstrapper.UserConfig?.GameFilesPath
                    : GameScannerManager.GetGameFilesRootPath();

                var spartanPath = Path.Combine(path, "Spartan.exe");

                if (!File.Exists(spartanPath))
                {
                    GenericMessageDialog.Show(Celeste_Launcher_Gui.Properties.Resources.WindowsFirewallHelperSpartanNotFound, DialogIcon.Error, DialogOptions.Ok);
                    Close();
                    return;
                }

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
                Logger.Error(ex, ex.Message);
                GenericMessageDialog.Show(Celeste_Launcher_Gui.Properties.Resources.GenericUnexpectedErrorMessage, DialogIcon.Error, DialogOptions.OkWithLog, Celeste_Public_Api.Logging.LogHelper.FindMostRecentLogFile(System.IO.Path.Combine("Logs", "firewall-helper.log")));
            }

            LoadFirewallRules();

            IsEnabled = true;
        }

        private void MultiplayerOpenBtn_Click(object sender, RoutedEventArgs e)
        {
            IsEnabled = false;
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
                Logger.Error(ex, ex.Message);
                GenericMessageDialog.Show(Celeste_Launcher_Gui.Properties.Resources.GenericUnexpectedErrorMessage, DialogIcon.Error, DialogOptions.OkWithLog, Celeste_Public_Api.Logging.LogHelper.FindMostRecentLogFile(System.IO.Path.Combine("Logs", "firewall-helper.log")));
            }

            LoadFirewallRules();

            IsEnabled = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadFirewallRules();
        }

        private void LoadFirewallRules()
        {
            try
            {
                //Launcher
                var launcherPath = Assembly.GetEntryAssembly().Location;

                if (!File.Exists(launcherPath))
                {
                    GenericMessageDialog.Show(Celeste_Launcher_Gui.Properties.Resources.WindowsFirewallHelperLauncherNotFound, DialogIcon.Error, DialogOptions.Ok);
                    Close();
                    return;
                }

                var rule = FirewallHelper.FindRule("celeste_launcher_outbound_tcp");
                if (rule == null)
                {
                    LauncherOutboundStatus.Content = Celeste_Launcher_Gui.Properties.Resources.WindowsFirewallHelperRuleNotFound;
                    LauncherOutboundStatus.Foreground = new SolidColorBrush(Colors.Red);
                }
                else
                {
                    if (rule.Protocol != FirewallProtocol.TCP || rule.ApplicationName != launcherPath ||
                        rule.LocalPortType != FirewallPortType.All || rule.Direction != FirewallDirection.Outbound)
                    {
                        LauncherOutboundStatus.Content = Celeste_Launcher_Gui.Properties.Resources.WindowsFirewallHelperRuleInvalid;
                        LauncherOutboundStatus.Foreground = new SolidColorBrush(Colors.Red);
                    }
                    else
                    {
                        LauncherOutboundStatus.Content = Celeste_Launcher_Gui.Properties.Resources.WindowsFirewallHelperRuleOpen;
                        LauncherOutboundStatus.Foreground = new SolidColorBrush(Colors.Green);
                    }
                }

                var path = !string.IsNullOrWhiteSpace(LegacyBootstrapper.UserConfig?.GameFilesPath)
                    ? LegacyBootstrapper.UserConfig?.GameFilesPath
                    : GameScannerManager.GetGameFilesRootPath();

                var spartanPath = Path.Combine(path, "Spartan.exe");

                if (!File.Exists(spartanPath))
                {
                    GenericMessageDialog.Show(Celeste_Launcher_Gui.Properties.Resources.WindowsFirewallHelperSpartanNotFound, DialogIcon.Error, DialogOptions.Ok);
                    Close();
                    return;
                }

                //Spartan
                var rule1 = FirewallHelper.FindRule("celeste_spartan_inbound_tcp");
                var rule2 = FirewallHelper.FindRule("celeste_spartan_inbound_udp");
                if (rule1 == null || rule2 == null)
                {

                    SpartanInboundStatus.Content = Celeste_Launcher_Gui.Properties.Resources.WindowsFirewallHelperRuleNotFound;
                    SpartanInboundStatus.Foreground = new SolidColorBrush(Colors.Red);
                }
                else
                {
                    if (rule1.Protocol != FirewallProtocol.TCP || rule1.ApplicationName != spartanPath ||
                        rule1.LocalPortType != FirewallPortType.All || rule1.Direction != FirewallDirection.Inbound ||
                        rule2.Protocol != FirewallProtocol.UDP || rule2.ApplicationName != spartanPath ||
                        rule2.LocalPortType != FirewallPortType.All || rule2.Direction != FirewallDirection.Inbound)
                    {
                        SpartanInboundStatus.Content = Celeste_Launcher_Gui.Properties.Resources.WindowsFirewallHelperRuleInvalid;
                        SpartanInboundStatus.Foreground = new SolidColorBrush(Colors.Red);
                    }
                    else
                    {
                        SpartanInboundStatus.Content = Celeste_Launcher_Gui.Properties.Resources.WindowsFirewallHelperRuleOpen;
                        SpartanInboundStatus.Foreground = new SolidColorBrush(Colors.Green);
                    }
                }

                rule1 = FirewallHelper.FindRule("celeste_spartan_outbound_tcp");
                rule2 = FirewallHelper.FindRule("celeste_spartan_outbound_udp");
                if (rule1 == null || rule2 == null)
                {
                    SpartanOutboundStatus.Content = Celeste_Launcher_Gui.Properties.Resources.WindowsFirewallHelperRuleNotFound;
                    SpartanOutboundStatus.Foreground = new SolidColorBrush(Colors.Red);
                }
                else
                {
                    if (rule1.Protocol != FirewallProtocol.TCP || rule1.ApplicationName != spartanPath ||
                        rule1.LocalPortType != FirewallPortType.All || rule1.Direction != FirewallDirection.Outbound ||
                        rule2.Protocol != FirewallProtocol.UDP || rule2.ApplicationName != spartanPath ||
                        rule2.LocalPortType != FirewallPortType.All || rule2.Direction != FirewallDirection.Outbound)
                    {
                        SpartanOutboundStatus.Content = Celeste_Launcher_Gui.Properties.Resources.WindowsFirewallHelperRuleInvalid;
                        SpartanOutboundStatus.Foreground = new SolidColorBrush(Colors.Red);
                    }
                    else
                    {
                        SpartanOutboundStatus.Content = Celeste_Launcher_Gui.Properties.Resources.WindowsFirewallHelperRuleOpen;
                        SpartanOutboundStatus.Foreground = new SolidColorBrush(Colors.Green);
                    }
                }

                //Port 1000
                rule = FirewallHelper.FindRule("celeste_port1000_inbound_udp");
                if (rule == null)
                {
                    MultiplayerInboundStatus.Content = Celeste_Launcher_Gui.Properties.Resources.WindowsFirewallHelperRuleNotFound;
                    MultiplayerInboundStatus.Foreground = new SolidColorBrush(Colors.Red);
                }
                else
                {
                    if (rule.Protocol != FirewallProtocol.UDP || rule.LocalPorts.All(key => key != 1000) ||
                        rule.LocalPortType != FirewallPortType.Specific || rule.Direction != FirewallDirection.Inbound)
                    {
                        MultiplayerInboundStatus.Content = Celeste_Launcher_Gui.Properties.Resources.WindowsFirewallHelperRuleInvalid;
                        MultiplayerInboundStatus.Foreground = new SolidColorBrush(Colors.Red);
                    }
                    else
                    {
                        MultiplayerInboundStatus.Content = Celeste_Launcher_Gui.Properties.Resources.WindowsFirewallHelperRuleOpen;
                        MultiplayerInboundStatus.Foreground = new SolidColorBrush(Colors.Green);
                    }
                }

                rule = FirewallHelper.FindRule("celeste_port1000_outbound_udp");
                if (rule == null)
                {
                    MultiplayerOutboundStatus.Content = Celeste_Launcher_Gui.Properties.Resources.WindowsFirewallHelperRuleNotFound;
                    MultiplayerOutboundStatus.Foreground = new SolidColorBrush(Colors.Red);
                }
                else
                {
                    if (rule.Protocol != FirewallProtocol.UDP || rule.LocalPorts.All(key => key != 1000) ||
                        rule.LocalPortType != FirewallPortType.Specific || rule.Direction != FirewallDirection.Outbound)
                    {
                        MultiplayerOutboundStatus.Content = Celeste_Launcher_Gui.Properties.Resources.WindowsFirewallHelperRuleInvalid;
                        MultiplayerOutboundStatus.Foreground = new SolidColorBrush(Colors.Red);
                    }
                    else
                    {
                        MultiplayerOutboundStatus.Content = Celeste_Launcher_Gui.Properties.Resources.WindowsFirewallHelperRuleOpen;
                        MultiplayerOutboundStatus.Foreground = new SolidColorBrush(Colors.Green);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, ex.Message);
                GenericMessageDialog.Show(Celeste_Launcher_Gui.Properties.Resources.GenericUnexpectedErrorMessage, DialogIcon.Error, DialogOptions.OkWithLog, Celeste_Public_Api.Logging.LogHelper.FindMostRecentLogFile(System.IO.Path.Combine("Logs", "firewall-helper.log")));
            }
        }
    }
}
