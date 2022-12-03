using UnityEngine;
using MARDEK.Stats;

namespace MARDEK.Skill
{
    [CreateAssetMenu(menuName = "MARDEK/Skill/SkillEffects/ModifyStats")]
    public class ModifyStat : SkillEffect
    {
        [SerializeField] IntegerStat targetStat; // most probably the current health stat, but who knows
        [SerializeField] StatExpression valueExpresion;

        public override void Apply(IStats user, IStats target)
        {
            var value = -valueExpresion.Evaluate(user, target);
            target.ModifyStat(targetStat, value);
            Debug.Log($"Stat {targetStat} modified by {value}");
        }
    }
}