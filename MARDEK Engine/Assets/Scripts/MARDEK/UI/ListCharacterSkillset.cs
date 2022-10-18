using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MARDEK.CharacterSystem;
using MARDEK.Skill;
using UnityEngine.UI;

namespace MARDEK.UI
{
    public class ListCharacterSkillset : ListActions
    {
        Skillset skillsetToShow;
        [SerializeField] Text skillsetNameLabel = null;
        [SerializeField] Image skillsetIcon = null; 
        [SerializeField] List<Skillset> possibleSkillsets;

        private void OnEnable()
        {
            var character = Battle.BattleManager.characterActing;
            skillsetToShow = null;
            foreach (var slot in character.SkillSlots)
            {
                foreach (var set in possibleSkillsets)
                {
                    if (set.Skills.Contains(slot.Skill))
                        skillsetToShow = set;
                }
                if (skillsetToShow)
                    break;
            }
            if (skillsetToShow)
            {
                skillsetNameLabel.text = skillsetToShow.name;
                skillsetIcon.sprite = skillsetToShow.Sprite;
            }
            else
            {
                skillsetNameLabel.text = " - ";
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