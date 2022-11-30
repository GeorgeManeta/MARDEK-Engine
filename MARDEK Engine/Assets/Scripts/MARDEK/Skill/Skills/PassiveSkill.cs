using MARDEK.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MARDEK.Skill
{
    [CreateAssetMenu(menuName = "MARDEK/Skill/PassiveSkill")]
    public class PassiveSkill : Skill
    {
        public override void Apply(IStats user, IStats target)
        {
            throw new System.NotImplementedException();
        }
    }
}