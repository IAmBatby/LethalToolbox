using LethalLevelLoader;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace LethalToolbox
{
    public class DynamicSpawnSyncedObject : SpawnSyncedObject
    {
        private void Awake()
        {
            if (spawnPrefab == null)
                gameObject.SetActive(false);
        }

        public void AwakeWithReference(ExtendedContent extendedContent)
        {
            if (extendedContent is ExtendedItem extendedItem)
                spawnPrefab = extendedItem.Item.spawnPrefab;
            else if (extendedContent is ExtendedEnemyType extendedEnemyType)
                spawnPrefab = extendedEnemyType.EnemyType.enemyPrefab;

            if (spawnPrefab != null)
                gameObject.SetActive(true);
        }
    }
}
