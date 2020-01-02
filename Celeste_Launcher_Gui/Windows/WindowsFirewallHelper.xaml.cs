﻿using Celeste_Launcher_Gui.Helpers;
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
using WindowsFirewallHelper.FirewallAPIv2.Rules;

namespace Celeste_Launcher_Gui.Windows
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
                    throw new FileNotFoundException("Launcher not found!", launcherPath);

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
                GenericMessageDialog.Show($@"Error: {ex.Message}", DialogIcon.Error, DialogOptions.Ok);
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
                Logger.Error(ex, ex.Message);
                GenericMessageDialog.Show($@"Error: {ex.Message}", DialogIcon.Error, DialogOptions.Ok);
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
                GenericMessageDialog.Show($@"Error: {ex.Message}", DialogIcon.Error, DialogOptions.Ok);
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
                    throw new FileNotFoundException("Launcher not found!", launcherPath);

                var rule = (StandardRuleWin7)FirewallHelper.FindRule("celeste_launcher_outbound_tcp");
                if (rule == null)
                {
                    LauncherOutboundStatus.Content = "Not Found";
                    LauncherOutboundStatus.Foreground = new SolidColorBrush(Colors.Red);
                }
                else
                {
                    if (rule.Protocol != FirewallProtocol.TCP || rule.ApplicationName != launcherPath ||
                        rule.LocalPortType != FirewallPortType.All || rule.Direction != FirewallDirection.Outbound)
                    {
                        LauncherOutboundStatus.Content = @"Invalid";
                        LauncherOutboundStatus.Foreground = new SolidColorBrush(Colors.Red);
                    }
                    else
                    {
                        LauncherOutboundStatus.Content = @"Open";
                        LauncherOutboundStatus.Foreground = new SolidColorBrush(Colors.Green);
                    }
                }

                var path = !string.IsNullOrWhiteSpace(LegacyBootstrapper.UserConfig?.GameFilesPath)
                    ? LegacyBootstrapper.UserConfig?.GameFilesPath
                    : GameScannnerManager.GetGameFilesRootPath();

                var spartanPath = Path.Combine(path, "Spartan.exe");

                if (!File.Exists(spartanPath))
                    throw new FileNotFoundException("Spartan.exe not found!", spartanPath);

                //Spartan
                var rule1 = (StandardRuleWin7)FirewallHelper.FindRule("celeste_spartan_inbound_tcp");
                var rule2 = (StandardRuleWin7)FirewallHelper.FindRule("celeste_spartan_inbound_udp");
                if (rule1 == null || rule2 == null)
                {

                    SpartanInboundStatus.Content = @"Not Found";
                    SpartanInboundStatus.Foreground = new SolidColorBrush(Colors.Red);
                }
                else
                {
                    if (rule1.Protocol != FirewallProtocol.TCP || rule1.ApplicationName != spartanPath ||
                        rule1.LocalPortType != FirewallPortType.All || rule1.Direction != FirewallDirection.Inbound ||
                        rule2.Protocol != FirewallProtocol.UDP || rule2.ApplicationName != spartanPath ||
                        rule2.LocalPortType != FirewallPortType.All || rule2.Direction != FirewallDirection.Inbound)
                    {
                        SpartanInboundStatus.Content = @"Invalid";
                        SpartanInboundStatus.Foreground = new SolidColorBrush(Colors.Red);
                    }
                    else
                    {
                        SpartanInboundStatus.Content = @"Open";
                        SpartanInboundStatus.Foreground = new SolidColorBrush(Colors.Green);
                    }
                }

                rule1 = (StandardRuleWin7)FirewallHelper.FindRule("celeste_spartan_outbound_tcp");
                rule2 = (StandardRuleWin7)FirewallHelper.FindRule("celeste_spartan_outbound_udp");
                if (rule1 == null || rule2 == null)
                {
                    SpartanOutboundStatus.Content = @"Not Found";
                    SpartanOutboundStatus.Foreground = new SolidColorBrush(Colors.Red);
                }
                else
                {
                    if (rule1.Protocol != FirewallProtocol.TCP || rule1.ApplicationName != spartanPath ||
                        rule1.LocalPortType != FirewallPortType.All || rule1.Direction != FirewallDirection.Outbound ||
                        rule2.Protocol != FirewallProtocol.UDP || rule2.ApplicationName != spartanPath ||
                        rule2.LocalPortType != FirewallPortType.All || rule2.Direction != FirewallDirection.Outbound)
                    {
                        SpartanOutboundStatus.Content = @"Invalid";
                        SpartanOutboundStatus.Foreground = new SolidColorBrush(Colors.Red);
                    }
                    else
                    {
                        SpartanOutboundStatus.Content = @"Open";
                        SpartanOutboundStatus.Foreground = new SolidColorBrush(Colors.Green);
                    }
                }

                //Port 1000
                rule = (StandardRuleWin7)FirewallHelper.FindRule("celeste_port1000_inbound_udp");
                if (rule == null)
                {
                    MultiplayerInboundStatus.Content = @"Not Found";
                    MultiplayerInboundStatus.Foreground = new SolidColorBrush(Colors.Red);
                }
                else
                {
                    if (rule.Protocol != FirewallProtocol.UDP || rule.LocalPorts.All(key => key != 1000) ||
                        rule.LocalPortType != FirewallPortType.Specific || rule.Direction != FirewallDirection.Inbound)
                    {
                        MultiplayerInboundStatus.Content = @"Invalid";
                        MultiplayerInboundStatus.Foreground = new SolidColorBrush(Colors.Red);
                    }
                    else
                    {
                        MultiplayerInboundStatus.Content = @"Open";
                        MultiplayerInboundStatus.Foreground = new SolidColorBrush(Colors.Green);
                    }
                }

                rule = (StandardRuleWin7)FirewallHelper.FindRule("celeste_port1000_outbound_udp");
                if (rule == null)
                {
                    MultiplayerOutboundStatus.Content = @"Not Found";
                    MultiplayerOutboundStatus.Foreground = new SolidColorBrush(Colors.Red);
                }
                else
                {
                    if (rule.Protocol != FirewallProtocol.UDP || rule.LocalPorts.All(key => key != 1000) ||
                        rule.LocalPortType != FirewallPortType.Specific || rule.Direction != FirewallDirection.Outbound)
                    {
                        MultiplayerOutboundStatus.Content = @"Invalid";
                        MultiplayerOutboundStatus.Foreground = new SolidColorBrush(Colors.Red);
                    }
                    else
                    {
                        MultiplayerOutboundStatus.Content = @"Open";
                        MultiplayerOutboundStatus.Foreground = new SolidColorBrush(Colors.Green);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, ex.Message);
                GenericMessageDialog.Show($@"Error: {ex.Message}", DialogIcon.Error, DialogOptions.Ok);
            }
        }
    }
}
