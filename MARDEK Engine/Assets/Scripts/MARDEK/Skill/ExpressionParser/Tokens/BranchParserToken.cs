namespace MARDEK.Skill.ExpressionParser
{
    public abstract class BranchParserToken : ParserToken
    {
        public ParserToken left, right;
    }
}