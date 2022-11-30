using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MARDEK.Stats;

namespace MARDEK.Skill
{
    [CreateAssetMenu(menuName = "MARDEK/Skill/ActionSkill")]
    public class ActionSkill : Skill
    {
        [SerializeField] List<SkillEffect> effects = new List<SkillEffect>();

        public override void Apply(IStats user, IStats target)
        {
            foreach (var effect in effects)
                effect.Apply(user, target);
        }
    }
}