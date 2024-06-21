using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MARDEK.Skill
{
     [CreateAssetMenu(menuName = "MARDEK/Skill/Skillset/Passive Skill Set")]
     public class PassiveSkillset : Skillset<PassiveSkill>
     {
          public override string Description { get; protected set; }
          public override Sprite Sprite { get; protected set; }
          public override List<PassiveSkill> Skills { get; protected set; }
     }
}