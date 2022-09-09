using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MARDEK.Skill;

namespace MARDEK.CharacterSystem
{
    [System.Serializable]
    public class SkillSlot
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
    }
}