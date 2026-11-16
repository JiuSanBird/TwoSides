using System;
using System.Collections.Generic;

/// <summary>
/// 混沌崩坏
/// </summary>
public abstract class Collapse : IHaveName, IHaveLabel, IDescribe
{
    public Collapse(CollapseCfg cfg)
    {
        Cfg = cfg;
    }

    private CollapseCfg Cfg { get; set; }
    public string Name => Cfg.Name;
    public List<int> Par => Cfg.Par;
    public string PrimaryDescribe => Cfg.Describe;

    public static Collapse GetByCfg(CollapseCfg cfg)
    {
        if (cfg == null || cfg.ClassName == "无") return null;

        Type type = typeof(Collapse).Assembly.GetType(cfg.ClassName);
        return (Collapse)Activator.CreateInstance(type, cfg);
    }
}
