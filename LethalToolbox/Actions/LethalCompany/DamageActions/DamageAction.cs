using LethalToolbox.Actions.LethalCompany.DamageActions;
using System;
using System.Collections.Generic;
using System.Text;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Timeline;
using Random = UnityEngine.Random;

namespace LethalToolbox.Actions
{
    public class DamageAction : ToolboxActionBase
    {
        [field: Header("Collision Damage Settings")]
        [field: SerializeField] public bool DamageOnTriggerEnter { get; set; }
        [field: SerializeField] public bool DamageOnTriggerExit { get; set; }
        [field: SerializeField] public bool DamageOnColliderEnter { get; set; }

        [field: Header("Continuous Damage Settings")]
        [field: SerializeField] public bool ContinuousDamage = true;
        [field: SerializeField] public float HitInterval = 0.5f;
        private float timer = 0;

        [field: Space(10)]
        [field: Header("Damage Settings")]
        [field: SerializeField] public Vector3 HitDirection { get; set; }
        [field: SerializeField] public bool PlayNormalDamageSFX { get; set; }
        [field: SerializeField] public List<AudioSource> CustomAudioSources { get; set; } = new List<AudioSource>();
        [field: SerializeField] public List<AudioClip> CustomAudioClips { get; set; } = new List<AudioClip>();


        [Space(10)]
        [Header("Target Settings")]
        public PlayerTargets PlayerTargets;
        public EnemyTargets EnemyTargets;
        public VehicleTargets VehicleTargets;
        public ObjectTargets ObjectTargets;

        private List<DamageTargets> AllDamageTargets = new List<DamageTargets>();

        private bool doesAnyDamageTargetsHaveActiveTargets;

        private enum DamageTargetOption { Add, Remove }

        private void Awake()
        {
            AllDamageTargets = new List<DamageTargets>() { PlayerTargets, EnemyTargets, VehicleTargets, ObjectTargets };
        }

        public void OnNewTriggerEnter(Collider other)
        {
            ProcessNewCollider(other, DamageTargetOption.Add, DamageOnTriggerEnter);
        }

        public void OnNewTriggerExit(Collider other)
        {
            ProcessNewCollider(other, DamageTargetOption.Remove, DamageOnTriggerExit);
        }

        public void OnNewCollisionEnter(Collision other)
        {
            ProcessNewCollider(other.collider, DamageTargetOption.Add, DamageOnColliderEnter);
        }

        private void ProcessNewCollider(Collider other, DamageTargetOption option, bool conditional)
        {
            foreach (DamageTargets damageTarget in AllDamageTargets)
            {
                if ((option == DamageTargetOption.Add && damageTarget.TryAddTarget(other)) || (option == DamageTargetOption.Remove && damageTarget.TryRemoveTarget(other)))
                {
                    TryDamageDamageTarget(damageTarget, other, conditional);
                    break;
                }
            }
        }

        private void Update()
        {
            if (doesAnyDamageTargetsHaveActiveTargets == false || ContinuousDamage == false) return;

            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                timer = HitInterval;
                foreach (DamageTargets damageTarget in AllDamageTargets)
                    damageTarget.DamageAllActiveTargets(PlayNormalDamageSFX, HitDirection);
            }
        }

        private void TryDamageDamageTarget(DamageTargets damageTarget, Collider targetCollider, bool conditional)
        {
            RefreshDoesAnyDamageTargetsHaveActiveTargetsValue();
            if (damageTarget.TryDamageTarget(targetCollider, DamageOnColliderEnter, PlayNormalDamageSFX, HitDirection))
            {
                if (CustomAudioClips.Count > 0 && CustomAudioSources.Count > 0)
                {
                    AudioClip clip = CustomAudioClips[Random.Range(0, CustomAudioClips.Count)];
                    foreach (AudioSource source in CustomAudioSources)
                    {
                        source.clip = clip;
                        source.Play();
                    }
                }
            }
        }

        private void RefreshDoesAnyDamageTargetsHaveActiveTargetsValue()
        {
            if (PlayerTargets.ActiveTargets.Count > 0)
                doesAnyDamageTargetsHaveActiveTargets = true;
            else if (EnemyTargets.ActiveTargets.Count > 0)
                doesAnyDamageTargetsHaveActiveTargets = true;
            else if (VehicleTargets.ActiveTargets.Count > 0)
                doesAnyDamageTargetsHaveActiveTargets = true;
            else if (ObjectTargets.ActiveTargets.Count > 0)
                doesAnyDamageTargetsHaveActiveTargets = true;
            else
                doesAnyDamageTargetsHaveActiveTargets = false;

        }
    }
}
