using UnityEngine;
using MARDEK.Stats;

namespace MARDEK.Skill
{
    using Core;
    [CreateAssetMenu(menuName = "MARDEK/Skill/SkillEffects/ModifyStats")]
    public class ModifyStat : SkillEffect
    {
        [SerializeField] IntegerStat targetStat; // most probably the current health stat, but who knows
        [SerializeField] StatExpression valueExpresion;

        public override void Apply(IActor user, IActor target)
        {
            var userCharacter = user as IStats;
            var targetCharacter = target as IStats;

            var value = -valueExpresion.Evaluate(userCharacter, targetCharacter);
            //Debug.Log($"Modifying {targetStat} by {value}");
            targetCharacter.ModifyStat(targetStat, (int)value);
        }
    }
}