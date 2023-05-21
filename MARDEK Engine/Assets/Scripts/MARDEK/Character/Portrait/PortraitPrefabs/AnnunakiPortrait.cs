using UnityEngine;
using System.Collections.Generic;

namespace MARDEK.CharacterSystem
{
    public class AnnunakiPortrait : PortraitPrefab
    {
        [field: SerializeField] public override PortraitType PortraitType { get; protected set; }

        [SerializeField] SpriteRenderer face;
        [SerializeField] SpriteRenderer eye;
        [SerializeField] SpriteRenderer torso;

        public override void SetPortrait(CharacterPortrait portrait)
        {
            face.sprite = portrait.Face;
            eye.sprite = portrait.Eye;
            torso.sprite = portrait.Torso;

            DefaultExpression.ApplyTransforms();
        }
    }
}
