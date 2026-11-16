using System.Collections.Generic;
using System.Linq;

public partial class TbSkill
{
    private static Dictionary<string, SkillCfg> Dic
    {
        get
        {
            field ??= NameIndex.Build(Loader.Tables.TbSkill._dataList, c => c.Name);
            return field;
        }
        set;
    }

    public static Dictionary<string, int> corr { get; set; } = [];

    public static Skill GetOne(Dictionary<string, int> d)
    {
        d ??= Dic.ToDictionary(o => o.Key, o => o.Value.Rarity);
        return GetByName(RM.GetOne(IRarity.Group(d, corr)));
    }

    public static List<Skill> GetByName(List<string> name)
        => name.Select(GetByName).ToList();

    public static Skill GetByName(string name)
        => Skill.GetByCfg(NameIndex.Find(Dic, name));
}
