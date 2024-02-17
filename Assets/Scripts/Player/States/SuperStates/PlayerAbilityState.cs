using UnityEngine;

public class PlayerAbilityState : PlayerState
{
    protected float startTime;
    protected bool isAbilityDone;

    protected PlayerAbilityState(PlayerStateMachine stateMachine, PlayerController owner, int animatorParamHash) : base(stateMachine, owner, animatorParamHash)
    {
    }

    public override void Enter()
    {
        base.Enter();
        isAbilityDone = false;
        startTime = Time.time;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!isAbilityDone) return;

        if (core.CollisionSenses.IsGrounded && core.Movement.CurrentVelocity.y < 0.01f)
        {
            stateMachine.TransitionTo<PlayerIdleState>();
        }
        else
        {
            stateMachine.GetStateInstance<PlayerAerialState>().SetIsJumping();
            stateMachine.TransitionTo<PlayerAerialState>();
        }
    }
}