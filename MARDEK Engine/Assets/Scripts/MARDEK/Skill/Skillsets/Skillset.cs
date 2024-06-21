using System.Collections.Generic;
using UnityEngine;

namespace MARDEK.Skill
{
    [CreateAssetMenu(menuName = "MARDEK/Skill/Skillset")]
    public abstract class Skillset<TSkill> : ScriptableObject where TSkill : Skill
    {
        abstract public string Description { get; protected set; }
        abstract public Sprite Sprite { get; protected set; }
        abstract public List<TSkill> Skills { get; protected set; }
    }
}