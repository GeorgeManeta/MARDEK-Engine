using UnityEngine;

namespace MARDEK.CharacterSystem
{
    public class AruanPortrait : MonoBehaviour, IPortrait
    {
        [field: SerializeField] public PortraitType PortraitType { get; private set; }

        [SerializeField] SpriteRenderer face;
        [SerializeField] SpriteRenderer leftEye;
        [SerializeField] SpriteRenderer leftBrow;
        [SerializeField] SpriteRenderer rightEye;
        [SerializeField] SpriteRenderer rightBrow;

        [SerializeField] SpriteRenderer mouth;

        [SerializeField] SpriteRenderer torso;

        public void SetPortrait(CharacterPortrait portrait)
        {
            face.sprite = portrait.Face;
            leftEye.sprite = portrait.Eye;
            rightEye.sprite = portrait.Eye;
            leftBrow.sprite = portrait.Eyebrow;
            rightBrow.sprite = portrait.Eyebrow;

            torso.sprite = portrait.Torso;

            mouth.sprite = portrait.Mouth;
        }
    }
}
