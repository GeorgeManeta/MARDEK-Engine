using UnityEngine;
using UnityEditor;
using FullSerializer;
using MARDEK.Core;
using MARDEK.Stats;
using MARDEK.Skill;
using System.Collections.Generic;

namespace MARDEK.Inventory
{
    public class JSONReader : MonoBehaviour
    {
        // All possible elements should be placed here in the editor for access.
        public Element[] elements;
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
        void DeserializeItems()
        {
            // Store the elements in a dictionary for quick access later.
            Dictionary<string, Element> elemDict = new Dictionary<string, Element>();
            foreach (Element elem in elements)
            {
                elemDict[elem.name] = elem;
                Debug.Log(elem.name);
            }
            UnobtainedPlotItems list = new UnobtainedPlotItems();
            fsSerializer serializer = new fsSerializer();
            fsJsonParser.Parse(jsonFile.text, out fsData data);
            serializer.TryDeserialize(data, ref list);
            foreach (UnobtainedPlotItem item in list.items)
            {
                Debug.Log(item.displayName);
                Debug.Log(item.price);
                Debug.Log(item.description);
                // Reactivate ElementText before uncommenting to use this script
                /*if (item.elementText != "")
                {
                    item.element = elemDict[item.elementText];
                    Debug.Log(item.element.name);
                }*/

                AssetDatabase.CreateAsset(
                    item,
                    "Assets/ScriptableObjects/Item/PlotItems/" + item.displayName + ".asset"
                );

                // Print the path of the created asset
                Debug.Log(AssetDatabase.GetAssetPath(item));
            }
        }
    }
}
