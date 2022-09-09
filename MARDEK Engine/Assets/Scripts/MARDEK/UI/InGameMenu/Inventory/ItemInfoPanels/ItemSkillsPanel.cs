using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MARDEK.Inventory;
using UnityEngine.UI;

namespace MARDEK.UI
{
    public class ItemSkillsPanel : MonoBehaviour
    {
        [SerializeField] Text textField;

        void Update()
        {
            var slot = SlotUI.selectedSlot;
            if (slot != null && slot.item != null)
                textField.text = "Skills: WIP";
            else
                textField.text = string.Empty;
        }
    }
}
