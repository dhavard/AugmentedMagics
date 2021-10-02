using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.PubSubSystem;
using Kingmaker.UnitLogic;
using AugmentedMagics.Utilities;
using Kingmaker.RuleSystem.Rules.Damage;
using System;
using Kingmaker.RuleSystem.Rules.Abilities;
using Kingmaker.UnitLogic.Abilities.Components;

namespace AugmentedMagics
{    public class AddShadowRule : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleCastSpell>, IRulebookHandler<RuleCastSpell>, ISubscriber, IInitiatorRulebookSubscriber
    {
        public void OnEventAboutToTrigger(RuleCastSpell evt)
        {
            Main.Log("AddShadowRule Handler");

            if (SpellTools.IsShadowSpellEventAbility(evt))
            {
                Main.Log("2 ability Shadow Factor " + evt.Spell.ShadowSpellSettings.Factor);
                if (evt.Spell.ShadowSpellSettings.Factor.IsValueShared)
                {
                    Main.Log("2 Shadow Factor is shared"); //got to be this one
                    if( evt.Reason != null )
                    {
                        if( evt.Reason.Context != null )
                        {
                            Main.Log("2 ability Shadow Factor reason CALC = " + evt.Spell.ShadowSpellSettings.Factor.Calculate(evt.Reason.Context));
                            Main.Log("2 ability Shadow Factor reason SF = " + evt.Reason.Context.ShadowFactorPercents);
                        }
                    }

                    if(evt.Context != null)
                    {
                        Main.Log("2 ability Shadow Factor CALC = " + evt.Spell.ShadowSpellSettings.Factor.Calculate(evt.Context));
                        Main.Log("2 ability Shadow Factor SF = " + evt.Context.ShadowFactorPercents);
                    }
                }
            }

            if (!SpellTools.ValidateEventSpellSchool(evt, SpellSchool.Illusion) &&
                !SpellTools.IsShadowSpellEventAbility(evt))
            {
                Main.Log("AddShadowRule Failed");
                return;
            }

            evt.Spell.ShadowSpellSettings.Factor = 20;
            evt.Context.ShadowFactorPercents += 20;
            Main.Log("AddShadowRule End");
        }

        public void OnEventDidTrigger(RuleCastSpell evt)
        {
        }
    }
}
