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

        [SerializeField] public StatOfType<int> CurrentHP;
        [SerializeField] public StatOfType<int> MaxHP;
        [SerializeField] public StatOfType<int> CurrentMP;
        [SerializeField] public StatOfType<int> MaxMP;
    }
}