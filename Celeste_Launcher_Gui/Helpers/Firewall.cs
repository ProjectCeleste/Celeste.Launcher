#region Using directives

using System;
using System.Collections.Generic;
using System.Linq;
using WindowsFirewallHelper;

#endregion

namespace Celeste_Launcher_Gui.Helpers
{
    public static class FirewallHelper
    {
        public static void AddApplicationRule(string ruleName, string fileName, FirewallDirection direction,
            FirewallProtocol protocol)
        {
            AddDefaultApplicationRule(ruleName, fileName, direction, protocol);
        }

        private static void AddDefaultApplicationRule(string ruleName, string fileName, FirewallDirection direction, FirewallProtocol protocol)
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
            AddDefaultPortRule(ruleName, portNumber, direction, protocol);
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

        public static IEnumerable<IFirewallRule> FindRules(string ruleName)
        {
            return FirewallManager.Instance.Rules.Where(r => string.Equals(r.Name, ruleName,
                StringComparison.OrdinalIgnoreCase)).ToArray();
        }

        public static IFirewallRule FindRule(string ruleName)
        {
            return FirewallManager.Instance.Rules.FirstOrDefault(r => string.Equals(r.Name, ruleName,
                StringComparison.OrdinalIgnoreCase));
        }
    }
}