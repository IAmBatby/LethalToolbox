using LethalLevelLoader;
using System;
using System.Collections.Generic;
using System.Text;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

namespace LethalToolbox
{
    public class TimeOfDayEvent : NetworkBehaviour
    {
        public DayMode filteredDayMode = DayMode.None;
        public LevelWeatherType filteredWeatherType = LevelWeatherType.None;
        public UnityEvent onTimeOfDayChanged;

        public void Awake()
        {
            LevelManager.GlobalLevelEvents.onDayModeToggle.AddListener(OnDayModeToggle);
        }

        public void OnDayModeToggle(DayMode dayMode)
        {
            if (dayMode == filteredDayMode && LevelManager.CurrentExtendedLevel.SelectableLevel.currentWeather == filteredWeatherType)
                onTimeOfDayChanged.Invoke();
        }
    }
}
