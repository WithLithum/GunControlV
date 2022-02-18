using GTA;
using GTA.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GunControl.Rules.Integrated
{
    public class ModelRule : IRule
    {
        private Model? _actualCondition = null;

        public string Condition { get; set; }

        public bool OutcomeIfMeet { get; set; }

        public bool Evaluate(Ped ped)
        {
            if (_actualCondition == null)
            {
                _actualCondition = new Model(Condition);
            }

            return !OutcomeIfMeet && ped.Model == _actualCondition;
        }
    }
}
