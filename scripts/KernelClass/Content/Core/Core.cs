using System;
using System.Collections.Generic;

/// <summary>
/// 核心
/// </summary>
public class Core : Trigger, IRarity, IHaveLabel, IHaveName, IDescribe
{
    public Core(CoreCfg cfg)
    {
        Cfg = cfg;
    }

    private CoreCfg Cfg { get; init; }
    public int Rarity => Cfg.Rarity;
    public string Name => Cfg.Name;
    public List<int> Par => Cfg.Par;
    public string PrimaryDescribe => Cfg.Describe;

    public static Core GetByCfg(CoreCfg cfg)
    {
        if (cfg == null || cfg.ClassName == "无") return null;

        Type type = typeof(Core).Assembly.GetType(cfg.ClassName);
        return (Core)Activator.CreateInstance(type, cfg);
    }
}