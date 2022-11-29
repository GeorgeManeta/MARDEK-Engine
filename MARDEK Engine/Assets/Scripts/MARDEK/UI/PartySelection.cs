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

        Party Party { get { return Party.Instance; } }
        Character mouseCharacter = new();

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
            Character characterAtIndex = (isSelected ? Party.Characters : Party.BenchedCharacters)[index];
            if (characterAtIndex.isRequired)
            {
                AudioManager.PlaySoundEffect(rejectSound);
                return;
            }

            Party.SetCharacterAtIndex(mouseCharacter, index, !isSelected);
            mouseCharacter = characterAtIndex;
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
        void LateUpdate()
        {
            RefreshSlots();
        }
        public void RefreshSlots()
        {
            for (int index = 0; index < selectedCharacterSlots.Count; index++)
            {
                if(index < Party.Characters.Count)
                    selectedCharacterSlots[index].SetCharacter(Party.Characters[index]);
                else
                    selectedCharacterSlots[index].SetCharacter(null);
            }
            for (int index = 0; index < unselectedCharacterSlots.Count; index++)
            {
                if(index < Party.BenchedCharacters.Count)
                    unselectedCharacterSlots[index].SetCharacter(Party.BenchedCharacters[index]);
                else
                    unselectedCharacterSlots[index].SetCharacter(null);
            }

            if (mouseCharacter == null)
                mouseCharacterImage.sprite = transparentSprite;
            else
                mouseCharacterImage.sprite = CharacterSlotUI.GetSprite(mouseCharacter);
        }
    }
}
