using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rage;
using Rage.Exceptions;

namespace GunControl.FirstResponse.Helpers
{
    internal static class PedExtensions
    {
        internal static bool IsGangMember(this Ped ped)
        {
            if (ped == null) throw new ArgumentNullException(nameof(ped));
            if (!ped.IsValid()) throw new InvalidHandleableException(ped);
            
            foreach (var group in ConfigUtil.GangGroups)
            {
                if (ped.RelationshipGroup == group) return true;
            }

            return false;
        }
    }
}
