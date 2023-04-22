using MARDEK.CharacterSystem;
using MARDEK.Stats;
using UnityEngine;
using TMPro;

namespace MARDEK.UI
{
    public class Performance2Entry : MonoBehaviour, PartyEntry
    {
        [SerializeField] TextMeshProUGUI damageDealtCount;
        [SerializeField] TextMeshProUGUI damageReceivedCount;

        [SerializeField] IntegerStat damageDealtStat;
        [SerializeField] IntegerStat damageReceivedStat;

        public void SetCharacter(Character character)
        {
            damageDealtCount.text = character.GetStat(damageDealtStat).ToString();
            damageReceivedCount.text = character.GetStat(damageReceivedStat).ToString();
        }

        private void UpdateStat(Character character, IntegerStat stat, TextMeshProUGUI text)
        {
            if (stat != null) text.text = character.GetStat(stat).ToString();
        }
    }
}
