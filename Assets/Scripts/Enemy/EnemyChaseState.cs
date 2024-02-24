public class EnemyChaseState : EnemyState
{
    public EnemyChaseState(StateMachine<Enemy> stateMachine, Enemy owner) : base(stateMachine, owner)
    {
    }

    public override void Enter()
    {
        base.Enter();
        owner.Animator.SetBool(IsMovingHash, true);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        CheckIfShouldFlip();

        if (!owner.IsPlayerDetected())
        {
            stateMachine.TransitionTo<EnemyGuardState>();
        }
        else if (DistanceToPlayer > owner.stopDistance)
        {
            owner.Core.Movement.SetVelocityX(owner.movementSpeed * owner.Core.Movement.FacingDirection);
        }
        else
        {
            stateMachine.TransitionTo<EnemyGuardState>();
        }
    }

    public override void Exit()
    {
        base.Exit();
        owner.Animator.SetBool(IsMovingHash, false);
    }
}