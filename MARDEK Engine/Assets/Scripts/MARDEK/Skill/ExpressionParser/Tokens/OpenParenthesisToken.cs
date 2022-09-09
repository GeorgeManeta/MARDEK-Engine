using MARDEK.Stats;

namespace MARDEK.Skill.ExpressionParser
{
    public class OpenParenthesisToken : ParserToken
    {
        public ParserToken internalToken = null;

        public override float Evaluate(IStats user, IStats target)
        {
            return internalToken.Evaluate(user, target);
        }
    }
}