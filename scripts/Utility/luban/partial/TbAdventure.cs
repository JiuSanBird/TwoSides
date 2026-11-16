using System.Collections.Generic;
using System.Linq;

public partial class TbAdventure
{
    private static Dictionary<string, AdventureCfg> Dic
    {
        get
        {
            field ??= NameIndex.Build(Loader.Tables.TbAdventure._dataList, c => c.Name);
            return field;
        }
        set;
    }

    public static Dictionary<string, int> corr { get; set; } = [];

    public static Adventure GetOne(Dictionary<string, int> d)
    {
        d ??= Dic.ToDictionary(o => o.Key, o => o.Value.Rarity);
        return GetByName(RM.GetOne(IRarity.Group(d, corr)));
    }

    public static Adventure GetByName(string name)
        => Adventure.GetByCfg(NameIndex.Find(Dic, name));
}
