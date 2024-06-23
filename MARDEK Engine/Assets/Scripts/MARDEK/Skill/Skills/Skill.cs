using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MARDEK.Stats;
using MARDEK.Core;

namespace MARDEK.Skill
{
    public abstract class Skill : AddressableScriptableObject
    {
        [field: SerializeField] public string DisplayName { get; private set; }
        [field: SerializeField] public string Description { get; private set; }
        [field: SerializeField] public int Cost { get; private set; }
        [field: SerializeField] public int PointsRequiredToMaster { get; private set; }
        [field: SerializeField] public Element Element { get; private set; }

        public abstract void Apply(IStats user, IStats target);
    }
}