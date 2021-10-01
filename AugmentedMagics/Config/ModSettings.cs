using Newtonsoft.Json;
using System.IO;
using System.Reflection;
using static UnityModManagerNet.UnityModManager;

/***
 * Taken from https://github.com/Vek17/WrathMods-TabletopTweaks/tree/master/TabletopTweaks/Config
 * Copyright Vek17
 * Licensed as MIT
 ***/
namespace AugmentedMagics.Config {
    static class ModSettings {
        public static ModEntry ModEntry;
       // public static Fixes Fixes;
        //public static AddedContent AddedContent;
        public static Blueprints Blueprints;
        public static void LoadAllSettings() {
          //  LoadSettings("Fixes.json", ref Fixes);
          //  LoadSettings("AddedContent.json", ref AddedContent);
            LoadSettings("Blueprints.json", ref Blueprints);
        }
        private static void LoadSettings<T>(string fileName, ref T setting) where T : IUpdatableSettings {
            var assembly = Assembly.GetExecutingAssembly();
            string userConfigFolder = ModEntry.Path + "UserSettings";
            Directory.CreateDirectory(userConfigFolder);
            var resourcePath = $"AugmentedMagics.Config.{fileName}";
            var userPath = $"{userConfigFolder}{Path.DirectorySeparatorChar}{fileName}";


    
            using (Stream stream = assembly.GetManifestResourceStream(resourcePath))
            using (StreamReader reader = new StreamReader(stream)) {
                setting = JsonConvert.DeserializeObject<T>(reader.ReadToEnd());
            }
            if (File.Exists(userPath)) {
                using (StreamReader reader = File.OpenText(userPath)) {
                    try {
                        T userSettings = JsonConvert.DeserializeObject<T>(reader.ReadToEnd());
                        setting.OverrideSettings(userSettings);
                    } catch {
                        AugmentedMagics.Main.Error("Failed to load user settings. Settings will be rebuilt.");
                        try { File.Copy(userPath, userConfigFolder + $"{Path.DirectorySeparatorChar}BROKEN_{fileName}", true); } catch { AugmentedMagics.Main.Error("Failed to archive broken settings."); }
                    }
                }
            }
            File.WriteAllText(userPath, JsonConvert.SerializeObject(setting, Formatting.Indented));
        }
    }
}
