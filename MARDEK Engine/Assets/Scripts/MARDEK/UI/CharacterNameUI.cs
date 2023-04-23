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

        void Start()
        {
            characteName.text = characterUI.character.Profile == null ? "Null" : characterUI.character.Profile.displayName;
        }
    }
}