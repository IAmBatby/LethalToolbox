using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace LethalToolbox.Actions.LethalCompany.DamageActions
{
    [System.Serializable]
    public class VehicleTargets : DamageTargets<VehicleController>
    {
        public override bool TryDamageTarget(VehicleController target, bool conditional, bool playNormalDamageSFX, Vector3 hitDirection)
        {
            if (conditional && Enabled && !target.carDestroyed)
            {
                //VehicleControllerPatch.DealPermanentDamage(target, DamageAmount, hitDirection);
                return (true);
            }
            return (false);
        }
    }
}
