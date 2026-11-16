using System;
using System.Collections.Generic;

/// <summary>
/// 词条
/// </summary>
public class Entry : Trigger, IRarity, IHaveName, IHaveLabel, IDescribe
{
    public Entry(EntryCfg cfg)
    {
        Cfg = cfg;
    }

    public EntryCfg Cfg { get; init; }

    public string Id => Cfg.Id;
    public string Name => Cfg.Name;
    public int Rarity => Cfg.Rarity;
    public List<EntryLabel> Label => Cfg.Label;
    public List<int> Par => Cfg.Par;
    public string PrimaryDescribe => Cfg.Describe;

    public bool HasLabel(EntryLabel label) => Cfg.Label != null && Cfg.Label.Contains(label);

    public static Entry GetByCfg(EntryCfg cfg)
    {
        if (cfg == null || string.IsNullOrWhiteSpace(cfg.ClassName) || cfg.ClassName == "无")
        {
            return null;
        }

        Type type = typeof(Entry).Assembly.GetType(cfg.ClassName);

        if (type == null || !typeof(Entry).IsAssignableFrom(type))
        {
            return null;
        }

        return (Entry)Activator.CreateInstance(type, cfg);
    }
}
