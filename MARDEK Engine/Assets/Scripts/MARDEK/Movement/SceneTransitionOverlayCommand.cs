using MARDEK.Event;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MARDEK.Movement {
    public class SceneTransitionOverlayCommand : CommandBase
    {
        const int MAX_STATE = 70;
        const int MIN_STATE = -20;

        public static SceneTransitionOverlayCommand instance = null;

        public static float DetermineAlpha() {
            if (instance == null) return 0f;
            if (instance.state > 0) return (MAX_STATE - instance.state) / (MAX_STATE * 0.8f);
            if (instance.state < 0) return (-MIN_STATE + instance.state) / -(MIN_STATE * 0.8f);
            return 0f;
        }

        [SerializeField] SceneReference scene = null;

        AsyncOperation loadingScene = null;
        public int state = 0;

        public override void Trigger()
        {
            instance = this;
            state = MAX_STATE;
            loadingScene = SceneManager.LoadSceneAsync(scene);
            loadingScene.allowSceneActivation = false;
        }

        void FixedUpdate() {
            if (state > 2 || state < 0) state -= 1;
            else if (state == 1)
            {
                state = -1;
                loadingScene.allowSceneActivation = true;
            }

            if (state < MIN_STATE)
            {
                state = 0;
                instance = null;
            }
        }
    }
}
