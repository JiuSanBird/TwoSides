using System;
using System.Collections.Generic;

/// <summary>
/// 地区
/// </summary>
public abstract class Area : Trigger,IRarity, IHaveName
{
    public Area(AreaCfg cfg)
    {
        Cfg = cfg;
        Parse(Cfg.Rrc, Rrc);
        Parse(Cfg.Trc, Trc);
        Parse(Cfg.Arc, Arc);
    }

    public AreaCfg Cfg { get; init; }
    public int Rarity => Cfg.Rarity;
    public string Name => Cfg.Name;
    public Dictionary<string, int> Rrc { get; init; } = [];
    public Dictionary<string, int> Trc { get; init; } = [];
    public Dictionary<string, int> Arc { get; init; } = [];

    public static Area GetByCfg(AreaCfg cfg)
    {
        if (cfg == null || cfg.ClassName == "无") return null;

        Type type = typeof(Area).Assembly.GetType(cfg.ClassName);
        return (Area)Activator.CreateInstance(type, cfg);
    }

    public override void Effectuate(Unit owner = null)
    {
        // 稀有度修正
        foreach (var (k, v) in Rrc) TbRace.corr[k] = TbRace.corr.GetValueOrDefault(k) + v;
        foreach (var (k, v) in Trc) TbTemplate.corr[k] = TbTemplate.corr.GetValueOrDefault(k) + v;
        foreach (var (k, v) in Arc) TbAdventure.corr[k] = TbAdventure.corr.GetValueOrDefault(k) + v;

        base.Effectuate(owner);
    }

    /// <summary>
    /// 交错
    /// </summary>
    public virtual void Interleave() => Effectuate();

    public override void Expire()
    {
        foreach (var (k, v) in Rrc) TbRace.corr[k] = TbRace.corr.GetValueOrDefault(k) - v;
        foreach (var (k, v) in Trc) TbTemplate.corr[k] = TbTemplate.corr.GetValueOrDefault(k) - v;
        foreach (var (k, v) in Arc) TbAdventure.corr[k] = TbAdventure.corr.GetValueOrDefault(k) - v;
        base.Expire();
    }

    /// <summary>
    /// 解析
    /// </summary>
    private static void Parse(List<string> list, Dictionary<string, int> dic)
    {
        foreach (string item in list) 
        {
            (string s, int x) = (item.Split('-')[0], int.Parse(item.Split('-')[1]));
            dic[s] = -x;
        }
    }
}