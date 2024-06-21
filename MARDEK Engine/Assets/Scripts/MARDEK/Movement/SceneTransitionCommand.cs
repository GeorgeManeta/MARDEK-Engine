using UnityEngine;
using UnityEngine.SceneManagement;
using MARDEK.Core;
using MARDEK.Event;

namespace MARDEK.Movement
{
    public class SceneTransitionCommand : CommandBase
    {
        public static WaypointEnum usedWaypoint { get; private set; }
        public static MoveDirection transitionFacingDirection { get; private set; }

        [SerializeField] SceneReference scene = null;
        [SerializeField] WaypointEnum waypoint = null;
        [SerializeField] MoveDirection overrideFacingDirection = null;

        public override void Trigger()
        {
            usedWaypoint = waypoint;
            SetupFacingDirection();

            //Command queue won't have the oportunity to reset the lockValue itself cause the scene reload will destroy the object
            PlayerLocks.EventSystemLock = 0;

            SceneManager.LoadScene(scene);
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