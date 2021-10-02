using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules.Abilities;
using Kingmaker.UnitLogic;
using AugmentedMagics.Utilities;
using Kingmaker.UnitLogic.Abilities;
using System;
using Kingmaker.UnitLogic.Abilities.Components;

namespace AugmentedMagics
{
    public class AddIllusionRule : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleCalculateAbilityParams>, IRulebookHandler<RuleCalculateAbilityParams>, ISubscriber, IInitiatorRulebookSubscriber
    {
        public void OnEventAboutToTrigger(RuleCalculateAbilityParams evt)
        {
            Main.Log("AddIllusionRule Handler");
            /*
            AbilityShadowSpell ass = SpellTools.GetShadowSpellEventComponent(evt);

            if (SpellTools.IsShadowSpellEventAbility(evt))
            {
                Main.Log("ability Shadow Factor " + evt.AbilityData.ShadowSpellSettings.Factor);
                if (evt.AbilityData.ShadowSpellSettings.Factor.IsValueSimple)
                {
                    Main.Log("Shadow Factor is simple");
                }
                else if (evt.AbilityData.ShadowSpellSettings.Factor.IsValueShared)
                {
                    Main.Log("Shadow Factor is shared");
                }
                else if (evt.AbilityData.ShadowSpellSettings.Factor.IsValueRank)
                {
                    Main.Log("Shadow Factor is rank");
                }
                else if (evt.AbilityData.ShadowSpellSettings.Factor.IsValueProperty)
                {
                    Main.Log("Shadow Factor is property");
                }
                else if (evt.AbilityData.ShadowSpellSettings.Factor.IsValueCustomProperty)
                {
                    Main.Log("Shadow Factor is custom");
                }
                else if (evt.AbilityData.ShadowSpellSettings.Factor.IsTargetProperty)
                {
                    Main.Log("Shadow Factor is target");
                }
            }

            if (ass != null)
            {
                Main.Log("ass Shadow Factor " + ass.Factor);
                if (ass.Factor.IsValueSimple)
                {
                    Main.Log("Shadow Factor is simple");
                }
                else if (ass.Factor.IsValueShared)
                {
                    Main.Log("Shadow Factor is shared ");
                    //ass.Factor = 100;
                    //Main.Log("Shadow Factor is shared " + evt.Reason.Context[ass.Factor.ValueShared]); // this one  //Shd.Damage
                    /*
                     * MechanicsContext context = evt.Reason.Context;
                     * case ContextValueType.Shared:
				        if (context != null)
				        {
					        return context[this.ValueShared];
				        }
				        break;
                     * 
                     * *
                }
                else if (ass.Factor.IsValueRank)
                {
                    Main.Log("Shadow Factor is rank");
                }
                else if (ass.Factor.IsValueProperty)
                {
                    Main.Log("Shadow Factor is property");
                }
                else if (ass.Factor.IsValueCustomProperty)
                {
                    Main.Log("Shadow Factor is custom");
                }
                else if (ass.Factor.IsTargetProperty)
                {
                    Main.Log("Shadow Factor is target");
                }
            }
            */

            if (!SpellTools.ValidateEventSpellSchool(evt, SpellSchool.Illusion) 
                //&& !(SpellTools.IsShadowSpellEventAbility(evt) || ass != null )
                )
            {
                Main.Log("AddIllusionRule Failed");
                return;
            }

            //evt.AddBonusDC(Math.Max(1, evt.Initiator.Stats.Intelligence.Bonus + 3), Kingmaker.Enums.ModifierDescriptor.Feat);
            Main.Log("AddIllusionRule End");
        }

        public void OnEventDidTrigger(RuleCalculateAbilityParams evt)
        {
        }
    }
}
