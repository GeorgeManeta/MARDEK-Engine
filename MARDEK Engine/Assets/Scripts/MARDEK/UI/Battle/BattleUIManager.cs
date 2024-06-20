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
          [SerializeField] GameObject[] characterVisuals;
          [SerializeField] RectTransform[] enemyLayouts;
          private void Start()
        {
            foreach(var enemy in battleManager.EnemyCharacters)
                CreateCharacterUI(enemyUILayout, enemy);
            foreach(var playerCharacter in battleManager.PlayableCharacters)
                CreateCharacterUI(partyUILayout, playerCharacter);

            CreatePartyVisuals();
            CreateEnemyVisuals();
        }

        void CreateCharacterUI(RectTransform layout, Character character)
        {
            var ui = Instantiate(characterUIPrefab, layout).GetComponent<CharacterUI>();
            ui.AssignCharacter(character);
        }

          void CreatePartyVisuals()
          {
               int numberOfCharacters = battleManager.PlayableCharacters.Count;
               const int MaxPartySize = 4;


               for (int characterIndex = 0; characterIndex < MaxPartySize; characterIndex++)
               {
                    GameObject characterGameObject = characterVisuals[characterIndex];

                    // Only display the same number of characters in the party
                    if (characterIndex > numberOfCharacters - 1)
                    {
                         characterGameObject.SetActive(false);
                         continue;
                    }

                    Character character = battleManager.PlayableCharacters[characterIndex];

                    characterGameObject.SetActive(true);
                    CharacterUI ui = characterGameObject.GetComponent<CharacterUI>();
                    ui.AssignCharacter(character);
               }
          }

          void CreateEnemyVisuals()
          {
               //Ensure all layouts are disabled before enabling the correct one.
               foreach (RectTransform enemyLayout in enemyLayouts)
               {
                    enemyLayout.gameObject.SetActive(false);
               }

               // Replace this with an encounterType enum in the future. Chooses the correct layout to enable.
               int enemyCount = battleManager.EnemyCharacters.Count;
               RectTransform layout;
               switch (enemyCount)
               {
                    case 1:
                         layout = enemyLayouts[0];
                         break;
                    case 2:
                         layout = enemyLayouts[1];
                         break;
                    case 3:
                         layout = enemyLayouts[2];
                         break;
                    default:
                         layout = enemyLayouts[0];
                         break;
               }
               layout.gameObject.SetActive(true);

               // Start from the centre and work outwards, priorising the top...
               for (int enemyIndex = 0; enemyIndex < enemyCount; enemyIndex++)
               {
                    GameObject enemy = layout.GetChild(enemyIndex).gameObject;
                    enemy.SetActive(true);

                    Character character = battleManager.EnemyCharacters[enemyIndex];
                    CharacterUI ui = enemy.GetComponent<CharacterUI>();
                    ui.AssignCharacter(character);
               }
          }

     }
}