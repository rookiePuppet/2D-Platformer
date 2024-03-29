﻿using UnityEngine;

public class EnemyState : StateBase<Enemy>
{
    protected readonly float originPositionX; // 初始位置的x坐标
    protected float DistanceToPlayer => Mathf.Abs(owner.transform.position.x - owner.Target.transform.position.x);
    
    protected static readonly int IsMovingHash = Animator.StringToHash("IsMoving");
    protected static readonly int AttackHash = Animator.StringToHash("Attack");
    protected static readonly int IsDizzyHash = Animator.StringToHash("IsDizzy");
    
    protected EnemyState(StateMachine<Enemy> stateMachine, Enemy owner) : base(stateMachine, owner)
    {
        originPositionX = owner.transform.position.x;
    }
    
    protected void CheckIfShouldFlipWhenChasing()
    {
        owner.Core.Movement.CheckIfShouldFlip(owner.Target.transform.position.x - owner.transform.position.x > 0
            ? 1
            : -1);
    }

    public override void Enter()
    {
    }

    public override void LogicUpdate()
    {
    }

    public override void FixedUpdate()
    {
    }

    public override void Exit()
    {
    }

    public override void AnimationTrigger()
    {
    }

    public override void AnimationFinishTrigger()
    {
    }
}