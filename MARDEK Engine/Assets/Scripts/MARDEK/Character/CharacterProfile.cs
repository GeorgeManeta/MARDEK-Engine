using UnityEngine;
using MARDEK.Core;
using MARDEK.Animation;
using MARDEK.Skill;
using MARDEK.Stats;

namespace MARDEK.CharacterSystem
{
    [CreateAssetMenu(menuName ="MARDEK/Character/CharacterProfile")]
    public class CharacterProfile : AddressableScriptableObject
    {
        [field: SerializeField] public string displayName { get; private set; }
        [field: SerializeField] public string displayClass { get; private set; }
        [field: SerializeField] public Element element { get; private set; }
        [field: SerializeField] public CharacterPortrait portrait { get; private set; }
        [field: SerializeField] public SpriteAnimationClipList WalkSprites { get; private set; }
        [field: SerializeField] public GameObject BattleModelPrefab { get; private set; }
        [field: SerializeField] public Skillset ActionSkillset { get; private set; }
        [field: SerializeField] public StatsSet StartingStats { get; private set; } = new StatsSet();

        [SerializeField] StatExpression MaxHPExpressionOveride;
        [SerializeField] StatExpression MaxMPExpressionOveride;
        public StatExpression MaxHPExpression
        { 
            get
            {
                if (MaxHPExpressionOveride)
                    return MaxHPExpressionOveride;
                else
                    return StatsGlobals.Instance.DefaultMaxHPExpression;
            }
        }
        public StatExpression MaxMPExpression
        {
            get
            {
                if (MaxMPExpressionOveride)
                    return MaxMPExpressionOveride;
                else
                    return StatsGlobals.Instance.DefaultMaxMPExpression;
            }
        }
    }
}