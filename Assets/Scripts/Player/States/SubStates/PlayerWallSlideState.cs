public class PlayerWallSlideState : PlayerState {
    private int InputY => (int)owner.InputHandler.RawMovementInput.y;

    public PlayerWallSlideState(PlayerStateMachine stateMachine, PlayerController owner, int animatorParamHash) : base(stateMachine, owner, animatorParamHash)
    {
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (InputY != 0) return;
        owner.SetVelocityY(-owner.PlayerData.wallSlideVelocity);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (owner.IsGrounded)
        {
            stateMachine.TransitionTo<PlayerIdleState>();
        }
    }
}