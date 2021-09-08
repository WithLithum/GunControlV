using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rage;

namespace GunControl.FirstResponse
{
    internal static class ConfigUtil
    {
        private static readonly InitializationFile _initialization = new InitializationFile(@"plugins\LSPDFR\GunControl.ini");

        internal static int GangMemberWeaponPrecentage { get; private set; }

        internal static List<string> GangGroups { get; private set; }

        internal static void InitConfig()
        {
            Game.LogTrivial("GunControl reading configuration.");
            GangMemberWeaponPrecentage = _initialization.ReadInt32("Peds", "GangMemberWeaponChance", 5);
            Game.LogTrivial("GunControl: Gang Member weapon chance is " + GangMemberWeaponPrecentage);

            var strings = _initialization.ReadString("Misc", "GangGroups", "AMBIENT_GANG_LOST,AMBIENT_GANG_MEXICAN,AMBIENT_GANG_FAMILY,AMBIENT_GANG_BALLAS,AMBIENT_GANG_MARABUNTE,AMBIENT_GANG_CULT,AMBIENT_GANG_SALVA,AMBIENT_GANG_WEICHENG,AMBIENT_GANG_HILLBILLY");
            GangGroups = strings.Split(',').ToList();
            Game.LogTrivial("GunControl: Gang groups are " + strings);
        }
    }
}
