using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules.Abilities;
using Kingmaker.UnitLogic;
using AugmentedMagics.Utilities;
using Kingmaker.UnitLogic.Abilities;
using System;

namespace AugmentedMagics
{    public class AddIllusionRule : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleCalculateAbilityParams>, IRulebookHandler<RuleCalculateAbilityParams>, ISubscriber, IInitiatorRulebookSubscriber
    {
        public void OnEventAboutToTrigger(RuleCalculateAbilityParams evt)
        {
            //Main.Log("AddIllusionRule Handler");
            if(!SpellTools.ValidateEventSpellSchool(evt, SpellSchool.Illusion))
            {
                //Main.Log("AddIllusionRule Failed");
                return;
            }
            evt.AddBonusDC(Math.Max(1,evt.Initiator.Stats.Intelligence.Bonus + 3), Kingmaker.Enums.ModifierDescriptor.Feat);
            //Main.Log("AddIllusionRule End");
        }

        public void OnEventDidTrigger(RuleCalculateAbilityParams evt)
        {
        }
    }
}
