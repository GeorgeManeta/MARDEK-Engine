using UnityEngine;
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
            Dictionary<string,Element> elemDict = new Dictionary<string,Element>();
            foreach(Element elem in elements){
                elemDict[elem.name] = elem;
                Debug.Log(elem.name);
            }
            UnobtainedPlotItems list = new UnobtainedPlotItems();
            fsSerializer serializer = new fsSerializer();
            fsJsonParser.Parse(jsonFile.text, out fsData data);
            serializer.TryDeserialize(data, ref list);
            foreach(UnobtainedPlotItem item in list.items){
                Debug.Log(item.displayName);
                Debug.Log(item.price);
                Debug.Log(item.description);
                if(item.elementText != ""){
                    Debug.Log(elemDict[item.elementText].name);
                }
            }
        }
    }
}