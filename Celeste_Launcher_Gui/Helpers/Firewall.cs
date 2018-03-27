#region Using directives

using System;
using System.Collections.Generic;
using System.Linq;
using WindowsFirewallHelper;
using WindowsFirewallHelper.FirewallAPIv2;
using WindowsFirewallHelper.FirewallAPIv2.Rules;

#endregion

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
                    goto default_rule;
                }
                return;
            }
            default_rule:
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
                    goto default_rule;
                }

                return;
            }
            default_rule:
            var defaultRule = FirewallManager.Instance.CreatePortRule(
                FirewallProfiles.Domain | FirewallProfiles.Private | FirewallProfiles.Public, ruleName,
                FirewallAction.Allow, portNumber, protocol);

            defaultRule.Direction = direction;

            FirewallManager.Instance.Rules.Add(defaultRule);
        }

        public static void RemoveRules(string ruleName)
        {
            var myRule = FindRules(ruleName).ToArray();
            foreach (var rule in myRule)
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
            var myRule = FindRule(ruleName);
            return FirewallManager.Instance.Rules.Remove(myRule);
        }

        public static bool RuleExist(string ruleName)
        {
            return FindRules(ruleName).Any();
        }

        public static IEnumerable<IRule> FindRules(string ruleName)
        {
            if (Firewall.Instance.IsSupported && (StandardRuleWin8.IsSupported || StandardRuleWin7.IsSupported))
                return Firewall.Instance.Rules.Where(r => string.Equals(r.Name, ruleName,
                    StringComparison.OrdinalIgnoreCase)).ToArray();

            return FirewallManager.Instance.Rules.Where(r => string.Equals(r.Name, ruleName,
                StringComparison.OrdinalIgnoreCase)).ToArray();
        }

        public static IRule FindRule(string ruleName)
        {
            if (Firewall.Instance.IsSupported && (StandardRuleWin8.IsSupported || StandardRuleWin7.IsSupported))
                return Firewall.Instance.Rules.FirstOrDefault(r => string.Equals(r.Name, ruleName,
                    StringComparison.OrdinalIgnoreCase));

            return FirewallManager.Instance.Rules.FirstOrDefault(r => string.Equals(r.Name, ruleName,
                StringComparison.OrdinalIgnoreCase));
        }
    }
}