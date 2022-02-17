using GTA;

namespace GunControl.Rules.Integrated
{
    [RuleInfo("Cop")]
    public class CopRule : IRule
    {
        public string Condition { get; set; }

        public bool OutcomeIfMeet { get; set; }

        public bool Evaluate(Ped ped)
        {
            if (!bool.TryParse(Condition, out bool result))
            {
                // Go ahead and ignore
                return true;
            }

            return result && !OutcomeIfMeet && ped.IsCop();
        }
    }
}
