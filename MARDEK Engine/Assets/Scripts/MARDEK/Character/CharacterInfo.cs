using UnityEngine;
using MARDEK.Core;
using MARDEK.Animation;
using MARDEK.Skill;
using MARDEK.Stats;

namespace MARDEK.CharacterSystem
{
    [CreateAssetMenu(menuName ="MARDEK/Character/CharacterInfo")]
    public class CharacterInfo : AddressableScriptableObject
    {
        [field: SerializeField] public string displayName { get; private set; }
        [field: SerializeField] public SpriteAnimationClipList WalkSprites { get; private set; }
        [field: SerializeField] public Skillset ActionSkillset { get; private set; }
        [field: SerializeField] public StatsSet StartingStats { get; private set; } = new StatsSet();
    }
}