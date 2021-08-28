using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LSPD_First_Response.Mod.API;
using Rage;

namespace GunControl.FirstResponse
{
    public class Main : Plugin
    {
        internal static bool EndLoop { get; private set; }

        public override void Finally()
        {
            EndLoop = true;
        }

        public override void Initialize()
        {
            Game.LogTrivial("Started GunControl");
            GameFiber.StartNew(Swapper.MainLoop, "GunControl Swapping");
        }
    }
}
