namespace MARDEK.Stats.ExpressionParser
{
    public class LiteralToken : ValueToken
    {
        float _value = 0;

        public LiteralToken(float value)
        {
            _value = value;
        }

        public override float Evaluate(IStats user, IStats target)
        {
            return _value;
        }
    }
}