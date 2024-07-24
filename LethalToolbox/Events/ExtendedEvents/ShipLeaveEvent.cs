using LethalLevelLoader;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using UnityEngine.Events;

namespace LethalToolbox
{
    public class ShipLeaveEvent
    {
        public UnityEvent onShipLeaveEvent;

        public void Awake()
        {
            LevelManager.GlobalLevelEvents.onShipLeave.AddListener(OnShipLeave);
        }

        public void OnShipLeave()
        {
            onShipLeaveEvent.Invoke();
        }

        
    }
}
