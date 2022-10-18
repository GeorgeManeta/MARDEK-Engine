using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MARDEK.UI
{
    public class ListCharacterExpendableItems : ListActions
    {
        public void SetSlots()
        {
            ClearSlots();
            foreach(var slot in Battle.BattleManager.characterActing.Inventory.Slots)
            {
                if (slot.item is Inventory.ExpendableItem)
                {
                    SetNextSlot(slot);
                }
            }
            UpdateLayout();
        }
    }
}