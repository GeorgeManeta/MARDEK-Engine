using System.Collections.Generic;
using UnityEngine;

namespace MARDEK.Progress
{
    [CreateAssetMenu(menuName = "MARDEK/Encyclopedia/List")]
    public class EncyclopediaList : ScriptableObject
    {
        [SerializeField] List<EncyclopediaItem> _items;

        public List<EncyclopediaItem> items { get { return _items; } }
    }
}