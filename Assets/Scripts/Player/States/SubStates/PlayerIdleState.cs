﻿public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(PlayerStateMachine stateMachine, PlayerController owner, int animatorParamHash) : base(stateMachine, owner, animatorParamHash)
    {
    }

    public override void Enter()
    {
        base.Enter();
        core.Movement.SetVelocity(0, 0);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isExiting) return;

        // 有横向输入时，进入行走状态
        if (InputX != 0)
        {
            stateMachine.TransitionTo<PlayerWalkState>();
        }
        else if (InputY == -1)
        {
            stateMachine.TransitionTo<PlayerCrouchIdleState>();
        }
    }
}