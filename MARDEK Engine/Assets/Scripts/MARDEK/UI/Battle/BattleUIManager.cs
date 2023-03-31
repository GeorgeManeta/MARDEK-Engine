using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MARDEK.Battle;
using MARDEK.CharacterSystem;

namespace MARDEK.UI
{
    public class BattleUIManager : MonoBehaviour
    {
        [SerializeField] BattleManager battleManager;
        [SerializeField] GameObject characterUIPrefab;
        [SerializeField] RectTransform enemyUILayout;
        [SerializeField] RectTransform partyUILayout;

        private void Start()
        {
            foreach(var enemy in battleManager.EnemyCharacters)
                CreateCharacterUI(enemyUILayout, enemy);
            foreach(var playerCharacter in battleManager.PlayableCharacters)
                CreateCharacterUI(partyUILayout, playerCharacter);
        }

        void CreateCharacterUI(RectTransform layout, Character character)
        {
            var ui = Instantiate(characterUIPrefab, layout).GetComponent<CharacterUI>();
            ui.AssignCharacter(character);
        }
    }
}