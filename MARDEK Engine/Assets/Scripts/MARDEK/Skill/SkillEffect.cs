using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MARDEK.Skill
{
    using Core;
    // Effects like dealing damage, healing and applying status effects should inherit from this
    public abstract class SkillEffect : ScriptableObject
    {
        public abstract void Apply(IActor user, IActor target);
    }
}