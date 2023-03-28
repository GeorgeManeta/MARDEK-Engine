using MARDEK.Audio;
using MARDEK.CharacterSystem;
using MARDEK.Event;
using MARDEK.Inventory;
using MARDEK.Save;
using UnityEngine;

namespace MARDEK.UI
{
    using Progress;
    public class OpenTreasureChestCommand : CommandBase
    {
        TreasureChest chest;
        [SerializeField] Item item;
        [SerializeField] int amount;
        [SerializeField] AudioObject openSound;

        private void OnValidate()
        {
            chest = GetComponentInParent<TreasureChest>();
        }

        override public void Trigger()
        {
            AudioManager.PlaySoundEffect(openSound);
            if (item != null) TreasureChestMenu.instance.Open(item, amount, chest);
            else
            {
                Party.Instance.money += amount;
                MoneyPopup.instance.Show(amount);
                chest.SetBoolValue(true);
            }
        }
    }
}