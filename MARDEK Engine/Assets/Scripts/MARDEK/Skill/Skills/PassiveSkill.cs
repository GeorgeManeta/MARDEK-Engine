using MARDEK.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MARDEK.Skill
{
    using Core;
    [CreateAssetMenu(menuName = "MARDEK/Skill/PassiveSkill")]
    public class PassiveSkill : Skill
    {
        public override void Apply(IActor user, IActor target)
        {
            throw new System.NotImplementedException();
        }
    }
}