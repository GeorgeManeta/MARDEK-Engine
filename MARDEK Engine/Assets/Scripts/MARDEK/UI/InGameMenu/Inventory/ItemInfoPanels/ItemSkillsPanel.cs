using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MARDEK.Inventory;
using UnityEngine.UI;
using TMPro;

namespace MARDEK.UI
{
    public class ItemSkillsPanel : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI textField;

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
