using UnityEngine;

namespace MARDEK.CharacterSystem
{
    public class HumanPortrait : MonoBehaviour, IPortrait
    {
        [SerializeField] SpriteRenderer neck;
        [SerializeField] SpriteRenderer face;

        [SerializeField] SpriteRenderer leftEye;
        [SerializeField] SpriteRenderer rightEye;
        [SerializeField] SpriteRenderer leftBrow;
        [SerializeField] SpriteRenderer rightBrow;

        [SerializeField] SpriteRenderer hair;
        [SerializeField] SpriteRenderer torso;

        public void SetPortrait(CharacterPortrait portrait)
        {
            neck.sprite = portrait.Neck;
            face.sprite = portrait.Face;
            hair.sprite = portrait.Hair;
            torso.sprite = portrait.Torso;

            leftEye.sprite = portrait.Eye;
            rightEye.sprite = portrait.Eye;
            leftBrow.sprite = portrait.Eyebrow;
            rightBrow.sprite = portrait.Eyebrow;
        }
    }
}
