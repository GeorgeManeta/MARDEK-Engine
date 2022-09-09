using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MARDEK.Save;

namespace MARDEK.CharacterSystem
{
    public class Party : AddressableMonoBehaviour
    {
        [SerializeField] List<Character> characters;
        public List<Character> Characters
        {
            get
            {
                var chars = new List<Character>();
                foreach (var playerCharacter in characters)
                    if (playerCharacter.CharacterInfo != null)
                        chars.Add(playerCharacter);
                return chars;
            }
        }
    }
}