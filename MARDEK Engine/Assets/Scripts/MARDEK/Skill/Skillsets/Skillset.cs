using System.Collections.Generic;
using UnityEngine;

namespace MARDEK.Skill
{
    public abstract class Skillset<TSkill> : ScriptableObject where TSkill : Skill
    {
        abstract public string Description { get; set; }
        abstract public Sprite Sprite { get; set; }
        abstract public List<TSkill> Skills { get; set; }
    }
}