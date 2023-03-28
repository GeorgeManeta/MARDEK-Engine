using MARDEK.Inventory;
using UnityEngine;
using UnityEngine.UI;

namespace MARDEK.UI
{
    public class InventorySpace : MonoBehaviour
    {
        [SerializeField] Image image;

        Color DetermineColor(Slot slot)
        {
            if (slot.IsEmpty()) return new Color(0f, 0f, 0f, 0f);
            else return slot.currentItem.GetInventorySpaceColor();
        }

        public void UpdateImage(Inventory.Inventory inventory)
        {
            Texture2D texture = new Texture2D(8, 8);
            texture.filterMode = FilterMode.Point;
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    var slot = inventory.Slots[x + 8 * y];
                    texture.SetPixel(x, 7 - y, DetermineColor(slot));
                }
            }
            texture.Apply();
            image.sprite = Sprite.Create(texture, new Rect(0, 0, 8, 8), Vector2.zero);
        }
    }
}
