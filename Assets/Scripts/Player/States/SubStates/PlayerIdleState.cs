public class PlayerIdleState : PlayerGroundedState {
    public PlayerIdleState(PlayerStateMachine stateMachine, PlayerController owner, int animatorParamHash) : base(stateMachine, owner, animatorParamHash)
    {
    }

    public override void Enter()
    {
        base.Enter();
        owner.SetVelocity(0, 0);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        // 有横向输入时，进入行走状态
        if (InputX != 0)
        {
            stateMachine.TransitionTo<PlayerWalkState>();
        }
    }
}