using System;
using Lib.Player;
using UnityEngine;

namespace Levels.Demo3
{
    public class PlayerController : AbstractPlayerController
    {
        protected override void Awake()
        {
            base.Awake();
        }

        protected override void Move()
        {
            base.Move();
        }

        protected override bool CanJump()
        {
            return base.CanJump();
        }

        protected override void Jump()
        {
            // This is a check to see if the player is even capable of jumping at the moment (e.g. not if he is on a ladder)
            if (Mathf.Abs(rb.gravityScale) <= 0.001f)
            {
                currentState = PlayerState.OnGround;
                return;
            }
            // The physics engine and my math don't work out exactly, this correction makes the jump height more predictable for lower jumps
            const float correction = 0.55f;
            rb.velocity = new Vector2(rb.velocity.x, (float)Math.Sqrt(2.0f * Mathf.Abs(rb.gravityScale) * jumpHeight * (10f + correction)) * -1f);
            currentState = PlayerState.Jumping;
        }

        protected override void Fall()
        {
            base.Fall();
        }

        protected override void Land()
        {
            base.Land();
        }

        public override void Hurt()
        {
            base.Hurt();
        }

        protected override void FixedUpdate()
        {
            Move();
            if (currentState == PlayerState.Jump)
            {
                Jump();
            }

            // Detects when the player is falling
            if (currentState != PlayerState.Falling && rb.velocity.y > 0)
            {
                Fall();
            }

            // Detects when the player lands on the ground
            if (currentState == PlayerState.Falling && rb.velocity.y == 0)
            {
                Land();
            }
        }

        protected override void OnAction()
        {
            var objRef = Instantiate(interactionTrigger, transform);
            objRef.transform.position += Vector3.down * 0.5f;
            Destroy(objRef, 0.1f);
        }
    }
}