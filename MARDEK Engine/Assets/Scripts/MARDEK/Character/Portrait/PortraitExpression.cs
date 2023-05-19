using UnityEngine;

namespace MARDEK.CharacterSystem
{
    [CreateAssetMenu(menuName = "MARDEK/Character/PortraitExpression")]
    public class PortraitExpression : ScriptableObject
    {
        [SerializeField] string _expressionName;

        public string expressionName { get { return _expressionName; } }

        private void OnValidate()
        {
            _expressionName = this.name;
        }
    }
}
