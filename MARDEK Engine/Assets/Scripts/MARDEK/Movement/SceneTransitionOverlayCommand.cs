using MARDEK.Event;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MARDEK.Movement {
    public class SceneTransitionOverlayCommand : CommandBase
    {
        const int MAX_STATE = 70;
        const int MIN_STATE = -70;

        private static int staticState;

        public static SceneTransitionOverlayCommand instance = null;

        public static float DetermineAlpha() {
            int state = staticState;
            if (instance != null) state = instance.state;
            else
            {
                if (staticState < 0 && staticState > MIN_STATE) staticState -= 1;
                else staticState = 0;
            }
            if (state > 0) return (MAX_STATE - state) / (MAX_STATE - 1f);
            if (state < 0) return (-MIN_STATE + state) / -(MIN_STATE + 1f);
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
            if (state > 2) state -= 1;
            else if (state == 1)
            {
                state = -1;
                staticState = -1;
                loadingScene.allowSceneActivation = true;
            }
        }
    }
}
