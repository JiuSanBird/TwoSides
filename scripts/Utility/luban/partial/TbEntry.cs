using System.Collections.Generic;
using System.Linq;

public partial class TbEntry
{
    /// <summary>
    /// 字典<名称, 配置>
    /// </summary>
    private static Dictionary<string, EntryCfg> Dic 
    {
        get
        {
            field ??= NameIndex.Build(Loader.Tables.TbEntry._dataList, c => c.Name);
            return field;
        }
        set;
    }
    /// <summary>
    /// 修正<名称, 稀有度修正>
    /// </summary>
    public static Dictionary<string, int> corr { get; private set; } = [];

    /// <summary>
    /// 根据稀有度获取一个
    /// </summary>
    /// <param name="d"><名称, 稀有度> 库, 需要筛选时, 自备素材库</param>
    /// <returns></returns>
    public static Entry GetOne(Dictionary<string, int> d)
    {
        d ??= Dic.ToDictionary(o => o.Key, o => o.Value.Rarity);
        return GetByName(RM.GetOne(IRarity.Group(d, corr)));
    }

    public static List<Entry> GetByName(List<string> name)
        => name.Select(GetByName).ToList();

    public static Entry GetByName(string name)
        => Entry.GetByCfg(NameIndex.Find(Dic, name));
}
