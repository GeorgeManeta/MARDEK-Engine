using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

namespace MARDEK.UI
{
    public class SlotCursorUI : MonoBehaviour
    {
        [SerializeField] Image itemImage;
        [SerializeField] TextMeshProUGUI amountText;
        [SerializeField] Sprite transparentSprite;

        void Update()
        {
            this.transform.position = new Vector2(Mouse.current.position.x.ReadValue(), Mouse.current.position.y.ReadValue());
            if (SlotCursor.instance.IsEmpty())
            {
                this.itemImage.sprite = transparentSprite;
                this.amountText.text = "";
            }
            else
            {
                this.itemImage.sprite = SlotCursor.instance.GetItem().sprite;
                this.amountText.text = SlotCursor.instance.GetAmount().ToString();
            }
        }
    }
}