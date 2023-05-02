using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using MARDEK.CharacterSystem;
using UnityEngine.EventSystems;

namespace MARDEK.UI
{
    public class CharacterUI : MonoBehaviour, IPointerClickHandler
    {
        public Character character { get; private set; }
        [SerializeField] GameObject basePanel;

        private void Awake()
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        public void AssignCharacter(Character c)
        {
            if (c == null)
                basePanel.SetActive(false);
            character = c;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            BattleUIManager.Instance.InspectCharacter(character);
        }
    }
}