using UnityEngine;
using MARDEK.Core;

namespace MARDEK.CharacterSystem
{
    [CreateAssetMenu(menuName ="MARDEK/Character/CharacterPortrait")]
    public class CharacterPortrait : ScriptableObject
    {
        [field: SerializeField] public PortraitType PortraitType { get; private set; }

        [field: SerializeField] public Sprite Face { get; private set; }
        [field: SerializeField] public Sprite Hair { get; private set; }
        [field: SerializeField] public Sprite Eye { get; private set; }
        [field: SerializeField] public Sprite Eyebrow { get; private set; }
        [field: SerializeField] public Sprite Neck { get; private set; }
        [field: SerializeField] public Sprite Torso { get; private set; }
        [field: SerializeField] public Sprite Mouth { get; private set; }

        // TODO implement
        [field: SerializeField] public Sprite Hat { get; private set; }

        [field: SerializeField] public string Ethnicity { get; private set; }
        [field: SerializeField] public int Facemask { get; private set; }

        // CharacterPortrait creator for use by PortraitImporter.cs
        public CharacterPortrait(PortraitType portraitType, Sprite face,
            Sprite hair, Sprite eye, Sprite eyebrow, Sprite neck, Sprite torso,
            Sprite mouth, Sprite hat, string ethnicity, int facemask)
        {
            PortraitType = portraitType;
            Face = face;
            Hair = hair;
            Eye = eye;
            Eyebrow = eyebrow;
            Neck = neck;
            Torso = torso;
            Mouth = mouth;
            Hat = hat;
            Ethnicity = ethnicity;
            Facemask = facemask;
        }
    }
}