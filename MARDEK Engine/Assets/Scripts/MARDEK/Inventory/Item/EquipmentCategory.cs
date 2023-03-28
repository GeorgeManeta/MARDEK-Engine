using MARDEK.Core;
using UnityEngine;

namespace MARDEK.Inventory
{
    [CreateAssetMenu(menuName = "MARDEK/Inventory/EquipmentCategory")]
    public class EquipmentCategory : ScriptableObject
    {
        [SerializeField] EquipmentSlot _slot;
        [SerializeField] ScriptableColor _color;
        [SerializeField] string _classification;

        public EquipmentSlot slot { get { return _slot; } }

        public ScriptableColor color { get { return _color; } }

        public string classification { get { return _classification; } }
    }
}