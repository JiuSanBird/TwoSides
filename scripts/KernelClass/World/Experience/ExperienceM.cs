using System.Collections.Generic;

/// <summary>
/// 岁月与光阴管理器
/// </summary>
public class ExperienceM : Singleton<ExperienceM>
{
    /// <summary>
    /// 当前牌库
    /// 左 中 右
    /// </summary>
    public List<Experience>[] Deck = new List<Experience>[3]
    {
        new(),
        new(),
        new()
    };

    /// <summary>
    /// 生成牌库
    /// 
    /// </summary>
    public void GenerateDeck()
    {

    }

}