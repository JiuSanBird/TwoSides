using Godot;
using System;

/// <summary>
/// 封装值
/// W : Wrapper
/// 将值限制在范围内,以及设置初始值
/// </summary>
public class WValue(int min, int max)
{
    public WValue(int basics) : this(basics, 0, 100) { }
    public WValue(int basics, int min, int max) : this(min, max) => SetBasics(basics);

    private Action<WValue> dataCompute;
    /// <summary>
    /// 数据计算
    /// 用于求出最终值的涉及因素
    /// </summary>
    public event Action<WValue> DataCompute
    {
        add
        {
            dataCompute -= value;
            dataCompute += value;
            CalculateFinalValue();
        }
        remove
        {
            dataCompute -= value;
            CalculateFinalValue();
        }
    }

    private Action<WValue> dataChange;
    /// <summary>
    /// 数据变化监听
    /// </summary>
    public event Action<WValue> DataChange
    {
        add
        {
            dataChange -= value;
            dataChange += value;
        }
        remove => dataChange -= value;
    }

    private int Basics;
    public void SetBasics(int value)
    {
        Basics = value;
        CalculateFinalValue();
    }
    /// <summary>
    /// 加值(可以为负)
    /// </summary>
    public int Addition { get; set; }
    /// <summary>
    /// 百分比增值
    /// </summary>
    public int PA { get; set; }
    /// <summary>
    /// 百分比减值
    /// </summary>
    public int PS { get; set; }

    /// <summary>
    /// 最终值
    /// </summary>
    public int FinalValue { get; private set; }

    private int Min { get; set; } = min;
    private int Max { get; set; } = max;

    /// <summary>
    /// 计算最终值
    /// </summary>
    private void CalculateFinalValue()
    {
        // 每次求最终值时直接重新计算
        Addition = PA = PS = 0;

        dataCompute?.Invoke(this);

        // 最终值 = (基础值 + 基础加成) * (100 + 百分比增)% * (100 - 百分比减)%
        FinalValue = Mathf.Clamp(
            (Basics + Addition) * (100 + PA) * (100 - PS) / 10000,
            Min, Max);

        dataChange?.Invoke(this);
    }
}