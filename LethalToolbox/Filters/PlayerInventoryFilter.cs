using GameNetcodeStuff;
using System;
using System.Collections.Generic;
using System.Text;
using Unity.Netcode;
using UnityEngine.Events;

namespace LethalToolbox.Filters
{
    public class PlayerInventoryFilter : NetworkBehaviour
    {
        public UnityEvent<PlayerControllerB> onFilteredEvent;

        public List<string> itemNameFilters = new List<string>();

        public void FilterEvent(PlayerControllerB player)
        {
            if (player != null)
                foreach (GrabbableObject grabbableObject in player.ItemSlots)
                    if (grabbableObject != null && itemNameFilters.Contains(grabbableObject.itemProperties.name))
                        onFilteredEvent.Invoke(player);
        }
    }
}
