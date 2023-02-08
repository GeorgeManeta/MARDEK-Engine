using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MARDEK.Progress
{
    public class TreasureChest : Save.LocalSwitchBool
    {
        [SerializeField] GameObject closedEvent = null;
        [SerializeField] GameObject openEvent = null;

        private void Start()
        {
            UpdateChestObject();
        }

        public override void SetBoolValue(bool setValue)
        {
            base.SetBoolValue(setValue);
            UpdateChestObject();
        }

        void UpdateChestObject()
        {
            if (value)
            {
                closedEvent.SetActive(false);
                openEvent.SetActive(true);
            }
            else
            {
                closedEvent.SetActive(true);
                openEvent.SetActive(false);
            }
        }
    }
}