using UnityEngine;

namespace MARDEK.Inventory
{
    [CreateAssetMenu(menuName = "MARDEK/Inventory/EquipmentSlot")]
    public class EquipmentSlot : ScriptableObject
    {
        [SerializeField] bool _forbidEmpty;

        public bool forbidEmpty { get { return _forbidEmpty; } }
    }
}