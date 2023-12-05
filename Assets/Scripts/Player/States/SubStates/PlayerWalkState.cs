public class PlayerWalkState: PlayerGroundedState {
    public PlayerWalkState(PlayerStateMachine stateMachine, PlayerController owner, int animatorParamHash) : base(stateMachine, owner, animatorParamHash)
    {
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        owner.CheckIfShouldFlip(InputX);
        owner.SetVelocityX(owner.PlayerData.movementVelocity * owner.FacingDirection);
        
        // 横向输入为0时，进入待机状态
        if (InputX == 0)
        {
            stateMachine.TransitionTo<PlayerIdleState>();
        }
    }
}