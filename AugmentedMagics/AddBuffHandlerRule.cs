using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.PubSubSystem;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Buffs;
using Kingmaker.UnitLogic.Mechanics;
using System;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Facts;
using AugmentedMagics.Settings;

namespace AugmentedMagics
{
    [AllowMultipleComponents]
    [AllowedOn(typeof(BlueprintUnitFact), false)]
    public class AddBuffHandlerRule : UnitFactComponentDelegate, IUnitBuffHandler, IGlobalSubscriber, ISubscriber
    {
        public SpellSchool? School = null;
        public AddBuffHandlerRule InitializeSchool(SpellSchool school)
        {
            School = school;
            return this;
        }

        public void HandleBuffDidAdded(Buff buff)
        {
            //Main.Log("Abjuration Handler");
            MechanicsContext maybeContext = buff.MaybeContext;
            if (((maybeContext != null) ? maybeContext.MaybeCaster : null) == base.Owner && School.HasValue)
            {
                if (!maybeContext.SourceAbility.IsSpell || !maybeContext.SourceAbility.School.Equals(School.Value))
                {
                    return;
                }
                SchoolSettingsData ssd = SettingHelper.GetSchoolSettings(School.Value);

                if (ssd.School.HasFlag(AugmentedSchoolSetting.OneDayBuffs))
                {
                    buff.SetEndTime(TimeSpan.FromHours(24) + buff.AttachTime);
                }
            }
        }

        public void HandleBuffDidRemoved(Buff buff)
        {
        }
    }
}
