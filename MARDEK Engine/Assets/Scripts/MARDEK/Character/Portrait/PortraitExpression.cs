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
                op.gameObject.SetActive(true);
                op.Obj.gameObject.SetActive(true);

                if (op.ApplyPosition)
                {
                    op.Obj.gameObject.transform.localPosition = op.Position;
                }

                if (op.ApplyRotation)
                {
                    op.Obj.gameObject.transform.localRotation = Quaternion.Euler(0, 0, op.Rotation);
                }

                if (op.ApplyScale)
                {
                    op.Obj.gameObject.transform.localScale = new Vector3(op.Scale.x, op.Scale.y, 1);
                }
            }
        }

        public void HideSprites()
        {
            foreach (var op in Transforms)
            {
                op.gameObject.SetActive(false);
                op.Obj.gameObject.SetActive(false);
            }
        }
    }
}
