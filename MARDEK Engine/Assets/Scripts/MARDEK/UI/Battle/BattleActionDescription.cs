using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MARDEK.UI
{
    using Stats;
    public class BattleActionDescription : MonoBehaviour
    {
        [SerializeField] Text nameLabel;
        [SerializeField] Text description;
        [SerializeField] Image icon;

        private void Awake()
        {
            BattleActionSlotUI.UpdateSelected += UpdateDescription;
        }
        private void OnEnable()
        {
            UpdateDescription(null);
        }

        public void UpdateDescription(IActionSlot action)
        {
            if (action != null)
            {
                nameLabel.text = action.DisplayName;
                description.text = action.Description;
                icon.sprite = action.Sprite;
            }
            else
            {
                nameLabel.text = string.Empty;
                description.text = string.Empty;
                icon.sprite = null;
            }
        }
    } 
}