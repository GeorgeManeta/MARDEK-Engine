using MARDEK.CharacterSystem;
using MARDEK.Stats;
using UnityEngine;
using TMPro;

namespace MARDEK.UI
{
    public class Performance1Entry : MonoBehaviour, PartyEntry
    {
        [SerializeField] TextMeshProUGUI battleCount;
        [SerializeField] TextMeshProUGUI killCount;
        [SerializeField] TextMeshProUGUI koCount;
        [SerializeField] TextMeshProUGUI meleeCount;
        [SerializeField] TextMeshProUGUI magicCount;
        [SerializeField] TextMeshProUGUI itemCount;

        [SerializeField] IntegerStat battleCountStat;
        [SerializeField] IntegerStat killCountStat;
        [SerializeField] IntegerStat koCountStat;
        [SerializeField] IntegerStat meleeCountStat;
        [SerializeField] IntegerStat magicCountStat;
        [SerializeField] IntegerStat itemCountStat;

        public void SetCharacter(Character character)
        {
            UpdateStat(character, battleCountStat, battleCount);
            UpdateStat(character, killCountStat, killCount);
            UpdateStat(character, koCountStat, koCount);
            UpdateStat(character, meleeCountStat, meleeCount);
            UpdateStat(character, magicCountStat, magicCount);
            UpdateStat(character, itemCountStat, itemCount);
        }

        private void UpdateStat(Character character, IntegerStat stat, TextMeshProUGUI text)
        {
            if (stat != null) text.text = character.GetStat(stat).ToString();
        }
    }
}
