using System;

/// <summary>
/// 时间管理器_标准时
/// 
/// 白昼:无特殊效果
/// 黑夜:
///    光线昏暗,|视距|-1
///    背袭伤害+10%
/// (有10%概率为红月夜,每月1日黑夜为月黑, 额外触发效果)
/// 红月夜:|懦狼|进入|狂暴|状态(10),|迅捷|状态(10)
/// 月黑:
///     光线无比昏暗,|视距|额外-1
///     |懦狼|攻击力-25%
/// </summary>
public partial class TM
{
    /// <summary>
    /// 一天对应的标准时数
    /// </summary>
    private static int TimeInDays => 10;

    /// <summary>
    /// 今日几时
    /// </summary>
    public int Time
    {
        get;
        set
        {
            StandardElapsed += value - field;
            // 下一天
            if (value > TimeInDays)
            {
                Days++;
                value -= TimeInDays;
            }
            // 发生了昼夜更替
            if (field != 0 && IsDaytime() != IsDaytime(value))
            {

            }

            field = value;
        }
    }

    public bool IsDaytime() => IsDaytime(Time);
    /// <summary>
    /// 是否为白天
    /// 
    /// 永无天日禁止变白天
    /// </summary>
    private bool IsDaytime(int value) => EternalDay <= 0 && value <= TimeInDays / 2;

    /// <summary>
    /// 强制昼夜更替
    /// 其实就是跳时间
    /// </summary>
    /// <param name="nycthemeron">目标时间</param>
    public void CompelReplacement(Nycthemeron nycthemeron)
    {
        // 目标是白天, 并且当前不是白天
        if(nycthemeron == Nycthemeron.Daytime && !IsDaytime())
        {
            Time += TimeInDays + 1 - Time;
        }
        if(nycthemeron == Nycthemeron.Night && IsDaytime())
        {
            Time += TimeInDays/2 + 1 - Time;
        }
    }

    /// <summary>
    /// 永无天日
    /// 0为正常 正数为永无天日
    /// </summary>
    private int EternalDay 
    { 
        get; 
        set
        {
            field = value;
            Nycthemeron nycthemeron = field > 0 ? Nycthemeron.Night : Nycthemeron.Daytime;
            CompelReplacement(nycthemeron);
        }
    }
}

/// <summary>
/// 昼夜
/// </summary>
public enum Nycthemeron
{
    Daytime = 1,
    Night = 2,
}
