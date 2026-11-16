using System.Collections.Generic;

/// <summary>
/// Unit_状态部分
/// </summary>
public partial class Unit
{
    /// <summary>
    /// 状态
    /// </summary>
    public Dictionary<string, State> States { get; } = new();

    /// <summary>
    /// 配置状态 仅供模板相关使用
    /// </summary>
    private Dictionary<string, int> ParseState(List<string> list)
    {
        Dictionary<string, int> output = [];
        if (list[0] == "无") return output;

        foreach(string item in list)
        {
            string name = item.Split("(")[0];
            // 层数
            int plies = int.Parse(item.Split("(")[1][..^1]);

            output[name] = plies;
        }
        return output;
    }
}