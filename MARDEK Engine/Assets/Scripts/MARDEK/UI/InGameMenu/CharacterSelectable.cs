using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MARDEK.CharacterSystem;
using UnityEngine.Serialization;

namespace MARDEK.UI
{
    public class CharacterSelectable : SelectableWithCurrentSelected<CharacterSelectable>
    {
        [SerializeField] Character character;
        public Character Character { get { return character; } }
        public override bool IsValid()
        {
            return Character.CharacterInfo != null;
        }
    }
}