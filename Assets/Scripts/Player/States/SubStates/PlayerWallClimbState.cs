public class PlayerWallClimbState : PlayerTouchingWallState
{
    public PlayerWallClimbState(PlayerStateMachine stateMachine, PlayerController owner, int animatorParamHash) : base(
        stateMachine, owner, animatorParamHash)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isExiting) return;

        // 爬墙时必须有抓墙输入
        if (GrabInput)
        {
            // 离开墙面切换待机状态
            if (!core.CollisionSenses.IsTouchingLedge)
            {
                stateMachine.TransitionTo<PlayerLedgeClimbState>();
            }
            else if (InputY == 1)
            // 有抓墙输入和向上的垂直输入，则向上爬
            {
                core.Movement.SetVelocityY(owner.Data.wallClimbVelocity * InputY);
            }
            // 抓墙时但没有向上的垂直输入，进入抓墙状态
            else
            {
                stateMachine.TransitionTo<PlayerWallGrabState>();
            }
        }
        // 取消抓强输入后，进入滑墙状态
        else
        {
            stateMachine.TransitionTo<PlayerWallSlideState>();
        }
    }
}