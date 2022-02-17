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

        internal static void RegisterRule(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            if (!(type.GetInterface("IRule") is IRule))
            {
                throw new ArgumentException("Rule invalid!", nameof(type));
            }

            var attr = type.GetCustomAttribute<RuleInfoAttribute>();

            if (attr == null)
            {
                throw new ArgumentException("Rule invalid!", nameof(type));
            }

            Rules.Add(attr.Name, type);
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
            if (!Rules.ContainsKey(describer.RuleName))
            {
                Notification.Show("Rule is errored - there is no such rule");
                return;
            }

            if (Activator.CreateInstance(Rules[describer.RuleName]) is IRule rule)
            {
                activated.Add(rule);
            }
        }
    }
}
