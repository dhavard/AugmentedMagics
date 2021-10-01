
/***
 * Taken from https://github.com/Vek17/WrathMods-TabletopTweaks/tree/master/TabletopTweaks/Config
 * Copyright Vek17
 * Licensed as MIT
 ***/
namespace AugmentedMagics.Config {
    public interface IUpdatableSettings {
        void OverrideSettings(IUpdatableSettings userSettings);
    }
}
