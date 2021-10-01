using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules.Abilities;
using Kingmaker.UnitLogic;
using AugmentedMagics.Utilities;
using Kingmaker.UnitLogic.Abilities;

namespace AugmentedMagics
{    public class AddTransmutationRule : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleCalculateAbilityParams>, IRulebookHandler<RuleCalculateAbilityParams>, ISubscriber, IInitiatorRulebookSubscriber
    {
        public void OnEventAboutToTrigger(RuleCalculateAbilityParams evt)
        {
            //Main.Log("AddTransmutationRule Handler");
            if (!SpellTools.ValidateEventSpellSchool(evt, SpellSchool.Transmutation))
            {
                //Main.Log("AddTransmutationRule Failed");
                return;
            }
            evt.AddBonusDC(SpellTools.GetHighestCasterStat(evt.Initiator), Kingmaker.Enums.ModifierDescriptor.Feat);
            SpellTools.ConditionalAddMetamagic(evt, Metamagic.Extend);
            //Main.Log("AddTransmutationRule End");
        }

        public void OnEventDidTrigger(RuleCalculateAbilityParams evt)
        {
        }
    }
}
