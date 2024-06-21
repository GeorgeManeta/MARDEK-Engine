using UnityEngine;

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
            var value = statHolderToGetFrom.GetStat(_targetStat);
            //Debug.Log($"Parsed {_targetStat.name} with value {value}");
            return value;
        }
    }
}