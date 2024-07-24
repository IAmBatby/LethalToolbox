using GameNetcodeStuff;
using LethalLevelLoader;
using System;
using System.Collections.Generic;
using System.Text;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

namespace LethalToolbox
{
    public class PlayerExitDungeonEvent : NetworkBehaviour
    {
        public UnityEvent<PlayerControllerB> onPlayerExitDungeonEvent;
        List<EntranceTeleport> whitelistedEntranceTeleports = new List<EntranceTeleport>();

        public void Awake()
        {
            LevelManager.GlobalLevelEvents.onPlayerExitDungeon.AddListener(OnPlayerExitDungeon);
        }

        public void OnPlayerExitDungeon((EntranceTeleport, PlayerControllerB) teleporterAndPlayer)
        {
            if (whitelistedEntranceTeleports.Contains(teleporterAndPlayer.Item1))
                onPlayerExitDungeonEvent.Invoke(teleporterAndPlayer.Item2);
        }
    }
}
