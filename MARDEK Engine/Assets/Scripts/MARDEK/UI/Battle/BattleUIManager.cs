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
        [SerializeField] GameObject CharacterInspectionCard;
        public Character characterBeingInspected { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public void InspectCharacter(Character character)
        {
            CharacterInspectionCard.SetActive(false);
            characterBeingInspected = character;
            CharacterInspectionCard.SetActive(true);
        }
    }
}