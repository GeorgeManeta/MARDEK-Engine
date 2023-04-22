using MARDEK.CharacterSystem;
using MARDEK.Stats;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace MARDEK.UI
{
    public class GrowthEntry : MonoBehaviour, PartyEntry
    {
        [SerializeField] TextMeshProUGUI xpValueText;
        [SerializeField] TextMeshProUGUI levelText;
        [SerializeField] Image xpProgressBar;

        [SerializeField] IntegerStat levelStat;

        public void SetCharacter(Character character)
        {
            // TODO Update xp value text
            this.levelText.text = "Level " + character.GetStat(this.levelStat);
            // TODO Update xp progress bar
        }
    }
}
