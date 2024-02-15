using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchIdleState : PlayerGroundedState
{
    public PlayerCrouchIdleState(PlayerStateMachine stateMachine, PlayerController owner, int animatorParamHash) : base(stateMachine, owner, animatorParamHash)
    {
    }

    public override void Enter()
    {
        base.Enter();
        owner.SetVelocity(0f, 0f);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isExiting) return;

        if (InputY != -1)
        {
            if (InputX == 0)
            {
                stateMachine.TransitionTo<PlayerIdleState>();
            }
            else
            {
                stateMachine.TransitionTo<PlayerWalkState>();
            }
        }
        else if (InputX != 0)
        {
            stateMachine.TransitionTo<PlayerCrouchMoveState>();
        }
    }
}
