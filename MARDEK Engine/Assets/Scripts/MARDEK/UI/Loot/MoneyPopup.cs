using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MARDEK.UI
{
    public class MoneyPopup : MonoBehaviour
    {
        public static MoneyPopup instance { get; private set; }

        [SerializeField] Image moneyImage;
        [SerializeField] TMP_Text moneyAmountText;

        int remainingTime = 0;

        void Awake()
        {
            instance = this;
        }

        void FixedUpdate()
        {
            if (remainingTime == 1)
            {
                moneyImage.gameObject.SetActive(false);
                moneyAmountText.gameObject.SetActive(false);
            }

            if (remainingTime > 0) remainingTime -= 1;

            if (remainingTime > 0 && remainingTime < 20) SetAlpha(remainingTime * 0.05f);
        }

        void SetAlpha(float amount)
        {
            Color c = moneyAmountText.color;
            Color withAlpha = new Color(c.r, c.g, c.b, amount);
            moneyImage.color = withAlpha;
            moneyAmountText.color = withAlpha;
        }

        public void Show(int amount)
        {
            moneyImage.gameObject.SetActive(true);
            moneyAmountText.gameObject.SetActive(true);
            moneyAmountText.text = amount.ToString();
            SetAlpha(1f);
            remainingTime = 100;
        }
    }
}
