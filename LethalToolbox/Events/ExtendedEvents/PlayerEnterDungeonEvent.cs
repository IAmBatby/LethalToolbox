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
    public class PlayerEnterDungeonEvent : NetworkBehaviour
    {
        public UnityEvent<PlayerControllerB> onPlayerEnterDungeonEvent;
        List<EntranceTeleport> whitelistedEntranceTeleports = new List<EntranceTeleport>();

        public void Awake()
        {
            LevelManager.GlobalLevelEvents.onPlayerEnterDungeon.AddListener(OnPlayerEnterDungeon);
        }

        public void OnPlayerEnterDungeon((EntranceTeleport, PlayerControllerB) teleporterAndPlayer)
        {
            if (whitelistedEntranceTeleports.Contains(teleporterAndPlayer.Item1))
            onPlayerEnterDungeonEvent.Invoke(teleporterAndPlayer.Item2);
        }
    }
}
