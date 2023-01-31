using MARDEK.Audio;
using MARDEK.CharacterSystem;
using MARDEK.Event;
using MARDEK.Inventory;
using MARDEK.Save;
using UnityEngine;

namespace MARDEK.UI
{
    public class OpenTreasureChestCommand : CommandBase
    {
        [SerializeField] Item item;
        [SerializeField] int amount;
        [SerializeField] ChestID chestID;
        [SerializeField] AudioObject openSound;

        override public void Trigger()
        {
            AudioManager.PlaySoundEffect(openSound);
            if (item != null) TreasureChestMenu.instance.Open(item, amount, chestID);
            else
            {
                GeneralProgressData.instance.MarkChestAsOpened(chestID);
                Party.Instance.money += amount;
                MoneyPopup.instance.Show(amount);
            }
        }
    }
}
