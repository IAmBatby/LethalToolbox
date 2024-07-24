using LethalLevelLoader;
using System;
using System.Collections.Generic;
using System.Text;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

namespace LethalToolbox
{
    public class PowerSwitchToggleEvent : NetworkBehaviour
    {
        public UnityEvent onPowerSwitchToggleOn;
        public UnityEvent onPowerSwitchToggleOff;

        public void Awake()
        {
            LevelManager.GlobalLevelEvents.onPowerSwitchToggle.AddListener(OnPowerSwitchToggle);
        }

        public void OnPowerSwitchToggle(bool result)
        {
            if (result == true)
                onPowerSwitchToggleOn.Invoke();
            else
                onPowerSwitchToggleOff.Invoke();
        }
    }
}
