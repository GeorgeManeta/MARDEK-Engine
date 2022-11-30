using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MARDEK.UI
{
    public class AttackButton : MonoBehaviour
    {
        //[SerializeField] Image sprite;
        [SerializeField] CharacterSystem.SkillSlot attackSkillSlot;

        public void SelectAction()
        {
            Battle.BattleManager.selectedAction = attackSkillSlot;
        }
    }
}