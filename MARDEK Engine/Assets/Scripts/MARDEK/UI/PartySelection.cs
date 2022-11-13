using MARDEK.Audio;
using MARDEK.CharacterSystem;
using MARDEK.Core;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace MARDEK.UI
{
    public class PartySelection : MonoBehaviour
    {
        [SerializeField] List<CharacterSlotUI> selectedCharacterSlots;
        [SerializeField] List<CharacterSlotUI> unselectedCharacterSlots;
        [SerializeField] AudioObject rejectSound;
        [SerializeField] Sprite transparentSprite;
        [SerializeField] Image mouseCharacterImage;

        Party party { get { return Party.getInstance(); } }

        int mouseCharacterIndex = -1;
        bool isMouseCharacterSelected;

        void OnEnable()
        {
            PlayerLocks.UISystemLock += 1;
            SetClickActions();
            RefreshSlots();
        }

        void SetClickActions()
        {
            for (int index = 0; index < selectedCharacterSlots.Count; index++)
            {
                int rememberIndex = index;
                selectedCharacterSlots[index].SetClickAction(() => HandleClick(rememberIndex, true));
            }
            for (int index = 0; index < unselectedCharacterSlots.Count; index++)
            {
                int rememberIndex = index;
                unselectedCharacterSlots[index].SetClickAction(() => HandleClick(rememberIndex, false));
            }
        }

        void HandleClick(int index, bool isSelected)
        {
            if (isSelected && party.rawCharacters[index].isRequired)
            {
                AudioManager.PlaySoundEffect(rejectSound);
                return;
            }

            if (index == mouseCharacterIndex)
            {
                mouseCharacterIndex = -1;
                return;
            }

            if (mouseCharacterIndex != -1)
            {
                party.SwapCharacters(mouseCharacterIndex, isMouseCharacterSelected, index, isSelected);
                bool isDummy;
                if (isMouseCharacterSelected) isDummy = party.rawCharacters[mouseCharacterIndex].IsDummy();
                else isDummy = party.unselectedCharacters[mouseCharacterIndex].IsDummy();

                if (isDummy)
                {
                    mouseCharacterIndex = -1;
                }
            }
            else
            {
                bool isDummy;
                if (isSelected) isDummy = party.rawCharacters[index].IsDummy();
                else isDummy = party.unselectedCharacters[index].IsDummy();

                if (!isDummy)
                {
                    mouseCharacterIndex = index;
                    isMouseCharacterSelected = isSelected;
                }
            } 
        }

        void OnDisable()
        {
            PlayerLocks.UISystemLock -= 1;
        }

        void Update()
        {
            mouseCharacterImage.transform.position = new Vector2(
                Mouse.current.position.x.ReadValue(), Mouse.current.position.y.ReadValue()
            );
        }

        void FixedUpdate()
        {
            RefreshSlots();
        }

        public void RefreshSlots()
        {
            if (selectedCharacterSlots.Count != party.rawCharacters.Count)
            {
                throw new System.ApplicationException("The number of selected character slots should be equal to the number of selected characters");
            }
            for (int index = 0; index < selectedCharacterSlots.Count; index++) {
                if (!isMouseCharacterSelected || mouseCharacterIndex != index)
                {
                    selectedCharacterSlots[index].SetCharacter(party.rawCharacters[index]);
                }
                else
                {
                    selectedCharacterSlots[index].SetCharacter(null);
                }
            }

            if (unselectedCharacterSlots.Count != party.unselectedCharacters.Count)
            {
                throw new System.ApplicationException(
                    "The number of unselected character (" + unselectedCharacterSlots.Count + 
                    ") slots should be equal to the number of unselected characters (" +
                    party.unselectedCharacters.Count + ")"
                );
            }
            for (int index = 0; index < unselectedCharacterSlots.Count; index++)
            {
                if (isMouseCharacterSelected || mouseCharacterIndex != index)
                {
                    unselectedCharacterSlots[index].SetCharacter(party.unselectedCharacters[index]);
                }
                else
                {
                    unselectedCharacterSlots[index].SetCharacter(null);
                }
            }

            if (mouseCharacterIndex != -1)
            {
                if (isMouseCharacterSelected)
                {
                    mouseCharacterImage.sprite = CharacterSlotUI.GetSprite(party.rawCharacters[mouseCharacterIndex]);
                }
                else
                {
                    mouseCharacterImage.sprite = CharacterSlotUI.GetSprite(party.unselectedCharacters[mouseCharacterIndex]);
                }
            }
            else
            {
                mouseCharacterImage.sprite = transparentSprite;
            }
        }
    }
}
