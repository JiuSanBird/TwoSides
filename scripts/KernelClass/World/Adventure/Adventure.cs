using System;

/// <summary>
/// 奇遇
/// </summary>
public class Adventure
{
    public Adventure(AdventureCfg cfg) => Cfg = cfg;

    private AdventureCfg Cfg { get; init; }

    public static Adventure GetByCfg(AdventureCfg cfg)
    {
        if (cfg == null || cfg.ClassName == "无") return null;

        Type type = typeof(Adventure).Assembly.GetType(cfg.ClassName);
        return (Adventure)Activator.CreateInstance(type, cfg);
    }
}
