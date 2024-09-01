using GameNetcodeStuff;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace LethalToolbox.Actions.LethalCompany.DamageActions
{
    [System.Serializable]
    public class EnemyTargets : DamageTargets<EnemyAI>
    {
        public override bool TryDamageTarget(EnemyAI target, bool conditional, bool playNormalDamageSFX, Vector3 hitDirection)
        {
            if (conditional && Enabled && !target.isEnemyDead)
            {
                target.HitEnemyOnLocalClient(DamageAmount, hitDirection, playHitSFX: playNormalDamageSFX);
                return (true);
            }
            return (false);
        }
    }
}
