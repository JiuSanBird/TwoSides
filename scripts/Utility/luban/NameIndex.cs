using System;
using System.Collections.Generic;

/// <summary>
/// 按名字建索引
/// </summary>
public static class NameIndex
{
    /// <summary>
    /// 从表数据建名字索引
    /// </summary>
    public static Dictionary<string, T> Build<T>(List<T> list, Func<T, string> nameOf)
    {
        Dictionary<string, T> map = new ();
        foreach (T item in list)
        {
            string key = nameOf(item);
            if (string.IsNullOrWhiteSpace(key)) continue;
            map[key] = item;
        }
        return map;
    }

    /// <summary>
    /// 按名字查
    /// </summary>
    public static T Find<T>(Dictionary<string, T> map, string key) where T : class
    {
        if (string.IsNullOrWhiteSpace(key) || key == "无") return null;
        return map[key];
    }
}
