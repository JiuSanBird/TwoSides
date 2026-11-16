using System.Collections.Generic;
using System.Linq;

public partial class TbCollapse
{
    private static Dictionary<string, CollapseCfg> Dic
    {
        get
        {
            field ??= NameIndex.Build(Loader.Tables.TbCollapse._dataList, c => c.Name);
            return field;
        }
        set;
    }

    public static Dictionary<string, int> corr { get; set; } = [];

    public static Collapse GetOne(Dictionary<string, int> d)
    {
        d ??= Dic.ToDictionary(o => o.Key, o => o.Value.Rarity);
        return GetByName(RM.GetOne(IRarity.Group(d, corr)));
    }

    public static Collapse GetByName(string name)
        => Collapse.GetByCfg(NameIndex.Find(Dic, name));
}
