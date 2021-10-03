using Kingmaker.Blueprints.Classes.Spells;
using System;
using System.Collections.Generic;
using UnityEngine;
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


    public class SettingsData : UnityModManager.ModSettings
    {

        public bool StrongerSpellFocus = true;
        public bool StrongerSpellPenetration = true;
        public bool AugmentedMagicSchoolFeats = true;

        public AugmentedSchoolSetting AbjurationSettings = AugmentedSchoolSetting.Default;
        public AugmentedSchoolSetting ConjurationSettings = AugmentedSchoolSetting.Default;
        public AugmentedSchoolSetting DivinationSettings = AugmentedSchoolSetting.Default;
        public AugmentedSchoolSetting EnchantmentSettings = AugmentedSchoolSetting.Default;
        public AugmentedSchoolSetting EvocationSettings = AugmentedSchoolSetting.Default;
        public AugmentedSchoolSetting IllusionSettings = AugmentedSchoolSetting.Default;
        public AugmentedSchoolSetting NecromancySettings = AugmentedSchoolSetting.Default;
        public AugmentedSchoolSetting TransmutationSettings = AugmentedSchoolSetting.Default;

        public override void Save(UnityModManager.ModEntry modEntry)
        {
            Save(this, modEntry);
        }
    }
}
