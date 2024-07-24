using GameNetcodeStuff;
using System;
using System.Collections.Generic;
using System.Text;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Animations;

namespace LethalToolbox
{
    public class ParentObject : NetworkBehaviour
    {
        public bool checkForNetworkObjects = true;

        public List<ParentConstraint> currentConstraints = new List<ParentConstraint>();
        public List<GameObject> registeredParents = new List<GameObject>();

        public Vector3 lastFramePosition;

        public void ParentGameObject(GameObject newChild)
        {
            PlayerControllerB playerController = GetComponentInParent<PlayerControllerB>();

            if (playerController != null)
                newChild = playerController.gameObject;

            if (!registeredParents.Contains(newChild))
                registeredParents.Add(newChild);
            //if (newChild.TryGetComponent(out PlayerControllerB player))
                //player.transform.parent = transform;
        }

        public void UnparentGameObject(GameObject currentChild)
        {
            PlayerControllerB playerController = GetComponentInParent<PlayerControllerB>();

            if (playerController != null)
                currentChild = playerController.gameObject;

            if (registeredParents.Contains(currentChild))
                registeredParents.Add(currentChild);
            //if (currentChild.TryGetComponent(out PlayerControllerB player))
                //player.transform.parent = null;
        }

        public void FixedUpdate()
        {
            foreach (GameObject child in registeredParents)
                if (child != null)
                    child.transform.position += (transform.position - lastFramePosition);
        }

        public void LateUpdate()
        {
            lastFramePosition = transform.position;
        }
    }
}
