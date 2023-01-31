using MARDEK.Save;
using UnityEngine;

namespace MARDEK.Tileset
{
    public class TreasureChestTile : MonoBehaviour
    {
        [SerializeField] SpriteRenderer spriteRenderer;
        [SerializeField] Sprite closedSprite;
        [SerializeField] Sprite openSprite;
        [SerializeField] ChestID chestID;

        void FixedUpdate()
        {
            if (GeneralProgressData.instance.HasOpenedChest(chestID)) spriteRenderer.sprite = openSprite;
            else spriteRenderer.sprite = closedSprite;
        }
    }
}
