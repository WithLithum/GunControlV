using System;
using System.Collections.Generic;
using System.Net;
using GTA;

namespace GunControl
{
    internal static class Util
    {
        internal static ScanType ScanType { get; set; }
        internal static float ScanRange { get; set; }
        internal static int MaximumGangWeaponCount { get; set; }

        private static readonly List<Ped> _gangMembers = new List<Ped>();

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

        internal static Ped[] ScanPeds()
        {
            switch (ScanType)
            {
                default:
                    return World.GetAllPeds();

                case ScanType.Ranged:
                    return World.GetNearbyPeds(Game.Player.Character, ScanRange);
            }
        }

        internal static void UpdateGangWeapons()
        {
            for (int i = 0; i < _gangMembers.Count; i++)
            {
                Script.Yield();
                if (_gangMembers[i]?.Exists() != true || _gangMembers[i].IsDead || _gangMembers[i].IsPersistent)
                {
                    _gangMembers.RemoveAt(i);
                }
            }
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
                if (_gangMembers.Count > MaximumGangWeaponCount)
                {
                    // Remove weapons at this time
                    ped.Weapons.RemoveAll();
                    return;
                }

                _gangMembers.Add(ped);
                ped.Weapons.Give(WeaponHash.Pistol, 9999, true, true);
            }
            else if (ped.IsCop())
            {
                if (Game.Player.WantedLevel < Main.LevelOfArmed)
                {
                    ped.Weapons.RemoveAll();
                    ped.Weapons.Give(WeaponHash.Nightstick, 1, false, false);
                    return;
                }
            }
            else
            {
                ped.Weapons.RemoveAll();
            }
        }
    }
}
