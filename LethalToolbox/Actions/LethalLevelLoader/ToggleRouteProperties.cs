using LethalLevelLoader;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace LethalToolbox.Actions.LethalLevelLoader
{
    public class ToggleRouteProperties : MonoBehaviour
    {
        public string extendedLevelName;
        public ExtendedLevelReference extendedLevelReference;

        public void Awake()
        {
            extendedLevelReference.GetExtendedLevel();
            if (extendedLevelReference.ExtendedLevel == null)
            {
                Debug.Log("LethalToolbox: Could Not Find ExtendedLevel: " + extendedLevelReference.extendedLevelName + ", Disabling Component!");
                this.enabled = false;
            }
        }

        public void ToggleIsRouteHidden(bool value)
        {
            if (extendedLevelReference.ExtendedLevel != null)
                extendedLevelReference.ExtendedLevel.IsRouteHidden = value;
        }

        public void ToggleIsRouteLocked(bool value)
        {
            if (extendedLevelReference.ExtendedLevel != null)
                extendedLevelReference.ExtendedLevel.IsRouteLocked = value;
        }
    }
}
