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
    public class AddAugmentedPenetrationRule : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleSpellResistanceCheck>, IRulebookHandler<RuleSpellResistanceCheck>, ISubscriber, IInitiatorRulebookSubscriber
    {
        private SpellSchool? _school = null;
        public SpellSchool? School
        {
            get
            {
                return _school;
            }

            set
            {
                _school = value;
            }
        }

        public AddAugmentedPenetrationRule InitializeSchool(SpellSchool school)
        {
            School = school;
            return this;
        }

        public void OnEventAboutToTrigger(RuleSpellResistanceCheck evt)
        {
            if (!_school.HasValue || !SpellTools.ValidateEventSpellSchool(evt, _school.Value))
            {
                return;
            }
            evt.IgnoreSpellResistance = true;
        }

        public void OnEventDidTrigger(RuleSpellResistanceCheck evt)
        {
        }
    }
}
