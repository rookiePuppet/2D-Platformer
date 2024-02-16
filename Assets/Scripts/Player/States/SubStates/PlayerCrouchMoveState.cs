using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchMoveState : PlayerGroundedState
{
    private bool _isTouchingCeiling;

    public PlayerCrouchMoveState(PlayerStateMachine stateMachine, PlayerController owner, int animatorParamHash) : base(stateMachine, owner, animatorParamHash)
    {
    }

    public override void Enter()
    {
        base.Enter();
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

        owner.CheckIfShouldFlip(InputX);
        owner.SetVelocityX(owner.Data.crouchMovementVelocity * owner.FacingDirection);

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
        else if (InputX == 0)
        {
            stateMachine.TransitionTo<PlayerCrouchIdleState>();
        }
    }
}
