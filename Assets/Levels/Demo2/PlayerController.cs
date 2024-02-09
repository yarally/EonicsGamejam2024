using Lib.Player;
using UnityEngine;

namespace Levels.Demo2
{
    public class PlayerController : AbstractPlayerController
    {
        private int _jumpCount = 2;
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
            return (currentState == PlayerState.OnGround || _jumpCount > 0) && Input.GetKeyDown(jumpKey);
        }

        protected override void Jump()
        {
            Debug.Log("test");
            _jumpCount--;
            base.Jump();
        }

        protected override void Fall()
        {
            base.Fall();
        }

        protected override void Land()
        {
            _jumpCount = 2;
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
    }
}