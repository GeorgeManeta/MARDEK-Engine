using UnityEngine;

namespace MARDEK.CharacterSystem
{
    public class ReptoidPortrait : PortraitPrefab
    {
        [field: SerializeField] public override PortraitType PortraitType { get; protected set; }

        [SerializeField] SpriteRenderer neck;
        [SerializeField] SpriteRenderer face;
        [SerializeField] SpriteRenderer mouth;
        [SerializeField] SpriteRenderer eye;
        [SerializeField] SpriteRenderer torso;

        public override void SetPortrait(CharacterPortrait portrait)
        {
            neck.sprite = portrait.Neck;
            face.sprite = portrait.Face;
            mouth.sprite = portrait.Mouth;
            torso.sprite = portrait.Torso;
            eye.sprite = portrait.Eye;
        }
    }
}
