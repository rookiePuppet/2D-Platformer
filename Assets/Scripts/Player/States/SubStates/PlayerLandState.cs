public class PlayerLandState : PlayerGroundedState {
    public PlayerLandState(PlayerStateMachine stateMachine, PlayerController owner, int animatorParamHash) : base(stateMachine, owner, animatorParamHash)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        // 落地时有横向输入，直接进入行走状态
        if (InputX != 0)
        {
            stateMachine.TransitionTo<PlayerWalkState>();
        }
        // 没有横向输入时，等待动画播放完毕才进入待机状态
        else if (isAnimationFinished)
        {
            stateMachine.TransitionTo<PlayerIdleState>();
        }
    }
}