using MARDEK.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MARDEK.UI
{
    public class PlotItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] Image image;

        PlotItem plotItem;
        Image selectedItemHeaderColor;
        TMP_Text selectedItemHeader;
        TMP_Text selectedItemDescription;

        public void Init(
            PlotItem plotItem, Image selectedItemHeaderColor, 
            TMP_Text selectedItemHeader, TMP_Text selectedItemDescription
        )
        {
            this.plotItem = plotItem;
            this.selectedItemHeaderColor = selectedItemHeaderColor;
            this.selectedItemHeader = selectedItemHeader;
            this.selectedItemDescription = selectedItemDescription;
            image.sprite = plotItem.sprite;
        }

        public void OnPointerEnter(PointerEventData pointerEvent)
        {
            selectedItemHeaderColor.color = plotItem.element.textColor;
            selectedItemHeader.text = plotItem.displayName;
            selectedItemDescription.text = plotItem.description;
        }

        public void OnPointerExit(PointerEventData pointerEvent)
        {
            selectedItemHeaderColor.color = new Color();
            selectedItemHeader.text = "";
            selectedItemDescription.text = "";
        }
    }
}
