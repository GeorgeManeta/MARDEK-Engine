using System.Collections.Generic;
using UnityEngine;

namespace MARDEK.Skill
{
     [CreateAssetMenu(menuName = "MARDEK/Skill/Skillset/Action Skill Set")]
     public class ActionSkillset : Skillset<ActionSkill>
     {
          override public string Description { get; protected set; }
          override public Sprite Sprite { get; protected set; }
          override public List<ActionSkill> Skills { get; protected set; }
     }
}