public class PlayerGroundedState : PlayerState {
    private PlayerJumpState _jumpState;
    protected int InputX => (int)owner.InputHandler.RawMovementInput.x;

    protected PlayerGroundedState(PlayerStateMachine stateMachine, PlayerController owner, int animatorParamHash) : base(stateMachine, owner, animatorParamHash)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _jumpState = stateMachine.GetStateInstance<PlayerJumpState>();
        _jumpState.ResetJumpCounter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (owner.InputHandler.JumpInput && _jumpState.CanJump)
        {
            stateMachine.TransitionTo<PlayerJumpState>();
        }
        else if (!owner.IsGrounded)
        {
            stateMachine.TransitionTo<PlayerAerialState>();
        }
    }
}