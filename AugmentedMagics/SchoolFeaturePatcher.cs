using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.JsonSystem;
using AugmentedMagics.Extensions;
using AugmentedMagics.Utilities;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Designers.Mechanics.Recommendations;
using Kingmaker.Designers.Mechanics.Facts;
using AugmentedMagics.Settings;

/**
 * Adopted from Scaling Cantrips by RealityMachina
 * See https://github.com/RealityMachina/Scaling-Cantrips
 * **/
namespace AugmentedMagics
{
    class SchoolFeaturePatcher
    {
        [HarmonyPatch(typeof(BlueprintsCache), "Init")]
        public static class BlueprintPatcher
        {
            static bool Initialized;

            static string SPELL_FOCUS = "16fa59cc9a72a6043b566b49184f53fe"; //BlueprintParameterizedFeature   //FeatureGroup      //NestedFeatureSelectionUtils
            static string SPELL_FOCUS_GREATER = "5b04b45b228461c43bad768eb0f7c7bf"; //BPF
            static string SPELL_FOCUS_MYTHIC = "41fa2470ab50ff441b4cfbb2fc725109"; //BPF

            //SpellFocusParametrized
            //IncreaseSpellSchoolDC

            static string SPELL_FOCUS_DESC = "Choose a school of magic. Any spells you cast of that school are more difficult to resist.\n\nBenefit: Add +2 to the Difficulty Class for all saving throws against spells from the school of magic you select.\n\nSpecial: You can gain this feat multiple times. Its effects do not stack. Each time you take the feat, it applies to a new school of magic.";
            static string SPELL_FOCUS_GR_DESC = "Choose a school of magic to which you have already applied to Spell Focus feat. Any spells you cast of that school are very hard to resist.\n\nBenefit: Add +2 to the Difficulty Class for all saving throws against spells from the school of magic you select. This bonus stacks with the bonus from Spell Focus.\n\nSpecial: You can gain this feat multiple times. Its effects do not stack. Each time you take the feat, it applies to a new school to which you have already applied the Spell Focus feat.";

            static string SPELL_PEN = "ee7dc126939e4d9438357fbd5980d459";
            static string SPELL_PEN_GREATER = "1978c3f91cfbbc24b9c9b0d017f4beec";
            static string SPELL_PEN_MYTHIC = "51b6b22ff184eef46a675449e837365d";

            static string SPELL_PEN_DESC = "Your spells break through Spell Resistance more easily than most. \n\nYou get +4 bonus on caster level checks (1d20 + caster level) made to overcome a creatures spell resistance.";
            static string SPELL_PEN_GR_DESC = SPELL_PEN_DESC + "\nThis bonus stacks with the one from Spell Penetration.";

            //Standard spell focus facts
            static string ABJURATION = "71a3f1c1ac77ae3488b9b3d6d2aac01a"; //PrerequisiteParametrizedFeature  //PrerequisiteFeature
            static string CONJURATION = "d342cc595f499434687f9765f56d525c";
            static string DIVINATION = "955e97411611d384db2cbc00d7ed5ead";
            static string ENCHANTMENT = "c5bf645f128c39b40850cde005b8538f";
            static string EVOCATION = "743d0106ee3076342839c6d550cdda25";
            static string ILLUSION = "e588279a80eb7a24b813fadad4bc83b5";
            static string NECROMANCY = "8791da25011fd1844ad61a3fea6ece54";
            static string TRANSMUTATION = "49907a2e51b49d641aad3c9781a3a698";

            //Greater spell focus facts
            static string ABJURATION_GR = "f4c39693ebaffd244bbe4694e3c3ce0d"; //PrerequisiteParametrizedFeature  //PrerequisiteFeature
            static string CONJURATION_GR = "d1e78eeeb5295e048be5b2500c002233";
            static string DIVINATION_GR = "1deb774e8ba7ec64c83cd26c2d09f019";
            static string ENCHANTMENT_GR = "f2ae8d44d24996c449e69b6f5248507d";
            static string EVOCATION_GR = "f2970e8a73baeae469407638f7e75f50";
            static string ILLUSION_GR = "b6dff9186edca3141ba0ab4376d1347d";
            static string NECROMANCY_GR = "7f7a73444f15b8644a8cf0f2f149c6aa";
            static string TRANSMUTATION_GR = "74fd23356fc4c9847986d48f1a40bc7a";

            //augment summoning
            static string AUG_SUMMON = "38155ca9e4055bb48a89240a2055dcc3";
            /*
               [AugmentedMagics] PPF is parameter spell Conjuration
               [AugmentedMagics] PPF feature name Spell Focus guid 16fa59cc9a72a6043b566b49184f53fe
            */

            static string MYTHIC_FEAT_SELECTION = "9ee0f6745f555484299b0a1563b99d81";
            static string MYTHIC_ABILITY_SELECTION = "ba0e5a900b775be4a99702f1ed08914d";

            [HarmonyPriority(Priority.LowerThanNormal)]
            static void Postfix()
            {
                if (Initialized) return;
                Initialized = true;
                AddFeatures();
                AddParentPrereqs();
                ImproveBaseFeats();
            }

            static void ImproveBaseFeats()
            {
                DoubleSpellPenetration(SPELL_PEN, SPELL_PEN_DESC);
                DoubleSpellPenetration(SPELL_PEN_GREATER, SPELL_PEN_GR_DESC);
                DoubleSpellPenetration(SPELL_PEN_MYTHIC, null);

                DoubleSpellDifficultyCheck(SPELL_FOCUS, SPELL_FOCUS_DESC);
                DoubleSpellDifficultyCheck(SPELL_FOCUS_GREATER, SPELL_FOCUS_GR_DESC);
            }

            static void AddParentPrereqs()
            {
                if (!Main.Settings.AugmentedMagicSchoolFeats)
                    return;

                AddParentPrerequisiteParameterized("AMAbjuration", SPELL_FOCUS_GREATER, SpellSchool.Abjuration);
                AddParentPrerequisiteParameterized("AMConjuration", SPELL_FOCUS_GREATER, SpellSchool.Conjuration);
                AddParentPrerequisiteParameterized("AMDivination", SPELL_FOCUS_GREATER, SpellSchool.Divination);
                AddParentPrerequisiteParameterized("AMEnchantment", SPELL_FOCUS_GREATER, SpellSchool.Enchantment);
                AddParentPrerequisiteParameterized("AMEvocation", SPELL_FOCUS_GREATER, SpellSchool.Evocation);
                AddParentPrerequisiteParameterized("AMIllusion", SPELL_FOCUS_GREATER, SpellSchool.Illusion);
                AddParentPrerequisiteParameterized("AMNecromancy", SPELL_FOCUS_GREATER, SpellSchool.Necromancy);
                AddParentPrerequisiteParameterized("AMTransmutation", SPELL_FOCUS_GREATER, SpellSchool.Transmutation);

                AddParentPrerequisite("AMAbjuration", SPELL_PEN_GREATER);
                AddParentPrerequisite("AMConjuration", SPELL_PEN_GREATER);
                AddParentPrerequisite("AMDivination", SPELL_PEN_GREATER);
                AddParentPrerequisite("AMEnchantment", SPELL_PEN_GREATER);
                AddParentPrerequisite("AMEvocation", SPELL_PEN_GREATER);
                AddParentPrerequisite("AMIllusion", SPELL_PEN_GREATER);
                AddParentPrerequisite("AMNecromancy", SPELL_PEN_GREATER);
                AddParentPrerequisite("AMTransmutation", SPELL_PEN_GREATER);
            }

            private static void DoubleSpellPenetration(string penfeat, string desc)
            {
                BlueprintFeature bp = Resources.GetBlueprint<BlueprintFeature>(penfeat);

                if (desc != null && Main.Settings.StrongerSpellPenetration)
                {
                    bp.SetDescription(desc);
                }

                Main.Log("Increasing Spell Penetration");
                bp.AddComponent(new AddPenetrationRule());
            }

            private static void DoubleSpellDifficultyCheck(string focusfeat, string desc)
            {
                BlueprintFeature bp = Resources.GetBlueprint<BlueprintFeature>(focusfeat);

                if (desc != null && Main.Settings.StrongerSpellFocus)
                {
                    bp.SetDescription(desc);
                }

                if (Main.Settings.StrongerSpellFocus)
                {
                    foreach (BlueprintComponent bc in bp.GetComponents<BlueprintComponent>())
                    {
                        if (bc is SpellFocusParametrized)
                        {
                            ((SpellFocusParametrized)bc).BonusDC += 1;
                        }
                    }
                }
            }

            private static void AddParentPrerequisite(string name, string parent)
            {
                var bp = Resources.GetModBlueprint<BlueprintFeature>(name);
                bp.AddComponent(Helpers.Create<PrerequisiteFeature>(c =>
                {
                    c.m_Feature = Resources.GetBlueprint<BlueprintFeature>(parent).ToReference<BlueprintFeatureReference>();
                }));
            }

            private static void AddParentPrerequisiteParameterized(string name, string parent, SpellSchool school)
            {
                var bp = Resources.GetModBlueprint<BlueprintFeature>(name);
                bp.AddComponent(Helpers.Create<PrerequisiteParametrizedFeature>(c =>
                {
                    c.m_Feature = Resources.GetBlueprint<BlueprintFeature>(parent).ToReference<BlueprintFeatureReference>();
                    c.ParameterType = FeatureParameterType.SpellSchool;
                    c.SpellSchool = school;
                }));
            }

            static void AddFeatures()
            {
                if (!Main.Settings.AugmentedMagicSchoolFeats)
                    return;

                AddWizardSchoolFeature("AMAbjuration", "Augmented Abjuration", SpellSchool.Abjuration);
                AddWizardSchoolFeature("AMConjuration", "Augmented Conjuration", SpellSchool.Conjuration);
                AddWizardSchoolFeature("AMDivination", "Augmented Divination", SpellSchool.Divination);
                AddWizardSchoolFeature("AMEnchantment", "Augmented Enchantment", SpellSchool.Enchantment);
                AddWizardSchoolFeature("AMEvocation", "Augmented Evocation", SpellSchool.Evocation);
                AddWizardSchoolFeature("AMIllusion", "Augmented Illusion", SpellSchool.Illusion);
                AddWizardSchoolFeature("AMNecromancy", "Augmented Necromancy", SpellSchool.Necromancy);
                AddWizardSchoolFeature("AMTransmutation", "Augmented Transmutation", SpellSchool.Transmutation);
            }

            static void AddFeaturetoSelection(BlueprintFeature feat)
            {

                var ArcaneRiderFeat = Resources.GetBlueprint<BlueprintFeatureSelection>("8e627812dc034b9db12fa396fdc9ec75");
                var BasicFeat = Resources.GetBlueprint<BlueprintFeatureSelection>("247a4068296e8be42890143f451b4b45");
                var ArcaneBloodline = Resources.GetBlueprint<BlueprintFeatureSelection>("ff4fd877b4c801342ab8e880b734a6b9");
                var InfernalBloodline = Resources.GetBlueprint<BlueprintFeatureSelection>("f19d9bfcbc1e3ea42bda754a03c40843");
                var DragonLevel2Feat = Resources.GetBlueprint<BlueprintFeatureSelection>("a21acdafc0169f5488a9bd3256e2e65b");
                var LoremasterFeat = Resources.GetBlueprint<BlueprintFeatureSelection>("689959eef3e972e458b52598dcc2c752");
                var MagusFeat = Resources.GetBlueprint<BlueprintFeatureSelection>("66befe7b24c42dd458952e3c47c93563");
                var SeekerFeat = Resources.GetBlueprint<BlueprintFeatureSelection>("c6b609279cc3174478624182ac1ad812");
                var SkaldFeat = Resources.GetBlueprint<BlueprintFeatureSelection>("0a1999535b4f77b4d89f689a385e5ec9");
                var WizardFeat = Resources.GetBlueprint<BlueprintFeatureSelection>("8c3102c2ff3b69444b139a98521a4899");


                AddtoSelection(feat, ArcaneRiderFeat);
                AddtoSelection(feat, BasicFeat);
                AddtoSelection(feat, ArcaneBloodline);
                AddtoSelection(feat, InfernalBloodline);
                AddtoSelection(feat, DragonLevel2Feat);
                AddtoSelection(feat, LoremasterFeat);
                AddtoSelection(feat, MagusFeat);
                AddtoSelection(feat, SeekerFeat);
                AddtoSelection(feat, SkaldFeat);
                AddtoSelection(feat, WizardFeat);
            }

            static void AddToMythicFeatSelection(BlueprintFeature feat)
            {
                var MythicFeat = Resources.GetBlueprint<BlueprintFeatureSelection>(MYTHIC_FEAT_SELECTION);

                AddtoSelection(feat, MythicFeat);
            }

            static void AddtoSelection(BlueprintFeature feat, BlueprintFeatureSelection selection)
            {

                selection.m_Features = selection.m_AllFeatures.AppendToArray(feat.ToReference<BlueprintFeatureReference>());
                selection.m_AllFeatures = selection.m_AllFeatures.AppendToArray(feat.ToReference<BlueprintFeatureReference>());

            }

            static BlueprintFeature AddWizardSchoolFeature(string name, string title, SpellSchool school)
            {
                var blueprint = Helpers.CreateBlueprint<BlueprintFeature>(name, bp => {
                    bp.IsClassFeature = true;
                    bp.Groups = new FeatureGroup[] {
                    FeatureGroup.WizardFeat, FeatureGroup.Feat};
                    bp.Ranks = 1;
                    bp.SetName(title);

                    bp.SetDescription(BuildDescription(school));
                    bp.m_DescriptionShort = bp.m_Description;

                    bp.AddComponent(new AddAbilityParamRule().InitializeSchool(school));
                    bp.AddComponent(new AddDamageRule().InitializeSchool(school));
                    bp.AddComponent(new AddAugmentedPenetrationRule().InitializeSchool(school));
                    bp.AddComponent(new AddBuffHandlerRule().InitializeSchool(school));
                    bp.AddComponent(Helpers.Create<RecommendationRequiresSpellbook>());
                    bp.AddComponent(Helpers.Create<FeatureTagsComponent>(c => {
                        c.FeatureTags = FeatureTag.Magic;
                    }));
                });

                if (Main.Settings.AugmentationsAreMythic)
                {
                    AddToMythicFeatSelection(blueprint);
                }
                else
                {
                    AddFeaturetoSelection(blueprint);
                }
                return blueprint;
            }

            static string BuildDescription(SpellSchool school)
            {
                string desc = school + " spells now have the following bonuses:";

                SchoolSettingsData ssd = SettingHelper.GetSchoolSettings(school);

                if(ssd.School.HasFlag(AugmentedSchoolSetting.Damage))
                {
                    desc += "\n  - Deals bonus damage equal to your caster stat bonus.";
                }
                if (ssd.School.HasFlag(AugmentedSchoolSetting.DC))
                {
                    desc += "\n  - Has bonus DC equal to 1 + caster level divided by 2.";
                }
                if (ssd.School.HasFlag(AugmentedSchoolSetting.InifiniteCast))
                {
                    desc += "\n  - May be cast inifinitely, but may still require being prepared.";
                }
                if (ssd.School.HasFlag(AugmentedSchoolSetting.OneDayBuffs))
                {
                    desc += "\n  - Buffs last 24 hours.";
                }
                if (ssd.School.HasFlag(AugmentedSchoolSetting.Penetration))
                {
                    desc += "\n  - Ignores spell resistance.";
                }

                foreach (MetamagicSetting meta in SettingHelper.MetamagicSettingOptions)
                {
                    if (ssd.Metamagic.HasFlag(meta))
                    {
                        desc += "\n  - Automatically have Metamagic " + meta;
                    }
                }

                return desc;
            }
        }
    }
}
