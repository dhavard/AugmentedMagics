using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.PubSubSystem;
using Kingmaker.UnitLogic;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Facts;
using Kingmaker.RuleSystem.Rules;
using AugmentedMagics.Utilities;
using AugmentedMagics.Settings;

namespace AugmentedMagics
{
    [AllowMultipleComponents]
    [AllowedOn(typeof(BlueprintUnitFact), false)]
    public class AddAugmentedPenetrationRule : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleSpellResistanceCheck>, IRulebookHandler<RuleSpellResistanceCheck>, ISubscriber, IInitiatorRulebookSubscriber
    {
        public SpellSchool? School = null;

        public AddAugmentedPenetrationRule InitializeSchool(SpellSchool school)
        {
            School = school;
            return this;
        }

        public void OnEventAboutToTrigger(RuleSpellResistanceCheck evt)
        {
            if (!School.HasValue || !SpellTools.ValidateEventSpellSchool(evt, School.Value))
            {
                return;
            }

            SchoolSettingsData ssd = SettingHelper.GetSchoolSettings(School.Value);

            if (ssd.School.HasFlag(AugmentedSchoolSetting.Penetration))
            {
                evt.IgnoreSpellResistance = true;
            }
        }

        public void OnEventDidTrigger(RuleSpellResistanceCheck evt)
        {
        }
    }
}
