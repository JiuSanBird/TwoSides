using System.Collections.Generic;
using System.Linq;

public partial class TbProp
{
    private static Dictionary<string, PropCfg> Dic
    {
        get
        {
            field ??= NameIndex.Build(Loader.Tables.TbProp._dataList, c => c.Name);
            return field;
        }
        set;
    }

    public static Dictionary<string, int> corr { get; set; } = [];

    public static Prop GetOne(Dictionary<string, int> d)
    {
        d ??= Dic.ToDictionary(o => o.Key, o => o.Value.Rarity);
        return GetByName(RM.GetOne(IRarity.Group(d, corr)));
    }

    public static Prop GetByName(string name)
        => Prop.GetByCfg(NameIndex.Find(Dic, name));
}
