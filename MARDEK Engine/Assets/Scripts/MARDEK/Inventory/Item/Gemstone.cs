using UnityEngine;
using MARDEK.Stats;
using System.Collections.Generic;

namespace MARDEK.Inventory
{
    public class Gemstone : EquippableItem
    {
        // Temporary fields only for until proper implementation
        [SerializeField] string _pfx;
        public string pfx { get { return _pfx; } }
        [SerializeField] int _spellDMG;
        public int spellDMG { get { return _spellDMG; } }
        [SerializeField] string _STFX;
        public string STFX { get { return _STFX; } }
        [SerializeField] int _STFXStrength;
        public int STFXStrength { get { return _STFXStrength; } }
    }
}