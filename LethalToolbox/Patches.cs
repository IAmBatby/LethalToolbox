using GameNetcodeStuff;
using HarmonyLib;
using LethalToolbox.Sources;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace LethalToolbox
{
    internal static class Patches
    {
        [HarmonyPatch(typeof(PlayerControllerB), nameof(PlayerControllerB.Awake))]
        [HarmonyPrefix]
        internal static void TryCachePlayerComponent(PlayerControllerB __instance)
        {
            CachedComponents.TryCacheComponentAll(__instance);
        }


        [HarmonyPatch(typeof(EnemyAI), nameof(EnemyAI.Start))]
        [HarmonyPrefix]
        internal static void TryCacheEnemyComponent(EnemyAI __instance)
        {
            CachedComponents.TryCacheComponentAll(__instance);
        }

        [HarmonyPatch(typeof(GrabbableObject), nameof(GrabbableObject.Start))]
        [HarmonyPrefix]
        internal static void TryCacheItemComponent(GrabbableObject __instance)
        {
            CachedComponents.TryCacheComponentAll(__instance);
        }
    }
}
