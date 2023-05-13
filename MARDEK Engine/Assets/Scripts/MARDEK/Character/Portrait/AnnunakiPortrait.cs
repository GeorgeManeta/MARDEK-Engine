using UnityEngine;

namespace MARDEK.CharacterSystem
{
    public class AnnunakiPortrait : MonoBehaviour, IPortrait
    {
        [SerializeField] SpriteRenderer face;
        [SerializeField] SpriteRenderer eye;
        [SerializeField] SpriteRenderer torso;

        public void SetPortrait(CharacterPortrait portrait)
        {
            face.sprite = portrait.Face;
            eye.sprite = portrait.Eye;
            torso.sprite = portrait.Torso;
        }
    }
}
