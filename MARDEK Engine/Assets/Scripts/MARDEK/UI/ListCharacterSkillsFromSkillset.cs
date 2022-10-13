using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MARDEK.CharacterSystem;
using MARDEK.Skill;

namespace MARDEK.UI
{
    public class ListCharacterSkillsFromSkillset : MonoBehaviour
    {
        [SerializeField] List<Skillset> possibleSkillsets;
        Skillset skillsetToShow;

        private void OnEnable()
        {
            SetCharacter(Battle.BattleManager.characterActing);
            GetComponent<SelectableLayout>().RefreshSelectables();
        }

        public void SetCharacter(Character character)
        {
            // TODO: optimize this search
            skillsetToShow = null;
            foreach(var slot in character.SkillSlots)
            {
                foreach (var set in possibleSkillsets)
                {
                    if (set.Skills.Contains(slot.Skill))
                        skillsetToShow = set;
                }
                if (skillsetToShow)
                    break;
            }

            if(skillsetToShow == null)
            {
                Debug.LogWarning($"{character} don't have a skill in the given skillsets");
                return;
            }
            Debug.Log($"Showing skills for skillset = {skillsetToShow}");

            var numSkillSlots = character.SkillSlots.Count;
            for(int i = 0; i < transform.childCount; i++)
            {
                if(i < numSkillSlots && skillsetToShow.Skills.Contains(character.SkillSlots[i].Skill))
                    SetSlot(transform.GetChild(i), character.SkillSlots[i]);
                else
                    SetSlot(transform.GetChild(i), null);
            }
            Debug.Log($"SET CHARACTER {character}, {numSkillSlots} to {transform.childCount}");
        }

        void SetSlot(Transform UI, SkillSlot skillSlot)
        {
            if (skillSlot != null)
            {
                UI.gameObject.SetActive(true);
                var name = string.IsNullOrEmpty(skillSlot.Skill.DisplayName) ? skillSlot.Skill.name : skillSlot.Skill.DisplayName;
                UI.gameObject.GetComponentInChildren<UnityEngine.UI.Text>().text = name;
            }
            else
            {
                UI.gameObject.SetActive(false);
            }
        }
    }

}