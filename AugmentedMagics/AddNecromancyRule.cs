using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules.Abilities;
using Kingmaker.UnitLogic;
using AugmentedMagics.Utilities;
using System;

namespace AugmentedMagics
{    public class AddNecromancyRule : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleCalculateAbilityParams>, IRulebookHandler<RuleCalculateAbilityParams>, ISubscriber, IInitiatorRulebookSubscriber
    {
        public void OnEventAboutToTrigger(RuleCalculateAbilityParams evt)
        {
            //Main.Log("AddNecromancyRule Handler");
            if (!SpellTools.ValidateEventSpellSchool(evt, SpellSchool.Necromancy))
            {
                //Main.Log("AddNecromancyRule Failed");
                return;
            }
            evt.AddBonusDC(Math.Max(1, evt.Initiator.Stats.Strength.Bonus + 3), Kingmaker.Enums.ModifierDescriptor.Feat);
            //Main.Log("AddNecromancyRule End");
        }

        public void OnEventDidTrigger(RuleCalculateAbilityParams evt)
        {
        }
    }
}
