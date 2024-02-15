using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchMoveState : PlayerGroundedState
{
    public PlayerCrouchMoveState(PlayerStateMachine stateMachine, PlayerController owner, int animatorParamHash) : base(stateMachine, owner, animatorParamHash)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isExiting) return;
        
        owner.CheckIfShouldFlip(InputX);
        owner.SetVelocityX(owner.Data.crouchMovementVelocity * owner.FacingDirection);

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
        else if (InputX == 0)
        {
            stateMachine.TransitionTo<PlayerCrouchIdleState>();
        }
    }
}
