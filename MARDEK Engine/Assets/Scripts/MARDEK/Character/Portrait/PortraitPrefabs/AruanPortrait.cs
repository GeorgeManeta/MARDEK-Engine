using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MARDEK.CharacterSystem
{
    public class AruanPortrait : PortraitPrefab
    {
        [field: SerializeField] public override PortraitType PortraitType { get; protected set; }

        [SerializeField] Image face;
        [SerializeField] Image leftEye;
        [SerializeField] Image leftBrow;
        [SerializeField] Image rightEye;
        [SerializeField] Image rightBrow;

        [SerializeField] Image mouth;

        [SerializeField] Image torso;

        public override void SetPortrait(CharacterPortrait portrait)
        {
            face.sprite = portrait.Face;
            leftEye.sprite = portrait.Eye;
            rightEye.sprite = portrait.Eye;
            leftBrow.sprite = portrait.Eyebrow;
            rightBrow.sprite = portrait.Eyebrow;
            torso.sprite = portrait.Torso;
            mouth.sprite = portrait.Mouth;

            DefaultExpression.ApplyTransforms();
        }
    }
}
