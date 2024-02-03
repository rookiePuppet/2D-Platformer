using UnityEngine;

public class PlayerWallJumpState : PlayerAbilityState
{
    public PlayerWallJumpState(PlayerStateMachine stateMachine, PlayerController owner, int animatorParamHash) : base(
        stateMachine, owner, animatorParamHash)
    {
    }

    public override void Enter()
    {
        base.Enter();
        var jumpState = stateMachine.GetStateInstance<PlayerJumpState>();
        jumpState.SetJumpCounterWhenWallJump();
        
        var wallJumpDirection = -owner.FacingDirection;
        owner.SetVelocity(owner.Data.wallJumpVelocity, owner.Data.wallJumpAngle, wallJumpDirection);
        owner.CheckIfShouldFlip(wallJumpDirection);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        owner.Animator.SetFloat(velocityXHash, Mathf.Abs(owner.CurrentVelocity.x));
        owner.Animator.SetFloat(velocityYHash, owner.CurrentVelocity.y);

        if (Time.time >= startTime + owner.Data.wallJumpTime)
        {
            isAbilityDone = true;
        }
    }
}