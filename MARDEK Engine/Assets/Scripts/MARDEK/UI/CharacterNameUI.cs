using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace MARDEK.UI
{
    public class CharacterNameUI : MonoBehaviour
    {
        [SerializeField] CharacterUI characterUI;
        [SerializeField] TextMeshProUGUI characteName;

        void OnEnable()
        {
            if(characterUI.character?.Profile == null)
                characteName.text = "Null";
            else
                characteName.text = characterUI.character.Profile.displayName;
        }
    }
}