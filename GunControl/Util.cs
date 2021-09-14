using System;
using GTA;

namespace GunControl
{
    internal static class Util
    {
        internal static bool IsGangMember(this Ped ped)
        {
            foreach (var group in Main.GangGroups)
            {
                if (ped.RelationshipGroup == group) return true;
            }

            return false;
        }

        internal static bool IsCop(this Ped ped)
        {
            return ped.RelationshipGroup == "COP";
        }

        internal static void SwapNow(Ped ped)
        {
            // Do not remove weapons from peds that are either persistent (commonly created by scripts),
            // or firefighters (their extingusher might get removed)
            if (ped.IsPersistent || ped.Model == PedHash.Fireman01SMY)
            {
                return;
            }

            if (ped.IsGangMember() && new Random().Next(0, 100) > Main.GangMemberWeaponPrecentage)
            {
                ped.Weapons.RemoveAll();
                switch (new Random(new Random().Next()).Next(1, 4))
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
            else if (ped.IsCop())
            {
                // If merged with parent, it will jump to last fallback which only
                // removes the weapon...
                if (ped.Model != PedHash.Swat01SMY && Game.Player.WantedLevel <= Main.LevelOfArmed)
                {
                    ped.Weapons.RemoveAll();
                    ped.Weapons.Give(WeaponHash.Nightstick, 1, false, false);
                    return;
                }

                if (ped.Model != PedHash.Swat01SMY || !Main.DoAllowSwatTeamWeapons)
                {
                    ped.Weapons.RemoveAll();
                    ped.Weapons.Give(WeaponHash.Nightstick, 1, false, false);
                }
            }
            else
            {
                ped.Weapons.RemoveAll();
            }
        }
    }
}
