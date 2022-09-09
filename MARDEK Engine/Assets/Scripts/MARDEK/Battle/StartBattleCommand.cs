using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MARDEK.Event;
using UnityEngine.SceneManagement;

namespace MARDEK.Battle
{
    public class StartBattleCommand : CommandBase
    {
        [SerializeField] EncounterSet encounter = null;
        [SerializeField] SceneReference battleScene = default;

        public override void Trigger()
        {
            BattleManager.encounter = encounter;
            SceneManager.LoadScene(battleScene);
        }
    }
}