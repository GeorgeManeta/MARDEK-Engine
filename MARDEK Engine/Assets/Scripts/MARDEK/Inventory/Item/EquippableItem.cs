using UnityEngine;
using MARDEK.Stats;
using System.Collections.Generic;
//using MARDEK.CharacterSystem;

namespace MARDEK.Inventory
{
    [CreateAssetMenu(menuName = "MARDEK/Inventory/EquippableItem")]
    public class EquippableItem : Item
    {
        [SerializeField] EquipmentCategory _category;
        [SerializeField] StatsSet _statsSet;
        
        // TODO Skills
        // TODO Status effects

        // Temporary field until SFX are implemented to mark which ones belong to which weapons. Remove for final build.
        [SerializeField] string _hitSFX;
        public string hitSFX { get { return _hitSFX; } }

        // Temporary fields only for use during import
        /*[SerializeField] int _ATK;
        public int ATK { get { return _ATK; } }
        [SerializeField] int _CRIT;
        public int CRIT { get { return _CRIT; } }
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
        [SerializeField] int _MaxMP;
        public int MaxMP { get { return _MaxMP; } }
        [SerializeField] int _MaxHP;
        public int MaxHP { get { return _MaxHP; } }
        [SerializeField] string _categoryText;
        public string categoryText { get { return _categoryText; } set{_categoryText = value;}}*/

        public EquipmentCategory category { get { return _category; } set{_category=value;} }

        public StatsSet statBoosts { get { return _statsSet; } private set{_statsSet = value;} }

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

        protected override string CreateProperties()
        {
            string result = "";
            // TODO Evasion and Hit
            result += CreateStatsString(new string[]{"ATK", "CRIT", "DEF", "MDEF"});
            if (!this.element.name.Equals("Normal")) result += this.element.name.ToUpper() + " Elemental\n";
            result += CreateStatsString(new string[]{"AGL", "SPR", "STR", "VIT"});
            result += CreateStatsString(new string[]{"MaxHP", "MaxMP"});
            result += CreateStatsString(new string[]{"FireResistance"}); 
            result += CreateStatsString(new string[]{"WaterResistance"}); 
            result += CreateStatsString(new string[]{"EarthResistance"}); 
            result += CreateStatsString(new string[]{"AirResistance"}); 
            result += CreateStatsString(new string[]{"LightResistance"}); 
            result += CreateStatsString(new string[]{"DarkResistance"}); 
            result += CreateStatsString(new string[]{"AetherResistance"}); 
            result += CreateStatsString(new string[]{"FigResistance"});  
            result += CreateStatsString(new string[]{"PhysicalResistance"});  
            result += CreateStatsString(new string[]{"ThaumaResistance"}); 
        // TODO Automatic status effects
            return result;
        }

        override public Color GetInventorySpaceColor()
        {
            return category.color.ToColor();
        }
    }
}
