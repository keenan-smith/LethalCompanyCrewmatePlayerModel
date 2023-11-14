using GameNetcodeStuff;
using HarmonyLib;

namespace AmogusModels.Patches
{
    [HarmonyPatch]
    internal class PlayerObjects
    {
        [HarmonyPatch(typeof(PlayerControllerB), "Start")]
        [HarmonyPostfix]
        public static void InitModels(ref PlayerControllerB __instance)
        {
            if (__instance.gameObject.name != "Player")
            {
                __instance.gameObject.AddComponent<AmogusController>();
            }
        }
    }
}
