public class PlayerWallSlideState : PlayerTouchingWallState {
    public PlayerWallSlideState(PlayerStateMachine stateMachine, PlayerController owner, int animatorParamHash) : base(stateMachine, owner, animatorParamHash)
    {
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        
        // 离开墙面切换待机状态
        if (!owner.IsTouchingWall)
        {
            stateMachine.TransitionTo<PlayerIdleState>();
        }
        else if (GrabInput)
        {
            stateMachine.TransitionTo<PlayerWallGrabState>();
        }
        else if (InputY != -1)
        {
            owner.SetVelocityY(-owner.Data.wallSlideVelocity);
        }
    }
}