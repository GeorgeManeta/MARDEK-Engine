using PlasticPipe.PlasticProtocol.Server.Stubs;
using System;
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
            return GetStatHolder(stat).Value;
        }
        public void ModifyStat(IntegerStat stat, int delta)
        {
            GetStatHolder(stat).Value += delta;
        }
        StatHolder GetStatHolder(IntegerStat stat)
        {
            foreach (var statusHolder in intStats)
                if (statusHolder.statusEnum == stat)
                    return statusHolder;
            var newHolder = new StatHolder(stat);
            if (expandable)
                intStats.Add(newHolder);
            return newHolder;
        }
    }
}