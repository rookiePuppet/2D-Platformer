public class PlayerTouchingWallState : PlayerState
{
    protected bool GrabInput => owner.InputHandler.GrabInput;

    protected PlayerTouchingWallState(PlayerStateMachine stateMachine, PlayerController owner, int animatorParamHash) : base(stateMachine, owner, animatorParamHash)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (owner.InputHandler.JumpInput)
        {
            stateMachine.TransitionTo<PlayerWallJumpState>();
        }
        // 接触地面并且没有抓墙输入时，回到待机状态
        else if (core.CollisionSenses.IsGrounded && !GrabInput)
        {
            stateMachine.TransitionTo<PlayerIdleState>();
        }

    }
}