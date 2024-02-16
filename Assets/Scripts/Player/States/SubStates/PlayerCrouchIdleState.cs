using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchIdleState : PlayerGroundedState
{
    private bool _isTouchingCeiling;

    public PlayerCrouchIdleState(PlayerStateMachine stateMachine, PlayerController owner, int animatorParamHash) : base(stateMachine, owner, animatorParamHash)
    {
    }

    public override void Enter()
    {
        base.Enter();
        owner.SetVelocity(0f, 0f);
        owner.SetCrouchCollider();
    }

    public override void Exit()
    {
        base.Exit();
        owner.SetNormalCollider();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isExiting) return;

        _isTouchingCeiling = owner.IsTouchingCeiling;

        if (InputY != -1 && !_isTouchingCeiling)
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
