using System.Collections.Generic;
using System.Linq;

public partial class TbGrid
{
    private static Dictionary<string, GridCfg> Dic
    {
        get
        {
            field ??= NameIndex.Build(Loader.Tables.TbGrid._dataList, c => c.Name);
            return field;
        }
        set;
    }

    public static Dictionary<string, int> corr { get; set; } = [];

    public static Grid GetOne(Dictionary<string, int> d)
    {
        d ??= Dic.ToDictionary(o => o.Key, o => o.Value.Rarity);
        return GetByName(RM.GetOne(IRarity.Group(d, corr)));
    }

    public static Grid GetByName(string name)
        => Grid.GetByCfg(NameIndex.Find(Dic, name));
}
