using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using MARDEK.CharacterSystem;
using UnityEngine.EventSystems;
using MARDEK.Battle;
using Codice.Client.BaseCommands.BranchExplorer;

namespace MARDEK.UI
{
    public class CharacterUI : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] BattleManager battleManager;
        [SerializeField] bool playableOrEnemy;
        public Character character { get; private set; }
        [SerializeField] GameObject basePanel;

        private void Start()
        {
            UpdateCharacter();
        }

        void UpdateCharacter()
        {
            basePanel.SetActive(false);
            var index = transform.GetSiblingIndex();
            List<Character> list = battleManager.EnemyCharacters;
            if (playableOrEnemy)
                list = battleManager.PlayableCharacters;
            if (index < list.Count)
            {
                character = list[index];
                basePanel.SetActive(true);
            }
            else
            {
                character = null;
                basePanel.SetActive(false);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            BattleUIManager.Instance.InspectCharacter(character);
        }
    }
}