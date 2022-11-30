using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MARDEK.Stats;

namespace MARDEK.Skill
{
    public abstract class Skill : ScriptableObject
    {
        [field: SerializeField] public string DisplayName { get; private set; }
        [field: SerializeField] public string Description { get; private set; }
        [field: SerializeField] public int Cost { get; private set; }
        [field: SerializeField] public int PointsRequiredToMaster { get; private set; }
        [field: SerializeField] public Element Element { get; private set; }

        public abstract void Apply(IStats user, IStats target);
    }
}