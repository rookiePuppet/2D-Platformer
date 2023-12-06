public class PlayerWallClimbState : PlayerTouchingWallState {
    public PlayerWallClimbState(PlayerStateMachine stateMachine, PlayerController owner, int animatorParamHash) : base(stateMachine, owner, animatorParamHash)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        // 爬墙时必须有抓墙输入
        if (GrabInput)
        {
            // 抓墙时但没有向上的垂直输入，进入抓墙状态
            if (InputY != 1)
            {
                stateMachine.TransitionTo<PlayerWallGrabState>();
            }
            // 有抓墙输入和向上的垂直输入，则向上爬
            else
            {
                owner.SetVelocityY(owner.PlayerData.wallClimbVelocity * InputY);
            }
        }
        // 取消抓强输入后，进入滑墙状态
        else
        {
            stateMachine.TransitionTo<PlayerWallSlideState>();
        }
    }
}