using System.Collections.Generic;

/// <summary>
/// 考验
/// </summary>
public class Ordeal : IHaveName, IDescribe
{
    public Ordeal(OrdealCfg cfg)
    {
        Cfg = cfg;
    }

    private OrdealCfg Cfg { get; set; }
    public string Name => Cfg.Name;
    public List<int> Par => Cfg.Par;
    public string PrimaryDescribe => Cfg.Describe;
}