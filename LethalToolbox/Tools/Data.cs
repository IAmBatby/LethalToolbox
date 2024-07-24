using GameNetcodeStuff;
using LethalLevelLoader;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace LethalToolbox
{
    internal static class Data
    {
        public static List<GameObject> playerRootObjectsList = new List<GameObject>();
        public static List<GameObject> playerAllObjectsList = new List<GameObject>();


        public static void GetPlayerObjects()
        {
            foreach (PlayerControllerB player in UnityEngine.Object.FindObjectsOfType<PlayerControllerB>())
            {
                playerRootObjectsList.Add(player.gameObject);
                playerAllObjectsList.Add(player.gameObject);

                foreach (Transform child in player.GetComponentsInChildren<Transform>())
                    if (!playerAllObjectsList.Contains(child.gameObject))
                        playerAllObjectsList.Add(child.gameObject);
            }
        }

        public static bool TryFindPlayer(GameObject child, out GameObject playerObject)
        {
            Transform previousParent = child.transform.parent;
            playerObject = null;

            while (previousParent != null)
            {
                foreach (GameObject playerRootObject in playerRootObjectsList)
                    if (previousParent.gameObject == playerRootObject)
                    {
                        playerObject = playerRootObject;
                        return (true);
                    }
                previousParent = previousParent.parent;
            }
            return (false);
        }
    }
}
