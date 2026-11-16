/// <summary>
/// 常态核心
/// </summary>
public class Normalcy(CoreCfg coreCfg) : Core(coreCfg)
{
    protected override void ReplenishEffectuate(Unit owner)
    {
        BindAndUntie(owner.HPMax, value => value.Addition += Par[0]);
        BindAndUntie(owner.ATK, value => value.Addition += Par[1]);
    }
}