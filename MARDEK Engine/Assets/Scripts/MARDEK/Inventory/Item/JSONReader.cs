using UnityEngine;
using FullSerializer;
using MARDEK.Core;
using MARDEK.Stats;
using MARDEK.Skill;

namespace MARDEK.Inventory
{
    public class JSONReader : MonoBehaviour
    {
        public TextAsset jsonFile;

        void Start()
        {
            //Debug.Log(jsonFile.text);
            //fsData data = fsJsonParser.Parse(jsonFile.text);
            //Debug.Log(data);
            UnobtainedPlotItems items = new UnobtainedPlotItems();
            items.items = new UnobtainedPlotItem[1];
            items.items[0] = ScriptableObject.CreateInstance<UnobtainedPlotItem>();
            UnobtainedPlotItem test = items.items[0];
            test.displayName = "test name";
            test.description = "test descript";
            test.element = Element.CreateInstance<Element>();
            test.element.name = "Fire";
            test.price = -1;
            items.items[0] = test;
            Debug.Log(test);
            Debug.Log(test.displayName);
            Debug.Log(test.element);
            fsData data;

            fsSerializer serializer = new fsSerializer();
            serializer.TrySerialize(items, out data).AssertSuccessWithoutWarnings();

            Debug.Log(data);            
            
            /*UnobtainedPlotItems items = null;
            fsSerializer serializer = new fsSerializer();
            serializer.TryDeserialize(data, ref items).AssertSuccessWithoutWarnings();
            Debug.Log("items");
            Debug.Log(items);
            Debug.Log(items.items);
            Debug.Log(items.items.Length);
            Debug.Log(items.items[0]);
            foreach (UnobtainedPlotItem item in items.items)
            {
                Debug.Log("Found item: " + item.displayName);
            }*/
        }
    }
}
