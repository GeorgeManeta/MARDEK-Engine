using MARDEK.CharacterSystem;
using UnityEngine;

namespace MARDEK.UI
{
    public class PortraitBoxEntry : MonoBehaviour, PartyEntry
    {
        [SerializeField] PortraitDisplay portrait;

        public void SetCharacter(Character character)
        {
            portrait.SetPortrait(character.Profile.portrait);
        }
    }
}

