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

        var wallJumpDirection = -core.Movement.FacingDirection;
        core.Movement.SetVelocity(owner.StatesConfigSo.wallJumpVelocity, owner.StatesConfigSo.wallJumpAngle, wallJumpDirection);
        core.Movement.CheckIfShouldFlip(wallJumpDirection);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        owner.Animator.SetFloat(velocityXHash, Mathf.Abs(core.Movement.CurrentVelocity.x));
        owner.Animator.SetFloat(velocityYHash, core.Movement.CurrentVelocity.y);

        if (Time.time >= startTime + owner.StatesConfigSo.wallJumpTime)
        {
            isAbilityDone = true;
        }
    }
}