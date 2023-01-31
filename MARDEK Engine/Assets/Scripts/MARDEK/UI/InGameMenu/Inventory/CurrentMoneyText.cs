using MARDEK.CharacterSystem;
using TMPro;
using UnityEngine;

namespace MARDEK.UI
{
    public class CurrentMoneyText : MonoBehaviour
    {
        [SerializeField] TMP_Text text;

        void FixedUpdate()
        {
            text.text = Party.Instance.money.ToString();
        }
    }
}
