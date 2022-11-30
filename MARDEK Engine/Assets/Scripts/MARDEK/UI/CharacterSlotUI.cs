using MARDEK.CharacterSystem;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MARDEK.UI
{
    public class CharacterSlotUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public static Sprite GetSprite(Character character)
        {
            if (character == null || character.CharacterInfo == null)
                return null;
            return character.CharacterInfo.WalkSprites.GetClipByIndex(0).GetSprite(Time.time % 1);
        }

        [SerializeField] Image backgroundImage;
        [SerializeField] Image characterImage;
        [SerializeField] Sprite baseSlotSprite;
        [SerializeField] Sprite hoverSlotSprite;
        [SerializeField] Sprite transparentSprite;

        Action clickAction;
        bool isRequired;

        public void SetClickAction(Action clickAction)
        {
            this.clickAction = clickAction;
        }

        public void SetCharacter(Character character)
        {
            if (character != null && character.CharacterInfo != null)
            {
                characterImage.sprite = GetSprite(character);
                if (character.isRequired) characterImage.color = new Color(1f, 1f, 1f, 0.5f);
                else characterImage.color = new Color(1f, 1f, 1f, 1f);
                isRequired = character.isRequired;
            }
            else
            {
                characterImage.sprite = transparentSprite;
                isRequired = false;
            }
        }

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            clickAction();
        }

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            if (!isRequired) backgroundImage.sprite = hoverSlotSprite;
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            backgroundImage.sprite = baseSlotSprite;
        }
    }
}
