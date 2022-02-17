using System;
using System.Collections.Generic;
using System.Net;
using GTA;
using GTA.UI;

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
            // The check is simple
            // All it does is compare peds' current group with every single groups defined in INI

            // TODO: Calculate hash at startup so we don't have to calculate every single time
            foreach (var group in Main.GangGroups)
            {
                if (ped.RelationshipGroup == group) return true;
            }

            // If it is not in groups returns false
            return false;
        }

        internal static bool IsCop(this Ped ped)
        {
            // The check is simple too
            // TODO: use hash instead of string
            return ped.RelationshipGroup == "COP";
        }

        internal static Ped[] ScanPeds()
        {
            switch (ScanType)
            {
                default:
                    // The old way of doing things
                    // Just dump all peds in the pool
                    return World.GetAllPeds();

                case ScanType.Ranged:
                    // Just get peds in the range from player
                    return World.GetNearbyPeds(Game.Player.Character, ScanRange);
            }
        }

        internal static void UpdateGangWeapons()
        {
            for (int i = 0; i < _gangMembers.Count; i++)
            {
                // Yield here so FPS don't drop fast when we cycle through
                // Cuz we checks if it is valid after the yield
                Script.Yield();
                if (_gangMembers[i]?.Exists() != true || _gangMembers[i].IsDead || _gangMembers[i].IsPersistent)
                {
                    _gangMembers.RemoveAt(i);
                }
            }

#if DEBUG
            // Debug function
            // Just press F11 and you'll have member count
            if (Game.IsKeyPressed(System.Windows.Forms.Keys.F11))
            {
                Screen.ShowSubtitle($"Gang members: {_gangMembers.Count} out of {MaximumGangWeaponCount}");
            }
#endif
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
                    // TODO: Add melee weapons that is configurable in INI
                    ped.Weapons.RemoveAll();
                    return;
                }

                // So we gave them pistol and add them to list
                _gangMembers.Add(ped);
                ped.Weapons.Give(WeaponHash.Pistol, 9999, true, true);
            }
            else if (ped.IsCop())
            {
                if (Game.Player.WantedLevel < Main.LevelOfArmed)
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
