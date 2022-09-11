using UnityEngine;
using UnityEditor;
using FullSerializer;
using MARDEK.Core;
using MARDEK.Stats;
using MARDEK.Skill;
using System.Collections.Generic;

namespace MARDEK.Inventory
{
    // To make this work:
    // 1. Comment the denoted line (currently line 10) in AddressableScriptableObject.cs
    // 2. Uncomment the setter for Element and the ElementValue attributes in Item.cs
    public class JSONReader : MonoBehaviour
    {
        // All possible elements should be placed here in the editor for access.
        public Element[] elements;
        public StatOfType<int>[] intStats;

        //public StatOfType<float>[] floatStats;
        // The JSON should be formatted as follows:
        // Elements must be Title Case.
        // ETHER must be renamed to Aether
        // NONE must be renamed to Normal
        // All field names should have an _ at the start of them
        // The JSON file must start with { items: and end with }, with an array of Items between them.
        public TextAsset jsonFile;
        public EquipmentCategory category;

        void Start()
        {
            DeserializeItems();
        }

        [ContextMenu("Deserialize")]
        void setIntStat(int value, StatOfType<int> stat, EquippableItem item)
        {
            if (value == 0)
            {
                return;
            }
            StatHolder<int, StatOfType<int>> statATK = new StatHolder<int, StatOfType<int>>(stat);
            statATK.Value = value;
            item.statBoosts.intStats.Add(statATK);
        }

        void DeserializeItems()
        {
                    Debug.Log("START DESERIALIZING");
            // Store the elements in a dictionary for quick access later.
            Dictionary<string, Element> elemDict = new Dictionary<string, Element>();
            foreach (Element elem in elements)
            {
                elemDict[elem.name] = elem;
            }
            Dictionary<string, StatOfType<int>> statDict =
                new Dictionary<string, StatOfType<int>>();
            foreach (StatOfType<int> stat in intStats)
            {
                statDict[stat.name] = stat;
            }
            ImportItems list = new ImportItems();
            fsSerializer serializer = new fsSerializer();
            fsJsonParser.Parse(jsonFile.text, out fsData data);
            serializer.TryDeserialize(data, ref list);
            foreach (EquippableItem item in list.items)
            {
                if (category != null)
                {
                    item.category = category;
                }
                if (item is EquippableItem)
                {
                    //item.statBoosts = new StatsSet();
                    setIntStat(item.ATK, statDict["ATK"], item);
                    setIntStat(item.CRITICAL, statDict["CRIT"], item);
                    setIntStat(item.DEF, statDict["DEF"], item);
                    setIntStat(item.EVA, statDict["EVA"], item);
                    setIntStat(item.HIT, statDict["HIT"], item);
                    setIntStat(item.MDEF, statDict["MDEF"], item);
                    setIntStat(item.SPR, statDict["SPR"], item);
                    setIntStat(item.STR, statDict["STR"], item);
                    setIntStat(item.VIT, statDict["VIT"], item);
                    Debug.Log(item.statBoosts.intStats[0].Value);
                }
                Debug.Log(item.displayName);
                Debug.Log(item.description);
                // Reactivate ElementText before uncommenting to use this script
                if (item.elementText != "" && item.elementText != null)
                {
                    item.element = elemDict[item.elementText];
                    Debug.Log(item.element.name);
                }

                AssetDatabase.CreateAsset(
                    item,
                    // Make sure this is pointed at the right directory for what you're importing
                    "Assets/ScriptableObjects/Item/Armour/Hat/" + item.displayName + ".asset"
                );

                // Print the path of the created asset
                Debug.Log(AssetDatabase.GetAssetPath(item));
            }
        }
    }
}
