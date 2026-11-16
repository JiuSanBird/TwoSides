using System;

/// <summary>
/// 道具
/// </summary>
public class Prop
{
    public Prop(PropCfg cfg) => Cfg = cfg;

    private PropCfg Cfg { get; init; }

    public static Prop GetByCfg(PropCfg cfg)
    {
        if (cfg == null || cfg.ClassName == "无") return null;

        Type type = typeof(Prop).Assembly.GetType(cfg.ClassName);
        return (Prop)Activator.CreateInstance(type, cfg);
    }
}
