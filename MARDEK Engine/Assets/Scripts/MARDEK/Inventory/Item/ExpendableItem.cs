using UnityEngine;
using MARDEK.Stats;

namespace MARDEK.Inventory
{
    [CreateAssetMenu(menuName = "MARDEK/Inventory/ExpendableItem")]
    public class ExpendableItem : Item
    {
        [SerializeField] int _percentHealthRestore;
        [SerializeField] int _percentManaRestore;
        /*[SerializeField] int _HP;
        [SerializeField] int _MP;
        [SerializeField] int _POW;
        [SerializeField] int _STR;
        [SerializeField] int _SPR;*/
        [SerializeField] string _colorHexCode;
        [SerializeField] string _pfx;
        [SerializeField] bool _canResurrect;
        [SerializeField] bool _canPotionSpray;
        [SerializeField] bool _grantUnderwaterBreathing;
        [SerializeField] StatsSet _statsSet;

        // TODO Giving and curing status effects

        public int percentHealthRestore { get { return _percentHealthRestore; } }
        public int percentManaRestore { get { return _percentManaRestore; } }
        /*public int HP { get { return _HP; } }
        public int MP { get { return _MP; } }
        public int POW { get { return _POW; } }
        public int STR { get { return _STR; } } 
        public int SPR { get { return _SPR; } }*/

        public string colorHexCode { get { return _colorHexCode; } }
        public string pfx { get { return _pfx; } }

        public bool canResurrect { get { return _canResurrect; } }
        public bool canPotionSpray { get { return _canPotionSpray; } }
        public bool grantUnderwaterBreathing { get { return _grantUnderwaterBreathing; } }

        public StatsSet statsSet { get { return _statsSet; } private set{_statsSet = value;} }

        public ExpendableItem(){
            statsSet = new StatsSet();
        }

        override protected string CreateFullDescription(string rawDescription)
        {
            return "EXPENDABLE ITEM\n\n\n" + rawDescription;
        }
    }
}