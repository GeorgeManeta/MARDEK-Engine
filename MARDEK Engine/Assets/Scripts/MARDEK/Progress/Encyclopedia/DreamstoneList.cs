using System.Collections.Generic;
using UnityEngine;

namespace MARDEK.Progress
{
    [CreateAssetMenu(menuName = "MARDEK/Encyclopedia/DreamstoneList")]
    public class DreamstoneList : ScriptableObject
    {
        [SerializeField] List<Dreamstone> _dreamstones;

        public List<Dreamstone> dreamstones { get { return _dreamstones; } }
    }
}