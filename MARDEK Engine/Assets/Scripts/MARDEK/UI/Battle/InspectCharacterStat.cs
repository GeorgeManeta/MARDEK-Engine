using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MARDEK.UI
{
    using Stats;
    public class InspectCharacterStat : MonoBehaviour
    {
        [SerializeField] IntegerStat stat;
        [SerializeField] TMP_Text number;

        private void OnEnable()
        {
            var value = BattleUIManager.Instance.characterBeingInspected.GetStat(stat);
            number.text = value.ToString();
        }
    }
}