using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MARDEK.UI
{
    public class CharacterNameUI : MonoBehaviour
    {
        [SerializeField] CharacterUI characterUI;
        [SerializeField] Text characteName;

        void Start()
        {
            characteName.text = characterUI.character.Profile == null ? "Null" : characterUI.character.Profile.displayName;
        }
    }
}