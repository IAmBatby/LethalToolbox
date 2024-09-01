using GameNetcodeStuff;
using LethalToolbox.Misc;
using LethalToolbox.Sources;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UIElements.StyleSheets;

namespace LethalToolbox.Actions.LethalCompany.DamageActions
{
    [System.Serializable]
    public class PlayerTargets : DamageTargets<PlayerControllerB>
    {
        [field: SerializeField] public CauseOfDeath DamageSource { get; set; } = CauseOfDeath.Unknown;
        [field: SerializeField] public int CorpseType = 0;
        [field: SerializeField] public PlayerRagdollType NewCorpseType;

        public override bool TryDamageTarget(PlayerControllerB target, bool conditional, bool playNormalDamageSFX, Vector3 hitDirection)
        {
            if (conditional && Enabled && !target.isPlayerDead)
            {
                target.DamagePlayer(DamageAmount, causeOfDeath: DamageSource, force: hitDirection, hasDamageSFX: playNormalDamageSFX, deathAnimation: Mathf.Clamp(CorpseType, 0, StartOfRound.Instance.playerRagdolls.Count));
                return (true);
            }
            return (false);
        }
    }
}
