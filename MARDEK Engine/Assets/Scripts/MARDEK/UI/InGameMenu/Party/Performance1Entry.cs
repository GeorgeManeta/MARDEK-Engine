using MARDEK.CharacterSystem;
using MARDEK.Stats;
using UnityEngine;
using UnityEngine.UI;

namespace MARDEK.UI
{
    public class Performance1Entry : MonoBehaviour, PartyEntry
    {
        [SerializeField] Text battleCount;
        [SerializeField] Text killCount;
        [SerializeField] Text koCount;
        [SerializeField] Text meleeCount;
        [SerializeField] Text magicCount;
        [SerializeField] Text itemCount;

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

        private void UpdateStat(Character character, IntegerStat stat, Text text)
        {
            if (stat != null) text.text = character.GetStat(stat).ToString();
        }
    }
}
