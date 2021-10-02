using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.PubSubSystem;
using Kingmaker.UnitLogic;
using AugmentedMagics.Utilities;
using Kingmaker.RuleSystem.Rules.Damage;
using System;

namespace AugmentedMagics
{    public class AddEvocationRule : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleCalculateDamage>, IRulebookHandler<RuleCalculateDamage>, ISubscriber, IInitiatorRulebookSubscriber
    {
        public void OnEventAboutToTrigger(RuleCalculateDamage evt)
        {
            //Main.Log("AddEvocationRule Handler");
            if (!SpellTools.ValidateEventSpellSchool(evt, SpellSchool.Evocation))
            {
                //Main.Log("AddEvocationRule Failed");
                return;
            }

            int maxBonus = Math.Max(1,SpellTools.GetHighestCasterStat(evt.Initiator));
            int bonus = maxBonus;
            foreach (BaseDamage baseDamage in evt.DamageBundle)
            {
                if (!baseDamage.Sneak && bonus > 0)
                {
                    //diminishing returns for things like magic missile capped at = (bonus * (bonus + 1) / 2 = (or sum 1 to bonus)
                    //Note that many evocation spells have a DC which results in this damage getting cut in half.
                    baseDamage.AddModifier(bonus--, base.Fact);
                }
            }
            //Main.Log("AddEvocationRule End");
        }

        public void OnEventDidTrigger(RuleCalculateDamage evt)
        {
        }
    }
}
