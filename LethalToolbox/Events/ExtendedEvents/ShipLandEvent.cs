using LethalLevelLoader;
using System;
using System.Collections.Generic;
using System.Text;
using Unity.Netcode;
using UnityEngine.Events;

namespace LethalToolbox
{
    public class ShipLandEvent : NetworkBehaviour
    {
        public UnityEvent onShipLandEvent;

        public void Awake()
        {
            LevelManager.GlobalLevelEvents.onShipLand.AddListener(OnShipLand);
        }

        public void OnShipLand()
        {
            onShipLandEvent.Invoke();
        }
    }
}
