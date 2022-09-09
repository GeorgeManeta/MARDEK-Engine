using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MARDEK.Stats;

namespace MARDEK.Skill
{
    // Effects like dealing damage, healing and applying status effects should inherit from this
    public abstract class SkillEffect : ScriptableObject
    {
        public abstract void Apply(IStats user, IStats target);
    }
}