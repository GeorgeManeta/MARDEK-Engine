using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MARDEK.CharacterSystem;
using MARDEK.Skill;
using MARDEK.Stats;
using System;

namespace MARDEK.UI
{
    public class SkillsCategorySelect : Selectable
    {
        [SerializeField] Image categoryIcon;
        [SerializeField] GridLayoutGroup skillEntriesLayout;
        [SerializeField] GameObject skillEntryPrefab;
        [SerializeField] Text skillCategoryLabel;
        [SerializeField] Text skillCategoryName;
        [SerializeField] Text skillSetDescription;

          Type category;
          Type lastCategory = null;

        private void Update()
        {
            CheckCategoryUpdate();
        }

        void CheckCategoryUpdate()
        {
            var character = CharacterSelectable.currentSelected.Character;
            if(character == null)
            {
                return;
            }
            CharacterProfile profile = character.Profile;
               if (category == null)
                    category = profile.ActionSkillset.GetType();

               if (category == lastCategory)
                    return;

               lastCategory = category;
               if (category == typeof(ActionSkill))

                UpdateCategory(profile.ActionSkillset.Sprite, profile.ActionSkillset.Description);
            
        }

        void UpdateCategory(Sprite icon, string description)
        {
               categoryIcon.sprite = icon;
        }

        public override void Select(bool playSFX = true)
        {
            base.Select(playSFX);
            skillCategoryLabel.text = gameObject.name;
        }
        // Not currently needed, gonna ignore for now. Pretty easy to implement back in I just dont want to rn.
        //void UpdateCategoryInfo()
        //{
        //    skillCategoryName.text = lastCategory?.name;
        //    skillSetDescription.text = lastCategory?.Description;
        //}
    }
}