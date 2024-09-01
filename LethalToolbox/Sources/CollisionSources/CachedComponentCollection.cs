using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LethalToolbox.Sources.CollisionSources
{
    public abstract class CachedComponentCollection { }
    public abstract class CachedComponentCollection<T> : CachedComponentCollection where T : class
    {
        private Dictionary<GameObject, T> componentDictionary = new Dictionary<GameObject, T>();
        public List<T> Components { get; private set; } = new List<T>();

        public bool TryCacheComponent(GameObject sourceObject)
        {
            if (TryGetCachedComponent(sourceObject, out T target))
            {
                TryCacheComponent(sourceObject, target);
                return (true);
            }
            return (false);
        }

        public bool TryCacheComponent(GameObject sourceObject, T source)
        {
            if (!componentDictionary.ContainsKey(sourceObject))
            {
                Components.Add(source);
                componentDictionary.Add(sourceObject, source);
                return (true);
            }
            return (false);
        }

        public bool TryRemoveComponent(GameObject sourceObject)
        {
            if (componentDictionary.TryGetValue(sourceObject, out T target))
            {
                int listIndex = componentDictionary.Keys.ToList().IndexOf(sourceObject);
                componentDictionary.Remove(sourceObject);
                Components.RemoveAt(listIndex);
                return (true);
            }
            return (false);
        }

        public bool TryGetCachedComponent(GameObject gameObject, out T target)
        {
            if (componentDictionary.TryGetValue(gameObject, out target))
            {
                if (target != null)
                    return (target != null);
                else //This implies we've added a target but it's now null or destroyed since then so we need to clear it out.
                    TryRemoveComponent(gameObject);
            }

            if (TryGetComponent(gameObject, out target))
                TryCacheComponent(gameObject, target);

            return (target != null);
        }

        protected abstract bool TryGetComponent(GameObject targetGameObject, out T target);
    }
}
