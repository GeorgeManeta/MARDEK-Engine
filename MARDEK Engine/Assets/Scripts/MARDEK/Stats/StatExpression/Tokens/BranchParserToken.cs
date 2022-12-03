namespace MARDEK.Stats.ExpressionParser
{
    public abstract class BranchParserToken : ParserToken
    {
        public ParserToken left, right;
    }
}