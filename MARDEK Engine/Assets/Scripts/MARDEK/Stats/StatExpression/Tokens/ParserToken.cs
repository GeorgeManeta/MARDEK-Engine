namespace MARDEK.Stats.ExpressionParser
{
    public abstract class ParserToken
    {
        public abstract float Evaluate(IStats user, IStats target);
    }
}