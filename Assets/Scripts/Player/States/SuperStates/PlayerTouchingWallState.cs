public class PlayerTouchingWallState : PlayerState {
    protected int InputY => owner.InputHandler.NormalizedMovementInput.y;
    protected bool GrabInput => owner.InputHandler.GrabInput;

    protected PlayerTouchingWallState(PlayerStateMachine stateMachine, PlayerController owner, int animatorParamHash) : base(stateMachine, owner, animatorParamHash)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        // 接触地面并且没有抓墙输入时，回到待机状态
        if (owner.IsGrounded && !GrabInput)
        {
            stateMachine.TransitionTo<PlayerIdleState>();
        }
    }
}