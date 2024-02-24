public class EnemyStateMachine : StateMachine<Enemy>
{
    public EnemyStateMachine(Enemy owner) : base(owner)
    {
        AddState(new EnemyGuardState(this, owner));
        AddState(new EnemyPatrolState(this, owner));
        AddState(new EnemyChaseState(this, owner));
        AddState(new EnemyAttackState(this, owner));
        
        Initialize<EnemyGuardState>();
    }
}