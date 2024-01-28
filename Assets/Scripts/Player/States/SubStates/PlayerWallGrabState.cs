using UnityEngine;

public class PlayerWallGrabState : PlayerTouchingWallState {
    private Vector3 _grabPosition;

    public PlayerWallGrabState(PlayerStateMachine stateMachine, PlayerController owner, int animatorParamHash) : base(stateMachine, owner, animatorParamHash)
    {
    }

    public override void Enter()
    {
        base.Enter();
        // 记录抓墙位置
        _grabPosition = owner.transform.position;
        // 人物垂直速度置为0
        owner.SetVelocityY(0);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isExiting) return;

        // 取消抓墙进入滑墙状态
        if (!GrabInput)
        {
            stateMachine.TransitionTo<PlayerWallSlideState>();
        }
        // 有抓墙并且有向上垂直输入时，进入爬墙状态
        else if (InputY == 1)
        {
            stateMachine.TransitionTo<PlayerWallClimbState>();
        }

        HoldPosition();
    }

    /// <summary>
    /// 固定人物位置
    /// </summary>
    private void HoldPosition()
    {
        owner.SetVelocityY(0);
        owner.SetPosition(_grabPosition);
    }
}