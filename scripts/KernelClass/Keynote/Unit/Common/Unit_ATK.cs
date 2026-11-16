/// <summary>
/// Unit_进攻部分
/// </summary>
public partial class Unit
{
    /// <summary>攻击力 </summary>
    public WValue ATK { get; set; } = new WValue(20, 0, 1000);
    /// <summary>系数</summary>
    public WValue ATKCoefficient { get; set; } = new WValue(1, 1, 1000);

    /// <summary>攻击距离</summary>
    public WValue ATKDistance { get; set; } = new WValue(1, 0, 10);
}