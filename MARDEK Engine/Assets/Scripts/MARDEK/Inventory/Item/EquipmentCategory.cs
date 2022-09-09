using UnityEngine;

namespace MARDEK.Inventory
{
    [CreateAssetMenu(menuName = "MARDEK/Inventory/EquipmentCategory")]
    public class EquipmentCategory : ScriptableObject
    {
        [SerializeField] EquipmentSlot _slot;
        [SerializeField] string _classification;

        public EquipmentSlot slot { get { return _slot; } }

        public string classification { get { return _classification; } }
    }
}