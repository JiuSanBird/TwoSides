using System;

/// <summary>
/// 时间管理器
/// 每经历一个岁月与光阴,标准时+2
/// 在战斗中每过1回合过1个标准时
/// 每流逝1标准时,混沌值+1
/// </summary>
public partial class TM : Singleton<TM>
{
    /// <summary>
    /// 一个年对应季节数
    /// </summary>
    public static int YearInSeason => 4;
    /// <summary>
    /// 一个季节对应月数
    /// </summary>
    public static int MonthInSeason => 1;
    /// <summary>
    /// 一个月对应天数
    /// 这样玩家在一局游戏里才有可能体会到季节更替
    /// </summary>
    private static int DaysInMonth => 10;

    /// <summary>
    /// 当前月份
    /// </summary>
    public int Month
    {
        get;
        // 将月份限制在1~YearInSeason之间
        set => field = (Math.Max(value, 0) - 1) % YearInSeason + 1;
    }

    /// <summary>
    /// 本月已经历的天数
    /// </summary>
    public int Days
    {
        get;
        set
        {
            if(field != 0 && value > DaysInMonth)
            {
                Month++;
                value -= DaysInMonth;
            }

            field = value;
        }
    }

    /// <summary>
    /// 已度过总标准时
    /// </summary>
    public int StandardElapsed { get; set; } = 0;
    /// <summary>
    /// 时间流逝
    /// </summary>
    public event Action TimeLapses;
    public void TimeFlow()
    {
        Time++;
        TimeLapses?.Invoke();
    }

    /// <summary>
    /// 生成一个随机时间并使用
    /// </summary>
    public void RandomTime()
    {
        Month = RM.GetRandom(1, YearInSeason + 1);
        Days = RM.GetRandom(1, DaysInMonth + 1);
        Time = RM.GetRandom(1, TimeInDays + 1);
    }
}
