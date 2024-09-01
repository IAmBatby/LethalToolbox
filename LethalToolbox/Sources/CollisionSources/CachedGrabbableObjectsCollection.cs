using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace LethalToolbox.Sources.CollisionSources
{
    public class CachedGrabbableObjectsCollection : CachedComponentCollection<GrabbableObject>
    {
        protected override bool TryGetComponent(GameObject targetGameObject, out GrabbableObject target)
        {
            target = null;
            if (targetGameObject.CompareTag("PhysicsProp"))
                targetGameObject.TryGetComponent(out target);
            return (target != null);
        }
    }
}
