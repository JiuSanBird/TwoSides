using System;
using System.Collections.Generic;

/// <summary>
/// 稀有度
/// 修改稀有度去修改数据库的稀有度
/// </summary>
public interface IRarity
{
    /// <summary>
    /// 稀有度
    /// 这里只提供基础稀有度,具体稀有度需要时计算,就像伤害一样
    /// </summary>
    public int Rarity { get; }

    /// <summary>
    /// 稀有度对应概率
    /// Corresponding : 相应的
    /// </summary>
    public static Dictionary<int, int> Corresponding { get; private set; } = new Dictionary<int, int>()
    {
        { 0, 19},{ 1, 17}, { 2, 15},
        { 3, 13},{ 4, 11}, { 5, 9},
        { 6, 7}, { 7, 5}, { 8, 3},
        { 9, 1},{ 10, 0},
    };

    /// <summary>
    /// 算上修正,算出真正的分组
    /// </summary>
    public static List<string>[] Group(Dictionary<string, int> d, Dictionary<string, int> corr)
    {
        // 根据稀有度分组
        List<string>[] list = Array.ConvertAll(new int[11], _ => new List<string>());

        foreach (KeyValuePair<string, int> item in d)
        {
            int delta = corr != null ? corr.GetValueOrDefault(item.Key) : 0;
            list[Math.Clamp(item.Value + delta, 0, 10)].Add(item.Key);
        }
        return list;
    }
}
