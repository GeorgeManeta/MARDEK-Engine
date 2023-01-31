using MARDEK.Event;
using MARDEK.Inventory;
using UnityEngine;

namespace MARDEK.UI
{
    public class OpenTreasureChestCommand : CommandBase
    {
        [SerializeField] Item item;
        [SerializeField] int amount;

        override public void Trigger()
        {
            TreasureChestMenu.instance.Open(item, amount);
        }
    }
}
