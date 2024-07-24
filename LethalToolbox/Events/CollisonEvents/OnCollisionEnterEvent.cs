using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

namespace LethalToolbox
{
    [System.Serializable]
    public class GameObjectEvent : UnityEvent<GameObject> { }

    public class OnCollisionEnterEvent : MonoBehaviour
    {
        public string tagFilter;
        public bool debugAllContacts;
        public GameObjectEvent onCollisionEnterEvent;

        public void OnCollisionEnter(Collision other)
        {
            if (debugAllContacts == true)
                Debug.Log(gameObject.name + " hit " + other.gameObject.name);

            if (tagFilter == string.Empty || other.gameObject.CompareTag(tagFilter))
                onCollisionEnterEvent?.Invoke(other.gameObject);
        }

        public void DebugLog(GameObject other)
        {
            Debug.Log(other.name);
        }
    }
}
