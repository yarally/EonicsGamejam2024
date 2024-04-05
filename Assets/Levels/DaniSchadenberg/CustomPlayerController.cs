using Lib.Player;
using UnityEngine;

namespace Levels.DaniSchadenberg
{
    public class PlayerController : AbstractPlayerController
    {
        [SerializeField]private int MaxJumpCount = 2;
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
            return (currentState == PlayerState.OnGround || MaxJumpCount > 0) && Input.GetKeyDown(jumpKey);
        }

        protected override void Jump()
        {
            base.Jump();
            MaxJumpCount -= 1;
        }

        protected override void Fall()
        {
            base.Fall();
        }

        protected override void Land()
        {
            base.Land();
            MaxJumpCount = 2;
            setJumpheight(2);
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

        public void setJumpheight(float x)
        {
            this.jumpHeight = x;    
        }
    }
}