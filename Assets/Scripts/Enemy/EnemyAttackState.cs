using UnityEngine;

public class EnemyAttackState : EnemyState
{
    public bool CanAttack => Time.time >= _lastAttackTime + owner.Data.attackCooldown;
    
    private bool _isAttackFinished;
    private float _lastAttackTime;

    public EnemyAttackState(StateMachine<Enemy> stateMachine, Enemy owner) : base(stateMachine, owner)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        CheckIfShouldFlipWhenChasing();
        
        owner.Animator.SetTrigger(AttackHash);
        _isAttackFinished = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        if (_isAttackFinished)
        {
            stateMachine.TransitionTo<EnemyGuardState>();
        }
    }
    
    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        
        _isAttackFinished = true;
        _lastAttackTime = Time.time;
    }
}