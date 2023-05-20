using UnityEngine;
using System.Collections.Generic;

namespace MARDEK.CharacterSystem
{
    public abstract class PortraitPrefab : MonoBehaviour
    {
        public abstract PortraitType PortraitType { get; protected set; }
        public abstract void SetPortrait(CharacterPortrait portrait);

        [field: SerializeField] public List<PortraitExpression> Expressions { get; private set; }
        [field: SerializeField] public PortraitExpression NormalExpression { get; private set; }

        public void SetExpression(PortraitExpressionEnum expression)
        {
            PortraitExpression currentExpression = null;
            foreach(var expr in Expressions)
            {
                if (expr.Expression == expression)
                {
                    currentExpression = expr;
                }
                else
                {
                    expr.gameObject.SetActive(false);
                    expr.HideSprites();
                }
            }
            if (currentExpression == null)
            {
                NormalExpression.gameObject.SetActive(true);
                NormalExpression.ApplyTransforms();
            }
            else
            {
                currentExpression.gameObject.SetActive(true);
                currentExpression.ApplyTransforms();
            }
        }
    }
}
