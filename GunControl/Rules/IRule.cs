using GTA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GunControl.Rules
{
    public interface IRule
    {
        string Condition { get; set; }

        bool OutcomeIfMeet { get; set; }

        bool Evaluate(Ped ped);
    }
}
