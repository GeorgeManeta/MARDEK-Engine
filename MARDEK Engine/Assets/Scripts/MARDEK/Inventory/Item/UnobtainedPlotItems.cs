using UnityEngine;

namespace MARDEK.Inventory{
    [System.Serializable]
    public class UnobtainedPlotItems
    {
        //items is case sensitive and must match the string "items" in the JSON.
        public UnobtainedPlotItem[] items;
    }
}