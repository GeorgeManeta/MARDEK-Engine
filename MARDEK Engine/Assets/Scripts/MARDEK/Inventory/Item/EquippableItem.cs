using UnityEngine;
using MARDEK.Stats;
using System.Collections.Generic;

namespace MARDEK.Inventory
{
    [CreateAssetMenu(menuName = "MARDEK/Inventory/EquippableItem")]
    public class EquippableItem : Item
    {
        [SerializeField] EquipmentCategory _category;
        [SerializeField] StatsSet _statsSet;

        // TODO Skills
        // TODO Status effects

        [SerializeField] int _ATK;
        public int ATK { get { return _ATK; } }
        [SerializeField] int _CRITICAL;
        public int CRITICAL { get { return _CRITICAL; } }
        [SerializeField] int _DEF;
        public int DEF { get { return _DEF; } }
        [SerializeField] int _EVA;
        public int EVA { get { return _EVA; } }
        [SerializeField] int _HIT;
        public int HIT { get { return _HIT; } }
        [SerializeField] int _MDEF;
        public int MDEF { get { return _MDEF; } }
        [SerializeField] int _SPR;
        public int SPR { get { return _SPR; } }
        [SerializeField] int _STR;
        public int STR { get { return _STR; } }
        [SerializeField] int _VIT;
        public int VIT { get { return _VIT; } }

        //public CharacterInfo[] onlyUsers { get { return _onlyUsers; } set{_onlyUsers=value;} }
        [field: SerializeField] public List<Skill.Skill> OnlyUsers { get; private set; }

        public EquipmentCategory category { get { return _category; } set{_category=value;} }

        public StatsSet statBoosts { get { return _statsSet; } set{_statsSet = value;} }

        public EquippableItem(){
            statBoosts = new StatsSet();
        }

        public override bool CanStack()
        {
            return false;
        }

        string CreateStatsString(string[] names)
        {
            string result = "";
            foreach (string importantStatName in names)
            {
                foreach (var candidateStat in this.statBoosts.intStats)
                {
                    if (candidateStat.statusEnum.name.Equals(importantStatName))
                    {
                        result += importantStatName + ": " + candidateStat.Value + "\n";
                    }
                }
                foreach (var candidateStat in this.statBoosts.floatStats)
                {
                    if (candidateStat.statusEnum.name.Equals(importantStatName))
                    {
                        result += importantStatName + ": " + candidateStat.Value + "\n";
                    }
                }
            }
            return result;
        }

        override protected string CreateFullDescription(string rawDescription)
        {
            return this.category.classification + "\n\n" + CreateStatsString(new string[]{"ATK", "DEF", "MDEF"}) + "\n" + rawDescription;
        }

        protected override string CreateProperties()
        {
            string result = "";
            result += CreateStatsString(new string[]{"ATK", "CRIT", "DEF", "MDEF"});
            if (!this.element.name.Equals("Normal")) result += this.element.name.ToUpper() + " Elemental\n";
            result += CreateStatsString(new string[]{"AGL", "SPR", "STR", "VIT"});
            result += CreateStatsString(new string[]{"MaxHP", "MaxMP"});
            result += CreateStatsString(new string[]{"FireResistance"}); // TODO Other resistances
        // TODO Automatic status effects
            return result;
        }
    }
}