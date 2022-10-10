using UnityEngine;

namespace MARDEK.Inventory{
    [System.Serializable]
    public class ImportItems
    {
        //items is case sensitive and must match the string "items" in the JSON.
        public ExpendableItem[] items;
    }
}