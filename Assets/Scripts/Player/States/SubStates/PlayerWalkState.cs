public class PlayerWalkState : PlayerGroundedState
{
    public PlayerWalkState(PlayerStateMachine stateMachine, PlayerController owner, int animatorParamHash) : base(stateMachine, owner, animatorParamHash)
    {
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (isExiting) return;

        core.Movement.CheckIfShouldFlip(InputX);
        core.Movement.SetVelocityX(owner.Data.movementVelocity * core.Movement.FacingDirection);

        // 横向输入为0时，进入待机状态
        if (InputX == 0)
        {
            stateMachine.TransitionTo<PlayerIdleState>();
        }
        else if (InputY == -1)
        {
            stateMachine.TransitionTo<PlayerCrouchMoveState>();
        }
    }
}