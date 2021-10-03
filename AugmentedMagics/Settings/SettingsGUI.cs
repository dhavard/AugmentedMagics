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

        public static void Draw()
        {
            DrawFeatureToggles();
            GUILayout.Space(10);

            Dictionary<SpellSchool, AugmentedSchoolSetting> AugmentationSettings = new Dictionary<SpellSchool, AugmentedSchoolSetting>();
            AugmentationSettings.Add(SpellSchool.Abjuration, Main.Settings.AbjurationSettings);
            AugmentationSettings.Add(SpellSchool.Conjuration, Main.Settings.ConjurationSettings);
            AugmentationSettings.Add(SpellSchool.Divination, Main.Settings.DivinationSettings);
            AugmentationSettings.Add(SpellSchool.Enchantment, Main.Settings.EnchantmentSettings);
            AugmentationSettings.Add(SpellSchool.Evocation, Main.Settings.EvocationSettings);
            AugmentationSettings.Add(SpellSchool.Illusion, Main.Settings.IllusionSettings);
            AugmentationSettings.Add(SpellSchool.Necromancy, Main.Settings.NecromancySettings);
            AugmentationSettings.Add(SpellSchool.Transmutation, Main.Settings.TransmutationSettings);

            DrawAugmentedOptions(SpellSchool.Abjuration, AugmentationSettings);
            GUILayout.Space(10);

            DrawAugmentedOptions(SpellSchool.Conjuration, AugmentationSettings);
            GUILayout.Space(10);

            DrawAugmentedOptions(SpellSchool.Divination, AugmentationSettings);
            GUILayout.Space(10);

            DrawAugmentedOptions(SpellSchool.Enchantment, AugmentationSettings);
            GUILayout.Space(10);

            DrawAugmentedOptions(SpellSchool.Evocation, AugmentationSettings);
            GUILayout.Space(10);

            DrawAugmentedOptions(SpellSchool.Illusion, AugmentationSettings);
            GUILayout.Space(10);

            DrawAugmentedOptions(SpellSchool.Necromancy, AugmentationSettings);
            GUILayout.Space(10);

            DrawAugmentedOptions(SpellSchool.Transmutation, AugmentationSettings);
            GUILayout.Space(10);
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

        private static void DrawAugmentedOptions(SpellSchool school, Dictionary<SpellSchool, AugmentedSchoolSetting> AugmentationSettings)
        {
            bool shown = DrawButton(school.ToString(), false);
            GUILayout.EndHorizontal();

            if (!shown) return;

            AugmentedSchoolSetting opts = default;

            foreach (AugmentedSchoolSetting flag in AugmentedSchoolSettingOptions)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Space(10);

                if (GUILayout.Toggle(AugmentationSettings[school].HasFlag(flag), $" {flag}"))
                {
                    opts |= flag;
                }

                GUILayout.EndHorizontal();
            }

            AugmentationSettings[school] = opts;

            GUILayout.Space(5);
        }
    }
}
