using MARDEK.CharacterSystem;
using MARDEK.Stats;
using UnityEngine;
using UnityEngine.UI;

namespace MARDEK.UI
{
    public class VitalStatisticsEntry : MonoBehaviour, PartyEntry
    {
        [SerializeField] Image portraitImage;

        [SerializeField] Text attackValue;
        [SerializeField] Text defValue;
        [SerializeField] Text mdefValue;

        [SerializeField] Text strValue;
        [SerializeField] Text vitValue;
        [SerializeField] Text sprValue;
        [SerializeField] Text aglValue;

        [SerializeField] Text strBonus;
        [SerializeField] Text vitBonus;
        [SerializeField] Text sprBonus;
        [SerializeField] Text aglBonus;

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
