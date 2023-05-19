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
        // [field: SerializeField] public PortraitType PortraitType { get; private set; }
        [field: SerializeField] public PortraitType PortraitType { get; private set; }

        [field: SerializeField] public Sprite Face { get; private set; }
        [field: SerializeField] public Sprite Hair { get; private set; }
        [field: SerializeField] public Sprite Eye { get; private set; }
        [field: SerializeField] public Sprite Eyebrow { get; private set; }
        [field: SerializeField] public Sprite Neck { get; private set; }
        [field: SerializeField] public Sprite Torso { get; private set; }
        [field: SerializeField] public Sprite Mouth { get; private set; }

        // TODO implement
        [field: SerializeField] public Sprite Hat { get; private set; }

        [field: SerializeField] public int Ethnicity { get; private set; }
    }
}