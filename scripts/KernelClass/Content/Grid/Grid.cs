using System;

/// <summary>
/// 格
/// </summary>
public class Grid
{
    public Grid(GridCfg cfg) => Cfg = cfg;

    private GridCfg Cfg { get; init; }

    public static Grid GetByCfg(GridCfg cfg)
    {
        if (cfg == null || cfg.ClassName == "无") return null;

        Type type = typeof(Grid).Assembly.GetType(cfg.ClassName);
        return (Grid)Activator.CreateInstance(type, cfg);
    }
}
