using System.Collections.Generic;
using UnityEngine;

namespace MARDEK.Skill
{
    [CreateAssetMenu(menuName = "MARDEK/Skill/Skillset")]
    public class Skillset<TSkill> : ScriptableObject where TSkill : Skill
    {
        public string Description { get; protected set; }
        public Sprite Sprite { get; protected set; }
        public List<TSkill> Skills { get; protected set; }
    }
}