using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MARDEK.Battle;
using UnityEngine.UI;

namespace MARDEK.UI
{
    public class SetCurrentCharacterInfo : MonoBehaviour
    {
        [SerializeField] Text nameLabel = null;
        //[SerializeField] Image elementIcon = null;

        private void OnEnable()
        {
            var character = BattleManager.characterActing;
            if (character != null)
            {
                nameLabel.text = character.CharacterInfo.displayName;
            }
            else
            {
                nameLabel.text = " - ";
            }
        }
    }
}