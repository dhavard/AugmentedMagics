using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.UnitLogic.Abilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AugmentedMagics.Settings
{
    public static class SettingHelper
    {
        public static IEnumerable<AugmentedSchoolSetting> AugmentedSchoolSettingOptions
            = Enum.GetValues(typeof(AugmentedSchoolSetting)).Cast<AugmentedSchoolSetting>().Where(i => i != AugmentedSchoolSetting.Default);

        public static IEnumerable<MetamagicSetting> MetamagicSettingOptions
            = Enum.GetValues(typeof(MetamagicSetting)).Cast<MetamagicSetting>().Where(i => i != MetamagicSetting.Default);


        private static Dictionary<SpellSchool, SchoolSettingsData> AugmentationSettings = null;

        public static Dictionary<SpellSchool, SchoolSettingsData> GetSchoolSettings()
        {
            if (AugmentationSettings == null)
            {
                AugmentationSettings = new Dictionary<SpellSchool, SchoolSettingsData>();

                AugmentationSettings.Add(SpellSchool.Abjuration, Main.Settings.AbjurationSettings);
                AugmentationSettings.Add(SpellSchool.Conjuration, Main.Settings.ConjurationSettings);
                AugmentationSettings.Add(SpellSchool.Divination, Main.Settings.DivinationSettings);
                AugmentationSettings.Add(SpellSchool.Enchantment, Main.Settings.EnchantmentSettings);
                AugmentationSettings.Add(SpellSchool.Evocation, Main.Settings.EvocationSettings);
                AugmentationSettings.Add(SpellSchool.Illusion, Main.Settings.IllusionSettings);
                AugmentationSettings.Add(SpellSchool.Necromancy, Main.Settings.NecromancySettings);
                AugmentationSettings.Add(SpellSchool.Transmutation, Main.Settings.TransmutationSettings);
            }

            return AugmentationSettings;
        }

        public static SchoolSettingsData GetSchoolSettings(SpellSchool school)
        {
            return GetSchoolSettings()[school];
        }

        public static bool HasMetamagic(SpellSchool school, Metamagic meta)
        {
            SchoolSettingsData ssd = GetSchoolSettings(school);
            return (meta == Metamagic.Bolstered && ssd.Metamagic.HasFlag(MetamagicSetting.Bolster))
                || (meta == Metamagic.Empower && ssd.Metamagic.HasFlag(MetamagicSetting.Empower))
                || (meta == Metamagic.Extend && ssd.Metamagic.HasFlag(MetamagicSetting.Extend))
                || (meta == Metamagic.Maximize && ssd.Metamagic.HasFlag(MetamagicSetting.Maximize))
                || (meta == Metamagic.Persistent && ssd.Metamagic.HasFlag(MetamagicSetting.Persistent))
                || (meta == Metamagic.Quicken && ssd.Metamagic.HasFlag(MetamagicSetting.Quicken))
                || (meta == Metamagic.Reach && ssd.Metamagic.HasFlag(MetamagicSetting.Reach))
                || (meta == Metamagic.Selective && ssd.Metamagic.HasFlag(MetamagicSetting.Selective));
        }
    }
}
