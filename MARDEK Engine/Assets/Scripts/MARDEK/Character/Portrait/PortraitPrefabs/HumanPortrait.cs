using UnityEngine;
using System.Linq;

namespace MARDEK.CharacterSystem
{
    using UnityEngine.UI;
    public class HumanPortrait : PortraitPrefab
    {
        [field:SerializeField] public override PortraitType PortraitType { get; protected set; }

        [SerializeField] Image neck;
        [SerializeField] Image face;

        [SerializeField] Image leftEye;
        [SerializeField] Image rightEye;
        [SerializeField] Image leftBrow;
        [SerializeField] Image rightBrow;

        [SerializeField] Image hair;
        [SerializeField] Image torso;

        [SerializeField] Image browShadow;

        [SerializeField] GameObject MouthExpressions;
        public override void SetPortrait(CharacterPortrait portrait)
        {
            neck.sprite = portrait.Neck;
            face.sprite = portrait.Face;
            hair.sprite = portrait.Hair;
            torso.sprite = portrait.Torso;

            leftEye.sprite = portrait.Eye;
            rightEye.sprite = portrait.Eye;
            leftBrow.sprite = portrait.Eyebrow;
            rightBrow.sprite = portrait.Eyebrow;

            SetSkinColor(GetSkinColor(portrait));
        }

        private Color GetSkinColor(CharacterPortrait portrait)
        {
            switch (portrait.Ethnicity)
            {
                case "zombie":
                    // hex #8C9BA6 (grey zombie skin color)
                    return new Color((float)0.5490196, (float)0.6078432, (float)0.6509804);

                case "clave":
                    // hex #C29C58 (light peach/brown, used by Clavis)
                    return new Color((float)0.7607844, (float)0.6117647, (float)0.345098);

                case "2":
                    // hex #5F330E (dark brown; used by Zach)
                    return new Color((float)0.372549, (float)0.2, (float)0.05490196);

                case "3":
                    // hex #E1B67F (light peach; used by "arabs", Muriance, others)
                    return new Color((float)0.882353, (float)0.7137255, (float)0.4980392);

                default:
                    return Color.white;
            }
        }

        private void SetSkinColor(Color skinColor) {
            browShadow.color = skinColor;

            // set lip color for male and child mouth
            // TODO find better way to do this
            if (MouthExpressions != null)
            {
                SpriteRenderer[] LipArray = MouthExpressions.GetComponentsInChildren<SpriteRenderer>()
                    .Where((e, i) => e.name == "Lips").ToArray();

                foreach (SpriteRenderer lip in LipArray)
                {
                    lip.color = skinColor;
                }
            }
        }
    }
}
