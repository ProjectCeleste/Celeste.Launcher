#region Using directives

using System;
using System.Collections.Generic;
using System.Linq;
using WindowsFirewallHelper;
using WindowsFirewallHelper.FirewallAPIv2;
using WindowsFirewallHelper.FirewallAPIv2.Rules;

#endregion Using directives

namespace Celeste_Launcher_Gui.Helpers
{
    public static class FirewallHelper
    {
        public static void AddApplicationRule(string ruleName, string fileName, FirewallDirection direction,
            FirewallProtocol protocol)
        {
            if (Firewall.Instance.IsSupported)
            {
                if (StandardRuleWin8.IsSupported)
                {
                    var rule = new StandardRuleWin8(ruleName, fileName, FirewallAction.Allow,
                        direction,
                        FirewallProfiles.Domain | FirewallProfiles.Private | FirewallProfiles.Public)
                    {
                        Grouping = "Celeste Fan Project",
                        Description = "Auto-generated rules by \"Celeste Fan Project Launcher\"",
                        InterfaceTypes = FirewallInterfaceTypes.Lan | FirewallInterfaceTypes.RemoteAccess |
                                         FirewallInterfaceTypes.Wireless,
                        Protocol = protocol
                    };

                    //if (direction == FirewallDirection.Inbound && (protocol.Equals(FirewallProtocol.TCP) || protocol.Equals(FirewallProtocol.UDP)))
                    //    rule.EdgeTraversalOptions = EdgeTraversalAction.DefferToUser;

                    Firewall.Instance.Rules.Add(rule);
                }
                else if (StandardRuleWin7.IsSupported)
                {
                    var rule = new StandardRuleWin7(ruleName, fileName, FirewallAction.Allow,
                        direction,
                        FirewallProfiles.Domain | FirewallProfiles.Private | FirewallProfiles.Public)
                    {
                        Grouping = "Celeste Fan Project",
                        Description = "Auto-generated rules by \"Celeste Fan Project Launcher\"",
                        InterfaceTypes = FirewallInterfaceTypes.Lan | FirewallInterfaceTypes.RemoteAccess |
                                         FirewallInterfaceTypes.Wireless,
                        Protocol = protocol
                    };

                    //if (direction == FirewallDirection.Inbound && (protocol.Equals(FirewallProtocol.TCP) || protocol.Equals(FirewallProtocol.UDP)))
                    //    rule.EdgeTraversalOptions = EdgeTraversalAction.DefferToUser;

                    Firewall.Instance.Rules.Add(rule);
                }
                else
                {
                    AddDefaultApplicationRule(ruleName, fileName, direction, protocol);
                }
            }
            else
            {
                AddDefaultApplicationRule(ruleName, fileName, direction, protocol);
            }
        }

        private static void AddDefaultApplicationRule(string ruleName, string fileName, FirewallDirection direction,
            FirewallProtocol protocol)
        {
            var defaultRule = FirewallManager.Instance.CreateApplicationRule(
                FirewallProfiles.Domain | FirewallProfiles.Private | FirewallProfiles.Public, ruleName,
                FirewallAction.Allow, fileName, protocol);

            defaultRule.Direction = direction;

            FirewallManager.Instance.Rules.Add(defaultRule);
        }

        public static void AddPortRule(string ruleName, ushort portNumber, FirewallDirection direction,
            FirewallProtocol protocol)
        {
            if (Firewall.Instance.IsSupported)
            {
                if (StandardRuleWin8.IsSupported)
                {
                    var rule = new StandardRuleWin8(ruleName, portNumber, FirewallAction.Allow,
                        direction,
                        FirewallProfiles.Domain | FirewallProfiles.Private | FirewallProfiles.Public)
                    {
                        Grouping = "Celeste Fan Project",
                        Description = "Auto-generated rules by \"Celeste Fan Project Launcher\"",
                        InterfaceTypes = FirewallInterfaceTypes.Lan | FirewallInterfaceTypes.RemoteAccess |
                                         FirewallInterfaceTypes.Wireless,
                        Protocol = protocol
                    };

                    Firewall.Instance.Rules.Add(rule);
                }
                else if (StandardRuleWin7.IsSupported)
                {
                    var rule = new StandardRuleWin7(ruleName, portNumber, FirewallAction.Allow,
                        direction,
                        FirewallProfiles.Domain | FirewallProfiles.Private | FirewallProfiles.Public)
                    {
                        Grouping = "Celeste Fan Project",
                        Description = "Auto-generated rules by \"Celeste Fan Project Launcher\"",
                        InterfaceTypes = FirewallInterfaceTypes.Lan | FirewallInterfaceTypes.RemoteAccess |
                                         FirewallInterfaceTypes.Wireless,
                        Protocol = protocol
                    };

                    Firewall.Instance.Rules.Add(rule);
                }
                else
                {
                    AddDefaultPortRule(ruleName, portNumber, direction, protocol);
                }
            }
            else
            {
                AddDefaultPortRule(ruleName, portNumber, direction, protocol);
            }
        }

        private static void AddDefaultPortRule(string ruleName, ushort portNumber, FirewallDirection direction,
            FirewallProtocol protocol)
        {
            var defaultRule = FirewallManager.Instance.CreatePortRule(
                FirewallProfiles.Domain | FirewallProfiles.Private | FirewallProfiles.Public, ruleName,
                FirewallAction.Allow, portNumber, protocol);

            defaultRule.Direction = direction;

            FirewallManager.Instance.Rules.Add(defaultRule);
        }

        public static void RemoveRules(string ruleName)
        {
            foreach (var rule in FindRules(ruleName).ToArray())
                try
                {
                    FirewallManager.Instance.Rules.Remove(rule);
                }
                catch (Exception)
                {
                    //
                }
        }

        public static bool RemoveRule(string ruleName)
        {
            return FirewallManager.Instance.Rules.Remove(FindRule(ruleName));
        }

        public static bool RuleExist(string ruleName)
        {
            return FindRules(ruleName).Any();
        }

        public static IEnumerable<IRule> FindRules(string ruleName)
        {
            return Firewall.Instance.IsSupported && (StandardRuleWin8.IsSupported || StandardRuleWin7.IsSupported)
                ? Firewall.Instance.Rules
                    .Where(r => string.Equals(r.Name, ruleName, StringComparison.OrdinalIgnoreCase)).ToArray()
                : FirewallManager.Instance.Rules
                    .Where(r => string.Equals(r.Name, ruleName, StringComparison.OrdinalIgnoreCase)).ToArray();
        }

        public static IRule FindRule(string ruleName)
        {
            return Firewall.Instance.IsSupported && (StandardRuleWin8.IsSupported || StandardRuleWin7.IsSupported)
                ? Firewall.Instance.Rules.FirstOrDefault(r =>
                    string.Equals(r.Name, ruleName, StringComparison.OrdinalIgnoreCase))
                : FirewallManager.Instance.Rules.FirstOrDefault(r =>
                    string.Equals(r.Name, ruleName, StringComparison.OrdinalIgnoreCase));
        }
    }
}