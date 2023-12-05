using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 状态机基类
/// </summary>
/// <typeparam name="TOwner">状态机宿主类型</typeparam>
public abstract class StateMachine<TOwner> {
    /// <summary>
    /// 宿主对象
    /// </summary>
    protected readonly TOwner owner;

    /// <summary>
    /// 字典保存宿主所有状态
    /// key：状态类名，value：状态实例
    /// </summary>
    private readonly Dictionary<string, StateBase<TOwner>> _statesDic = new();

    /// <summary>
    /// 当前状态
    /// </summary>
    public StateBase<TOwner> CurrentState { get; private set; }

    protected StateMachine(TOwner owner)
    {
        this.owner = owner;
    }

    /// <summary>
    /// 添加状态
    /// </summary>
    /// <param name="state"></param>
    protected void AddState(StateBase<TOwner> state)
    {
        var stateName = state.GetType().Name;
        Debug.Log($"Add State: state name is {stateName}");
        _statesDic.TryAdd(stateName, state);
    }

    /// <summary>
    /// 初始化状态机
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    protected void Initialize<TState>()
    {
        TransitionTo<TState>();

        Debug.Log("---STATES LIST---");
        foreach (var state in _statesDic)
        {
            Debug.Log($"{state.Key}");
        }
        Debug.Log($"INITIAL STATE IS {typeof(TState).Name}");
        Debug.Log("------");
    }

    // ReSharper disable Unity.PerformanceAnalysis
    /// <summary>
    /// 状态转移
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    public void TransitionTo<TState>()
    {
        if (!_statesDic.TryGetValue(typeof(TState).Name, out var targetState))
        {
            Debug.LogError("State not found: " + typeof(TState).Name);
            return;
        }

        CurrentState?.Exit();
        CurrentState = targetState;
        CurrentState.Enter();
    }

    public TState GetStateInstance<TState>() where TState: StateBase<TOwner>
        => _statesDic[typeof(TState).Name] as TState;

    /*/// <summary>
    /// 创建所有状态的实例，并存入字典
    /// 必须先重写GetStateClassList方法
    /// </summary>
    protected void CreateInstanceForAllStates()
    {
        foreach (var stateClassType in GetStateClassList())
        {
            var state = Activator.CreateInstance(stateClassType, this, owner) as StateBase<TOwner>;
            _statesDic.Add(stateClassType.Name, state);
        }
    }*/

    /*/// <summary>
    /// 获取状态类列表，用于实例化所有状态类
    /// </summary>
    /// <returns></returns>
    protected virtual IEnumerable<Type> GetStateClassList() => new List<Type>();*/
}