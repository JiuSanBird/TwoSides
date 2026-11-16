using System.Collections.Generic;
using System.Linq;

public partial class TbRace
{
    private static Dictionary<string, RaceCfg> Dic
    {
        get
        {
            field ??= NameIndex.Build(Loader.Tables.TbRace._dataList, c => c.Name);
            return field;
        }
        set;
    }

    public static Dictionary<string, int> corr { get; set; } = [];

    public static Race GetOne(Dictionary<string, int> d)
    {
        d ??= Dic.ToDictionary(o => o.Key, o => o.Value.Rarity);
        return GetByName(RM.GetOne(IRarity.Group(d, corr)));
    }

    public static Race GetByName(string name)
        => Race.GetByCfg(NameIndex.Find(Dic, name));
}
