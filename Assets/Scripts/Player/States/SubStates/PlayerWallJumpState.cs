using UnityEngine;

public class PlayerWallJumpState : PlayerAbilityState
{
    private readonly int _velocityXHash = Animator.StringToHash("VelocityX");
    private readonly int _velocityYHash = Animator.StringToHash("VelocityY");
    
    public PlayerWallJumpState(PlayerStateMachine stateMachine, PlayerController owner, int animatorParamHash) : base(
        stateMachine, owner, animatorParamHash)
    {
    }

    public override void Enter()
    {
        base.Enter();
        var jumpState = stateMachine.GetStateInstance<PlayerJumpState>();
        jumpState.ResetJumpCounter();
        var wallJumpDirection = -owner.FacingDirection;
        owner.SetVelocity(owner.Data.wallJumpVelocity, owner.Data.wallJumpAngle, wallJumpDirection);
        owner.CheckIfShouldFlip(wallJumpDirection);
        jumpState.IncreaseJumpCounter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        owner.Animator.SetFloat(_velocityXHash, Mathf.Abs(owner.CurrentVelocity.x));
        owner.Animator.SetFloat(_velocityYHash, owner.CurrentVelocity.y);

        if (Time.time >= startTime + owner.Data.wallJumpTime)
        {
            isAbilityDone = true;
        }
    }
}