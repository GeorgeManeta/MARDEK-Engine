using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using MARDEK.Core;
using MARDEK.Event;
using MARDEK.Animation;

namespace MARDEK.Movement
{
    public class PlayerController : MovementController
    {
        static PlayerController instance;
        MoveDirection desiredDirection = null;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                GetComponentInChildren<SpriteAnimator>().SetAsPlayerAnimator();
            }
            else
                Debug.LogError("there was already a PlayerController instance when a new PlayerController awoke");
        }

        public static Movable GetPlayerMovement()
        {
            if (instance)
                return instance.movement;
            else
                return null;
        }

        public void OnInteraction(InputAction.CallbackContext ctx)
        {
            if (PlayerLocks.isPlayerLocked)
                return;

            if (ctx.performed == false)
                return;
            if(movement == null || movement.isMoving || movement.currentDirection == null)
                return;

            movement.colliderHelper.OffsetCollider(movement.currentDirection.value);

            List<Collider2D> collidersHit = movement.colliderHelper.Overlaping();
            foreach(Collider2D c in collidersHit)
            {
                EventTrigger ev = c.GetComponent<EventTrigger>();
                if (ev) ev.Interact();
            }
            movement.colliderHelper.OffsetCollider(Vector2.zero);
        }
        
        public void OnMovementInput(InputAction.CallbackContext ctx)
        {
            Vector2 direction = ctx.ReadValue<Vector2>();
            if (direction.x == 0 || direction.y == 0)
                desiredDirection = ApproximanteDirectionByVector2(direction);
            else
                desiredDirection = null;
        }

        private void Update()
        {
            if (PlayerLocks.isPlayerLocked)
                return;
            SendDirection(desiredDirection);
        }
    }
}