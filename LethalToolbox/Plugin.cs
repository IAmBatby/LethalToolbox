using BepInEx;
using BepInEx.Configuration;
using DunGen;
using HarmonyLib;
using LethalToolbox;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Permissions;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.SceneManagement;

namespace LethalToolkit
{
    [BepInPlugin(ModGUID, ModName, ModVersion)]
    [BepInDependency(LethalLevelLoader.Plugin.ModGUID)]
    internal class Plugin : BaseUnityPlugin
    {
        public const string ModGUID = "imabatby.lethaltoolbox";
        public const string ModName = "LethalToolbox";
        public const string ModVersion = "1.0.4";

        public static Plugin Instance;

        internal static readonly Harmony Harmony = new Harmony(ModGUID);

        internal static BepInEx.Logging.ManualLogSource logger;


        private void Awake()
        {
            if (Instance == null)
                Instance = this;

            logger = Logger;

            Logger.LogInfo($"LethalToolbox loaded!!");

            NetcodePatch();

            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "SampleSceneRelay")
            {
                Data.GetPlayerObjects();
            }
        }

        private void NetcodePatch()
        {
            try
            {
                var types = Assembly.GetExecutingAssembly().GetTypes();
                foreach (var type in types)
                {
                    var methods = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
                    foreach (var method in methods)
                    {
                        var attributes = method.GetCustomAttributes(typeof(RuntimeInitializeOnLoadMethodAttribute), false);
                        if (attributes.Length > 0)
                        {
                            method.Invoke(null, null);
                        }
                    }
                }
            }
            catch
            {
                Debug.Log("NetcodePatcher did a big fucksie wuckise!");
            }
        }
    }
}