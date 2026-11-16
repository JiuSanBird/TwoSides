/// <summary>
/// 单位
/// </summary>
public partial class Unit : IHaveName, IHaveLabel
{
    public Unit(TemplateCfg cfg)
    {
        SetTemplate(cfg);
    }

    public string Name { get; set; }
    /// <summary>反应</summary>
    public WValue Response { get; set; }
}
