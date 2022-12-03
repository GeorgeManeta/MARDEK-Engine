namespace MARDEK.Stats.ExpressionParser
{
    public class MultiplicationToken : LeftmostDerivationToken
    {
        public override float Evaluate(IStats user, IStats target)
        {
            var leftValue = left.Evaluate(user, target);
            var rightValue = right.Evaluate(user, target);
            return leftValue * rightValue;
        }
    }
}