using UnityEngine;

namespace MARDEK.CharacterSystem
{
    [CreateAssetMenu(menuName = "MARDEK/Character/PortraitExpression")]
    public class PortraitExpression : ScriptableObject
    {
        [SerializeField] string _name;

        public string name { get { return _name; } }
    }
}
