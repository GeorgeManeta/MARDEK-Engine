using UnityEngine;
using MARDEK.Core;
using MARDEK.Animation;
using MARDEK.Stats;

namespace MARDEK.CharacterSystem
{
    [CreateAssetMenu(menuName ="MARDEK/Character/CharacterPortrait")]
    public class CharacterPortrait : AddressableScriptableObject
    {
        // [field: SerializeField] public SpriteAnimationClipList WalkSprites { get; private set; }
        [field: SerializeField] public Sprite face { get; private set; }
        [field: SerializeField] public Sprite hair { get; private set; }
        [field: SerializeField] public Sprite eye { get; private set; }
        [field: SerializeField] public Sprite eyebrow { get; private set; }
        [field: SerializeField] public Sprite neck { get; private set; }
        [field: SerializeField] public Sprite armour { get; private set; }

    }
}