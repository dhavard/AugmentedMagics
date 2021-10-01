using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules.Abilities;
using Kingmaker.UnitLogic;
using AugmentedMagics.Utilities;
using Kingmaker.UnitLogic.Abilities;

namespace AugmentedMagics
{    public class AddEnchantmentRule : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleCalculateAbilityParams>, IRulebookHandler<RuleCalculateAbilityParams>, ISubscriber, IInitiatorRulebookSubscriber
    {
        public void OnEventAboutToTrigger(RuleCalculateAbilityParams evt)
        {
            //Main.Log("AddEnchantmentRule Handler");
            if (!SpellTools.ValidateEventSpellSchool(evt, SpellSchool.Enchantment))
            {
                //Main.Log("AddEnchantmentRule Failed");
                return;
            }
            evt.AddBonusDC(evt.Initiator.Stats.Charisma.Bonus + 5, Kingmaker.Enums.ModifierDescriptor.Feat);
            //Main.Log("AddEnchantmentRule End");
        }

        public void OnEventDidTrigger(RuleCalculateAbilityParams evt)
        {
        }
    }
}
