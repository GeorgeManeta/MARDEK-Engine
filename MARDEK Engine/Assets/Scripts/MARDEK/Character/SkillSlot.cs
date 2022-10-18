using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MARDEK.Skill;

namespace MARDEK.CharacterSystem
{
    [System.Serializable]
    public class SkillSlot : Core.IActionSlot
    {
        [field: SerializeField] public Skill.Skill Skill {get;set;}
        [field: SerializeField] public int MasteryPoints { get; set; }
        public bool IsMastered
        {
            get
            {
                var points = MasteryPoints;
                var requiredPoints = Skill.PointsRequiredToMaster;
                return ((points >= requiredPoints) || (points == -1));
            }
        }

        public string DisplayName
        {
            get
            {
                if (string.IsNullOrEmpty(Skill.DisplayName))
                    return Skill.name;
                else
                    return Skill.DisplayName;
            }
        }
        public Sprite Sprite
        {
            get
            {
                return Skill.Element.thickSprite;
            }
        }
        public int Number => Skill.Cost;
    }
}