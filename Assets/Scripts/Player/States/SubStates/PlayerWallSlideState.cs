public class PlayerWallSlideState : PlayerTouchingWallState {
    public PlayerWallSlideState(PlayerStateMachine stateMachine, PlayerController owner, int animatorParamHash) : base(stateMachine, owner, animatorParamHash)
    {
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (GrabInput)
        {
            stateMachine.TransitionTo<PlayerWallGrabState>();
        }
        else if (InputY != -1)
        {
            owner.SetVelocityY(-owner.PlayerData.wallSlideVelocity);
        }
    }
}