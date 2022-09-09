using MARDEK.CharacterSystem;
using UnityEngine;

namespace MARDEK.UI
{
    interface PartyEntry
    {
        void SetCharacter(Character character);

        GameObject gameObject { get; }
    }
}
