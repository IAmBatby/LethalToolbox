using System;
using System.Collections.Generic;
using System.Text;
using Unity.Netcode;
using UnityEngine.Events;

namespace LethalToolbox
{
    public class AwakeEvent : NetworkBehaviour
    {
        public UnityEvent onAwakeEvent;

        public void Awake()
        {
            onAwakeEvent.Invoke();
        }
    }
}
