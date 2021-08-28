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

            return ped.RelationshipGroup == RelationshipGroup.AmbientGangBallas || ped.RelationshipGroup == RelationshipGroup.AmbientGangCult || ped.RelationshipGroup == RelationshipGroup.AmbientGangFamily
                || ped.RelationshipGroup == RelationshipGroup.AmbientGangHillbilly || ped.RelationshipGroup == RelationshipGroup.AmbientGangLost || ped.RelationshipGroup == RelationshipGroup.AmbientGangMarabunte
                || ped.RelationshipGroup == RelationshipGroup.AmbientGangMexican || ped.RelationshipGroup == RelationshipGroup.AmbientGangSalva || ped.RelationshipGroup == RelationshipGroup.AmbientGangWeicheng;
        }
    }
}
