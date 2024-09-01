using GameNetcodeStuff;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace LethalToolbox.Sources.CollisionSources
{
    public class CachedIHittablesCollection : CachedComponentCollection<IHittable>
    {
        protected override bool TryGetComponent(GameObject targetGameObject, out IHittable target)
        {
            return (targetGameObject.TryGetComponent(out target));
        }
    }
}
