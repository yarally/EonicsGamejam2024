using Lib.Player;
using UnityEngine;

namespace Levels.IlhanCicilia
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
            base.Jump();
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

        protected override void OnAction()
        {
            base.OnAction();
            Debug.Log("Action key pressed!");
        }

        public void gainJumpHeight()
        {
            this.jumpHeight += 1;
            Debug.Log("jump height increased");
        }
    }
}