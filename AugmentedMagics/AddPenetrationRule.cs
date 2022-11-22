using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.PubSubSystem;
using Kingmaker.UnitLogic;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Facts;
using Kingmaker.RuleSystem.Rules;
using AugmentedMagics.Utilities;

namespace AugmentedMagics
{
    [AllowMultipleComponents]
    [AllowedOn(typeof(BlueprintUnitFact), false)]
    public class AddPenetrationRule : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleSpellResistanceCheck>, IRulebookHandler<RuleSpellResistanceCheck>, ISubscriber, IInitiatorRulebookSubscriber
    {
        public void OnEventAboutToTrigger(RuleSpellResistanceCheck evt)
        {
            if (Main.Settings.StrongerSpellPenetration)
            {
                evt.AddSpellPenetration(2, Kingmaker.Enums.ModifierDescriptor.Feat);
            }
        }

        public void OnEventDidTrigger(RuleSpellResistanceCheck evt)
        {
        }
    }
}
