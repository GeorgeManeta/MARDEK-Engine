using UnityEngine;
using FullSerializer;

namespace MARDEK.Inventory
{
    public class JSONReader : MonoBehaviour
    {
        public TextAsset jsonFile;

        void Start()
        {
            Debug.Log(jsonFile.text);
            fsData data = fsJsonParser.Parse(jsonFile.text);

            UnobtainedPlotItems items = null;
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
            }
        }
    }
}
