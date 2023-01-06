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
        public void OnValidate(){
            if(element == null){
                Debug.LogError("ELEMENT IS MISSING IN " + this.displayName);
            }
        }
        [SerializeField] string _displayName;
        public string displayName { get { return _displayName; } }
        // Added only to fascilitate mass inputs. Should not be active unless you are attempting to create items via JSONReader.cs
        /*[SerializeField] string _elementText;
        public string elementText { get { return _elementText; } }*/
        [SerializeField] string _description;
        public string description { get { return _description; } }
        [SerializeField] Sprite _sprite;
        public Sprite sprite { get { return _sprite; } }
        [SerializeField] Element _element;
        // Setter is needed to mass import items, but should be unavailable for normal development
        public Element element { get { return _element; } /*set{ _element = value;}*/ }
        [SerializeField] int _price;
        public int price { get { return _price; } }
        public string properties { get { return CreateProperties(); } }

        [field: SerializeField] public List<Skill.Skill> SkillsToEquip { get; private set; }

        public virtual bool CanStack()
        {
            return true;
        }
        protected virtual string CreateProperties()
        {
            return "";
        }

        /**
         * The color that will be used to display the type of item this is when the player is in the battle loot
         * menu or the treasure chest menu.
         */
        public virtual Color GetInventorySpaceColor()
        {
            return new Color(193f / 255f, 145 / 255f, 89f / 255f);
        }
    }
}