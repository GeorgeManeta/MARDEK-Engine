using System.Collections.Generic;
using UnityEngine;
using MARDEK.Audio;

namespace MARDEK.UI
{
    public class SubmenuLayoutController : SelectableLayout
    {        
        [SerializeField] AudioObject focusSound;
        [SerializeField] AudioObject unfocusSound;
        [SerializeField] GameObject blurPanel;
        [SerializeField] InputReader disableOnUnfocus;

        public void Unfocus()
        {
            enabled = false;
            blurPanel.SetActive(true);
            if(disableOnUnfocus)
                disableOnUnfocus.enabled = false;
            AudioManager.PlaySoundEffect(unfocusSound);
        }

        public void Focus()
        {
            enabled = true;
            blurPanel.SetActive(false);
            if(disableOnUnfocus)
                disableOnUnfocus.enabled = true;
            AudioManager.PlaySoundEffect(focusSound);
        }

        public bool IsFocussed()
        {
            return enabled;
        }
    }
}