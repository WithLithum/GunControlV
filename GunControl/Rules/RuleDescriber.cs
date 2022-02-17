using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GunControl.Rules
{
    [Serializable]
    [XmlType(TypeName = "Rule")]
    public class RuleDescriber
    {
        public string RuleName { get; set; }
        public string Condition { get; set; }
        public bool DoesClear { get; set; }
    }

    [Serializable]
    public class Rules
    {
        public int Version { get; set; } = 1;
#pragma warning disable S1104 // Fields should not have public accessibility
        public RuleDescriber[] Contents;
#pragma warning restore S1104 // Fields should not have public accessibility
    }
}
