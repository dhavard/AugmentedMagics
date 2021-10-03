using System;
using System.Reflection;
using HarmonyLib;
using JetBrains.Annotations;
using Kingmaker.Blueprints.JsonSystem;
using AugmentedMagics.Config;
using UnityModManagerNet;
using Kingmaker.Localization;
using AugmentedMagics.Settings;
using UnityEngine;

namespace AugmentedMagics
{

	public class Main
	{
        public static SettingsData Settings;

        private static bool Load(UnityModManager.ModEntry modEntry)
		{
            var harmony = new Harmony(modEntry.Info.Id);
            
            ModSettings.ModEntry = modEntry;
            Log("Loading mod.");

            ModSettings.LoadAllSettings();
            Settings = SettingsData.Load<SettingsData>(modEntry);

            modEntry.OnGUI = new Action<UnityModManager.ModEntry>(Main.OnGUI);
			modEntry.OnSaveGUI = new Action<UnityModManager.ModEntry>(Main.OnSaveGUI);
            
            harmony.PatchAll(Assembly.GetExecutingAssembly());
            Log("Loaded mod.");
            return true;
		}

		private static void OnSaveGUI(UnityModManager.ModEntry modEntry)
		{
            Settings.Save(modEntry);
        }

        private static void OnGUI(UnityModManager.ModEntry modEntry)
        {
            GUILayout.Space(4);
            SettingsGUI.Draw();
            GUILayout.Space(4);
        }


        public static void Log(string msg)
        {
            ModSettings.ModEntry.Logger.Log(msg);
        }
        [System.Diagnostics.Conditional("DEBUG")]
        public static void LogDebug(string msg)
        {
            ModSettings.ModEntry.Logger.Log(msg);
        }
        public static void LogPatch(string action, [NotNull] IScriptableObjectWithAssetId bp)
        {
            Log($"{action}: {bp.AssetGuid} - {bp.name}");
        }
        public static void LogHeader(string msg)
        {
            Log($"--{msg.ToUpper()}--");
        }
        public static Exception Error(String message)
        {
            Log(message);
            return new InvalidOperationException(message);
        }

        public static LocalizedString MakeLocalizedString(string key, string value)
        {
            LocalizationManager.CurrentPack.Strings[key] = value;
            LocalizedString localizedString = new LocalizedString();
            typeof(LocalizedString).GetField("m_Key", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(localizedString, key);
            return localizedString;
        }

    }
}
