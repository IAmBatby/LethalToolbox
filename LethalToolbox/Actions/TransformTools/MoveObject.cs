using System;
using System.Collections.Generic;
using System.Text;
using Unity.Netcode;
using UnityEngine;

namespace LethalToolbox
{
    public class MoveObject : NetworkBehaviour
    {
        public float moveSpeed;
        public List<Vector3> targetPositions = new List<Vector3>();

        private NetworkVariable<int> networkedTargetPositionsIndex = new NetworkVariable<int>();
        private int targetPositionsIndex { get { return (networkedTargetPositionsIndex.Value); } set { networkedTargetPositionsIndex.Value = value; } }

        public void Update()
        {
            transform.position = Vector3.Lerp(transform.position, targetPositions[targetPositionsIndex], Time.deltaTime * moveSpeed);
        }

        public void TogglePositionForward(GameObject gameObject)
        {
            TogglePositionServerRpc(true);
        }

        public void TogglePositionBackward(GameObject gameObject)
        {
            TogglePositionServerRpc(false);
        }

        [ServerRpc]
        public void TogglePositionServerRpc(bool isForward)
        {
            if (isForward == true)
            {
                Debug.Log("Moving Forward");
                if (targetPositionsIndex != targetPositions.Count - 1)
                    targetPositionsIndex += 1;
                else
                    targetPositionsIndex = 0;
            }
            else
            {
                Debug.Log("Moving Backward");
                if (targetPositionsIndex == 0)
                    targetPositionsIndex = targetPositions.Count - 1;
                else
                    targetPositionsIndex -= 1;
            }
        }
    }
}
