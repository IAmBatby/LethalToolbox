using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace LethalToolbox.Sources.CollisionSources
{
    public class CachedVehiclesCollection : CachedComponentCollection<VehicleController>
    {
        protected override bool TryGetComponent(GameObject targetGameObject, out VehicleController target)
        {
            target = null;
            if (StartOfRound.Instance.attachedVehicle != null && StartOfRound.Instance.attachedVehicle.gameObject == targetGameObject)
                target = StartOfRound.Instance.attachedVehicle;
            return (target != null);
        }
    }
}
