using System.Collections.Generic;
using UnityEngine;
using MARDEK.Core;
using MARDEK.Stats;
using MARDEK.Skill;

namespace MARDEK.Inventory
{
    [CreateAssetMenu(menuName = "MARDEK/Inventory/Item")]
    public class Item : AddressableScriptableObject
    {
        [SerializeField] string _displayName;
        public string displayName { get { return _displayName; } set { _displayName = value;} }
        [SerializeField] string _elementText;
        public string elementText { get { return _elementText; } set { _elementText = value;} }
        [SerializeField] string _description;
        public string description { get { return CreateFullDescription(_description); } set { _description = value;} }
        [SerializeField] Sprite _sprite;
        public Sprite sprite { get { return _sprite; } }
        [SerializeField] Element _element;
        public Element element { get { return _element; } set { _element = value;}  }
        [SerializeField] int _price;
        public int price { get { return _price; }  set { _price = value;} }
        public string properties { get { return CreateProperties(); } }

        [field: SerializeField] public List<Skill.Skill> SkillsToEquip { get; private set; }

        public virtual bool CanStack()
        {
            return true;
        }
        protected virtual string CreateFullDescription(string rawDescription)
        {
            return "MISCELLANIOUS ITEM\n\n\n" + rawDescription;
        }
        protected virtual string CreateProperties()
        {
            return "";
        }
    }
}