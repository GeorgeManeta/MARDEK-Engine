using System.Collections.Generic;
using UnityEngine;

namespace MARDEK.CharacterSystem
{
    public class PortraitDisplay : MonoBehaviour
    {
        [field: SerializeField] List<MonoBehaviour> portraitPrefabs;
        PortraitPrefab activePortrait;

        public void SetPortrait(CharacterPortrait portrait)
        {
            if (portrait != null)
            {
                this.gameObject.SetActive(true);

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
                this.gameObject.SetActive(false);
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