using GameNetcodeStuff;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace LethalToolbox.Sources.CollisionSources
{
    public class CachedEnemiesCollection : CachedComponentCollection<EnemyAI>
    {
        protected override bool TryGetComponent(GameObject targetGameObject, out EnemyAI target)
        {
            target = null;
            if (targetGameObject.CompareTag("Enemy"))
                if (targetGameObject.TryGetComponent(out EnemyAICollisionDetect enemyCollider))
                    target = enemyCollider.mainScript;
            return (target != null);
        }
    }
}
