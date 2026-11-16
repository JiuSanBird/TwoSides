/// <summary>
/// 成就
/// </summary>
public class Achievement : IHaveName, IHaveIcon
{
    public Achievement(AchievementCfg cfg)
    {
        Cfg = cfg;
    }

    public AchievementCfg Cfg { get; private set; }
    public string Name => Cfg.Name;
}