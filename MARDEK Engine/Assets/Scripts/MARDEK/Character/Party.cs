using System.Collections.Generic;
using UnityEngine;
using MARDEK.Save;

namespace MARDEK.CharacterSystem
{
    public class Party : AddressableMonoBehaviour
    {
        static Party instance;

        public static Party getInstance()
        {
            if (instance == null) throw new System.ApplicationException("No Party instance found");
            return instance;
        }

        [SerializeField] List<Character> characters;
        public List<Character> Characters
        {
            get
            {
                var chars = new List<Character>();
                foreach (var playerCharacter in characters)
                    if (!playerCharacter.IsDummy())
                        chars.Add(playerCharacter);
                return chars;
            }
        }

        public List<Character> rawCharacters { get { return characters; } }

        [field: SerializeField] public List<Character> unselectedCharacters { get; private set; }

        [SerializeField] Character dummyCharacter;

        Character TakeCharacter(int index, bool isSelected)
        {
            if (isSelected && characters[index].isRequired)
            {
                throw new System.ArgumentException("Can't move required characters out of the party");
            }
            if (isSelected) return characters[index];
            else return unselectedCharacters[index];
        }

        override protected void Awake()
        {
            base.Awake();
            if (instance != null && instance != this)
            {
                throw new System.ApplicationException("Multiple Party instances detected");
            }
            instance = this;
        }

        void PutCharacter(Character character, int index, bool isSelected)
        {
            if (isSelected) characters[index] = character;
            else unselectedCharacters[index] = character;
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

            Debug.Log("New unselected characters are (" + unselectedCharacters.Count + "):");
            foreach (var character in unselectedCharacters)
            {
                Debug.Log(character);
            }
        }

        public int AddCharacter(Character newCharacter)
        {
            for (int index = 0; index < unselectedCharacters.Count; index++)
            {
                if (unselectedCharacters[index].IsDummy())
                {
                    unselectedCharacters[index] = newCharacter;
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
                    characters[index] = dummyCharacter;
                }
            }

            for (int index = 0; index < unselectedCharacters.Count; index++)
            {
                if (unselectedCharacters[index] == toRemove)
                {
                    unselectedCharacters[index] = dummyCharacter;
                }
            }
        }
    }
}
