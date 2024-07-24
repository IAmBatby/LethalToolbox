using LethalLib.Modules;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

namespace LethalToolbox
{
    public class OnTriggerEnterEvent : MonoBehaviour
    {
        public ComparisonType comparisonType;
        public string filter;
        public bool debugAllContacts;
        public bool checkPreviousRoot;
        public GameObjectEvent onCollisionEnterEvent;
        public int frameCooldown = 0;
        private int frameCooldownCounter;
        public bool onCooldown => (frameCooldownCounter != frameCooldown);

        public Transform previousRoot;

        public void OnTriggerEnter(Collider other)
        {
            if (other.transform.root == previousRoot || onCooldown)
                return;

            Comparison comparison = new Comparison(comparisonType, filter);

            if (debugAllContacts == true)
                Debug.Log("GameObject Name: " + other.gameObject.name + " GameObject Tag: " + other.gameObject.tag);

            if (comparison.ValidComparison(other.gameObject) == true)
            {
                previousRoot = other.transform.root;
                onCollisionEnterEvent?.Invoke(other.gameObject);

                /*if (comparisonType == ComparisonType.IsPlayer)
                    if (Data.TryFindPlayer(other.gameObject, out GameObject playerObject))
                    {
                        onCollisionEnterEvent?.Invoke(playerObject);
                        return;
                    }
                else
                    onCollisionEnterEvent?.Invoke(other.gameObject);*/
            }
        }

        public void OnTriggerExit(Collider other)
        {
            if (other.transform.root == previousRoot)
                previousRoot = null;
        }

        public void Update()
        {
            if (frameCooldownCounter == frameCooldown)
                frameCooldownCounter = 0;
            else
                frameCooldownCounter++;
        }

        public void DebugLog(GameObject other)
        {
            Debug.Log("DebugLog - GameObject Name: " + other.gameObject.name + " GameObject Tag: " + other.gameObject.tag);
        }
    }
}
