using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MARDEK.CharacterSystem;
using MARDEK.Skill;
using UnityEngine.UI;
using TMPro;

namespace MARDEK.UI
{
    public class ListCharacterSkillset : ListActions
    {
        ActionSkillset skillsetToShow;
        [SerializeField] TextMeshProUGUI skillsetNameLabel = null;
        [SerializeField] Image skillsetIcon = null; 

        private void OnEnable()
        {
            skillsetToShow = null;
            
            if (skillsetToShow)
            {
                skillsetNameLabel.text = skillsetToShow.name;
                skillsetIcon.sprite = skillsetToShow.Sprite;
            }
            else
            {
                Debug.LogWarning("No skillset to show");
                skillsetNameLabel.text = "-";
                skillsetIcon.sprite = null;
            }
        }

        public void SetSlots()
        {
            ClearSlots();
            if (skillsetToShow)
            {
                var character = Battle.BattleManager.characterActing;
                foreach (var skill in skillsetToShow.Skills)
                {
                    foreach (var skillSlot in character.SkillSlots)
                    {
                        if (skill == skillSlot.Skill)
                        {
                            SetNextSlot(skillSlot);
                            break;
                        }
                    }
                }
            }
            UpdateLayout();
        }
    }
}