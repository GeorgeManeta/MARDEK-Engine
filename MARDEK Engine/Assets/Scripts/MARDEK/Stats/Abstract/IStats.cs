namespace MARDEK.Stats 
{
    public interface IStats
    {
        public int GetStat(IntegerStat stat);
        public void ModifyStat(IntegerStat stat, int delta);
    } 
}