using UnityEngine;

namespace MARDEK.Stats
{
    [System.Serializable]
    public class StatHolder
    {
        public StatHolder(IntegerStat status)
        {
            statusEnum = status;
        }

        [field: SerializeField] public IntegerStat statusEnum { get; private set; }
        [SerializeField] int _value = default;
        public int Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }
    }
}