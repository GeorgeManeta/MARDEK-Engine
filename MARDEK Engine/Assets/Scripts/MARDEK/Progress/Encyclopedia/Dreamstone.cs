using System.Collections.Generic;
using UnityEngine;

namespace MARDEK.Progress
{
    [CreateAssetMenu(menuName = "MARDEK/Encyclopedia/Dreamstone")]
    public class Dreamstone : ScriptableObject
    {
        [SerializeField] List<DreamstoneMessage> _messages;
        public bool isNew;

        public List<DreamstoneMessage> messages { get { return _messages; } }
    }
}