using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.PubSubSystem;
using Kingmaker.UnitLogic;
using AugmentedMagics.Utilities;
using Kingmaker.RuleSystem.Rules.Damage;
using System;
using AugmentedMagics.Settings;

namespace AugmentedMagics
{    public class AddDamageRule : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleCalculateDamage>, IRulebookHandler<RuleCalculateDamage>, ISubscriber, IInitiatorRulebookSubscriber
    {
        public SpellSchool? School = null;
        public AddDamageRule InitializeSchool(SpellSchool school)
        {
            School = school;
            return this;
        }

        public void OnEventAboutToTrigger(RuleCalculateDamage evt)
        {
            //Main.Log("AddDamageRule Handler");
            if (!School.HasValue || !SpellTools.ValidateEventSpellSchool(evt, School.Value))
            {
                //Main.Log("AddDamageRule Failed");
                return;
            }


            SchoolSettingsData ssd = SettingHelper.GetSchoolSettings(School.Value);
            if (ssd.School.HasFlag(AugmentedSchoolSetting.Damage))
            {
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
            }
            //Main.Log("AddDamageRule End");
        }

        public void OnEventDidTrigger(RuleCalculateDamage evt)
        {
        }
    }
}
