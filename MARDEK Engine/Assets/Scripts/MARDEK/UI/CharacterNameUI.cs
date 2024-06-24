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
            var profile = characterUI.character?.Character?.Profile;
            if (profile == null)
                characteName.text = "Null";
            else
                characteName.text = profile.displayName;
        }
    }
}