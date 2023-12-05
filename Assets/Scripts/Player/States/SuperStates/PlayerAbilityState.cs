public class PlayerAbilityState : PlayerState {
    protected bool isAbilityDone;

    protected PlayerAbilityState(PlayerStateMachine stateMachine, PlayerController owner, int animatorParamHash) : base(stateMachine, owner, animatorParamHash)
    {
    }

    public override void Enter()
    {
        base.Enter();
        isAbilityDone = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!isAbilityDone) return;

        if (owner.IsGrounded && owner.CurrentVelocity.y < 0.01f)
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