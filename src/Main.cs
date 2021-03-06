using UnityEngine;
using System;
using System.Collections.Generic;
using HBS.Logging;
using Harmony;
using Newtonsoft.Json;
using System.Reflection;

using DarkCrew.Utils;

namespace DarkCrew {
    public class Main {
        public static ILog Logger;
        public static Settings Settings { get; private set; }
        public static Assembly DarkCrewAssembly { get; set; } 
        public static AssetBundle DarkCrewBundle { get; set; }
        public static string Path { get; private set; }

        public static void InitLogger(string modDirectory) {
            Dictionary<string, LogLevel> logLevels = new Dictionary<string, LogLevel> {
                ["DarkCrew"] = LogLevel.Debug
            };
            LogManager.Setup(modDirectory + "/output.log", logLevels);
            Logger = LogManager.GetLogger("DarkCrew");
            Path = modDirectory;
        }

        // Entry point into the mod, specified in the `mod.json`
        public static void Init(string modDirectory, string modSettings) {
            try {
                InitLogger(modDirectory);

                Logger.Log("Loading DarkCrew settings");
                Settings = JsonConvert.DeserializeObject<Settings>(modSettings);
            } catch (Exception e) {
                Logger.LogError(e);
                Logger.Log("Error loading mod settings - using defaults.");
                Settings = new Settings();
            }

            HarmonyInstance harmony = HarmonyInstance.Create("co.uk.cwolf.DarkCrew");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}