﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTA;
using GunControl.Rules;

namespace GunControl
{
    public class Main : Script
    {
        private readonly List<Ped> _swappedPeds = new List<Ped>();

        internal static bool DoAllowSwatTeamWeapons { get; private set; }

        internal static int GangMemberWeaponPrecentage { get; private set; }

        internal static int LevelOfArmed { get; private set; }

#pragma warning disable S2223 // Non-constant static fields should not be visible
        internal static string[] GangGroups;
#pragma warning restore S2223 // Non-constant static fields should not be visible

        public Main()
        {
            // Initializes the script tick
            this.Interval = Settings.GetValue("Misc", "ScanningInterval", 250);
            this.Tick += Main_Tick;

            // Initialize config
            DoAllowSwatTeamWeapons = Settings.GetValue("Peds", "AllowSwatWeapons", true);
            GangMemberWeaponPrecentage = Settings.GetValue("Peds", "GangMemberWeaponChance", 5);
            LevelOfArmed = Settings.GetValue("Wanted", "LevelOfArmed", 3);

            Util.ScanType = Settings.GetValue("Misc", "ScanType", ScanType.All);
            Util.ScanRange = Settings.GetValue("Misc", "ScanRange", 250f);
            Util.MaximumGangWeaponCount = Settings.GetValue("Peds", "MaximumGangMembersWithWeapons", 5);

            // cache the values first
            var strings = Settings.GetValue("Misc", "GangGroups", "AMBIENT_GANG_LOST,AMBIENT_GANG_MEXICAN,AMBIENT_GANG_FAMILY,AMBIENT_GANG_BALLAS,AMBIENT_GANG_MARABUNTE,AMBIENT_GANG_CULT,AMBIENT_GANG_SALVA,AMBIENT_GANG_WEICHENG,AMBIENT_GANG_HILLBILLY");
#pragma warning disable S3010 // Static fields should not be updated in constructors
            GangGroups = strings.Split(','); // Read gang groups configuration as an enum
#pragma warning restore S3010 // Static fields should not be updated in constructors
        }

        private void Main_Tick(object sender, EventArgs e)
        {
            foreach (var ped in World.GetAllPeds())
            {
                // Loop through all peds
                // Thread blocking will decrease FPS
                Yield();
                if (ped?.Exists() != true) continue;
                if (_swappedPeds.Contains(ped)) continue;
                if (ped.IsDead || ped.IsPlayer) continue;
                if (!RuleManager.EvaluateRules(ped)) continue;

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
