using Kingmaker.Blueprints.Classes.Spells;
using System;
using System.Linq;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.RuleSystem.Rules.Abilities;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.RuleSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.UnitLogic;

/***
 * NOT actually taken from https://github.com/Vek17/WrathMods-TabletopTweaks/tree/master/TabletopTweaks/Utilities, but it was useful for learning.
 ***/
namespace AugmentedMagics.Utilities {
    static class SpellTools
    {
        public static void ConditionalAddMetamagic(RuleCalculateAbilityParams evt, Metamagic meta)
        {
            if (MetamagicHelper.HasMetamagic(evt.Spell.AvailableMetamagic, meta))
            {
                //Main.Log("AddAbbilityParmRule Spell " + evt.Spell.Name + " Metamagic " + meta);
                evt.AddMetamagic(meta);
            }
        }

        public static bool ValidateEventSpellSchool(RuleCalculateAbilityParams evt, SpellSchool school)
        {
            /*
            if (evt.Spell == null) 
            {
                Main.Log("School " + school + " spell is null");
            }
            else
            {
                if (!evt.Spell.IsSpell)
                {

                    Main.Log("School " + school + " spell is not spell");
                }
                if(!evt.Spell.School.Equals(school))
                {

                    Main.Log("School " + school + " context School is " + evt.Spell.School);
                }
            }
            */

            if (evt.Spell == null || !evt.Spell.IsSpell || !evt.Spell.School.Equals(school))
            {
                //Main.Log("Tools return false;");
                return false;
            }

            //Main.Log("Tools return true;");
            return true;
        }

        public static bool ValidateEventSpellSchool(RulebookTargetEvent evt, SpellSchool school)
        {
            MechanicsContext context = evt.Reason.Context;

            /*
            if ((context?.SourceAbility) == null)
            {
                Main.Log("School " + school + " context SourceAbility is null");
            }
            else if (!context.SourceAbility.IsSpell)
            {
                Main.Log("School " + school + " context SourceAbility is not a spell");
            }
            if (!context.SpellSchool.Equals(school))
            {
                Main.Log("School " + school + " context School is " + context.SpellSchool);
            }
            */

            if ((context?.SourceAbility) == null || !context.SourceAbility.IsSpell || !context.SpellSchool.Equals(school))
            {

                //Main.Log("Tools return false;");
                return false;
            }

            //Main.Log("Tools return true;");
            return true;
        }

        public static int GetHighestCasterLevel(UnitEntityData unit)
        {
            if (unit == null)
            {
                return 0;
            }

            return unit.Spellbooks.Aggregate(delegate (Spellbook s1, Spellbook s2)
            {
                if (s1.MaxSpellLevel <= s2.MaxSpellLevel)
                {
                    return s2;
                }
                return s1;
            }).MaxSpellLevel;

        }

        public static int GetHighestCasterStat(UnitEntityData unit)
        {
            if (unit == null)
            {
                return 0;
            }

            return StatTypeHelper.Attributes.Select(new Func<StatType, ModifiableValueAttributeStat>(unit.Stats.GetStat<ModifiableValueAttributeStat>)).Aggregate(delegate (ModifiableValueAttributeStat s1, ModifiableValueAttributeStat s2)
            {
                if (!s1.Type.IsAttribute() || !(s1.Type.Equals(StatType.Charisma) || s1.Type.Equals(StatType.Intelligence) || s1.Type.Equals(StatType.Wisdom)))
                {
                    return s2;
                }
                if (s1.Bonus <= s2.Bonus)
                {
                    return s2;
                }
                return s1;
            }).Bonus;
        }
    }
}