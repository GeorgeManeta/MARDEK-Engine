using UnityEngine;
using UnityEngine.SceneManagement;
using MARDEK.Core;
using MARDEK.Event;

namespace MARDEK.Movement
{
    public class SceneTransitionCommand : CommandBase
    {
        const int MAX_STATE = 70;
        const int MIN_STATE = -70;

        private static int staticState;

        static SceneTransitionCommand instance = null;

        public static WaypointEnum usedWaypoint { get; private set; }
        public static MoveDirection transitionFacingDirection { get; private set; }

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

        public static void ClearUsedWaypoint()
        {
            usedWaypoint = null;
        }

        [SerializeField] WaypointEnum waypoint = null;
        [SerializeField] MoveDirection overrideFacingDirection = null;
        [SerializeField] SceneReference scene = null;

        AsyncOperation loadingScene = null;
        public int state = 0;
        bool didTrigger = false;

        public override void Prepare()
        {
            instance = this;
            state = MAX_STATE;
            loadingScene = SceneManager.LoadSceneAsync(scene);
            loadingScene.allowSceneActivation = false;
        }

        public override void Trigger()
        {
            usedWaypoint = waypoint;
            SetupFacingDirection();

            //Command queue won't have the oportunity to reset the lockValue itself cause the scene reload will destroy the object
            PlayerLocks.EventSystemLock = 0;
            didTrigger = true;
        }

        void Update() {
            if (didTrigger && instance.state == 2) {
                instance.state = 1;
                didTrigger = false;
            }
        }

        void SetupFacingDirection()
        {
            if (overrideFacingDirection)
                transitionFacingDirection = overrideFacingDirection;
            else
            {
                var player = PlayerController.GetPlayerMovement();
                if (player)
                    transitionFacingDirection = player.currentDirection;
                else
                    transitionFacingDirection = null;
            }
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
