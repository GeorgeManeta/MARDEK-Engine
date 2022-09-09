using UnityEngine;
using System.Collections.Generic;
using MARDEK.Core;

namespace MARDEK.Movement
{
    [RequireComponent(typeof(GridObject))]
    public class Waypoint : MonoBehaviour
    {
        [SerializeField] WaypointEnum thisWaypoint = null;

        private void Awake()
        {
            if (SceneTransitionCommand.usedWaypoint != null)
            {
                if (thisWaypoint == SceneTransitionCommand.usedWaypoint)
                {
                    var pos = new List<Vector2>();
                    var directions = new List<MoveDirection>();

                    pos.Add(transform.position);
                    directions.Add(SceneTransitionCommand.transitionFacingDirection);

                    MapParty.OverrideAfterTransition(pos, directions);
                    SceneTransitionCommand.ClearUsedWaypoint();
                }
            }
        }
    } 
}