using GTA;
using GTA.UI;

namespace GunControl.Rules.Integrated
{
    [RuleInfo("Cop")]
    public class CopRule : IRule
    {
        private bool _invalidNotified;

        public string Condition { get; set; }

        public bool OutcomeIfMeet { get; set; }

        public bool Evaluate(Ped ped)
        {
            if (_invalidNotified || !bool.TryParse(Condition, out bool result))
            {
                // So we don't waste more time setting values here.
                if (!_invalidNotified)
                {
                    Notification.Show("~y~GunControl Warning:~s~ One of the Cop rules has invalid ~r~\"Condition\"~s~ value. It shoule be either ~b~\"True\"~s~ or ~b~\"False\"~s~ and it is ~r~case-sensitive~s~.");
                    _invalidNotified = true;
                }

                return true;
            }

            return result && !OutcomeIfMeet && ped.IsCop();
        }
    }
}
