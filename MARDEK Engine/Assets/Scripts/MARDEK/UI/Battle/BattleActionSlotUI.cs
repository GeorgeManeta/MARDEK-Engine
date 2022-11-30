using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MARDEK.CharacterSystem;
using UnityEngine.UI;

namespace MARDEK.UI
{
    using Stats;
    public class BattleActionSlotUI : Selectable
    {
        public delegate void UpdateSelectedSlot(IActionSlot slot);
        public static UpdateSelectedSlot UpdateSelected { get; set; }
        [SerializeField] Image sprite;
        [SerializeField] Text nameLabel;
        [SerializeField] Text number;
        IActionSlot slot;

        public void SetSlot(IActionSlot _slot)
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

        public void SelectAction()
        {
            Battle.BattleManager.selectedAction = slot;
        }

        public override void Select(bool playSFX = true)
        {
            base.Select(playSFX);
            UpdateSelected?.Invoke(slot);
        }
    }
}