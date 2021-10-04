using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules.Abilities;
using Kingmaker.UnitLogic;
using AugmentedMagics.Utilities;
using Kingmaker.UnitLogic.Abilities;
using System;
using AugmentedMagics.Settings;

namespace AugmentedMagics
{    public class AddAbilityParamRule : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleCalculateAbilityParams>, IRulebookHandler<RuleCalculateAbilityParams>, ISubscriber, IInitiatorRulebookSubscriber
    {
        public SpellSchool? School = null;
        public AddAbilityParamRule InitializeSchool(SpellSchool school)
        {
            School = school;
            return this;
        }

        public void OnEventAboutToTrigger(RuleCalculateAbilityParams evt)
        {
            //Main.Log("AddAbbilityParmRule Handler");
            if (!School.HasValue || !SpellTools.ValidateEventSpellSchool(evt, School.Value))
            {
                //Main.Log("AddAbbilityParmRule Failed");
                return;
            }
            //Main.Log("AddAbbilityParmRule Passed");

            SchoolSettingsData ssd = SettingHelper.GetSchoolSettings(School.Value);

            if (ssd.School.HasFlag(AugmentedSchoolSetting.DC))
            {
                int bonus = 1 + Math.Max(1, SpellTools.GetHighestCasterLevel(evt.Initiator) / 2);
                evt.AddBonusDC(bonus, Kingmaker.Enums.ModifierDescriptor.Feat);
            }

            if (ssd.School.HasFlag(AugmentedSchoolSetting.DCbyStats))
            {
                int bonus = 1 + Math.Max(1, SpellTools.GetHighestCasterStat(evt.Initiator));
                evt.AddBonusDC(bonus, Kingmaker.Enums.ModifierDescriptor.Feat);
            }

            if (ssd.School.HasFlag(AugmentedSchoolSetting.InifiniteCast))
            {
                evt.AbilityData.SaveSpellbookSlot = true;
            }

            if (ssd.School.HasFlag(AugmentedSchoolSetting.OneDayBuffs))
            {

            }

            foreach (Metamagic meta in (Metamagic[])Enum.GetValues(typeof(Metamagic)))
            {
                if (SettingHelper.HasMetamagic(School.Value, meta))
                {
                    Main.Log("AddAbbilityParmRule School " + School.Value + " Metamagic " + meta);
                    SpellTools.ConditionalAddMetamagic(evt, meta);
                }
            }
            //Main.Log("AddAbbilityParmRule End");
        }

        public void OnEventDidTrigger(RuleCalculateAbilityParams evt)
        {
        }
    }
}
