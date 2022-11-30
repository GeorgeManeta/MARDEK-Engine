using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MARDEK.CharacterSystem;
using MARDEK.Skill;
using MARDEK.Stats;

namespace MARDEK.UI
{
    public class SkillsCategorySelect : Selectable
    {
        [SerializeField] Skillset category;
        [SerializeField] Image categoryIcon;
        [SerializeField] GridLayoutGroup skillEntriesLayout;
        [SerializeField] GameObject skillEntryPrefab;
        [SerializeField] Text skillCategoryLabel;
        [SerializeField] Text skillCategoryName;
        [SerializeField] Text skillSetDescription;
        Skillset lastCategory = null;

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

            var categoryToUse = category;
            if (categoryToUse == null)
                categoryToUse = character.CharacterInfo.ActionSkillset;
            if (categoryToUse != lastCategory)
            {
                UpdateCategory(categoryToUse);
                lastCategory = categoryToUse;
            }
        }

        void UpdateCategory(Skillset category)
        {
            categoryIcon.sprite = category.Sprite;
        }

        public override void Select(bool playSFX = true)
        {
            base.Select(playSFX);
            skillCategoryLabel.text = gameObject.name;
        }

        void UpdateCategoryInfo()
        {
            skillCategoryName.text = lastCategory?.name;
            skillSetDescription.text = lastCategory?.Description;
        }
    }
}