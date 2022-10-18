using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MARDEK.CharacterSystem;
using UnityEngine.UI;

namespace MARDEK.UI
{
    public class BattleActionSlotUI : Selectable
    {
        [SerializeField] Image sprite;
        [SerializeField] Text nameLabel;
        [SerializeField] Text number;
        Core.IActionSlot slot;

        public void SetSlot(Core.IActionSlot _slot)
        {
            slot = _slot;
            if (slot == null)
                gameObject.SetActive(false);
            else
            {
                nameLabel.text = slot.DisplayName;
                sprite.sprite = slot.Sprite;
                number.text = slot.Number.ToString();
                gameObject.SetActive(true);
            }
        }

        public void SelectSkill()
        {
            Battle.BattleManager.selectedAction = slot;
        }
    }
}