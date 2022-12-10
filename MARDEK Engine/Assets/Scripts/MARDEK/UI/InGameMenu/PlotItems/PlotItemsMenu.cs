using MARDEK.Inventory;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MARDEK.UI
{
    public class PlotItemsMenu : MonoBehaviour
    {
        [SerializeField] GridLayoutGroup grid;
        [SerializeField] Image selectedItemHeaderColor;
        [SerializeField] TMP_Text selectedItemHeader;
        [SerializeField] TMP_Text selectedItemDescription;
        
        [SerializeField] List<PlotItem> allPlotItems;
        [SerializeField] GameObject plotItemSlotPrefab;

        void Awake()
        {
            UpdateEntries();
        }

        void ClearOldEntries()
        {
            var oldChildren = new List<Transform>();
            foreach (Transform oldChild in grid.gameObject.transform)
            {
                oldChildren.Add(oldChild);
            }
            foreach (Transform oldChild in oldChildren)
            {
                oldChild.gameObject.SetActive(false);
                Destroy(oldChild.gameObject);
            }
        }

        void UpdateEntries()
        {
            ClearOldEntries();
            foreach (PlotItem plotItem in allPlotItems)
            {
                if (plotItem.isOwned)
                {
                    PlotItemSlot itemSlot = Instantiate(this.plotItemSlotPrefab, new Vector3(0, 0, 0), Quaternion.identity).GetComponent<PlotItemSlot>();
                    itemSlot.Init(plotItem, selectedItemHeaderColor, selectedItemHeader, selectedItemDescription);
                    itemSlot.transform.SetParent(this.grid.transform);
                    itemSlot.transform.localScale = new Vector3(1f, 1f, 1f);
                }
            }
        }
    }
}
