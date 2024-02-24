public class EnemyPatrolState : EnemyState
{
    public EnemyPatrolState(StateMachine<Enemy> stateMachine, Enemy owner) : base(stateMachine, owner)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //Animator.SetBool(IsMovingHash, true);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void Exit()
    {
        base.Exit();
        //Animator.SetBool(IsMovingHash, false);
    }
}