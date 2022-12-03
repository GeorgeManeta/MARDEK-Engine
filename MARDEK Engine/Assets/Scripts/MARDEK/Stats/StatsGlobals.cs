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

        [SerializeField] public StatBase CurrentHP;
        [SerializeField] public StatBase MaxHP;
        [SerializeField] public StatBase CurrentMP;
        [SerializeField] public StatBase MaxMP;
    }
}