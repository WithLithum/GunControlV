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

        internal static void InitConfig()
        {
            GangMemberWeaponPrecentage = _initialization.ReadInt32("Peds", "GangMemberWeaponChance", 5);
        }
    }
}
