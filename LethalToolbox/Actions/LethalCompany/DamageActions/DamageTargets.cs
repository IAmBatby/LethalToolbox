using LethalToolbox.Sources;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.Utilities;

namespace LethalToolbox.Actions.LethalCompany.DamageActions
{
    public abstract class DamageTargets
    {
        [field: SerializeField] public bool Enabled { get; set; }
        [field: SerializeField] public int DamageAmount { get; set; }

        public abstract bool TryAddTarget(Collider targetCollider);
        public abstract bool TryRemoveTarget(Collider targetCollider);
        public abstract bool TryDamageTarget(Collider targetCollider, bool conditional, bool playNormalDamageSFX, Vector3 hitDirection);
        public abstract void DamageAllActiveTargets(bool playNormalDamageSFX, Vector3 hitDirection);
    }

    public abstract class DamageTargets<T> : DamageTargets where T : class
    {
        public UnityEvent<T> OnTargetDamaged;


        [HideInInspector] public List<T> ActiveTargets = new List<T>();


        private Dictionary<Collider, T> targetDictionary = new Dictionary<Collider, T>();

        public abstract bool TryDamageTarget(T target, bool conditional, bool playNormalDamageSFX, Vector3 hitDirection);

        public override bool TryDamageTarget(Collider targetCollider, bool conditional, bool playNormalDamageSFX, Vector3 hitDirection)
        {
            if (targetDictionary.TryGetValue(targetCollider, out T target) && target != null)
                return (TryDamageTarget(target, conditional, playNormalDamageSFX, hitDirection));
            else
                return (false);
        }

        public override void DamageAllActiveTargets(bool playNormalDamageSFX, Vector3 hitDirection)
        {
            foreach (T target in ActiveTargets)
                TryDamageTarget(target, false, playNormalDamageSFX, hitDirection);
        }


        public override bool TryAddTarget(Collider targetCollider)
        {
            if (TryGetTarget(targetCollider, out T target))
            {
                ActiveTargets.Add(target);
                targetDictionary.Add(targetCollider, target);
                return (true);
            }
            return (false);
        }

        public override bool TryRemoveTarget(Collider targetCollider)
        {
            if (targetDictionary.TryGetValue(targetCollider, out T target))
            {
                int listIndex = targetDictionary.Keys.IndexOf(targetCollider);
                targetDictionary.Remove(targetCollider);
                ActiveTargets.RemoveAt(listIndex);
                return (true);
            }
            return (false);
        }

        public virtual bool TryGetTarget(Collider targetCollider, out T target)
        {
            if (targetDictionary.TryGetValue(targetCollider, out target))
            {
                if (target != null)
                    return (target != null);
                else //This implies we've added a target but it's now null or destroyed since then so we need to clear it out.
                    TryRemoveTarget(targetCollider);
            }

            TryGetTargetComponent(targetCollider, out target);

            return (target != null);
        }

        protected bool TryGetTargetComponent(Collider targetCollider, out T target)
        {
            return (CachedComponents.TryGetCacheComponent(targetCollider, out target));
        }
    }
}
