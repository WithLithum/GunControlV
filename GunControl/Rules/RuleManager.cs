using GTA;
using GTA.UI;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace GunControl.Rules
{
    internal static class RuleManager
    {
        internal static readonly Dictionary<string, Type> Rules = new Dictionary<string, Type>();
        private static readonly List<IRule> activated = new List<IRule>();

        internal static void RegisterRule(string name, Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            if (type.GetInterface("IRule") != typeof(IRule))
            {
                throw new ArgumentException("Rule did not inherit IRule.", nameof(type));
            }

            Rules.Add(name, type);
        }

        internal static bool EvaluateRules(Ped ped)
        {
            foreach (var rule in activated)
            {
                if (!rule.Evaluate(ped))
                {
                    return false;
                }
            }

            return true;
        }

        internal static void ParseRule(RuleDescriber describer)
        {
            if (describer == null)
            {
                Notification.Show("Describer null - check parser");
                return;
            }

            if (!Rules.ContainsKey(describer.RuleName))
            {
                Notification.Show("Rule is errored - there is no such rule");
                return;
            }

            if (Activator.CreateInstance(Rules[describer.RuleName]) is IRule rule)
            {
                rule.Condition = describer.Condition;
                rule.OutcomeIfMeet = describer.DoesClear;
                activated.Add(rule);
            }
        }
    }
}
