using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MARDEK.Progress
{
    using Save;
    using Event;
    using Inventory;
    using Audio;
    using MARDEK.UI;

    public class TreasureChestCommand : OngoingCommand
    {
        [SerializeField, HideInInspector] LocalSwitchBool localSwitch;
        [Tooltip("Leave null for gold")]
        [SerializeField] Item item;
        [SerializeField] int amount = 1;
        [SerializeField] AudioObject openSound;
        [SerializeField] Sprite openChestSprite;

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (localSwitch == null && UnityEditor.PrefabUtility.IsPartOfNonAssetPrefabInstance(gameObject))
                localSwitch = gameObject.AddComponent<LocalSwitchBool>();
            _waitExcecutionEnd = true;
        }
#endif

        private void Start()
        {
            if (localSwitch.GetBoolValue())
                SetOpenChestSprite();
        }

        public override void Trigger()
        {
            if (localSwitch.GetBoolValue())
                return;

            AudioManager.PlaySoundEffect(openSound);

            if (item != null)
                TreasureChestMenuUI.instance.Open(item, amount);
            else
            {
                Party.Instance.money += amount;
                MoneyPopup.instance.Show(amount);
                SetChestAsOpen();
            }
        }

        public override bool IsOngoing()
        {
            if (TreasureChestMenuUI.instance.IsOpen)
                return true;
            if (TreasureChestMenuUI.instance.SuccessfullyTookItem)
                SetChestAsOpen();
            return false;
        }

        void SetChestAsOpen()
        {
            localSwitch.SetBoolValue(true);
            SetOpenChestSprite();
        }

        void SetOpenChestSprite()
        {
            var spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer)
                spriteRenderer.sprite = openChestSprite;
        }
    }
}