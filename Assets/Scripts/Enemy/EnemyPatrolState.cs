using UnityEngine;

public class EnemyPatrolState : EnemyState
{
    private float CurrentPositionX => owner.transform.position.x; // 当前位置的x坐标
    private int FacingDirection => owner.Core.Movement.FacingDirection; // 当前朝向
    
    public EnemyPatrolState(StateMachine<Enemy> stateMachine, Enemy owner) : base(stateMachine, owner)
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

        if (owner.IsPlayerDetected())
        {
            stateMachine.TransitionTo<EnemyChaseState>();
        }

        if (FacingDirection == 1 && CurrentPositionX > originPositionX + owner.Data.patrolRadius)
        {
            owner.Core.Movement.CheckIfShouldFlip(-1);
        }
        else if(FacingDirection == -1 && CurrentPositionX < originPositionX - owner.Data.patrolRadius)
        {
            owner.Core.Movement.CheckIfShouldFlip(1);
        }
        else
        {
            owner.Core.Movement.SetVelocityX(owner.Data.movementSpeed * FacingDirection);
        }
    }

    public override void Exit()
    {
        base.Exit();
        owner.Animator.SetBool(IsMovingHash, false);
    }
}