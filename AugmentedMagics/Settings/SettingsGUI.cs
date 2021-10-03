using Kingmaker.Blueprints.Classes.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AugmentedMagics.Settings
{
    public static class SettingsGUI
    {
        private static readonly Dictionary<string, bool> statedict = new Dictionary<string, bool>();

        public static IEnumerable<AugmentedSchoolSetting> AugmentedSchoolSettingOptions
            = Enum.GetValues(typeof(AugmentedSchoolSetting)).Cast<AugmentedSchoolSetting>().Where(i => i != AugmentedSchoolSetting.Default);

        public static IEnumerable<MetamagicSetting> MetamagicSettingOptions
            = Enum.GetValues(typeof(MetamagicSetting)).Cast<MetamagicSetting>().Where(i => i != MetamagicSetting.Default);

        public static void Draw()
        {
            DrawFeatureToggles();
            GUILayout.Space(10);

            Dictionary<SpellSchool, SchoolSettingsData> AugmentationSettings = new Dictionary<SpellSchool, SchoolSettingsData>();
            AugmentationSettings.Add(SpellSchool.Abjuration, Main.Settings.AbjurationSettings);
            AugmentationSettings.Add(SpellSchool.Conjuration, Main.Settings.ConjurationSettings);
            AugmentationSettings.Add(SpellSchool.Divination, Main.Settings.DivinationSettings);
            AugmentationSettings.Add(SpellSchool.Enchantment, Main.Settings.EnchantmentSettings);
            AugmentationSettings.Add(SpellSchool.Evocation, Main.Settings.EvocationSettings);
            AugmentationSettings.Add(SpellSchool.Illusion, Main.Settings.IllusionSettings);
            AugmentationSettings.Add(SpellSchool.Necromancy, Main.Settings.NecromancySettings);
            AugmentationSettings.Add(SpellSchool.Transmutation, Main.Settings.TransmutationSettings);

            foreach(SpellSchool school in AugmentationSettings.Keys)
            {
                DrawAugmentedOptions(school, AugmentationSettings);
                GUILayout.Space(10);
            }

        }

        private static bool DrawButton(string label, bool isShown = true)
        {
            GUIStyle style = new GUIStyle(GUI.skin.GetStyle("Label"));
            style.normal.textColor = Color.white;

            if (!statedict.ContainsKey(label))
            {
                statedict[label] = isShown;
            }

            string icon = statedict[label] ? "-" : "+";

            statedict[label] = GUILayout.Button($"{icon} {label}", style) ? !statedict[label] : statedict[label];
            return statedict[label];
        }

        private static void DrawFeatureToggles()
        {
            GUILayout.BeginHorizontal();
            bool shown = DrawButton("Features", true);
            GUILayout.EndHorizontal();

            if (!shown) return;

            GUILayout.BeginHorizontal();
            Main.Settings.StrongerSpellFocus = GUILayout.Toggle(Main.Settings.StrongerSpellFocus, " Enables increasing Spell Focus, Greater, and Mythic feats DC bonus");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            Main.Settings.StrongerSpellPenetration = GUILayout.Toggle(Main.Settings.StrongerSpellPenetration, " Enables increasing Spell Penetration and Greater feats SR penetration");
            GUILayout.EndHorizontal();
        }

        private static void DrawAugmentedOptions(SpellSchool school, Dictionary<SpellSchool, SchoolSettingsData> AugmentationSettings)
        {
            GUILayout.BeginHorizontal();
            bool shown = DrawButton(school.ToString(), false);
            GUILayout.EndHorizontal();

            if (!shown) return;
            
            DrawAugmentedSchoolOptions(school, AugmentationSettings);
            GUILayout.Space(10);
            DrawAugmentedMetamagicOptions(school, AugmentationSettings);
        }

        private static void DrawAugmentedSchoolOptions(SpellSchool school, Dictionary<SpellSchool, SchoolSettingsData> AugmentationSettings)
        {
            AugmentedSchoolSetting opts = default;
            foreach (AugmentedSchoolSetting flag in AugmentedSchoolSettingOptions)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Space(10);

                if (GUILayout.Toggle(AugmentationSettings[school].School.HasFlag(flag), $" {flag}"))
                {
                    opts |= flag;
                }

                GUILayout.EndHorizontal();
            }
            AugmentationSettings[school].School = opts;
            GUILayout.Space(5);
        }

        private static void DrawAugmentedMetamagicOptions(SpellSchool school, Dictionary<SpellSchool, SchoolSettingsData> AugmentationSettings)
        {
            GUILayout.BeginHorizontal();
            bool shown = DrawButton(school.ToString() + " Metamagic", false);
            GUILayout.EndHorizontal();

            if (!shown) return;

            MetamagicSetting opts = default;
            foreach (MetamagicSetting flag in AugmentedSchoolSettingOptions)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Space(10);

                if (GUILayout.Toggle(AugmentationSettings[school].Metamagic.HasFlag(flag), $" {flag}"))
                {
                    opts |= flag;
                }

                GUILayout.EndHorizontal();
            }
            AugmentationSettings[school].Metamagic = opts;
            GUILayout.Space(5);
        }
    }
}
