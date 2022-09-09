using MARDEK.Stats;

namespace MARDEK.Skill.ExpressionParser
{
    public abstract class ParserToken
    {
        public abstract float Evaluate(IStats user, IStats target);
    }
}