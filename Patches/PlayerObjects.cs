using GameNetcodeStuff;
using HarmonyLib;
using System.Diagnostics;
using System.Numerics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace AmogusModels.Patches
{
    [HarmonyPatch]
    internal class PlayerObjects
    {
        public static void InitModels()
        {
            var localPlayer = GameNetworkManager.Instance.localPlayerController;
            Debug.Log(localPlayer.gameObject.name);
            var players = UnityEngine.Object.FindObjectsOfType<PlayerControllerB>();
            foreach (var player in players)
            {
                if (player == localPlayer) continue;
                player.gameObject.AddComponent<AmogusController>();
            }
        }

        [HarmonyPatch(typeof(PlayerControllerB), "SpawnPlayerAnimation")]
        [HarmonyPostfix]
        public static void InitModel(ref PlayerControllerB __instance)
        {
            InitModels();
        }

        [HarmonyPatch(typeof(PlayerControllerB), "DisablePlayerModel")]
        [HarmonyPostfix] 
        public static void DisablePlayerModel(ref PlayerControllerB __instance, GameObject playerObject)
        {
            var localPlayer = GameNetworkManager.Instance.localPlayerController;
            if (playerObject == localPlayer) return;
            playerObject.gameObject.GetComponentInChildren<LODGroup>().enabled = false;
            var meshes = playerObject.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
            foreach (var LODmesh in meshes)
            {
                if (LODmesh.name == "Body") continue;
                LODmesh.enabled = false;
            }
        }
    }
}
