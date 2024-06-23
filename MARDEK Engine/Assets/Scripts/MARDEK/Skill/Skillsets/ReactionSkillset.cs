using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MARDEK.Skill
{
     [CreateAssetMenu(menuName = "MARDEK/Skill/Skillset/Reaction Skill Set")]
     public class ReactionSkillset : ScriptableObject
     {
          [field: SerializeField] public string Description { get; set; }
          [field: SerializeField]public Sprite Sprite { get; set; }
          [field: SerializeField]public List<PassiveSkill> Skills { get; set; }
     }
}