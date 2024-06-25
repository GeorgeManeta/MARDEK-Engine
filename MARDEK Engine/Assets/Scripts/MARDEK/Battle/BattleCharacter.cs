using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MARDEK.Battle
{
    using Core;
    using CharacterSystem;
    using MARDEK.Stats;

    public class BattleCharacter : IActor, IStats
    {
        public Character Character { get; private set; }
        BattleModel battleModel = null;
        public string Name { get { return Character.Profile.displayName; } }
        public BattleCharacter(Character character_, Vector3 position)
        {
            Character = character_;
            battleModel = Object.Instantiate(Character.Profile.BattleModelPrefab).GetComponent<BattleModel>();
            battleModel.SetBattlePosition(position);
        }

        public int GetStat(IntegerStat stat)
        {
            return Character.GetStat(stat);
        }

        public void ModifyStat(IntegerStat stat, int delta)
        {
            Character.ModifyStat(stat, delta);
        }
    }
}