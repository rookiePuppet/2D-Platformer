using UnityEngine;

public class PlayerState : StateBase<PlayerController>
{
    protected Core core;
    protected int InputX => owner.InputHandler.NormalizedMoveInput.x;
    protected int InputY => owner.InputHandler.NormalizedMoveInput.y;
    protected bool isAnimationFinished;
    protected bool isExiting;

    private readonly int _animatorParamHash;

    protected static readonly int velocityXHash = Animator.StringToHash("VelocityX");
    protected static readonly int velocityYHash = Animator.StringToHash("VelocityY");
    protected static readonly int climbLedgeHash = Animator.StringToHash("ClimbLedge");

    protected PlayerState(PlayerStateMachine stateMachine, PlayerController owner, int animatorParamHash) : base(stateMachine, owner)
    {
        core = owner.Core;
        _animatorParamHash = animatorParamHash;
    }

    public override void Enter()
    {
        isAnimationFinished = false;
        isExiting = false;
        owner.Animator.SetBool(_animatorParamHash, true);
    }

    public override void LogicUpdate()
    {
    }

    public override void FixedUpdate()
    {
    }

    public override void Exit()
    {
        isExiting = true;
        owner.Animator.SetBool(_animatorParamHash, false);
    }

    // 动画开始触发
    public override void AnimationTrigger()
    {
        isAnimationFinished = false;
    }

    // 动画结束触发
    public override void AnimationFinishTrigger()
    {
        isAnimationFinished = true;
    }
}