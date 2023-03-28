namespace MARDEK.Stats.ExpressionParser
{
    public class StatToken : ValueToken
    {
        IntegerStat _targetStat;
        bool _getFromTarget;

        public StatToken(IntegerStat targetStat, bool getFromTarget)
        {
            _targetStat = targetStat;
            _getFromTarget = getFromTarget;
        }

        public override float Evaluate(IStats user, IStats target)
        {
            IStats statHolderToGetFrom = _getFromTarget ? target : user;
            return statHolderToGetFrom.GetStat(_targetStat);
        }
    }
}