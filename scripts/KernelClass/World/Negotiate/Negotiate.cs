/// <summary>
/// 交谈
/// </summary>
public class Negotiate : IHaveName
{
    public Negotiate(NegotiateCfg cfg)
    {
        Cfg = cfg;
    }

    private NegotiateCfg Cfg { get; set; }
    public string Name => Cfg.Name;
}