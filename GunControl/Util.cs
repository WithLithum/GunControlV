using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTA;

namespace GunControl
{
    internal static class Util
    {
        internal static bool IsGangMember(this Ped ped)
        {
            return ped.RelationshipGroup == "AMBIENT_GANG_LOST" || ped.RelationshipGroup == "AMBIENT_GANG_MEXICAN" || ped.RelationshipGroup == "AMBIENT_GANG_FAMILY"
                || ped.RelationshipGroup == "AMBIENT_GANG_BALLAS" || ped.RelationshipGroup == "AMBIENT_GANG_MARABUNTE" || ped.RelationshipGroup == "AMBIENT_GANG_CULT"
                || ped.RelationshipGroup == "AMBIENT_GANG_SALVA" || ped.RelationshipGroup == "AMBIENT_GANG_WEICHENG" || ped.RelationshipGroup == "AMBIENT_GANG_HILLBILLY";
        }

        internal static bool IsCop(this Ped ped)
        {
            return ped.RelationshipGroup == "COP";
        }

        internal static void SwapNow(Ped ped)
        {
            ped.Weapons.RemoveAll();
            if (ped.IsGangMember())
            {
                var randomInt = new Random(new Random().Next()).Next(1, 4);
                switch (randomInt)
                {
                    case 1:
                        ped.Weapons.Give(WeaponHash.Knife, 1, true, true);
                        break;
                    case 2:
                        ped.Weapons.Give(WeaponHash.Bat, 1, true, true);
                        break;
                    case 3:
                        ped.Weapons.Give(WeaponHash.Bottle, 1, true, true);
                        break;
                }
            }
            if (ped.IsCop())
            {
                ped.Weapons.Give(WeaponHash.Nightstick, 1, false, false);
            }
        }
    }
}
