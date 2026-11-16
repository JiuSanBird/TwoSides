using System;
using System.Collections.Generic;

/// <summary>
/// 种族
/// </summary>
public class Race : IHaveName
{
    public Race(RaceCfg cfg)
    {
        Cfg = cfg;

        Entry = TbEntry.GetByName(cfg.Entry);
        Skill = TbSkill.GetByName(cfg.Skill);
    }

    public RaceCfg Cfg { get; private set; }
    public string Name => Cfg.Name;

    public List<Entry> Entry { get; private set; } = [];
    public List<Skill> Skill { get; private set; } = [];

    public static Race GetByCfg(RaceCfg cfg)
    {
        if (cfg == null || cfg.ClassName == "无") return null;

        Type type = typeof(Race).Assembly.GetType(cfg.ClassName);
        return (Race)Activator.CreateInstance(type, cfg);
    }
}