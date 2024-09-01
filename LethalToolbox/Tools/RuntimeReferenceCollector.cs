using DunGen.Graph;
using LethalLevelLoader;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

namespace LethalToolbox
{
    [System.Serializable]
    public class UnityEventExtendedContent : UnityEvent<ExtendedContent>
    {

    }
    public class RuntimeReferenceCollector : MonoBehaviour
    {
        public ScriptableObject contentToFind;

        [HideInInspector] ExtendedContent foundContent;

        public UnityEventExtendedContent onContentFound;

        public void Awake()
        {
            if (contentToFind == null) return;

            foundContent = TryFindContent(contentToFind);

            if (foundContent != null)
                onContentFound.Invoke(foundContent);
        }

        public ExtendedContent TryFindContent(ScriptableObject referenceContent)
        {
            ExtendedContent returnContent = null;
            if (referenceContent is SelectableLevel selectableLevel)
            {
                if (PatchedContent.TryGetExtendedContent(selectableLevel, out ExtendedLevel foundExtendedLevel))
                    returnContent = foundExtendedLevel;
            }
            else if (referenceContent is DungeonFlow dungeonFlow)
            {
                if (PatchedContent.TryGetExtendedContent(dungeonFlow, out ExtendedDungeonFlow foundExtendedDungeon))
                    returnContent = foundExtendedDungeon;
            }
            else if (referenceContent is Item item)
            {
                if (PatchedContent.TryGetExtendedContent(item, out ExtendedItem foundExtendedItem))
                    returnContent = foundExtendedItem;
            }
            else if (referenceContent is EnemyType enemyType)
            {
                if (PatchedContent.TryGetExtendedContent(enemyType, out ExtendedEnemyType foundExtendedEnemyType))
                    returnContent = foundExtendedEnemyType;
            }

            return (returnContent);
        }
    }
}
