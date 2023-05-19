using UnityEngine;

namespace MARDEK.CharacterSystem
{
    public class ExpressionSprite : MonoBehaviour
    {
        [field: SerializeField] public PortraitExpression expression { get; private set; }
    }
}
