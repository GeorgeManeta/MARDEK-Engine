using MARDEK.Stats;
using UnityEngine;

namespace MARDEK.Progress
{
    [System.Serializable]
    public class DreamstoneMessage
    {
        [SerializeField] string _text;
        [SerializeField] Element _element;

        public string text { get { return _text; } }
        public Element element { get { return _element; } }
    }
}