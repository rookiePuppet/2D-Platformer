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

        CheckIfShouldFlipWhenChasing();

        if (!owner.IsPlayerDetected())
        {
            if (owner.Data.isPatrol)
            {
                stateMachine.TransitionTo<EnemyPatrolState>();
            }
            else
            {
                stateMachine.TransitionTo<EnemyGuardState>();
            }
        }
        // 追击玩家直到进入攻击范围
        else if (DistanceToPlayer > owner.Data.stopDistance && !owner.IsEdgeDetected)
        {
            owner.Core.Movement.SetVelocityX(owner.Data.movementSpeed * owner.Data.chaseSpeedMultiplier *
                                             owner.Core.Movement.FacingDirection);
        }
        // 进入攻击范围，等待攻击
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