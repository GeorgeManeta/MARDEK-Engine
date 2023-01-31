using System.Collections.Generic;
using UnityEngine;

namespace MARDEK.CharacterSystem
{
    using Save;
    using Inventory;
    public class Party : AddressableMonoBehaviour
    {
        public static Party Instance { get; private set; }

        [field: SerializeField] public List<Character> Characters { get; private set; }
        [field: SerializeField] public List<Character> BenchedCharacters { get; private set; }
        public List<PlotItem> plotItems = new List<PlotItem>();
        public int money;
        
        override protected void Awake()
        {
            base.Awake();
            if (Instance != null && Instance != this)
            {
                throw new System.ApplicationException("Multiple Party instances detected");
            }
            Instance = this;
        }

        public void SetCharacterAtIndex(Character character, int index, bool isBenched)
        {
            if (isBenched)
                BenchedCharacters[index] = character;
            else
                Characters[index] = character;
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
            for (int index = 0; index < Characters.Count; index++)
            {
                if (Characters[index] == toRemove)
                {
                    Characters[index] = null;
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
