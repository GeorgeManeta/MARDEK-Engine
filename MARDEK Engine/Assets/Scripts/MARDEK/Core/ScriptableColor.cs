using UnityEngine;

namespace MARDEK.Core
{
    [CreateAssetMenu(menuName = "MARDEK/Core/Color")]
    public class ScriptableColor : ScriptableObject
    {
        [SerializeField] int _red;
        [SerializeField] int _green;
        [SerializeField] int _blue;
        [SerializeField] int _alpha;

        public int red { get { return _red; } }

        public int green { get { return _green; } }

        public int blue { get { return _blue; } }

        public int alpha { get { return _alpha; } }

        public Color ToColor() {
            return new Color(red / 255f, green / 255f, blue / 255f, alpha / 255f);
        }
    }
}
