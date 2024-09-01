using GameNetcodeStuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LethalToolbox.Sources.CollisionSources
{
    public class CachedPlayersCollection : CachedComponentCollection<PlayerControllerB>
    {
        protected override bool TryGetComponent(GameObject targetGameObject, out PlayerControllerB target)
        {
            target = null;
            if (targetGameObject.CompareTag("Player"))
                targetGameObject.TryGetComponent(out target);
            return (target != null);
        }
    }
}
