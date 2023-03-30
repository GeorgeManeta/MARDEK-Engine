using MARDEK.Audio;
using MARDEK.CharacterSystem;
using MARDEK.Core;
using MARDEK.Inventory;
using MARDEK.Save;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace MARDEK.UI
{
    using Progress;
    public class TreasureChestMenuUI : MonoBehaviour
    {
        public static TreasureChestMenuUI instance { get; private set; }

        [SerializeField] GameObject container;
        [SerializeField] TMP_Text itemName;
        [SerializeField] TMP_Text itemDescription;
        [SerializeField] Image itemImage;
        [SerializeField] Image itemNameBackground;
        [SerializeField] CharacterSelectable[] characters;
        [SerializeField] TMP_Text[] alreadyHasLabels;
        [SerializeField] InventorySpace[] inventorySpaces;
        [SerializeField] TMP_Text amountText;
        [SerializeField] TMP_Text keyName;

        [SerializeField] InputActionReference interactInput;
        [SerializeField] AudioObject takeSound;
        [SerializeField] AudioObject cancelSound;
        [SerializeField] AudioObject rejectSound;

        Item currentItem;
        int currentAmount;

        public bool IsOpen
        {
            get
            {
                return container.activeSelf;
            }
        }
        public bool SuccessfullyTookItem { get; private set; }

        void Awake()
        {
            if (instance == null) instance = this;
        }

        public void Open(Item item, int amount)
        {
            PlayerLocks.EventSystemLock++;
            container.SetActive(true);
            currentItem = item;
            currentAmount = amount;

            itemName.text = item.displayName;
            itemDescription.text = item.description;
            itemImage.sprite = item.sprite;
            itemNameBackground.color = item.element.textColor;
            characters[0].Select(false);
            for (int index = 0; index < 4; index++)
            {
                if (index < Party.Instance.Characters.Count)
                {
                    var inventory = Party.Instance.Characters[index].Inventory;
                    alreadyHasLabels[index].text = inventory.CountItem(item).ToString();
                    inventorySpaces[index].gameObject.SetActive(true);
                    inventorySpaces[index].UpdateImage(inventory);
                }
                else
                {
                    alreadyHasLabels[index].text = "";
                    inventorySpaces[index].gameObject.SetActive(false);
                }
            }
            amountText.text = "x " + amount;
            var bindPath = interactInput.action.bindings[0].effectivePath;
            var options = InputControlPath.HumanReadableStringOptions.None;
            var fullKeyName = InputControlPath.ToHumanReadableString(bindPath, options);
            keyName.text = fullKeyName.Substring(0, 2).Trim();
        }

        int getSelectedCharacterIndex()
        {
            for (int currentIndex = 0; currentIndex < characters.Length; currentIndex++)
            {
                if (characters[currentIndex] == CharacterSelectable.currentSelected) return currentIndex;
            }
            throw new System.ApplicationException("No selected character?");
        }

        public void ScrollCharacter(InputAction.CallbackContext ctx)
        {
            var value = ctx.ReadValue<Vector2>();
            if (value.x > 0)
            {
                int currentIndex = getSelectedCharacterIndex();
                characters[currentIndex].Deselect();
                currentIndex++;
                if (currentIndex >= characters.Length || !characters[currentIndex].IsValid()) currentIndex = 0;
                characters[currentIndex].Select();
            }
            else if (value.x < 0)
            {
                int currentIndex = getSelectedCharacterIndex();
                characters[currentIndex].Deselect();
                currentIndex--;
                if (currentIndex < 0) currentIndex = characters.Length - 1;
                while (!characters[currentIndex].IsValid()) currentIndex--;
                characters[currentIndex].Select();
            }
        }

        public void ExitWithoutTakingItem()
        {
            SuccessfullyTookItem = false;
            container.SetActive(false);
            PlayerLocks.EventSystemLock--;
            AudioManager.PlaySoundEffect(cancelSound);
        }

        public void TryGiveItemToSelectedCharacter()
        {
            var character = Party.Instance.Characters[getSelectedCharacterIndex()];
            if (character.Inventory.AddItem(currentItem, currentAmount))
            {
                SuccessfullyTookItem = true;
                container.SetActive(false);
                PlayerLocks.EventSystemLock--;
                AudioManager.PlaySoundEffect(takeSound);
            }
            else
            {
                AudioManager.PlaySoundEffect(rejectSound);
            }
        }
    }
}
