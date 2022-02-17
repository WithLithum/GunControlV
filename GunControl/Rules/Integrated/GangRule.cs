using GTA;
using GTA.UI;

namespace GunControl.Rules.Integrated
{
    [RuleInfo("GangMember")]
    public class GangRule : IRule
    {
        public string Condition { get; set; }

        public bool OutcomeIfMeet { get; set; }

        public bool Evaluate(Ped ped)
        {
            if (!bool.TryParse(Condition, out bool result))
            {
#if DEBUG
                Screen.ShowSubtitle($"Invalid value - {Condition} (should be {bool.TrueString} or {bool.FalseString})");
#endif
                // Go ahead and ignore
                return true;
            }

            return result && !OutcomeIfMeet && ped.IsGangMember();
        }
    }
}
