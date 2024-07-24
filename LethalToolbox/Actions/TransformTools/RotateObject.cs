using System;
using System.Collections.Generic;
using System.Text;
using Unity.Netcode;
using UnityEngine;

namespace LethalToolbox
{
    public class RotateObject : NetworkBehaviour
    {
        public float moveSpeed;
        public List<Vector3> targetRotations = new List<Vector3>();

        private NetworkVariable<int> networkedTargetRotationsIndex = new NetworkVariable<int>();
        private int targetRotationsIndex { get { return (networkedTargetRotationsIndex.Value); } set { networkedTargetRotationsIndex.Value = value; } }
        private Vector3 targetRotation;

        public void Update()
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(targetRotations[targetRotationsIndex]), Time.deltaTime * moveSpeed);
        }

        public void ToggleRotationForward(GameObject gameObject)
        {
            ToggleRotationServerRpc(true);
        }

        public void ToggleRotationBackward(GameObject gameObject)
        {
            ToggleRotationServerRpc(false);
        }

        [ServerRpc]
        public void ToggleRotationServerRpc(bool isForward)
        {
            if (isForward == true)
            {
                Debug.Log("Rotating Forward");
                if (targetRotationsIndex != targetRotations.Count - 1)
                    targetRotationsIndex += 1;
                else
                    targetRotationsIndex = 0;
            }
            else
            {
                Debug.Log("Rotating Backward");
                if (targetRotationsIndex == 0)
                    targetRotationsIndex = targetRotations.Count - 1;
                else
                    targetRotationsIndex -= 1;
            }
        }
    }
}
