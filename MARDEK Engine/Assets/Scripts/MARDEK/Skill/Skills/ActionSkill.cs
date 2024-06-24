using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MARDEK.Skill
{
    using Core;
    [CreateAssetMenu(menuName = "MARDEK/Skill/ActionSkill")]
    public class ActionSkill : Skill
    {
        [SerializeField] List<SkillEffect> effects = new List<SkillEffect>();

        public override void Apply(IActor user, IActor target)
        {
            foreach (var effect in effects)
                effect.Apply(user, target);
        }
    }
}