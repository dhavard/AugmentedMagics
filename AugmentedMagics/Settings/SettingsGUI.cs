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

        public static void Draw()
        {
            DrawFeatureToggles();
            GUILayout.Space(20);

            GUILayout.BeginHorizontal();
            GUILayout.Label("These features should take effect immediately, but may require restart to have description text update.");
            GUILayout.EndHorizontal();
            GUILayout.Space(10);

            Dictionary<SpellSchool, SchoolSettingsData> AugmentationSettings = SettingHelper.GetSchoolSettings();
            foreach (SpellSchool school in AugmentationSettings.Keys)
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
            GUILayout.Label("These features may require restarting the game to take effect.");
            GUILayout.EndHorizontal();
            GUILayout.Space(10);

            GUILayout.BeginHorizontal();
            Main.Settings.StrongerSpellFocus = GUILayout.Toggle(Main.Settings.StrongerSpellFocus, " Enables increasing Spell Focus, Greater, and Mythic feats DC bonus");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            Main.Settings.StrongerSpellPenetration = GUILayout.Toggle(Main.Settings.StrongerSpellPenetration, " Enables increasing Spell Penetration and Greater feats SR penetration");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            Main.Settings.AugmentedMagicSchoolFeats = GUILayout.Toggle(Main.Settings.AugmentedMagicSchoolFeats, " Augmented Magic active.");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            Main.Settings.AugmentationsAreMythic = GUILayout.Toggle(Main.Settings.AugmentationsAreMythic, " Augmented Magic feats are mythic (turn off to conver to Wizard feats).");
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
            foreach (AugmentedSchoolSetting flag in SettingHelper.AugmentedSchoolSettingOptions.OrderBy(i => i.ToString()))
            {
                GUILayout.BeginHorizontal();
                GUILayout.Space(10);

                string label = $" {flag}";
                if (flag == AugmentedSchoolSetting.DC)
                {
                    label = " DC by caster level";
                }
                switch(flag)
                {
                    case AugmentedSchoolSetting.DC:
                        label = " DC by caster level";
                        break;
                    case AugmentedSchoolSetting.DCbyStats:
                        label = " DC by caster stat bonus";
                        break;
                    case AugmentedSchoolSetting.InifiniteCast:
                        label = " Infinite usage";
                        break;
                    case AugmentedSchoolSetting.OneDayBuffs:
                        label = " 24 hour buffs";
                        break;
                    case AugmentedSchoolSetting.Penetration:
                        label = " Ignore Spell Resistance";
                        break;

                }
                if (GUILayout.Toggle(AugmentationSettings[school].School.HasFlag(flag), label))
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
            foreach (MetamagicSetting flag in SettingHelper.MetamagicSettingOptions)
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
