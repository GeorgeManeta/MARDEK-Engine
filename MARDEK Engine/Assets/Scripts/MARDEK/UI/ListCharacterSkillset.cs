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
        [SerializeField] ActionSkillset characterSkillset;
        [SerializeField] TextMeshProUGUI skillsetNameLabel = null;
        [SerializeField] Image skillsetIcon = null; 

        private void OnEnable()
        {
               var character = Battle.BattleManager.characterActing;
               if (character is null)
               {
                    Debug.Log("Attempted to list character skill with no character acting");
                    return;
               }
               characterSkillset = character.Profile.ActionSkillset;
               
               if (characterSkillset)
               {
                    skillsetNameLabel.text = characterSkillset.name;
                    skillsetIcon.sprite = characterSkillset.Sprite;
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
               if (!characterSkillset)
                    return;

               var character = Battle.BattleManager.characterActing;

               foreach (var skill in characterSkillset.Skills)
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
               UpdateLayout();
        }
    }
}