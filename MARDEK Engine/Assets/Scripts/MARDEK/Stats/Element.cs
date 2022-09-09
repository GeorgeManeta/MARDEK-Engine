using UnityEngine;

namespace MARDEK.Stats
{
    [CreateAssetMenu(menuName = "MARDEK/Stats/Element")]
    public class Element : ScriptableObject
    {
        [SerializeField] Sprite _thinSprite;
        [SerializeField] Sprite _thickSprite;
        [SerializeField] Color _textColor;

        public Sprite thinSprite { get { return _thinSprite; } }

        public Sprite thickSprite { get { return _thickSprite; } }

        public Color textColor { get { return _textColor; } }
    }
}