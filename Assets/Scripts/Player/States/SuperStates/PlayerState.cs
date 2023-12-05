public class PlayerState : StateBase<PlayerController> {
    private readonly int _animatorParamHash;
    protected bool isAnimationFinished;
    protected PlayerState(PlayerStateMachine stateMachine, PlayerController owner, int animatorParamHash) : base(stateMachine, owner)
    {
        _animatorParamHash = animatorParamHash;
    }

    public override void Enter()
    {
        isAnimationFinished = false;
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