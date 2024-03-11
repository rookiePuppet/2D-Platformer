using UnityEngine;

public class EnemyGuardState : EnemyState
{
    private float CurrentPositionX => owner.transform.position.x; // 当前位置的x坐标
    private bool IsOnOriginPosition => Mathf.Abs(CurrentPositionX - originPositionX) < 1f; // 是否在初始位置

    private bool _isWaiting; // 是否在等待，当玩家离开视野时，等待一段时间再回到初始位置
    private float _waitingStartTime;

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

        if (owner.IsPlayerDetected())
        {
            if (owner.IsEdgeDetected) return;
            _isWaiting = false;

            // 玩家不在攻击范围内时，转换到追逐状态
            if (DistanceToPlayer > owner.Data.stopDistance)
            {
                stateMachine.TransitionTo<EnemyChaseState>();
            }
            // 玩家处于攻击范围，可以攻击
            else if (stateMachine.GetStateInstance<EnemyAttackState>().CanAttack)
            {
                stateMachine.TransitionTo<EnemyAttackState>();
            }
        }
        else if (_isWaiting)
        {
            if (IsOnOriginPosition)
            {
                _isWaiting = false;
                owner.Animator.SetBool(IsMovingHash, false);
            }
            // 等待结束
            else if (Time.time - _waitingStartTime > owner.Data.targetLostWaitingTime)
            {
                var direction = CurrentPositionX - originPositionX > 0 ? -1 : 1;
                owner.Core.Movement.CheckIfShouldFlip(direction);
                owner.Core.Movement.SetVelocityX(owner.Data.movementSpeed * direction);
                owner.Animator.SetBool(IsMovingHash, true);
            }
        }
        // 玩家离开感知范围，等待一段时间后回到起始位置
        else if (!IsOnOriginPosition)
        {
            _isWaiting = true;
            _waitingStartTime = Time.time;
        }
        else
        {
            _isWaiting = false;
            owner.Core.Movement.SetVelocityX(0f);
            if (owner.Data.isPatrol)
            {
                stateMachine.TransitionTo<EnemyPatrolState>();
            }
        }
    }
}