using System.Collections.Generic;
using UnityEngine;

namespace MARDEK.CharacterSystem
{
    public class AruanPortrait : PortraitPrefab
    {
        [field: SerializeField] public override PortraitType PortraitType { get; protected set; }

        [SerializeField] SpriteRenderer face;
        [SerializeField] SpriteRenderer leftEye;
        [SerializeField] SpriteRenderer leftBrow;
        [SerializeField] SpriteRenderer rightEye;
        [SerializeField] SpriteRenderer rightBrow;

        [SerializeField] SpriteRenderer mouth;

        [SerializeField] SpriteRenderer torso;

        public override void SetPortrait(CharacterPortrait portrait)
        {
            face.sprite = portrait.Face;
            leftEye.sprite = portrait.Eye;
            rightEye.sprite = portrait.Eye;
            leftBrow.sprite = portrait.Eyebrow;
            rightBrow.sprite = portrait.Eyebrow;
            torso.sprite = portrait.Torso;
            mouth.sprite = portrait.Mouth;

            NormalExpression.ApplyTransforms();
        }
    }
}
