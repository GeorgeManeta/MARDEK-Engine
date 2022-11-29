using System.Collections.Generic;
using UnityEngine;
using MARDEK.Save;

namespace MARDEK.CharacterSystem
{
    public class Party : AddressableMonoBehaviour
    {
        public static Party Instance { get; private set; }
        
        public List<Character> Characters
        {
            get
            {
                return characters;
                //var chars = new List<Character>();
                //foreach (var playerCharacter in characters)
                //    if (!playerCharacter.IsDummy())
                //        chars.Add(playerCharacter);
                //return chars;
            }
        }
        public List<Character> rawCharacters { get { return characters; } }
        [SerializeField] List<Character> characters;
        [field: SerializeField] public List<Character> BenchedCharacters { get; private set; }

        override protected void Awake()
        {
            base.Awake();
            if (Instance != null && Instance != this)
            {
                throw new System.ApplicationException("Multiple Party instances detected");
            }
            Instance = this;
        }
        
        Character TakeCharacter(int index, bool isSelected)
        {
            if (isSelected && characters[index].isRequired)
            {
                throw new System.ArgumentException("Can't move required characters out of the party");
            }
            if (isSelected) return characters[index];
            else return BenchedCharacters[index];
        }
        void PutCharacter(Character character, int index, bool isSelected)
        {
            if (isSelected) characters[index] = character;
            else BenchedCharacters[index] = character;
        }
        public void SwapCharacters(int index1, bool isSelected1, int index2, bool isSelected2)
        {
            Character oldCharacter1 = TakeCharacter(index1, isSelected1);
            Character oldCharacter2 = TakeCharacter(index2, isSelected2);
            PutCharacter(oldCharacter1, index2, isSelected2);
            PutCharacter(oldCharacter2, index1, isSelected1);

            Debug.Log("New selected characters are (" + rawCharacters.Count + "):");
            foreach (var character in rawCharacters)
            {
                Debug.Log(character);
            }

            Debug.Log("New unselected characters are (" + BenchedCharacters.Count + "):");
            foreach (var character in BenchedCharacters)
            {
                Debug.Log(character);
            }
        }
        public int AddCharacter(Character newCharacter)
        {
            for (int index = 0; index < BenchedCharacters.Count; index++)
            {
                if (BenchedCharacters[index] == null)
                {
                    BenchedCharacters[index] = newCharacter;
                    return index;
                }
            }

            throw new System.ApplicationException("There are not enough unselected character slots left");
        }
        public void RemoveCharacter(Character toRemove)
        {
            for (int index = 0; index < characters.Count; index++)
            {
                if (characters[index] == toRemove)
                {
                    characters[index] = null;
                }
            }

            for (int index = 0; index < BenchedCharacters.Count; index++)
            {
                if (BenchedCharacters[index] == toRemove)
                {
                    BenchedCharacters[index] = null;
                }
            }
        }
    }
}
