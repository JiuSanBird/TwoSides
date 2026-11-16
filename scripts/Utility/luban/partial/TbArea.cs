using System.Collections.Generic;
using System.Linq;

public partial class TbArea
{
    private static Dictionary<string, AreaCfg> Dic
    {
        get
        {
            field ??= NameIndex.Build(Loader.Tables.TbArea._dataList, c => c.Name);
            return field;
        }
        set;
    }

    public static Dictionary<string, int> Corr { get; set; } = [];

    /// <summary>
    /// 获取一个Area
    /// </summary>
    /// <param name="area">不能是这个</param>
    /// <returns></returns>
    public static Area GetOne(Dictionary<string, int> d = null, Area area = null)
    {
        d ??= Dic.ToDictionary(o => o.Key, o => o.Value.Rarity);
        if(area != null) d.Remove(area.Cfg.Name);
        return GetByName(RM.GetOne(IRarity.Group(d, Corr)));
    }

    public static Area GetByName(string name)
        => Area.GetByCfg(NameIndex.Find(Dic, name));
}
