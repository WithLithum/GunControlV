using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GunControl.FirstResponse.Helpers;
using LSPD_First_Response.Mod.API;
using Rage;

namespace GunControl.FirstResponse
{
    internal static class Swapper
    {
        private static readonly List<Ped> _swappedPeds = new List<Ped>();

        internal static void MainLoop()
        {
            while (!Main.EndLoop)
            {
                GameFiber.Sleep(250);
                foreach (var ped in World.GetAllPeds())
                {
                    GameFiber.Yield();
                    if (!ped) continue;
                    if (_swappedPeds.Contains(ped)) continue;
                    if (ped.IsDead || ped.IsPlayer) continue;

                    Swap(ped);
                    _swappedPeds.Add(ped);
                    GameFiber.Yield();
                }

                GameFiber.Yield();
                for (var i = 0; i < _swappedPeds.Count; i++)
                {
                    GameFiber.Yield();
                    if (!_swappedPeds[i] || _swappedPeds[i].IsDead)
                    {
                        _swappedPeds.RemoveAt(i);
                    }
                }
            }
        }

        internal static void Swap(Ped ped)
        {
            // 3065114024 is Firefighter model
            // Do not remove weapons from peds that are either persistent (commonly created by scripts),
            // or firefighters (their extingusher might get removed)
            if (ped.IsPersistent || ped.Model.Hash == 3065114024)
            {
                return;
            }

            if (ped.IsGangMember() && new Random().Next(0, 100) > ConfigUtil.GangMemberWeaponPrecentage)
            {
                ped.Inventory.Weapons.Clear();
                switch (new Random(new Random().Next()).Next(1, 7))
                {
                    case 1:
                        ped.Inventory.GiveNewWeapon(WeaponHash.Knife, 1, true);
                        break;
                    case 2:
                        ped.Inventory.GiveNewWeapon(WeaponHash.Bat, 1, true);
                        break;
                }
            }
            else if (Functions.IsPedACop(ped) || ped.IsPersistent)
            {
                // Do nothing
            }
            else
            {
                ped.Inventory.Weapons.Clear();
            }
        }
    }
}
