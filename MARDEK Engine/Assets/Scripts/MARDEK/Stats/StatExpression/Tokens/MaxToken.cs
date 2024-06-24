using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MARDEK.Stats.ExpressionParser
{
    public class MaxToken : LeftmostDerivationToken
    {
        public override float Evaluate(IStats user, IStats target)
        {
            var leftValue = left == null ? 0 : left.Evaluate(user, target);
            var rightValue = right == null ? 0 : right.Evaluate(user, target);
            //Debug.Log($"Max between {leftValue} and {rightValue}");
            return Mathf.Max(leftValue, rightValue);
        }
    }
}