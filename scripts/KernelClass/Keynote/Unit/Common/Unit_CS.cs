/// <summary>
/// Unit_暴击部分
/// </summary>
public partial class Unit
{
    /// <summary>暴击概率</summary>
    public WValue CSChance { get; set; } = new WValue(10, 0, 300);
    /// <summary>暴击倍率</summary>
    public WValue CSRate { get; set; } = new WValue(150, 100, 1000);


}