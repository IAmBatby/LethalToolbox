using GameNetcodeStuff;
using LethalToolbox.Sources.CollisionSources;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace LethalToolbox.Sources
{
    public static class CachedComponents
    {
        public static CachedPlayersCollection Players = new CachedPlayersCollection();
        public static CachedEnemiesCollection Enemies = new CachedEnemiesCollection();
        public static CachedGrabbableObjectsCollection Items = new CachedGrabbableObjectsCollection();
        public static CachedVehiclesCollection Vehicles = new CachedVehiclesCollection();
        public static CachedIHittablesCollection Hittables = new CachedIHittablesCollection();

        public static List<CachedComponentCollection> AllCachedCollections { get; private set; }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
        private static void Initialize()
        {
            AllCachedCollections = new List<CachedComponentCollection>() { Players, Enemies, Items, Vehicles, Hittables };
        }

        public static void TryCacheComponentAll<T>(T sourceObject) where T : MonoBehaviour
        {
            foreach (Collider collider in sourceObject.GetComponents<Collider>())
                CachedComponents.TryCacheComponent(collider.gameObject, sourceObject);
            foreach (Collider collider in sourceObject.GetComponentsInChildren<Collider>())
                CachedComponents.TryCacheComponent(collider.gameObject, sourceObject);
        }

        public static bool TryCacheComponent<T>(GameObject sourceObject, T source) where T : class
        {
            foreach (CachedComponentCollection sourceCollection in AllCachedCollections)
                if (TryCacheComponentWithCollection(sourceObject, source, sourceCollection))
                    return (true);

            return (false);
        }

        public static bool TryGetComponent<T>(GameObject sourceObject, out T source) where T : class
        {
            foreach (CachedComponentCollection sourceCollection in AllCachedCollections)
                if (TryGetCachedComponentFromCollection(sourceObject, sourceCollection, out source))
                    return (true);

            source = null;
            return (false);
        }

        public static bool TryGetCacheComponent<T>(Collider sourceCollider, out T source) where T : class
        {
            return (TryGetComponent(sourceCollider.gameObject, out source));
        }

        public static bool TryGetCacheComponent<T>(Transform sourceTransform, out T source) where T : class
        {
            return (TryGetComponent(sourceTransform.gameObject, out source));
        }

        public static bool TryGetCachedComponentFromCollection<T>(GameObject sourceObject, CachedComponentCollection sourceCollection, out T source) where T : class
        {
            source = null;
            if (sourceCollection is CachedComponentCollection<T> castedSourceCollection)
                castedSourceCollection.TryGetCachedComponent(sourceObject, out source);

            return (source != null);  
        }

        public static bool TryCacheComponentWithCollection<T>(GameObject sourceObject, T source, CachedComponentCollection sourceCollection) where T : class
        {
            if (sourceCollection is CachedComponentCollection<T> castedCollection)
                return (castedCollection.TryCacheComponent(sourceObject, source));
            else
                return false;
        }
    }
}
