using UnityEngine;
using UnityEditor;
using FullSerializer;
using MARDEK.Core;
using MARDEK.Stats;
using MARDEK.Skill;
using System.Collections.Generic;

namespace MARDEK.Inventory
{
    public class JSONReader2 : MonoBehaviour
    {
        public Element[] elements;
        public TextAsset jsonFile;

        void Start()
        {
            DeserializeItems();
        }

        [ContextMenu("Deserialize")]
        void DeserializeItems()
        {
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
                if (item.elementText != "")
                {
                    item.element = elemDict[item.elementText];
                    Debug.Log(item.element.name);
                }

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
