using System;
using UnityModManagerNet;

namespace AugmentedMagics.Settings
{
    [Flags]
    public enum AugmentedSchoolSetting
    {
        Damage = 1 << 0,
        DC = 1 << 1,
        Penetration = 1 << 2,

        Default = DC | Penetration
    }

    [Flags]
    public enum MetamagicSetting
    {
        Bolster = 1 << 0,
        Empower = 1 << 1,
        Extend = 1 << 2,
        Maximize = 1 << 3,
        Persistent = 1 << 4,
        Quicken = 1 << 5,
        Reach = 1 << 6,
        Selective = 1 << 7,

        Default = 0 //nothing
    }

    public class SchoolSettingsData
    {
        public AugmentedSchoolSetting School = AugmentedSchoolSetting.Default;
        public MetamagicSetting Metamagic = MetamagicSetting.Default;
    }

    public class SettingsData : UnityModManager.ModSettings
    {
        public bool StrongerSpellFocus = true;
        public bool StrongerSpellPenetration = true;
        public bool AugmentedMagicSchoolFeats = true;

        public SchoolSettingsData AbjurationSettings = new SchoolSettingsData();
        public SchoolSettingsData ConjurationSettings = new SchoolSettingsData();
        public SchoolSettingsData DivinationSettings = new SchoolSettingsData();
        public SchoolSettingsData EnchantmentSettings = new SchoolSettingsData();
        public SchoolSettingsData EvocationSettings = new SchoolSettingsData();
        public SchoolSettingsData IllusionSettings = new SchoolSettingsData();
        public SchoolSettingsData NecromancySettings = new SchoolSettingsData();
        public SchoolSettingsData TransmutationSettings = new SchoolSettingsData();

        public override void Save(UnityModManager.ModEntry modEntry)
        {
            Save(this, modEntry);
        }
    }
}
