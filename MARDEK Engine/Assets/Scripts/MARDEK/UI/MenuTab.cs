using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace MARDEK.UI
{
    public class MenuTab : Selectable
    {
        [SerializeField] Image tabImage;
        [SerializeField] TextMeshProUGUI tabNameLabel;
        [SerializeField] string tabName;
        
        bool isSelected;

        public override void Select(bool playSFX = true)
        {
            base.Select(playSFX: playSFX);
            isSelected = true;
            UpdateAppearance();
        }

        public override void Deselect()
        {
            base.Deselect();
            isSelected = false;
            UpdateAppearance();
        }

        void Awake()
        {
            UpdateAppearance();
        }

        void UpdateAppearance()
        {
            tabNameLabel.text = tabName;

            // if tabImage is not set, try to use the image of the GameObject this MonoBehavior is attached to
            Image updatedImage;
            if (tabImage == null)
            {
                updatedImage = this.GetComponent<Image>();
            }
            else
            {
                updatedImage = tabImage;
            }

            if (updatedImage != null)
            {
                if (isSelected)
                {
                    updatedImage.color = new Color(1f, 1f, 1f, 1f);
                }
                else updatedImage.color = new Color(1f, 1f, 1f, 0.1f);
            }
        }
    }
}