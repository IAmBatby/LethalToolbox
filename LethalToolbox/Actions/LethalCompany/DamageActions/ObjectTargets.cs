using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace LethalToolbox.Actions.LethalCompany.DamageActions
{
    [System.Serializable]
    public class ObjectTargets : DamageTargets<IHittable>
    {
        public override bool TryDamageTarget(IHittable target, bool conditional, bool playNormalDamageSFX, Vector3 hitDirection)
        {
            if (conditional && Enabled)
            {
                target.Hit(DamageAmount, hitDirection, playHitSFX: playNormalDamageSFX);
                return (true);
            }
            return (false);
        }
    }
}
