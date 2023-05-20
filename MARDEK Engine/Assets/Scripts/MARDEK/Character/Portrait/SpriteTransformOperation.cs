using UnityEngine;

namespace MARDEK.CharacterSystem
{
    public class SpriteTransformOperation : MonoBehaviour
    {
        [field: SerializeField] public SpriteRenderer SpriteRenderer { get; private set; }
        [field:SerializeField] public Vector2 Position { get; private set; }
        [field: SerializeField] public bool ApplyPosition { get; private set; }
        [field: SerializeField] public float Rotation { get; private set; }
        [field: SerializeField] public bool ApplyRotation { get; private set; }
        [field: SerializeField] public Vector2 Scale { get; private set; }
        [field: SerializeField] public bool ApplyScale { get; private set; }

    }
}
