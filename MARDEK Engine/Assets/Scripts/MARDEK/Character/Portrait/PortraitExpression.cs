using UnityEngine;
using System.Collections.Generic;

namespace MARDEK.CharacterSystem
{
    public class PortraitExpression : MonoBehaviour
    {
        [field: SerializeField] public PortraitExpressionEnum Expression { get; private set; }
        [field: SerializeField] public List<SpriteTransformOperation> Transforms { get; private set; }

        public void ApplyTransforms()
        {
            foreach (var op in Transforms)
            {
                if (op.ApplyOperation)
                {
                    op.gameObject.SetActive(true);
                    op.SpriteRenderer.gameObject.SetActive(true);

                    op.SpriteRenderer.gameObject.transform.localPosition = op.Position;
                    op.SpriteRenderer.gameObject.transform.localRotation = Quaternion.Euler(0, 0, op.Rotation);
                    op.SpriteRenderer.gameObject.transform.localScale = new Vector3(op.Scale.x, op.Scale.y, 1);
                }
                else
                {
                    op.gameObject.SetActive(false);
                }
            }
        }
    }
}
