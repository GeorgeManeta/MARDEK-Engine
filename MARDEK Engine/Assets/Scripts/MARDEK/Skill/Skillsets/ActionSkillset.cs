using System.Collections.Generic;
using UnityEngine;

namespace MARDEK.Skill
{
     [CreateAssetMenu(menuName = "MARDEK/Skill/Skillset/Action Skill Set")]
     public class ActionSkillset : Skillset<ActionSkill>
     {
          [field: SerializeField] public override string Description { get; set; }
          [field: SerializeField] public override Sprite Sprite { get; set; }
          [field: SerializeField] public override List<ActionSkill> Skills { get; set; }
          [field: SerializeField] public string SkillsetName { get; set; }
     }
}