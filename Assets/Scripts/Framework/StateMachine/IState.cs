/// <summary>
/// 状态接口
/// </summary>
public interface IState {
    /// <summary>
    /// 进入状态
    /// </summary>
    public void Enter();

    /// <summary>
    /// 逻辑/帧更新
    /// </summary>
    public void LogicUpdate();

    /// <summary>
    /// 物理更新
    /// </summary>
    public void FixedUpdate();

    /// <summary>
    /// 退出状态
    /// </summary>
    public void Exit();
}