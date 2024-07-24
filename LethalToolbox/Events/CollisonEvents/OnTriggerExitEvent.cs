using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

namespace LethalToolbox
{
    public class OnTriggerExitEvent : MonoBehaviour
    {
        public ComparisonType comparisonType;
        public string filter;
        public bool debugAllContacts;
        public GameObjectEvent onCollisionExitEvent;

        public void OnTriggerExit(Collider other)
        {
            Comparison comparison = new Comparison(comparisonType, filter);

            if (debugAllContacts == true)
                Debug.Log("GameObject Name: " + other.gameObject.name + " GameObject Tag: " + other.gameObject.tag);

            if (comparison.ValidComparison(other.gameObject) == true)
                onCollisionExitEvent?.Invoke(other.gameObject);
        }

        public void DebugLog(GameObject other)
        {
            Debug.Log("DebugLog - GameObject Name: " + other.gameObject.name + " GameObject Tag: " + other.gameObject.tag);
        }
    }
}
