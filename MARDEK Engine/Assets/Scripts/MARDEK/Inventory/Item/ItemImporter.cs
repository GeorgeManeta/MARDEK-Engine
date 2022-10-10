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
    // 2. Uncomment the setter for listed attributes and setters in Item.cs
    public class ItemImporter : MonoBehaviour
    {
        // All possible elements should be placed here in the editor for access.
        public Element[] elements;
        public StatOfType<int>[] intStats;
        public EquipmentCategory[] categories;

        //public StatOfType<float>[] floatStats;
        // The JSON should be formatted as follows:
        // Elements must be Title Case.
        // ETHER must be renamed to Aether
        // NONE must be renamed to Normal
        // All field names should have an _ at the start of them
        // The JSON file must start with { items: and end with }, with an array of Items between them.
        public TextAsset jsonFile;

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
        [ContextMenu("Deserialize")]
        void setIntStatExp(int value, StatOfType<int> stat, ExpendableItem item)
        {
            if (value == 0)
            {
                return;
            }
            StatHolder<int, StatOfType<int>> statATK = new StatHolder<int, StatOfType<int>>(stat);
            statATK.Value = value;
            item.statsSet.intStats.Add(statATK);
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
            Dictionary<string, EquipmentCategory> categoryDict =
                new Dictionary<string, EquipmentCategory>();
            foreach (EquipmentCategory cat in categories)
            {
                categoryDict[cat.name] = cat;
            }
            ImportItems list = new ImportItems();
            fsSerializer serializer = new fsSerializer();
            fsJsonParser.Parse(jsonFile.text, out fsData data);
            serializer.TryDeserialize(data, ref list);
            foreach (ExpendableItem item in list.items)
            {
                /*if (item is ExpendableItem)
                {
                    setIntStatExp(item.HP, statDict["CurrentHP"], item);
                    setIntStatExp(item.MP, statDict["CurrentMP"], item);
                    setIntStatExp(item.STR, statDict["STR"], item);
                    setIntStatExp(item.SPR, statDict["SPR"], item);
                    setIntStatExp(item.POW, statDict["POW"], item);
                }*/
                /*if (item is EquippableItem)
                {
                    //Debug.Log(item.categoryText);
                    item.categoryText = "Gemstone";
                    if(item.categoryText!=null){
                        item.category = categoryDict[item.categoryText];
                    }
                    //Debug.Log(item.category);
                    //item.statBoosts = new StatsSet();
                    setIntStat(item.ATK, statDict["ATK"], item);
                    setIntStat(item.CRIT, statDict["CRIT"], item);
                    setIntStat(item.DEF, statDict["DEF"], item);
                    setIntStat(item.EVA, statDict["EVA"], item);
                    setIntStat(item.HIT, statDict["HIT"], item);
                    setIntStat(item.MDEF, statDict["MDEF"], item);
                    setIntStat(item.SPR, statDict["SPR"], item);
                    setIntStat(item.STR, statDict["STR"], item);
                    setIntStat(item.VIT, statDict["VIT"], item);
                    setIntStat(item.MaxHP, statDict["MaxHP"], item);
                    setIntStat(item.MaxMP, statDict["MaxMP"], item);
                    //Debug.Log(item.statBoosts.intStats[0].Value);
                }*/
                Debug.Log(item.displayName);
                //Debug.Log(item.description);
                // Reactivate ElementText before uncommenting to use this script
                /*if (item.elementText != "" && item.elementText != null)
                {
                    item.element = elemDict[item.elementText];
                    Debug.Log(item.element.name);
                }*/

                AssetDatabase.CreateAsset(
                    item,
                    // Make sure this is pointed at the right directory for what you're importing
                    "Assets/ScriptableObjects/Item/Expendable/"+ /*item.categoryText*/ "" + "" + item.displayName + ".asset"
                );

                // Print the path of the created asset
                Debug.Log(AssetDatabase.GetAssetPath(item));
            }
        }
    }
}
