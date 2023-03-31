using MARDEK.CharacterSystem;
using MARDEK.Inventory;
using System.Collections.Generic;
using UnityEngine;

namespace MARDEK.UI
{
    using Progress;
    public class BattleLoot : MonoBehaviour
    {
        Item[] currentItems = new Item[4];
        int[] currentAmounts = new int[4];

        // TODO Remove after testing
        [SerializeField] List<Item> initialItems;
        [SerializeField] List<int> initialAmounts;
        [SerializeField] List<BattleLootEntry> entries;
        [SerializeField] List<InventorySpace> inventorySpaces;

        void Start()
        {
            // TODO Remove after testing
            SetLoot(initialItems, initialAmounts);
        }

        void Reset()
        {
            for (int index = 0; index < currentAmounts.Length; index++) {
                currentAmounts[index] = 0;
            }
        }

        public void SetLoot(List<Item> items, List<int> amounts)
        {
            if (items.Count != amounts.Count) throw new System.ArgumentException("#items must be equal to #amounts");
            Reset();

            for (int index = 0; index < items.Count; index++) {
                currentItems[index] = items[index];
                currentAmounts[index] = amounts[index];
                entries[index].gameObject.SetActive(true);
            }

            for (int index = 0; index < entries.Count; index++) {
                entries[index].SetItem(index, currentItems, currentAmounts);
            }

            UpdateInventorySpaceGrids();
        }

        void UpdateInventorySpaceGrids()
        {
            for (int index = 0; index < inventorySpaces.Count; index++)
            {
                if (index < Party.Instance.Characters.Count)
                {
                    inventorySpaces[index].gameObject.SetActive(true);
                    inventorySpaces[index].UpdateImage(Party.Instance.Characters[index].Inventory);
                }
                else inventorySpaces[index].gameObject.SetActive(false);
            }
        }

        void UpdateLoot()
        {
            List<Item> newItems = new List<Item>();
            List<int> newAmounts = new List<int>();

            for (int index = 0; index < currentItems.Length; index++)
            {
                if (currentAmounts[index] != 0)
                {
                    newItems.Add(currentItems[index]);
                    newAmounts.Add(currentAmounts[index]);
                }
            }

            SetLoot(newItems, newAmounts);
        }

        public void Interact()
        {
            BattleLootSelectable.currentlySelected.Interact(currentItems, currentAmounts);
            UpdateLoot();
        }
    }
}
