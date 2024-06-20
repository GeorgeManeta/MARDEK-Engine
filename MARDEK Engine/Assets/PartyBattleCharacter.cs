using UnityEngine;

namespace MARDEK
{
     using MARDEK.Stats;
     using UI;
     [RequireComponent(typeof(CharacterUI))]
     public class PartyBattleCharacter : MonoBehaviour
     {
          [SerializeField] CharacterUI characterUI;
          [SerializeField] IntegerStat currentHP;

          private void Update()
          {
               bool dead = characterUI.character.GetStat(currentHP) < 0;
               if (dead)
                    gameObject.SetActive(false);
          }
     }
}
