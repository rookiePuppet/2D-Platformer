/// <summary>
/// 状态基类
/// </summary>
/// <typeparam name="TOwner">状态宿主类型</typeparam>
public abstract class StateBase<TOwner> {
    /// <summary>
    /// 状态机
    /// </summary>
    protected readonly StateMachine<TOwner> stateMachine;
    /// <summary>
    /// 宿主对象
    /// </summary>
    protected readonly TOwner owner;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="stateMachine">状态机对象</param>
    /// <param name="owner">宿主对象</param>
    protected StateBase(StateMachine<TOwner> stateMachine, TOwner owner)
    {
        this.stateMachine = stateMachine;
        this.owner = owner;
    }

    /// <summary>
    /// 进入状态
    /// </summary>
    public abstract void Enter();

    /// <summary>
    /// 逻辑/帧更新
    /// </summary>
    public abstract void LogicUpdate();

    /// <summary>
    /// 物理更新
    /// </summary>
    public abstract void FixedUpdate();

    /// <summary>
    /// 退出状态
    /// </summary>
    public abstract void Exit();

    public abstract void AnimationTrigger();
    public abstract void AnimationFinishTrigger();
}