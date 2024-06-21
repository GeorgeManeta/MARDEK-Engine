using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MARDEK.Stats
{
    //[CreateAssetMenu(menuName = "StatsGlobals")]
    public class StatsGlobals : ScriptableObject
    {
        static StatsGlobals _instance;
        public static StatsGlobals Instance
        {
            get
            {
                if (_instance == null)
                    _instance = Resources.Load("StatsGlobals") as StatsGlobals;
                return _instance;
            }
        }

        [field: SerializeField] public IntegerStat Level { get; private set; }
        [field: SerializeField] public IntegerStat Experience { get; private set; }
        [field: SerializeField] public IntegerStat CurrentHP { get; private set; }
        [field: SerializeField] public IntegerStat MaxHP { get; private set; }
        [field: SerializeField] public IntegerStat CurrentMP { get; private set; }
        [field: SerializeField] public IntegerStat MaxMP { get; private set; }
        [field: SerializeField] public StatExpression DefaultMaxHPExpression { get; private set; }
        [field: SerializeField] public StatExpression DefaultMaxMPExpression { get; private set; }
    }
}