using MARDEK.CharacterSystem;
using TMPro;
using UnityEngine;

namespace MARDEK.UI
{
    using Progress;

    public class CurrentMoneyText : MonoBehaviour
    {
        [SerializeField] TMP_Text text;

        void FixedUpdate()
        {
            text.text = Party.Instance.money.ToString();
        }
    }
}
