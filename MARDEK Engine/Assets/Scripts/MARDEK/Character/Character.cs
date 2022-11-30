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

        [field: SerializeField] public CharacterInfo CharacterInfo { get; private set; }
        [field: SerializeField] public Inventory.Inventory EquippedItems { get; private set; }
        [field: SerializeField] public Inventory.Inventory Inventory { get; private set; }
        [field: SerializeField] public List<SkillSlot> SkillSlots { get; private set; } = new List<SkillSlot>();
        [SerializeField] StatsSet stats = new StatsSet();

        public StatHolder<T, StatOfType<T>> GetStat<T>(StatOfType<T> desiredStat)
        {            
            var resultHolder = new StatHolder<T, StatOfType<T>>(desiredStat);

            SumHolders(ref resultHolder, CharacterInfo.StartingStats.GetStat(desiredStat));
            SumHolders(ref resultHolder, stats.GetStat(desiredStat));
            foreach(var slot in EquippedItems.Slots)
            {
                var equippableItem = slot.item as EquippableItem;
                if(equippableItem != null)
                    SumHolders(ref resultHolder, equippableItem.statBoosts.GetStat(desiredStat));
            }
            
            return resultHolder;

            void SumHolders(ref StatHolder<T, StatOfType<T>> firstHolder, StatHolder<T, StatOfType<T>> secondHolder)
            {
                if (firstHolder == null)
                    return;
                if (typeof(T) == typeof(int))
                {
                    var firstValue = firstHolder as StatHolder<int, StatOfType<int>>;
                    var secondValue = secondHolder as StatHolder<int, StatOfType<int>>;
                    firstValue.Value += secondValue.Value;
                }
                if (typeof(T) == typeof(float))
                {
                    var firstValue = firstHolder as StatHolder<float, StatOfType<float>>;
                    var secondValue = secondHolder as StatHolder<float, StatOfType<float>>;
                    firstValue.Value += secondValue.Value;
                }
            }
        }
        public void ModifyStat<T>(StatOfType<T> stat, float delta)
        {
            stats.ModifyStat(stat, delta);
        }
    }
}