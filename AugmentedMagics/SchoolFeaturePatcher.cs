using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.JsonSystem;
using AugmentedMagics.Extensions;
using AugmentedMagics.Utilities;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes.Spells;

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

            static string SPELL_PEN = "ee7dc126939e4d9438357fbd5980d459";
            static string SPELL_PEN_GREATER = "1978c3f91cfbbc24b9c9b0d017f4beec";
            static string SPELL_PEN_MYTHIC = "51b6b22ff184eef46a675449e837365d";

            //Greater spell focus facts
            static string ABJURATION = "f4c39693ebaffd244bbe4694e3c3ce0d"; //PrerequisiteParametrizedFeature  //PrerequisiteFeature
            static string CONJURATION = "d1e78eeeb5295e048be5b2500c002233";
            static string DIVINATION = "1deb774e8ba7ec64c83cd26c2d09f019";
            static string ENCHANTMENT = "f2ae8d44d24996c449e69b6f5248507d";
            static string EVOCATION = "f2970e8a73baeae469407638f7e75f50";
            static string ILLUSION = "b6dff9186edca3141ba0ab4376d1347d";
            static string NECROMANCY = "7f7a73444f15b8644a8cf0f2f149c6aa";
            static string TRANSMUTATION = "74fd23356fc4c9847986d48f1a40bc7a";

            //augment summoning
            static string AUG_SUMMON = "38155ca9e4055bb48a89240a2055dcc3";
            /*
               [AugmentedMagics] PPF is parameter spell Conjuration
               [AugmentedMagics] PPF feature name Spell Focus guid 16fa59cc9a72a6043b566b49184f53fe
            */

            [HarmonyPriority(Priority.LowerThanNormal)]
            static void Postfix()
            {
                if (Initialized) return;
                Initialized = true;
                AddFeatures();
                AddParentPrereqs();
            }

            static void AddParentPrereqs()
            {
                /*
                BlueprintFeature augsum = Resources.GetBlueprint<BlueprintFeature>(AUG_SUMMON);
                if( augsum != null )
                {
                    foreach(BlueprintComponent bf in augsum.GetComponents<BlueprintComponent>())
                    { 
                        if(bf is PrerequisiteParametrizedFeature)
                        {
                            PrerequisiteParametrizedFeature ppf = (PrerequisiteParametrizedFeature)bf;
                            if (ppf.IsParameterSpellSchool)
                            {
                                Main.Log("PPF is parameter spell " + ppf.SpellSchool);
                            }
                            BlueprintFeature reff = ppf.Feature;
                            Main.Log("PPF feature name " + reff.Name + " guid " + reff.AssetGuid);
                        }
                    }
                }
                */

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
                AddWizardFeature("AMAbjuration", "Augmented Abjuration", "Abjurations you cast now last 24 hours and ignore spell resistance.", new AddAbjurationRule(), SpellSchool.Abjuration);
                AddWizardFeature("AMConjuration", "Augmented Conjuration", "Conjuration spells now automatically have the Selective Metamagic and have a higher DC based on your Constitution bonus.", new AddConjurationRule(), SpellSchool.Conjuration);
                AddWizardFeature("AMDivination", "Augmented Divination", "Divination spells you cast ignore spell resistance. Divination spells are not consumed when cast, but still must be prepared if required.", new AddDivinationRule(), SpellSchool.Divination);
                AddWizardFeature("AMEnchantment", "Augmented Enchantment", "Your enchantments now have a higher DC based on your Charisma bonus and ignore spell resistance.", new AddEnchantmentRule(), SpellSchool.Enchantment);
                AddWizardFeature("AMEvocation", "Augmented Evocation", "Evocation spells you cast now deal bonus damage equal to your caster stat bonus. For spells with multiple missles per cast, each subsequent missile recieves 1 less bonus damage than the previous missile.", new AddEvocationRule());
                AddWizardFeature("AMIllusion", "Augmented Illusion", "Your illusions now have a higher DC based on your Intelligence bonus and ignore spell resistance.", new AddIllusionRule(), SpellSchool.Illusion);
                AddWizardFeature("AMNecromancy", "Augmented Necromancy", "Your Necromancy spells now have a higher DC based on your Strength bonus and ignore spell resistance.", new AddNecromancyRule(), SpellSchool.Necromancy);
                AddWizardFeature("AMTransmutation", "Augmented Transmutation", "Transmutation spells you cast ignore spell resistance. Transmutation spells now automatically have the Extend Metamagic and have a higher DC.", new AddTransmutationRule(), SpellSchool.Transmutation);
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
                var WizardFeat = Resources.GetBlueprint<BlueprintFeatureSelection>("8e627812dc034b9db12fa396fdc9ec75");


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

            static void AddtoSelection(BlueprintFeature feat, BlueprintFeatureSelection selection)
            {

                selection.m_Features = selection.m_AllFeatures.AppendToArray(feat.ToReference<BlueprintFeatureReference>());
                selection.m_AllFeatures = selection.m_AllFeatures.AppendToArray(feat.ToReference<BlueprintFeatureReference>());

            }

            static BlueprintFeature AddWizardFeature(string name, string title, string desc, BlueprintComponent component)
            {
                var blueprint = Helpers.CreateBlueprint<BlueprintFeature>(name, bp => {
                    bp.IsClassFeature = true;
                    bp.Groups = new FeatureGroup[] {
                    FeatureGroup.WizardFeat, FeatureGroup.Feat};
                    bp.Ranks = 1;
                    bp.SetName(title);
                    bp.SetDescription(desc);
                    bp.m_DescriptionShort = bp.m_Description;
                    bp.AddComponent(component);
                });
                AddFeaturetoSelection(blueprint);
                return blueprint;
            }

            static BlueprintFeature AddWizardFeature(string name, string title, string desc, BlueprintComponent component, SpellSchool school)
            {
                var blueprint = Helpers.CreateBlueprint<BlueprintFeature>(name, bp => {
                    bp.IsClassFeature = true;
                    bp.Groups = new FeatureGroup[] {
                    FeatureGroup.WizardFeat, FeatureGroup.Feat};
                    bp.Ranks = 1;
                    bp.SetName(title);
                    bp.SetDescription(desc);
                    bp.m_DescriptionShort = bp.m_Description;
                    bp.AddComponent(component);
                    bp.AddComponent(new AddAugmentedPenetrationRule().InitializeSchool(school));
                });
                AddFeaturetoSelection(blueprint);
                return blueprint;
            }
        }
    }
}
