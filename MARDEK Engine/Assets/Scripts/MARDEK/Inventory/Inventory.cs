using UnityEngine;
using System.Collections.Generic;

namespace MARDEK.Inventory
{
    [System.Serializable]
    public class Inventory
    {
        [SerializeField] List<Slot> slots;
        public List<Slot> Slots { get { return slots; } }

        public int CountItem(Item item)
        {
            int result = 0;
            foreach (var slot in slots)
            {
                if (!slot.IsEmpty() && slot.currentItem == item) result += slot.amount;
            }
            return result;
        }

        public bool AddItem(Item item, int amount)
        {
            // First try to fill slots that already have the right item
            foreach (var slot in slots)
            {
                if (slot.canPlayerPutItems && !slot.IsEmpty() && slot.currentItem == item)
                {
                    slot.currentAmount += amount;
                    return true;
                }
            }

            // Then try to fill empty slots
            foreach (var slot in slots)
            {
                if (slot.canPlayerPutItems && slot.IsEmpty())
                {
                    slot.currentItem = item;
                    slot.currentAmount = amount;
                    return true;
                }
            }

            // If there are no suitable slots, return false and don't take the item
            return false;
        }
    }
}
