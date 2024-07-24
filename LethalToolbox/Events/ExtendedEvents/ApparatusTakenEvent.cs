using LethalLevelLoader;
using System;
using System.Collections.Generic;
using System.Text;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

namespace LethalToolbox
{
    public class ApparatusTakenEvent : NetworkBehaviour
    {
        public UnityEvent onApparatusTakenEvent;

        public void Awake()
        {
            LevelManager.GlobalLevelEvents.onApparatusTaken.AddListener(OnApparatusTakenEvent);
        }

        public void OnApparatusTakenEvent(LungProp lungProp)
        {
            onApparatusTakenEvent.Invoke();
        }
    }
}
