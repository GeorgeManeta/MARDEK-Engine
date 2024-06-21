using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MARDEK.Skill
{
     [CreateAssetMenu(menuName = "MARDEK/Skill/Skillset/Reaction Skill Set")]
     public class ReactionSkillset : Skillset<ReactionSkill>
     {
          public override string Description { get ; protected set ; }
          public override Sprite Sprite { get ; protected set; }
          public override List<ReactionSkill> Skills { get ; protected set ; }
     }
}