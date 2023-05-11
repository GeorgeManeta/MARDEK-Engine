using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MARDEK.CharacterSystem
{
    public class AnnunakiPortrait : MonoBehaviour
    {
        [SerializeField] SpriteRenderer face;
        [SerializeField] SpriteRenderer eye;
        [SerializeField] SpriteRenderer armour;

        public void SetPortrait(CharacterPortrait portrait)
        {
            face.sprite = portrait.Face;
            eye.sprite = portrait.Eye;
            armour.sprite = portrait.Armour;
        }
    }
}
