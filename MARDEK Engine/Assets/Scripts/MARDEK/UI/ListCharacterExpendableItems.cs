using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MARDEK.UI
{
    using Battle;
    public class ListCharacterExpendableItems : ListActions
    {
        public void SetSlots()
        {
            ClearSlots();
            foreach (var slot in BattleManager.characterActing.Character.Inventory.Slots)
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