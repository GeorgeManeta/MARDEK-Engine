using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MARDEK
{
     using CharacterSystem;
     using MARDEK.Stats;
     using UI;
     [RequireComponent(typeof(CharacterUI))]
     public class PartyBattleCharacter : MonoBehaviour
     {
          [SerializeField] CharacterUI characterUI;
          [SerializeField] IntegerStat currentHP;
          // Start is called before the first frame update
          void Start()
          {
                 
          }
          private void Update()
          {
               if (characterUI.character is null)
                    return;
               if (characterUI.character.GetStat(currentHP) < 0)
                    gameObject.SetActive(false);
          }

     }
}
