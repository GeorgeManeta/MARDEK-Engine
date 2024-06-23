using System.Collections.Generic;
using UnityEngine;

namespace MARDEK.Skill
{
     [CreateAssetMenu(menuName = "MARDEK/Skill/Skillset/Passive Skill Set")]
     public class PassiveSkillset : ScriptableObject
     {
          [field: SerializeField]public string Description { get; set; }
          [field: SerializeField]public Sprite Sprite { get; set; }
          [field: SerializeField] public List<PassiveSkill> Skills { get; set; }
     }
}