using System.Collections.Generic;
using UnityEngine;

namespace MARDEK.Skill
{
     [CreateAssetMenu(menuName = "MARDEK/Skill/Skillset/Action Skill Set")]
     public class ActionSkillset : ScriptableObject
     {
          [field: SerializeField] public string Description { get; set; }
          [field: SerializeField] public Sprite Sprite { get; set; }
          [field: SerializeField] public List<ActionSkill> Skills { get; set; }
          [field: SerializeField] public string SkillsetName { get; set; }
     }
}