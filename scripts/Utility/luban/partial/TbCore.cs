using System.Collections.Generic;
using System.Linq;

public partial class TbCore
{
    private static Dictionary<string, CoreCfg> Dic
    {
        get
        {
            field ??= NameIndex.Build(Loader.Tables.TbCore._dataList, c => c.Name);
            return field;
        }
        set;
    }

    public static Dictionary<string, int> corr { get; set; } = [];

    public static Core GetOne(Dictionary<string, int> d)
    {
        d ??= Dic.ToDictionary(o => o.Key, o => o.Value.Rarity);
        return GetByName(RM.GetOne(IRarity.Group(d, corr)));
    }

    public static Core GetByName(string name)
        => Core.GetByCfg(NameIndex.Find(Dic, name));
}
