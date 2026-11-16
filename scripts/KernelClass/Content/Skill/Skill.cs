using System;
using System.Collections.Generic;

public class Skill : IHaveName, IHaveLabel, IHaveIcon, IDescribe
{
    public Skill(SkillCfg cfg)
    {
        Cfg = cfg;
    }
    private SkillCfg Cfg { get; init; }
    public string Name { get; set; }
    public List<int> Par => Cfg.Par;
    public string PrimaryDescribe => Cfg.Describe;

    public static Skill GetByCfg(SkillCfg cfg)
    {
        if (cfg == null || cfg.ClassName == "无") return null;

        Type type = typeof(Skill).Assembly.GetType(cfg.ClassName);
        return (Skill)Activator.CreateInstance(type, cfg);
    }
}