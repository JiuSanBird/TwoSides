using System;

/// <summary>
/// Unit_血条部分
/// </summary>
public partial class Unit
{
    /// <summary>
    /// 当前生命值
    /// </summary>
    public int Value { get; private set; }
    public void SetHP(int value, Unit unit)
    {
        // 生命值不得高于最大生命值
        Value = Math.Min(value, HPMax.FinalValue);
        SetPercent(unit);
    }

    /// <summary>
    /// 生命值上限
    /// </summary>
    public WValue HPMax { get; private set; } = new(100, 1, 9999);

    /// <summary>
    /// 生命值百分比
    /// </summary>
    public float Percent { get; private set; }
    private void SetPercent(Unit unit)
    {
        Percent = 100 * Value / HPMax.FinalValue;
        monitorData?.Invoke(unit);
    }

    /// <summary>
    /// 血线判定
    /// </summary>
    private Action<Unit> monitorData;
}