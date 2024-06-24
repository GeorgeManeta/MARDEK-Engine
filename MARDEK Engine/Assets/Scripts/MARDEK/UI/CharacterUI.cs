using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using MARDEK.CharacterSystem;
using UnityEngine.EventSystems;

namespace MARDEK.UI
{
    using Battle;
    public class CharacterUI : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] BattleManager battleManager;
        [SerializeField] bool playableOrEnemy;
        public BattleCharacter character { get; private set; }
        [SerializeField] GameObject basePanel;

        private void Start()
        {
            UpdateCharacter();
        }

        void UpdateCharacter()
        {
            basePanel.SetActive(false);
            var index = transform.GetSiblingIndex();
            var list = battleManager.EnemyBattleParty;
            if (playableOrEnemy)
                list = battleManager.PlayerBattleParty;
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
            BattleUIManager.Instance.InspectCharacter(character.Character);
        }
    }
}