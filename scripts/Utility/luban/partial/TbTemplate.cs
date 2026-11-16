using System.Collections.Generic;
using System.Linq;

public partial class TbTemplate
{
    private static Dictionary<string, TemplateCfg> Dic
    {
        get
        {
            field ??= NameIndex.Build(Loader.Tables.TbTemplate._dataList, c => c.Name);
            return field;
        }
        set;
    }

    public static Dictionary<string, int> corr { get; set; } = [];

    public static TemplateCfg GetOne(Dictionary<string, int> d)
    {
        d ??= Dic.ToDictionary(o => o.Key, o => o.Value.Rarity);
        return GetByName(RM.GetOne(IRarity.Group(d, corr)));
    }

    public static TemplateCfg GetByName(string name)
        => NameIndex.Find(Dic, name);
}
