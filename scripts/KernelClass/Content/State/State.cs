using Godot;
using System;
using System.Collections.Generic;

/// <summary>
/// 状态
/// </summary>
public class State : Trigger, IHaveName, IDescribe
{
    public State(StateCfg cfg)
    {
        Cfg = cfg;
    }

    public StateCfg Cfg { get; init; }

    public string Name => Cfg.Name;
    public List<int> Par => Cfg.Par;
    public string PrimaryDescribe => Cfg.Describe;

    /// <summary>
    /// 当前层数
    /// </summary>
    public int Plies
    {
        get => Mathf.Clamp(field, 0, Cfg.MaxPlies);
        private set => field = Math.Max(value, 0);
    }

    /// <summary>
    /// 增加层数
    /// </summary>
    public void Increase(int value)
    {

    }

    /// <summary>
    /// 减少层数
    /// </summary>
    public void Reduce(int value)
    {

    }
}