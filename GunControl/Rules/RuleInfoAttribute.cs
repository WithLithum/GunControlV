using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GunControl.Rules
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class RuleInfoAttribute : Attribute
    {
        public string Name { get; set; }

        public RuleInfoAttribute(string name)
        {
            this.Name = name;
        }
    }
}
