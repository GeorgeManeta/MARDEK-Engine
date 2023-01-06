using MARDEK.Core;
using MARDEK.Event;
using MARDEK.Inventory;
using UnityEngine;

namespace MARDEK.UI
{
    public class OpenTreasureChestCommand : CommandBase
    {
        [SerializeField] TreasureChestMenu menu;
        [SerializeField] Item item;
        [SerializeField] int amount;

        override public void Trigger()
        {
            PlayerLocks.EventSystemLock++;
            menu.gameObject.SetActive(true);
            menu.Open(item, amount);
        }
    }
}
