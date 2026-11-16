using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// 概率管理器
/// RM : Random Manager
/// </summary>
public class RM
{
    // RandomNumberGenerator生成的随机数与Random生成的都差不多随机,不过是真随机,效率低
    private static readonly System.Random random = new();
    /// <summary>
    /// 获取随机数
    /// 取值范围[0,max)
    /// </summary>
    public static int GetRandom(int max) => random.Next(0, max);
    public static double GetRandom(double max) => random.NextDouble() * max;
    public static int GetRandom(int min, int max) => random.Next(min, max);
    public static double GetRandom(double min, double max)
        => random.NextDouble() * (max - min) + min;

    /// <summary>
    /// 封装正态分布
    /// 将一个数进行基础正态分布处理
    /// PND : PackagingNormalDistribution
    /// </summary>
    public static int PND(int value, double variance) => (int)Math.Round(NormalDistribution(value, variance));

    /// <summary>
    /// 正态分布
    /// 与此数据相乘时必须调用Math.Round()进行四舍五入
    /// </summary>
    /// <param name="mathematicalExpectation">数学期望</param>
    /// <param name="variance">方差</param>
    private static double NormalDistribution(double mathematicalExpectation = 1, double variance = 0.4)
    {
        if (variance <= 0)
        {
            return mathematicalExpectation;
        }
        //生成两个0~1的随机数
        ValueTuple<double, double> random = (new System.Random(GetRandom(100)).NextDouble(),
                                             new System.Random(GetRandom(100)).NextDouble());

        //概率密度函数
        //f(x) = √￣(-2 * ln(random.Item1)) * Sin(2π * random.Item2)
        double probabilityDensityFunction = Math.Sqrt(-2 * Math.Log(random.Item1)) *
                                            Math.Sin(2 * Math.PI * random.Item2);
        return mathematicalExpectation + variance * probabilityDensityFunction;
    }

    /// <summary>
    /// 判决
    /// </summary>
    /// <returns></returns>
    public static bool Jud(int Probability) => GetRandom(100) < Probability;

    /// <summary>
    /// 获取任意一个
    /// 概率
    /// </summary>
    public static T GetOne<T>(List<T> list, int limit = 100) where T : IPercentage
    {
        return list[GetOne(list.Select(item => item.Probability).ToList(), limit)];
    }
    public static T GetAaaryOne<T>(T[] array) => array[GetRandom(array.Length)];
    public static T GetListOne<T>(List<T> list) => list?.Count > 0 ? list[GetRandom(list.Count)] : default;

    /// <summary>
    /// 获取任意一个
    /// </summary>
    /// <param name="limit">概率和</param>
    /// <returns>索引</returns>
    public static int GetOne(List<int> list, int limit = 100)
    {
        int value = GetRandom(limit);
        for (int i = 0; i < list.Count; ++i)
        {
            value -= list[i];
            if (value < 0)
            {
                return i;
            }
        }
        throw new InvalidOperationException("GetOne<T>(List<T> list)出现错误");
    }

    /// <summary>
    /// 从稀有度可选项中选一个
    /// </summary>
    public static string GetOne(List<string>[] group)
    {
        // 形成list, 如果为空, 则概率为0
        List<int> list = group
            .Select((item, i) => item.Count == 0 ? 0 : IRarity.Corresponding[i])
            .ToList();

        // 获取随机稀有度；权重和不一定等于 100，必须把真实总和传进去
        int total = list.Sum();
        if (total <= 0)
        {
            return null;
        }

        int rarity = GetOne(list, total);
        // 在稀有度中选一个
        return GetListOne(group[rarity]);
    }
}