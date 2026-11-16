using System.Collections.Generic;

/// <summary>
/// Unit_模板管理
/// 切换模板,会根据模板改变净秽态
/// 切换净秽态,会随机获取一个对应模板, 但是这个一旦生成, 就锁死
/// </summary>
public partial class Unit
{
    /// <summary>当前模板</summary>
    private TemplateCfg Cfg { get; set; }
    /// <summary>当前净秽态</summary>
    public MentalState MentalState { get; private set; }
    /// <summary>
    /// 每个净秽态对应的模板
    /// 0: 常态 1:秽土 2:净土
    /// </summary>
    public Dictionary<MentalState, TemplateCfg> AllCorrCfg { get; private set; } = new();

    public void SetTemplate(TemplateCfg cfg) => SetTemplate(cfg, cfg.Ms);
    /// <summary>
    /// 改变模板
    /// </summary>
    public void SetTemplate(TemplateCfg cfg, MentalState mental)
    {
        if (cfg != null)
        {
            LoseTemplate();
        }
        MentalState = mental;
        AllCorrCfg[mental] = cfg;

        HPMax.SetBasics(cfg.HpCoef);
        ATKCoefficient.SetBasics(cfg.AtkCoef);
        ATKDistance.SetBasics(cfg.AtkDistance);

        Cfg = cfg;
        
        // ParseState(Cfg.State);
    }

    /// <summary>
    /// 失去模板
    /// </summary>
    private void LoseTemplate()
    {
        
    }

    /// <summary>
    /// 被考验改变净秽态
    /// </summary>
    /// <param name="mental">目标净秽态</param>
    public void ChangeByOrdeal(MentalState mental)
    {
        // 如果已经有对应净秽态的模板, 就直接切换过去
        if (AllCorrCfg.ContainsKey(mental)) 
        {
            SetTemplate(AllCorrCfg[mental]);
        }
        // 否则随机生成一个
        // SetTemplate(cfg, mental);
    }
}
