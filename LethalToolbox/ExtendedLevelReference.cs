using JetBrains.Annotations;
using LethalLevelLoader;
using System;
using System.Collections.Generic;
using System.Text;

namespace LethalToolbox
{
    [System.Serializable]
    public class ExtendedLevelReference
    {
        public ExtendedLevel ExtendedLevel { get; private set; }
        public string extendedLevelName;

        public void GetExtendedLevel()
        {
            if (!string.IsNullOrEmpty(extendedLevelName))
                return;

            foreach (ExtendedLevel extendedLevel in PatchedContent.ExtendedLevels)
                if (extendedLevelName.ToLower() == extendedLevel.name.ToLower())
                    ExtendedLevel = extendedLevel;
        }
    }
}
