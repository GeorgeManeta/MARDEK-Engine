using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MARDEK.Battle;
using MARDEK.CharacterSystem;

namespace MARDEK.UI
{
    public class BattleUIManager : MonoBehaviour
    {
        public static BattleUIManager Instance { get; private set; }

        [SerializeField] BattleManager battleManager;
        [SerializeField] GameObject characterUIPrefab;
        [SerializeField] RectTransform enemyUILayout;
        [SerializeField] RectTransform partyUILayout;
        [SerializeField] GameObject CharacterInspectionCard;
        public Character characterBeingInspected { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            foreach(var enemy in battleManager.EnemyCharacters)
                CreateCharacterUI(enemyUILayout, enemy);
            while(enemyUILayout.transform.childCount < 4)
                CreateCharacterUI(enemyUILayout, null);
            
            foreach(var playerCharacter in battleManager.PlayableCharacters)
                CreateCharacterUI(partyUILayout, playerCharacter);
            while (partyUILayout.transform.childCount < 4)
                CreateCharacterUI(partyUILayout, null);
        }

        void CreateCharacterUI(RectTransform layout, Character character)
        {
            var ui = Instantiate(characterUIPrefab, layout).GetComponent<CharacterUI>();
            ui.AssignCharacter(character);
        }

        public void InspectCharacter(Character character)
        {
            CharacterInspectionCard.SetActive(false);
            characterBeingInspected = character;
            CharacterInspectionCard.SetActive(true);
        }
    }
}