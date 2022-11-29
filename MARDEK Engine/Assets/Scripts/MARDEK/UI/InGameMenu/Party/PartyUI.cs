using MARDEK.CharacterSystem;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MARDEK.UI
{
    public class PartyUI : MonoBehaviour
    {
        [SerializeField] List<ConditionEntry> conditionEntries;
        [SerializeField] List<VitalStatisticsEntry> vitalStatisticsEntries;
        [SerializeField] List<ResistancesEntry> elementalResistancesEntries;
        [SerializeField] List<ResistancesEntry> statusEffectResistancesEntries;
        [SerializeField] List<GrowthEntry> growthEntries;
        [SerializeField] List<Performance1Entry> performance1Entries;
        [SerializeField] List<Performance2Entry> performance2Entries;

        void OnEnable()
        {
            List<PartyEntry>[] entriesList = new List<PartyEntry>[]{
                conditionEntries.Cast<PartyEntry>().ToList(),
                vitalStatisticsEntries.Cast<PartyEntry>().ToList(),
                elementalResistancesEntries.Cast<PartyEntry>().ToList(),
                statusEffectResistancesEntries.Cast<PartyEntry>().ToList(),
                growthEntries.Cast<PartyEntry>().ToList(),
                performance1Entries.Cast<PartyEntry>().ToList(),
                performance2Entries.Cast<PartyEntry>().ToList()
            };

            foreach (var entries in entriesList) {
                for (int index = 0; index < entries.Count; index++)
                {
                    var entry = entries[index];
                    if (index < Party.Instance.Characters.Count)
                    {
                        entry.gameObject.SetActive(true);
                        entry.SetCharacter(Party.Instance.Characters[index]);
                    }
                    else entry.gameObject.SetActive(false);
                }
            }  
        }
    }
}
