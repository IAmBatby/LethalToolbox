using GameNetcodeStuff;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.Netcode;
using UnityEngine;

namespace LethalToolbox
{
    public class HurtPlayer : NetworkBehaviour
    {
        public enum RagdollType { Default, HeadBurst, Spring, Electrocuted, ComedyMask, TragedyMask, Burnt }

        public int damage;
        public float cooldown;
        public Vector3 playerForceVelocity = Vector3.zero;

        [Space(10)]

        public bool spawnBody;
        public CauseOfDeath causeOfDeath = CauseOfDeath.Unknown;
        public RagdollType bodyType = RagdollType.Default;

        private bool isOnCooldown = false;

        public void KillPlayer(PlayerControllerB player)
        {
            player.KillPlayer(bodyVelocity: playerForceVelocity, spawnBody: spawnBody, causeOfDeath: causeOfDeath, deathAnimation: (int)bodyType);
        }

        public void DamagePlayer(PlayerControllerB player)
        {
            if (isOnCooldown == false)
            {
                StartCoroutine(StartCooldown(cooldown));
                player.DamagePlayer(damageNumber: damage, causeOfDeath: causeOfDeath, force: playerForceVelocity, deathAnimation: (int)bodyType);
            }
        }

        public IEnumerator StartCooldown(float cooldown)
        {
            isOnCooldown = true;
            yield return new WaitForSeconds(cooldown);
            isOnCooldown = false;
        }
    }
}
