namespace MARDEK.Stats.ExpressionParser
{
    public class SubtractionToken : BranchParserToken
    {
        public override float Evaluate(IStats user, IStats target)
        {
            var leftValue = left == null ? 0 : left.Evaluate(user, target);
            var rightValue = right == null ? 0 : right.Evaluate(user, target);
            return leftValue - rightValue;
        }
    }
}