using UnityEngine;
using MARDEK.Core;
using MARDEK.Event;

namespace MARDEK.Movement
{
    public class SceneTransitionCommand : CommandBase
    {
        public static WaypointEnum usedWaypoint { get; private set; }
        public static MoveDirection transitionFacingDirection { get; private set; }

        [SerializeField] WaypointEnum waypoint = null;
        [SerializeField] MoveDirection overrideFacingDirection = null;

        bool didTrigger = false;

        public override void Trigger()
        {
            usedWaypoint = waypoint;
            SetupFacingDirection();

            //Command queue won't have the oportunity to reset the lockValue itself cause the scene reload will destroy the object
            PlayerLocks.EventSystemLock = 0;
            didTrigger = true;
        }

        void Update() {
            if (didTrigger && SceneTransitionOverlayCommand.instance.state == 2) {
                SceneTransitionOverlayCommand.instance.state = 1;
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

        public static void ClearUsedWaypoint()
        {
            usedWaypoint = null;
        }
    }
}