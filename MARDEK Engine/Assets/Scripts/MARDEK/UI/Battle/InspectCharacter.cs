using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MARDEK.UI
{
    public class InspectCharacter : MonoBehaviour
    {
        [SerializeField] TMP_Text characterName;
        [SerializeField] TMP_Text characterLevel;
        private void OnEnable()
        {
            characterName.text = BattleUIManager.Instance.characterBeingInspected.Profile.displayName;
            var level = BattleUIManager.Instance.characterBeingInspected.GetStat(Stats.StatsGlobals.Instance.Level);
            characterLevel.text = $"Level {level}";
        }
    }
}