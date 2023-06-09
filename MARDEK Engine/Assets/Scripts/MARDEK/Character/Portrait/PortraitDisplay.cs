using System.Collections.Generic;
using UnityEngine;

namespace MARDEK.CharacterSystem
{
    public class PortraitDisplay : MonoBehaviour
    {
        [field: SerializeField] List<MonoBehaviour> portraitPrefabs;
        PortraitPrefab activePortrait;

        // Lets you test the appearance of a character portrait.
        [SerializeField] CharacterPortrait testPortrait;
        [ContextMenu("Test Portrait")]
        void TestPortrait()
        {
            SetPortrait(testPortrait);
        }

        public void SetPortrait(CharacterPortrait portrait)
        {
            if (portrait != null)
            {
                gameObject.SetActive(true);

                foreach (var portraitPrefab in portraitPrefabs) {
                    if ( ((PortraitPrefab) portraitPrefab).PortraitType == portrait.PortraitType )
                    {
                        ((PortraitPrefab) portraitPrefab).SetPortrait(portrait);
                        portraitPrefab.gameObject.SetActive(true);
                        activePortrait = (PortraitPrefab) portraitPrefab;
                    }
                    else
                    {
                        portraitPrefab.gameObject.SetActive(false);
                    }
                }
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        public void SetExpression(PortraitExpressionEnum expression)
        {
            if (activePortrait != null)
            {
                activePortrait.SetExpression(expression);
            }
        }
    }
}