using System.Collections.Generic;
using UnityEngine;

namespace MARDEK.CharacterSystem
{
    public class PortraitDisplay : MonoBehaviour
    {
        [field: SerializeField] List<MonoBehaviour> portraitPrefabs;

        public void SetPortrait(CharacterPortrait portrait)
        {
            if (portrait != null)
            {
                this.gameObject.SetActive(true);

                foreach (var portraitPrefab in portraitPrefabs) {
                    if ( ((IPortrait) portraitPrefab).PortraitType == portrait.PortraitType )
                    {
                        ((IPortrait) portraitPrefab).SetPortrait(portrait);
                        portraitPrefab.gameObject.SetActive(true);
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
    }
}