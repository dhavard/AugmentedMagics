using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.PubSubSystem;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Buffs;
using Kingmaker.UnitLogic.Mechanics;
using System;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Facts;

namespace AugmentedMagics
{
    [AllowMultipleComponents]
    [AllowedOn(typeof(BlueprintUnitFact), false)]
    public class AddAbjurationRule : UnitFactComponentDelegate, IUnitBuffHandler, IGlobalSubscriber, ISubscriber
    {
        public void HandleBuffDidAdded(Buff buff)
        {
            //Main.Log("Abjuration Handler");
            MechanicsContext maybeContext = buff.MaybeContext;
            if (((maybeContext != null) ? maybeContext.MaybeCaster : null) == base.Owner)
            {
                if (!maybeContext.SourceAbility.IsSpell || !maybeContext.SourceAbility.School.Equals(SpellSchool.Abjuration))
                {
                    /*
                    if (!maybeContext.SourceAbility.IsSpell)
                    {
                        Main.Log("Buff is not spell " + maybeContext.SourceAbility.m_DisplayName);
                    }
                    if (!buff.SourceAbility.School.Equals(SpellSchool.Abjuration))
                    {
                        Main.Log("Spell is not abjuration " + maybeContext.SourceAbility.m_DisplayName);
                    }
                    */
                    return;
                }
                buff.SetEndTime(TimeSpan.FromHours(24) + buff.AttachTime);
            }
        }

        public void HandleBuffDidRemoved(Buff buff)
        {
        }
    }
}
