using MARDEK.CharacterSystem;
using MARDEK.Stats;
using UnityEngine;
using UnityEngine.UI;

namespace MARDEK.UI
{
    public class Performance2Entry : MonoBehaviour, PartyEntry
    {
        [SerializeField] Text damageDealtCount;
        [SerializeField] Text damageReceivedCount;

        [SerializeField] IntegerStat damageDealtStat;
        [SerializeField] IntegerStat damageReceivedStat;

        public void SetCharacter(Character character)
        {
            this.damageDealtCount.text = character.GetStat(this.damageDealtStat).Value.ToString();
            this.damageReceivedCount.text = character.GetStat(this.damageReceivedStat).Value.ToString();
        }

        private void UpdateStat(Character character, IntegerStat stat, Text text)
        {
            if (stat != null) text.text = character.GetStat(stat).Value.ToString();
        }
    }
}
