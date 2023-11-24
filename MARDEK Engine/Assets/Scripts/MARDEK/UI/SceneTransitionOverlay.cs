using MARDEK.Movement;
using UnityEngine;
using UnityEngine.UI;

namespace MARDEK.UI {
    public class SceneTransitionOverlay : MonoBehaviour {

        [SerializeField] Image overlayImage;

        void Start()
        {
            overlayImage.color = new Color(0f, 0f, 0f, SceneTransitionOverlayCommand.DetermineAlpha());
        }

        void FixedUpdate()
        {
            overlayImage.color = new Color(0f, 0f, 0f, SceneTransitionOverlayCommand.DetermineAlpha());
        }
    }
}
