using GameNetcodeStuff;
using LethalToolbox.Sources;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace LethalToolbox.Events
{
    public class TestTriggerEvent : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (CachedComponents.TryGetCacheComponent(other, out PlayerControllerB player))
            {

            }
            else if (CachedComponents.TryGetCacheComponent(other, out EnemyAICollisionDetect enemyCollider))
            {

            }
            else if (CachedComponents.TryGetCacheComponent(other, out VehicleController vehicle))
            {

            }
            else if (CachedComponents.TryGetCacheComponent(other, out IHittable hittableObject))
            {

            }
        }
    }
}
