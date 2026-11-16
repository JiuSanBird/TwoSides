using System.Collections.Generic;

/// <summary>
/// TbState 的按名字索引
/// 用到的地方:TemplateCfg.state, 值是 "禁锢(20)" 这种"名字 + 层数"
/// </summary>
public partial class TbState
{
    private static Dictionary<string, StateCfg> _byName;

    public static StateCfg GetByName(string name)
    {
        _byName ??= NameIndex.Build(Loader.Tables.TbState._dataList, c => c.Name);
        return NameIndex.Find(_byName, name);
    }
}
