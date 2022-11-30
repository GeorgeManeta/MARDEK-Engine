using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MARDEK.UI
{
    using Stats;
    public abstract class ListActions : MonoBehaviour
    {
        [SerializeField] SelectableLayout listUI;
        List<BattleActionSlotUI> actionSlotUIs = new();
        int index = 0;

        private void Awake()
        {
            actionSlotUIs = new List<BattleActionSlotUI>(listUI.GetComponentsInChildren<BattleActionSlotUI>());
        }

        protected void ClearSlots()
        {
            foreach (var slot in actionSlotUIs)
                slot.SetSlot(null);
            index = 0;
        }

        protected void SetNextSlot(IActionSlot action)
        {
            actionSlotUIs[index].SetSlot(action);
            index++;
        }

        public void UpdateLayout()
        {
            listUI.UpdateSelectionAtIndex(false);
        }
    }
}
