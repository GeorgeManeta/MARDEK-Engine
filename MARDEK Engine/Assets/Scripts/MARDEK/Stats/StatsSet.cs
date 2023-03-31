using System.Collections.Generic;

namespace MARDEK.Stats
{
    [System.Serializable]
    public class StatsSet : IStats
    {
        public List<StatHolder> intStats;
        bool expandable = false;
        public StatsSet(bool _expandable = false)
        {
            expandable = _expandable;
            intStats = new List<StatHolder>();
        }
        public int GetStat(IntegerStat stat)
        {
            var holder = GetStatHolder(stat);
            if(holder != null)
                return holder.Value;
            else
                return new StatHolder(stat).Value;
        }
        public void ModifyStat(IntegerStat stat, int delta)
        {
            var holder = GetStatHolder(stat);
            if (holder == null)
            {
                holder = new StatHolder(stat);
                if (expandable)
                    intStats.Add(holder);
            }
            holder.Value += delta;
        }
        StatHolder GetStatHolder(IntegerStat stat)
        {
            foreach (var statusHolder in intStats)
                if (statusHolder.statusEnum == stat)
                    return statusHolder;
            return null;
        }
    }
}