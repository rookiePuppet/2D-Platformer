public class EnemyGuardState : EnemyState
{
    public EnemyGuardState(StateMachine<Enemy> stateMachine, Enemy owner) : base(stateMachine, owner)
    {
    }

    public override void Enter()
    {
        base.Enter();
        owner.Core.Movement.SetVelocityX(0f);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!owner.IsPlayerDetected()) return;

        if (DistanceToPlayer > owner.stopDistance)
        {
            stateMachine.TransitionTo<EnemyChaseState>();
        }
        else if(stateMachine.GetStateInstance<EnemyAttackState>().CanAttack)
        {
            stateMachine.TransitionTo<EnemyAttackState>();
        }
    }
}