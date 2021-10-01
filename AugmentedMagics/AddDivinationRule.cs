using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules.Abilities;
using Kingmaker.UnitLogic;
using AugmentedMagics.Utilities;

namespace AugmentedMagics
{    public class AddDivinationRule : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleCalculateAbilityParams>, IRulebookHandler<RuleCalculateAbilityParams>, ISubscriber, IInitiatorRulebookSubscriber
    {
        public void OnEventAboutToTrigger(RuleCalculateAbilityParams evt)
        {
            //Main.Log("AddDivinationRule Handler");
            if (!SpellTools.ValidateEventSpellSchool(evt, SpellSchool.Divination))
            {
                //Main.Log("AddDivinationRule Failed");
                return;
            }
            evt.AbilityData.SaveSpellbookSlot = true;
            //Main.Log("AddDivinationRule End");
        }

        public void OnEventDidTrigger(RuleCalculateAbilityParams evt)
        {
        }
    }
}
