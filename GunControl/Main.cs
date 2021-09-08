using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTA;

namespace GunControl
{
    public class Main : Script
    {
        private readonly List<Ped> _swappedPeds = new List<Ped>();

        internal static bool DoAllowSwatTeamWeapons { get; private set; }

        internal static int GangMemberWeaponPrecentage { get; private set; }

        internal static int LevelOfArmed { get; private set; }

        public Main()
        {
            this.Interval = 250;
            this.Tick += Main_Tick;

            DoAllowSwatTeamWeapons = Settings.GetValue("Peds", "AllowSwatWeapons", true);
            GangMemberWeaponPrecentage = Settings.GetValue("Peds", "GangMemberWeaponChance", 5);
            LevelOfArmed = Settings.GetValue("Wanted", "LevelOfArmed", 3);
        }

        private void Main_Tick(object sender, EventArgs e)
        {
            foreach (var ped in World.GetAllPeds())
            {
                Yield();
                if (ped?.Exists() != true) continue;
                if (_swappedPeds.Contains(ped)) continue;
                if (ped.IsDead || ped.IsPlayer) continue;

                Util.SwapNow(ped);
                Yield();
                _swappedPeds.Add(ped);
            }

            Yield();
            for (var i = 0; i < _swappedPeds.Count; i++)
            {
                Yield();
                if (_swappedPeds[i]?.Exists() != true || _swappedPeds[i].IsDead)
                {
                    _swappedPeds.RemoveAt(i);
                }
            }
        }
    }
}
