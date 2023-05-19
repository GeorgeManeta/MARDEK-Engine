using UnityEngine;

namespace MARDEK.CharacterSystem
{
    public class ReptoidPortrait : MonoBehaviour, IPortrait
    {
        [field: SerializeField] public PortraitType PortraitType { get; private set; }

        [SerializeField] SpriteRenderer neck;
        [SerializeField] SpriteRenderer face;
        [SerializeField] SpriteRenderer mouth;
        [SerializeField] SpriteRenderer eye;
        [SerializeField] SpriteRenderer torso;

        public void SetPortrait(CharacterPortrait portrait)
        {
            neck.sprite = portrait.Neck;
            face.sprite = portrait.Face;
            mouth.sprite = portrait.Mouth;
            torso.sprite = portrait.Torso;
            eye.sprite = portrait.Eye;
        }

        public void SetExpression(PortraitExpression expression)
        {

        }
    }
}
