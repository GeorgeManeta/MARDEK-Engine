using MARDEK.CharacterSystem;
using MARDEK.Stats;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace MARDEK.UI
{
    public class VitalStatisticsEntry : MonoBehaviour, PartyEntry
    {
        [SerializeField] TextMeshProUGUI attackValue;
        [SerializeField] TextMeshProUGUI defValue;
        [SerializeField] TextMeshProUGUI mdefValue;

        [SerializeField] TextMeshProUGUI strValue;
        [SerializeField] TextMeshProUGUI vitValue;
        [SerializeField] TextMeshProUGUI sprValue;
        [SerializeField] TextMeshProUGUI aglValue;

        [SerializeField] TextMeshProUGUI strBonus;
        [SerializeField] TextMeshProUGUI vitBonus;
        [SerializeField] TextMeshProUGUI sprBonus;
        [SerializeField] TextMeshProUGUI aglBonus;

        [SerializeField] IntegerStat attackStat;
        [SerializeField] IntegerStat defStat;
        [SerializeField] IntegerStat mdefStat;

        [SerializeField] IntegerStat strStat;
        [SerializeField] IntegerStat vitStat;
        [SerializeField] IntegerStat sprStat;
        [SerializeField] IntegerStat aglStat;

        public void SetCharacter(Character character)
        {
            attackValue.text = character.GetStat(attackStat).ToString();
            defValue.text = character.GetStat(defStat).ToString();
            mdefValue.text = character.GetStat(mdefStat).ToString();

            strValue.text = character.GetStat(strStat).ToString();
            vitValue.text = character.GetStat(vitStat).ToString();
            sprValue.text = character.GetStat(sprStat).ToString();
            aglValue.text = character.GetStat(aglStat).ToString();

            // TODO Update the colors of the values and bonuses
            // TODO Update the bonuses
        }
    }
}
