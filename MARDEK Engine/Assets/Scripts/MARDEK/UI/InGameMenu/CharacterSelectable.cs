using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MARDEK.CharacterSystem;
using UnityEngine.Serialization;
using TMPro;

namespace MARDEK.UI
{
    using Progress;
    public class CharacterSelectable : SelectableWithCurrentSelected<CharacterSelectable>
    {
        [SerializeField] TextMeshProUGUI characterNameText;
        [SerializeField] GameObject wrapper;
        public Character Character { 
            get
            {
                var index = transform.GetSiblingIndex();
                if (Party.Instance == null || Party.Instance.Characters.Count <= index)
                    return null;
                return Party.Instance.Characters[index];
            }
        }

        public override bool IsValid()
        {
            return Character != null;
        }

        private void OnEnable()
        {
            if (IsValid())
            {
                if (wrapper)
                    wrapper.SetActive(true);
                if (characterNameText)
                    characterNameText.text = Character.Profile.displayName;
            }
            else
            {
                if (wrapper)
                    wrapper.SetActive(false);
            }
        }
    }
}