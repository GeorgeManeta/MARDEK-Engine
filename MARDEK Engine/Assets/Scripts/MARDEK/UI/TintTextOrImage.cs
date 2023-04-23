using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace MARDEK.UI
{
    public class TintTextOrImage: MonoBehaviour
    {
        // Should only use TextMeshProUGUI, Text is kept for compatability with not-yet-converted text
        [SerializeField] Text text;
        [SerializeField] TextMeshProUGUI textMeshProText;
        [SerializeField] Color textColor;
        [Space]
        [SerializeField] Image image;
        [SerializeField] Color imageColor;

        public void ApplyTint()
        {
            if (text)
                text.color = textColor;
            if (textMeshProText)
                textMeshProText.color = textColor;
            if (image)
                image.color = imageColor;
        }
    }
}
