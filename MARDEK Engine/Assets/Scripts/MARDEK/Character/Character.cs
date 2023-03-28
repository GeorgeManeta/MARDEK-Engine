using UnityEngine;
using System.Collections.Generic;
using MARDEK.Inventory;
using MARDEK.Stats;

namespace MARDEK.CharacterSystem
{
    [System.Serializable]
    public class Character : IStats
    {
        [SerializeField] public bool isRequired;

        [field: SerializeField] public CharacterProfile Profile { get; private set; }
        [field: SerializeField] public Inventory.Inventory EquippedItems { get; private set; }
        [field: SerializeField] public Inventory.Inventory Inventory { get; private set; }
        [field: SerializeField] public List<SkillSlot> SkillSlots { get; private set; } = new List<SkillSlot>();
        [Header("Stats")]
        [SerializeField] StatsSet volatileStats = new StatsSet(true);
        int MaxHP
        {
            get
            {
                return (int)Profile.MaxHPExpression.Evaluate(this, null);
            }
        }
        int MaxMP
        {
            get
            {
                return (int)Profile.MaxMPExpression.Evaluate(this, null);
            }
        }
        int _currentHP = -1;
        int _currentMP = -1;
        int CurrentHP
        {
            get
            {
                if (_currentHP == -1)
                    _currentHP = GetStat(StatsGlobals.Instance.MaxHP);
                return _currentHP;
            }
            set
            {
                _currentHP = value;
            }
        }
        int CurrentMP
        {
            get
            {
                if (_currentMP == -1)
                    _currentMP = GetStat(StatsGlobals.Instance.MaxMP);
                return _currentMP;
            }
            set
            {
                _currentMP = value;
            }
        }

        public int GetStat(IntegerStat desiredStat)
        {            
            if (desiredStat == StatsGlobals.Instance.CurrentHP)
                return CurrentHP;
            if (desiredStat == StatsGlobals.Instance.CurrentMP)
                return CurrentMP;

            var resultHolder = new StatHolder(desiredStat);
            if (desiredStat == StatsGlobals.Instance.MaxHP)
                resultHolder.Value = MaxHP;
            if (desiredStat == StatsGlobals.Instance.MaxMP)
                resultHolder.Value = MaxMP;

            resultHolder.Value += Profile.StartingStats.GetStat(desiredStat);
            resultHolder.Value += volatileStats.GetStat(desiredStat);
            foreach(var slot in EquippedItems.Slots)
            {
                var equippableItem = slot.item as EquippableItem;
                if(equippableItem != null)
                    resultHolder.Value += equippableItem.statBoosts.GetStat(desiredStat);
            }
            return resultHolder.Value;
        }
        public void ModifyStat(IntegerStat stat, int delta)
        {
            if (stat == StatsGlobals.Instance.CurrentHP)
            {
                CurrentHP += delta;
            }
            else if (stat == StatsGlobals.Instance.CurrentMP)
            {
                CurrentMP += delta;
            }
            else
                volatileStats.ModifyStat(stat, delta);
        }
    }
}